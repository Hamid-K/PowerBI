using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Fabric.Common;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000026 RID: 38
	internal class PartitionNodeInfo
	{
		// Token: 0x06000116 RID: 278 RVA: 0x000069BC File Offset: 0x00004BBC
		public PartitionNodeInfo(PartitionId partitionId)
		{
			this._partitionId = partitionId;
			this._currentLsn = new NotificationLsn(0L, 0L);
			this._regionCountTable = new Dictionary<string, int>();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000069E8 File Offset: 0x00004BE8
		public void AddRegion(string regionName)
		{
			try
			{
				int num = this._regionCountTable[regionName];
				num++;
				this._regionCountTable[regionName] = num;
			}
			catch (KeyNotFoundException)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<string>("DistributedCache.PartitionNodeInfo", "AddRegion : Key not found {0}", regionName);
				}
				if (this._regionList == null)
				{
					this._regionList = new List<string>();
				}
				this._regionList.Add(regionName);
				this._regionCountTable[regionName] = 1;
			}
			this._refCount++;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006A7C File Offset: 0x00004C7C
		public void RemoveRegion(string regionName)
		{
			try
			{
				int num = this._regionCountTable[regionName];
				num--;
				if (num > 0)
				{
					this._regionCountTable[regionName] = num;
				}
				else
				{
					this._regionCountTable.Remove(regionName);
					this._regionList.Remove(regionName);
					if (this._regionList.Count == 0)
					{
						this._regionList = null;
					}
				}
				this._refCount--;
			}
			catch (KeyNotFoundException)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<string>("DistributedCache.PartitionNodeInfo", "RemoveRegion : Key not found {0}", regionName);
				}
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006B18 File Offset: 0x00004D18
		public void AddForCache()
		{
			this._cacheLevelCount++;
			this._refCount++;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006B36 File Offset: 0x00004D36
		public void RemoveForCache()
		{
			this._cacheLevelCount--;
			this._refCount--;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006B54 File Offset: 0x00004D54
		public PartitionId PartitionId
		{
			get
			{
				return this._partitionId;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00006B5C File Offset: 0x00004D5C
		public int RefCount
		{
			get
			{
				return this._refCount;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00006B64 File Offset: 0x00004D64
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00006B6C File Offset: 0x00004D6C
		public NotificationLsn CurrentLsn
		{
			get
			{
				return this._currentLsn;
			}
			set
			{
				this._currentLsn = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00006B75 File Offset: 0x00004D75
		public bool IsCacheLevel
		{
			get
			{
				return this._cacheLevelCount > 0;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00006B80 File Offset: 0x00004D80
		public List<string> RegionList
		{
			get
			{
				return this._regionList;
			}
		}

		// Token: 0x0400009D RID: 157
		private const string _myComponentName = "DistributedCache.PartitionNodeInfo";

		// Token: 0x0400009E RID: 158
		private PartitionId _partitionId;

		// Token: 0x0400009F RID: 159
		private List<string> _regionList;

		// Token: 0x040000A0 RID: 160
		private Dictionary<string, int> _regionCountTable;

		// Token: 0x040000A1 RID: 161
		private int _cacheLevelCount;

		// Token: 0x040000A2 RID: 162
		private int _refCount;

		// Token: 0x040000A3 RID: 163
		private NotificationLsn _currentLsn;
	}
}
