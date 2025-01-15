using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200001D RID: 29
	internal class RegionNotificationDescriptor : DataCacheNotificationDescriptor
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00005284 File Offset: 0x00003484
		internal RegionNotificationDescriptor(string cacheName, string regionName)
			: base(cacheName)
		{
			this._regionName = regionName;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005294 File Offset: 0x00003494
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}:{1}:{2}", new object[] { base.CacheName, this._regionName, base.DelegateId });
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000052D8 File Offset: 0x000034D8
		// (set) Token: 0x060000CD RID: 205 RVA: 0x000052E0 File Offset: 0x000034E0
		internal string RegionName
		{
			get
			{
				return this._regionName;
			}
			set
			{
				this._regionName = value;
			}
		}

		// Token: 0x0400007E RID: 126
		private string _regionName;
	}
}
