using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000009 RID: 9
	internal sealed class ClientPerformanceCounters : IDisposable
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002B3C File Offset: 0x00000D3C
		private static void InitializeClientPerformanceCounters()
		{
			if (ClientPerformanceCounters.Instance == null)
			{
				lock (ClientPerformanceCounters.initializationLock)
				{
					if (ClientPerformanceCounters.Instance == null)
					{
						ClientPerformanceCounters.isPerfCountersEnabled = false;
						try
						{
							if (PerformanceCounterCategory.Exists("Windows Azure Caching:Client"))
							{
								ClientPerformanceCounters.Instance = new ClientPerformanceCounters();
								ClientPerformanceCounters.isPerfCountersEnabled = true;
							}
						}
						catch (InvalidOperationException)
						{
						}
						catch (Win32Exception)
						{
						}
						catch (UnauthorizedAccessException)
						{
						}
						catch (AppDomainUnloadedException)
						{
						}
					}
				}
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002BE4 File Offset: 0x00000DE4
		private ClientPerformanceCounters()
		{
			this.totalClientCounters = Enum.GetValues(typeof(ClientPerfCounterUpdate.StatName)).Length;
			ClientPerformanceCounters.clientInstancePerformanceCounters = new PerformanceCounter[this.totalClientCounters];
			ClientPerformanceCounters.clientAggregatePerformanceCounters = new PerformanceCounter[this.totalClientCounters];
			ClientPerformanceCounters.CreateCounters();
			this.counterTimer = new Timer(new TimerCallback(ClientPerformanceCounters.ClientPerfCounterWorker), null, 950L, 950L);
			AppDomain.CurrentDomain.UnhandledException += ClientPerformanceCounters.CurrentDomain_UnhandledException;
			AppDomain.CurrentDomain.DomainUnload += ClientPerformanceCounters.CurrentDomain_ProcessShutdown;
			AppDomain.CurrentDomain.ProcessExit += ClientPerformanceCounters.CurrentDomain_ProcessShutdown;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C9C File Offset: 0x00000E9C
		private static void CurrentDomain_ProcessShutdown(object sender, EventArgs e)
		{
			if (ClientPerformanceCounters.isPerfCountersEnabled)
			{
				ClientPerformanceCounters.CleanupCounters();
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002CAC File Offset: 0x00000EAC
		private static void CleanupCounters()
		{
			if (Monitor.TryEnter(ClientPerformanceCounters.updateLock, 2000))
			{
				try
				{
					if (!ClientPerformanceCounters.isPerfCountersCleanedUp)
					{
						ClientPerformanceCounters.Instance.CleanupTimer();
						foreach (PerformanceCounter performanceCounter in ClientPerformanceCounters.clientInstancePerformanceCounters)
						{
							performanceCounter.RemoveInstance();
						}
						ClientPerformanceCounters.clientAggregatePerformanceCounters[20].IncrementBy(-ClientPerfCounterUpdate.LastPublishedStats[20]);
						ClientPerformanceCounters.clientAggregatePerformanceCounters[10].IncrementBy(-ClientPerfCounterUpdate.LastPublishedStats[10]);
						ClientPerformanceCounters.clientAggregatePerformanceCounters[11].IncrementBy(-ClientPerfCounterUpdate.LastPublishedStats[11]);
						ClientPerformanceCounters.clientAggregatePerformanceCounters[24].IncrementBy(-ClientPerfCounterUpdate.LastPublishedStats[24]);
						ClientPerformanceCounters.clientAggregatePerformanceCounters[18].IncrementBy(-ClientPerfCounterUpdate.LastPublishedStats[18]);
						ClientPerfCounterUpdate.Stats[20] = 0L;
						ClientPerfCounterUpdate.Stats[10] = 0L;
						ClientPerfCounterUpdate.Stats[11] = 0L;
						ClientPerfCounterUpdate.Stats[24] = 0L;
						ClientPerfCounterUpdate.Stats[18] = 0L;
						ClientPerfCounterUpdate.LastPublishedStats[20] = 0L;
						ClientPerfCounterUpdate.LastPublishedStats[10] = 0L;
						ClientPerfCounterUpdate.LastPublishedStats[11] = 0L;
						ClientPerfCounterUpdate.LastPublishedStats[24] = 0L;
						ClientPerfCounterUpdate.LastPublishedStats[18] = 0L;
						ClientPerformanceCounters.isPerfCountersCleanedUp = true;
					}
				}
				catch (InvalidOperationException)
				{
				}
				finally
				{
					Monitor.Exit(ClientPerformanceCounters.updateLock);
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C9C File Offset: 0x00000E9C
		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			if (ClientPerformanceCounters.isPerfCountersEnabled)
			{
				ClientPerformanceCounters.CleanupCounters();
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E24 File Offset: 0x00001024
		private static void CreateCounters()
		{
			ClientPerformanceCounters.CreateInstanceCounters();
			ClientPerformanceCounters.CreateAggregateCounters();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E30 File Offset: 0x00001030
		private static void CreateAggregateCounters()
		{
			ClientPerformanceCounters.clientAggregatePerformanceCounters[26] = new PerformanceCounter("Windows Azure Caching:Client Host", "Requests", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[27] = new PerformanceCounter("Windows Azure Caching:Client Host", "Requests / sec", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[15] = new PerformanceCounter("Windows Azure Caching:Client Host", "Server Responses Dropped / sec", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[19] = new PerformanceCounter("Windows Azure Caching:Client Host", "Failure Exceptions", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[6] = new PerformanceCounter("Windows Azure Caching:Client Host", "Failure Exceptions / sec", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[21] = new PerformanceCounter("Windows Azure Caching:Client Host", "Total Local Cache Hits", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[20] = new PerformanceCounter("Windows Azure Caching:Client Host", "Total Local Cache Objects", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[2] = new PerformanceCounter("Windows Azure Caching:Client Host", "Average Get Latency (Network) / operation Microsecond", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[3] = new PerformanceCounter("Windows Azure Caching:Client Host", "Average Get Latency (Network) / operation Microsecond Base", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[25] = new PerformanceCounter("Windows Azure Caching:Client Host", "Read Requests", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[30] = new PerformanceCounter("Windows Azure Caching:Client Host", "Write Requests", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[7] = new PerformanceCounter("Windows Azure Caching:Client Host", "Bytes Received / sec", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[9] = new PerformanceCounter("Windows Azure Caching:Client Host", "Bytes Sent / sec", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[0] = new PerformanceCounter("Windows Azure Caching:Client Host", "Average Get Latency / operation Microsecond", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[1] = new PerformanceCounter("Windows Azure Caching:Client Host", "Average Get Latency / operation Microsecond Base", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[4] = new PerformanceCounter("Windows Azure Caching:Client Host", "Average Put Latency / operation Microsecond", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[5] = new PerformanceCounter("Windows Azure Caching:Client Host", "Average Put Latency / operation Microsecond Base", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[10] = new PerformanceCounter("Windows Azure Caching:Client Host", "Local Cache Filled Percentage", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[11] = new PerformanceCounter("Windows Azure Caching:Client Host", "Local Cache Filled Percentage Base", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[12] = new PerformanceCounter("Windows Azure Caching:Client Host", "Local Cache Hits Percentage", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[13] = new PerformanceCounter("Windows Azure Caching:Client Host", "Local Cache Hits Percentage Base", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[23] = new PerformanceCounter("Windows Azure Caching:Client Host", "Total Notifications Received", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[28] = new PerformanceCounter("Windows Azure Caching:Client Host", "Retry Exceptions", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[29] = new PerformanceCounter("Windows Azure Caching:Client Host", "Timeout Exceptions", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[14] = new PerformanceCounter("Windows Azure Caching:Client Host", "Retry Exceptions / sec", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[16] = new PerformanceCounter("Windows Azure Caching:Client Host", "Timeout Exceptions / sec", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[18] = new PerformanceCounter("Windows Azure Caching:Client Host", "Current Server Connections", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[17] = new PerformanceCounter("Windows Azure Caching:Client Host", "Total Connection Requests Failed", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[22] = new PerformanceCounter("Windows Azure Caching:Client Host", "Network Exceptions", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[8] = new PerformanceCounter("Windows Azure Caching:Client Host", "Network Exceptions / sec", false);
			ClientPerformanceCounters.clientAggregatePerformanceCounters[24] = new PerformanceCounter("Windows Azure Caching:Client Host", "Current Waiting Requests", false);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000311C File Offset: 0x0000131C
		private static void CreateInstanceCounters()
		{
			string text = Process.GetCurrentProcess().ProcessName;
			if (text.Length > 120)
			{
				text = text.Substring(0, 120);
			}
			string text2 = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", new object[]
			{
				text,
				Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture)
			});
			ClientPerformanceCounters.clientInstancePerformanceCounters[26] = new PerformanceCounter("Windows Azure Caching:Client", "Requests", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[27] = new PerformanceCounter("Windows Azure Caching:Client", "Requests / sec", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[15] = new PerformanceCounter("Windows Azure Caching:Client", "Server Responses Dropped / sec", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[19] = new PerformanceCounter("Windows Azure Caching:Client", "Failure Exceptions", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[6] = new PerformanceCounter("Windows Azure Caching:Client", "Failure Exceptions / sec", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[20] = new PerformanceCounter("Windows Azure Caching:Client", "Total Local Cache Objects", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[21] = new PerformanceCounter("Windows Azure Caching:Client", "Total Local Cache Hits", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[2] = new PerformanceCounter("Windows Azure Caching:Client", "Average Get Latency (Network) / operation Microsecond", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[3] = new PerformanceCounter("Windows Azure Caching:Client", "Average Get Latency (Network) / operation Microsecond Base", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[25] = new PerformanceCounter("Windows Azure Caching:Client", "Read Requests", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[30] = new PerformanceCounter("Windows Azure Caching:Client", "Write Requests", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[7] = new PerformanceCounter("Windows Azure Caching:Client", "Bytes Received / sec", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[9] = new PerformanceCounter("Windows Azure Caching:Client", "Bytes Sent / sec", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[0] = new PerformanceCounter("Windows Azure Caching:Client", "Average Get Latency / operation Microsecond", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[1] = new PerformanceCounter("Windows Azure Caching:Client", "Average Get Latency / operation Microsecond Base", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[4] = new PerformanceCounter("Windows Azure Caching:Client", "Average Put Latency / operation Microsecond", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[5] = new PerformanceCounter("Windows Azure Caching:Client", "Average Put Latency / operation Microsecond Base", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[10] = new PerformanceCounter("Windows Azure Caching:Client", "Local Cache Filled Percentage", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[11] = new PerformanceCounter("Windows Azure Caching:Client", "Local Cache Filled Percentage Base", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[12] = new PerformanceCounter("Windows Azure Caching:Client", "Local Cache Hits Percentage", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[13] = new PerformanceCounter("Windows Azure Caching:Client", "Local Cache Hits Percentage Base", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[23] = new PerformanceCounter("Windows Azure Caching:Client", "Total Notifications Received", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[28] = new PerformanceCounter("Windows Azure Caching:Client", "Retry Exceptions", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[29] = new PerformanceCounter("Windows Azure Caching:Client", "Timeout Exceptions", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[14] = new PerformanceCounter("Windows Azure Caching:Client", "Retry Exceptions / sec", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[16] = new PerformanceCounter("Windows Azure Caching:Client", "Timeout Exceptions / sec", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[18] = new PerformanceCounter("Windows Azure Caching:Client", "Current Server Connections", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[17] = new PerformanceCounter("Windows Azure Caching:Client", "Total Connection Requests Failed", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[22] = new PerformanceCounter("Windows Azure Caching:Client", "Network Exceptions", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[8] = new PerformanceCounter("Windows Azure Caching:Client", "Network Exceptions / sec", text2, false);
			ClientPerformanceCounters.clientInstancePerformanceCounters[24] = new PerformanceCounter("Windows Azure Caching:Client", "Current Waiting Requests", text2, false);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000347C File Offset: 0x0000167C
		internal void Update()
		{
			if (ClientPerformanceCounters.isPerfCountersEnabled && Monitor.TryEnter(ClientPerformanceCounters.updateLock))
			{
				try
				{
					if (!ClientPerformanceCounters.isPerfCountersCleanedUp)
					{
						for (int i = 0; i < this.totalClientCounters; i++)
						{
							long num = ClientPerfCounterUpdate.Stats[i];
							ClientPerformanceCounters.clientInstancePerformanceCounters[i].RawValue = num;
							long num2 = num - ClientPerfCounterUpdate.LastPublishedStats[i];
							ClientPerfCounterUpdate.LastPublishedStats[i] = num;
							ClientPerformanceCounters.clientAggregatePerformanceCounters[i].IncrementBy(num2);
						}
					}
				}
				catch (InvalidOperationException)
				{
					ClientPerformanceCounters.isPerfCountersEnabled = false;
				}
				catch (Win32Exception)
				{
					ClientPerformanceCounters.isPerfCountersEnabled = false;
				}
				catch (PlatformNotSupportedException)
				{
					ClientPerformanceCounters.isPerfCountersEnabled = false;
				}
				catch (UnauthorizedAccessException)
				{
					ClientPerformanceCounters.isPerfCountersEnabled = false;
				}
				finally
				{
					Monitor.Exit(ClientPerformanceCounters.updateLock);
				}
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003564 File Offset: 0x00001764
		internal static bool IsPerfCounterCategoryExists()
		{
			if (ClientPerformanceCounters.Instance == null)
			{
				ClientPerformanceCounters.InitializeClientPerformanceCounters();
			}
			return ClientPerformanceCounters.isPerfCountersEnabled;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003577 File Offset: 0x00001777
		private static void ClientPerfCounterWorker(object obj)
		{
			DataCacheFactoryConfigurationSynchronizer.SynchronizerInstance.UpdateLocalCacheInformation();
			ClientPerformanceCounters.Instance.Update();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000358D File Offset: 0x0000178D
		private void CleanupTimer()
		{
			if (this.counterTimer != null)
			{
				this.counterTimer.Dispose();
				this.counterTimer = null;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000035A9 File Offset: 0x000017A9
		public void Dispose()
		{
			this.CleanupTimer();
		}

		// Token: 0x0400001C RID: 28
		public const string InstanceCounterCategoryName = "Windows Azure Caching:Client";

		// Token: 0x0400001D RID: 29
		public const string CounterCategoryHelp = "Performance counters on client side";

		// Token: 0x0400001E RID: 30
		public const string AggregateCounterCategoryName = "Windows Azure Caching:Client Host";

		// Token: 0x0400001F RID: 31
		public const string TotalRequestsCounterName = "Requests";

		// Token: 0x04000020 RID: 32
		public const string RequestPerSecondCounterName = "Requests / sec";

		// Token: 0x04000021 RID: 33
		public const string ServerResponseDroppedCounterName = "Server Responses Dropped / sec";

		// Token: 0x04000022 RID: 34
		public const string TotalFailureExceptionsCounterName = "Failure Exceptions";

		// Token: 0x04000023 RID: 35
		public const string FailureExceptionRate = "Failure Exceptions / sec";

		// Token: 0x04000024 RID: 36
		public const string TotalLocalCacheHitsCounterName = "Total Local Cache Hits";

		// Token: 0x04000025 RID: 37
		public const string TotalItemsInLocalCacheCounterName = "Total Local Cache Objects";

		// Token: 0x04000026 RID: 38
		public const string AverageNetworkLatencyCounterName = "Average Get Latency (Network) / operation Microsecond";

		// Token: 0x04000027 RID: 39
		public const string AverageNetworkLatencyBaseCounterName = "Average Get Latency (Network) / operation Microsecond Base";

		// Token: 0x04000028 RID: 40
		public const string TotalReadsCounterName = "Read Requests";

		// Token: 0x04000029 RID: 41
		public const string TotalWritesCounterName = "Write Requests";

		// Token: 0x0400002A RID: 42
		public const string IncomingDataRateCounterName = "Bytes Received / sec";

		// Token: 0x0400002B RID: 43
		public const string OutgoingDataRateCounterName = "Bytes Sent / sec";

		// Token: 0x0400002C RID: 44
		public const string AverageCacheGetLatencyCounterName = "Average Get Latency / operation Microsecond";

		// Token: 0x0400002D RID: 45
		public const string AverageCacheGetLatencyBaseCounterName = "Average Get Latency / operation Microsecond Base";

		// Token: 0x0400002E RID: 46
		public const string AverageCachePutLatencyCounterName = "Average Put Latency / operation Microsecond";

		// Token: 0x0400002F RID: 47
		public const string AverageCachePutLatencyBaseCounterName = "Average Put Latency / operation Microsecond Base";

		// Token: 0x04000030 RID: 48
		public const string PercentageLocalCacheFullCounterName = "Local Cache Filled Percentage";

		// Token: 0x04000031 RID: 49
		public const string PercentageLocalCacheFullBaseCounterName = "Local Cache Filled Percentage Base";

		// Token: 0x04000032 RID: 50
		public const string PercentageLocalCacheHitsCounterName = "Local Cache Hits Percentage";

		// Token: 0x04000033 RID: 51
		public const string PercentageLocalCacheHitsBaseCounterName = "Local Cache Hits Percentage Base";

		// Token: 0x04000034 RID: 52
		public const string TotalNotificationsReceivedCounterName = "Total Notifications Received";

		// Token: 0x04000035 RID: 53
		public const string TotalRetryExceptionsCounterName = "Retry Exceptions";

		// Token: 0x04000036 RID: 54
		public const string TotalTimeoutExceptionsCounterName = "Timeout Exceptions";

		// Token: 0x04000037 RID: 55
		public const string RetryExceptionRateCounterName = "Retry Exceptions / sec";

		// Token: 0x04000038 RID: 56
		public const string TimeoutExceptionRateCounterName = "Timeout Exceptions / sec";

		// Token: 0x04000039 RID: 57
		public const string TotalConnectionsCounterName = "Current Server Connections";

		// Token: 0x0400003A RID: 58
		public const string TotalConnectionRequestsFailedCounterName = "Total Connection Requests Failed";

		// Token: 0x0400003B RID: 59
		public const string TotalNetworkExceptionsCounterName = "Network Exceptions";

		// Token: 0x0400003C RID: 60
		public const string NetworkExceptionRate = "Network Exceptions / sec";

		// Token: 0x0400003D RID: 61
		public const string TotalOutstandingRequestsCounterName = "Current Waiting Requests";

		// Token: 0x0400003E RID: 62
		private const int updateLockWaitTimeout = 2000;

		// Token: 0x0400003F RID: 63
		private static PerformanceCounter[] clientInstancePerformanceCounters;

		// Token: 0x04000040 RID: 64
		private static PerformanceCounter[] clientAggregatePerformanceCounters;

		// Token: 0x04000041 RID: 65
		private static bool isPerfCountersEnabled;

		// Token: 0x04000042 RID: 66
		private static readonly object initializationLock = new object();

		// Token: 0x04000043 RID: 67
		private static readonly object updateLock = new object();

		// Token: 0x04000044 RID: 68
		private readonly int totalClientCounters;

		// Token: 0x04000045 RID: 69
		private Timer counterTimer;

		// Token: 0x04000046 RID: 70
		internal static ClientPerformanceCounters Instance;

		// Token: 0x04000047 RID: 71
		private static bool isPerfCountersCleanedUp;
	}
}
