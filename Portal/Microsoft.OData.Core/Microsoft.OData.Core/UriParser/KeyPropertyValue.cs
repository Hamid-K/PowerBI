using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000143 RID: 323
	internal sealed class KeyPropertyValue
	{
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0002EDCD File Offset: 0x0002CFCD
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x0002EDD5 File Offset: 0x0002CFD5
		public IEdmProperty KeyProperty { get; set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0002EDDE File Offset: 0x0002CFDE
		// (set) Token: 0x060010C2 RID: 4290 RVA: 0x0002EDE6 File Offset: 0x0002CFE6
		public SingleValueNode KeyValue { get; set; }
	}
}
