using System;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200084E RID: 2126
	internal sealed class GetReaderArgs
	{
		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x06003D4E RID: 15694 RVA: 0x000C75D9 File Offset: 0x000C57D9
		// (set) Token: 0x06003D4F RID: 15695 RVA: 0x000C75E1 File Offset: 0x000C57E1
		public bool IsFeed { get; set; }

		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x06003D50 RID: 15696 RVA: 0x000C75EA File Offset: 0x000C57EA
		// (set) Token: 0x06003D51 RID: 15697 RVA: 0x000C75F2 File Offset: 0x000C57F2
		public bool Catch404 { get; set; }

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x06003D52 RID: 15698 RVA: 0x000C75FB File Offset: 0x000C57FB
		// (set) Token: 0x06003D53 RID: 15699 RVA: 0x000C7603 File Offset: 0x000C5803
		public Uri Uri { get; set; }

		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x06003D54 RID: 15700 RVA: 0x000C760C File Offset: 0x000C580C
		// (set) Token: 0x06003D55 RID: 15701 RVA: 0x000C7614 File Offset: 0x000C5814
		public int? Column { get; set; }

		// Token: 0x06003D56 RID: 15702 RVA: 0x000C761D File Offset: 0x000C581D
		internal static Lazy<ODataReaderWrapper> MakeReader(Func<ODataReaderWrapper> ctor)
		{
			return new Lazy<ODataReaderWrapper>(ctor);
		}
	}
}
