using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000103 RID: 259
	internal sealed class KeyPropertyValue
	{
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0002176B File Offset: 0x0001F96B
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x00021773 File Offset: 0x0001F973
		public IEdmProperty KeyProperty { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0002177C File Offset: 0x0001F97C
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x00021784 File Offset: 0x0001F984
		public SingleValueNode KeyValue { get; set; }
	}
}
