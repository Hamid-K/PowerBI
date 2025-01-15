using System;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200030A RID: 778
	internal abstract class HostPerfCounter : PerfCounter
	{
		// Token: 0x06001C95 RID: 7317 RVA: 0x000566C0 File Offset: 0x000548C0
		protected HostPerfCounter(HostPerfCounter.Name name)
		{
			this.Initialize(name);
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x000566CF File Offset: 0x000548CF
		protected HostPerfCounter(HostPerfCounter.Name name, bool register)
			: base(register)
		{
			this.Initialize(name);
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x000566E0 File Offset: 0x000548E0
		private void Initialize(HostPerfCounter.Name name)
		{
			this._counterName = name;
			try
			{
				if (HostPerfCounter.CounterSetInstance != null)
				{
					int counterID = HostPerfCounter.GetCounterID(name);
					this._counter = HostPerfCounter.CounterSetInstance.Counters[counterID];
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose<string, string, int>(PerfCounter.LogSource, " counter createdCategory = {0}: CounterName = {1} index = {2}", HostPerfCounter.GetCategory(), HostPerfCounter.GetCounterName(name), (int)this._counterName);
					}
				}
				else
				{
					this.Delete();
					if (Provider.IsEnabled(TraceLevel.Error))
					{
						EventLogWriter.WriteError(PerfCounter.LogSource, "Failed to create counter Category = {0}: CounterName = {1}. CounterSetInstance = null ", new object[]
						{
							HostPerfCounter.GetCategory(),
							HostPerfCounter.GetCounterName(name)
						});
					}
				}
			}
			catch (Exception ex)
			{
				if (!PerformanceMonitorCounter.CheckException(ex))
				{
					throw;
				}
				this.Delete();
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(PerfCounter.LogSource, "Failed to create counter Category = {0}: CounterName = {1}. {2}", new object[]
					{
						HostPerfCounter.GetCategory(),
						HostPerfCounter.GetCounterName(name),
						ex
					});
				}
			}
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x000567D4 File Offset: 0x000549D4
		internal static int GetCounterID(HostPerfCounter.Name name)
		{
			return HostPerfCounter.Map[(int)name].ID;
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x000567E6 File Offset: 0x000549E6
		internal static string GetCounterName(HostPerfCounter.Name name)
		{
			return HostPerfCounter.Map[(int)name].Name;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x000567F8 File Offset: 0x000549F8
		internal static string GetCategory()
		{
			return ConfigManager.HostCounterCategory;
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x000567FF File Offset: 0x000549FF
		private new void Delete()
		{
			base.Delete();
			if (this._counter != null)
			{
				this._counter = null;
			}
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00056818 File Offset: 0x00054A18
		internal override void Update()
		{
			if (this._counter != null)
			{
				try
				{
					long value = this.GetValue();
					this._counter.Value = value;
				}
				catch (Exception ex)
				{
					if (!PerformanceMonitorCounter.CheckException(ex))
					{
						throw;
					}
					this.Delete();
					if (Provider.IsEnabled(TraceLevel.Error))
					{
						EventLogWriter.WriteError(PerfCounter.LogSource, "counter updation failed Category = {0}. {1}", new object[]
						{
							HostPerfCounter.GetCategory(),
							ex
						});
					}
				}
			}
		}

		// Token: 0x06001C9D RID: 7325
		internal abstract long GetValue();

		// Token: 0x06001C9E RID: 7326 RVA: 0x00056890 File Offset: 0x00054A90
		internal static bool InitializeCounterSet()
		{
			bool flag = false;
			CounterSet counterSet = PerfCounter.InitializeCounterSetCommon(ref HostPerfCounter.CallOnce, HostPerfCounter.CounterSetGuid, CounterSetInstanceType.Single, HostPerfCounter.Map, HostPerfCounter.GetCategory());
			if (counterSet != null)
			{
				HostPerfCounter.CounterSet = counterSet;
				try
				{
					HostPerfCounter.CounterSetInstance = HostPerfCounter.CounterSet.CreateCounterSetInstance(HostPerfCounter.GetCategory());
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose(PerfCounter.LogSource, "CounterSetInstance is created : " + HostPerfCounter.GetCategory());
					}
					flag = true;
				}
				catch (ArgumentException ex)
				{
					if (Provider.IsEnabled(TraceLevel.Error))
					{
						EventLogWriter.WriteError(PerfCounter.LogSource, "Instance creation failed failed Category = {0} instance name = {1} Error{2}", new object[]
						{
							HostPerfCounter.GetCategory(),
							HostPerfCounter.GetCategory(),
							ex.ToString()
						});
					}
				}
				catch (InvalidOperationException ex2)
				{
					if (Provider.IsEnabled(TraceLevel.Error))
					{
						EventLogWriter.WriteError(PerfCounter.LogSource, "Instance creation failed failed Category = {0} instance name = {1} Error{2}", new object[]
						{
							HostPerfCounter.GetCategory(),
							HostPerfCounter.GetCategory(),
							ex2.ToString()
						});
					}
				}
			}
			return flag;
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x000569A0 File Offset: 0x00054BA0
		internal static bool IsInstalled()
		{
			return PerformanceCounterCategory.Exists(ConfigManager.HostCounterCategory);
		}

		// Token: 0x04000F6E RID: 3950
		private static readonly Guid CounterSetGuid = new Guid("{64c8448e-6a84-429a-95e5-6abeccb67f53}");

		// Token: 0x04000F6F RID: 3951
		internal static CounterSet CounterSet;

		// Token: 0x04000F70 RID: 3952
		internal static CounterSetInstance CounterSetInstance;

		// Token: 0x04000F71 RID: 3953
		private static int CallOnce;

		// Token: 0x04000F72 RID: 3954
		private CounterData _counter;

		// Token: 0x04000F73 RID: 3955
		private HostPerfCounter.Name _counterName;

		// Token: 0x04000F74 RID: 3956
		private static CounterTypeIDMap[] Map = new CounterTypeIDMap[]
		{
			new CounterTypeIDMap(ConfigManager.HostTotalDataSize, CounterType.RawData64, 0),
			new CounterTypeIDMap(ConfigManager.HostTotalMissCount, CounterType.RawData64, 3),
			new CounterTypeIDMap(ConfigManager.HostTotalMissCountPerSecond, CounterType.RateOfCountPerSecond64, 4),
			new CounterTypeIDMap(ConfigManager.HostTotalGetRequest, CounterType.RawData64, 5),
			new CounterTypeIDMap(ConfigManager.HostTotalGetRequestPerSecond, CounterType.RateOfCountPerSecond64, 6),
			new CounterTypeIDMap(ConfigManager.HostTotalPollRequest, CounterType.RawData64, 29),
			new CounterTypeIDMap(ConfigManager.HostTotalPollRequestPerSecond, CounterType.RateOfCountPerSecond64, 30),
			new CounterTypeIDMap(ConfigManager.HostTotalNotificationDelivered, CounterType.RawData64, 31),
			new CounterTypeIDMap(ConfigManager.HostTotalNotificationDeliveredPerSecond, CounterType.RateOfCountPerSecond64, 32),
			new CounterTypeIDMap(ConfigManager.HostTotalRequest, CounterType.RawData64, 17),
			new CounterTypeIDMap(ConfigManager.HostTotalRequestPerSecond, CounterType.RateOfCountPerSecond64, 18),
			new CounterTypeIDMap(ConfigManager.HostTotalResponse, CounterType.RawData64, 19),
			new CounterTypeIDMap(ConfigManager.HostTotalResponsePerSecond, CounterType.RateOfCountPerSecond64, 20),
			new CounterTypeIDMap(ConfigManager.HostTotalReadRequest, CounterType.RawData64, 11),
			new CounterTypeIDMap(ConfigManager.HostTotalReadRequestPerSecond, CounterType.RateOfCountPerSecond64, 12),
			new CounterTypeIDMap(ConfigManager.HostTotalWriteRequest, CounterType.RawData64, 15),
			new CounterTypeIDMap(ConfigManager.HostTotalWriteRequestPerSecond, CounterType.RateOfCountPerSecond64, 16),
			new CounterTypeIDMap(ConfigManager.HostTotalException, CounterType.RawData64, 25),
			new CounterTypeIDMap(ConfigManager.HostTotalExceptionPerSecond, CounterType.RateOfCountPerSecond64, 26),
			new CounterTypeIDMap(ConfigManager.HostTotalRetryException, CounterType.RawData64, 28),
			new CounterTypeIDMap(ConfigManager.HostTotalRetryExceptionPerSecond, CounterType.RateOfCountPerSecond64, 27),
			new CounterTypeIDMap(ConfigManager.HostTotalGetAndLockRequest, CounterType.RawData64, 33),
			new CounterTypeIDMap(ConfigManager.HostTotalGetAndLockRequestPerSecond, CounterType.RateOfCountPerSecond64, 34),
			new CounterTypeIDMap(ConfigManager.HostTotalSuccessfulGetAndLockRequest, CounterType.RawData64, 35),
			new CounterTypeIDMap(ConfigManager.HostTotalSuccessfulGetAndLockRequestPerSecond, CounterType.RateOfCountPerSecond64, 36),
			new CounterTypeIDMap(ConfigManager.HostPercentageMiss, CounterType.RawFraction64, 10),
			new CounterTypeIDMap(ConfigManager.HostPercentageMissBase, CounterType.RawBase64, 9),
			new CounterTypeIDMap(ConfigManager.HostTotalExpiredObjects, CounterType.RawData64, 21),
			new CounterTypeIDMap(ConfigManager.HostTotalEvictedObjects, CounterType.RawData64, 22),
			new CounterTypeIDMap(ConfigManager.HostTotalEvictionRun, CounterType.RawData64, 24),
			new CounterTypeIDMap(ConfigManager.HostTotalMemoryEvicted, CounterType.RawData64, 23),
			new CounterTypeIDMap(ConfigManager.HostTotalObjectReturned, CounterType.RawData64, 13),
			new CounterTypeIDMap(ConfigManager.HostAverageQuorumResponseTime, CounterType.AverageCount64, 38),
			new CounterTypeIDMap(ConfigManager.HostAverageQuorumResponseTimeBase, CounterType.AverageBase, 37),
			new CounterTypeIDMap(ConfigManager.HostAverageAllSecondaryResponseTime, CounterType.AverageCount64, 40),
			new CounterTypeIDMap(ConfigManager.HostAverageAllSecondaryResponseTimeBase, CounterType.AverageBase, 39),
			new CounterTypeIDMap(ConfigManager.HostTotalObjectReturnedPerSec, CounterType.RateOfCountPerSecond64, 14),
			new CounterTypeIDMap(ConfigManager.HostTotalGetRequestMissCount, CounterType.RawData64, 7),
			new CounterTypeIDMap(ConfigManager.HostTotalGetRequestMissCountPerSecond, CounterType.RateOfCountPerSecond64, 8),
			new CounterTypeIDMap(ConfigManager.HostTotalObjectCount, CounterType.RawData64, 1),
			new CounterTypeIDMap(ConfigManager.HostTotalPrimaryDataSize, CounterType.RawData64, 2),
			new CounterTypeIDMap(ConfigManager.HostTotalSecondaryDataSize, CounterType.RawData64, 41),
			new CounterTypeIDMap(ConfigManager.TotalAvailableMemory, CounterType.RawData64, 42),
			new CounterTypeIDMap(ConfigManager.AvailableMemoryPercentageBase, CounterType.RawBase64, 43),
			new CounterTypeIDMap(ConfigManager.AvailableMemoryPercentage, CounterType.RawFraction64, 44),
			new CounterTypeIDMap(ConfigManager.TotalAvailableCacheItemCount, CounterType.RawData64, 45),
			new CounterTypeIDMap(ConfigManager.TotalAllocatedCacheItemCount, CounterType.RawData64, 46),
			new CounterTypeIDMap(ConfigManager.AvailableCacheItemPercentageBase, CounterType.RawBase64, 47),
			new CounterTypeIDMap(ConfigManager.AvailableCacheItemPercentage, CounterType.RawFraction64, 48),
			new CounterTypeIDMap(ConfigManager.TotalAvailableDirectoryCount, CounterType.RawData64, 49),
			new CounterTypeIDMap(ConfigManager.TotalAllocatedDirectoryCount, CounterType.RawData64, 50),
			new CounterTypeIDMap(ConfigManager.AvailableDirectoryPercentageBase, CounterType.RawBase64, 51),
			new CounterTypeIDMap(ConfigManager.AvailableDirectoryPercentage, CounterType.RawFraction64, 52),
			new CounterTypeIDMap(ConfigManager.HostWBFlushedItemCount, CounterType.RawData64, 53),
			new CounterTypeIDMap(ConfigManager.HostWBDroppedItemCount, CounterType.RawData64, 54),
			new CounterTypeIDMap(ConfigManager.HostWBQueueCount, CounterType.RawData64, 55),
			new CounterTypeIDMap(ConfigManager.TotalAvailableWBItemCount, CounterType.RawData64, 56),
			new CounterTypeIDMap(ConfigManager.TotalAllocatedWBItemCount, CounterType.RawData64, 57),
			new CounterTypeIDMap(ConfigManager.AvailableWBItemPercentageBase, CounterType.RawBase64, 58),
			new CounterTypeIDMap(ConfigManager.AvailableWBItemPercentage, CounterType.RawFraction64, 59),
			new CounterTypeIDMap(ConfigManager.HostRTPendingCount, CounterType.RawFraction64, 60),
			new CounterTypeIDMap(ConfigManager.HostRTSuccessCount, CounterType.RawFraction64, 61),
			new CounterTypeIDMap(ConfigManager.HostRTMissingCount, CounterType.RawFraction64, 62),
			new CounterTypeIDMap(ConfigManager.HostRTErrorCount, CounterType.RawFraction64, 63),
			new CounterTypeIDMap(ConfigManager.RouteFailingPercentageBase, CounterType.AverageBase, 64),
			new CounterTypeIDMap(ConfigManager.RouteFailingPercentage, CounterType.AverageCount64, 65),
			new CounterTypeIDMap(ConfigManager.GatewayProcessTimeBase, CounterType.AverageBase, 66),
			new CounterTypeIDMap(ConfigManager.GatewayProcessTime, CounterType.AverageTimer32, 67),
			new CounterTypeIDMap(ConfigManager.RequestProcessingErrorsPercentageBase, CounterType.AverageBase, 68),
			new CounterTypeIDMap(ConfigManager.RequestProcessingErrorsPercentage, CounterType.AverageCount64, 69),
			new CounterTypeIDMap(ConfigManager.TimeSinceGracefulShutdownStart, CounterType.RawData64, 70),
			new CounterTypeIDMap(ConfigManager.TotalConnectionsCount, CounterType.RawData64, 71),
			new CounterTypeIDMap(ConfigManager.ThrottledConnectionsCount, CounterType.RawData64, 72)
		};

		// Token: 0x0200030B RID: 779
		internal enum Name
		{
			// Token: 0x04000F76 RID: 3958
			TOTAL_DATA_SIZE,
			// Token: 0x04000F77 RID: 3959
			TOTAL_MISS_COUNT,
			// Token: 0x04000F78 RID: 3960
			TOTAL_MISS_COUNT_PER_SEC,
			// Token: 0x04000F79 RID: 3961
			TOTAL_GET_REQUEST,
			// Token: 0x04000F7A RID: 3962
			TOTAL_GET_REQUEST_PER_SEC,
			// Token: 0x04000F7B RID: 3963
			TOTAL_POLL_REQUEST,
			// Token: 0x04000F7C RID: 3964
			TOTAL_POLL_REQUEST_PER_SEC,
			// Token: 0x04000F7D RID: 3965
			TOTAL_NOTIFICATION_DELIVERED,
			// Token: 0x04000F7E RID: 3966
			TOTAL_NOTIFICATION_DELIVERED_PER_SEC,
			// Token: 0x04000F7F RID: 3967
			TOTAL_REQUEST,
			// Token: 0x04000F80 RID: 3968
			TOTAL_REQUEST_PER_SEC,
			// Token: 0x04000F81 RID: 3969
			TOTAL_RESPONSE,
			// Token: 0x04000F82 RID: 3970
			TOTAL_RESPONSE_PER_SEC,
			// Token: 0x04000F83 RID: 3971
			TOTAL_READ_REQUEST,
			// Token: 0x04000F84 RID: 3972
			TOTAL_READ_REQUEST_PER_SEC,
			// Token: 0x04000F85 RID: 3973
			TOTAL_WRITE_REQUEST,
			// Token: 0x04000F86 RID: 3974
			TOTAL_WRITE_REQUEST_PER_SEC,
			// Token: 0x04000F87 RID: 3975
			TOTAL_EXCEPTION,
			// Token: 0x04000F88 RID: 3976
			TOTAL_EXCEPTION_PER_SEC,
			// Token: 0x04000F89 RID: 3977
			TOTAL_RETRY_EXCEPTION,
			// Token: 0x04000F8A RID: 3978
			TOTAL_RETRY_EXCEPTION_PER_SEC,
			// Token: 0x04000F8B RID: 3979
			TOTAL_GETANDLOCK_REQUEST,
			// Token: 0x04000F8C RID: 3980
			TOTAL_GETANDLOCK_REQUEST_PER_SEC,
			// Token: 0x04000F8D RID: 3981
			TOTAL_SUCCESSFULGETANDLOCK_REQUEST,
			// Token: 0x04000F8E RID: 3982
			TOTAL_SUCCESSFULGETANDLOCK_REQUEST_PER_SEC,
			// Token: 0x04000F8F RID: 3983
			PERCENTAGE_MISS,
			// Token: 0x04000F90 RID: 3984
			PERCENTAGE_MISS_BASE,
			// Token: 0x04000F91 RID: 3985
			TOTAL_EXPIRED_OBJECTS,
			// Token: 0x04000F92 RID: 3986
			TOTAL_EVICTED_OBJECTS,
			// Token: 0x04000F93 RID: 3987
			TOTAL_EVICTION_RUN,
			// Token: 0x04000F94 RID: 3988
			TOTAL_MEMORY_EVICTED,
			// Token: 0x04000F95 RID: 3989
			TOTAL_OBJECTS_RETURNED,
			// Token: 0x04000F96 RID: 3990
			AVERAGE_QUORUM_RESPONSE_TIME,
			// Token: 0x04000F97 RID: 3991
			AVERAGE_QUORUM_RESPONSE_TIME_BASE,
			// Token: 0x04000F98 RID: 3992
			AVERAGE_ALL_SECONDARY_RESPONSE_TIME,
			// Token: 0x04000F99 RID: 3993
			AVERAGE_ALL_SECONDARY_RESPONSE_TIME_BASE,
			// Token: 0x04000F9A RID: 3994
			TOTAL_OBJECTS_RETURNED_PER_SEC,
			// Token: 0x04000F9B RID: 3995
			TOTAL_GET_REQUEST_MISS,
			// Token: 0x04000F9C RID: 3996
			TOTAL_GET_REQUEST_MISS_PER_SEC,
			// Token: 0x04000F9D RID: 3997
			TOTAL_OBJECT_COUNT,
			// Token: 0x04000F9E RID: 3998
			TOTAL_PRIMARY_DATA_SIZE,
			// Token: 0x04000F9F RID: 3999
			TOTAL_SECONDARY_DATA_SIZE,
			// Token: 0x04000FA0 RID: 4000
			TOTAL_AVAILABLE_MEMORY,
			// Token: 0x04000FA1 RID: 4001
			AVAILABLE_MEMORY_PERCENTAGE_BASE,
			// Token: 0x04000FA2 RID: 4002
			AVAILABLE_MEMORY_PERCENTAGE,
			// Token: 0x04000FA3 RID: 4003
			TOTAL_AVAILABLE_CACHEITEM_COUNT,
			// Token: 0x04000FA4 RID: 4004
			TOTAL_ALLOCATED_CACHEITEM_COUNT,
			// Token: 0x04000FA5 RID: 4005
			AVAILABLE_CACHEITEM_PERCENTAGE_BASE,
			// Token: 0x04000FA6 RID: 4006
			AVAILABLE_CACHEITEM_PERCENTAGE,
			// Token: 0x04000FA7 RID: 4007
			TOTAL_AVAILABLE_DIRECTORY_COUNT,
			// Token: 0x04000FA8 RID: 4008
			TOTAL_ALLOCATED_DIRECTORY_COUNT,
			// Token: 0x04000FA9 RID: 4009
			AVAILABLE_DIRECTORY_PERCENTAGE_BASE,
			// Token: 0x04000FAA RID: 4010
			AVAILABLE_DIRECTORY_PERCENTAGE,
			// Token: 0x04000FAB RID: 4011
			FLUSHED_ITEM_COUNT,
			// Token: 0x04000FAC RID: 4012
			DROPPED_ITEM_COUNT,
			// Token: 0x04000FAD RID: 4013
			WB_QUEUE_ITEM_COUNT,
			// Token: 0x04000FAE RID: 4014
			TOTAL_AVAILABLE_WBITEM_COUNT,
			// Token: 0x04000FAF RID: 4015
			TOTAL_ALLOCATED_WBITEM_COUNT,
			// Token: 0x04000FB0 RID: 4016
			AVAILABLE_WBITEM_PERCENTAGE_BASE,
			// Token: 0x04000FB1 RID: 4017
			AVAILABLE_WBITEM_PERCENTAGE,
			// Token: 0x04000FB2 RID: 4018
			RT_PENDING_READS,
			// Token: 0x04000FB3 RID: 4019
			RT_SUCCESSFUL_READS,
			// Token: 0x04000FB4 RID: 4020
			RT_MISSED_READS,
			// Token: 0x04000FB5 RID: 4021
			RT_FAILED_READS,
			// Token: 0x04000FB6 RID: 4022
			GATEWAY_FAILURE_PERCENTAGE_BASE,
			// Token: 0x04000FB7 RID: 4023
			GATEWAY_FAILURE_PERCENTAGE,
			// Token: 0x04000FB8 RID: 4024
			GATEWAY_REQUEST_PROCESS_TIME_BASE,
			// Token: 0x04000FB9 RID: 4025
			GATEWAY_REQUEST_PROCESS_TIME,
			// Token: 0x04000FBA RID: 4026
			PERCENTAGE_RETRY_ERRORS_BASE,
			// Token: 0x04000FBB RID: 4027
			PERCENTAGE_RETRY_ERRORS,
			// Token: 0x04000FBC RID: 4028
			TIME_SINCE_GRACEFUL_SHUTDOWN_START,
			// Token: 0x04000FBD RID: 4029
			TOTAL_CONNECTIONS_COUNT,
			// Token: 0x04000FBE RID: 4030
			THROTTLED_CONNECTIONS_COUNT,
			// Token: 0x04000FBF RID: 4031
			MAX_COUNTER
		}
	}
}
