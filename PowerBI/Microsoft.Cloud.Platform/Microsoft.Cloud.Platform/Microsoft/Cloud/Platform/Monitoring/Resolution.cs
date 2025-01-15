using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.ConfigurationClasses.Monitoring;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000096 RID: 150
	internal class Resolution
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x0000FA20 File Offset: 0x0000DC20
		public Resolution([NotNull] AnalysisResolutionConfig config, [NotNull] AsyncEvent<StateCalculatedEventArgs> stateCalculatedNotifier, [NotNull] string streamId)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(streamId, "streamId");
			ExtendedDiagnostics.EnsureArgumentNotNull<AnalysisResolutionConfig>(config, "config");
			ExtendedDiagnostics.EnsureArgumentNotNull<AsyncEvent<StateCalculatedEventArgs>>(stateCalculatedNotifier, "stateCalculatedNotifier");
			this.m_histories = new Dictionary<Differentiators, History>(new DifferentiatorComparerForResolution(config.DifferentiatorIndexes));
			this.m_stateCalculatedNotifier = stateCalculatedNotifier;
			this.m_config = config;
			this.m_streamId = streamId;
			this.m_historiesDictionaryWritersLock = new object();
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Finished creating resolution: {0}", new object[] { this });
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000FAA8 File Offset: 0x0000DCA8
		public bool ReceiveReport(DateTime reportTime, ReportTypes reportType, [NotNull] Differentiators differentiators, object failureContext)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Differentiators>(differentiators, "differentiators");
			ExtendedDiagnostics.EnsureArgument("reportType", reportType == ReportTypes.Failure || failureContext == null, "Received a context with a success report");
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "{0} received {1} report with differentiators: {2}. Configuration is: {3}", new object[] { this, reportType, differentiators, this.m_config });
			History history = this.TryGetHistoryForDifferentiatorsSet(reportTime, differentiators);
			if (history == null)
			{
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Differentiators do not belong to resolution");
				return false;
			}
			history.ReceiveReport(reportTime, reportType, delegate(object result)
			{
				StateCalculatedResult stateCalculatedResult = result as StateCalculatedResult;
				StateCalculatedEventArgs stateCalculatedEventArgs = new StateCalculatedEventArgs(stateCalculatedResult.Time, this.m_streamId, this.MakeDiffsOnlyForResolution(differentiators), stateCalculatedResult.State, (stateCalculatedResult.State == State.Error) ? history.FailureContext : null);
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Firing StateCalculated event with args: {0}", new object[] { stateCalculatedEventArgs });
				this.m_stateCalculatedNotifier.FireEvent(this, stateCalculatedEventArgs);
			}, failureContext);
			return true;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000FB74 File Offset: 0x0000DD74
		[CanBeNull]
		private History TryGetHistoryForDifferentiatorsSet(DateTime queryTime, Differentiators differentiators)
		{
			if (!this.DoDifferentiatorsBelongToResolution(differentiators))
			{
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Differentiators {0} do not belong to resolution {1}, returning null", new object[] { differentiators, this });
				return null;
			}
			History history = null;
			if (!this.m_histories.TryGetValue(differentiators, out history))
			{
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "History was not found for {0}, getting writer lock", new object[] { differentiators });
				object historiesDictionaryWritersLock = this.m_historiesDictionaryWritersLock;
				lock (historiesDictionaryWritersLock)
				{
					if (!this.m_histories.TryGetValue(differentiators, out history))
					{
						history = new History(queryTime, this.m_config.AlarmThreshold, this.m_config.MaxPeriodsInHistory, this.m_config.SuccessTolerance, this.m_config.TrafficQuotaPerPeriod, this.m_config.FaultThresholdPerPeriod, TimeSpan.FromSeconds((double)this.m_config.TimePeriodLength));
						TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Created history for differentiators: {0} under resolution: {1}.", new object[] { differentiators, this });
						this.m_histories = new Dictionary<Differentiators, History>(this.m_histories, this.m_histories.Comparer) { { differentiators, history } };
						TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Re-created the histories dictionary");
						return history;
					}
					TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "After acquiring writer lock, discovered that history for differentiators: {0} under resolution: {1} already exists. Returning it", new object[] { differentiators, this });
					return history;
				}
			}
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "History for differentiators: {0} under resolution: {1} already exists, returning it.", new object[] { differentiators, this });
			return history;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000FD08 File Offset: 0x0000DF08
		private bool DoDifferentiatorsBelongToResolution(Differentiators differentiators)
		{
			bool flag = this.m_config.DifferentiatorIndexes.All((int index) => !string.IsNullOrEmpty(differentiators[index]));
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Differentiators: {0} {1} match resolution {2}", new object[]
			{
				differentiators,
				flag ? "" : "do not",
				this
			});
			return flag;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000FD78 File Offset: 0x0000DF78
		private string[] MakeDiffsOnlyForResolution(Differentiators source)
		{
			string[] array = new string[source.NumOfDifferentiators];
			foreach (int num in this.m_config.DifferentiatorIndexes)
			{
				array[num] = source[num];
			}
			return array;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000FDDC File Offset: 0x0000DFDC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Resolution for indices: {0}", new object[] { this.m_config.DifferentiatorIndexes.StringJoin(",") });
		}

		// Token: 0x04000170 RID: 368
		private readonly AnalysisResolutionConfig m_config;

		// Token: 0x04000171 RID: 369
		private readonly AsyncEvent<StateCalculatedEventArgs> m_stateCalculatedNotifier;

		// Token: 0x04000172 RID: 370
		private readonly string m_streamId;

		// Token: 0x04000173 RID: 371
		private readonly object m_historiesDictionaryWritersLock;

		// Token: 0x04000174 RID: 372
		private volatile Dictionary<Differentiators, History> m_histories;
	}
}
