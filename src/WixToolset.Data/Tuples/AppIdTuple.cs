// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Data
{
    using WixToolset.Data.Tuples;

    public static partial class TupleDefinitions
    {
        public static readonly IntermediateTupleDefinition AppId = new IntermediateTupleDefinition(
            TupleDefinitionType.AppId,
            new[]
            {
                new IntermediateFieldDefinition(nameof(AppIdTupleFields.AppId), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(AppIdTupleFields.RemoteServerName), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(AppIdTupleFields.LocalService), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(AppIdTupleFields.ServiceParameters), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(AppIdTupleFields.DllSurrogate), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(AppIdTupleFields.ActivateAtStorage), IntermediateFieldType.Number),
                new IntermediateFieldDefinition(nameof(AppIdTupleFields.RunAsInteractiveUser), IntermediateFieldType.Number),
            },
            typeof(AppIdTuple));
    }
}

namespace WixToolset.Data.Tuples
{
    public enum AppIdTupleFields
    {
        AppId,
        RemoteServerName,
        LocalService,
        ServiceParameters,
        DllSurrogate,
        ActivateAtStorage,
        RunAsInteractiveUser,
    }

    public class AppIdTuple : IntermediateTuple
    {
        public AppIdTuple() : base(TupleDefinitions.AppId, null, null)
        {
        }

        public AppIdTuple(SourceLineNumber sourceLineNumber, Identifier id = null) : base(TupleDefinitions.AppId, sourceLineNumber, id)
        {
        }

        public IntermediateField this[AppIdTupleFields index] => this.Fields[(int)index];

        public string AppId
        {
            get => (string)this.Fields[(int)AppIdTupleFields.AppId]?.Value;
            set => this.Set((int)AppIdTupleFields.AppId, value);
        }

        public string RemoteServerName
        {
            get => (string)this.Fields[(int)AppIdTupleFields.RemoteServerName]?.Value;
            set => this.Set((int)AppIdTupleFields.RemoteServerName, value);
        }

        public string LocalService
        {
            get => (string)this.Fields[(int)AppIdTupleFields.LocalService]?.Value;
            set => this.Set((int)AppIdTupleFields.LocalService, value);
        }

        public string ServiceParameters
        {
            get => (string)this.Fields[(int)AppIdTupleFields.ServiceParameters]?.Value;
            set => this.Set((int)AppIdTupleFields.ServiceParameters, value);
        }

        public string DllSurrogate
        {
            get => (string)this.Fields[(int)AppIdTupleFields.DllSurrogate]?.Value;
            set => this.Set((int)AppIdTupleFields.DllSurrogate, value);
        }

        public int ActivateAtStorage
        {
            get => (int)this.Fields[(int)AppIdTupleFields.ActivateAtStorage]?.Value;
            set => this.Set((int)AppIdTupleFields.ActivateAtStorage, value);
        }

        public int RunAsInteractiveUser
        {
            get => (int)this.Fields[(int)AppIdTupleFields.RunAsInteractiveUser]?.Value;
            set => this.Set((int)AppIdTupleFields.RunAsInteractiveUser, value);
        }
    }
}