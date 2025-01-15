using System;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200010E RID: 270
	internal class NavigationSourceAndAnnotations
	{
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x000269E0 File Offset: 0x00024BE0
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x000269E8 File Offset: 0x00024BE8
		public EdmNavigationSource NavigationSource { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x000269F1 File Offset: 0x00024BF1
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x000269F9 File Offset: 0x00024BF9
		public NavigationSourceConfiguration Configuration { get; set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00026A02 File Offset: 0x00024C02
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x00026A0A File Offset: 0x00024C0A
		public NavigationSourceLinkBuilderAnnotation LinkBuilder { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00026A13 File Offset: 0x00024C13
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x00026A1B File Offset: 0x00024C1B
		public NavigationSourceUrlAnnotation Url { get; set; }
	}
}
