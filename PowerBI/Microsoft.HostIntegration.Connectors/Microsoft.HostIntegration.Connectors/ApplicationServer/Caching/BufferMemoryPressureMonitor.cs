using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002E1 RID: 737
	internal class BufferMemoryPressureMonitor : IMonitor
	{
		// Token: 0x06001B2C RID: 6956 RVA: 0x000523EC File Offset: 0x000505EC
		private BufferMemoryPressureMonitor(MemoryUsageDelegate totalMemoryUsageCallback, ObjectCountDelegate totalObjectCountCallback, ObjectCountDelegate totalWBItemCountCallback, OnMemoryPressureCallBackEx loadSheddingCallback, RunCompactionDelegate runCompactionCallBack, PeriodicEvictionCheck periodicEvictionCheck, ThrottleDelegate startWriteThrottle, ThrottleDelegate stopWriteThrottle, ThrottleDelegate startConnectionThrottle, ThrottleDelegate stopConnectionThrottle, BufferMemoryManager memoryManager)
		{
			this._lastMemoryManagementRunTime = DateTime.UtcNow;
			this._lastGCRunThrottling = DateTime.UtcNow;
			this._totalMemoryUsageCallback = totalMemoryUsageCallback;
			this._totalObjectCountCallBack = totalObjectCountCallback;
			this._totalWBItemCountCallback = totalWBItemCountCallback;
			this._onLoadSheddingCallback = loadSheddingCallback;
			this._runCompactionCallBack = runCompactionCallBack;
			this._periodicEvictionCallback = periodicEvictionCheck;
			this._startWriteThrottle = startWriteThrottle;
			this._stopWriteThrottle = stopWriteThrottle;
			this._startConnectionThrottle = startConnectionThrottle;
			this._stopConnectionThrottle = stopConnectionThrottle;
			this._memoryManager = memoryManager;
			this._externalMemoryReleaseTargetInstance = new BufferMemoryPressureMonitor.ExternalMemoryReleaseTarget(this);
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x00052478 File Offset: 0x00050678
		public BufferMemoryPressureMonitor(MemoryUsageDelegate totalMemoryUsageCallback, ObjectCountDelegate totalObjectCountCallback, ObjectCountDelegate totalWBItemCountCallback, OnMemoryPressureCallBackEx loadSheddingCallback, RunCompactionDelegate runCompactionCallBack, ServiceConfigurationManager configurationManager, PeriodicEvictionCheck periodicEvictionCheck, ThrottleDelegate startWriteThrottle, ThrottleDelegate stopWriteThrottle, ThrottleDelegate startConnectionThrottle, ThrottleDelegate stopConnectionThrottle, BufferMemoryManager memoryManager)
			: this(totalMemoryUsageCallback, totalObjectCountCallback, totalWBItemCountCallback, loadSheddingCallback, runCompactionCallBack, periodicEvictionCheck, startWriteThrottle, stopWriteThrottle, startConnectionThrottle, stopConnectionThrottle, memoryManager)
		{
			MemoryPressureMonitorProperties memoryPressureMonitorProperties = configurationManager.AdvancedProperties.MemoryPressureMonitorProperties;
			this._interval = memoryPressureMonitorProperties.Interval;
			this._lowMemoryPercent = memoryPressureMonitorProperties.LowMemoryPercent;
			this._isSystemMemoryPressureEnabled = configurationManager.IsSystemMemoryCheckEnabled;
			this._throttleLowPercentExternal = memoryPressureMonitorProperties.ThrottleLowPercent;
			this._throttleHighPercentExternal = memoryPressureMonitorProperties.ThrottleHighPercent;
			this._throttleLowPercentInternal = memoryPressureMonitorProperties.InternalThrottleLowPercent;
			this._throttleHighPercentInternal = memoryPressureMonitorProperties.InternalThrottleHighPercent;
			this._percentItemInFinalizerQueue = memoryPressureMonitorProperties.PercentItemInFinalizerQueue;
			this._throttleGCInterval = memoryPressureMonitorProperties.ThrottleGCInterval;
			this._gcPauseThreshold = memoryPressureMonitorProperties.MemoryManagerPauseThreshold;
			this.Initialize();
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0005252C File Offset: 0x0005072C
		public BufferMemoryPressureMonitor(MemoryUsageDelegate totalMemoryUsageCallback, ObjectCountDelegate totalObjectCountCallback, ObjectCountDelegate totalWBItemCountCallback, OnMemoryPressureCallBackEx loadSheddingCallback, RunCompactionDelegate runCompactionCallBack, PeriodicEvictionCheck periodicEvictionCheck, ThrottleDelegate startWriteThrottle, ThrottleDelegate stopWriteThrottle, ThrottleDelegate startConnectionThrottle, ThrottleDelegate stopConnectionThrottle, BufferMemoryManager memoryManager, int interval, int lowMemoryPercent, int throttleLowPercent, int throttleHighPercent, int throttleGCInterval, bool systemMemoryCheck, int percentItemInFinalizerQueue)
			: this(totalMemoryUsageCallback, totalObjectCountCallback, totalWBItemCountCallback, loadSheddingCallback, runCompactionCallBack, periodicEvictionCheck, startWriteThrottle, stopWriteThrottle, startConnectionThrottle, stopConnectionThrottle, memoryManager)
		{
			this._isSystemMemoryPressureEnabled = systemMemoryCheck;
			this._interval = interval;
			this._lowMemoryPercent = lowMemoryPercent;
			this._throttleLowPercentExternal = throttleLowPercent;
			this._throttleHighPercentExternal = throttleHighPercent;
			this._throttleGCInterval = throttleGCInterval;
			this._throttleLowPercentInternal = throttleLowPercent;
			this._throttleHighPercentInternal = throttleHighPercent;
			this._percentItemInFinalizerQueue = percentItemInFinalizerQueue;
			this._gcPauseThreshold = 15;
			this.Initialize();
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x000525A8 File Offset: 0x000507A8
		private void Initialize()
		{
			this.UpdateMemoryStats();
			this.UpdateSytemMemoryStats();
			if (this.IsProcessMemoryMonitoringEnabled())
			{
				ulong num = (ulong)(this._memoryManagerStats.MaxMemoryCapacity * 100L / 50L);
				BufferMemoryPressureMonitor.SetProcessMinWorkingSetSize(num);
			}
			ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(this.MonitorThread), null);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x000525F6 File Offset: 0x000507F6
		private void ManageAllocatorMemoryAndObjectPool()
		{
			if (this.CanRunMemoryAndPoolManagement())
			{
				this.ManageAllocatorMemoryAndObjectPoolActual();
			}
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x00052608 File Offset: 0x00050808
		private void ManageAllocatorMemoryAndObjectPoolActual()
		{
			this._lastMemoryManagementRunTime = DateTime.UtcNow;
			bool flag = false;
			if (this._memoryManagerStats.DirectoryPoolStats.AvailableCurrentPercentageInPool < (double)this._percentItemInFinalizerQueue)
			{
				int num = this._runCompactionCallBack();
				if (num > 0)
				{
					flag = true;
				}
			}
			if (this.MemoryDiffPercentageInPoolAndHostSize >= (double)this._percentItemInFinalizerQueue || this.CacheItemDiffPercentageInPoolAndHostCount >= (double)this._percentItemInFinalizerQueue || this.WriteBackItemDiffPercentageInPoolAndHostCount >= (double)this._percentItemInFinalizerQueue)
			{
				flag = true;
			}
			else if (this.MemoryAllocatorNeedThrottling() || this.CacheItemPoolNeedThrottling() || this.DirectoryPoolNeedThrottling() || this.WriteBackItemPoolNeedThrottling())
			{
				flag = true;
			}
			if (flag)
			{
				this.RunNonBlockingGCAndWaitForFinalizer();
			}
			if (this._memoryManagerStats.DirectoryPoolStats.AvailableCurrentPercentageInPool < (double)this._percentItemInFinalizerQueue && this._memoryManagerStats.DirectoryPoolStats.CanGrowPool())
			{
				this._memoryManager.GrowDirectoryNodePool();
			}
			if (this._memoryManagerStats.CacheItemPoolStats.AvailableCurrentPercentageInPool < (double)this._percentItemInFinalizerQueue && this._memoryManagerStats.CacheItemPoolStats.CanGrowPool())
			{
				this._memoryManager.GrowCacheItemPool();
			}
			if (this._memoryManagerStats.WriteBackPoolStats.AvailableCurrentPercentageInPool < (double)this._percentItemInFinalizerQueue && this._memoryManager.IsWriteBackItemPoolAllocated && this._memoryManagerStats.WriteBackPoolStats.CanGrowPool())
			{
				this._memoryManager.GrowWriteBackItemPool();
			}
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x00052758 File Offset: 0x00050958
		private void UpdatePoolThrottleState()
		{
			if (!this._internalMemoryPressure)
			{
				if (this.MemoryAllocatorNeedThrottling() || this.CacheItemPoolNeedThrottling() || this.DirectoryPoolNeedThrottling() || this.WriteBackItemPoolNeedThrottling())
				{
					this._internalMemoryPressure = true;
					this.StartPoolThrottle();
					this.CallLoadSheddingForPoolMemoryPressureCallback();
				}
				return;
			}
			if (this.MemoryAllocatorOutofThrottling() && this.CacheItemPoolOutOfThrottling() && this.DirectoryPoolOutOfThrottling() && this.WriteBackItemPoolOutOfThrottling())
			{
				this._internalMemoryPressure = false;
				this.StopPoolThrottle();
				return;
			}
			this.CallLoadSheddingForPoolMemoryPressureCallback();
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x000527D5 File Offset: 0x000509D5
		private void StartPoolThrottle()
		{
			if (!this._externalMemoryPressure)
			{
				this.EnterThrottledState();
			}
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x000527E5 File Offset: 0x000509E5
		private void StopPoolThrottle()
		{
			if (!this._externalMemoryPressure)
			{
				this.ExitThrottledState();
			}
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x000527F5 File Offset: 0x000509F5
		private void StartExternalThrottle()
		{
			if (!this._internalMemoryPressure)
			{
				this.EnterThrottledState();
			}
			if (this._startConnectionThrottle != null)
			{
				this._startConnectionThrottle();
			}
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x00052818 File Offset: 0x00050A18
		private void StopExternalThrottle()
		{
			if (this._stopConnectionThrottle != null)
			{
				this._stopConnectionThrottle();
			}
			if (!this._internalMemoryPressure)
			{
				this.ExitThrottledState();
			}
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0005283C File Offset: 0x00050A3C
		private void EnterThrottledState()
		{
			if (Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning("DistributedCache.MemoryPressureMonitor", "Starting throttle, Memory Pressure: external: {0}, internal: {1}", new object[] { this._externalMemoryPressure, this._internalMemoryPressure });
			}
			string text = (this._internalMemoryPressure ? GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "PoolExhausted") : GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "AvailableSystemMemoryLow"));
			EventLogProvider.EventWritePoolThrottleStarted("DistributedCache.MemoryPressureMonitor", text, (int)Math.Round(this._memoryManagerStats.CacheItemPoolStats.AvailablePercentageInPool), (int)Math.Round(this._memoryManagerStats.DirectoryPoolStats.AvailablePercentageInPool), (int)Math.Round(this._memoryManagerStats.WriteBackPoolStats.AvailablePercentageInPool), (int)Math.Round(this._memoryManagerStats.AvailableMemoryPercentage), (int)Math.Round(this._availablePhysicalMemoryPercent), (int)Math.Round(this.CacheItemInUsePercentage), (int)Math.Round(this.WriteBackItemInUsePercentage), (int)Math.Round(this.PoolMemoryInUsePercentage), this._memoryManagerStats.CacheItemPoolStats.MaxCapacity, this._memoryManagerStats.DirectoryPoolStats.MaxCapacity, this._memoryManagerStats.WriteBackPoolStats.MaxCapacity, this._memoryManagerStats.MaxMemoryCapacity, (long)this._systemMemory);
			this._throttlingStartTime = DateTime.UtcNow;
			this._lastLogTimeForPoolThrottling = DateTime.UtcNow;
			this._startWriteThrottle();
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x000529A0 File Offset: 0x00050BA0
		private void ExitThrottledState()
		{
			this._stopWriteThrottle();
			long num = (long)(DateTime.UtcNow - this._throttlingStartTime).TotalMilliseconds;
			if (Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning("DistributedCache.MemoryPressureMonitor", "Throttling Stopped", new object[0]);
			}
			EventLogProvider.EventWritePoolThrottleStopped("DistributedCache.MemoryPressureMonitor", (int)Math.Round(this._memoryManagerStats.CacheItemPoolStats.AvailablePercentageInPool), (int)Math.Round(this._memoryManagerStats.DirectoryPoolStats.AvailablePercentageInPool), (int)Math.Round(this._memoryManagerStats.WriteBackPoolStats.AvailablePercentageInPool), (int)Math.Round(this._memoryManagerStats.AvailableMemoryPercentage), (int)Math.Round(this._availablePhysicalMemoryPercent), (int)Math.Round(this.CacheItemInUsePercentage), (int)Math.Round(this.WriteBackItemInUsePercentage), (int)Math.Round(this.PoolMemoryInUsePercentage), this._memoryManagerStats.CacheItemPoolStats.MaxCapacity, this._memoryManagerStats.DirectoryPoolStats.MaxCapacity, this._memoryManagerStats.WriteBackPoolStats.MaxCapacity, this._memoryManagerStats.MaxMemoryCapacity, (long)this._systemMemory, num);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x00052ABF File Offset: 0x00050CBF
		private void CleanCacheMemory()
		{
			if (this._periodicEvictionCallback() > 0L)
			{
				this.UpdateMemoryStats();
				this.ManageAllocatorMemoryAndObjectPoolActual();
			}
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00052ADC File Offset: 0x00050CDC
		private void CheckExternalMemoryPressure()
		{
			this.UpdateSytemMemoryStats();
			if (this._isSystemMemoryPressureEnabled)
			{
				if (this._externalMemoryPressure)
				{
					if (this._availablePhysicalMemoryPercent > (double)this._throttleHighPercentExternal)
					{
						this._externalMemoryPressure = false;
						this.StopExternalThrottle();
						return;
					}
					this.CallLoadSheddingForExternalPressureCallback();
					return;
				}
				else if (this._availablePhysicalMemoryPercent <= (double)this._throttleLowPercentExternal)
				{
					this.RunBlockingGCAndUpdateStats();
					this.UpdateSytemMemoryStats();
					if (this._availablePhysicalMemoryPercent <= (double)this._throttleLowPercentExternal)
					{
						this._externalMemoryPressure = true;
						this.StartExternalThrottle();
						this.CallLoadSheddingForExternalPressureCallback();
					}
				}
			}
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00052B60 File Offset: 0x00050D60
		private void CheckForGCPauses()
		{
			if (Provider.IsEnabled(TraceLevel.Error) && this._lastMemoryMonitoringCycleTime != default(DateTime))
			{
				TimeSpan timeSpan = DateTime.UtcNow - this._lastMemoryMonitoringCycleTime - TimeSpan.FromMilliseconds((double)this._interval);
				if (timeSpan.Ticks >= (long)this._gcPauseThreshold * 10000000L)
				{
					EventLogWriter.WriteError("DistributedCache.MemoryPressureMonitor", "Memory pressure monitor took {0} more than expected per cycle. Threshold: {1} seconds", new object[] { timeSpan, this._gcPauseThreshold });
				}
			}
			this._lastMemoryMonitoringCycleTime = DateTime.UtcNow;
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00052C00 File Offset: 0x00050E00
		private bool IsProcessMemoryMonitoringEnabled()
		{
			ulong num = (ulong)(this._memoryManagerStats.MaxMemoryCapacity * 100L / 50L);
			return this._isSystemMemoryPressureEnabled && this._memoryManagerStats.MaxMemoryCapacity >= 1937768448L && num < this._systemMemory * (ulong)(100L - (long)Math.Max(this._throttleLowPercentExternal, 1)) / 100UL;
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x00052C60 File Offset: 0x00050E60
		private static void SetProcessMinWorkingSetSize(ulong workingSetSize)
		{
			bool flag = Provider.IsEnabled(TraceLevel.Error);
			if (!NativeMethods.SetProcessWorkingSetSizeEx(Process.GetCurrentProcess().Handle, new IntPtr((long)workingSetSize), BufferMemoryPressureMonitor.WorkingSetSizeDefault, QuotaLimitsHardWS.QUOTA_LIMITS_HARDWS_MIN_ENABLE) && flag)
			{
				EventLogWriter.WriteError("DistributedCache.MemoryPressureMonitor", "Could not set min working set size to {0}. GetLastError: {1}.", new object[]
				{
					workingSetSize,
					Marshal.GetLastWin32Error()
				});
			}
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x00052CC4 File Offset: 0x00050EC4
		private void UpdateSytemMemoryStats()
		{
			ulong num;
			MemoryStatus.GetStatus(out this._systemMemory, out num);
			this._availablePhysicalMemoryPercent = num / (double)this._systemMemory * 100.0;
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x00052CFC File Offset: 0x00050EFC
		private void CallLoadSheddingForPoolMemoryPressureCallback()
		{
			double num = Math.Max((double)this._throttleLowPercentInternal, (double)this._throttleHighPercentInternal - this.GetLowerAvailableMemoryObjectPercentage());
			this._onLoadSheddingCallback(new BasicMemoryReleaseTarget((long)Math.Ceiling((double)this._memoryManagerStats.MaxMemoryCapacity * num / 100.0)));
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x00052D52 File Offset: 0x00050F52
		private void CallLoadSheddingForExternalPressureCallback()
		{
			this._onLoadSheddingCallback(this._externalMemoryReleaseTargetInstance);
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x00052D68 File Offset: 0x00050F68
		private void MonitorThread(object obj)
		{
			while (!this._canceled)
			{
				this.UpdateMemoryStats();
				this.ManageAllocatorMemoryAndObjectPool();
				this.UpdatePoolThrottleState();
				this.CleanCacheMemory();
				this.CheckExternalMemoryPressure();
				if ((this._externalMemoryPressure || this._internalMemoryPressure) && (DateTime.UtcNow - this._lastLogTimeForPoolThrottling).TotalSeconds > 60.0)
				{
					EventLogProvider.EventWriteInPoolThrottling("DistributedCache.MemoryPressureMonitor", (int)Math.Round(this._memoryManagerStats.CacheItemPoolStats.AvailablePercentageInPool), (int)Math.Round(this._memoryManagerStats.DirectoryPoolStats.AvailablePercentageInPool), (int)Math.Round(this._memoryManagerStats.WriteBackPoolStats.AvailablePercentageInPool), (int)Math.Round(this._memoryManagerStats.AvailableMemoryPercentage), (int)Math.Round(this._availablePhysicalMemoryPercent), (int)Math.Round(this.CacheItemInUsePercentage), (int)Math.Round(this.WriteBackItemInUsePercentage), (int)Math.Round(this.PoolMemoryInUsePercentage), this._memoryManagerStats.CacheItemPoolStats.MaxCapacity, this._memoryManagerStats.DirectoryPoolStats.MaxCapacity, this._memoryManagerStats.WriteBackPoolStats.MaxCapacity, this._memoryManagerStats.MaxMemoryCapacity, (long)this._systemMemory, (long)(DateTime.UtcNow - this._throttlingStartTime).TotalMilliseconds);
					this._lastLogTimeForPoolThrottling = DateTime.UtcNow;
				}
				this.CheckForGCPauses();
				Thread.Sleep(this._interval);
			}
			this._internalMemoryPressure = false;
			this._externalMemoryPressure = false;
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x00052EF0 File Offset: 0x000510F0
		private double MemoryDiffPercentageInPoolAndHostSize
		{
			get
			{
				return (double)(this._memoryManagerStats.CurrentMemoryUsage - this._currentHostSize) / (double)this._memoryManagerStats.MaxMemoryCapacity * 100.0;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x00052F1C File Offset: 0x0005111C
		private double CacheItemDiffPercentageInPoolAndHostCount
		{
			get
			{
				return (double)(this._memoryManagerStats.CacheItemPoolStats.UsedObjects - this._currentHostObjectCount) / (double)this._memoryManagerStats.CacheItemPoolStats.CurrentCapacity * 100.0;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x00052F52 File Offset: 0x00051152
		private double WriteBackItemDiffPercentageInPoolAndHostCount
		{
			get
			{
				return (double)(this._memoryManagerStats.WriteBackPoolStats.UsedObjects - this._currentHostWBItemCount) / (double)this._memoryManagerStats.WriteBackPoolStats.CurrentCapacity * 100.0;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x00052F88 File Offset: 0x00051188
		private double CacheItemInUsePercentage
		{
			get
			{
				return (double)this._currentHostObjectCount / (double)this._memoryManagerStats.CacheItemPoolStats.MaxCapacity * 100.0;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x00052FAD File Offset: 0x000511AD
		private double WriteBackItemInUsePercentage
		{
			get
			{
				return (double)this._currentHostWBItemCount / (double)this._memoryManagerStats.WriteBackPoolStats.MaxCapacity * 100.0;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x00052FD2 File Offset: 0x000511D2
		private double PoolMemoryInUsePercentage
		{
			get
			{
				return (double)this._currentHostSize / (double)this._memoryManagerStats.MaxMemoryCapacity * 100.0;
			}
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x00052FF4 File Offset: 0x000511F4
		private bool DirectoryPoolNeedThrottling()
		{
			return this._memoryManagerStats.DirectoryPoolStats.CurrentCapacity == this._memoryManagerStats.DirectoryPoolStats.MaxCapacity && this._memoryManagerStats.DirectoryPoolStats.AvailableCurrentPercentageInPool <= (double)this._throttleLowPercentInternal;
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x00053044 File Offset: 0x00051244
		private bool CacheItemPoolNeedThrottling()
		{
			return this._memoryManagerStats.CacheItemPoolStats.CurrentCapacity == this._memoryManagerStats.CacheItemPoolStats.MaxCapacity && this._memoryManagerStats.CacheItemPoolStats.AvailableCurrentPercentageInPool <= (double)this._throttleLowPercentInternal;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00053091 File Offset: 0x00051291
		private bool MemoryAllocatorNeedThrottling()
		{
			return this._memoryManagerStats.AvailableMemoryPercentage <= (double)this._throttleLowPercentInternal;
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x000530AC File Offset: 0x000512AC
		private bool WriteBackItemPoolNeedThrottling()
		{
			return this._memoryManagerStats.WriteBackPoolStats.CurrentCapacity == this._memoryManagerStats.WriteBackPoolStats.MaxCapacity && this._memoryManagerStats.WriteBackPoolStats.AvailableCurrentPercentageInPool <= (double)this._throttleLowPercentInternal;
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x000530F9 File Offset: 0x000512F9
		private bool DirectoryPoolOutOfThrottling()
		{
			return this._memoryManagerStats.DirectoryPoolStats.CurrentCapacity < this._memoryManagerStats.DirectoryPoolStats.MaxCapacity || this._memoryManagerStats.DirectoryPoolStats.AvailableCurrentPercentageInPool > (double)this._throttleHighPercentInternal;
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x00053138 File Offset: 0x00051338
		private bool CacheItemPoolOutOfThrottling()
		{
			return this._memoryManagerStats.CacheItemPoolStats.CurrentCapacity < this._memoryManagerStats.CacheItemPoolStats.MaxCapacity || this._memoryManagerStats.CacheItemPoolStats.AvailableCurrentPercentageInPool > (double)this._throttleHighPercentInternal;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x00053177 File Offset: 0x00051377
		private bool WriteBackItemPoolOutOfThrottling()
		{
			return this._memoryManagerStats.WriteBackPoolStats.CurrentCapacity < this._memoryManagerStats.WriteBackPoolStats.MaxCapacity || this._memoryManagerStats.WriteBackPoolStats.AvailableCurrentPercentageInPool > (double)this._throttleHighPercentInternal;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x000531B6 File Offset: 0x000513B6
		private bool MemoryAllocatorOutofThrottling()
		{
			return this._memoryManagerStats.AvailableMemoryPercentage > (double)this._throttleHighPercentInternal;
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x000531CC File Offset: 0x000513CC
		private void RunBlockingGCForExternalMemoryPressure()
		{
			DateTime lastGCRunThrottling = this._lastGCRunThrottling;
			if (lastGCRunThrottling.AddSeconds((double)this._throttleGCInterval).CompareTo(DateTime.UtcNow) < 0)
			{
				BufferMemoryPressureMonitor.RunBlockingGCWithoutUpdatingStats();
				this._lastMemoryManagementRunTime = DateTime.UtcNow;
				this._lastGCRunThrottling = DateTime.UtcNow;
			}
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0005321C File Offset: 0x0005141C
		private static void RunBlockingGCWithoutUpdatingStats()
		{
			DateTime utcNow = DateTime.UtcNow;
			BufferMemoryPressureMonitor.BlockingGC();
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.MemoryPressureMonitor", "Time taken for blocking GC: {0}", new object[] { DateTime.UtcNow - utcNow });
			}
			BufferMemoryPressureMonitor.WaitForPendingFinalizer();
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0005326B File Offset: 0x0005146B
		private void RunBlockingGCAndUpdateStats()
		{
			BufferMemoryPressureMonitor.RunBlockingGCWithoutUpdatingStats();
			this.UpdateMemoryStats();
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00053278 File Offset: 0x00051478
		private static void BlockingGC()
		{
			GC.Collect();
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00053280 File Offset: 0x00051480
		private double GetLowerAvailableMemoryObjectPercentage()
		{
			double num = this._memoryManagerStats.AvailableMemoryPercentage;
			if (this._memoryManagerStats.CacheItemPoolStats.AvailableCurrentPercentageInPool < num)
			{
				num = this._memoryManagerStats.CacheItemPoolStats.AvailableCurrentPercentageInPool;
			}
			if (this._memoryManagerStats.WriteBackPoolStats.AvailableCurrentPercentageInPool < num)
			{
				num = this._memoryManagerStats.WriteBackPoolStats.AvailableCurrentPercentageInPool;
			}
			if (this._memoryManagerStats.DirectoryPoolStats.AvailableCurrentPercentageInPool < num)
			{
				num = this._memoryManagerStats.DirectoryPoolStats.AvailableCurrentPercentageInPool;
			}
			return num;
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00053308 File Offset: 0x00051508
		private void UpdateMemoryStats()
		{
			this._memoryManagerStats = this._memoryManager.GetStats() as BufferMemoryManagerStats;
			this._currentHostObjectCount = this._totalObjectCountCallBack();
			this._currentHostWBItemCount = this._totalWBItemCountCallback();
			this._currentHostSize = this._totalMemoryUsageCallback();
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.MemoryPressureMonitor", "UpdateMemoryStats finished. Memory Manager: [{0}], ObjectCount: {1}, HostSize: {2}, AvailablePhysicalMemory: {3:F}%", new object[]
				{
					this._memoryManagerStats.GetPoolStatString(true),
					this._currentHostObjectCount,
					this._currentHostSize,
					this._availablePhysicalMemoryPercent
				});
			}
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x000533B8 File Offset: 0x000515B8
		private bool CanRunMemoryAndPoolManagement()
		{
			return this._lastMemoryManagementRunTime.AddSeconds(15.0).CompareTo(DateTime.UtcNow) < 0 || this.InCriticalMemoryPressureState || this._memoryManagerStats.AvailableMemoryPercentage < (double)this._lowMemoryPercent || this._memoryManagerStats.CacheItemPoolStats.AvailableCurrentPercentageInPool < (double)this._lowMemoryPercent || this._memoryManagerStats.WriteBackPoolStats.AvailableCurrentPercentageInPool < (double)this._lowMemoryPercent || this._memoryManagerStats.DirectoryPoolStats.AvailableCurrentPercentageInPool < (double)this._lowMemoryPercent;
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00053452 File Offset: 0x00051652
		private void RunNonBlockingGCAndWaitForFinalizer()
		{
			BufferMemoryPressureMonitor.RunNonBlockingGC();
			BufferMemoryPressureMonitor.WaitForPendingFinalizer();
			this.UpdateMemoryStats();
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x00053464 File Offset: 0x00051664
		private static void WaitForPendingFinalizer()
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.MemoryPressureMonitor", "Starting waitforpending finalizers", new object[0]);
			}
			GC.WaitForPendingFinalizers();
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.MemoryPressureMonitor", "Ending waitforpending finalizers", new object[0]);
			}
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000534B0 File Offset: 0x000516B0
		private static void RunNonBlockingGC()
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.MemoryPressureMonitor", "NonBlockingGC start.", new object[0]);
			}
			GC.Collect();
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.MemoryPressureMonitor", "NonBlockingGC finished.", new object[0]);
			}
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x000534FC File Offset: 0x000516FC
		public void Cancel()
		{
			this._canceled = true;
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x00053507 File Offset: 0x00051707
		public bool InCriticalMemoryPressureState
		{
			get
			{
				return this._externalMemoryPressure || this._internalMemoryPressure;
			}
		}

		// Token: 0x04000E69 RID: 3689
		private const long _minPoolSizeForProcessMemoryThrottling = 1937768448L;

		// Token: 0x04000E6A RID: 3690
		private const int SecondsBetweenEvents = 60;

		// Token: 0x04000E6B RID: 3691
		private const string _logSource = "DistributedCache.MemoryPressureMonitor";

		// Token: 0x04000E6C RID: 3692
		private static readonly IntPtr WorkingSetSizeDefault = new IntPtr(-1);

		// Token: 0x04000E6D RID: 3693
		private MemoryUsageDelegate _totalMemoryUsageCallback;

		// Token: 0x04000E6E RID: 3694
		private ObjectCountDelegate _totalObjectCountCallBack;

		// Token: 0x04000E6F RID: 3695
		private ObjectCountDelegate _totalWBItemCountCallback;

		// Token: 0x04000E70 RID: 3696
		private OnMemoryPressureCallBackEx _onLoadSheddingCallback;

		// Token: 0x04000E71 RID: 3697
		private RunCompactionDelegate _runCompactionCallBack;

		// Token: 0x04000E72 RID: 3698
		private PeriodicEvictionCheck _periodicEvictionCallback;

		// Token: 0x04000E73 RID: 3699
		private ThrottleDelegate _startWriteThrottle;

		// Token: 0x04000E74 RID: 3700
		private ThrottleDelegate _stopWriteThrottle;

		// Token: 0x04000E75 RID: 3701
		private ThrottleDelegate _startConnectionThrottle;

		// Token: 0x04000E76 RID: 3702
		private ThrottleDelegate _stopConnectionThrottle;

		// Token: 0x04000E77 RID: 3703
		private volatile bool _canceled;

		// Token: 0x04000E78 RID: 3704
		private double _availablePhysicalMemoryPercent;

		// Token: 0x04000E79 RID: 3705
		private ulong _systemMemory;

		// Token: 0x04000E7A RID: 3706
		private DateTime _throttlingStartTime;

		// Token: 0x04000E7B RID: 3707
		private DateTime _lastLogTimeForPoolThrottling;

		// Token: 0x04000E7C RID: 3708
		private DateTime _lastMemoryMonitoringCycleTime;

		// Token: 0x04000E7D RID: 3709
		private readonly int _interval;

		// Token: 0x04000E7E RID: 3710
		private int _gcPauseThreshold;

		// Token: 0x04000E7F RID: 3711
		private readonly int _lowMemoryPercent;

		// Token: 0x04000E80 RID: 3712
		private readonly int _throttleLowPercentExternal;

		// Token: 0x04000E81 RID: 3713
		private readonly int _throttleHighPercentExternal;

		// Token: 0x04000E82 RID: 3714
		private readonly int _throttleLowPercentInternal;

		// Token: 0x04000E83 RID: 3715
		private readonly int _throttleHighPercentInternal;

		// Token: 0x04000E84 RID: 3716
		private readonly int _percentItemInFinalizerQueue;

		// Token: 0x04000E85 RID: 3717
		private readonly int _throttleGCInterval;

		// Token: 0x04000E86 RID: 3718
		private readonly BufferMemoryManager _memoryManager;

		// Token: 0x04000E87 RID: 3719
		private bool _externalMemoryPressure;

		// Token: 0x04000E88 RID: 3720
		private bool _internalMemoryPressure;

		// Token: 0x04000E89 RID: 3721
		private long _currentHostSize;

		// Token: 0x04000E8A RID: 3722
		private long _currentHostObjectCount;

		// Token: 0x04000E8B RID: 3723
		private long _currentHostWBItemCount;

		// Token: 0x04000E8C RID: 3724
		private BufferMemoryManagerStats _memoryManagerStats;

		// Token: 0x04000E8D RID: 3725
		private DateTime _lastMemoryManagementRunTime;

		// Token: 0x04000E8E RID: 3726
		private DateTime _lastGCRunThrottling;

		// Token: 0x04000E8F RID: 3727
		private bool _isSystemMemoryPressureEnabled;

		// Token: 0x04000E90 RID: 3728
		private readonly BufferMemoryPressureMonitor.ExternalMemoryReleaseTarget _externalMemoryReleaseTargetInstance;

		// Token: 0x020002E2 RID: 738
		private class ExternalMemoryReleaseTarget : IMemoryReleaseTarget
		{
			// Token: 0x06001B5D RID: 7005 RVA: 0x00053526 File Offset: 0x00051726
			public ExternalMemoryReleaseTarget(BufferMemoryPressureMonitor monitor)
			{
				this._monitor = monitor;
			}

			// Token: 0x06001B5E RID: 7006 RVA: 0x000036A9 File Offset: 0x000018A9
			public void PoolMemoryReleased(long internalReleasedMemory)
			{
			}

			// Token: 0x06001B5F RID: 7007 RVA: 0x00053535 File Offset: 0x00051735
			public bool UpdateStatsAndCheckIfTargetMet()
			{
				this._monitor.RunBlockingGCForExternalMemoryPressure();
				this._monitor.UpdateSytemMemoryStats();
				return this._monitor._availablePhysicalMemoryPercent > (double)this._monitor._throttleHighPercentExternal;
			}

			// Token: 0x04000E91 RID: 3729
			private readonly BufferMemoryPressureMonitor _monitor;
		}
	}
}
