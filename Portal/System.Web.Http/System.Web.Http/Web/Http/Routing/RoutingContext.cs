using System;
using System.Collections.Generic;

namespace System.Web.Http.Routing
{
	// Token: 0x02000158 RID: 344
	internal class RoutingContext
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x000179E0 File Offset: 0x00015BE0
		public static RoutingContext Invalid()
		{
			return RoutingContext.CachedInvalid;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000179E7 File Offset: 0x00015BE7
		public static RoutingContext Valid(List<string> pathSegments)
		{
			return new RoutingContext
			{
				PathSegments = pathSegments,
				IsValid = true
			};
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00003AA7 File Offset: 0x00001CA7
		private RoutingContext()
		{
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x000179FC File Offset: 0x00015BFC
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x00017A04 File Offset: 0x00015C04
		public bool IsValid { get; private set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00017A0D File Offset: 0x00015C0D
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x00017A15 File Offset: 0x00015C15
		public List<string> PathSegments { get; private set; }

		// Token: 0x04000282 RID: 642
		private static readonly RoutingContext CachedInvalid = new RoutingContext
		{
			IsValid = false
		};
	}
}
