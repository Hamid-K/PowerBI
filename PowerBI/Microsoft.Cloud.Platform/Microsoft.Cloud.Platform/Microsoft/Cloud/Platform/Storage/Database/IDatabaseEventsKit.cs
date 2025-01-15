using System;
using System.Diagnostics;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000030 RID: 48
	[EventsKit(3412550397702103433L)]
	[Trace(typeof(StorageTrace))]
	[PublishedEvent]
	public interface IDatabaseEventsKit
	{
		// Token: 0x06000103 RID: 259
		[PerformanceCounter(551440516, "RateOfStorageCommands", CounterModifier.Increment, CounterType.RateOfCountsPerSecond)]
		[Event(2058965226453672925L, 1, Level = EventLevel.Informational, Version = 1)]
		void NotifyDatabaseAccess(string serverName, string databaseName, string commandName, long throttleDurationMs, long connectDurationMs, long requestDurationMs, long replyDurationMs, int connectTryCount, long requestTryCount, long replyRowCount, long totalDurationMs, string finalStage);

		// Token: 0x06000104 RID: 260
		[Event(8478155798253327003L, 2, Level = EventLevel.Error, Format = "Storage command '{2}' failed on {0}/{1} due to: {3}", Version = 1)]
		[FilteredWindowsEventLog(3004)]
		void NotifyCommandExecuteError(string serverName, string databaseName, string commandName, MonitoredException exception);

		// Token: 0x06000105 RID: 261
		[Event(7475805171163038438L, 3, Level = EventLevel.Error, Format = "Storage command '{2}' failed with error code '{3}' on {0}/{1}  due to: {4}", Version = 1)]
		[FilteredWindowsEventLog(3002)]
		void NotifyCommandExecuteCriticalError(string serverName, string databaseName, string commandName, int errorCode, MonitoredException exception);

		// Token: 0x06000106 RID: 262
		[PerformanceCounter(1594293467, "NumberOfRetryAttemptsForStorageCommands", CounterModifier.Increment, CounterType.CounterDelta)]
		[Event(6566669757032282198L, 4, Level = EventLevel.Warning, Format = "Storage command '{2}' on {0}/{1} is being retried due to: {3}. Rety #{4}", Version = 1)]
		void NotifyQueryRetry(string serverName, string databaseName, string commandName, string reason, int tryCount, long tryDuration);

		// Token: 0x06000107 RID: 263
		[PerformanceCounter(417256183, "NumberOfRetryAttemptsToOpenConnection", CounterModifier.Increment, CounterType.CounterDelta)]
		[Event(7824715648484525745L, 5, Level = EventLevel.Warning, Format = "Connection open for storage command '{2}' on {0}/{1} is being retried due to: {3}. Rety #{4}", Version = 1)]
		void NotifyCommandConnectionOpenRetry(string serverName, string databaseName, string commandName, string reason, int tryCount, long tryDuration);

		// Token: 0x06000108 RID: 264
		[Event(3511550402565874271L, 6, Level = EventLevel.Critical, Format = "Storage specification '{0}': not available", Version = 1)]
		[WindowsEventLog(EventLogEntryType.Error, 3006)]
		void NotifyMissingStorageSpecification(string specification, MonitoredException exception);

		// Token: 0x06000109 RID: 265
		[Event(2314238858175141691L, 7, Level = EventLevel.Error, Format = "Storage specification '{0}': throttler has overflowed", Version = 1)]
		[FilteredWindowsEventLog(3003)]
		void NotifyStorageThrottleOverflow(string specification, MonitoredException exception);

		// Token: 0x0600010A RID: 266
		[Event(4862725982625058653L, 8, Level = EventLevel.Error, Format = "Storage command '{2}' on {0}/{1} timed out after {3}ms", Version = 1)]
		[FilteredWindowsEventLog(3005)]
		void NotifyCommandTimeout(string serverName, string databaseName, string commandName, long timeoutMs, MonitoredException exception);
	}
}
