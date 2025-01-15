using System;
using System.Xml.Linq;

namespace Dapper
{
	// Token: 0x02000016 RID: 22
	internal sealed class XDocumentHandler : XmlTypeHandler<XDocument>
	{
		// Token: 0x06000157 RID: 343 RVA: 0x00009AB7 File Offset: 0x00007CB7
		protected override XDocument Parse(string xml)
		{
			return XDocument.Parse(xml);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00009ABF File Offset: 0x00007CBF
		protected override string Format(XDocument xml)
		{
			return xml.ToString();
		}
	}
}
