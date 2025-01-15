using System;
using System.Globalization;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002DE RID: 734
	internal class BufferMemoryManagerStats
	{
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001B21 RID: 6945 RVA: 0x000521E8 File Offset: 0x000503E8
		public double AvailableMemoryPercentage
		{
			get
			{
				return (double)(this.MaxMemoryCapacity - this.CurrentMemoryUsage) / (double)this.MaxMemoryCapacity * 100.0;
			}
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0005220C File Offset: 0x0005040C
		public string GetPoolStatString(bool logPercentages)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("BufferMemory: Available: {0:F}%, Capacity: {1} bytes, ", this.AvailableMemoryPercentage, this.MaxMemoryCapacity);
			if (logPercentages)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "DirectoryPool: {0}, ", new object[] { this.DirectoryPoolStats.GetAvailablePercentageLogString() });
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "CacheItemPool: {0}, ", new object[] { this.CacheItemPoolStats.GetAvailablePercentageLogString() });
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "WriteBackItemPool: {0}", new object[] { this.WriteBackPoolStats.GetAvailablePercentageLogString() });
			}
			else
			{
				stringBuilder.AppendFormat("DirectoryPool: {0}", this.DirectoryPoolStats.GetCapacityLogString());
				stringBuilder.AppendFormat("CacheItemPool: {0}", this.CacheItemPoolStats.GetCapacityLogString());
				stringBuilder.AppendFormat("WriteBackItemPool: {0}", this.WriteBackPoolStats.GetCapacityLogString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000E61 RID: 3681
		internal long MaxMemoryCapacity;

		// Token: 0x04000E62 RID: 3682
		internal long CurrentMemoryUsage;

		// Token: 0x04000E63 RID: 3683
		internal BufferMemoryManagerStats.PoolStats CacheItemPoolStats;

		// Token: 0x04000E64 RID: 3684
		internal BufferMemoryManagerStats.PoolStats DirectoryPoolStats;

		// Token: 0x04000E65 RID: 3685
		internal BufferMemoryManagerStats.PoolStats WriteBackPoolStats;

		// Token: 0x020002DF RID: 735
		internal struct PoolStats
		{
			// Token: 0x1700059E RID: 1438
			// (get) Token: 0x06001B24 RID: 6948 RVA: 0x00052306 File Offset: 0x00050506
			internal long UsedObjects
			{
				get
				{
					return this.CurrentCapacity - this.AvailableObjects;
				}
			}

			// Token: 0x06001B25 RID: 6949 RVA: 0x00052315 File Offset: 0x00050515
			internal bool CanGrowPool()
			{
				return this.MaxCapacity > this.CurrentCapacity;
			}

			// Token: 0x1700059F RID: 1439
			// (get) Token: 0x06001B26 RID: 6950 RVA: 0x00052325 File Offset: 0x00050525
			public double AvailableCurrentPercentageInPool
			{
				get
				{
					return (double)this.AvailableObjects / (double)this.CurrentCapacity * 100.0;
				}
			}

			// Token: 0x170005A0 RID: 1440
			// (get) Token: 0x06001B27 RID: 6951 RVA: 0x00052340 File Offset: 0x00050540
			public double AvailablePercentageInPool
			{
				get
				{
					return (double)this.AvailableObjects / (double)this.MaxCapacity * 100.0;
				}
			}

			// Token: 0x06001B28 RID: 6952 RVA: 0x0005235C File Offset: 0x0005055C
			public string GetCapacityLogString()
			{
				return string.Format(CultureInfo.InvariantCulture, "MaxCapacity: {0}, CurrentCapacity: {1}, Available: {2}", new object[] { this.MaxCapacity, this.CurrentCapacity, this.AvailableObjects });
			}

			// Token: 0x06001B29 RID: 6953 RVA: 0x000523AC File Offset: 0x000505AC
			public string GetAvailablePercentageLogString()
			{
				return string.Format(CultureInfo.InvariantCulture, "Available: {0:F}%, AvailableOfCurrentCapacity: {1:F}%", new object[] { this.AvailablePercentageInPool, this.AvailableCurrentPercentageInPool });
			}

			// Token: 0x04000E66 RID: 3686
			internal long MaxCapacity;

			// Token: 0x04000E67 RID: 3687
			internal long CurrentCapacity;

			// Token: 0x04000E68 RID: 3688
			internal long AvailableObjects;
		}
	}
}
