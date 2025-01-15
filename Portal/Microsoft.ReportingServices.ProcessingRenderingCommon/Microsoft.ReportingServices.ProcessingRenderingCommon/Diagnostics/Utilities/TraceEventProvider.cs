using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Internal;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B8 RID: 184
	internal sealed class TraceEventProvider : IRSEventProvider
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x00011961 File Offset: 0x0000FB61
		public void NotifyReportServerDumpEvent(Exception exception, string assertionMessage, bool unhandledException)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Report server dump occured. Exception: {0}, Message: {1}, Unhandled Exception: {2}", new object[] { exception, assertionMessage, unhandledException });
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001198A File Offset: 0x0000FB8A
		public void NotifyReportServerUniqueDumpEvent(Exception exception, string assertionMessage, bool unhandledException)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Report server unique dump occured. Exception: {0}, Message: {1}, Unhandled Exception: {2}", new object[] { exception, assertionMessage, unhandledException });
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000119B3 File Offset: 0x0000FBB3
		public void NotifyClientActivitiesParsingFailure(int numClientActivitiesSent, int numClientActivitiesLogged)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Failed to parse all all client activities, sent {0} activities and parsed {1}", new object[] { numClientActivitiesSent, numClientActivitiesLogged });
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000119DD File Offset: 0x0000FBDD
		public void NotifyReportServerApiVersion(string command, string groupVersion, Version apiVersion)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "PowerView API: Command={0}, GroupVersion={1}, Api-Version={2}", new object[] { command, groupVersion, apiVersion });
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00011A01 File Offset: 0x0000FC01
		public void NotifyPowerViewQueryPattern(string pattern, string reasons, Version dataShapeProcessingVersion)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Power View Query Pattern: Pattern={0}, Reasons={1}, DataShapeProcessingVersion={2}", new object[] { pattern, reasons, dataShapeProcessingVersion });
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00011A25 File Offset: 0x0000FC25
		public void NotifyRestoreSession(long sessionSize, string sessionId)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Restore Session: Size={0}, SessionId={1}", new object[] { sessionSize, sessionId });
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00011A4A File Offset: 0x0000FC4A
		public void NotifySerializeSession(long sessionSize, string sessionId)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Serialize Session: Size={0}, SessionId={1}", new object[] { sessionSize, sessionId });
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00011A6F File Offset: 0x0000FC6F
		public void NotifySessionEvicted(string sessionId, string reason)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Session Evicted: SessionId={0}, Reason={1}", new object[] { sessionId, reason });
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00011A8F File Offset: 0x0000FC8F
		public void NotifyCrescentClientTraceEvent(TraceLevel traceLevel, string traceMsg)
		{
			RSTrace.CatalogTrace.Trace(traceLevel, "Crescent client trace: level: {0}, message: {1}", new object[] { traceLevel, traceMsg });
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00011AB4 File Offset: 0x0000FCB4
		public void NotifyAttemptRestoreSession(string sessionId, string reason, string outcome)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Attempt Restore Session: SessionId={0}, Reason={1}, Outcome={2}", new object[] { sessionId, reason, outcome });
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		public void NotifyPowerViewOverlappingKeysOnOppositeAxesError()
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Power View Overlapping Keys On Opposite Axes Error");
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00011AEC File Offset: 0x0000FCEC
		public void NotifyModelRetrieval(string caller, bool isExternalModel, bool isMultiDimensional, string dataSource, string database, string username, NullableBool isConnectionFromPool, int modelSize, NullableBool modelCacheHit, NullableBool stringCacheHit, bool hasError, string error, long totalTime, long resolutionTime, long connectionOpenTime, long retrievalTime, long parsingTime, string puid, NullableBool cloudCacheHit, long modelId)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "Caller: {0}, IsOnPrem: {1}, IsMD: {2}, DataSourceName: {3}, DbName: {4}, UserName: {5}, IsConnectionFromPool: {6}, Model size: {7}, ModelCacheHit: {8}, StringCacheHit: {9}, HasError: {10}, Error: {11}, Total time: {12}ms, Resolution time: {13}ms, Connection open time: {14}ms, Retrieval time: {15}ms, Parsing time: {16}ms, Puid: {17}, CloudCacheHit: {18}, ModelId: {19}", new object[]
			{
				caller, isExternalModel, isMultiDimensional, dataSource, database, username, isConnectionFromPool, modelSize, modelCacheHit, stringCacheHit,
				hasError, error, totalTime, resolutionTime, connectionOpenTime, retrievalTime, parsingTime, puid, cloudCacheHit, modelId
			});
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, text);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00011BCE File Offset: 0x0000FDCE
		public void NotifyModelCacheLastUpdatedTimeMismatch(string methodName, bool cacheEntryHasLastUpdatedTime, bool requestHasLastUpdatedTime)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Method {0}: cacheEntryHasLastUpdatedTime={1}, requestHasLastUpdatedTime={2}", new object[] { methodName, cacheEntryHasLastUpdatedTime, requestHasLastUpdatedTime });
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00011BFC File Offset: 0x0000FDFC
		public void NotifyDataShapingEvent(string eventName, string properties)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "DataShapingEvent: {0}, Properties: {1}", new object[] { eventName, properties });
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00011C1C File Offset: 0x0000FE1C
		public void NotifyDataTransformEvent(string eventName, string properties)
		{
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, "DataTransformEvent: {0}, Properties: {1}", new object[] { eventName, properties });
		}
	}
}
