// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Data.WindowsInstaller
{
    using System;

    /// <summary>
    /// Exception thrown when a table definition is missing.
    /// </summary>
    [Serializable]
    public class WixMissingTableDefinitionException : WixException
    {
        /// <summary>
        /// Instantiate new WixMissingTableDefinitionException.
        /// </summary>
        /// <param name="error">Localized error information.</param>
        public WixMissingTableDefinitionException(Message error)
            : base(error)
        {
        }
    }
}
