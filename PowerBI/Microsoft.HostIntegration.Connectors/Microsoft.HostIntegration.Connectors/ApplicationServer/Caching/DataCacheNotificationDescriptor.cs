using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200001C RID: 28
	public class DataCacheNotificationDescriptor
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000521E File Offset: 0x0000341E
		internal DataCacheNotificationDescriptor(string cacheName)
		{
			this._cacheName = cacheName;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005230 File Offset: 0x00003430
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}:{1}", new object[] { this._cacheName, this._delegateId });
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000526B File Offset: 0x0000346B
		public string CacheName
		{
			get
			{
				return this._cacheName;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00005273 File Offset: 0x00003473
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x0000527B File Offset: 0x0000347B
		public long DelegateId
		{
			get
			{
				return this._delegateId;
			}
			internal set
			{
				this._delegateId = value;
			}
		}

		// Token: 0x0400007C RID: 124
		private string _cacheName;

		// Token: 0x0400007D RID: 125
		private long _delegateId;
	}
}
