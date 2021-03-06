// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Data
{
    using WixToolset.Data.Tuples;

    public static partial class TupleDefinitions
    {
        public static readonly IntermediateTupleDefinition WixBootstrapperApplication = new IntermediateTupleDefinition(
            TupleDefinitionType.WixBootstrapperApplication,
            new[]
            {
                new IntermediateFieldDefinition(nameof(WixBootstrapperApplicationTupleFields.Id), IntermediateFieldType.String),
            },
            typeof(WixBootstrapperApplicationTuple));
    }
}

namespace WixToolset.Data.Tuples
{
    public enum WixBootstrapperApplicationTupleFields
    {
        Id,
    }

    public class WixBootstrapperApplicationTuple : IntermediateTuple
    {
        public WixBootstrapperApplicationTuple() : base(TupleDefinitions.WixBootstrapperApplication, null, null)
        {
        }

        public WixBootstrapperApplicationTuple(SourceLineNumber sourceLineNumber, Identifier id = null) : base(TupleDefinitions.WixBootstrapperApplication, sourceLineNumber, id)
        {
        }

        public IntermediateField this[WixBootstrapperApplicationTupleFields index] => this.Fields[(int)index];

        public string Id
        {
            get => (string)this.Fields[(int)WixBootstrapperApplicationTupleFields.Id]?.Value;
            set => this.Set((int)WixBootstrapperApplicationTupleFields.Id, value);
        }
    }
}