using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.ConfigurationClasses.Monitoring;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000090 RID: 144
	internal class MonitoringTimerTicksCounter
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x0000EC18 File Offset: 0x0000CE18
		public MonitoringTimerTicksCounter(ActivityTypeElementIdPairConfigCollection configuration)
		{
			this.m_timerTicks = new Dictionary<PerElementActivityType, WindowsEventLogIntervalConfig>();
			foreach (KeyValuePair<PerElementActivityType, ActivityElementIdMonitoringThresholdConfig> keyValuePair in ((IEnumerable<KeyValuePair<PerElementActivityType, ActivityElementIdMonitoringThresholdConfig>>)configuration))
			{
				this.m_timerTicks.Add(keyValuePair.Key, new WindowsEventLogIntervalConfig(keyValuePair.Value.WindowsEventLogWriteInterval));
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Adding: PerElementActivityType:{0} with Interval: {1}", new object[]
				{
					keyValuePair.Key,
					keyValuePair.Value.WindowsEventLogWriteInterval
				});
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000ECC4 File Offset: 0x0000CEC4
		public void Tick()
		{
			foreach (KeyValuePair<PerElementActivityType, WindowsEventLogIntervalConfig> keyValuePair in this.m_timerTicks)
			{
				keyValuePair.Value.Tick();
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000ED1C File Offset: 0x0000CF1C
		public bool IsTriggered(PerElementActivityType key)
		{
			WindowsEventLogIntervalConfig windowsEventLogIntervalConfig;
			if (this.m_timerTicks.TryGetValue(key, out windowsEventLogIntervalConfig))
			{
				return windowsEventLogIntervalConfig.Counter == 0;
			}
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "PerElementActivityType:{0} doesn't have a specific timer configured, checking with default element id", new object[] { key });
			PerElementActivityType perElementActivityType = new PerElementActivityType(PerElementActivityType.DefaultElementId, new ActivityType(key.FlowName.ShortName));
			if (this.m_timerTicks.TryGetValue(perElementActivityType, out windowsEventLogIntervalConfig))
			{
				return windowsEventLogIntervalConfig.Counter == 0;
			}
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "PerElementActivityType:{0} doesn't have a specific timer configured, returning default timer", new object[] { perElementActivityType });
			WindowsEventLogIntervalConfig windowsEventLogIntervalConfig2 = this.m_timerTicks[PerElementActivityType.Default];
			ExtendedDiagnostics.EnsureNotNull<WindowsEventLogIntervalConfig>(windowsEventLogIntervalConfig2, "There is no default timer (for the DFLT /DFLT pair) in m_timerTicks ");
			return windowsEventLogIntervalConfig2.Counter == 0;
		}

		// Token: 0x0400015D RID: 349
		private Dictionary<PerElementActivityType, WindowsEventLogIntervalConfig> m_timerTicks;
	}
}
