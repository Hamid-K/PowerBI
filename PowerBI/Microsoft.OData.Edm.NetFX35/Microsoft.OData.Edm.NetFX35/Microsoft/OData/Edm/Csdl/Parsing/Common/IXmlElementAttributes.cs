using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x02000194 RID: 404
	internal interface IXmlElementAttributes
	{
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060007C4 RID: 1988
		IEnumerable<XmlAttributeInfo> Unused { get; }

		// Token: 0x17000331 RID: 817
		XmlAttributeInfo this[string attributeName] { get; }
	}
}
