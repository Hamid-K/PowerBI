using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200007A RID: 122
	[Serializable]
	public class RowsetManager : IRowsetManager, IXmlSerializable
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00016A07 File Offset: 0x00014C07
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x00016A0F File Offset: 0x00014C0F
		public ConnectionCollection Connections { get; protected set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00016A18 File Offset: 0x00014C18
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x00016A20 File Offset: 0x00014C20
		public RowsetCollection Rowsets { get; protected set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00016A29 File Offset: 0x00014C29
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x00016A31 File Offset: 0x00014C31
		public RecordBindingCollection RecordBindings { get; protected set; }

		// Token: 0x060004E3 RID: 1251 RVA: 0x00016A3A File Offset: 0x00014C3A
		public RowsetManager()
		{
			this.Connections = new ConnectionCollection();
			this.Rowsets = new RowsetCollection();
			this.RecordBindings = new RecordBindingCollection();
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00016A63 File Offset: 0x00014C63
		public RowsetManager(XmlReader reader)
			: this()
		{
			this.ReadXml(reader);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00016A74 File Offset: 0x00014C74
		public RecordBinding GetRecordBinding(string rowsetName, ConnectionManager connectionManager)
		{
			RecordBinding recordBinding;
			if (this.TryGetRecordBinding(rowsetName, connectionManager, out recordBinding))
			{
				return recordBinding;
			}
			throw new Exception(string.Format("No rowset binding was found for rowset named '{0}'.", rowsetName));
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00016AA0 File Offset: 0x00014CA0
		public bool TryGetRecordBinding(string rowsetName, ConnectionManager connectionManager, out RecordBinding recordBinding)
		{
			IRowsetDefinition rowsetDefinition = null;
			bool flag = false;
			if (this.RecordBindings.TryGetRecordBindingForRowset(rowsetName, out recordBinding))
			{
				flag = true;
				if (!string.IsNullOrEmpty(recordBinding.Name))
				{
					this.Rowsets.TryGetItem(recordBinding.Name, out rowsetDefinition);
				}
				else
				{
					this.Rowsets.TryGetItem(rowsetName, out rowsetDefinition);
				}
			}
			if (recordBinding != null && recordBinding.Schema == null && rowsetDefinition != null)
			{
				recordBinding.Schema = rowsetDefinition.GetSchemaTable(connectionManager);
			}
			return flag;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00016B15 File Offset: 0x00014D15
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00016B18 File Offset: 0x00014D18
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader));
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode;
			if ((xmlNode = xmlDocument.SelectSingleNode("/*/ns:Connections", xmlNamespaceManager)) != null)
			{
				this.Connections.ReadXml(new XmlNodeReader(xmlNode));
			}
			if ((xmlNode = xmlDocument.SelectSingleNode("/*/ns:Rowsets", xmlNamespaceManager)) != null)
			{
				this.Rowsets.ReadXml(new XmlNodeReader(xmlNode));
			}
			else if ((xmlNode = xmlDocument.SelectSingleNode("//ns:SqlRowset", xmlNamespaceManager)) != null)
			{
				this.Rowsets.Add(new SqlRowsetDefinition(new XmlNodeReader(xmlNode)));
				this.Rowsets.Default = "0";
			}
			else if ((xmlNode = xmlDocument.SelectSingleNode("//ns:InlineRowset", xmlNamespaceManager)) != null)
			{
				this.Rowsets.Add(new InlineRowset(new XmlNodeReader(xmlNode)));
				this.Rowsets.Default = "0";
			}
			else if ((xmlNode = xmlDocument.SelectSingleNode("//ns:CsvFileRowset", xmlNamespaceManager)) != null)
			{
				this.Rowsets.Add(new CsvFileRowset(new XmlNodeReader(xmlNode)));
				this.Rowsets.Default = "0";
			}
			if ((xmlNode = xmlDocument.SelectSingleNode("/*/ns:RecordBindings", xmlNamespaceManager)) != null)
			{
				this.RecordBindings.ReadXml(new XmlNodeReader(xmlNode));
				return;
			}
			if ((xmlNode = xmlDocument.SelectSingleNode("/*/ns:RecordBinding", xmlNamespaceManager)) != null)
			{
				this.RecordBindings.Add(new RecordBinding(new XmlNodeReader(xmlNode)));
				this.RecordBindings.Default = "0";
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00016C83 File Offset: 0x00014E83
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("RowsetManager");
			this.Connections.WriteXml(writer);
			this.Rowsets.WriteXml(writer);
			this.RecordBindings.WriteXml(writer);
			writer.WriteEndElement();
		}
	}
}
