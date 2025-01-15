using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Internal;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B6 RID: 182
	internal interface IRSEventProvider
	{
		// Token: 0x060005F1 RID: 1521
		void NotifyCrescentClientTraceEvent(TraceLevel traceLevel, string traceMsg);

		// Token: 0x060005F2 RID: 1522
		void NotifyReportServerDumpEvent(Exception exception, string assertionMessage, bool unhandledException);

		// Token: 0x060005F3 RID: 1523
		void NotifyReportServerUniqueDumpEvent(Exception exception, string assertionMessage, bool unhandledException);

		// Token: 0x060005F4 RID: 1524
		void NotifyClientActivitiesParsingFailure(int numClientActivitiesSent, int numClientActivitiesLogged);

		// Token: 0x060005F5 RID: 1525
		void NotifyReportServerApiVersion(string command, string groupVersion, Version apiVersion);

		// Token: 0x060005F6 RID: 1526
		void NotifyPowerViewQueryPattern(string pattern, string reasons, Version dataShapeProcessingVersion);

		// Token: 0x060005F7 RID: 1527
		void NotifyRestoreSession(long sessionSize, string sessionId);

		// Token: 0x060005F8 RID: 1528
		void NotifySerializeSession(long sessionSize, string sessionId);

		// Token: 0x060005F9 RID: 1529
		void NotifySessionEvicted(string sessionId, string reason);

		// Token: 0x060005FA RID: 1530
		void NotifyAttemptRestoreSession(string sessionId, string reason, string outcome);

		// Token: 0x060005FB RID: 1531
		void NotifyPowerViewOverlappingKeysOnOppositeAxesError();

		// Token: 0x060005FC RID: 1532
		void NotifyModelRetrieval(string caller, bool isExternalModel, bool isMultiDimensional, string dataSource, string database, string username, NullableBool isConnectionFromPool, int modelSize, NullableBool modelCacheHit, NullableBool stringCacheHit, bool hasError, string error, long totalTime, long resolutionTime, long connectionOpenTime, long retrievalTime, long parsingTime, string puid, NullableBool cloudCacheHit, long modelId);

		// Token: 0x060005FD RID: 1533
		void NotifyModelCacheLastUpdatedTimeMismatch(string methodName, bool cacheEntryHasLastUpdatedTime, bool requestHasLastUpdatedTime);

		// Token: 0x060005FE RID: 1534
		void NotifyDataShapingEvent(string eventName, string properties);

		// Token: 0x060005FF RID: 1535
		void NotifyDataTransformEvent(string eventName, string properties);
	}
}
