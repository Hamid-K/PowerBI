using System;
using System.Data;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	public abstract class RowsetDefinition : IRowsetDefinition, IXmlSerializable, IName
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002D70 File Offset: 0x00000F70
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002D78 File Offset: 0x00000F78
		public string Name { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002D81 File Offset: 0x00000F81
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002D89 File Offset: 0x00000F89
		public string Description { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002D92 File Offset: 0x00000F92
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002D9A File Offset: 0x00000F9A
		public virtual string RidColumnName
		{
			get
			{
				return this.m_ridColumnName;
			}
			set
			{
				this.m_ridColumnName = value;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002DA3 File Offset: 0x00000FA3
		public RowsetDefinition()
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002DAC File Offset: 0x00000FAC
		public static RowsetDefinition Create(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode;
			if ((xmlNode = xmlDocument.SelectSingleNode("//ns:SqlRowset", xmlNamespaceManager)) != null)
			{
				return new SqlRowsetDefinition(new XmlNodeReader(xmlNode));
			}
			if ((xmlNode = xmlDocument.SelectSingleNode("//ns:InlineRowset", xmlNamespaceManager)) != null)
			{
				return new InlineRowset(new XmlNodeReader(xmlNode));
			}
			if ((xmlNode = xmlDocument.SelectSingleNode("//ns:CsvFileRowset", xmlNamespaceManager)) != null)
			{
				return new CsvFileRowset(new XmlNodeReader(xmlNode));
			}
			throw new ArgumentException("XML does not define a valid Rowset.  Supported tags are SqlRowset and InlineRowset.");
		}

		// Token: 0x0600004A RID: 74
		public abstract IDataReader CreateDataReader(ConnectionManager connectionManager);

		// Token: 0x0600004B RID: 75
		public abstract DataTable GetSchemaTable(ConnectionManager connectionManager);

		// Token: 0x0600004C RID: 76 RVA: 0x00002E2B File Offset: 0x0000102B
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E2E File Offset: 0x0000102E
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E30 File Offset: 0x00001030
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
		}

		// Token: 0x0400001F RID: 31
		protected string m_ridColumnName;
	}
}
