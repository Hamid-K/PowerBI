using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200001E RID: 30
	internal class ItemNotificationDescriptor : RegionNotificationDescriptor
	{
		// Token: 0x060000CE RID: 206 RVA: 0x000052E9 File Offset: 0x000034E9
		internal ItemNotificationDescriptor(string cacheName, string regionName, string itemKey)
			: base(cacheName, regionName)
		{
			this._key = itemKey;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000052FC File Offset: 0x000034FC
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}:{1}:{2}:{3}", new object[] { base.CacheName, base.RegionName, this._key, base.DelegateId });
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005349 File Offset: 0x00003549
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00005351 File Offset: 0x00003551
		internal string Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		// Token: 0x0400007F RID: 127
		private string _key;
	}
}
