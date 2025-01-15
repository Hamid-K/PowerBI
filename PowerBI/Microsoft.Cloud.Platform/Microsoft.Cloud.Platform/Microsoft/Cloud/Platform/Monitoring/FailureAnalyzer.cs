using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.ConfigurationClasses.Monitoring;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000097 RID: 151
	public class FailureAnalyzer : IFailureAnalyzer
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000FE0B File Offset: 0x0000E00B
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x0000FE13 File Offset: 0x0000E013
		public string StreamId { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000FE1C File Offset: 0x0000E01C
		internal int NumOfDifferentiators
		{
			get
			{
				return this.m_resolutionsAndConfig.NumOfDifferentiators;
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000FE2C File Offset: 0x0000E02C
		internal FailureAnalyzer([NotNull] FailureAnalyzerStreamConfig config)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<FailureAnalyzerStreamConfig>(config, "config");
			this.m_stateCalculatedEvent = new AsyncEvent<StateCalculatedEventArgs>("StateCalculatedEvent" + Guid.NewGuid().ToString(), AsyncEventSubscriptionOptions.None);
			this.UpdateConfiguration(config);
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Finished creating FailureAnalyzer for stream id: {0} with config: {1}", new object[] { this.StreamId, config });
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		internal void UpdateConfiguration([NotNull] FailureAnalyzerStreamConfig config)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<FailureAnalyzerStreamConfig>(config, "config");
			ExtendedDiagnostics.EnsureArgument("config", this.m_resolutionsAndConfig == null || config.StreamId == this.StreamId, "Received config for a different stream id");
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "FailureAnalyzer for stream id: {0} received new config: {1}", new object[] { this.StreamId, config });
			if (this.StreamId == null)
			{
				this.StreamId = config.StreamId;
			}
			Resolution[] array = new Resolution[config.AnalysisResolutionConfigList.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new Resolution(config.AnalysisResolutionConfigList[i], this.m_stateCalculatedEvent, config.StreamId);
			}
			FailureAnalyzer.ResoluionsAndConfig resoluionsAndConfig = new FailureAnalyzer.ResoluionsAndConfig(array, config.NumOfDifferentiators);
			this.m_resolutionsAndConfig = resoluionsAndConfig;
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Resolutions switched");
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000FF81 File Offset: 0x0000E181
		public void ReportSuccess(DateTime reportTime, string[] differentiators)
		{
			this.ReportImpl(reportTime, ReportTypes.Success, differentiators, null);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000FF8D File Offset: 0x0000E18D
		public void ReportFailure(DateTime reportTime, string[] differentiators, object context)
		{
			this.ReportImpl(reportTime, ReportTypes.Failure, differentiators, context);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000FF9C File Offset: 0x0000E19C
		public void SubscribeForStateCalculatedNotifications([NotNull] IIdentifiable subscriber, [NotNull] WorkTicket subscriberTicket, [NotNull] EventHandler<StateCalculatedEventArgs> callback)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IIdentifiable>(subscriber, "subscriber");
			ExtendedDiagnostics.EnsureArgumentNotNull<WorkTicket>(subscriberTicket, "subscriberTicket");
			ExtendedDiagnostics.EnsureArgumentNotNull<EventHandler<StateCalculatedEventArgs>>(callback, "callback");
			ExtendedDiagnostics.EnsureArgument("subscriberTicket", subscriberTicket.IsValid(), "Received an invalid ticket");
			this.m_stateCalculatedEvent.Subscribe(subscriber, subscriberTicket, callback, AsyncEventSubscriptionOptions.None);
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Failure Analyzer for stream id: {0} subscribed subscriber: {1} with callback {2} for state calculated notifications.", new object[]
			{
				this.StreamId,
				subscriber,
				callback.Method.Name
			});
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00010020 File Offset: 0x0000E220
		public void UnsubscribeFromStateCalculatedNotifications([NotNull] IIdentifiable subscriber)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IIdentifiable>(subscriber, "subscriber");
			this.m_stateCalculatedEvent.Unsubscribe(subscriber);
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Failure Analyzer for stream id: {0} unsubscribed subscriber: {1} from state calculated notifications", new object[] { this.StreamId, subscriber.Name });
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001006C File Offset: 0x0000E26C
		private void ReportImpl(DateTime reportTime, ReportTypes reportType, [NotNull] string[] differentiators, object context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string[]>(differentiators, "differentiators");
			ExtendedDiagnostics.EnsureArgument("reportType", reportType == ReportTypes.Failure || context == null, "Received a success report and context isn't null");
			if (differentiators.Length != this.NumOfDifferentiators)
			{
				ArgumentException ex = new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Received incorrect # of differentiators. Expected: {0}. Received: {1}", new object[] { this.NumOfDifferentiators, differentiators.Length }));
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Error, "Received incorrect # of differentiators. Throwing exception {0}", new object[] { ex });
				throw ex;
			}
			if (differentiators.All((string diff) => string.IsNullOrEmpty(diff)))
			{
				ArgumentException ex2 = new ArgumentException("All differentiator values are null or empty", "differentiators");
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Error, "All values received are null or empty. Throwing exception: {0}", new object[] { ex2 });
				throw ex2;
			}
			Differentiators differentiators2 = new Differentiators(differentiators);
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Failure Analyzer for stream id: {0} received of type {1} report for differentiators: {2}. Context: {3}. # of diffs is: {4}", new object[] { this.StreamId, reportType, differentiators2, context, this.NumOfDifferentiators });
			bool flag = false;
			foreach (Resolution resolution in this.m_resolutionsAndConfig.Resolutions)
			{
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "FailureAnalyzer with stream id: {0} passing {1} report with differentiators: {2} to resolution: {3}", new object[] { this.StreamId, reportType, differentiators, resolution });
				flag |= resolution.ReceiveReport(reportTime.ToUniversalTime(), reportType, differentiators2, context);
			}
			if (!flag)
			{
				ArgumentException ex3 = new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Received differentiators did not belong to any resolution. Differentiators: {0}", new object[] { differentiators2 }), "differentiators");
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Error, "Received differentiators did not belong to any resolution. Throwing exception: {0}", new object[] { ex3 });
				throw ex3;
			}
		}

		// Token: 0x04000175 RID: 373
		private volatile FailureAnalyzer.ResoluionsAndConfig m_resolutionsAndConfig;

		// Token: 0x04000176 RID: 374
		private readonly AsyncEvent<StateCalculatedEventArgs> m_stateCalculatedEvent;

		// Token: 0x020005AF RID: 1455
		private class ResoluionsAndConfig
		{
			// Token: 0x170006EE RID: 1774
			// (get) Token: 0x06002B30 RID: 11056 RVA: 0x00099FB8 File Offset: 0x000981B8
			// (set) Token: 0x06002B31 RID: 11057 RVA: 0x00099FC0 File Offset: 0x000981C0
			public IEnumerable<Resolution> Resolutions { get; private set; }

			// Token: 0x170006EF RID: 1775
			// (get) Token: 0x06002B32 RID: 11058 RVA: 0x00099FC9 File Offset: 0x000981C9
			// (set) Token: 0x06002B33 RID: 11059 RVA: 0x00099FD1 File Offset: 0x000981D1
			public int NumOfDifferentiators { get; private set; }

			// Token: 0x06002B34 RID: 11060 RVA: 0x00099FDA File Offset: 0x000981DA
			public ResoluionsAndConfig([NotNull] IEnumerable<Resolution> resolutions, int numOfDifferentiators)
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<Resolution>>(resolutions, "resolutions");
				ExtendedDiagnostics.EnsureArgumentIsPositive(numOfDifferentiators, "numOfDifferentiators");
				this.NumOfDifferentiators = numOfDifferentiators;
				this.Resolutions = resolutions;
			}
		}
	}
}
