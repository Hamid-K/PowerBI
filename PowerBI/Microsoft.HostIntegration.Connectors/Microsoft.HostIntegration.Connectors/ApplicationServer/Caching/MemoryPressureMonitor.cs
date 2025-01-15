using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200033D RID: 829
	internal class MemoryPressureMonitor : IMonitor
	{
		// Token: 0x06001DB9 RID: 7609 RVA: 0x000594B4 File Offset: 0x000576B4
		public MemoryPressureMonitor(MemoryUsageDelegate totalMemoryUsageCallback, OnMemoryPressureCallBack memPressureCallback, OnMemoryPressureCallBackEx loadSheddingCallback, ServiceConfigurationManager configurationManager, PeriodicEvictionCheck periodicEvictionCheck, ThrottleDelegate startThrottle, ThrottleDelegate stopThrottle)
		{
			this._configurationManager = configurationManager;
			this._totalMemoryUsageCallback = totalMemoryUsageCallback;
			this._onMemoryPressureCallback = memPressureCallback;
			this._onLoadSheddingCallback = loadSheddingCallback;
			this._periodicEvictionCallback = periodicEvictionCheck;
			this._startThrottle = startThrottle;
			this._stopThrottle = stopThrottle;
			if (this._configurationManager.AdvancedProperties.MemoryPressureMonitorProperties.IsEnabled)
			{
				ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(this.MonitorThread), null);
				return;
			}
			ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(this.PeriodicEvictionThread), null);
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00059545 File Offset: 0x00057745
		public void Cancel()
		{
			this._canceled = true;
			this._inCriticalMemoryPressureState = false;
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x00059559 File Offset: 0x00057759
		public bool InCriticalMemoryPressureState
		{
			get
			{
				return this._inCriticalMemoryPressureState;
			}
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x00059564 File Offset: 0x00057764
		private void MonitorThread(object obj)
		{
			while (!this._canceled)
			{
				MemoryPressureMonitorProperties memoryPressureMonitorProperties = this._configurationManager.AdvancedProperties.MemoryPressureMonitorProperties;
				this.UpdateMemoryState();
				this.UpdateThrottleState();
				this.CleanCacheMemory();
				Thread.Sleep(memoryPressureMonitorProperties.Interval);
			}
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x000595AC File Offset: 0x000577AC
		private void UpdateMemoryState()
		{
			ulong num;
			ulong num2;
			MemoryStatus.GetStatus(out num, out num2);
			Process currentProcess = Process.GetCurrentProcess();
			if (IntPtr.Size == 4 && num > 1395864371UL)
			{
				num = Math.Max(1395864371UL, (ulong)currentProcess.PrivateMemorySize64);
				ulong num3 = num - (ulong)currentProcess.PrivateMemorySize64;
				num2 = Math.Min(num3, num2);
			}
			this._physicalMemory = (long)num;
			this._availableMemoryPercent = num2 * 100UL / num;
			this._privateBytesPercent = (double)(currentProcess.PrivateMemorySize64 * 100L) / num;
			this._workingSetPercent = (double)(currentProcess.WorkingSet64 * 100L) / num;
			long num4 = this._totalMemoryUsageCallback();
			this._currentCacheSizePercent = (double)(num4 * 100L) / num;
			int num5 = GC.CollectionCount(2);
			if (num5 > this._gencount)
			{
				this._gencount = num5;
				this._releasedMemoryPercent = 0.0;
				this._baseCacheSizePercent = this._currentCacheSizePercent;
				this._throttledGCTime = DateTime.UtcNow;
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<double, double, double, double, int, double>(this.logSource, "Private bytes percent {0}, Working set percent {1} Cache size percent {2} Available memory percent {3} Gen2 count {4} Release memory percent {5}", this._privateBytesPercent, this._workingSetPercent, this._currentCacheSizePercent, this._availableMemoryPercent, this._gencount, this._releasedMemoryPercent);
			}
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x000596D8 File Offset: 0x000578D8
		private void UpdateThrottleState()
		{
			MemoryPressureMonitorProperties memoryPressureMonitorProperties = this._configurationManager.AdvancedProperties.MemoryPressureMonitorProperties;
			if (!this._inCriticalMemoryPressureState)
			{
				if (memoryPressureMonitorProperties.ThrottleLowPercent > 0 && (this._availableMemoryPercent < (double)memoryPressureMonitorProperties.ThrottleLowPercent || this._availableMemoryPercent + this._workingSetPercent < this._privateBytesPercent + (double)memoryPressureMonitorProperties.ThrottleLowPercent))
				{
					GC.Collect(2);
					this.UpdateMemoryState();
					if (this._availableMemoryPercent < (double)memoryPressureMonitorProperties.ThrottleLowPercent || this._availableMemoryPercent + this._workingSetPercent < this._privateBytesPercent + (double)memoryPressureMonitorProperties.ThrottleLowPercent)
					{
						this._inCriticalMemoryPressureState = true;
						this._baseCacheSizePercent = (double)((int)(this._totalMemoryUsageCallback() * 100L / this._physicalMemory));
						this._startThrottle();
						EventLogProvider.EventWriteThrottleStarted(this.logSource, (int)this._privateBytesPercent, (int)this._workingSetPercent, (int)this._currentCacheSizePercent, (int)this._availableMemoryPercent, this._gencount, (int)this._releasedMemoryPercent);
						this._throttledGCTime = DateTime.UtcNow;
						this._throttledEventTime = DateTime.UtcNow;
						this.CallLoadSheddingCallback();
					}
				}
				return;
			}
			double num = this._baseCacheSizePercent - this._currentCacheSizePercent;
			if (this._releasedMemoryPercent > (double)memoryPressureMonitorProperties.ThrottleHighPercent || num > (double)memoryPressureMonitorProperties.ThrottleHighPercent || DateTime.UtcNow.Subtract(this._throttledGCTime).TotalSeconds > (double)memoryPressureMonitorProperties.ThrottleGCInterval)
			{
				GC.Collect(2);
				this.UpdateMemoryState();
			}
			if (this._availableMemoryPercent > (double)memoryPressureMonitorProperties.ThrottleHighPercent && this._availableMemoryPercent + this._workingSetPercent > (double)memoryPressureMonitorProperties.ThrottleHighPercent + this._privateBytesPercent)
			{
				this._inCriticalMemoryPressureState = false;
				this._stopThrottle();
				EventLogProvider.EventWriteThrottleStopped(this.logSource, (int)this._privateBytesPercent, (int)this._workingSetPercent, (int)this._currentCacheSizePercent, (int)this._availableMemoryPercent, this._gencount, (int)this._releasedMemoryPercent);
				return;
			}
			if (DateTime.UtcNow.Subtract(this._throttledEventTime).TotalSeconds > 60.0)
			{
				EventLogProvider.EventWriteInThrottledState(this.logSource, (int)this._privateBytesPercent, (int)this._workingSetPercent, (int)this._currentCacheSizePercent, (int)this._availableMemoryPercent, this._gencount, (int)this._releasedMemoryPercent);
				this._throttledEventTime = DateTime.UtcNow;
			}
			this.CallLoadSheddingCallback();
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x00059938 File Offset: 0x00057B38
		private void CallLoadSheddingCallback()
		{
			MemoryPressureMonitorProperties memoryPressureMonitorProperties = this._configurationManager.AdvancedProperties.MemoryPressureMonitorProperties;
			double num = Math.Max((double)memoryPressureMonitorProperties.ThrottleLowPercent, (double)memoryPressureMonitorProperties.ThrottleHighPercent - this._availableMemoryPercent);
			this._onLoadSheddingCallback(new BasicMemoryReleaseTarget((long)((double)this._physicalMemory * num / 100.0)));
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x00059998 File Offset: 0x00057B98
		private void CleanCacheMemory()
		{
			MemoryPressureMonitorProperties memoryPressureMonitorProperties = this._configurationManager.AdvancedProperties.MemoryPressureMonitorProperties;
			IHostConfiguration nodeProperties = this._configurationManager.NodeProperties;
			long num = (nodeProperties.Size << 20) * nodeProperties.LowWaterMarkPercentage / this._physicalMemory;
			long num3;
			if (this._availableMemoryPercent + (double)((int)this._releasedMemoryPercent) <= (double)memoryPressureMonitorProperties.LowMemoryPercent)
			{
				double num2 = (double)memoryPressureMonitorProperties.LowMemoryPercent - (this._availableMemoryPercent + (double)((int)this._releasedMemoryPercent));
				num2 = (double)memoryPressureMonitorProperties.LowMemoryReleasePercent * ((num2 + (double)memoryPressureMonitorProperties.LowMemoryReleasePercent) / (double)memoryPressureMonitorProperties.LowMemoryReleasePercent);
				num3 = this._onMemoryPressureCallback((long)((double)this._physicalMemory * num2 / 100.0));
			}
			else
			{
				num3 = this._periodicEvictionCallback();
			}
			this._releasedMemoryPercent += (double)(num3 * 100L) / (double)this._physicalMemory;
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this.logSource, "Memory released percent {0}", new object[] { this._releasedMemoryPercent });
			}
			if ((this._availableMemoryPercent + this._releasedMemoryPercent < (double)(memoryPressureMonitorProperties.LowMemoryPercent + memoryPressureMonitorProperties.LowMemoryReleasePercent) || this._currentCacheSizePercent > (double)num) && !this._inCriticalMemoryPressureState && DateTime.UtcNow.Subtract(this._lowMemoryEventTime).TotalSeconds > 60.0)
			{
				EventLogProvider.EventWriteAvailableMemoryLow(this.logSource, (int)this._privateBytesPercent, (int)this._workingSetPercent, (int)this._currentCacheSizePercent, (int)this._availableMemoryPercent, this._gencount, (int)this._releasedMemoryPercent);
				this._lowMemoryEventTime = DateTime.UtcNow;
			}
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x00059B3C File Offset: 0x00057D3C
		private void PeriodicEvictionThread(object obj)
		{
			while (!this._canceled)
			{
				MemoryPressureMonitorProperties memoryPressureMonitorProperties = this._configurationManager.AdvancedProperties.MemoryPressureMonitorProperties;
				Thread.Sleep(memoryPressureMonitorProperties.Interval);
				this._periodicEvictionCallback();
			}
		}

		// Token: 0x04001068 RID: 4200
		private const int SecondsBetweenEvents = 60;

		// Token: 0x04001069 RID: 4201
		private MemoryUsageDelegate _totalMemoryUsageCallback;

		// Token: 0x0400106A RID: 4202
		private OnMemoryPressureCallBack _onMemoryPressureCallback;

		// Token: 0x0400106B RID: 4203
		private OnMemoryPressureCallBackEx _onLoadSheddingCallback;

		// Token: 0x0400106C RID: 4204
		private PeriodicEvictionCheck _periodicEvictionCallback;

		// Token: 0x0400106D RID: 4205
		private ThrottleDelegate _startThrottle;

		// Token: 0x0400106E RID: 4206
		private ThrottleDelegate _stopThrottle;

		// Token: 0x0400106F RID: 4207
		private volatile bool _canceled;

		// Token: 0x04001070 RID: 4208
		private string logSource = "DistributedCache.MemoryPressureMonitor";

		// Token: 0x04001071 RID: 4209
		private long _physicalMemory;

		// Token: 0x04001072 RID: 4210
		private double _availableMemoryPercent;

		// Token: 0x04001073 RID: 4211
		private double _privateBytesPercent;

		// Token: 0x04001074 RID: 4212
		private double _workingSetPercent;

		// Token: 0x04001075 RID: 4213
		private double _releasedMemoryPercent;

		// Token: 0x04001076 RID: 4214
		private volatile bool _inCriticalMemoryPressureState;

		// Token: 0x04001077 RID: 4215
		private double _baseCacheSizePercent;

		// Token: 0x04001078 RID: 4216
		private double _currentCacheSizePercent;

		// Token: 0x04001079 RID: 4217
		private int _gencount;

		// Token: 0x0400107A RID: 4218
		private DateTime _throttledEventTime;

		// Token: 0x0400107B RID: 4219
		private DateTime _throttledGCTime;

		// Token: 0x0400107C RID: 4220
		private DateTime _lowMemoryEventTime;

		// Token: 0x0400107D RID: 4221
		private ServiceConfigurationManager _configurationManager;
	}
}
