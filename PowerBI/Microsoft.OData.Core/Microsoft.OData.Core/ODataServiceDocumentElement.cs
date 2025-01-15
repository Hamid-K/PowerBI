using System;

namespace Microsoft.OData
{
	// Token: 0x020000BB RID: 187
	public abstract class ODataServiceDocumentElement : ODataAnnotatable
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00013949 File Offset: 0x00011B49
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x00013951 File Offset: 0x00011B51
		public Uri Url { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x0001395A File Offset: 0x00011B5A
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x00013962 File Offset: 0x00011B62
		public string Name { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0001396B File Offset: 0x00011B6B
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x00013973 File Offset: 0x00011B73
		public string Title { get; set; }
	}
}
