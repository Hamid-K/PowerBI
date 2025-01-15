using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design
{
	// Token: 0x02000385 RID: 901
	public class ExternalXmlElement : IXmlSerializable
	{
		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x0007A6C8 File Offset: 0x000788C8
		public XmlElement XmlElement
		{
			get
			{
				if (this.m_xml != null)
				{
					return this.m_xml.FirstChild as XmlElement;
				}
				return null;
			}
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x000025F4 File Offset: 0x000007F4
		public ExternalXmlElement()
		{
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x0007A6E4 File Offset: 0x000788E4
		public ExternalXmlElement(XmlElement externalXmlElement)
		{
			this.m_xml = new XmlDocument();
			XmlNode xmlNode = ((XmlDocument)this.m_xml).ImportNode(externalXmlElement, true);
			this.m_xml.AppendChild(xmlNode);
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x00005C88 File Offset: 0x00003E88
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x0007A724 File Offset: 0x00078924
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlNode));
			this.m_xml = xmlSerializer.Deserialize(reader) as XmlNode;
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x0007A753 File Offset: 0x00078953
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_xml != null)
			{
				writer.WriteRaw(this.m_xml.InnerXml);
			}
		}

		// Token: 0x04000C9A RID: 3226
		private XmlNode m_xml;
	}
}
