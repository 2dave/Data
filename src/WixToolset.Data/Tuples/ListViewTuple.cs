// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Data
{
    using WixToolset.Data.Tuples;

    public static partial class TupleDefinitions
    {
        public static readonly IntermediateTupleDefinition ListView = new IntermediateTupleDefinition(
            TupleDefinitionType.ListView,
            new[]
            {
                new IntermediateFieldDefinition(nameof(ListViewTupleFields.Property), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(ListViewTupleFields.Order), IntermediateFieldType.Number),
                new IntermediateFieldDefinition(nameof(ListViewTupleFields.Value), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(ListViewTupleFields.Text), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(ListViewTupleFields.Binary_), IntermediateFieldType.String),
            },
            typeof(ListViewTuple));
    }
}

namespace WixToolset.Data.Tuples
{
    public enum ListViewTupleFields
    {
        Property,
        Order,
        Value,
        Text,
        Binary_,
    }

    public class ListViewTuple : IntermediateTuple
    {
        public ListViewTuple() : base(TupleDefinitions.ListView, null, null)
        {
        }

        public ListViewTuple(SourceLineNumber sourceLineNumber, Identifier id = null) : base(TupleDefinitions.ListView, sourceLineNumber, id)
        {
        }

        public IntermediateField this[ListViewTupleFields index] => this.Fields[(int)index];

        public string Property
        {
            get => (string)this.Fields[(int)ListViewTupleFields.Property]?.Value;
            set => this.Set((int)ListViewTupleFields.Property, value);
        }

        public int Order
        {
            get => (int)this.Fields[(int)ListViewTupleFields.Order]?.Value;
            set => this.Set((int)ListViewTupleFields.Order, value);
        }

        public string Value
        {
            get => (string)this.Fields[(int)ListViewTupleFields.Value]?.Value;
            set => this.Set((int)ListViewTupleFields.Value, value);
        }

        public string Text
        {
            get => (string)this.Fields[(int)ListViewTupleFields.Text]?.Value;
            set => this.Set((int)ListViewTupleFields.Text, value);
        }

        public string Binary_
        {
            get => (string)this.Fields[(int)ListViewTupleFields.Binary_]?.Value;
            set => this.Set((int)ListViewTupleFields.Binary_, value);
        }
    }
}