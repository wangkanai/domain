﻿// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;

namespace Wangkanai.Domain;

/// <summary>Represents an abstract base class for value objects in the domain-driven design context. A value object is an immutable conceptual object that is compared based on its property values rather than a unique identity.</summary>
/// <remarks>Value objects provide a way to encapsulate and model domain concepts with specific attributes, ensuring immutability and equality based on their state. The class implements
/// <see cref="IValueObject"/> for domain definition, <see cref="ICacheKey"/> to enable caching based on object states, and
/// <see cref="ICloneable"/> to support shallow copying of the object.</remarks>
public abstract class ValueObject : IValueObject, ICacheKey, ICloneable
{
   private static readonly ConcurrentDictionary<Type, IReadOnlyCollection<PropertyInfo>> TypeProperties = new();

   public virtual string GetCacheKey()
   {
      var keyValues = GetEqualityComponents()
                     .Select(x => x is string ? $"'{x}'" : x)
                     .Select(x => x is ICacheKey cacheKey ? cacheKey.GetCacheKey() : x?.ToString());

      return string.Join("|", keyValues);
   }

   public object Clone()
      => MemberwiseClone();

   public override bool Equals(object? obj)
   {
      if (ReferenceEquals(this, obj))
      {
         return true;
      }

      if (ReferenceEquals(null, obj))
      {
         return false;
      }

      if (GetType() != obj.GetType())
      {
         return false;
      }

      var other = obj as ValueObject;
      return other is not null && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
   }

   public override int GetHashCode()
   {
      unchecked
      {
         return GetEqualityComponents()
           .Aggregate(17, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));
      }
   }

   public static bool operator ==(ValueObject left, ValueObject right)
      => Equals(left, right);

   public static bool operator !=(ValueObject left, ValueObject right)
      => !Equals(left, right);

   public override string ToString()
      => $"{{{string.Join(", ", GetProperties().Select(f => $"{f.Name}: {f.GetValue(this)}"))}}}";

   public virtual IEnumerable<PropertyInfo> GetProperties()
      => TypeProperties.GetOrAdd(GetType(), t => t.GetTypeInfo().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                       .OrderBy(p => p.Name)
                       .ToList();

   protected virtual IEnumerable<object> GetEqualityComponents()
   {
      foreach (var property in GetProperties())
      {
         var value = property.GetValue(this);
         if (value is null)
         {
            yield return null!;
         }
         else
         {
            var valueType = value.GetType();
            if (valueType.IsAssignableFromGenericList())
            {
               yield return '[';
               foreach (var child in (IEnumerable)value)
                  yield return child;

               yield return ']';
            }
            else
            {
               yield return value;
            }
         }
      }
   }
}