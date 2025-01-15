using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002EB RID: 747
	internal abstract class PerfCounter
	{
		// Token: 0x06001BFD RID: 7165 RVA: 0x0005426A File Offset: 0x0005246A
		protected PerfCounter()
			: this(true)
		{
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00054273 File Offset: 0x00052473
		protected PerfCounter(bool toRegister)
		{
			if (toRegister)
			{
				PerformanceMonitorCounter.Register(this);
			}
			this._registered = toRegister;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x0005428B File Offset: 0x0005248B
		protected void Delete()
		{
			if (this._registered)
			{
				PerformanceMonitorCounter.UnRegister(this);
			}
		}

		// Token: 0x06001C00 RID: 7168
		internal abstract void Update();

		// Token: 0x06001C01 RID: 7169 RVA: 0x0005429C File Offset: 0x0005249C
		protected static bool HandleCounterSetException(Exception e)
		{
			Type type = e.GetType();
			return type == typeof(ArgumentException) || type == typeof(InsufficientMemoryException) || type == typeof(Win32Exception) || type == typeof(PlatformNotSupportedException);
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000542FC File Offset: 0x000524FC
		protected static CounterSet InitializeCounterSetCommon(ref int callOnce, Guid counterSetGuid, CounterSetInstanceType instanceType, CounterTypeIDMap[] map, string counterName)
		{
			if (Interlocked.CompareExchange(ref callOnce, 1, 0) == 0)
			{
				CounterSet counterSet;
				try
				{
					counterSet = new CounterSet(PerfCounter.ProviderGuid, counterSetGuid, instanceType);
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose(PerfCounter.LogSource, "Counter set is created : " + counterName);
					}
				}
				catch (Exception ex)
				{
					if (Provider.IsEnabled(TraceLevel.Error))
					{
						EventLogWriter.WriteError(PerfCounter.LogSource, "{0} InitializeCounterSet() failed. {1} ", new object[] { counterName, ex });
					}
					if (!PerfCounter.HandleCounterSetException(ex))
					{
						throw;
					}
					return null;
				}
				foreach (CounterTypeIDMap counterTypeIDMap in map)
				{
					counterSet.AddCounter(counterTypeIDMap.ID, counterTypeIDMap.Type);
				}
				return counterSet;
			}
			return null;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x000543CC File Offset: 0x000525CC
		private static long PerfCounterDefaultValue()
		{
			return 0L;
		}

		// Token: 0x04000ECC RID: 3788
		protected static readonly PerfCounterValue DefaultValue = new PerfCounterValue(PerfCounter.PerfCounterDefaultValue);

		// Token: 0x04000ECD RID: 3789
		internal static readonly Guid ProviderGuid = new Guid("{6B09CE88-F32A-40ed-9AF8-26BB292DFB3F}");

		// Token: 0x04000ECE RID: 3790
		private bool _registered;

		// Token: 0x04000ECF RID: 3791
		internal static string LogSource = "DistributedCache.PerformanceCounter";
	}
}
