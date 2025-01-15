using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000EA RID: 234
	internal struct MemoryPressureMonitorChange
	{
		// Token: 0x1700012C RID: 300
		public bool this[string key]
		{
			get
			{
				return (this._changeFlag & MemoryPressureMonitorChange.MapStringToEnum(key)) != MemoryPressureMonitorChanges.NoChange;
			}
			set
			{
				if (value)
				{
					this._changeFlag |= MemoryPressureMonitorChange.MapStringToEnum(key);
					return;
				}
				this._changeFlag &= ~MemoryPressureMonitorChange.MapStringToEnum(key);
			}
		}

		// Token: 0x1700012D RID: 301
		public bool this[MemoryPressureMonitorChanges key]
		{
			get
			{
				return (this._changeFlag & key) != MemoryPressureMonitorChanges.NoChange;
			}
			set
			{
				if (value)
				{
					this._changeFlag |= key;
					return;
				}
				this._changeFlag &= ~key;
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001A09D File Offset: 0x0001829D
		public MemoryPressureMonitorChange(MemoryPressureMonitorChange original)
		{
			this._changeFlag = original._changeFlag;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001A0AC File Offset: 0x000182AC
		private static MemoryPressureMonitorChanges MapStringToEnum(string name)
		{
			switch (name)
			{
			case "bufferSize":
				return MemoryPressureMonitorChanges.BufferSizeChange;
			case "maxObjectPoolSize":
				return MemoryPressureMonitorChanges.MaxObjectPoolSizeChange;
			case "minObjectPoolSize":
				return MemoryPressureMonitorChanges.MinObjectPoolSizeChange;
			case "syncGCInterval":
				return MemoryPressureMonitorChanges.SyncGCIntervalChange;
			case "throttleGCInterval":
				return MemoryPressureMonitorChanges.ThrottleGCIntervalChange;
			case "internalThrottleHighPercent":
				return MemoryPressureMonitorChanges.InternalThrottleHighPercentChange;
			case "internalThrottleLowPercent":
				return MemoryPressureMonitorChanges.InternalThrottleLowPercentChange;
			case "throttleHighPercent":
				return MemoryPressureMonitorChanges.ThrottleHighPercentChange;
			case "throttleLowPercent":
				return MemoryPressureMonitorChanges.ThrottleLowPercentChange;
			case "isEnabled":
				return MemoryPressureMonitorChanges.IsEnabledChange;
			case "memoryManagerEnabled":
				return MemoryPressureMonitorChanges.IsMemoryManagerEnabledChange;
			case "lowMemoryPercent":
				return MemoryPressureMonitorChanges.LowMemoryPercentChange;
			case "lowMemoryReleasePercent":
				return MemoryPressureMonitorChanges.LowMemoryReleasePercentChange;
			case "interval":
				return MemoryPressureMonitorChanges.IntervalChange;
			case "percentItemInFinalizerQueue":
				return MemoryPressureMonitorChanges.PercentItemInFinalizerQueue;
			case "memoryManagerPauseThreshold":
				return MemoryPressureMonitorChanges.MemoryManagerPauseThreshold;
			case "averageCacheItemSize":
				return MemoryPressureMonitorChanges.AverageCacheItemSizeInBytes;
			case "cacheUserDataSizePerNode":
				return MemoryPressureMonitorChanges.CacheUserDataSizePerNodeChange;
			}
			throw new ArgumentException("Unknown Config Element", "name");
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001A282 File Offset: 0x00018482
		public bool Changed
		{
			get
			{
				return this._changeFlag != MemoryPressureMonitorChanges.NoChange;
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001A290 File Offset: 0x00018490
		internal bool CanUpdateConfigDynamically()
		{
			MemoryPressureMonitorChange memoryPressureMonitorChange = new MemoryPressureMonitorChange(this);
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.IsMemoryManagerEnabledChange] = false;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.BufferSizeChange] = false;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.MinObjectPoolSizeChange] = false;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.MaxObjectPoolSizeChange] = false;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.CacheUserDataSizePerNodeChange] = false;
			return !memoryPressureMonitorChange.Changed;
		}

		// Token: 0x0400042B RID: 1067
		private MemoryPressureMonitorChanges _changeFlag;
	}
}
