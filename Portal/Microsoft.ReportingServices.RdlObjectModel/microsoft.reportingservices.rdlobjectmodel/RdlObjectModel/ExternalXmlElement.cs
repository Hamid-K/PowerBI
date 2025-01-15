using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000C5 RID: 197
	public class ExternalXmlElement : IXmlSerializable
	{
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0001C2FC File Offset: 0x0001A4FC
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x0001C380 File Offset: 0x0001A580
		public XmlElement XmlElement
		{
			get
			{
				if (this.m_xml == null || this.m_xml.ChildNodes == null)
				{
					return null;
				}
				foreach (object obj in this.m_xml.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode is XmlElement)
					{
						return (XmlElement)xmlNode;
					}
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.m_xml = null;
					return;
				}
				this.m_xml = XmlUtils.CreateXmlDocumentWithNullResolver();
				XmlNode xmlNode = ((XmlDocument)this.m_xml).ImportNode(value, true);
				this.m_xml.AppendChild(xmlNode);
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001C3C3 File Offset: 0x0001A5C3
		public ExternalXmlElement()
		{
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001C3CB File Offset: 0x0001A5CB
		public ExternalXmlElement(XmlElement externalXmlElement)
		{
			this.XmlElement = externalXmlElement;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001C3DA File Offset: 0x0001A5DA
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001C3E0 File Offset: 0x0001A5E0
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlNode));
			this.m_xml = xmlSerializer.Deserialize(reader) as XmlNode;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001C410 File Offset: 0x0001A610
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_xml != null)
			{
				foreach (object obj in this.m_xml.ChildNodes)
				{
					((XmlNode)obj).WriteTo(writer);
				}
			}
		}

		// Token: 0x0400016F RID: 367
		private XmlNode m_xml;
	}
}
