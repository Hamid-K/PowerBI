using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000015 RID: 21
	internal class CacheDelegateStore
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00004948 File Offset: 0x00002B48
		public CacheDelegateStore()
		{
			this._regionStore = new Dictionary<string, RegionDelegateStore>();
			this._dList = new DelegateList();
			this._bulkList = new DelegateList();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004974 File Offset: 0x00002B74
		public DataCacheNotificationDescriptor AddDelegate(ItemNotificationDescriptor id, int filterMask, DataCacheNotificationCallback cacheDelegate)
		{
			RegionDelegateStore regionDelegateStore;
			this._regionStore.TryGetValue(id.RegionName, out regionDelegateStore);
			if (regionDelegateStore == null)
			{
				regionDelegateStore = new RegionDelegateStore();
				this._regionStore.Add(id.RegionName, regionDelegateStore);
			}
			this._count++;
			return regionDelegateStore.AddDelegate(id, filterMask, cacheDelegate);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000049C8 File Offset: 0x00002BC8
		public DataCacheNotificationDescriptor AddDelegate(RegionNotificationDescriptor rd, int filterMask, DataCacheNotificationCallback cacheDelegate)
		{
			RegionDelegateStore regionDelegateStore;
			this._regionStore.TryGetValue(rd.RegionName, out regionDelegateStore);
			if (regionDelegateStore == null)
			{
				regionDelegateStore = new RegionDelegateStore();
				this._regionStore.Add(rd.RegionName, regionDelegateStore);
			}
			this._count++;
			return regionDelegateStore.AddDelegate(rd, filterMask, cacheDelegate);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004A1B File Offset: 0x00002C1B
		public DataCacheNotificationDescriptor AddDelegate(DataCacheNotificationDescriptor nd, int filterMask, object cacheDelegate)
		{
			this._count++;
			return this._dList.AddDelegate(nd, cacheDelegate, filterMask);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004A39 File Offset: 0x00002C39
		public DataCacheNotificationDescriptor AddBulkDelegate(DataCacheNotificationDescriptor nd, int filterMask, object callabck)
		{
			this._count++;
			this._bulkCount++;
			return this._bulkList.AddDelegate(nd, callabck, filterMask);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004A65 File Offset: 0x00002C65
		public bool IsNonBulkNotificationsPresent()
		{
			return this._count > this._bulkCount;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004A78 File Offset: 0x00002C78
		public PerDelegateInfo RemoveDelegate(DataCacheNotificationDescriptor nd)
		{
			PerDelegateInfo perDelegateInfo = null;
			RegionNotificationDescriptor regionNotificationDescriptor = nd as RegionNotificationDescriptor;
			if (regionNotificationDescriptor != null)
			{
				RegionDelegateStore regionDelegateStore;
				this._regionStore.TryGetValue(regionNotificationDescriptor.RegionName, out regionDelegateStore);
				if (regionDelegateStore != null)
				{
					perDelegateInfo = regionDelegateStore.RemoveDelegate(regionNotificationDescriptor);
					if (regionDelegateStore.Count == 0)
					{
						this._regionStore.Remove(regionNotificationDescriptor.RegionName);
					}
				}
			}
			else
			{
				perDelegateInfo = this._bulkList.RemoveDelegate(nd);
				if (perDelegateInfo == null)
				{
					perDelegateInfo = this._dList.RemoveDelegate(nd);
				}
				else
				{
					this._bulkCount--;
				}
			}
			if (perDelegateInfo != null)
			{
				this._count--;
			}
			return perDelegateInfo;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004B0C File Offset: 0x00002D0C
		public void GetInvocationList(DataCacheOperationDescriptor nEvent, Queue<PerDelegateInfo> invocationList)
		{
			if (nEvent.RegionName != null)
			{
				RegionDelegateStore regionDelegateStore = null;
				this._regionStore.TryGetValue(nEvent.RegionName, out regionDelegateStore);
				if (regionDelegateStore != null)
				{
					regionDelegateStore.GetInvocationList(nEvent, invocationList);
				}
			}
			this._dList.GetInvocationList((int)nEvent.InternalOperationType, invocationList);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004B54 File Offset: 0x00002D54
		public void GetBulkInvocationList(int filterMask, Queue<PerDelegateInfo> invocationList)
		{
			this._bulkList.GetInvocationList(filterMask, invocationList);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004B63 File Offset: 0x00002D63
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x0400006A RID: 106
		private DelegateList _dList;

		// Token: 0x0400006B RID: 107
		private DelegateList _bulkList;

		// Token: 0x0400006C RID: 108
		private Dictionary<string, RegionDelegateStore> _regionStore;

		// Token: 0x0400006D RID: 109
		private int _count;

		// Token: 0x0400006E RID: 110
		private int _bulkCount;
	}
}
