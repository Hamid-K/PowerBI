using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000016 RID: 22
	internal class RegionDelegateStore
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00004B6B File Offset: 0x00002D6B
		public RegionDelegateStore()
		{
			this._itemDelegateStore = new Dictionary<string, DelegateList>();
			this._dList = new DelegateList();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004B8C File Offset: 0x00002D8C
		public DataCacheNotificationDescriptor AddDelegate(ItemNotificationDescriptor id, int filterMask, DataCacheNotificationCallback cacheDelegate)
		{
			DelegateList delegateList = null;
			this._itemDelegateStore.TryGetValue(id.Key, out delegateList);
			if (delegateList == null)
			{
				delegateList = new DelegateList();
				this._itemDelegateStore.Add(id.Key, delegateList);
			}
			this._count++;
			return delegateList.AddDelegate(id, cacheDelegate, filterMask);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004BE1 File Offset: 0x00002DE1
		public DataCacheNotificationDescriptor AddDelegate(RegionNotificationDescriptor rd, int filterMask, DataCacheNotificationCallback cacheDelegate)
		{
			this._count++;
			return this._dList.AddDelegate(rd, cacheDelegate, filterMask);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004C00 File Offset: 0x00002E00
		public PerDelegateInfo RemoveDelegate(RegionNotificationDescriptor nd)
		{
			PerDelegateInfo perDelegateInfo = null;
			ItemNotificationDescriptor itemNotificationDescriptor = nd as ItemNotificationDescriptor;
			if (itemNotificationDescriptor != null)
			{
				DelegateList delegateList;
				this._itemDelegateStore.TryGetValue(itemNotificationDescriptor.Key, out delegateList);
				if (delegateList != null)
				{
					perDelegateInfo = delegateList.RemoveDelegate(itemNotificationDescriptor);
					if (delegateList.Count == 0)
					{
						this._itemDelegateStore.Remove(itemNotificationDescriptor.Key);
					}
				}
			}
			else
			{
				perDelegateInfo = this._dList.RemoveDelegate(nd);
			}
			if (perDelegateInfo != null)
			{
				this._count--;
			}
			return perDelegateInfo;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004C74 File Offset: 0x00002E74
		public void GetInvocationList(DataCacheOperationDescriptor nEvent, Queue<PerDelegateInfo> invocationList)
		{
			CacheEventType internalOperationType = nEvent.InternalOperationType;
			if (internalOperationType <= CacheEventType.CreateRegionEvent)
			{
				switch (internalOperationType)
				{
				case CacheEventType.AddEvent:
				case CacheEventType.ReplaceEvent:
				case CacheEventType.RemoveEvent:
				{
					DelegateList delegateList = null;
					this._itemDelegateStore.TryGetValue(nEvent.Key, out delegateList);
					if (delegateList != null)
					{
						delegateList.GetInvocationList((int)nEvent.InternalOperationType, invocationList);
					}
					break;
				}
				case CacheEventType.AddEvent | CacheEventType.ReplaceEvent:
					break;
				default:
					if (internalOperationType != CacheEventType.CreateRegionEvent)
					{
					}
					break;
				}
			}
			else if (internalOperationType == CacheEventType.RemoveRegionEvent || internalOperationType == CacheEventType.ClearRegionEvent)
			{
				foreach (DelegateList delegateList2 in this._itemDelegateStore.Values)
				{
					delegateList2.GetInvocationList(4, invocationList);
				}
			}
			this._dList.GetInvocationList((int)nEvent.InternalOperationType, invocationList);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004D3C File Offset: 0x00002F3C
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x0400006F RID: 111
		private DelegateList _dList;

		// Token: 0x04000070 RID: 112
		private Dictionary<string, DelegateList> _itemDelegateStore;

		// Token: 0x04000071 RID: 113
		private int _count;
	}
}
