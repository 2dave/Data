﻿// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Data
{
    using System;

    public static class IntermediateFieldValueExtensions
    {
        public static bool AsBool(this IntermediateFieldValue value)
        {
            var result = value.AsNullableBool();
            return result.HasValue && result.Value;
        }

        public static bool? AsNullableBool(this IntermediateFieldValue value)
        {
            if (value?.Data == null)
            {
                return null;
            }
            else if (value.Data is bool b)
            {
                return b;
            }
            else if (value.Data is int n)
            {
                return n != 0;
            }
            else if (value.Data is string s)
            {
                if (s.Equals("yes", StringComparison.OrdinalIgnoreCase) || s.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (s.Equals("no", StringComparison.OrdinalIgnoreCase) || s.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return (bool)value.Data;
        }

        public static int AsNumber(this IntermediateFieldValue value)
        {
            var result = value.AsNullableNumber();
            return result ?? 0;
        }

        public static int? AsNullableNumber(this IntermediateFieldValue value)
        {
            if (value?.Data == null)
            {
                return null;
            }
            else if (value.Data is int n)
            {
                return n;
            }
            else if (value.Data is bool b)
            {
                return b ? 1 : 0;
            }
            else if (value.Data is string s)
            {
                return Convert.ToInt32(s);
            }

            return (int)value.Data;
        }

        public static IntermediateFieldPathValue AsPath(this IntermediateFieldValue value)
        {
            return (IntermediateFieldPathValue)value?.Data;
        }

        public static string AsString(this IntermediateFieldValue value)
        {
            if (value?.Data == null)
            {
                return null;
            }
            else if (value.Data is string s)
            {
                return s;
            }
            else if (value.Data is int n)
            {
                return n.ToString();
            }
            else if (value.Data is bool b)
            {
                return b ? "true" : "false";
            }
            else if (value.Data is IntermediateFieldPathValue p)
            {
                return p.Path;
            }

            return (string)value.Data;
        }
    }
}
