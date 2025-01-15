using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200005D RID: 93
	[Serializable]
	public class Column : IXmlSerializable, IName
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0001186C File Offset: 0x0000FA6C
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00011874 File Offset: 0x0000FA74
		public string Name { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0001187D File Offset: 0x0000FA7D
		// (set) Token: 0x0600039F RID: 927 RVA: 0x00011885 File Offset: 0x0000FA85
		public string Description { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0001188E File Offset: 0x0000FA8E
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x00011896 File Offset: 0x0000FA96
		public int Ordinal { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x000118A7 File Offset: 0x0000FAA7
		public Type Type { get; set; }

		// Token: 0x060003A4 RID: 932 RVA: 0x000118B0 File Offset: 0x0000FAB0
		public Column()
		{
			this.Ordinal = -1;
			this.Type = typeof(string);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000118CF File Offset: 0x0000FACF
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000118D4 File Offset: 0x0000FAD4
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.Ordinal = -1;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/ns:Column", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.Name = xmlNode.Attributes.GetNamedItemOrDefault("name", null);
				this.Description = xmlNode.Attributes.GetNamedItemOrDefault("description", null);
				XmlNode xmlNode2;
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("ordinal")) != null)
				{
					this.Ordinal = int.Parse(xmlNode2.Value);
				}
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("type")) != null)
				{
					this.Type = Type.GetType(xmlNode2.Value);
				}
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00011984 File Offset: 0x0000FB84
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Column");
			if (!string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(this.Name))
			{
				writer.WriteAttributeString("name", this.Name);
			}
			if (!string.IsNullOrEmpty(this.Description) && !string.IsNullOrEmpty(this.Description))
			{
				writer.WriteAttributeString("description", this.Description);
			}
			if (this.Ordinal >= 0)
			{
				writer.WriteAttributeString("ordinal", this.Ordinal.ToString());
			}
			if (this.Type != null)
			{
				writer.WriteAttributeString("type", this.Type.FullName);
			}
			writer.WriteEndElement();
		}
	}
}
