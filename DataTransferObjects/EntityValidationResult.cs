using System.ComponentModel.DataAnnotations;
using ArCell.NET.ARValidation.Interfaces;

namespace ArCell.NET.ARValidation.DataTransferObjects;

public class EntityValidationResult : IValidationResult
{
    public bool Validated { get; set; }
    public string Message { get; set; }
}