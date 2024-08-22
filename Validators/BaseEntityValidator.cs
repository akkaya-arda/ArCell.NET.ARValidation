using ArCell.NET.ARValidation.Collections;
using ArCell.NET.ARValidation.DataTransferObjects;
using ArCell.NET.ARValidation.Interfaces;
using ArCell.NET.ARValidation.RuleDefinition;

namespace ArCell.NET.ARValidation.Validators;

public abstract class BaseEntityValidator<TEntity> : IEntityValidator<TEntity>
    where TEntity : class
{
    protected EntityValidationRuleCollection<TEntity> _validationRules = new();

    public abstract void ConfigureRules(TEntity entity);
    
    public virtual IValidationResult Validate(TEntity entity)
    {
        ConfigureRules(entity);
        return _validationRules.ExecuteRules(entity);
    }

    public virtual async Task<IValidationResult> ValidateAsync(TEntity entity)
    {
        ConfigureRules(entity);
        var result = await Task.Run(() => _validationRules.ExecuteRules(entity)); //The processes are CPU-bound. So run it on another thread and wait until it's done.
        return result;
    }

    protected EntityValidationRule<TEntity> CreateValidationRule(Func<TEntity, object> fieldSelector)
    {
        var rule = new EntityValidationRule<TEntity>(fieldSelector);
        _validationRules.AddRule(rule);
        return rule;
    }
}