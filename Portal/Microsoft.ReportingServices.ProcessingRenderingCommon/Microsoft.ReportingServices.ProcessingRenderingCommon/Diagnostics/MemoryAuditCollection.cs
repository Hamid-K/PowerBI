using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200009D RID: 157
	public sealed class MemoryAuditCollection
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x0000EC86 File Offset: 0x0000CE86
		private MemoryAuditCollection()
		{
			this.m_intervalTimer.Start();
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000ECB7 File Offset: 0x0000CEB7
		private static MemoryAuditCollection GetMAPGroup(MemoryGroupType groupType)
		{
			return MemoryAuditCollection.m_current;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000ECBE File Offset: 0x0000CEBE
		private static MemoryAuditCollection GetMAPGroup()
		{
			return MemoryAuditCollection.m_current;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		public static void Register(MemoryAuditProxy map)
		{
			MemoryAuditCollection mapgroup = MemoryAuditCollection.GetMAPGroup();
			WeakReference weakReference = new WeakReference(map);
			map.CollectionToken = weakReference;
			Dictionary<WeakReference, WeakReference> maps = mapgroup.m_maps;
			lock (maps)
			{
				mapgroup.m_maps.Add(weakReference, weakReference);
				MemoryAuditCollection.SweepReleasedMapsIfNecessary(mapgroup);
			}
			mapgroup.IncrementVersion();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000ED30 File Offset: 0x0000CF30
		public static void UnRegister(MemoryAuditProxy map)
		{
			MemoryAuditCollection mapgroup = MemoryAuditCollection.GetMAPGroup();
			WeakReference weakReference = map.CollectionToken as WeakReference;
			if (weakReference != null)
			{
				Dictionary<WeakReference, WeakReference> maps = mapgroup.m_maps;
				lock (maps)
				{
					mapgroup.m_maps.Remove(weakReference);
				}
			}
			mapgroup.IncrementVersion();
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000ED94 File Offset: 0x0000CF94
		public static bool InitiateMemoryShrink(long kBytes, bool notifyAllConsumers, MemoryGroupType groupType, out long estimatedKBytesFreed, out long totalAuditedMemoryKb)
		{
			bool flag = false;
			estimatedKBytesFreed = 0L;
			totalAuditedMemoryKb = 0L;
			MemoryAuditCollection current = MemoryAuditCollection.m_current;
			bool flag3;
			try
			{
				if (current.m_timeFinishedLastNotification > 0L)
				{
					long elapsedMilliseconds = current.m_intervalTimer.ElapsedMilliseconds;
					if (!current.ShouldPerformShrink(kBytes, elapsedMilliseconds))
					{
						return false;
					}
					long num = Math.Max(0L, elapsedMilliseconds - current.m_timeFinishedLastNotification);
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, string.Format("{0} ms has elapsed since last memory shrink attempt for this appdomain ({1})", num, AppDomain.CurrentDomain.FriendlyName));
					RSTrace.SanitizedRdlEngineHostTracer.Trace(string.Format("{0} ms has elapsed since last memory shrink attempt", num));
				}
				flag = true;
				PriorityQueue<MemoryAuditProxy> priorityQueue = new PriorityQueue<MemoryAuditProxy>();
				long num2 = 0L;
				long elapsedMilliseconds2 = current.m_intervalTimer.ElapsedMilliseconds;
				Dictionary<WeakReference, WeakReference> maps = current.m_maps;
				lock (maps)
				{
					foreach (MemoryAuditProxy memoryAuditProxy in MemoryAuditCollection.GetProxies(current))
					{
						long num3 = Math.Max(0L, memoryAuditProxy.CurrentMemoryUsageKBytes);
						long num4 = Math.Max(0L, memoryAuditProxy.CurrentFreeableMemoryKBytes);
						long num5 = Math.Max(0L, memoryAuditProxy.PendingFreeKBytes);
						totalAuditedMemoryKb += num3;
						num2 += num4;
						if (num5 > 0L)
						{
							kBytes -= num5;
							estimatedKBytesFreed += num5;
						}
						long num6 = MemoryAuditCollection.ComputeMapScore(memoryAuditProxy);
						priorityQueue.Push(memoryAuditProxy, num6);
						if (notifyAllConsumers && num5 <= 0L)
						{
							memoryAuditProxy.SetNewMemoryTarget(num3);
						}
					}
				}
				long elapsedMilliseconds3 = current.m_intervalTimer.ElapsedMilliseconds;
				if (kBytes < 0L)
				{
					if (RSTrace.AppDomainManagerTracer.TraceVerbose)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "Memory shrink request satisfied by pending shrink notifications.");
					}
					RSTrace.SanitizedRdlEngineHostTracer.Trace("Memory shrink request satisfied by pending shrink notifications.");
				}
				else
				{
					long num7 = Process.GetCurrentProcess().PrivateMemorySize64 / 1024L;
					if (RSTrace.AppDomainManagerTracer.TraceVerbose)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "Memory Statistics: {0} items, {1}KB Audited, {2}KB Freeable, {3}KB Private Bytes", new object[] { priorityQueue.Count, totalAuditedMemoryKb, num2, num7 });
					}
					RSTrace.SanitizedRdlEngineHostTracer.Trace(string.Format("Memory Statistics: {0} items, {1}KB Audited, {2}KB Freeable, {3}KB Private Bytes", new object[] { priorityQueue.Count, totalAuditedMemoryKb, num2, num7 }));
					long elapsedMilliseconds4 = current.m_intervalTimer.ElapsedMilliseconds;
					foreach (MemoryAuditProxy memoryAuditProxy2 in priorityQueue.GetPoppingIterator())
					{
						long currentMemoryUsageKBytes = memoryAuditProxy2.CurrentMemoryUsageKBytes;
						long num8 = Math.Min(kBytes, memoryAuditProxy2.CurrentFreeableMemoryKBytes);
						if (num8 < 64L)
						{
							break;
						}
						long num9 = currentMemoryUsageKBytes - num8;
						memoryAuditProxy2.SetNewMemoryTarget(num9);
						kBytes -= num8;
						estimatedKBytesFreed += num8;
					}
					long elapsedMilliseconds5 = current.m_intervalTimer.ElapsedMilliseconds;
					if (RSTrace.AppDomainManagerTracer.TraceVerbose)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "Spent {0}ms enumerating MAP items and {1}ms dispatching notifications.", new object[]
						{
							Math.Max(0L, elapsedMilliseconds3 - elapsedMilliseconds2),
							Math.Max(0L, elapsedMilliseconds5 - elapsedMilliseconds4)
						});
					}
					RSTrace.SanitizedRdlEngineHostTracer.Trace(string.Format("Spent {0}ms enumerating MAP items and {1}ms dispatching notifications.", Math.Max(0L, elapsedMilliseconds3 - elapsedMilliseconds2), Math.Max(0L, elapsedMilliseconds5 - elapsedMilliseconds4)));
				}
				current.SetLastVersion();
				flag3 = true;
			}
			catch (Exception ex)
			{
				if (RSTrace.AppDomainManagerTracer.TraceWarning)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Warning, "Error while attempting to perform memory shrink: {0}", new object[] { ex.ToString() });
				}
				RSTrace.SanitizedRdlEngineHostTracer.Trace(string.Format("Error while attempting to perform memory shrink: {0}", ex));
				flag3 = false;
			}
			finally
			{
				if (flag)
				{
					current.m_timeFinishedLastNotification = current.m_intervalTimer.ElapsedMilliseconds;
				}
			}
			return flag3;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000F1EC File Offset: 0x0000D3EC
		private static long ComputeMapScore(MemoryAuditProxy map)
		{
			double num = Math.Min(1.0, Math.Max(0.0001, map.FreeOverhead));
			return (long)((double)map.CurrentFreeableMemoryKBytes * num);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000F226 File Offset: 0x0000D426
		private static IEnumerable<MemoryAuditProxy> GetProxies(MemoryAuditCollection currentGroup)
		{
			foreach (WeakReference weakReference in currentGroup.m_maps.Keys)
			{
				MemoryAuditProxy memoryAuditProxy = weakReference.Target as MemoryAuditProxy;
				if (memoryAuditProxy != null)
				{
					yield return memoryAuditProxy;
				}
			}
			Dictionary<WeakReference, WeakReference>.KeyCollection.Enumerator enumerator = default(Dictionary<WeakReference, WeakReference>.KeyCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000F238 File Offset: 0x0000D438
		private static void SweepReleasedMapsIfNecessary(MemoryAuditCollection currentGroup)
		{
			if ((currentGroup.m_tick = (currentGroup.m_tick + 1) % 1024) == 0)
			{
				if (1024 > currentGroup.m_maps.Keys.Count)
				{
					return;
				}
				List<WeakReference> list = new List<WeakReference>(1024);
				foreach (WeakReference weakReference in currentGroup.m_maps.Keys)
				{
					if (!weakReference.IsAlive)
					{
						list.Add(weakReference);
					}
					if (list.Count >= 1024)
					{
						break;
					}
				}
				foreach (WeakReference weakReference2 in list)
				{
					currentGroup.m_maps.Remove(weakReference2);
				}
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000F32C File Offset: 0x0000D52C
		private bool ShouldPerformShrink(long kBytes, long currentTime)
		{
			long num = currentTime - this.m_timeFinishedLastNotification;
			if (num < 250L)
			{
				if (RSTrace.AppDomainManagerTracer.TraceVerbose)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "Skipping shrink request for appdomain ({0}) because not enough time has passed since last shrink request.", new object[] { AppDomain.CurrentDomain.FriendlyName });
				}
				return false;
			}
			if (Interlocked.Read(ref this.m_version) == Interlocked.Read(ref this.m_lastVersion) && num < 1000L)
			{
				if (RSTrace.AppDomainManagerTracer.TraceVerbose)
				{
					RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "Skipping shrink request for appdomain ({0}) because no memory consuming requests have been added.", new object[] { AppDomain.CurrentDomain.FriendlyName });
				}
				RSTrace.SanitizedRdlEngineHostTracer.Trace("Skipping shrink request because no memory consuming requests have been added.");
				return false;
			}
			Dictionary<WeakReference, WeakReference> maps = this.m_maps;
			lock (maps)
			{
				if (this.m_maps.Count == 0)
				{
					if (RSTrace.AppDomainManagerTracer.TraceVerbose)
					{
						RSTrace.AppDomainManagerTracer.Trace(TraceLevel.Verbose, "Skipping shrink request for appdomain ({0}) because no memory consuming requests are registered.", new object[] { AppDomain.CurrentDomain.FriendlyName });
					}
					RSTrace.SanitizedRdlEngineHostTracer.Trace("Skipping shrink request  because no memory consuming requests are registered.");
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000F45C File Offset: 0x0000D65C
		private long IncrementVersion()
		{
			return Interlocked.Increment(ref this.m_version);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000F469 File Offset: 0x0000D669
		private long GetVersion()
		{
			return Interlocked.Read(ref this.m_version);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000F476 File Offset: 0x0000D676
		private void SetLastVersion()
		{
			Interlocked.Exchange(ref this.m_lastVersion, Interlocked.Read(ref this.m_version));
		}

		// Token: 0x040002CD RID: 717
		private const long MinimumTimeBetweenNotification = 250L;

		// Token: 0x040002CE RID: 718
		private const int SweepCount = 1024;

		// Token: 0x040002CF RID: 719
		private const int MaxReleasePerSweep = 1024;

		// Token: 0x040002D0 RID: 720
		private const int PerformSweepThreshold = 1024;

		// Token: 0x040002D1 RID: 721
		private static MemoryAuditCollection m_current = new MemoryAuditCollection();

		// Token: 0x040002D2 RID: 722
		private static MemoryAuditCollection m_BackGroundCurrent = new MemoryAuditCollection();

		// Token: 0x040002D3 RID: 723
		private static MemoryAuditCollection m_InteractiveCurrent = new MemoryAuditCollection();

		// Token: 0x040002D4 RID: 724
		private int m_tick;

		// Token: 0x040002D5 RID: 725
		private Dictionary<WeakReference, WeakReference> m_maps = new Dictionary<WeakReference, WeakReference>();

		// Token: 0x040002D6 RID: 726
		private long m_version;

		// Token: 0x040002D7 RID: 727
		private long m_lastVersion;

		// Token: 0x040002D8 RID: 728
		private Stopwatch m_intervalTimer = new Stopwatch();

		// Token: 0x040002D9 RID: 729
		private long m_timeFinishedLastNotification = -1L;
	}
}
