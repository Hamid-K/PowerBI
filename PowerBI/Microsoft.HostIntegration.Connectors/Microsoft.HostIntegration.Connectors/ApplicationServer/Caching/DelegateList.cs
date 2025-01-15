using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000017 RID: 23
	internal class DelegateList
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004D44 File Offset: 0x00002F44
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004D4C File Offset: 0x00002F4C
		public DelegateList()
		{
			this._delegateList = new List<PerDelegateInfo>();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004D60 File Offset: 0x00002F60
		public DataCacheNotificationDescriptor AddDelegate(DataCacheNotificationDescriptor nd, object cacheDelegate, int filterMask)
		{
			PerDelegateInfo perDelegateInfo = new PerDelegateInfo(filterMask, cacheDelegate, nd);
			this._delegateList.Add(perDelegateInfo);
			this._count++;
			return nd;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004D94 File Offset: 0x00002F94
		public PerDelegateInfo RemoveDelegate(DataCacheNotificationDescriptor nd)
		{
			PerDelegateInfo perDelegateInfo = null;
			foreach (PerDelegateInfo perDelegateInfo2 in this._delegateList)
			{
				if (perDelegateInfo2.Nd.DelegateId == nd.DelegateId)
				{
					this._delegateList.Remove(perDelegateInfo2);
					this._count--;
					perDelegateInfo = perDelegateInfo2;
					break;
				}
			}
			return perDelegateInfo;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004E18 File Offset: 0x00003018
		public void GetInvocationList(int eventType, Queue<PerDelegateInfo> invocationList)
		{
			foreach (PerDelegateInfo perDelegateInfo in this._delegateList)
			{
				if ((perDelegateInfo.FilterMask & eventType) != 0)
				{
					invocationList.Enqueue(perDelegateInfo);
				}
			}
		}

		// Token: 0x04000072 RID: 114
		private List<PerDelegateInfo> _delegateList;

		// Token: 0x04000073 RID: 115
		private int _count;
	}
}
