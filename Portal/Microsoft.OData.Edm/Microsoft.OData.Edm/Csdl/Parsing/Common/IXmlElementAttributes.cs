using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001BD RID: 445
	internal interface IXmlElementAttributes
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000CD0 RID: 3280
		IEnumerable<XmlAttributeInfo> Unused { get; }

		// Token: 0x17000423 RID: 1059
		XmlAttributeInfo this[string attributeName] { get; }
	}
}
