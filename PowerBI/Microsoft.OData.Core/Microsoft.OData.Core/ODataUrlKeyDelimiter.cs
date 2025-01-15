using System;

namespace Microsoft.OData
{
	// Token: 0x020000D9 RID: 217
	public sealed class ODataUrlKeyDelimiter
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x0001AD7A File Offset: 0x00018F7A
		private ODataUrlKeyDelimiter(bool enablekeyAsSegment)
		{
			this.enableKeyAsSegment = enablekeyAsSegment;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0001AD89 File Offset: 0x00018F89
		public static ODataUrlKeyDelimiter Parentheses
		{
			get
			{
				return ODataUrlKeyDelimiter.parenthesesDelimiter;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0001AD90 File Offset: 0x00018F90
		public static ODataUrlKeyDelimiter Slash
		{
			get
			{
				return ODataUrlKeyDelimiter.slashDelimiter;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0001AD97 File Offset: 0x00018F97
		internal bool EnableKeyAsSegment
		{
			get
			{
				return this.enableKeyAsSegment;
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0001ADA0 File Offset: 0x00018FA0
		internal static ODataUrlKeyDelimiter GetODataUrlKeyDelimiter(IServiceProvider container)
		{
			if (!ODataSimplifiedOptions.GetODataSimplifiedOptions(container, null).EnableParsingKeyAsSegmentUrl)
			{
				return ODataUrlKeyDelimiter.Parentheses;
			}
			return ODataUrlKeyDelimiter.Slash;
		}

		// Token: 0x040003BF RID: 959
		private static readonly ODataUrlKeyDelimiter slashDelimiter = new ODataUrlKeyDelimiter(true);

		// Token: 0x040003C0 RID: 960
		private static readonly ODataUrlKeyDelimiter parenthesesDelimiter = new ODataUrlKeyDelimiter(false);

		// Token: 0x040003C1 RID: 961
		private readonly bool enableKeyAsSegment;
	}
}
