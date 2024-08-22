namespace ArCell.NET.ARValidation.Interfaces;

public interface IValidationResult
{
    public bool Validated { get; set; }
    public string Message { get; set; }
}