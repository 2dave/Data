// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Data
{
    using WixToolset.Data.Tuples;

    public static partial class TupleDefinitions
    {
        public static readonly IntermediateTupleDefinition WixBundleCatalog = new IntermediateTupleDefinition(
            TupleDefinitionType.WixBundleCatalog,
            new[]
            {
                new IntermediateFieldDefinition(nameof(WixBundleCatalogTupleFields.WixBundleCatalog), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixBundleCatalogTupleFields.Payload_), IntermediateFieldType.String),
            },
            typeof(WixBundleCatalogTuple));
    }
}

namespace WixToolset.Data.Tuples
{
    public enum WixBundleCatalogTupleFields
    {
        WixBundleCatalog,
        Payload_,
    }

    public class WixBundleCatalogTuple : IntermediateTuple
    {
        public WixBundleCatalogTuple() : base(TupleDefinitions.WixBundleCatalog, null, null)
        {
        }

        public WixBundleCatalogTuple(SourceLineNumber sourceLineNumber, Identifier id = null) : base(TupleDefinitions.WixBundleCatalog, sourceLineNumber, id)
        {
        }

        public IntermediateField this[WixBundleCatalogTupleFields index] => this.Fields[(int)index];

        public string WixBundleCatalog
        {
            get => (string)this.Fields[(int)WixBundleCatalogTupleFields.WixBundleCatalog]?.Value;
            set => this.Set((int)WixBundleCatalogTupleFields.WixBundleCatalog, value);
        }

        public string Payload_
        {
            get => (string)this.Fields[(int)WixBundleCatalogTupleFields.Payload_]?.Value;
            set => this.Set((int)WixBundleCatalogTupleFields.Payload_, value);
        }
    }
}