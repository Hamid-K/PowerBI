using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public class Connection : IXmlSerializable, IName
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003DF1 File Offset: 0x00001FF1
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003DF9 File Offset: 0x00001FF9
		public string Name { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003E02 File Offset: 0x00002002
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003E0A File Offset: 0x0000200A
		public string Description { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003E13 File Offset: 0x00002013
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00003E1B File Offset: 0x0000201B
		public string ConnectionString { get; set; }

		// Token: 0x0600009A RID: 154 RVA: 0x00003E2C File Offset: 0x0000202C
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003E30 File Offset: 0x00002030
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode;
			if ((xmlNode = xmlDocument.SelectSingleNode("/ns:Connection", xmlNamespaceManager)) != null)
			{
				this.Name = xmlNode.Attributes.GetNamedItemOrDefault("name", this.Name);
				this.Description = xmlNode.Attributes.GetNamedItemOrDefault("description", this.Description);
				this.ConnectionString = xmlNode.Attributes.GetNamedItemOrDefault("connectionString", this.ConnectionString);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003EB4 File Offset: 0x000020B4
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Connection");
			if (!string.IsNullOrEmpty(this.Name))
			{
				writer.WriteAttributeString("name", this.Name);
			}
			if (!string.IsNullOrEmpty(this.Description))
			{
				writer.WriteAttributeString("description", this.Description);
			}
			if (!string.IsNullOrEmpty(this.ConnectionString))
			{
				writer.WriteAttributeString("connectionString", this.ConnectionString);
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000032 RID: 50
		public static readonly Connection ContextConnection = new Connection
		{
			Name = ConnectionManager.ContextConnectionName,
			ConnectionString = ConnectionManager.ContextConnectionString,
			Description = "SQL CLR Context Connection"
		};
	}
}
