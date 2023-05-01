﻿using System.Globalization;

namespace ADS.BabySleepSounds.Server.Helpers;

public static class Constants
{
    public const string CorsPolicy = nameof(CorsPolicy);
    public static readonly CultureInfo[] SupportedCultures = new CultureInfo[] { new("en-US"), new("pt-BR"), new("es-ES") };
    public static readonly CultureInfo DefaultCulture = SupportedCultures[0];
}
