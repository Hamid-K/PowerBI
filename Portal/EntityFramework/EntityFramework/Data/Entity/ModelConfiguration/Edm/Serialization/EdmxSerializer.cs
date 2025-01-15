using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Xml;

namespace System.Data.Entity.ModelConfiguration.Edm.Serialization
{
	// Token: 0x0200017B RID: 379
	internal sealed class EdmxSerializer
	{
		// Token: 0x060016E2 RID: 5858 RVA: 0x0003CF7C File Offset: 0x0003B17C
		public void Serialize(DbDatabaseMapping databaseMapping, XmlWriter xmlWriter)
		{
			this._xmlWriter = xmlWriter;
			this._databaseMapping = databaseMapping;
			this._version = databaseMapping.Model.SchemaVersion;
			this._namespace = (object.Equals(this._version, 3.0) ? "http://schemas.microsoft.com/ado/2009/11/edmx" : (object.Equals(this._version, 2.0) ? "http://schemas.microsoft.com/ado/2008/10/edmx" : "http://schemas.microsoft.com/ado/2007/06/edmx"));
			this._xmlWriter.WriteStartDocument();
			using (this.Element("Edmx", new string[]
			{
				"Version",
				string.Format(CultureInfo.InvariantCulture, "{0:F1}", new object[] { this._version })
			}))
			{
				this.WriteEdmxRuntime();
				this.WriteEdmxDesigner();
			}
			this._xmlWriter.WriteEndDocument();
			this._xmlWriter.Flush();
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0003D08C File Offset: 0x0003B28C
		private void WriteEdmxRuntime()
		{
			using (this.Element("Runtime", new string[0]))
			{
				using (this.Element("ConceptualModels", new string[0]))
				{
					this._databaseMapping.Model.ValidateAndSerializeCsdl(this._xmlWriter);
				}
				using (this.Element("Mappings", new string[0]))
				{
					new MslSerializer().Serialize(this._databaseMapping, this._xmlWriter);
				}
				using (this.Element("StorageModels", new string[0]))
				{
					new SsdlSerializer().Serialize(this._databaseMapping.Database, this._databaseMapping.ProviderInfo.ProviderInvariantName, this._databaseMapping.ProviderInfo.ProviderManifestToken, this._xmlWriter, true);
				}
			}
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0003D1B0 File Offset: 0x0003B3B0
		private void WriteEdmxDesigner()
		{
			using (this.Element("Designer", new string[0]))
			{
				this.WriteEdmxConnection();
				this.WriteEdmxOptions();
				this.WriteEdmxDiagrams();
			}
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0003D200 File Offset: 0x0003B400
		private void WriteEdmxConnection()
		{
			using (this.Element("Connection", new string[0]))
			{
				using (this.Element("DesignerInfoPropertySet", new string[0]))
				{
					this.WriteDesignerPropertyElement("MetadataArtifactProcessing", "EmbedInOutputAssembly");
				}
			}
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x0003D274 File Offset: 0x0003B474
		private void WriteEdmxOptions()
		{
			using (this.Element("Options", new string[0]))
			{
				using (this.Element("DesignerInfoPropertySet", new string[0]))
				{
					this.WriteDesignerPropertyElement("ValidateOnBuild", "False");
					this.WriteDesignerPropertyElement("CodeGenerationStrategy", "None");
					this.WriteDesignerPropertyElement("ProcessDependentTemplatesOnSave", "False");
					this.WriteDesignerPropertyElement("UseLegacyProvider", "False");
				}
			}
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0003D318 File Offset: 0x0003B518
		private void WriteDesignerPropertyElement(string name, string value)
		{
			using (this.Element("DesignerProperty", new string[] { "Name", name, "Value", value }))
			{
			}
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0003D36C File Offset: 0x0003B56C
		private void WriteEdmxDiagrams()
		{
			using (this.Element("Diagrams", new string[0]))
			{
			}
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0003D3A8 File Offset: 0x0003B5A8
		private IDisposable Element(string elementName, params string[] attributes)
		{
			this._xmlWriter.WriteStartElement(elementName, this._namespace);
			for (int i = 0; i < attributes.Length - 1; i += 2)
			{
				this._xmlWriter.WriteAttributeString(attributes[i], attributes[i + 1]);
			}
			return new EdmxSerializer.EndElement(this._xmlWriter);
		}

		// Token: 0x04000A19 RID: 2585
		private const string EdmXmlNamespaceV1 = "http://schemas.microsoft.com/ado/2007/06/edmx";

		// Token: 0x04000A1A RID: 2586
		private const string EdmXmlNamespaceV2 = "http://schemas.microsoft.com/ado/2008/10/edmx";

		// Token: 0x04000A1B RID: 2587
		private const string EdmXmlNamespaceV3 = "http://schemas.microsoft.com/ado/2009/11/edmx";

		// Token: 0x04000A1C RID: 2588
		private DbDatabaseMapping _databaseMapping;

		// Token: 0x04000A1D RID: 2589
		private double _version;

		// Token: 0x04000A1E RID: 2590
		private XmlWriter _xmlWriter;

		// Token: 0x04000A1F RID: 2591
		private string _namespace;

		// Token: 0x0200086E RID: 2158
		private class EndElement : IDisposable
		{
			// Token: 0x06005AAF RID: 23215 RVA: 0x0013CEC5 File Offset: 0x0013B0C5
			public EndElement(XmlWriter xmlWriter)
			{
				this._xmlWriter = xmlWriter;
			}

			// Token: 0x06005AB0 RID: 23216 RVA: 0x0013CED4 File Offset: 0x0013B0D4
			public void Dispose()
			{
				this._xmlWriter.WriteEndElement();
			}

			// Token: 0x04002336 RID: 9014
			private readonly XmlWriter _xmlWriter;
		}
	}
}
