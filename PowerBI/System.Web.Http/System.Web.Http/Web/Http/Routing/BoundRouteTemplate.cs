using System;

namespace System.Web.Http.Routing
{
	// Token: 0x0200015A RID: 346
	internal class BoundRouteTemplate
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00017A40 File Offset: 0x00015C40
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x00017A48 File Offset: 0x00015C48
		public string BoundTemplate { get; set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00017A51 File Offset: 0x00015C51
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x00017A59 File Offset: 0x00015C59
		public HttpRouteValueDictionary Values { get; set; }
	}
}
