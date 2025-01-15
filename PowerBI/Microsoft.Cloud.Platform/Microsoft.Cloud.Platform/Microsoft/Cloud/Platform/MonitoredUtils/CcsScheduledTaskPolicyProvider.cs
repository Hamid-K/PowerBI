using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.ConfigurationClasses.Scheduler;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000146 RID: 326
	internal sealed class CcsScheduledTaskPolicyProvider : IScheduledTaskPolicyProvider
	{
		// Token: 0x0600088E RID: 2190 RVA: 0x0001D81C File Offset: 0x0001BA1C
		public CcsScheduledTaskPolicyProvider([NotNull] string policyName, [NotNull] IConfigurationManager configManager, [NotNull] string taskName, [NotNull] IRescheduleTaskExecution scheduler)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(policyName, "policyName");
			ExtendedDiagnostics.EnsureArgumentNotNull<IConfigurationManager>(configManager, "configManager");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(taskName, "taskName");
			ExtendedDiagnostics.EnsureArgumentNotNull<IRescheduleTaskExecution>(scheduler, "scheduler");
			this.m_scheduler = scheduler;
			this.m_cfgMgr = configManager;
			this.m_taskName = taskName;
			this.m_policyName = policyName;
			this.m_locker = new object();
			this.m_cfgMgr.Subscribe(new List<Type> { typeof(ScheduledTasksConfiguration) }, new CcsEventHandler(this.ConfigurationChangedHandler));
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001D8B0 File Offset: 0x0001BAB0
		public IEnumerable<ExclusionGroupIdentifier> GetExclusionGroups()
		{
			object locker = this.m_locker;
			IEnumerable<ExclusionGroupIdentifier> exclusionGroups;
			lock (locker)
			{
				exclusionGroups = this.m_exclusionGroups;
			}
			return exclusionGroups;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001D8F4 File Offset: 0x0001BAF4
		public TimeSpan GetTimeout()
		{
			object locker = this.m_locker;
			TimeSpan timeout;
			lock (locker)
			{
				timeout = this.m_timeout;
			}
			return timeout;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001D938 File Offset: 0x0001BB38
		public bool GetIsCrashAllowedOnTimeout()
		{
			object locker = this.m_locker;
			bool isCrashAllowedOnTimeout;
			lock (locker)
			{
				isCrashAllowedOnTimeout = this.m_isCrashAllowedOnTimeout;
			}
			return isCrashAllowedOnTimeout;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001D97C File Offset: 0x0001BB7C
		public DateTime GetNextRunTime(DateTime now)
		{
			object locker = this.m_locker;
			DateTime dateTime3;
			lock (locker)
			{
				TimeSpan timeSpan;
				switch (this.m_frequency)
				{
				case SchedulingFrequency.Minute:
					timeSpan = new TimeSpan(0, 0, 0, now.Second, now.Millisecond);
					break;
				case SchedulingFrequency.Hour:
					timeSpan = new TimeSpan(0, 0, now.Minute, now.Second, now.Millisecond);
					break;
				case SchedulingFrequency.Day:
					timeSpan = new TimeSpan(0, now.Hour, now.Minute, now.Second, now.Millisecond);
					break;
				default:
					throw new CcsMalformedConfigurationException(string.Format(CultureInfo.CurrentCulture, "Illegal frequency for task'{0}'", new object[] { this.m_policyName }));
				}
				DateTime dateTime = now.Subtract(timeSpan);
				DateTime dateTime2;
				for (int i = 0; i < this.m_offsets.Count; i++)
				{
					dateTime2 = dateTime.Add(this.m_offsets[i]);
					if (dateTime2 > now)
					{
						return dateTime2;
					}
				}
				dateTime2 = dateTime.Add(this.m_maxOffset);
				if (this.m_offsets.Any<TimeSpan>())
				{
					dateTime2 = dateTime2.Add(this.m_offsets[0]);
				}
				dateTime3 = dateTime2;
			}
			return dateTime3;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001DAE0 File Offset: 0x0001BCE0
		private void ConfigurationChangedHandler(IConfigurationContainer configurationContainer)
		{
			ScheduledTasksConfiguration configuration = configurationContainer.GetConfiguration<ScheduledTasksConfiguration>();
			ExtendedDiagnostics.EnsureArgumentNotNull<ScheduledTasksConfiguration>(configuration, "cfg");
			ScheduledTaskSettings scheduledTaskSettings = configuration.Settings.Where((ScheduledTaskSettings p) => p.PolicyName.Equals(this.m_policyName, StringComparison.Ordinal)).FirstOrDefault<ScheduledTaskSettings>();
			if (scheduledTaskSettings == null)
			{
				throw new CcsMalformedConfigurationException(string.Format(CultureInfo.CurrentCulture, "Missing policy for task '{0}'", new object[] { this.m_policyName }));
			}
			List<ExclusionGroupIdentifier> list = scheduledTaskSettings.RequiredResources.Select((string g) => new ExclusionGroupIdentifier(g)).ToList<ExclusionGroupIdentifier>();
			if (list.Count == 0)
			{
				throw new CcsMalformedConfigurationException(string.Format(CultureInfo.CurrentCulture, "Missing exclusion groups for task '{0}'", new object[] { this.m_policyName }));
			}
			SchedulingFrequency executionsFrequency = scheduledTaskSettings.ExecutionsFrequency;
			TimeSpan maxOffset;
			switch (executionsFrequency)
			{
			case SchedulingFrequency.Minute:
				maxOffset = TimeSpan.FromMinutes(1.0);
				break;
			case SchedulingFrequency.Hour:
				maxOffset = TimeSpan.FromHours(1.0);
				break;
			case SchedulingFrequency.Day:
				maxOffset = TimeSpan.FromDays(1.0);
				break;
			default:
				throw new CcsMalformedConfigurationException();
			}
			List<TimeSpan> list2;
			try
			{
				list2 = (from offset in scheduledTaskSettings.ExecutionFrequencyOffsets
					let s = offset.Split(new char[] { ':' })
					select new TimeSpan(int.Parse(s[0], CultureInfo.InvariantCulture), int.Parse(s[1], CultureInfo.InvariantCulture), int.Parse(s[2], CultureInfo.InvariantCulture)) into t
					orderby t
					select t).ToList<TimeSpan>();
			}
			catch (FormatException ex)
			{
				throw new CcsMalformedConfigurationException(string.Format(CultureInfo.CurrentCulture, "Wrong offset format for task '{0}'", new object[] { this.m_policyName }), ex);
			}
			catch (OverflowException ex2)
			{
				throw new CcsMalformedConfigurationException(string.Format(CultureInfo.CurrentCulture, "Offset for task '{0}' is out of range of int values.", new object[] { this.m_policyName }), ex2);
			}
			catch (ArgumentOutOfRangeException ex3)
			{
				throw new CcsMalformedConfigurationException(string.Format(CultureInfo.CurrentCulture, "Offset for task '{0}' is out of range of TimeSpan Values.", new object[] { this.m_policyName }), ex3);
			}
			if (list2.Any((TimeSpan s) => maxOffset <= s))
			{
				throw new CcsMalformedConfigurationException(string.Format(CultureInfo.CurrentCulture, "Offset for task '{0}' is over maximum timespan.", new object[] { this.m_policyName }));
			}
			if (list2.Count == 0)
			{
				throw new CcsMalformedConfigurationException(string.Format(CultureInfo.CurrentCulture, "Missing offsets for task '{0}'", new object[] { this.m_policyName }));
			}
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_frequency = executionsFrequency;
				this.m_offsets = list2;
				this.m_maxOffset = maxOffset;
				this.m_exclusionGroups = list;
				this.m_timeout = scheduledTaskSettings.Timeout;
				this.m_isCrashAllowedOnTimeout = scheduledTaskSettings.IsCrashAllowedOnTimeout;
				StringBuilder stringBuilder = new StringBuilder(string.Format(CultureInfo.InvariantCulture, "Policy '{0}' executed every {1} at ", new object[] { this.m_policyName, this.m_frequency }));
				foreach (TimeSpan timeSpan in this.m_offsets)
				{
					stringBuilder.AppendFormat("{0}, ", timeSpan);
				}
				stringBuilder.Append("with exclusion groups ");
				foreach (ExclusionGroupIdentifier exclusionGroupIdentifier in this.m_exclusionGroups)
				{
					stringBuilder.AppendFormat("{0}, ", exclusionGroupIdentifier);
				}
				TraceSourceBase<MonitoredUtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Updating configuration: {0}", new object[] { stringBuilder });
			}
			this.m_scheduler.RescheduleNextExecutionTime(this.m_taskName, ExtendedDateTime.UtcNow);
		}

		// Token: 0x04000332 RID: 818
		private IConfigurationManager m_cfgMgr;

		// Token: 0x04000333 RID: 819
		private object m_locker;

		// Token: 0x04000334 RID: 820
		private SchedulingFrequency m_frequency;

		// Token: 0x04000335 RID: 821
		private List<TimeSpan> m_offsets;

		// Token: 0x04000336 RID: 822
		private TimeSpan m_maxOffset;

		// Token: 0x04000337 RID: 823
		private List<ExclusionGroupIdentifier> m_exclusionGroups;

		// Token: 0x04000338 RID: 824
		private TimeSpan m_timeout;

		// Token: 0x04000339 RID: 825
		private bool m_isCrashAllowedOnTimeout;

		// Token: 0x0400033A RID: 826
		private readonly string m_policyName;

		// Token: 0x0400033B RID: 827
		private IRescheduleTaskExecution m_scheduler;

		// Token: 0x0400033C RID: 828
		private string m_taskName;
	}
}
