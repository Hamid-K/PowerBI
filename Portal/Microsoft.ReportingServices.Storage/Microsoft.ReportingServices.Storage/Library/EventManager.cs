using System;
using System.Data;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200000D RID: 13
	internal class EventManager : Storage
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00004928 File Offset: 0x00002B28
		internal void FireEvent(string eventType, string eventData)
		{
			if (!Globals.Configuration.IsSupportedEvent(eventType))
			{
				throw new UnknownEventTypeException(eventType);
			}
			if (eventType == "ReportHistorySnapshotCreated" || eventType == "SnapshotCreated" || eventType == "SnapshotUpdated")
			{
				throw new UnknownEventTypeException(eventType);
			}
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("AddEvent", null))
			{
				instrumentedSqlCommand.AddParameter("@EventType", SqlDbType.NVarChar, eventType);
				instrumentedSqlCommand.AddParameter("@EventData", SqlDbType.NVarChar, eventData);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000049C8 File Offset: 0x00002BC8
		internal void FireSnapShotCreatedEvent(Guid reportID)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("AddEvent", null))
			{
				instrumentedSqlCommand.AddParameter("@EventType", SqlDbType.NVarChar, "SnapshotUpdated");
				instrumentedSqlCommand.AddParameter("@EventData", SqlDbType.NVarChar, reportID.ToString());
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004A34 File Offset: 0x00002C34
		internal void FireSharedDatasetCacheUpdate(Guid id)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("AddEvent", null))
			{
				instrumentedSqlCommand.AddParameter("@EventType", SqlDbType.NVarChar, "SharedDatasetCacheUpdate");
				instrumentedSqlCommand.AddParameter("@EventData", SqlDbType.NVarChar, id.ToString());
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}
	}
}
