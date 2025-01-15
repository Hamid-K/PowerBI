using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Cloud.Platform.ConfigurationClasses.Monitoring;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000077 RID: 119
	internal class HealthStateMachine : IMonitoredEventHandler, IDisposable
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000D8A4 File Offset: 0x0000BAA4
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000D8AC File Offset: 0x0000BAAC
		private ActivityTypeElementIdPairConfigCollection Configuration { get; set; }

		// Token: 0x06000389 RID: 905 RVA: 0x0000D8B8 File Offset: 0x0000BAB8
		internal HealthStateMachine(IMonitoredEventHandler alertIssuer, IMonitoredEventHandler warningIssuer, TimeSpan period, ActivityTypeElementIdPairConfigCollection configuration)
		{
			this.m_lock = new object();
			this.m_alertIssuer = alertIssuer;
			this.m_warningIssuer = warningIssuer;
			this.Configuration = configuration;
			this.m_correlatedFlowErrorState = new HealthStateMachine.HealthStateDictionary<CorrelatedMonitoredErrorEvent>(new HealthStateMachine.CorrelatedMonitoredErrorEventComparer());
			this.m_uncorrelatedFlowErrorState = new HealthStateMachine.HealthStateDictionary<MonitoredFlowErrorEvent>(new HealthStateMachine.MonitoredFlowErrorEventComparer());
			this.m_uncorrelatedLowLevelErrorState = new HealthStateMachine.HealthStateDictionary<MonitoredLowLevelErrorEvent>(new HealthStateMachine.MonitoredLowLevelErrorEventComparer());
			this.m_flowSuccessCounters = new CountedHashSet<MonitoredFlowSuccessEvent>(new HealthStateMachine.MonitoredFlowSuccessComparer());
			this.m_uncorrelatedFlowErrorCounters = new CountedHashSet<MonitoredFlowErrorEvent>(new HealthStateMachine.MonitoredFlowErrorEventComparer());
			this.m_correlatedFlowErrorCounters = new CountedHashSet<CorrelatedMonitoredErrorEvent>(new HealthStateMachine.CorrelatedMonitoredErrorEventComparer());
			this.m_uncorrelatedLowLevelErrorCounters = new CountedHashSet<MonitoredLowLevelErrorEvent>(new HealthStateMachine.MonitoredLowLevelErrorEventComparer());
			this.m_timerTicksCounter = new MonitoringTimerTicksCounter(configuration);
			this.m_timerFactory = new TimerFactory("Monitoring.HealthStateMachine.TimerFactory", TimerCreationFlags.Crash);
			this.m_timerFactory.SchedulePeriodicTimer("Monitoring.HealthStateMachine.Timer", -1, new TimerCallback(this.TimerCallback), this).UpdatePeriod((int)period.TotalMilliseconds);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000D9A8 File Offset: 0x0000BBA8
		private void TimerCallback(object source)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (this.m_stopFlag)
				{
					TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Timer CB called in Monitoring Health state machine while disposing, so return after doing nothing");
				}
				else
				{
					this.m_timerTicksCounter.Tick();
					this.IssueAlerts<MonitoredFlowErrorEvent>(this.m_uncorrelatedFlowErrorCounters, this.m_uncorrelatedFlowErrorState);
					this.UpdateHealthyState<MonitoredFlowErrorEvent>(this.m_uncorrelatedFlowErrorCounters, this.m_uncorrelatedFlowErrorState);
					this.IssueAlerts<CorrelatedMonitoredErrorEvent>(this.m_correlatedFlowErrorCounters, this.m_correlatedFlowErrorState);
					this.UpdateHealthyState<CorrelatedMonitoredErrorEvent>(this.m_correlatedFlowErrorCounters, this.m_correlatedFlowErrorState);
					this.UpdateHealthyState<MonitoredLowLevelErrorEvent>(this.m_uncorrelatedLowLevelErrorCounters, this.m_uncorrelatedLowLevelErrorState);
					this.ClearCounters();
					this.m_alertIssuer.OnBatchCompleted();
				}
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000DA78 File Offset: 0x0000BC78
		private void IssueAlerts<TMonitoredError>(CountedHashSet<TMonitoredError> countedHashSet, HealthStateMachine.HealthStateDictionary<TMonitoredError> healthStateDictionary) where TMonitoredError : IPerElementActivityType, IMonitoredEventHandlerVisitor
		{
			foreach (TMonitoredError tmonitoredError in countedHashSet)
			{
				if (this.m_timerTicksCounter.IsTriggered(tmonitoredError.PerElementActivityType))
				{
					int num = countedHashSet.CountInstances(tmonitoredError);
					int flowTotalCount = this.GetFlowTotalCount(tmonitoredError.PerElementActivityType);
					ActivityElementIdMonitoringThresholdConfig config = this.Configuration.GetConfig(tmonitoredError.PerElementActivityType);
					HealthStateMachine.HealthState healthState = healthStateDictionary.GetHealthState(tmonitoredError);
					HealthStateMachine.HealthState healthState2 = (HealthStateMachine.CheckThreshold(flowTotalCount, num, config) ? healthState.GetNextHealthStateFailuresAboveThreshold() : healthState.GetNextHealthStateFailuresBelowThreshold());
					healthStateDictionary.SetHealthState(tmonitoredError, healthState2);
					if (healthState2.NumberOfConsecutiveFailures >= config.MinConsecutiveIntervals)
					{
						TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "NumberOfConsecutiveFailures:{0} MinConsecutiveIntervals:{1} event:{2}", new object[] { healthState2.NumberOfConsecutiveFailures, config.MinConsecutiveIntervals, tmonitoredError });
						tmonitoredError.Visit(this.m_alertIssuer);
					}
				}
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000DB9C File Offset: 0x0000BD9C
		private void UpdateHealthyState<TMonitoredError>(CountedHashSet<TMonitoredError> countedHashSet, HealthStateMachine.HealthStateDictionary<TMonitoredError> healthStateDictionary) where TMonitoredError : IPerElementActivityType
		{
			foreach (TMonitoredError tmonitoredError in healthStateDictionary.EnumerateUnhealthyMonitoredErrors.Where((TMonitoredError e) => countedHashSet.CountInstances(e) == 0))
			{
				if (this.m_timerTicksCounter.IsTriggered(tmonitoredError.PerElementActivityType))
				{
					TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Item : {0} Moving to healthy state  ", new object[] { tmonitoredError.PerElementActivityType });
					healthStateDictionary.SetHealthState(tmonitoredError, HealthStateMachine.HealthState.InitialHealthyState());
				}
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000DC4C File Offset: 0x0000BE4C
		private int GetFlowTotalCount(PerElementActivityType flowId)
		{
			int num = this.m_flowSuccessCounters.CountInstances((MonitoredFlowSuccessEvent k) => k.PerElementActivityType.Equals(flowId));
			int num2 = this.m_uncorrelatedFlowErrorCounters.CountInstances((MonitoredFlowErrorEvent k) => k.PerElementActivityType.Equals(flowId));
			int num3 = this.m_correlatedFlowErrorCounters.CountInstances((CorrelatedMonitoredErrorEvent k) => k.PerElementActivityType.Equals(flowId));
			int num4 = num + num2 + num3;
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Total event count for {0} = {1} (successCount={2}, uncorrelatedFailureFlowCount={3}, correlatedFailureFlowCount={4}", new object[] { flowId, num4, num, num2, num3 });
			return num4;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000DCFC File Offset: 0x0000BEFC
		private static bool CheckThreshold(int totalCount, int failureCount, ActivityElementIdMonitoringThresholdConfig config)
		{
			bool flag = totalCount > 0 && totalCount >= config.MinActivitiesCompletedPerInterval && (double)failureCount / (double)totalCount * 100.0 >= (double)config.ActivityFailureThreshold;
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "CheckThreshold(totalCount={0},failureCount={1})={2} since ActivityFailureThreshold={3} , MinActivitiesCompletedPerInterval={4}", new object[]
			{
				totalCount,
				failureCount,
				flag ? "AboveThreshold" : "BelowThreshold",
				config.ActivityFailureThreshold,
				config.MinActivitiesCompletedPerInterval
			});
			return flag;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000DD8C File Offset: 0x0000BF8C
		private void ClearCounters()
		{
			this.ClearCounterImpl<MonitoredFlowSuccessEvent>(this.m_flowSuccessCounters);
			this.ClearCounterImpl<MonitoredFlowErrorEvent>(this.m_uncorrelatedFlowErrorCounters);
			this.ClearCounterImpl<CorrelatedMonitoredErrorEvent>(this.m_correlatedFlowErrorCounters);
			this.ClearCounterImpl<MonitoredLowLevelErrorEvent>(this.m_uncorrelatedLowLevelErrorCounters);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000DDC0 File Offset: 0x0000BFC0
		private void ClearCounterImpl<T>(CountedHashSet<T> setCounters) where T : IPerElementActivityType
		{
			foreach (T t in setCounters.Where((T c) => this.m_timerTicksCounter.IsTriggered(c.PerElementActivityType)).ToList<T>())
			{
				setCounters.RemoveItem(t);
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Removed item: {0} from set", new object[] { t.PerElementActivityType });
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000DE4C File Offset: 0x0000C04C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000DE58 File Offset: 0x0000C058
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				object @lock = this.m_lock;
				lock (@lock)
				{
					this.m_stopFlag = true;
					if (this.m_alertIssuer != null)
					{
						this.m_alertIssuer.Dispose();
						this.m_alertIssuer = null;
					}
					if (this.m_warningIssuer != null)
					{
						this.m_warningIssuer.Dispose();
						this.m_warningIssuer = null;
					}
				}
				if (this.m_timerFactory != null)
				{
					this.m_timerFactory.Stop();
					this.m_timerFactory.WaitForStopToComplete();
					this.m_timerFactory.Shutdown();
					this.m_timerFactory = null;
				}
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000DF04 File Offset: 0x0000C104
		public void OnBatchCompleted()
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (!this.m_stopFlag)
				{
					this.m_warningIssuer.OnBatchCompleted();
				}
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000DF54 File Offset: 0x0000C154
		public void HandleFlowSuccess(MonitoredFlowSuccessEvent successEvent)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (!this.m_stopFlag)
				{
					this.m_flowSuccessCounters.AddInstance(successEvent);
				}
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		public void HandleUncorrelatedFlowError(MonitoredFlowErrorEvent flowError)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (!this.m_stopFlag)
				{
					TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Called with flowError: {0}", new object[] { flowError });
					if (flowError.HasValidWindowsEventLogId && flowError.IsInsideMonitoringScope)
					{
						this.IssueWarning<MonitoredFlowErrorEvent>(flowError, this.m_uncorrelatedFlowErrorState);
						this.m_uncorrelatedFlowErrorCounters.AddInstance(flowError);
					}
					else
					{
						this.m_flowSuccessCounters.AddInstance(flowError.ConvertToFlowSuccessEvent());
					}
				}
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000E040 File Offset: 0x0000C240
		public void HandleCorrelatedFlowError(CorrelatedMonitoredErrorEvent correlatedFlowError)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (!this.m_stopFlag)
				{
					TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Called with correlatedFlowError: {0}", new object[] { correlatedFlowError });
					if (correlatedFlowError.HasValidWindowsEventLogId && correlatedFlowError.FlowError.IsInsideMonitoringScope)
					{
						this.IssueWarning<CorrelatedMonitoredErrorEvent>(correlatedFlowError, this.m_correlatedFlowErrorState);
						this.m_correlatedFlowErrorCounters.AddInstance(correlatedFlowError);
					}
					else
					{
						this.m_flowSuccessCounters.AddInstance(correlatedFlowError.FlowError.ConvertToFlowSuccessEvent());
					}
				}
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		private void IssueWarning<TMonitoredError>(TMonitoredError monitoredError, HealthStateMachine.HealthStateDictionary<TMonitoredError> healthStateDictionary) where TMonitoredError : IMonitoredEventHandlerVisitor
		{
			HealthStateMachine.HealthState healthState = healthStateDictionary.GetHealthState(monitoredError);
			if (healthState.Healthy)
			{
				HealthStateMachine.HealthState nextHealthStateEncounteredFirstFailure = healthState.GetNextHealthStateEncounteredFirstFailure();
				healthStateDictionary.SetHealthState(monitoredError, nextHealthStateEncounteredFirstFailure);
				monitoredError.Visit(this.m_warningIssuer);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000E124 File Offset: 0x0000C324
		public void HandleUncorrelatedLowLevelError(MonitoredLowLevelErrorEvent lowLevelError)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (!this.m_stopFlag)
				{
					this.IssueWarning<MonitoredLowLevelErrorEvent>(lowLevelError, this.m_uncorrelatedLowLevelErrorState);
					this.m_uncorrelatedLowLevelErrorCounters.AddInstance(lowLevelError);
				}
			}
		}

		// Token: 0x04000136 RID: 310
		private readonly object m_lock;

		// Token: 0x04000137 RID: 311
		private CountedHashSet<MonitoredFlowSuccessEvent> m_flowSuccessCounters;

		// Token: 0x04000138 RID: 312
		private CountedHashSet<MonitoredFlowErrorEvent> m_uncorrelatedFlowErrorCounters;

		// Token: 0x04000139 RID: 313
		private CountedHashSet<CorrelatedMonitoredErrorEvent> m_correlatedFlowErrorCounters;

		// Token: 0x0400013A RID: 314
		private CountedHashSet<MonitoredLowLevelErrorEvent> m_uncorrelatedLowLevelErrorCounters;

		// Token: 0x0400013B RID: 315
		private readonly HealthStateMachine.HealthStateDictionary<MonitoredFlowErrorEvent> m_uncorrelatedFlowErrorState;

		// Token: 0x0400013C RID: 316
		private readonly HealthStateMachine.HealthStateDictionary<CorrelatedMonitoredErrorEvent> m_correlatedFlowErrorState;

		// Token: 0x0400013D RID: 317
		private readonly HealthStateMachine.HealthStateDictionary<MonitoredLowLevelErrorEvent> m_uncorrelatedLowLevelErrorState;

		// Token: 0x0400013E RID: 318
		private bool m_stopFlag;

		// Token: 0x0400013F RID: 319
		private TimerFactory m_timerFactory;

		// Token: 0x04000140 RID: 320
		private IMonitoredEventHandler m_alertIssuer;

		// Token: 0x04000141 RID: 321
		private IMonitoredEventHandler m_warningIssuer;

		// Token: 0x04000142 RID: 322
		private MonitoringTimerTicksCounter m_timerTicksCounter;

		// Token: 0x020005A4 RID: 1444
		private class MonitoredFlowSuccessComparer : PerElementActivityTypeComparer<MonitoredFlowSuccessEvent>
		{
		}

		// Token: 0x020005A5 RID: 1445
		private class MonitoredFlowErrorEventComparer : PerElementActivityTypeComparer<MonitoredFlowErrorEvent>
		{
		}

		// Token: 0x020005A6 RID: 1446
		private class MonitoredLowLevelErrorEventComparer : EventIdComparer<MonitoredLowLevelErrorEvent>
		{
		}

		// Token: 0x020005A7 RID: 1447
		private class CorrelatedMonitoredErrorEventComparer : IEqualityComparer<CorrelatedMonitoredErrorEvent>
		{
			// Token: 0x06002B13 RID: 11027 RVA: 0x00099CCF File Offset: 0x00097ECF
			public CorrelatedMonitoredErrorEventComparer()
			{
				this.m_flowComparer = new HealthStateMachine.MonitoredFlowErrorEventComparer();
				this.m_lowLevelComparer = new HealthStateMachine.MonitoredLowLevelErrorEventComparer();
			}

			// Token: 0x06002B14 RID: 11028 RVA: 0x00099CF0 File Offset: 0x00097EF0
			public bool Equals(CorrelatedMonitoredErrorEvent x, CorrelatedMonitoredErrorEvent y)
			{
				return (x == null && y == null) || (x != null && y != null && this.m_flowComparer.Equals(x.FlowError, y.FlowError) && this.m_lowLevelComparer.Equals(x.LowLevelError, y.LowLevelError));
			}

			// Token: 0x06002B15 RID: 11029 RVA: 0x00099D3D File Offset: 0x00097F3D
			public int GetHashCode(CorrelatedMonitoredErrorEvent obj)
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<CorrelatedMonitoredErrorEvent>(obj, "obj");
				return this.m_flowComparer.GetHashCode(obj.FlowError) ^ this.m_lowLevelComparer.GetHashCode(obj.LowLevelError);
			}

			// Token: 0x04000F6B RID: 3947
			private readonly HealthStateMachine.MonitoredFlowErrorEventComparer m_flowComparer;

			// Token: 0x04000F6C RID: 3948
			private readonly HealthStateMachine.MonitoredLowLevelErrorEventComparer m_lowLevelComparer;
		}

		// Token: 0x020005A8 RID: 1448
		private class HealthState
		{
			// Token: 0x06002B16 RID: 11030 RVA: 0x00099D6D File Offset: 0x00097F6D
			private HealthState(bool healthy, int aboveThresholdCount)
			{
				ExtendedDiagnostics.EnsureArgumentIsNotNegative(aboveThresholdCount, "aboveThresholdCount");
				ExtendedDiagnostics.EnsureOperation(!healthy || aboveThresholdCount == 0, "healthy cannot be true when above threshold count is more than zero.");
				this.m_healthy = healthy;
				this.m_consecutiveAboveThresholdCount = aboveThresholdCount;
			}

			// Token: 0x06002B17 RID: 11031 RVA: 0x00099DA2 File Offset: 0x00097FA2
			internal HealthStateMachine.HealthState GetNextHealthStateFailuresAboveThreshold()
			{
				return new HealthStateMachine.HealthState(false, this.m_consecutiveAboveThresholdCount + 1);
			}

			// Token: 0x06002B18 RID: 11032 RVA: 0x00099DB2 File Offset: 0x00097FB2
			internal HealthStateMachine.HealthState GetNextHealthStateFailuresBelowThreshold()
			{
				return this.GetNextHealthStateEncounteredFirstFailure();
			}

			// Token: 0x06002B19 RID: 11033 RVA: 0x00099DBA File Offset: 0x00097FBA
			public HealthStateMachine.HealthState GetNextHealthStateEncounteredFirstFailure()
			{
				return new HealthStateMachine.HealthState(false, 0);
			}

			// Token: 0x170006EB RID: 1771
			// (get) Token: 0x06002B1A RID: 11034 RVA: 0x00099DC3 File Offset: 0x00097FC3
			internal bool Healthy
			{
				get
				{
					return this.m_healthy;
				}
			}

			// Token: 0x170006EC RID: 1772
			// (get) Token: 0x06002B1B RID: 11035 RVA: 0x00099DCB File Offset: 0x00097FCB
			internal int NumberOfConsecutiveFailures
			{
				get
				{
					return this.m_consecutiveAboveThresholdCount;
				}
			}

			// Token: 0x06002B1C RID: 11036 RVA: 0x00099DD3 File Offset: 0x00097FD3
			internal static HealthStateMachine.HealthState InitialHealthyState()
			{
				return new HealthStateMachine.HealthState(true, 0);
			}

			// Token: 0x06002B1D RID: 11037 RVA: 0x00099DDC File Offset: 0x00097FDC
			public override string ToString()
			{
				string text;
				if (this.Healthy)
				{
					text = "Healthy";
				}
				else if (this.NumberOfConsecutiveFailures == 0)
				{
					text = "Warning";
				}
				else
				{
					text = "Failure #" + this.NumberOfConsecutiveFailures;
				}
				return text;
			}

			// Token: 0x04000F6D RID: 3949
			private readonly bool m_healthy;

			// Token: 0x04000F6E RID: 3950
			private readonly int m_consecutiveAboveThresholdCount;
		}

		// Token: 0x020005A9 RID: 1449
		private class HealthStateDictionary<TMonitoredError>
		{
			// Token: 0x06002B1E RID: 11038 RVA: 0x00099E20 File Offset: 0x00098020
			public HealthStateDictionary(IEqualityComparer<TMonitoredError> comparer)
			{
				this.m_dictionary = new Dictionary<TMonitoredError, HealthStateMachine.HealthState>(comparer);
			}

			// Token: 0x06002B1F RID: 11039 RVA: 0x00099E34 File Offset: 0x00098034
			public HealthStateMachine.HealthState GetHealthState(TMonitoredError monitoredEvent)
			{
				HealthStateMachine.HealthState healthState;
				if (!this.m_dictionary.TryGetValue(monitoredEvent, out healthState))
				{
					healthState = HealthStateMachine.HealthState.InitialHealthyState();
				}
				return healthState;
			}

			// Token: 0x06002B20 RID: 11040 RVA: 0x00099E58 File Offset: 0x00098058
			public void SetHealthState(TMonitoredError monitoredError, HealthStateMachine.HealthState healthState)
			{
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Changed health state from {0} to {1}", new object[]
				{
					this.GetHealthState(monitoredError),
					healthState
				});
				if (healthState.Healthy)
				{
					this.m_dictionary.Remove(monitoredError);
					return;
				}
				this.m_dictionary[monitoredError] = healthState;
			}

			// Token: 0x170006ED RID: 1773
			// (get) Token: 0x06002B21 RID: 11041 RVA: 0x00099EAC File Offset: 0x000980AC
			public IEnumerable<TMonitoredError> EnumerateUnhealthyMonitoredErrors
			{
				get
				{
					return new List<TMonitoredError>(this.m_dictionary.Keys);
				}
			}

			// Token: 0x04000F6F RID: 3951
			private readonly Dictionary<TMonitoredError, HealthStateMachine.HealthState> m_dictionary;
		}
	}
}
