using System;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001E7 RID: 487
	public sealed class KeyPropertyValue
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0004011A File Offset: 0x0003E31A
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x00040122 File Offset: 0x0003E322
		public IEdmProperty KeyProperty { get; set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0004012B File Offset: 0x0003E32B
		// (set) Token: 0x060011C8 RID: 4552 RVA: 0x00040133 File Offset: 0x0003E333
		public SingleValueNode KeyValue { get; set; }
	}
}
