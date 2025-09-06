// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Audit;

/// <summary>Represents an entity that is auditable, tracking creation and update timestamps.</summary>
/// <remarks>
/// This interface combines the functionality of <see cref="ICreatedEntity"/> and
/// <see cref="IUpdatedEntity"/> to provide a standard structure for auditing entities.
/// </remarks>
public interface IAuditableEntity : ICreatedEntity, IUpdatedEntity;