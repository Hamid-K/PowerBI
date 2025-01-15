using System;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000310 RID: 784
	internal abstract class DistributedCachePerSecodaryMachineCounter : PerfCounter, IDisposable
	{
		// Token: 0x06001CB5 RID: 7349 RVA: 0x0005737E File Offset: 0x0005557E
		protected DistributedCachePerSecodaryMachineCounter(DistributedCachePerSecodaryMachineCounter.Name name, string instanceName)
			: this(name, instanceName, true)
		{
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0005738C File Offset: 0x0005558C
		protected DistributedCachePerSecodaryMachineCounter(DistributedCachePerSecodaryMachineCounter.Name name, string instanceName, bool toRegister)
			: base(toRegister)
		{
			if (DistributedCachePerSecodaryMachineCounter.CounterSet != null)
			{
				CounterSetInstance counterSetInstance = DistributedCachePerSecodaryMachineCounter.CounterSetInstanceContainer.Get(instanceName) as CounterSetInstance;
				if (counterSetInstance == null)
				{
					counterSetInstance = DistributedCachePerSecodaryMachineCounter.CounterSetInstanceContainer.AddOrGet(instanceName, new DistributedCachePerSecodaryMachineCounter.MachineInstanceCreator(instanceName)) as CounterSetInstance;
				}
				if (counterSetInstance != null)
				{
					int counterID = DistributedCachePerSecodaryMachineCounter.GetCounterID(name);
					this._counter = counterSetInstance.Counters[counterID];
					return;
				}
				this.Delete();
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(PerfCounter.LogSource, "Failed to create counter Category = {0}: CounterName = {1} Instance{2} instance = null", new object[]
					{
						DistributedCachePerSecodaryMachineCounter.GetCategory(),
						DistributedCachePerSecodaryMachineCounter.GetCounterName(name),
						instanceName
					});
					return;
				}
			}
			else
			{
				this.Delete();
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(PerfCounter.LogSource, "Failed to create counter Category = {0}: CounterName = {1} Instance{2} CounterSet = null", new object[]
					{
						DistributedCachePerSecodaryMachineCounter.GetCategory(),
						DistributedCachePerSecodaryMachineCounter.GetCounterName(name),
						instanceName
					});
				}
			}
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x00057467 File Offset: 0x00055667
		private static string GetCategory()
		{
			return ConfigManager.PerSecondaryCounterCategory;
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0005746E File Offset: 0x0005566E
		internal new void Delete()
		{
			base.Delete();
			if (this._counter != null)
			{
				this._counter = null;
			}
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x00057488 File Offset: 0x00055688
		internal override void Update()
		{
			if (this._counter != null)
			{
				try
				{
					this._counter.Value = this.GetValue();
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
						EventLogWriter.WriteError(PerfCounter.LogSource, "counter updattion failed Category = {0}. {1}", new object[]
						{
							DistributedCachePerSecodaryMachineCounter.GetCategory(),
							ex
						});
					}
				}
			}
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x00057500 File Offset: 0x00055700
		internal static int GetCounterID(DistributedCachePerSecodaryMachineCounter.Name name)
		{
			return DistributedCachePerSecodaryMachineCounter.Map[(int)name].ID;
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x00057512 File Offset: 0x00055712
		internal static string GetCounterName(DistributedCachePerSecodaryMachineCounter.Name name)
		{
			return DistributedCachePerSecodaryMachineCounter.Map[(int)name].Name;
		}

		// Token: 0x06001CBC RID: 7356
		internal abstract long GetValue();

		// Token: 0x06001CBD RID: 7357 RVA: 0x00057524 File Offset: 0x00055724
		private static CounterSetInstance GetInstance(string instanceName)
		{
			if (DistributedCachePerSecodaryMachineCounter.CounterSet != null)
			{
				try
				{
					return DistributedCachePerSecodaryMachineCounter.CounterSet.CreateCounterSetInstance(instanceName);
				}
				catch (ArgumentException ex)
				{
					if (Provider.IsEnabled(TraceLevel.Error))
					{
						EventLogWriter.WriteError(PerfCounter.LogSource, "Instance creation failed failed Category = {0} instance name = {1} Error{2}", new object[]
						{
							DistributedCachePerSecodaryMachineCounter.GetCategory(),
							instanceName,
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
							DistributedCachePerSecodaryMachineCounter.GetCategory(),
							instanceName,
							ex2.ToString()
						});
					}
				}
			}
			return null;
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x000575E4 File Offset: 0x000557E4
		public void Dispose()
		{
			this._counter = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x000575F4 File Offset: 0x000557F4
		internal static bool InitializeCounterSet()
		{
			CounterSet counterSet = PerfCounter.InitializeCounterSetCommon(ref DistributedCachePerSecodaryMachineCounter.CallOnce, DistributedCachePerSecodaryMachineCounter.CounterSetGuid, CounterSetInstanceType.Multiple, DistributedCachePerSecodaryMachineCounter.Map, DistributedCachePerSecodaryMachineCounter.GetCategory());
			if (counterSet != null)
			{
				DistributedCachePerSecodaryMachineCounter.CounterSet = counterSet;
			}
			return counterSet != null;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0005762C File Offset: 0x0005582C
		internal static bool IsInstalled()
		{
			return PerformanceCounterCategory.Exists(ConfigManager.PerSecondaryCounterCategory);
		}

		// Token: 0x04000FC8 RID: 4040
		internal static readonly Guid CounterSetGuid = new Guid("{d2c8bc17-4226-4886-a39c-6eb9459b52b8}");

		// Token: 0x04000FC9 RID: 4041
		private static CounterSet CounterSet;

		// Token: 0x04000FCA RID: 4042
		private static int CallOnce;

		// Token: 0x04000FCB RID: 4043
		private static BaseHashTable CounterSetInstanceContainer = new BaseHashTable(new ObjectDirectoryNodeFactory());

		// Token: 0x04000FCC RID: 4044
		private CounterData _counter;

		// Token: 0x04000FCD RID: 4045
		private static CounterTypeIDMap[] Map = new CounterTypeIDMap[]
		{
			new CounterTypeIDMap(ConfigManager.PerSecondaryTotalReplicationRetries, CounterType.RawData64, 0)
		};

		// Token: 0x02000311 RID: 785
		internal enum Name
		{
			// Token: 0x04000FCF RID: 4047
			TOTAL_REPLICATION_RETRY,
			// Token: 0x04000FD0 RID: 4048
			MAX_COUNTER
		}

		// Token: 0x02000312 RID: 786
		private class MachineInstanceCreator : IObjectCreator
		{
			// Token: 0x06001CC2 RID: 7362 RVA: 0x0005768C File Offset: 0x0005588C
			public MachineInstanceCreator(string instanceName)
			{
				this._instanceName = instanceName;
			}

			// Token: 0x06001CC3 RID: 7363 RVA: 0x0005769B File Offset: 0x0005589B
			public object GetObject()
			{
				return DistributedCachePerSecodaryMachineCounter.GetInstance(this._instanceName);
			}

			// Token: 0x04000FD1 RID: 4049
			private string _instanceName;
		}
	}
}
