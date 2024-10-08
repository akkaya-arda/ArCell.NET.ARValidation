using System;
using System.Collections.Generic;

namespace ArCell.NET.ARValidation.Constants;

public enum CountryCode
{
    US,
    CA,
    GB,
    DE,
    FR,
    IT,
    ES,
    AU,
    IN,
    CN,
    JP,
    BR,
    MX,
    RU,
    ZA,
    KR,
    AR,
    CL,
    CO,
    PE,
    VE,
    PH,
    SG,
    MY,
    TH,
    ID,
    PK,
    NG,
    EG,
    SA,
    IL,
    TR,
    UA,
    PL,
    SE,
    NO,
    FI,
    DK,
    BE,
    NL,
    AT,
    CH,
    LI,
    LU,
    MT,
    IS,
    IM,
    JE,
    GG
}

public static class PhoneNumberPatterns
{
    private static readonly IReadOnlyDictionary<CountryCode, string> CountryPhonePatterns = new Dictionary<CountryCode, string>
    {
        { CountryCode.US, @"^\+1\s?\(?\d{3}\)?\s?\d{3}-\d{4}$" },
        { CountryCode.CA, @"^\+1\s?\(?\d{3}\)?\s?\d{3}-\d{4}$" },
        { CountryCode.GB, @"^\+44\s?\d{4}\s?\d{6}$" },
        { CountryCode.DE, @"^\+49\s?\d{11}$" },
        { CountryCode.FR, @"^\+33\s?\d{1}\s?\d{2}\s?\d{2}\s?\d{2}\s?\d{2}$" },
        { CountryCode.IT, @"^\+39\s?\d{2}\s?\d{6,8}$" },
        { CountryCode.ES, @"^\+34\s?\d{9}$" },
        { CountryCode.AU, @"^\+61\s?\d{9}$" },
        { CountryCode.IN, @"^\+91\s?\d{10}$" },
        { CountryCode.CN, @"^\+86\s?\d{11}$" },
        { CountryCode.JP, @"^\+81\s?\d{10}$" },
        { CountryCode.BR, @"^\+55\s?\d{2}\s?\d{8,9}$" },
        { CountryCode.MX, @"^\+52\s?\d{10}$" },
        { CountryCode.RU, @"^\+7\s?\d{10}$" },
        { CountryCode.ZA, @"^\+27\s?\d{9}$" },
        { CountryCode.KR, @"^\+82\s?\d{10}$" },
        { CountryCode.AR, @"^\+54\s?\d{10}$" },
        { CountryCode.CL, @"^\+56\s?\d{9}$" },
        { CountryCode.CO, @"^\+57\s?\d{10}$" },
        { CountryCode.PE, @"^\+51\s?\d{9}$" },
        { CountryCode.VE, @"^\+58\s?\d{10}$" },
        { CountryCode.PH, @"^\+63\s?\d{10}$" },
        { CountryCode.SG, @"^\+65\s?\d{8}$" },
        { CountryCode.MY, @"^\+60\s?\d{10}$" },
        { CountryCode.TH, @"^\+66\s?\d{9}$" },
        { CountryCode.ID, @"^\+62\s?\d{10,13}$" },
        { CountryCode.PK, @"^\+92\s?\d{10}$" },
        { CountryCode.NG, @"^\+234\s?\d{10}$" },
        { CountryCode.EG, @"^\+20\s?\d{10}$" },
        { CountryCode.SA, @"^\+966\s?\d{9}$" },
        { CountryCode.IL, @"^\+972\s?\d{9}$" },
        { CountryCode.TR, @"^\+90\s?\d{10}$" },
        { CountryCode.UA, @"^\+380\s?\d{9,10}$" },
        { CountryCode.PL, @"^\+48\s?\d{9}$" },
        { CountryCode.SE, @"^\+46\s?\d{9}$" },
        { CountryCode.NO, @"^\+47\s?\d{8}$" },
        { CountryCode.FI, @"^\+358\s?\d{6,10}$" },
        { CountryCode.DK, @"^\+45\s?\d{8}$" },
        { CountryCode.BE, @"^\+32\s?\d{8,9}$" },
        { CountryCode.NL, @"^\+31\s?\d{9}$" },
        { CountryCode.AT, @"^\+43\s?\d{4,13}$" },
        { CountryCode.CH, @"^\+41\s?\d{9}$" },
        { CountryCode.LI, @"^\+423\s?\d{6,8}$" },
        { CountryCode.LU, @"^\+352\s?\d{6,8}$" },
        { CountryCode.MT, @"^\+356\s?\d{8}$" },
        { CountryCode.IS, @"^\+354\s?\d{6}$" },
        { CountryCode.IM, @"^\+44\s?1624\s?\d{6}$" },
        { CountryCode.JE, @"^\+44\s?1534\s?\d{6}$" },
        { CountryCode.GG, @"^\+44\s?1481\s?\d{6}$" }
    };

    public static string? GetPhoneNumberPattern(CountryCode countryCode)
    {
        return CountryPhonePatterns.TryGetValue(countryCode, out var pattern) ? pattern : null;
    }
}
