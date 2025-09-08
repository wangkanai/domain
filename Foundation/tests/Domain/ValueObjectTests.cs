// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Foundation;

public class ValueObjectTests
{
   [Fact]
   public void ValueObject_Equals_ShouldBeTrue()
   {
      var value1 = new Address("123 Main St", "Redmond", "WA", "98052");
      var value2 = new Address("123 Main St", "Redmond", "WA", "98052");
      Assert.True(value1.Equals(value2));
   }

   [Fact]
   public void ValueObject_Equals_ShouldBeFalse()
   {
      var value1 = new Address("123 Main St", "Redmond", "WA", "98052");
      var value2 = new Address("123 Main St", "Redmond", "WA", "98053");
      Assert.False(value1.Equals(value2));
   }

   [Fact]
   public void ValueObject_Equals_ShouldBeFalse_WhenNull()
   {
      var value1 = new Address("123 Main St", "Redmond", "WA", "98052");
      Assert.False(value1.Equals(null));
   }

   [Fact]
   public void ValueObject_Equals_ShouldBeFalse_WhenDifferentType()
   {
      var value1 = new Address("123 Main St", "Redmond", "WA", "98052");
      Assert.False(value1.Equals(new()));
   }

   [Fact]
   public void ValueObject_GetHashCode_ShouldBeTrue()
   {
      var value1 = new Address("123 Main St", "Redmond", "WA", "98052");
      var value2 = new Address("123 Main St", "Redmond", "WA", "98052");
      Assert.Equal(value1.GetHashCode(), value2.GetHashCode());
   }

   [Fact]
   public void ValueObject_GetHashCode_ShouldBeFalse()
   {
      var value1 = new Address("123 Main St", "Redmond", "WA", "98052");
      var value2 = new Address("123 Main St", "Redmond", "WA", "98053");
      Assert.NotEqual(value1.GetHashCode(), value2.GetHashCode());
   }
}

public class Address(string street, string city, string state, string zip) : ValueObject
{
   public string? Street { get; set; } = street;
   public string? City   { get; set; } = city;
   public string? State  { get; set; } = state;
   public string? Zip    { get; set; } = zip;
}