using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200024F RID: 591
	internal struct EvictedElement
	{
		// Token: 0x060013CA RID: 5066 RVA: 0x0003E004 File Offset: 0x0003C204
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}:{2}", new object[] { this.RegionName, this.Key, this.Version });
		}

		// Token: 0x04000BEA RID: 3050
		public int index;

		// Token: 0x04000BEB RID: 3051
		public Key Key;

		// Token: 0x04000BEC RID: 3052
		public string RegionName;

		// Token: 0x04000BED RID: 3053
		public InternalCacheItemVersion Version;

		// Token: 0x04000BEE RID: 3054
		public AOMCacheItem CacheItem;
	}
}
