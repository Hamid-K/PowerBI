using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.ConfigurationClasses.Monitoring;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x0200006E RID: 110
	internal sealed class ActivityTypeElementIdPairConfigCollection : IEnumerable<KeyValuePair<PerElementActivityType, ActivityElementIdMonitoringThresholdConfig>>, IEnumerable
	{
		// Token: 0x0600034B RID: 843 RVA: 0x0000CAA8 File Offset: 0x0000ACA8
		public ActivityTypeElementIdPairConfigCollection(IEnumerable<ActivityElementIdMonitoringThresholdConfig> configuration)
		{
			this.m_dictionary = new Dictionary<PerElementActivityType, ActivityElementIdMonitoringThresholdConfig>();
			foreach (ActivityElementIdMonitoringThresholdConfig activityElementIdMonitoringThresholdConfig in configuration)
			{
				PerElementActivityType perElementActivityType = new PerElementActivityType(new ElementId(activityElementIdMonitoringThresholdConfig.ElementId), new ActivityType(activityElementIdMonitoringThresholdConfig.ActivityType));
				this.m_dictionary.Add(perElementActivityType, activityElementIdMonitoringThresholdConfig);
				TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Info, "Adding:  PerElementActivityType:{0} with Config: {1}", new object[]
				{
					perElementActivityType,
					ActivityTypeElementIdPairConfigCollection.ConfigAsString(activityElementIdMonitoringThresholdConfig)
				});
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000CB48 File Offset: 0x0000AD48
		public ActivityElementIdMonitoringThresholdConfig GetConfig(PerElementActivityType elementActivityPair)
		{
			ActivityElementIdMonitoringThresholdConfig activityElementIdMonitoringThresholdConfig;
			if (this.m_dictionary.TryGetValue(elementActivityPair, out activityElementIdMonitoringThresholdConfig))
			{
				return activityElementIdMonitoringThresholdConfig;
			}
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "PerElementActivityType:{0} wasn't configured, checking with default element id", new object[] { elementActivityPair });
			PerElementActivityType perElementActivityType = new PerElementActivityType(PerElementActivityType.DefaultElementId, new ActivityType(elementActivityPair.FlowName.ShortName));
			if (this.m_dictionary.TryGetValue(perElementActivityType, out activityElementIdMonitoringThresholdConfig))
			{
				return activityElementIdMonitoringThresholdConfig;
			}
			TraceSourceBase<MonitoringTrace>.Tracer.Trace(TraceVerbosity.Verbose, "PerElementActivityType:{0} wasn't configured, returning configuration for the default pair (DFLT/DFLT)", new object[] { perElementActivityType });
			if (this.m_dictionary.TryGetValue(PerElementActivityType.Default, out activityElementIdMonitoringThresholdConfig))
			{
				return activityElementIdMonitoringThresholdConfig;
			}
			ExtendedEnvironment.FailSlow(this, "ActivityTypeElementIdPairConfigCollection doesn't hold the default PerElementActivityType");
			return null;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000CBEC File Offset: 0x0000ADEC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<PerElementActivityType, ActivityElementIdMonitoringThresholdConfig> keyValuePair in this.m_dictionary)
			{
				ActivityElementIdMonitoringThresholdConfig value = keyValuePair.Value;
				stringBuilder.Append(ActivityTypeElementIdPairConfigCollection.ConfigAsString(value));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000CC5C File Offset: 0x0000AE5C
		private static string ConfigAsString(ActivityElementIdMonitoringThresholdConfig config)
		{
			return string.Format(CultureInfo.InvariantCulture, "ActivityType={0}  ElementId={1} WindowsEventLogWriteInterval={2} MinActivitiesCompletedPerInterval={3}  ActivityFailureThreshold={4}  MinConsecutiveIntervals={5}", new object[] { config.ActivityType, config.ElementId, config.WindowsEventLogWriteInterval, config.MinActivitiesCompletedPerInterval, config.ActivityFailureThreshold, config.MinConsecutiveIntervals });
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000CCC8 File Offset: 0x0000AEC8
		IEnumerator<KeyValuePair<PerElementActivityType, ActivityElementIdMonitoringThresholdConfig>> IEnumerable<KeyValuePair<PerElementActivityType, ActivityElementIdMonitoringThresholdConfig>>.GetEnumerator()
		{
			return this.m_dictionary.GetEnumerator();
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000CCC8 File Offset: 0x0000AEC8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_dictionary.GetEnumerator();
		}

		// Token: 0x0400011B RID: 283
		private Dictionary<PerElementActivityType, ActivityElementIdMonitoringThresholdConfig> m_dictionary;
	}
}
