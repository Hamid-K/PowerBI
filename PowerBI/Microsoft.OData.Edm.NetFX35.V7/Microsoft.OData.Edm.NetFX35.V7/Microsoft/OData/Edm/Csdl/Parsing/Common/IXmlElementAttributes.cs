using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001B0 RID: 432
	internal interface IXmlElementAttributes
	{
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000C1E RID: 3102
		IEnumerable<XmlAttributeInfo> Unused { get; }

		// Token: 0x170003D9 RID: 985
		XmlAttributeInfo this[string attributeName] { get; }
	}
}
