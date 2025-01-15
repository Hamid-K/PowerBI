using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200002A RID: 42
	internal class CacheNotificationGroup
	{
		// Token: 0x06000133 RID: 307 RVA: 0x00006D18 File Offset: 0x00004F18
		internal CacheNotificationGroup(string cacheName, List<DataCacheOperationDescriptor> notifications, CacheEventType operation)
		{
			this._notifications = notifications;
			this._controlOperation = operation;
			this._cacheName = cacheName;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006D35 File Offset: 0x00004F35
		internal CacheNotificationGroup(string cacheName, List<DataCacheOperationDescriptor> notifications)
		{
			this._notifications = notifications;
			this._cacheName = cacheName;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00006D4B File Offset: 0x00004F4B
		internal List<DataCacheOperationDescriptor> Notifications
		{
			get
			{
				return this._notifications;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006D53 File Offset: 0x00004F53
		internal CacheEventType ControlOperation
		{
			get
			{
				return this._controlOperation;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00006D5B File Offset: 0x00004F5B
		internal string CacheName
		{
			get
			{
				return this._cacheName;
			}
		}

		// Token: 0x040000AC RID: 172
		private CacheEventType _controlOperation;

		// Token: 0x040000AD RID: 173
		private string _cacheName;

		// Token: 0x040000AE RID: 174
		private List<DataCacheOperationDescriptor> _notifications;
	}
}
