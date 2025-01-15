using System;

namespace Microsoft.ReportingServices.Diagnostics.Caching
{
	// Token: 0x0200007C RID: 124
	internal class CacheItem
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00010610 File Offset: 0x0000E810
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x00010618 File Offset: 0x0000E818
		public ICacheItemVersion Version { get; private set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00010621 File Offset: 0x0000E821
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x00010629 File Offset: 0x0000E829
		public object Value { get; private set; }

		// Token: 0x0600040E RID: 1038 RVA: 0x00010632 File Offset: 0x0000E832
		public CacheItem(object value, ICacheItemVersion version)
		{
			this.Value = value;
			this.Version = version;
		}
	}
}
