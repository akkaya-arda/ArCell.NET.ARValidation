namespace ArCell.NET.ARValidation.Interfaces;

/// <summary>
/// An interface for validating entities.
/// </summary>
public interface IEntityValidator
{
}

public interface IEntityValidator<in TEntity> : IEntityValidator
    where TEntity : class
{
    IValidationResult Validate(TEntity entity);
    Task<IValidationResult> ValidateAsync(TEntity entity);
}