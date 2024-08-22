# Validation Library

This project provides a validation library for .NET. It offers advanced validation rules and functions to simplify data validation in application development.

## Features

- **Email Validation:** Checks the validity of email addresses.
- **Minimum and Maximum Length:** Validates the length of string data.
- **Pattern Matching:** Ensures data matches a specific pattern.
- **Custom Condition:** Validates based on user-defined custom conditions.
- **SEO Friendly:** Checks the accuracy of SEO-compliant content.
- **Phone Number Validation** Validates phone numbers by 49 countries phone number patterns.
- **Date Range** Ensures data is a DateTime object and between specified DateTime objects.
-  **And More** Validator has 40+ methods to validate data.

## Installation
- Install NuGet Package via Console or NuGet Package Manager.
  ```NuGet Install ArCell.NET.ARValidation```

## Creating Validators
Your validators should be inherited from BaseEntityValidator class.
```
public class ProductValidator : BaseEntityValidator<Product>{
  public override void ConfigureRules(Product entity)
  {
    CreateValidationRule(x => x.Name).MinLength(10).MaxLength(100).WithMessage("Product name must be between 10 and 100 characters.");
    CreateValidationRule(x => x.ColorHex).IsValidHexColor().WithMessage("Color is invalid.");
    CreateValidationRule(x => x.Price).HasMinValue(100);
  }
}
```
## Registering Validators
You should register validators as scoped.
```
services.AddValidators(true) //It will search all project and find all Validators and inject them as scoped.
//Or you can manually add validators which you want to use.
services.AddScoped<IEntityValidator<Product>, ProductValidator>();
```

## Validating Entities
Validators normally returns a EntityValidationResult object which implements IValidationResult interface.
```
var result = validator.Validate(product);
Console.WriteLine($"Success: {result.Validated} | Message: {result.Message}");
```
## Thanks For Your Attention
Thanks for your attention. This is my first public library. :)
