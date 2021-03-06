// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Data
{
    using WixToolset.Data.Tuples;

    public static partial class TupleDefinitions
    {
        public static readonly IntermediateTupleDefinition WixInstanceTransforms = new IntermediateTupleDefinition(
            TupleDefinitionType.WixInstanceTransforms,
            new[]
            {
                new IntermediateFieldDefinition(nameof(WixInstanceTransformsTupleFields.Id), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixInstanceTransformsTupleFields.PropertyId), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixInstanceTransformsTupleFields.ProductCode), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixInstanceTransformsTupleFields.ProductName), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixInstanceTransformsTupleFields.UpgradeCode), IntermediateFieldType.String),
            },
            typeof(WixInstanceTransformsTuple));
    }
}

namespace WixToolset.Data.Tuples
{
    public enum WixInstanceTransformsTupleFields
    {
        Id,
        PropertyId,
        ProductCode,
        ProductName,
        UpgradeCode,
    }

    public class WixInstanceTransformsTuple : IntermediateTuple
    {
        public WixInstanceTransformsTuple() : base(TupleDefinitions.WixInstanceTransforms, null, null)
        {
        }

        public WixInstanceTransformsTuple(SourceLineNumber sourceLineNumber, Identifier id = null) : base(TupleDefinitions.WixInstanceTransforms, sourceLineNumber, id)
        {
        }

        public IntermediateField this[WixInstanceTransformsTupleFields index] => this.Fields[(int)index];

        public string Id
        {
            get => (string)this.Fields[(int)WixInstanceTransformsTupleFields.Id]?.Value;
            set => this.Set((int)WixInstanceTransformsTupleFields.Id, value);
        }

        public string PropertyId
        {
            get => (string)this.Fields[(int)WixInstanceTransformsTupleFields.PropertyId]?.Value;
            set => this.Set((int)WixInstanceTransformsTupleFields.PropertyId, value);
        }

        public string ProductCode
        {
            get => (string)this.Fields[(int)WixInstanceTransformsTupleFields.ProductCode]?.Value;
            set => this.Set((int)WixInstanceTransformsTupleFields.ProductCode, value);
        }

        public string ProductName
        {
            get => (string)this.Fields[(int)WixInstanceTransformsTupleFields.ProductName]?.Value;
            set => this.Set((int)WixInstanceTransformsTupleFields.ProductName, value);
        }

        public string UpgradeCode
        {
            get => (string)this.Fields[(int)WixInstanceTransformsTupleFields.UpgradeCode]?.Value;
            set => this.Set((int)WixInstanceTransformsTupleFields.UpgradeCode, value);
        }
    }
}