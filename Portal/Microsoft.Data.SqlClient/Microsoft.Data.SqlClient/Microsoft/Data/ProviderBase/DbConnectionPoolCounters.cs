using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using Microsoft.Data.Common;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x02000173 RID: 371
	internal abstract class DbConnectionPoolCounters
	{
		// Token: 0x06001B8C RID: 7052 RVA: 0x00070EF1 File Offset: 0x0006F0F1
		protected DbConnectionPoolCounters()
			: this(null, null)
		{
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x00070EFC File Offset: 0x0006F0FC
		protected DbConnectionPoolCounters(string categoryName, string categoryHelp)
		{
			AppDomain.CurrentDomain.DomainUnload += this.UnloadEventHandler;
			AppDomain.CurrentDomain.ProcessExit += this.ExitEventHandler;
			AppDomain.CurrentDomain.UnhandledException += this.ExceptionEventHandler;
			string text = null;
			if (!ADP.IsEmpty(categoryName) && ADP.s_isPlatformNT5)
			{
				text = this.GetInstanceName();
			}
			this.HardConnectsPerSecond = new DbConnectionPoolCounters.Counter(categoryName, text, DbConnectionPoolCounters.CreationData.HardConnectsPerSecond.CounterName, DbConnectionPoolCounters.CreationData.HardConnectsPerSecond.CounterType);
			this.HardDisconnectsPerSecond = new DbConnectionPoolCounters.Counter(categoryName, text, DbConnectionPoolCounters.CreationData.HardDisconnectsPerSecond.CounterName, DbConnectionPoolCounters.CreationData.HardDisconnectsPerSecond.CounterType);
			this.NumberOfNonPooledConnections = new DbConnectionPoolCounters.Counter(categoryName, text, DbConnectionPoolCounters.CreationData.NumberOfNonPooledConnections.CounterName, DbConnectionPoolCounters.CreationData.NumberOfNonPooledConnections.CounterType);
			this.NumberOfPooledConnections = new DbConnectionPoolCounters.Counter(categoryName, text, DbConnectionPoolCounters.CreationData.NumberOfPooledConnections.CounterName, DbConnectionPoolCounters.CreationData.NumberOfPooledConnections.CounterType);
			this.NumberOfActiveConnectionPoolGroups = new DbConnectionPoolCounters.Counter(categoryName, text, DbConnectionPoolCounters.CreationData.NumberOfActiveConnectionPoolGroups.CounterName, DbConnectionPoolCounters.CreationData.NumberOfActiveConnectionPoolGroups.CounterType);
			this.NumberOfInactiveConnectionPoolGroups = new DbConnectionPoolCounters.Counter(categoryName, text, DbConnectionPoolCounters.CreationData.NumberOfInactiveConnectionPoolGroups.CounterName, DbConnectionPoolCounters.CreationData.NumberOfInactiveConnectionPoolGroups.CounterType);
			this.NumberOfActiveConnectionPools = new DbConnectionPoolCounters.Counter(categoryName, text, DbConnectionPoolCounters.CreationData.NumberOfActiveConnectionPools.CounterName, DbConnectionPoolCounters.CreationData.NumberOfActiveConnectionPools.CounterType);
			this.NumberOfInactiveConnectionPools = new DbConnectionPoolCounters.Counter(categoryName, text, DbConnectionPoolCounters.CreationData.NumberOfInactiveConnectionPools.CounterName, DbConnectionPoolCounters.CreationData.NumberOfInactiveConnectionPools.CounterType);
			this.NumberOfStasisConnections = new DbConnectionPoolCounters.Counter(categoryName, text, DbConnectionPoolCounters.CreationData.NumberOfStasisConnections.CounterName, DbConnectionPoolCounters.CreationData.NumberOfStasisConnections.CounterType);
			this.NumberOfReclaimedConnections = new DbConnectionPoolCounters.Counter(categoryName, text, DbConnectionPoolCounters.CreationData.NumberOfReclaimedConnections.CounterName, DbConnectionPoolCounters.CreationData.NumberOfReclaimedConnections.CounterType);
			string text2 = null;
			if (!ADP.IsEmpty(categoryName))
			{
				TraceSwitch traceSwitch = new TraceSwitch("ConnectionPoolPerformanceCounterDetail", "level of detail to track with connection pool performance counters");
				if (TraceLevel.Verbose == traceSwitch.Level)
				{
					text2 = categoryName;
				}
			}
			this.SoftConnectsPerSecond = new DbConnectionPoolCounters.Counter(text2, text, DbConnectionPoolCounters.CreationData.SoftConnectsPerSecond.CounterName, DbConnectionPoolCounters.CreationData.SoftConnectsPerSecond.CounterType);
			this.SoftDisconnectsPerSecond = new DbConnectionPoolCounters.Counter(text2, text, DbConnectionPoolCounters.CreationData.SoftDisconnectsPerSecond.CounterName, DbConnectionPoolCounters.CreationData.SoftDisconnectsPerSecond.CounterType);
			this.NumberOfActiveConnections = new DbConnectionPoolCounters.Counter(text2, text, DbConnectionPoolCounters.CreationData.NumberOfActiveConnections.CounterName, DbConnectionPoolCounters.CreationData.NumberOfActiveConnections.CounterType);
			this.NumberOfFreeConnections = new DbConnectionPoolCounters.Counter(text2, text, DbConnectionPoolCounters.CreationData.NumberOfFreeConnections.CounterName, DbConnectionPoolCounters.CreationData.NumberOfFreeConnections.CounterType);
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00071160 File Offset: 0x0006F360
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private string GetAssemblyName()
		{
			string text = null;
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			if (null != entryAssembly)
			{
				AssemblyName name = entryAssembly.GetName();
				if (name != null)
				{
					text = name.Name;
				}
			}
			return text;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x00071190 File Offset: 0x0006F390
		private string GetInstanceName()
		{
			string text = this.GetAssemblyName();
			if (ADP.IsEmpty(text))
			{
				AppDomain currentDomain = AppDomain.CurrentDomain;
				if (currentDomain != null)
				{
					text = currentDomain.FriendlyName;
				}
			}
			int currentProcessId = SafeNativeMethods.GetCurrentProcessId();
			string text2 = string.Format(null, "{0}[{1}]", text, currentProcessId);
			text2 = text2.Replace('(', '[').Replace(')', ']').Replace('#', '_')
				.Replace('/', '_')
				.Replace('\\', '_');
			if (text2.Length > 127)
			{
				int num = (127 - "[...]".Length) / 2;
				int num2 = 127 - num - "[...]".Length;
				text2 = string.Format(null, "{0}{1}{2}", text2.Substring(0, num), "[...]", text2.Substring(text2.Length - num2, num2));
			}
			return text2;
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00071260 File Offset: 0x0006F460
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void Dispose()
		{
			this.SafeDispose(this.HardConnectsPerSecond);
			this.SafeDispose(this.HardDisconnectsPerSecond);
			this.SafeDispose(this.SoftConnectsPerSecond);
			this.SafeDispose(this.SoftDisconnectsPerSecond);
			this.SafeDispose(this.NumberOfNonPooledConnections);
			this.SafeDispose(this.NumberOfPooledConnections);
			this.SafeDispose(this.NumberOfActiveConnectionPoolGroups);
			this.SafeDispose(this.NumberOfInactiveConnectionPoolGroups);
			this.SafeDispose(this.NumberOfActiveConnectionPools);
			this.SafeDispose(this.NumberOfActiveConnections);
			this.SafeDispose(this.NumberOfFreeConnections);
			this.SafeDispose(this.NumberOfStasisConnections);
			this.SafeDispose(this.NumberOfReclaimedConnections);
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x00071309 File Offset: 0x0006F509
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private void SafeDispose(DbConnectionPoolCounters.Counter counter)
		{
			if (counter != null)
			{
				counter.Dispose();
			}
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00071314 File Offset: 0x0006F514
		[PrePrepareMethod]
		private void ExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
		{
			if (e != null && e.IsTerminating)
			{
				this.Dispose();
			}
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x00071327 File Offset: 0x0006F527
		[PrePrepareMethod]
		private void ExitEventHandler(object sender, EventArgs e)
		{
			this.Dispose();
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x00071327 File Offset: 0x0006F527
		[PrePrepareMethod]
		private void UnloadEventHandler(object sender, EventArgs e)
		{
			this.Dispose();
		}

		// Token: 0x04000B35 RID: 2869
		private const int CounterInstanceNameMaxLength = 127;

		// Token: 0x04000B36 RID: 2870
		internal readonly DbConnectionPoolCounters.Counter HardConnectsPerSecond;

		// Token: 0x04000B37 RID: 2871
		internal readonly DbConnectionPoolCounters.Counter HardDisconnectsPerSecond;

		// Token: 0x04000B38 RID: 2872
		internal readonly DbConnectionPoolCounters.Counter SoftConnectsPerSecond;

		// Token: 0x04000B39 RID: 2873
		internal readonly DbConnectionPoolCounters.Counter SoftDisconnectsPerSecond;

		// Token: 0x04000B3A RID: 2874
		internal readonly DbConnectionPoolCounters.Counter NumberOfNonPooledConnections;

		// Token: 0x04000B3B RID: 2875
		internal readonly DbConnectionPoolCounters.Counter NumberOfPooledConnections;

		// Token: 0x04000B3C RID: 2876
		internal readonly DbConnectionPoolCounters.Counter NumberOfActiveConnectionPoolGroups;

		// Token: 0x04000B3D RID: 2877
		internal readonly DbConnectionPoolCounters.Counter NumberOfInactiveConnectionPoolGroups;

		// Token: 0x04000B3E RID: 2878
		internal readonly DbConnectionPoolCounters.Counter NumberOfActiveConnectionPools;

		// Token: 0x04000B3F RID: 2879
		internal readonly DbConnectionPoolCounters.Counter NumberOfInactiveConnectionPools;

		// Token: 0x04000B40 RID: 2880
		internal readonly DbConnectionPoolCounters.Counter NumberOfActiveConnections;

		// Token: 0x04000B41 RID: 2881
		internal readonly DbConnectionPoolCounters.Counter NumberOfFreeConnections;

		// Token: 0x04000B42 RID: 2882
		internal readonly DbConnectionPoolCounters.Counter NumberOfStasisConnections;

		// Token: 0x04000B43 RID: 2883
		internal readonly DbConnectionPoolCounters.Counter NumberOfReclaimedConnections;

		// Token: 0x02000274 RID: 628
		private static class CreationData
		{
			// Token: 0x0400175A RID: 5978
			internal static readonly CounterCreationData HardConnectsPerSecond = new CounterCreationData("HardConnectsPerSecond", "The number of actual connections per second that are being made to servers", PerformanceCounterType.RateOfCountsPerSecond32);

			// Token: 0x0400175B RID: 5979
			internal static readonly CounterCreationData HardDisconnectsPerSecond = new CounterCreationData("HardDisconnectsPerSecond", "The number of actual disconnects per second that are being made to servers", PerformanceCounterType.RateOfCountsPerSecond32);

			// Token: 0x0400175C RID: 5980
			internal static readonly CounterCreationData SoftConnectsPerSecond = new CounterCreationData("SoftConnectsPerSecond", "The number of connections we get from the pool per second", PerformanceCounterType.RateOfCountsPerSecond32);

			// Token: 0x0400175D RID: 5981
			internal static readonly CounterCreationData SoftDisconnectsPerSecond = new CounterCreationData("SoftDisconnectsPerSecond", "The number of connections we return to the pool per second", PerformanceCounterType.RateOfCountsPerSecond32);

			// Token: 0x0400175E RID: 5982
			internal static readonly CounterCreationData NumberOfNonPooledConnections = new CounterCreationData("NumberOfNonPooledConnections", "The number of connections that are not using connection pooling", PerformanceCounterType.NumberOfItems32);

			// Token: 0x0400175F RID: 5983
			internal static readonly CounterCreationData NumberOfPooledConnections = new CounterCreationData("NumberOfPooledConnections", "The number of connections that are managed by the connection pooler", PerformanceCounterType.NumberOfItems32);

			// Token: 0x04001760 RID: 5984
			internal static readonly CounterCreationData NumberOfActiveConnectionPoolGroups = new CounterCreationData("NumberOfActiveConnectionPoolGroups", "The number of unique connection strings", PerformanceCounterType.NumberOfItems32);

			// Token: 0x04001761 RID: 5985
			internal static readonly CounterCreationData NumberOfInactiveConnectionPoolGroups = new CounterCreationData("NumberOfInactiveConnectionPoolGroups", "The number of unique connection strings waiting for pruning", PerformanceCounterType.NumberOfItems32);

			// Token: 0x04001762 RID: 5986
			internal static readonly CounterCreationData NumberOfActiveConnectionPools = new CounterCreationData("NumberOfActiveConnectionPools", "The number of connection pools", PerformanceCounterType.NumberOfItems32);

			// Token: 0x04001763 RID: 5987
			internal static readonly CounterCreationData NumberOfInactiveConnectionPools = new CounterCreationData("NumberOfInactiveConnectionPools", "The number of connection pools", PerformanceCounterType.NumberOfItems32);

			// Token: 0x04001764 RID: 5988
			internal static readonly CounterCreationData NumberOfActiveConnections = new CounterCreationData("NumberOfActiveConnections", "The number of connections currently in-use", PerformanceCounterType.NumberOfItems32);

			// Token: 0x04001765 RID: 5989
			internal static readonly CounterCreationData NumberOfFreeConnections = new CounterCreationData("NumberOfFreeConnections", "The number of connections currently available for use", PerformanceCounterType.NumberOfItems32);

			// Token: 0x04001766 RID: 5990
			internal static readonly CounterCreationData NumberOfStasisConnections = new CounterCreationData("NumberOfStasisConnections", "The number of connections currently waiting to be made ready for use", PerformanceCounterType.NumberOfItems32);

			// Token: 0x04001767 RID: 5991
			internal static readonly CounterCreationData NumberOfReclaimedConnections = new CounterCreationData("NumberOfReclaimedConnections", "The number of connections we reclaim from GC'd external connections", PerformanceCounterType.NumberOfItems32);
		}

		// Token: 0x02000275 RID: 629
		internal sealed class Counter
		{
			// Token: 0x06001F3F RID: 7999 RVA: 0x0007FB08 File Offset: 0x0007DD08
			internal Counter(string categoryName, string instanceName, string counterName, PerformanceCounterType counterType)
			{
				if (ADP.s_isPlatformNT5)
				{
					try
					{
						if (!ADP.IsEmpty(categoryName) && !ADP.IsEmpty(instanceName))
						{
							this._instance = new PerformanceCounter
							{
								CategoryName = categoryName,
								CounterName = counterName,
								InstanceName = instanceName,
								InstanceLifetime = PerformanceCounterInstanceLifetime.Process,
								ReadOnly = false,
								RawValue = 0L
							};
						}
					}
					catch (InvalidOperationException ex)
					{
						ADP.TraceExceptionWithoutRethrow(ex);
					}
				}
			}

			// Token: 0x06001F40 RID: 8000 RVA: 0x0007FB88 File Offset: 0x0007DD88
			internal void Decrement()
			{
				PerformanceCounter instance = this._instance;
				if (instance != null)
				{
					instance.Decrement();
				}
			}

			// Token: 0x06001F41 RID: 8001 RVA: 0x0007FBA8 File Offset: 0x0007DDA8
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			internal void Dispose()
			{
				PerformanceCounter instance = this._instance;
				this._instance = null;
				if (instance != null)
				{
					instance.RemoveInstance();
				}
			}

			// Token: 0x06001F42 RID: 8002 RVA: 0x0007FBCC File Offset: 0x0007DDCC
			internal void Increment()
			{
				PerformanceCounter instance = this._instance;
				if (instance != null)
				{
					instance.Increment();
				}
			}

			// Token: 0x04001768 RID: 5992
			private PerformanceCounter _instance;
		}
	}
}
