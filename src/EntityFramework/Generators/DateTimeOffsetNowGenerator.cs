// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Diagnostics;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Wangkanai.EntityFramework.Generators;

public sealed class DateTimeOffsetNowGenerator : ValueGenerator<DateTimeOffset>
{
   public override bool GeneratesTemporaryValues => false;

   public override DateTimeOffset Next(EntityEntry entry)
   {
      ArgumentNullException.ThrowIfNull(entry);
      return DateTimeOffset.Now;
   }
}