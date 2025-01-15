using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000335 RID: 821
	internal sealed class MemoryManagerPerformanceCounter
	{
		// Token: 0x06001D9C RID: 7580 RVA: 0x00059328 File Offset: 0x00057528
		internal MemoryManagerPerformanceCounter(BufferMemoryManager memoryManager)
		{
			this._availableMemory = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_AVAILABLE_MEMORY, new PerfCounterValue(memoryManager.GetAvailableMemory));
			this._availableMemoryPercentage = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.AVAILABLE_MEMORY_PERCENTAGE, new PerfCounterValue(memoryManager.GetAvailableMemory));
			this._availableMemoryPercentageBase = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.AVAILABLE_MEMORY_PERCENTAGE_BASE, new PerfCounterValue(memoryManager.GetMaxMemory));
			this._availableCacheItemCount = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_AVAILABLE_CACHEITEM_COUNT, new PerfCounterValue(memoryManager.GetAvailableCacheItemCount));
			this._allocatedCacheItemCount = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_ALLOCATED_CACHEITEM_COUNT, new PerfCounterValue(memoryManager.GetAllocatedCaceItemCount));
			this._availableCacheItemPercentage = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.AVAILABLE_CACHEITEM_PERCENTAGE, new PerfCounterValue(memoryManager.GetAvailableCacheItemCount));
			this._availableCacheItemPercentageBase = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.AVAILABLE_CACHEITEM_PERCENTAGE_BASE, new PerfCounterValue(memoryManager.GetMaxCacheItemCapacityCount));
			this._availableDirectoryCount = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_AVAILABLE_DIRECTORY_COUNT, new PerfCounterValue(memoryManager.GetAvailableDirectoryCount));
			this._allocatedDirectoryCount = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_ALLOCATED_DIRECTORY_COUNT, new PerfCounterValue(memoryManager.GetAllocatedDirectoryCount));
			this._availableDirectoryPercentage = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.AVAILABLE_DIRECTORY_PERCENTAGE, new PerfCounterValue(memoryManager.GetAvailableDirectoryCount));
			this._availableDirectoryPercentageBase = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.AVAILABLE_DIRECTORY_PERCENTAGE_BASE, new PerfCounterValue(memoryManager.GetMaxDirectoryCapacityCount));
			this._availableWBItemCount = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_AVAILABLE_WBITEM_COUNT, new PerfCounterValue(memoryManager.GetAvailableWBItemCount));
			this._allocatedWBItemCount = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_ALLOCATED_WBITEM_COUNT, new PerfCounterValue(memoryManager.GetAllocatedWBItemCount));
			this._availableWBItemPercentage = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.AVAILABLE_WBITEM_PERCENTAGE, new PerfCounterValue(memoryManager.GetAvailableWBItemCount));
			this._availableWBItemPercentageBase = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.AVAILABLE_WBITEM_PERCENTAGE_BASE, new PerfCounterValue(memoryManager.GetMaxWBItemCapacityCount));
		}

		// Token: 0x04001059 RID: 4185
		private DelegateBasedHostPerfCounter _availableMemory;

		// Token: 0x0400105A RID: 4186
		private DelegateBasedHostPerfCounter _availableMemoryPercentageBase;

		// Token: 0x0400105B RID: 4187
		private DelegateBasedHostPerfCounter _availableMemoryPercentage;

		// Token: 0x0400105C RID: 4188
		private DelegateBasedHostPerfCounter _availableCacheItemCount;

		// Token: 0x0400105D RID: 4189
		private DelegateBasedHostPerfCounter _allocatedCacheItemCount;

		// Token: 0x0400105E RID: 4190
		private DelegateBasedHostPerfCounter _availableCacheItemPercentageBase;

		// Token: 0x0400105F RID: 4191
		private DelegateBasedHostPerfCounter _availableCacheItemPercentage;

		// Token: 0x04001060 RID: 4192
		private DelegateBasedHostPerfCounter _availableDirectoryCount;

		// Token: 0x04001061 RID: 4193
		private DelegateBasedHostPerfCounter _allocatedDirectoryCount;

		// Token: 0x04001062 RID: 4194
		private DelegateBasedHostPerfCounter _availableDirectoryPercentageBase;

		// Token: 0x04001063 RID: 4195
		private DelegateBasedHostPerfCounter _availableDirectoryPercentage;

		// Token: 0x04001064 RID: 4196
		private DelegateBasedHostPerfCounter _availableWBItemCount;

		// Token: 0x04001065 RID: 4197
		private DelegateBasedHostPerfCounter _allocatedWBItemCount;

		// Token: 0x04001066 RID: 4198
		private DelegateBasedHostPerfCounter _availableWBItemPercentageBase;

		// Token: 0x04001067 RID: 4199
		private DelegateBasedHostPerfCounter _availableWBItemPercentage;
	}
}
