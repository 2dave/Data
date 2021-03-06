// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Data.WindowsInstaller
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml;

    /// <summary>
    /// Output is generated by the linker.
    /// </summary>
    public sealed class Output
    {
        public const string XmlNamespaceUri = "http://wixtoolset.org/schemas/v4/wixout";
        private static readonly Version CurrentVersion = new Version("4.0.0.0");

        /// <summary>
        /// Creates a new empty output object.
        /// </summary>
        /// <param name="sourceLineNumbers">The source line information for the output.</param>
        public Output(SourceLineNumber sourceLineNumbers)
        {
            this.SourceLineNumbers = sourceLineNumbers;
            this.SubStorages = new List<SubStorage>();
            this.Tables = new TableIndexedCollection();
        }

        /// <summary>
        /// Gets the type of the output.
        /// </summary>
        /// <value>Type of the output.</value>
        public OutputType Type { get; set; }

        /// <summary>
        /// Gets or sets the codepage for this output.
        /// </summary>
        /// <value>Codepage of the output.</value>
        public int Codepage { get; set; }

        /// <summary>
        /// Gets the source line information for this output.
        /// </summary>
        /// <value>The source line information for this output.</value>
        public SourceLineNumber SourceLineNumbers { get; private set; }

        /// <summary>
        /// Gets the substorages in this output.
        /// </summary>
        /// <value>The substorages in this output.</value>
        public ICollection<SubStorage> SubStorages { get; private set; }

        /// <summary>
        /// Gets the tables contained in this output.
        /// </summary>
        /// <value>Collection of tables.</value>
        public TableIndexedCollection Tables { get; private set; }

        /// <summary>
        /// Loads an output from a path on disk.
        /// </summary>
        /// <param name="path">Path to output file saved on disk.</param>
        /// <param name="suppressVersionCheck">Suppresses wix.dll version mismatch check.</param>
        /// <returns>Output object.</returns>
        public static Output Load(string path, bool suppressVersionCheck)
        {
            using (FileStream stream = File.OpenRead(path))
            using (FileStructure fs = FileStructure.Read(stream))
            {
                if (FileFormat.Wixout != fs.FileFormat)
                {
                    throw new WixUnexpectedFileFormatException(path, FileFormat.Wixout, fs.FileFormat);
                }

                Uri uri = new Uri(Path.GetFullPath(path));
                using (XmlReader reader = XmlReader.Create(fs.GetDataStream(), null, uri.AbsoluteUri))
                {
                    try
                    {
                        reader.MoveToContent();
                        return Output.Read(reader, suppressVersionCheck);
                    }
                    catch (XmlException xe)
                    {
                        throw new WixCorruptFileException(path, fs.FileFormat, xe);
                    }
                }
            }
        }

        /// <summary>
        /// Saves an output to a path on disk.
        /// </summary>
        /// <param name="path">Path to save output file to on disk.</param>
        public void Save(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path)));

            using (FileStream stream = File.Create(path))
            using (FileStructure fs = FileStructure.Create(stream, FileFormat.Wixout, null))
            using (XmlWriter writer = XmlWriter.Create(fs.GetDataStream()))
            {
                writer.WriteStartDocument();
                this.Write(writer);
                writer.WriteEndDocument();
            }
        }

        /// <summary>
        /// Processes an XmlReader and builds up the output object.
        /// </summary>
        /// <param name="reader">Reader to get data from.</param>
        /// <param name="suppressVersionCheck">Suppresses wix.dll version mismatch check.</param>
        /// <returns>The Output represented by the Xml.</returns>
        internal static Output Read(XmlReader reader, bool suppressVersionCheck)
        {
            if (!reader.LocalName.Equals("wixOutput"))
            {
                throw new XmlException();
            }

            bool empty = reader.IsEmptyElement;
            Output output = new Output(SourceLineNumber.CreateFromUri(reader.BaseURI));
            Version version = null;

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "codepage":
                        output.Codepage = Convert.ToInt32(reader.Value, CultureInfo.InvariantCulture.NumberFormat);
                        break;
                    case "type":
                        switch (reader.Value)
                        {
                            case "Bundle":
                                output.Type = OutputType.Bundle;
                                break;
                            case "Module":
                                output.Type = OutputType.Module;
                                break;
                            case "Patch":
                                output.Type = OutputType.Patch;
                                break;
                            case "PatchCreation":
                                output.Type = OutputType.PatchCreation;
                                break;
                            case "Product":
                                output.Type = OutputType.Product;
                                break;
                            case "Transform":
                                output.Type = OutputType.Transform;
                                break;
                            default:
                                throw new XmlException();
                        }
                        break;
                    case "version":
                        version = new Version(reader.Value);
                        break;
                }
            }

            if (!suppressVersionCheck && null != version && !Output.CurrentVersion.Equals(version))
            {
                throw new WixException(ErrorMessages.VersionMismatch(SourceLineNumber.CreateFromUri(reader.BaseURI), "wixOutput", version.ToString(), Output.CurrentVersion.ToString()));
            }

            // loop through the rest of the xml building up the Output object
            TableDefinitionCollection tableDefinitions = null;
            List<Table> tables = new List<Table>();
            if (!empty)
            {
                bool done = false;

                // loop through all the fields in a row
                while (!done && reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            switch (reader.LocalName)
                            {
                                case "subStorage":
                                    output.SubStorages.Add(SubStorage.Read(reader));
                                    break;
                                case "table":
                                    if (null == tableDefinitions)
                                    {
                                        throw new XmlException();
                                    }
                                    tables.Add(Table.Read(reader, tableDefinitions));
                                    break;
                                case "tableDefinitions":
                                    tableDefinitions = TableDefinitionCollection.Read(reader);
                                    break;
                                default:
                                    throw new XmlException();
                            }
                            break;
                        case XmlNodeType.EndElement:
                            done = true;
                            break;
                    }
                }

                if (!done)
                {
                    throw new XmlException();
                }
            }

            output.Tables = new TableIndexedCollection(tables);
            return output;
        }

        /// <summary>
        /// Ensure this output contains a particular table.
        /// </summary>
        /// <param name="tableDefinition">Definition of the table that should exist.</param>
        /// <param name="section">Optional section to use for the table. If one is not provided, the entry section will be used.</param>
        /// <returns>The table in this output.</returns>
        public Table EnsureTable(TableDefinition tableDefinition)
        {
            if (!this.Tables.TryGetTable(tableDefinition.Name, out Table table))
            {
                table = new Table(tableDefinition);
                this.Tables.Add(table);
            }

            return table;
        }

        /// <summary>
        /// Persists an output in an XML format.
        /// </summary>
        /// <param name="writer">XmlWriter where the Output should persist itself as XML.</param>
        internal void Write(XmlWriter writer)
        {
            writer.WriteStartElement("wixOutput", XmlNamespaceUri);

            writer.WriteAttributeString("type", this.Type.ToString());

            if (0 != this.Codepage)
            {
                writer.WriteAttributeString("codepage", this.Codepage.ToString(CultureInfo.InvariantCulture));
            }

            writer.WriteAttributeString("version", Output.CurrentVersion.ToString());

            // Collect all the table definitions and write them.
            TableDefinitionCollection tableDefinitions = new TableDefinitionCollection();
            foreach (Table table in this.Tables)
            {
                tableDefinitions.Add(table.Definition);
            }
            tableDefinitions.Write(writer);

            foreach (Table table in this.Tables.OrderBy(t => t.Name))
            {
                table.Write(writer);
            }

            foreach (SubStorage subStorage in this.SubStorages)
            {
                subStorage.Write(writer);
            }

            writer.WriteEndElement();
        }
    }
}
