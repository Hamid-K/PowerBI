using System;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002EC RID: 748
	internal abstract class CachePerfCounter : PerfCounter
	{
		// Token: 0x06001C05 RID: 7173 RVA: 0x000543FC File Offset: 0x000525FC
		protected CachePerfCounter(CachePerfCounter.Name name, string instanceName)
		{
			try
			{
				if (CachePerfCounter.CounterSet != null)
				{
					CounterSetInstance counterSetInstance = CachePerfCounter.CounterSetInstanceContainer.Get(instanceName) as CounterSetInstance;
					if (counterSetInstance != null)
					{
						int counterID = CachePerfCounter.GetCounterID(name);
						this._counter = counterSetInstance.Counters[counterID];
						if (Provider.IsEnabled(TraceLevel.Verbose))
						{
							EventLogWriter.WriteVerbose<string, string, string>(PerfCounter.LogSource, "counter created  Category = {0}: CounterName = {1} Instance{2} instance = null", CachePerfCounter.GetCategory(), CachePerfCounter.GetCounterName(name), instanceName);
						}
					}
					else
					{
						this.Delete();
						if (Provider.IsEnabled(TraceLevel.Error))
						{
							EventLogWriter.WriteError(PerfCounter.LogSource, "Failed to create counter Category = {0}: CounterName = {1} Instance{2} instance = null", new object[]
							{
								CachePerfCounter.GetCategory(),
								CachePerfCounter.GetCounterName(name),
								instanceName
							});
						}
					}
				}
				else
				{
					this.Delete();
					if (Provider.IsEnabled(TraceLevel.Error))
					{
						EventLogWriter.WriteError(PerfCounter.LogSource, "Failed to create counter Category = {0}: CounterName = {1} Instance{2} CounterSet = null", new object[]
						{
							CachePerfCounter.GetCategory(),
							CachePerfCounter.GetCounterName(name),
							instanceName
						});
					}
				}
			}
			catch (Exception ex)
			{
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(PerfCounter.LogSource, "Failed to create counter Category = {0}: CounterName = {1} Instance{2} Message = {3}", new object[]
					{
						CachePerfCounter.GetCategory(),
						CachePerfCounter.GetCounterName(name),
						instanceName,
						ex.Message
					});
				}
				if (!PerformanceMonitorCounter.CheckException(ex))
				{
					throw;
				}
				this.Delete();
			}
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x00054554 File Offset: 0x00052754
		private static string GetCategory()
		{
			return ConfigManager.CacheCounterCategory;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x0005455B File Offset: 0x0005275B
		internal new void Delete()
		{
			base.Delete();
			this._counter = null;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x0005456C File Offset: 0x0005276C
		internal override void Update()
		{
			CounterData counter = this._counter;
			if (counter != null)
			{
				try
				{
					counter.Value = this.GetValue();
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
						EventLogWriter.WriteError(PerfCounter.LogSource, "counter updation failed Category = {0} ", new object[] { CachePerfCounter.GetCategory() });
					}
				}
			}
		}

		// Token: 0x06001C09 RID: 7177
		internal abstract long GetValue();

		// Token: 0x06001C0A RID: 7178 RVA: 0x000545DC File Offset: 0x000527DC
		internal static void RemoveInstance(string instanceName)
		{
			if (CachePerfCounter.CounterSet != null)
			{
				CounterSetInstance counterSetInstance = CachePerfCounter.CounterSetInstanceContainer.Delete(instanceName) as CounterSetInstance;
				if (counterSetInstance != null)
				{
					counterSetInstance.Dispose();
					return;
				}
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(PerfCounter.LogSource, "RemoveInstance failed Category = {0} instance name = {1} ", new object[]
					{
						CachePerfCounter.GetCategory(),
						instanceName
					});
				}
			}
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00054638 File Offset: 0x00052838
		internal static bool CreateInstance(string instanceName)
		{
			if (CachePerfCounter.CounterSet != null)
			{
				CounterSetInstance counterSetInstance;
				try
				{
					counterSetInstance = CachePerfCounter.CounterSet.CreateCounterSetInstance(instanceName);
				}
				catch (ArgumentException ex)
				{
					if (Provider.IsEnabled(TraceLevel.Error))
					{
						EventLogWriter.WriteError(PerfCounter.LogSource, "Instance creation failed Category = {0} instance name = {1} Error{2}", new object[]
						{
							CachePerfCounter.GetCategory(),
							instanceName,
							ex.ToString()
						});
					}
					return false;
				}
				catch (InvalidOperationException ex2)
				{
					if (Provider.IsEnabled(TraceLevel.Error))
					{
						EventLogWriter.WriteError(PerfCounter.LogSource, "Instance creation failed failed Category = {0} instance name = {1} Error{2}", new object[]
						{
							CachePerfCounter.GetCategory(),
							instanceName,
							ex2.ToString()
						});
					}
					return false;
				}
				CachePerfCounter.CounterSetInstanceContainer.Add(instanceName, counterSetInstance);
				return true;
			}
			return false;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x00054708 File Offset: 0x00052908
		internal static bool InitializeCounterSet()
		{
			CounterSet counterSet = PerfCounter.InitializeCounterSetCommon(ref CachePerfCounter.CallOnce, CachePerfCounter.CounterSetGuid, CounterSetInstanceType.Multiple, CachePerfCounter.Map, CachePerfCounter.GetCategory());
			if (counterSet != null)
			{
				CachePerfCounter.CounterSet = counterSet;
			}
			return counterSet != null;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00054740 File Offset: 0x00052940
		internal static bool IsInstalled()
		{
			return PerformanceCounterCategory.Exists(ConfigManager.CacheCounterCategory);
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x0005474C File Offset: 0x0005294C
		internal static int GetCounterID(CachePerfCounter.Name name)
		{
			return CachePerfCounter.Map[(int)name].ID;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x0005475E File Offset: 0x0005295E
		internal static string GetCounterName(CachePerfCounter.Name name)
		{
			return CachePerfCounter.Map[(int)name].Name;
		}

		// Token: 0x04000ED0 RID: 3792
		internal static readonly Guid CounterSetGuid = new Guid("{86bc60eb-5e2f-4002-97a1-dc0f0209ee03}");

		// Token: 0x04000ED1 RID: 3793
		private static CounterSet CounterSet;

		// Token: 0x04000ED2 RID: 3794
		private static BaseHashTable CounterSetInstanceContainer = new BaseHashTable(new ObjectDirectoryNodeFactory());

		// Token: 0x04000ED3 RID: 3795
		private static int CallOnce;

		// Token: 0x04000ED4 RID: 3796
		private CounterData _counter;

		// Token: 0x04000ED5 RID: 3797
		private static CounterTypeIDMap[] Map = new CounterTypeIDMap[]
		{
			new CounterTypeIDMap(ConfigManager.CacheTotalDataSize, CounterType.RawData64, 0),
			new CounterTypeIDMap(ConfigManager.CacheTotalMissCount, CounterType.RawData64, 4),
			new CounterTypeIDMap(ConfigManager.CacheTotalMissCountPerSecond, CounterType.RateOfCountPerSecond64, 5),
			new CounterTypeIDMap(ConfigManager.CachePercentageMiss, CounterType.RawFraction64, 7),
			new CounterTypeIDMap(ConfigManager.CachePercentageMissBase, CounterType.RawBase64, 6),
			new CounterTypeIDMap(ConfigManager.CacheTotalReadRequest, CounterType.RawData64, 8),
			new CounterTypeIDMap(ConfigManager.CacheTotalReadRequestPerSecond, CounterType.RateOfCountPerSecond64, 9),
			new CounterTypeIDMap(ConfigManager.CacheTotalWriteRequest, CounterType.RawData64, 12),
			new CounterTypeIDMap(ConfigManager.CacheTotalWriteRequestPerSecond, CounterType.RateOfCountPerSecond64, 13),
			new CounterTypeIDMap(ConfigManager.CacheTotalObjectReturned, CounterType.RawData64, 10),
			new CounterTypeIDMap(ConfigManager.CacheTotalObjectReturnedPerSec, CounterType.RateOfCountPerSecond64, 11),
			new CounterTypeIDMap(ConfigManager.CacheTotalGetAndLockRequest, CounterType.RawData64, 16),
			new CounterTypeIDMap(ConfigManager.CacheTotalGetAndLockRequestPerSecond, CounterType.RateOfCountPerSecond64, 17),
			new CounterTypeIDMap(ConfigManager.CacheTotalSuccessfulGetAndLockRequest, CounterType.RawData64, 18),
			new CounterTypeIDMap(ConfigManager.CacheTotalSuccessfulGetAndLockRequestPerSecond, CounterType.RateOfCountPerSecond64, 19),
			new CounterTypeIDMap(ConfigManager.CacheTotalRequest, CounterType.RawData64, 14),
			new CounterTypeIDMap(ConfigManager.CacheTotalRequestPerSecond, CounterType.RateOfCountPerSecond64, 15),
			new CounterTypeIDMap(ConfigManager.CacheTotalObjectCount, CounterType.RawData64, 1),
			new CounterTypeIDMap(ConfigManager.CacheTotalPrimaryDataSize, CounterType.RawData64, 2),
			new CounterTypeIDMap(ConfigManager.CacheTotalSecodaryDataSize, CounterType.RawData64, 3),
			new CounterTypeIDMap(ConfigManager.CacheWBFlushedItemCount, CounterType.RawData64, 20),
			new CounterTypeIDMap(ConfigManager.CacheWBDroppedItemCount, CounterType.RawData64, 21),
			new CounterTypeIDMap(ConfigManager.CacheWBQueueCount, CounterType.RawData64, 22),
			new CounterTypeIDMap(ConfigManager.CacheRTPendingCount, CounterType.RawData64, 23),
			new CounterTypeIDMap(ConfigManager.CacheRTSuccessCount, CounterType.RawData64, 24),
			new CounterTypeIDMap(ConfigManager.CacheRTMissingCount, CounterType.RawData64, 25),
			new CounterTypeIDMap(ConfigManager.CacheRTErrorCount, CounterType.RawData64, 26),
			new CounterTypeIDMap(ConfigManager.CacheTotalEvictionRuns, CounterType.RawData64, 27)
		};

		// Token: 0x020002ED RID: 749
		internal enum Name
		{
			// Token: 0x04000ED7 RID: 3799
			TOTAL_DATA_SIZE,
			// Token: 0x04000ED8 RID: 3800
			TOTAL_MISS_COUNT,
			// Token: 0x04000ED9 RID: 3801
			TOTAL_MISS_COUNT_PER_SEC,
			// Token: 0x04000EDA RID: 3802
			PERCENTAGE_MISS,
			// Token: 0x04000EDB RID: 3803
			PERCENTAGE_MISS_BASE,
			// Token: 0x04000EDC RID: 3804
			TOTAL_READ_REQUEST,
			// Token: 0x04000EDD RID: 3805
			TOTAL_READ_REQUEST_PER_SEC,
			// Token: 0x04000EDE RID: 3806
			TOTAL_WRITE_REQUEST,
			// Token: 0x04000EDF RID: 3807
			TOTAL_WRITE_REQUEST_PER_SEC,
			// Token: 0x04000EE0 RID: 3808
			TOTAL_OBJECTS_RETURNED,
			// Token: 0x04000EE1 RID: 3809
			TOTAL_OBJECTS_RETURNED_PER_SEC,
			// Token: 0x04000EE2 RID: 3810
			TOTAL_GETANDLOCK_REQUEST,
			// Token: 0x04000EE3 RID: 3811
			TOTAL_GETANDLOCK_REQUEST_PER_SEC,
			// Token: 0x04000EE4 RID: 3812
			TOTAL_SUCCESSFULGETANDLOCK_REQUEST,
			// Token: 0x04000EE5 RID: 3813
			TOTAL_SUCCESSFULGETANDLOCK_REQUEST_PER_SEC,
			// Token: 0x04000EE6 RID: 3814
			TOTAL_REQUEST,
			// Token: 0x04000EE7 RID: 3815
			TOTAL_REQUEST_PER_SEC,
			// Token: 0x04000EE8 RID: 3816
			TOTAL_OBJECT_COUNT,
			// Token: 0x04000EE9 RID: 3817
			TOTAL_PRIMARY_DATA_SIZE,
			// Token: 0x04000EEA RID: 3818
			TOTAL_SECONDARY_DATA_SIZE,
			// Token: 0x04000EEB RID: 3819
			FLUSHED_ITEM_COUNT,
			// Token: 0x04000EEC RID: 3820
			DROPPED_ITEM_COUNT,
			// Token: 0x04000EED RID: 3821
			WB_QUEUE_ITEM_COUNT,
			// Token: 0x04000EEE RID: 3822
			RT_PENDING_READS,
			// Token: 0x04000EEF RID: 3823
			RT_SUCCESSFUL_READS,
			// Token: 0x04000EF0 RID: 3824
			RT_MISSED_READS,
			// Token: 0x04000EF1 RID: 3825
			RT_FAILED_READS,
			// Token: 0x04000EF2 RID: 3826
			TOTAL_EVICTION_RUNS,
			// Token: 0x04000EF3 RID: 3827
			MAX_COUNTER
		}
	}
}
