using System;

namespace Microsoft.OData
{
	// Token: 0x020000D6 RID: 214
	public sealed class ODataUrlKeyDelimiter
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x000178F4 File Offset: 0x00015AF4
		private ODataUrlKeyDelimiter(bool enablekeyAsSegment)
		{
			this.enableKeyAsSegment = enablekeyAsSegment;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00017903 File Offset: 0x00015B03
		public static ODataUrlKeyDelimiter Parentheses
		{
			get
			{
				return ODataUrlKeyDelimiter.parenthesesDelimiter;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0001790A File Offset: 0x00015B0A
		public static ODataUrlKeyDelimiter Slash
		{
			get
			{
				return ODataUrlKeyDelimiter.slashDelimiter;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00017911 File Offset: 0x00015B11
		internal bool EnableKeyAsSegment
		{
			get
			{
				return this.enableKeyAsSegment;
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00017919 File Offset: 0x00015B19
		internal static ODataUrlKeyDelimiter GetODataUrlKeyDelimiter(IServiceProvider container)
		{
			if (!ODataSimplifiedOptions.GetODataSimplifiedOptions(container).EnableParsingKeyAsSegmentUrl)
			{
				return ODataUrlKeyDelimiter.Parentheses;
			}
			return ODataUrlKeyDelimiter.Slash;
		}

		// Token: 0x04000390 RID: 912
		private static readonly ODataUrlKeyDelimiter slashDelimiter = new ODataUrlKeyDelimiter(true);

		// Token: 0x04000391 RID: 913
		private static readonly ODataUrlKeyDelimiter parenthesesDelimiter = new ODataUrlKeyDelimiter(false);

		// Token: 0x04000392 RID: 914
		private readonly bool enableKeyAsSegment;
	}
}
