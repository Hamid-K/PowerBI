using System;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.ConfigurationClasses.Monitoring;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000098 RID: 152
	[BlockServiceProvider(typeof(IFailureAnalyzerFactory))]
	public class FailureAnalyzerFactory : Block, IFailureAnalyzerFactory
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x00010268 File Offset: 0x0000E468
		public FailureAnalyzerFactory()
			: base(typeof(FailureAnalyzerFactory).Name)
		{
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001028C File Offset: 0x0000E48C
		protected override BlockInitializationStatus OnInitialize()
		{
			if (base.OnInitialize() == BlockInitializationStatus.PartiallyDone)
			{
				return BlockInitializationStatus.PartiallyDone;
			}
			this.m_failureAnalyzers = new Dictionary<string, FailureAnalyzer>();
			this.m_configManager = this.m_configManagerFactory.GetConfigurationManager();
			this.m_configManager.Subscribe(new List<Type> { typeof(FailureAnalyzersConfiguration) }, new CcsEventHandler(this.OnConfigChange));
			return BlockInitializationStatus.Done;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000102EF File Offset: 0x0000E4EF
		protected override void OnStop()
		{
			this.m_configManager.Unsubscribe(new CcsEventHandler(this.OnConfigChange));
			base.OnStop();
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001030E File Offset: 0x0000E50E
		protected override void OnShutdown()
		{
			this.m_failureAnalyzers = null;
			this.m_configManager = null;
			base.OnShutdown();
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00010328 File Offset: 0x0000E528
		public IFailureAnalyzer GetFailureAnalyzer([NotNull] string streamId)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(streamId, "streamId");
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Received request to get a FailureAnalyzer with stream id: {0}", new object[] { streamId });
			FailureAnalyzer failureAnalyzer = null;
			if (!this.m_failureAnalyzers.TryGetValue(streamId, out failureAnalyzer))
			{
				ArgumentException ex = new ArgumentException(string.Format(CultureInfo.InvariantCulture, "There is no configuration for a stream with id: {0}.", new object[] { streamId }), "streamId");
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Error, "Received request to get a FailureAnalyzer with stream id for which there is no configuration: {0}. Throwing Exception: {1}", new object[] { streamId, ex });
				throw ex;
			}
			return failureAnalyzer;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000103B4 File Offset: 0x0000E5B4
		private void OnConfigChange(IConfigurationContainer configContainer)
		{
			FailureAnalyzersConfiguration configuration = configContainer.GetConfiguration<FailureAnalyzersConfiguration>();
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Received new configuration: {0}.", new object[] { configuration });
			object configChangeLock = this.m_configChangeLock;
			lock (configChangeLock)
			{
				configuration.ValidateConfiguration(this.m_failureAnalyzers.Keys);
				Dictionary<string, FailureAnalyzer> dictionary = new Dictionary<string, FailureAnalyzer>(this.m_failureAnalyzers);
				foreach (FailureAnalyzerStreamConfig failureAnalyzerStreamConfig in configuration.FailureAnalyzerStreamConfigList)
				{
					FailureAnalyzer failureAnalyzer = null;
					dictionary.TryGetValue(failureAnalyzerStreamConfig.StreamId, out failureAnalyzer);
					if (failureAnalyzer == null)
					{
						TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "There is no existing FailureAnalyzer for id: {0}. Creating one.", new object[] { failureAnalyzerStreamConfig.StreamId });
						dictionary.Add(failureAnalyzerStreamConfig.StreamId, new FailureAnalyzer(failureAnalyzerStreamConfig));
					}
					else
					{
						TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "There is an existing FailureAnalyzer for id: {0}. Updating its config.", new object[] { failureAnalyzerStreamConfig.StreamId });
						failureAnalyzer.UpdateConfiguration(failureAnalyzerStreamConfig);
					}
				}
				this.m_failureAnalyzers = dictionary;
			}
		}

		// Token: 0x04000178 RID: 376
		private volatile Dictionary<string, FailureAnalyzer> m_failureAnalyzers;

		// Token: 0x04000179 RID: 377
		private object m_configChangeLock = new object();

		// Token: 0x0400017A RID: 378
		[BlockServiceDependency]
		private IConfigurationManagerFactory m_configManagerFactory;

		// Token: 0x0400017B RID: 379
		private IConfigurationManager m_configManager;
	}
}
