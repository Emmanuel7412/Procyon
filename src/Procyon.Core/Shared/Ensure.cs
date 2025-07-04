﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Core.Shared
{
    public static class Ensure
    {
        public static void NotNullOrEmpty(
            [NotNull] string? value,
            [CallerArgumentExpression(nameof(value))] string? paramName = default)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
