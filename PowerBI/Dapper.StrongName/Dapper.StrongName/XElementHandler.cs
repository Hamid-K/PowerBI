using System;
using System.Xml.Linq;

namespace Dapper
{
	// Token: 0x02000017 RID: 23
	internal sealed class XElementHandler : XmlTypeHandler<XElement>
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00009ACF File Offset: 0x00007CCF
		protected override XElement Parse(string xml)
		{
			return XElement.Parse(xml);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00009AD7 File Offset: 0x00007CD7
		protected override string Format(XElement xml)
		{
			return xml.ToString();
		}
	}
}
