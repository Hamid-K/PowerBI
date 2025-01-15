using System;

namespace Microsoft.IdentityModel.Json.Linq
{
	// Token: 0x020000C6 RID: 198
	internal class JsonSelectSettings
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0002B29C File Offset: 0x0002949C
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x0002B2A4 File Offset: 0x000294A4
		public TimeSpan? RegexMatchTimeout { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0002B2AD File Offset: 0x000294AD
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0002B2B5 File Offset: 0x000294B5
		public bool ErrorWhenNoMatch { get; set; }
	}
}
