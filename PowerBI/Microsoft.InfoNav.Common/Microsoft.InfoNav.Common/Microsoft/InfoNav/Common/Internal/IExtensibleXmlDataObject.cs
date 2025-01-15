using System;
using System.Xml;

namespace Microsoft.InfoNav.Common.Internal
{
	// Token: 0x02000088 RID: 136
	public interface IExtensibleXmlDataObject
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060004E9 RID: 1257
		// (set) Token: 0x060004EA RID: 1258
		XmlElement[] ExtendedXmlElements { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060004EB RID: 1259
		// (set) Token: 0x060004EC RID: 1260
		XmlAttribute[] ExtendedXmlAttributes { get; set; }
	}
}
