using ArCell.NET.ARValidation.DataTransferObjects;
using ArCell.NET.ARValidation.Interfaces;
using ArCell.NET.ARValidation.RuleDefinition;

namespace ArCell.NET.ARValidation.Collections;

public class EntityValidationRuleCollection<TEntity> : List<EntityValidationRule<TEntity>> where TEntity : class 
{
    public void AddRule(EntityValidationRule<TEntity> rule)
    {
        this.Add(rule);
    }

    public void AddRules(List<EntityValidationRule<TEntity>> rules)
    {
        this.AddRange(rules);
    }

    public IValidationResult ExecuteRules(TEntity entityToValidate)
    {
        foreach (var rule in this)
        {
            var valueToApplyRule = rule.GetFieldValue(entityToValidate);
            foreach (var ruleFieldValidatorFunction in rule.FieldValidatorFunctions)
            {
                var result = ruleFieldValidatorFunction.Invoke(valueToApplyRule);
                if (!result.Validated)
                    return result;
            }
        }

        return new EntityValidationResult()
        {
            Validated = true,
            Message = "Entity validation rules succeeded."
        };
    }
}