using System;
using System.Xml;

namespace Dapper
{
	// Token: 0x02000015 RID: 21
	internal sealed class XmlDocumentHandler : XmlTypeHandler<XmlDocument>
	{
		// Token: 0x06000154 RID: 340 RVA: 0x00009A8C File Offset: 0x00007C8C
		protected override XmlDocument Parse(string xml)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			return doc;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00009AA7 File Offset: 0x00007CA7
		protected override string Format(XmlDocument xml)
		{
			return xml.OuterXml;
		}
	}
}
