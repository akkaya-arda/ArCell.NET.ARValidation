using System.Net.Mail;
using System.Text.RegularExpressions;
using ArCell.NET.ARValidation.Constants;
using ArCell.NET.ARValidation.DataTransferObjects;
using ArCell.NET.ARValidation.Interfaces;

namespace ArCell.NET.ARValidation.RuleDefinition;

public class EntityValidationRule<TEntity>(Func<TEntity, object> fieldSelector)
    where TEntity : class
{
    public List<Func<object, IValidationResult>> FieldValidatorFunctions = new();
    private string? Message;

    public object GetFieldValue(TEntity entity)
    {
        return fieldSelector(entity);
    }
    
    public EntityValidationRule<TEntity> IsEmailValid()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var email = c as string;

                if (new MailAddress(email).Address != email)
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "Email address is not valid."
                    };
                }
                
                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> MinLength(int minLength)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var value = c as string;
                if (value != null && value.Length < minLength)
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? $"The value must be at least {minLength} characters long."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> MaxLength(int maxLength)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var value = c as string;
                if (value != null && value.Length > maxLength)
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? $"The value must not exceed {maxLength} characters."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsSeoFriendly()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var value = c as string;
                var seoFriendly = Regex.IsMatch(value ?? string.Empty, @"^[a-z0-9-]+$");
                
                if (!seoFriendly)
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not SEO-friendly."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> MatchesPattern(string pattern)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var value = c as string;
                if (value != null && !Regex.IsMatch(value, pattern))
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value does not match the specified pattern."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> Must(Func<object, bool> condition)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                if (!condition(c))
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value does not satisfy the condition."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsNotNull()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                if (c == null)
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value cannot be null."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsNotEmpty()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var value = c as string;
                if (string.IsNullOrWhiteSpace(value))
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value cannot be empty."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsInRange(int min, int max)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                if (c is int value)
                {
                    if (value < min || value > max)
                    {
                        return new EntityValidationResult()
                        {
                            Validated = false,
                            Message = Message ?? $"The value must be between {min} and {max}."
                        };
                    }
                }
                else
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not an integer."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsGreaterThan(int min)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                if (c is int value)
                {
                    if (value <= min)
                    {
                        return new EntityValidationResult()
                        {
                            Validated = false,
                            Message = Message ?? $"The value must be greater than {min}."
                        };
                    }
                }
                else
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not an integer."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsLessThan(int max)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                if (c is int value)
                {
                    if (value >= max)
                    {
                        return new EntityValidationResult()
                        {
                            Validated = false,
                            Message = Message ?? $"The value must be less than {max}."
                        };
                    }
                }
                else
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not an integer."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsDateInRange(DateTime minDate, DateTime maxDate)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                if (c is DateTime value)
                {
                    if (value < minDate || value > maxDate)
                    {
                        return new EntityValidationResult()
                        {
                            Validated = false,
                            Message = Message ?? $"The date must be between {minDate:yyyy-MM-dd} and {maxDate:yyyy-MM-dd}."
                        };
                    }
                }
                else
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not a date."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsFutureDate()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                if (c is DateTime value)
                {
                    if (value <= DateTime.Now)
                    {
                        return new EntityValidationResult()
                        {
                            Validated = false,
                            Message = Message ?? "The date must be in the future."
                        };
                    }
                }
                else
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not a date."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsPastDate()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                if (c is DateTime value)
                {
                    if (value >= DateTime.Now)
                    {
                        return new EntityValidationResult()
                        {
                            Validated = false,
                            Message = Message ?? "The date must be in the past."
                        };
                    }
                }
                else
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not a date."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsValidPhoneNumber(CountryCode countryCode)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var phone = c as string;
                var pattern = PhoneNumberPatterns.GetPhoneNumberPattern(countryCode);

                if (!Regex.IsMatch(phone ?? string.Empty, pattern))
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The phone number is not valid."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsValidUrl()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var url = c as string;

                if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult) || uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps)
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The URL is not valid."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsNotEqualTo(object value)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                if (Equals(c, value))
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value must not be equal to the specified value."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsEqualTo(object value)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                if (!Equals(c, value))
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value must be equal to the specified value."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsAlphanumeric()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var value = c as string;
                if (value != null && !Regex.IsMatch(value, @"^[a-zA-Z0-9]+$"))
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value must be alphanumeric."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsAlpha()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var value = c as string;
                if (value != null && !Regex.IsMatch(value, @"^[a-zA-Z]+$"))
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value must contain only alphabetic characters."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsNumeric()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var value = c as string;
                if (value != null && !Regex.IsMatch(value, @"^\d+$"))
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value must be numeric."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsWithinRange(decimal min, decimal max)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                if (c is decimal value)
                {
                    if (value < min || value > max)
                    {
                        return new EntityValidationResult()
                        {
                            Validated = false,
                            Message = Message ?? $"The value must be between {min} and {max}."
                        };
                    }
                }
                else
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not a decimal number."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> HasMinValue(decimal minValue)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                decimal d = decimal.Parse(c.ToString());
                if (d is decimal value)
                {
                    if (value < minValue)
                    {
                        return new EntityValidationResult()
                        {
                            Validated = false,
                            Message = Message ?? $"The value must be at least {minValue}."
                        };
                    }
                }
                else
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not a decimal number."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> HasMaxValue(decimal maxValue)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                if (c is decimal value)
                {
                    if (value > maxValue)
                    {
                        return new EntityValidationResult()
                        {
                            Validated = false,
                            Message = Message ?? $"The value must not exceed {maxValue}."
                        };
                    }
                }
                else
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not a decimal number."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsDateBefore(DateTime date)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                if (c is DateTime value)
                {
                    if (value >= date)
                    {
                        return new EntityValidationResult()
                        {
                            Validated = false,
                            Message = Message ?? $"The date must be before {date:yyyy-MM-dd}."
                        };
                    }
                }
                else
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not a date."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsDateAfter(DateTime date)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                if (c is DateTime value)
                {
                    if (value <= date)
                    {
                        return new EntityValidationResult()
                        {
                            Validated = false,
                            Message = Message ?? $"The date must be after {date:yyyy-MM-dd}."
                        };
                    }
                }
                else
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not a date."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsValidCurrency()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var value = c as string;
                var pattern = @"^\d+(\.\d{2})?$"; // Simple currency format

                if (!Regex.IsMatch(value ?? string.Empty, pattern))
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not a valid currency."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsValidJson()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var json = c as string;

                try
                {
                    var jsonObject = System.Text.Json.JsonDocument.Parse(json);
                }
                catch
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not a valid JSON."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsValidXml()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var xml = c as string;

                try
                {
                    var xmlDocument = new System.Xml.XmlDocument();
                    xmlDocument.LoadXml(xml);
                }
                catch
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not a valid XML."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsValidUuid()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var uuid = c as string;

                if (!Guid.TryParse(uuid, out _))
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not a valid UUID."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsValidEnum<TEnum>()
        where TEnum : struct, Enum
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                if (Enum.IsDefined(typeof(TEnum), c))
                {
                    return new EntityValidationResult()
                    {
                        Validated = true,
                        Message = "Entity validated successfully.",
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = false,
                    Message = Message ?? "The value is not a valid enum."
                };
            }
        );

        return this;
    }

    /// <summary>
    /// Valid genders: Male, Female, Non-Binary, Other.
    /// </summary>
    /// <returns></returns>
    public EntityValidationRule<TEntity> IsValidGender()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var gender = c as string;
                var validGenders = new[] { "Male", "Female", "Non-Binary", "Other" };

                if (Array.IndexOf(validGenders, gender) < 0)
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not a valid gender."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsValidTimezone()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var timezone = c as string;

                if (TimeZoneInfo.FindSystemTimeZoneById(timezone) == null)
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The timezone is not valid."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsValidPassword()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var password = c as string;
                var hasUpperChar = Regex.IsMatch(password ?? string.Empty, @"[A-Z]");
                var hasLowerChar = Regex.IsMatch(password ?? string.Empty, @"[a-z]");
                var hasNumeric = Regex.IsMatch(password ?? string.Empty, @"\d");
                var hasSpecialChar = Regex.IsMatch(password ?? string.Empty, @"[\W_]");

                if (!(hasUpperChar && hasLowerChar && hasNumeric && hasSpecialChar))
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The password must contain uppercase, lowercase, numeric, and special characters."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsValidHexColor()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var color = c as string;
                var pattern = @"^#[0-9A-Fa-f]{6}$"; // Hex color format

                if (!Regex.IsMatch(color ?? string.Empty, pattern))
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not a valid hex color."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsValidIpv4Address()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var ip = c as string;
                var pattern = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\." +
                              @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\." +
                              @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\." +
                              @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"; // IPv4 pattern

                if (!Regex.IsMatch(ip ?? string.Empty, pattern))
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The IP address is not a valid IPv4 address."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsValidIpv6Address()
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var ip = c as string;
                var pattern = @"^(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|" +
                              @"([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:" +
                              @"[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|"+
                              @"([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|"+
                              @"([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|"+
                              @"([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|"+
                              @"[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|"+
                              @":((:[0-9a-fA-F]{1,4}){1,7}|:)|"+
                              @"fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|"+
                              @"::(ffff(:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){1,4}" +
                              @"([0-9a-fA-F]{1,4}|:)|"+
                              @"([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){1,4}"+
                              @"([0-9a-fA-F]{1,4}|:))$"; // IPv6 pattern

                if (!Regex.IsMatch(ip ?? string.Empty, pattern))
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The IP address is not a valid IPv6 address."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsValidDateRange(DateTime minDate, DateTime maxDate)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                if (c is DateTime value)
                {
                    if (value < minDate || value > maxDate)
                    {
                        return new EntityValidationResult()
                        {
                            Validated = false,
                            Message = Message ?? $"The date must be between {minDate:yyyy-MM-dd} and {maxDate:yyyy-MM-dd}."
                        };
                    }
                }
                else
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value is not a date."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> IsValidRegex(string pattern)
    {
        FieldValidatorFunctions.Add(
            c =>
            {
                var value = c as string;
                if (!Regex.IsMatch(value ?? string.Empty, pattern))
                {
                    return new EntityValidationResult()
                    {
                        Validated = false,
                        Message = Message ?? "The value does not match the specified pattern."
                    };
                }

                return new EntityValidationResult()
                {
                    Validated = true,
                    Message = "Entity validated successfully.",
                };
            }
        );

        return this;
    }

    public EntityValidationRule<TEntity> WithMessage(string message)
    {
        Message = message;
        return this;
    }
}
