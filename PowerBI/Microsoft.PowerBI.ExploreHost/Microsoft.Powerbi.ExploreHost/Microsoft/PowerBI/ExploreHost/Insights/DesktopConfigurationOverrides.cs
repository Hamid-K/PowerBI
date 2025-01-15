using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Common;
using Microsoft.InfoNav.Experimental.Insights.ServiceContracts.Internal;
using Microsoft.PowerBI.ReportingServicesHost.Insights;

namespace Microsoft.PowerBI.ExploreHost.Insights
{
	// Token: 0x0200007E RID: 126
	internal sealed class DesktopConfigurationOverrides
	{
		// Token: 0x06000365 RID: 869 RVA: 0x0000AC0C File Offset: 0x00008E0C
		public static ConfigurationProviderOverrides GetConfigurationOverrides(bool isQuerySuggestions, InsightsSessionDataSourceType dataSourceType)
		{
			bool flag = dataSourceType > InsightsSessionDataSourceType.Local;
			int? num3;
			int? num4;
			int? num5;
			int? num6;
			int? num7;
			int? num8;
			bool? flag2;
			bool? flag3;
			int? num9;
			if (isQuerySuggestions)
			{
				IReadOnlyList<TimeSpan> readOnlyList = (flag ? DesktopConfigurationOverrides.querySuggestionsTimedRunDurations : DesktopConfigurationOverrides.querySuggestionsTimedRunDurations_Local);
				int num = (flag ? DesktopConfigurationOverrides.querySuggestion_MaxSimultaneousConnectionsAndThreads : DesktopConfigurationOverrides.querySuggestion_MaxSimultaneousConnectionsAndThreads_Local);
				int num2 = (flag ? DesktopConfigurationOverrides.querySuggestion_LongRunningAnalysisCommandTimeoutSeconds : DesktopConfigurationOverrides.querySuggestion_LongRunningAnalysisCommandTimeoutSeconds_Local);
				num3 = new int?(num);
				num4 = new int?(DesktopConfigurationOverrides.querySuggestion_LongRunningAnalysisCommandTimeoutSeconds);
				IReadOnlyList<TimeSpan> readOnlyList2 = readOnlyList;
				Optional<int?> optional = new int?(25000);
				num5 = new int?(DesktopConfigurationOverrides.maxNumberOfWorkerThreads);
				num6 = new int?(DesktopConfigurationOverrides.maxNumberOfQueriesPerRequest);
				num7 = new int?(num2);
				num8 = new int?(DesktopConfigurationOverrides.longRunningAnalysisMaxCommandMemoryInKB);
				flag2 = new bool?(DesktopConfigurationOverrides.ReportAllQueryErrorMessages);
				flag3 = new bool?(flag);
				num9 = null;
				int? num10 = num9;
				int? num11 = num5;
				num9 = null;
				int? num12 = num9;
				num9 = null;
				int? num13 = num9;
				num9 = null;
				int? num14 = num9;
				int? num15 = num3;
				int? num16 = num6;
				int? num17 = num7;
				int? num18 = num4;
				int? num19 = num8;
				num9 = null;
				return new ConfigurationProviderOverrides(num10, num11, num12, num13, num14, num15, num16, num17, num18, num19, num9, flag2, optional, flag3, readOnlyList2);
			}
			num8 = new int?(DesktopConfigurationOverrides.CommandTimeoutSeconds);
			num7 = new int?(DesktopConfigurationOverrides.maxNumberOfWorkerThreads);
			num6 = new int?(DesktopConfigurationOverrides.maxNumberOfThreadsPerComponent);
			num5 = new int?(DesktopConfigurationOverrides.maxSimultaneousConnections);
			num4 = new int?(DesktopConfigurationOverrides.maxNumberOfQueriesPerRequest);
			num3 = new int?(DesktopConfigurationOverrides.longRunningAnalysisConnectionTimeout);
			num9 = new int?(DesktopConfigurationOverrides.longRunningAnalysisCommandTimeout);
			int? num20 = new int?(DesktopConfigurationOverrides.longRunningAnalysisMaxCommandMemoryInKB);
			int? num21 = new int?(DesktopConfigurationOverrides.keyDriversSampleSize);
			flag3 = new bool?(DesktopConfigurationOverrides.ReportAllQueryErrorMessages);
			flag2 = new bool?(flag);
			return new ConfigurationProviderOverrides(null, num7, num6, null, num8, num5, num4, num3, num9, num20, num21, flag3, default(Optional<int?>), flag2, null);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000ADC0 File Offset: 0x00008FC0
		private static IReadOnlyList<T> Repeat<T>(T value, int times)
		{
			return (from i in Enumerable.Range(0, times)
				select value).ToList<T>();
		}

		// Token: 0x04000185 RID: 389
		private static readonly int CommandTimeoutSeconds = 3;

		// Token: 0x04000186 RID: 390
		private static readonly int maxNumberOfWorkerThreads = Environment.ProcessorCount;

		// Token: 0x04000187 RID: 391
		private static readonly int maxNumberOfThreadsPerComponent = Math.Max(1, Environment.ProcessorCount / 2);

		// Token: 0x04000188 RID: 392
		private static readonly int maxSimultaneousConnections = 16;

		// Token: 0x04000189 RID: 393
		private static readonly int maxNumberOfQueriesPerRequest = 3000;

		// Token: 0x0400018A RID: 394
		private static readonly int longRunningAnalysisConnectionTimeout = 60;

		// Token: 0x0400018B RID: 395
		private static readonly int longRunningAnalysisCommandTimeout = 225;

		// Token: 0x0400018C RID: 396
		private static readonly int longRunningAnalysisMaxCommandMemoryInKB = 1048576;

		// Token: 0x0400018D RID: 397
		private static readonly int keyDriversSampleSize = 10000;

		// Token: 0x0400018E RID: 398
		private static readonly int querySuggestion_MaxSimultaneousConnectionsAndThreads_Local = 6;

		// Token: 0x0400018F RID: 399
		private static readonly int querySuggestion_MaxSimultaneousConnectionsAndThreads = 5;

		// Token: 0x04000190 RID: 400
		private static readonly TimeSpan querySuggestion_TimedRunDuration_Local = TimeSpan.FromSeconds(6.0);

		// Token: 0x04000191 RID: 401
		private static readonly TimeSpan querySuggestion_TimedRunDuration = TimeSpan.FromSeconds(8.0);

		// Token: 0x04000192 RID: 402
		private static readonly int querySuggestion_TimedRunCount_Local = 9;

		// Token: 0x04000193 RID: 403
		private static readonly int querySuggestion_TimedRunCount = 7;

		// Token: 0x04000194 RID: 404
		private static readonly int querySuggestion_LongRunningAnalysisCommandTimeoutSeconds_Local = 40;

		// Token: 0x04000195 RID: 405
		private static readonly int querySuggestion_LongRunningAnalysisCommandTimeoutSeconds = 80;

		// Token: 0x04000196 RID: 406
		private const int statisticsProviderSampleSize = 25000;

		// Token: 0x04000197 RID: 407
		private static readonly bool ReportAllQueryErrorMessages = true;

		// Token: 0x04000198 RID: 408
		private static readonly IReadOnlyList<TimeSpan> querySuggestionsTimedRunDurations_Local = DesktopConfigurationOverrides.Repeat<TimeSpan>(DesktopConfigurationOverrides.querySuggestion_TimedRunDuration_Local, DesktopConfigurationOverrides.querySuggestion_TimedRunCount_Local);

		// Token: 0x04000199 RID: 409
		private static readonly IReadOnlyList<TimeSpan> querySuggestionsTimedRunDurations = DesktopConfigurationOverrides.Repeat<TimeSpan>(DesktopConfigurationOverrides.querySuggestion_TimedRunDuration, DesktopConfigurationOverrides.querySuggestion_TimedRunCount);
	}
}
