﻿// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.ComponentModel.DataAnnotations;

namespace Wangkanai.Audit;

/// <summary>
/// Represents an abstract base class for auditable entities that captures user-related information during create and update operations.
/// This class extends the <see cref="AuditableEntity{T}"/> class and adheres to the <see cref="IUserAuditableEntity"/> interface.
/// </summary>
/// <typeparam name="T">
/// The type of the entity identifier, which must implement <see cref="IComparable{T}"/> and <see cref="IEquatable{T}"/>.
/// </typeparam>
public abstract class UserAuditableEntity<T>
   : AuditableEntity<T>, IUserAuditableEntity
   where T : IComparable<T>, IEquatable<T>
{
   [StringLength(128)]
   public string? CreatedBy { get; set; }

   [StringLength(128)]
   public string? UpdatedBy { get; set; }


   public virtual bool ShouldSerializeCreatedBy()
      => ShouldSerializeAuditableProperties;

   public virtual bool ShouldSerializeUpdatedBy()
      => ShouldSerializeAuditableProperties;
}