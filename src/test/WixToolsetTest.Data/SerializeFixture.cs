// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolsetTest.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using WixToolset.Data;
    using WixToolset.Data.Bind;
    using WixToolset.Data.Tuples;
    using Xunit;

    public class SerializeFixture
    {
        [Fact]
        public void CanSaveAndLoadIntermediate()
        {
            var sln = new SourceLineNumber("test.wxs", 1);

            var section = new IntermediateSection("test", SectionType.Product, 65001);

            section.Tuples.Add(new ComponentTuple(sln, new Identifier("TestComponent", AccessModifier.Public))
            {
                ComponentId = new Guid(1, 0, 0, new byte[8]).ToString("B"),
                Directory_ = "TestFolder",
                Attributes = 2,
            });

            var intermediate = new Intermediate("TestIntermediate", new[] { section }, null, null);

            var path = Path.GetTempFileName();
            intermediate.Save(path);

            var loaded = Intermediate.Load(path);

            var tuple = (ComponentTuple)loaded.Sections.Single().Tuples.Single();

            Assert.Equal("TestComponent", tuple.Id.Id);
            Assert.Equal(AccessModifier.Public, tuple.Id.Access);
            Assert.Equal("TestFolder", tuple.Directory_);
            Assert.Equal(2, tuple.Attributes);
        }

        [Fact]
        public void CanSaveAndLoadIntermediateWithCustomDefinitions()
        {
            var sln = new SourceLineNumber("test.wxs", 1);

            var section = new IntermediateSection("test", SectionType.Product, 65001);

            var fieldDefs = new[]
            {
                new IntermediateFieldDefinition("A", IntermediateFieldType.String),
                new IntermediateFieldDefinition("B", IntermediateFieldType.Number),
                new IntermediateFieldDefinition("C", IntermediateFieldType.Bool),
            };

            var tupleDef = new IntermediateTupleDefinition("CustomDef", fieldDefs, null);

            var tuple = tupleDef.CreateTuple(sln, new Identifier(AccessModifier.Public, "customT"));
            tuple.Set(0, "foo");
            tuple.Set(1, 2);
            tuple.Set(2, true);

            section.Tuples.Add(tuple);

            var intermediate = new Intermediate("TestIntermediate", new[] { section }, null, null);

            var path = Path.GetTempFileName();
            try
            {
                intermediate.Save(path);

                var loaded = Intermediate.Load(path);
                var loadedSection = loaded.Sections.Single();
                var loadedTuple = loadedSection.Tuples.Single();

                Assert.Equal("foo", loadedTuple.AsString(0));
                Assert.Equal(2, loadedTuple[1].AsNumber());
                Assert.True(loadedTuple[2].AsBool());
            }
            finally
            {
                File.Delete(path);
            }
        }

        [Fact]
        public void CanSaveAndLoadMultipleIntermediateWithCustomDefinitions()
        {
            var sln = new SourceLineNumber("test.wxs", 1);

            // Intermediate #1
            var fieldDefs = new[]
            {
                new IntermediateFieldDefinition("A", IntermediateFieldType.String),
                new IntermediateFieldDefinition("B", IntermediateFieldType.Number),
                new IntermediateFieldDefinition("C", IntermediateFieldType.Bool),
            };

            var tupleDef = new IntermediateTupleDefinition("CustomDef", fieldDefs, null);

            var tuple = tupleDef.CreateTuple(sln, new Identifier(AccessModifier.Public, "customT"));
            tuple.Set(0, "foo");
            tuple.Set(1, 2);
            tuple.Set(2, true);

            var section = new IntermediateSection("test", SectionType.Product, 65001);
            section.Tuples.Add(tuple);

            var intermediate1 = new Intermediate("TestIntermediate", new[] { section }, null, null);

            // Intermediate #2
            var fieldDefs2 = new[]
            {
                new IntermediateFieldDefinition("A", IntermediateFieldType.String),
                new IntermediateFieldDefinition("B", IntermediateFieldType.Number),
                new IntermediateFieldDefinition("C", IntermediateFieldType.Bool),
                new IntermediateFieldDefinition("D", IntermediateFieldType.String),
            };

            var tupleDef2 = new IntermediateTupleDefinition("CustomDef", 1, fieldDefs2, null);

            var tuple2 = tupleDef2.CreateTuple(sln, new Identifier(AccessModifier.Public, "customT2"));
            tuple2.Set(0, "bar");
            tuple2.Set(1, 3);
            tuple2.Set(2, false);
            tuple2.Set(3, "baz");

            var section2 = new IntermediateSection("test2", SectionType.Fragment, 65001);
            section2.Tuples.Add(tuple2);

            var intermediate2 = new Intermediate("TestIntermediate2", new[] { section2 }, null, null);

            // Save
            var path1 = Path.GetTempFileName();
            var path2 = Path.GetTempFileName();
            try
            {
                intermediate1.Save(path1);
                intermediate2.Save(path2);

                var loaded = Intermediate.Load(new[] { path1, path2 });

                var loaded1 = loaded.First();
                var loaded2 = loaded.Skip(1).Single();

                var loadedTuple1 = loaded1.Sections.Single().Tuples.Single();
                var loadedTuple2 = loaded2.Sections.Single().Tuples.Single();

                Assert.Equal("foo", loadedTuple1.AsString(0));
                Assert.Equal(2, loadedTuple1[1].AsNumber());
                Assert.True(loadedTuple1[2].AsBool());
                Assert.Null(loadedTuple1.AsString(3));

                Assert.Equal("bar", loadedTuple2.AsString(0));
                Assert.Equal(3, loadedTuple2[1].AsNumber());
                Assert.False(loadedTuple2[2].AsBool());
                Assert.Equal("baz", loadedTuple2.AsString(3));
            }
            finally
            {
                File.Delete(path2);
                File.Delete(path1);
            }
        }

        [Fact]
        public void CanSaveAndLoadIntermediateWithLocalization()
        {
            var sln = new SourceLineNumber("test.wxs", 10);

            var bindVariables = new[]
            {
                new BindVariable { Id = "TestVar1", Value = "TestValue1", SourceLineNumbers = sln },
                new BindVariable { Id = "TestVar2", Value = "TestValue2", Overridable = true, SourceLineNumbers = sln },
            };

            var controls = new[]
            {
                new LocalizedControl("TestDlg1", "TestControl1", 10, 10, 100, 100, 0, null),
                new LocalizedControl("TestDlg1", "TestControl2", 100, 90, 50, 70, 0, "localized"),
            };

            var localizations = new[]
            {
                new Localization(65001, null, bindVariables.ToDictionary(b => b.Id), controls.ToDictionary(c => c.GetKey()))
            };

            var section = new IntermediateSection("test", SectionType.Product, 65001);

            section.Tuples.Add(new ComponentTuple(sln, new Identifier("TestComponent", AccessModifier.Public))
            {
                ComponentId = new Guid(1, 0, 0, new byte[8]).ToString("B"),
                Directory_ = "TestFolder",
                Attributes = 2,
            });

            var intermediate = new Intermediate("TestIntermediate", new[] { section }, localizations.ToDictionary(l => l.Culture), null);

            var path = Path.GetTempFileName();
            try
            {
                intermediate.Save(path);

                var loaded = Intermediate.Load(path);

                var loc = loaded.Localizations.Single();
                Assert.Equal(65001, loc.Codepage);
                Assert.Empty(loc.Culture);
                Assert.Equal(new[]
                {
                    "TestVar1/TestValue1/False",
                    "TestVar2/TestValue2/True",
                }, loc.Variables.Select(v => String.Join("/", v.Id, v.Value, v.Overridable)).ToArray());
            }
            finally
            {
                File.Delete(path);
            }
        }
    }
}
