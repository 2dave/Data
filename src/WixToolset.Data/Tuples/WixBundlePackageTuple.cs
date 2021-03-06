// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Data
{
    using WixToolset.Data.Tuples;

    public static partial class TupleDefinitions
    {
        public static readonly IntermediateTupleDefinition WixBundlePackage = new IntermediateTupleDefinition(
            TupleDefinitionType.WixBundlePackage,
            new[]
            {
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.WixChainItem_), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.Type), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.Payload_), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.Attributes), IntermediateFieldType.Number),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.InstallCondition), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.Cache), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.CacheId), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.Vital), IntermediateFieldType.Bool),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.PerMachine), IntermediateFieldType.Bool),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.LogPathVariable), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.RollbackLogPathVariable), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.Size), IntermediateFieldType.Number),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.InstallSize), IntermediateFieldType.Number),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.Version), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.Language), IntermediateFieldType.Number),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.DisplayName), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.Description), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.RollbackBoundary_), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.RollbackBoundaryBackward_), IntermediateFieldType.String),
                new IntermediateFieldDefinition(nameof(WixBundlePackageTupleFields.x64), IntermediateFieldType.Number),
            },
            typeof(WixBundlePackageTuple));
    }
}

namespace WixToolset.Data.Tuples
{
    using System;

    public enum WixBundlePackageTupleFields
    {
        WixChainItem_,
        Type,
        Payload_,
        Attributes,
        InstallCondition,
        Cache,
        CacheId,
        Vital,
        PerMachine,
        LogPathVariable,
        RollbackLogPathVariable,
        Size,
        InstallSize,
        Version,
        Language,
        DisplayName,
        Description,
        RollbackBoundary_,
        RollbackBoundaryBackward_,
        x64,
    }

    /// <summary>
    /// Types of bundle packages.
    /// </summary>
    public enum WixBundlePackageType
    {
        Exe,
        Msi,
        Msp,
        Msu,
    }

    [Flags]
    public enum WixBundlePackageAttributes
    {
        Permanent = 0x1,
        Visible = 0x2,
    }

    public class WixBundlePackageTuple : IntermediateTuple
    {
        public WixBundlePackageTuple() : base(TupleDefinitions.WixBundlePackage, null, null)
        {
        }

        public WixBundlePackageTuple(SourceLineNumber sourceLineNumber, Identifier id = null) : base(TupleDefinitions.WixBundlePackage, sourceLineNumber, id)
        {
        }

        public IntermediateField this[WixBundlePackageTupleFields index] => this.Fields[(int)index];

        public string WixChainItem_
        {
            get => (string)this.Fields[(int)WixBundlePackageTupleFields.WixChainItem_]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.WixChainItem_, value);
        }

        public WixBundlePackageType Type
        {
            get => (WixBundlePackageType)Enum.Parse(typeof(WixBundlePackageType), (string)this.Fields[(int)WixBundlePackageTupleFields.Type]?.Value, true);
            set => this.Set((int)WixBundlePackageTupleFields.Type, value.ToString());
        }

        public string Payload_
        {
            get => (string)this.Fields[(int)WixBundlePackageTupleFields.Payload_]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.Payload_, value);
        }

        public WixBundlePackageAttributes Attributes
        {
            get => (WixBundlePackageAttributes)(int)this.Fields[(int)WixBundlePackageTupleFields.Attributes]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.Attributes, (int)value);
        }

        public string InstallCondition
        {
            get => (string)this.Fields[(int)WixBundlePackageTupleFields.InstallCondition]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.InstallCondition, value);
        }

        public YesNoAlwaysType Cache
        {
            get => Enum.TryParse((string)this.Fields[(int)WixBundlePackageTupleFields.Cache]?.Value, true, out YesNoAlwaysType value) ? value : YesNoAlwaysType.NotSet;
            set => this.Set((int)WixBundlePackageTupleFields.Cache, value.ToString().ToLowerInvariant());
        }

        public string CacheId
        {
            get => (string)this.Fields[(int)WixBundlePackageTupleFields.CacheId]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.CacheId, value);
        }

        public bool? Vital
        {
            get => (bool?)this.Fields[(int)WixBundlePackageTupleFields.Vital]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.Vital, value);
        }

        public YesNoDefaultType PerMachine
        {
            get => Enum.TryParse((string)this.Fields[(int)WixBundlePackageTupleFields.PerMachine]?.Value, true, out YesNoDefaultType value) ? value : YesNoDefaultType.NotSet;
            set => this.Set((int)WixBundlePackageTupleFields.PerMachine, value.ToString().ToLowerInvariant());
        }

        public string LogPathVariable
        {
            get => (string)this.Fields[(int)WixBundlePackageTupleFields.LogPathVariable]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.LogPathVariable, value);
        }

        public string RollbackLogPathVariable
        {
            get => (string)this.Fields[(int)WixBundlePackageTupleFields.RollbackLogPathVariable]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.RollbackLogPathVariable, value);
        }

        public int Size
        {
            get => (int)this.Fields[(int)WixBundlePackageTupleFields.Size]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.Size, value);
        }

        public int InstallSize
        {
            get => (int)this.Fields[(int)WixBundlePackageTupleFields.InstallSize]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.InstallSize, value);
        }

        public string Version
        {
            get => (string)this.Fields[(int)WixBundlePackageTupleFields.Version]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.Version, value);
        }

        public int Language
        {
            get => (int)this.Fields[(int)WixBundlePackageTupleFields.Language]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.Language, value);
        }

        public string DisplayName
        {
            get => (string)this.Fields[(int)WixBundlePackageTupleFields.DisplayName]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.DisplayName, value);
        }

        public string Description
        {
            get => (string)this.Fields[(int)WixBundlePackageTupleFields.Description]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.Description, value);
        }

        public string RollbackBoundary_
        {
            get => (string)this.Fields[(int)WixBundlePackageTupleFields.RollbackBoundary_]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.RollbackBoundary_, value);
        }

        public string RollbackBoundaryBackward_
        {
            get => (string)this.Fields[(int)WixBundlePackageTupleFields.RollbackBoundaryBackward_]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.RollbackBoundaryBackward_, value);
        }

        public int x64
        {
            get => (int)this.Fields[(int)WixBundlePackageTupleFields.x64]?.Value;
            set => this.Set((int)WixBundlePackageTupleFields.x64, value);
        }
    }
}