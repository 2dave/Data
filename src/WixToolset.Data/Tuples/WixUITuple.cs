// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Data
{
    using WixToolset.Data.Tuples;

    public static partial class TupleDefinitions
    {
        public static readonly IntermediateTupleDefinition WixUI = new IntermediateTupleDefinition(
            TupleDefinitionType.WixUI,
            new[]
            {
                new IntermediateFieldDefinition(nameof(WixUITupleFields.WixUI), IntermediateFieldType.String),
            },
            typeof(WixUITuple));
    }
}

namespace WixToolset.Data.Tuples
{
    public enum WixUITupleFields
    {
        WixUI,
    }

    public class WixUITuple : IntermediateTuple
    {
        public WixUITuple() : base(TupleDefinitions.WixUI, null, null)
        {
        }

        public WixUITuple(SourceLineNumber sourceLineNumber, Identifier id = null) : base(TupleDefinitions.WixUI, sourceLineNumber, id)
        {
        }

        public IntermediateField this[WixUITupleFields index] => this.Fields[(int)index];

        public string WixUI
        {
            get => (string)this.Fields[(int)WixUITupleFields.WixUI]?.Value;
            set => this.Set((int)WixUITupleFields.WixUI, value);
        }
    }
}