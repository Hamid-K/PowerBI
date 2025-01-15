using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	public class DomainName : IXmlSerializable, IName
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000572F File Offset: 0x0000392F
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00005737 File Offset: 0x00003937
		public string Name { get; set; }

		// Token: 0x06000126 RID: 294 RVA: 0x00005740 File Offset: 0x00003940
		public DomainName()
		{
			this.Name = string.Empty;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005753 File Offset: 0x00003953
		public override bool Equals(object obj)
		{
			return obj is DomainName && (obj as DomainName).Name.Equals(this.Name);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005775 File Offset: 0x00003975
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005778 File Offset: 0x00003978
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/ns:Domain", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.Name = xmlNode.InnerText;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000057B3 File Offset: 0x000039B3
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Domain");
			writer.WriteString(this.Name);
			writer.WriteEndElement();
		}
	}
}
