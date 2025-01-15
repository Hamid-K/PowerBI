using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Monitoring;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Monitoring
{
	// Token: 0x02000442 RID: 1090
	[ConfigurationRoot(Consumers = "E.ServerManager.*", Options = ConfigurationOptions.AutoReconfigure)]
	[Serializable]
	public sealed class WindowsEventLogWriterConfiguration : ConfigurationClass
	{
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060021C4 RID: 8644 RVA: 0x0007D2AE File Offset: 0x0007B4AE
		// (set) Token: 0x060021C5 RID: 8645 RVA: 0x0007D2B6 File Offset: 0x0007B4B6
		[ConfigurationProperty]
		public int EventDelay { get; set; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x0007D2BF File Offset: 0x0007B4BF
		// (set) Token: 0x060021C7 RID: 8647 RVA: 0x0007D2C7 File Offset: 0x0007B4C7
		[ConfigurationProperty]
		public int EventStorageCapacity { get; set; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x0007D2D0 File Offset: 0x0007B4D0
		// (set) Token: 0x060021C9 RID: 8649 RVA: 0x0007D2D8 File Offset: 0x0007B4D8
		[ConfigurationProperty]
		public double MinTimeBetweenEventsInMsec { get; set; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x0007D2E1 File Offset: 0x0007B4E1
		// (set) Token: 0x060021CB RID: 8651 RVA: 0x0007D2E9 File Offset: 0x0007B4E9
		[ConfigurationProperty]
		public ConfigurationCollection<ActivityElementIdMonitoringThresholdConfig> ActivityElementIdMonitoringThresholdConfigList { get; set; }

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x0007D2F2 File Offset: 0x0007B4F2
		// (set) Token: 0x060021CD RID: 8653 RVA: 0x0007D2FA File Offset: 0x0007B4FA
		[ConfigurationProperty]
		public string EventLogSourceName { get; set; }

		// Token: 0x060021CE RID: 8654 RVA: 0x0007D304 File Offset: 0x0007B504
		public static void ValidateConfiguration(WindowsEventLogWriterConfiguration windowsEventLogWriterConfiguration)
		{
			if (windowsEventLogWriterConfiguration.EventDelay <= 0)
			{
				WindowsEventLogWriterConfiguration.ThrowMalformedNonPositiveConfiguration("EventDelay");
			}
			if (windowsEventLogWriterConfiguration.EventStorageCapacity <= 0)
			{
				WindowsEventLogWriterConfiguration.ThrowMalformedNonPositiveConfiguration("EventStorageCapacity");
			}
			if (windowsEventLogWriterConfiguration.ActivityElementIdMonitoringThresholdConfigList.Count < 1)
			{
				throw new CcsMalformedConfigurationException("Configuration should have at least one ActivityEventMonitoringThresholdConfig element ");
			}
			bool flag = false;
			HashSet<PerElementActivityType> hashSet = new HashSet<PerElementActivityType>();
			foreach (ActivityElementIdMonitoringThresholdConfig activityElementIdMonitoringThresholdConfig in windowsEventLogWriterConfiguration.ActivityElementIdMonitoringThresholdConfigList)
			{
				PerElementActivityType perElementActivityType = new PerElementActivityType(new ElementId(activityElementIdMonitoringThresholdConfig.ElementId), new ActivityType(activityElementIdMonitoringThresholdConfig.ActivityType));
				if (hashSet.Contains(perElementActivityType))
				{
					throw new CcsMalformedConfigurationException("FilteredWindowsEventLogSinkConfiguration.ActivityElementIdMonitoringThresholdConfigList contains a duplicate (ElementId,ActivityType) pair: {0}".FormatWithInvariantCulture(new object[] { perElementActivityType }));
				}
				hashSet.Add(perElementActivityType);
				if (PerElementActivityType.Default.Equals(perElementActivityType))
				{
					flag = true;
				}
				if (activityElementIdMonitoringThresholdConfig.WindowsEventLogWriteInterval <= 0)
				{
					WindowsEventLogWriterConfiguration.ThrowMalformedNonPositiveConfiguration("WindowsEventLogWriteInterval In: {0}".FormatWithInvariantCulture(new object[] { activityElementIdMonitoringThresholdConfig.ActivityType }));
				}
				if (activityElementIdMonitoringThresholdConfig.MinConsecutiveIntervals <= 0)
				{
					WindowsEventLogWriterConfiguration.ThrowMalformedNonPositiveConfiguration("MinConsecutiveIntervals In: {0}".FormatWithInvariantCulture(new object[] { activityElementIdMonitoringThresholdConfig.ActivityType }));
				}
				if (activityElementIdMonitoringThresholdConfig.ActivityFailureThreshold <= 0)
				{
					WindowsEventLogWriterConfiguration.ThrowMalformedNonPositiveConfiguration("ActivityFailureThreshold In: {0}".FormatWithInvariantCulture(new object[] { activityElementIdMonitoringThresholdConfig.ActivityType }));
				}
				if (activityElementIdMonitoringThresholdConfig.ActivityFailureThreshold > 100)
				{
					throw new CcsMalformedConfigurationException("FilteredWindowsEventLogSinkConfiguration.ActivityFailureThreshold must be less than or equals 100. In: {0}".FormatWithInvariantCulture(new object[] { activityElementIdMonitoringThresholdConfig.ActivityType }));
				}
			}
			if (!flag)
			{
				throw new CcsMalformedConfigurationException("Configuration should contain a default ActivityEventMonitoringThresholdConfig element ");
			}
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x0007D4AC File Offset: 0x0007B6AC
		private static void ThrowMalformedNonPositiveConfiguration(string propertyName)
		{
			throw new CcsMalformedConfigurationException("FilteredWindowsEventLogSinkConfiguration. {0} must be greater than zero.".FormatWithInvariantCulture(new object[] { propertyName }));
		}
	}
}
