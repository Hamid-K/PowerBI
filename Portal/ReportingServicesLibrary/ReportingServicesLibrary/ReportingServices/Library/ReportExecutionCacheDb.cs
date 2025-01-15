using System;
using System.Data;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002A9 RID: 681
	internal class ReportExecutionCacheDb : Storage
	{
		// Token: 0x060018BC RID: 6332 RVA: 0x0006408B File Offset: 0x0006228B
		internal ReportExecutionCacheDb(RSService service)
		{
			this.m_service = service;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0006409C File Offset: 0x0006229C
		internal void GetCacheOptions(CatalogItemPath reportPath, out bool cacheReport, out ExpirationDefinition expiration)
		{
			cacheReport = false;
			expiration = null;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetCacheOptions", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@Path", reportPath.Value).SqlDbType = SqlDbType.NVarChar;
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (dataReader.Read())
					{
						cacheReport = true;
						int @int = dataReader.GetInt32(0);
						if (@int == 1)
						{
							expiration = new TimeExpiration
							{
								Minutes = dataReader.GetInt32(1)
							};
						}
						if (@int == 2)
						{
							ScheduleExpiration scheduleExpiration = new ScheduleExpiration();
							Microsoft.ReportingServices.Diagnostics.Task task = new Microsoft.ReportingServices.Diagnostics.Task(dataReader, 2, false);
							if (task.Type == ScheduleType.Shared)
							{
								scheduleExpiration.Schedule = ScheduleReference.TaskToThis(task);
							}
							else
							{
								scheduleExpiration.Schedule = ScheduleDefinition.TaskToThis(task);
							}
							expiration = scheduleExpiration;
						}
					}
				}
			}
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0006417C File Offset: 0x0006237C
		internal void SetCacheOptions(CatalogItemPath reportPath, Guid reportID, bool cacheReport, ExpirationDefinition expiration)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetCacheOptions", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@Path", reportPath.Value).SqlDbType = SqlDbType.NVarChar;
				instrumentedSqlCommand.Parameters.AddWithValue("@CacheReport", cacheReport).SqlDbType = SqlDbType.Bit;
				ReportExecutionCacheDb.ExpirationFlags expirationFlags = ReportExecutionCacheDb.ExpirationFlags.ExpiresOnTime;
				int num = 0;
				if (expiration is TimeExpiration)
				{
					num = ((TimeExpiration)expiration).Minutes;
				}
				instrumentedSqlCommand.Parameters.AddWithValue("@CacheExpiration", num).SqlDbType = SqlDbType.Int;
				ScheduleDefinitionOrReference scheduleDefinitionOrReference = null;
				if (expiration is ScheduleExpiration)
				{
					scheduleDefinitionOrReference = ((ScheduleExpiration)expiration).Schedule;
				}
				if (this.SetReportSchedule(reportID, scheduleDefinitionOrReference, ReportScheduleActions.InvalidateCache) != Guid.Empty)
				{
					expirationFlags = ReportExecutionCacheDb.ExpirationFlags.ExpiresOnSchedule;
				}
				instrumentedSqlCommand.Parameters.AddWithValue("@ExpirationFlags", expirationFlags).SqlDbType = SqlDbType.Int;
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00064274 File Offset: 0x00062474
		internal void SetExecutionOptionsIfChanged(CatalogItemPath reportPath, int newExecOptions, int existingExecOptions)
		{
			if (newExecOptions != existingExecOptions)
			{
				bool flag = (newExecOptions & ExecutionOptions.LiveOrSnapshotMask) != (existingExecOptions & ExecutionOptions.LiveOrSnapshotMask);
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetExecutionOptions", null))
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@Path", reportPath.Value).SqlDbType = SqlDbType.NVarChar;
					instrumentedSqlCommand.Parameters.AddWithValue("@ExecutionFlag", newExecOptions).SqlDbType = SqlDbType.Int;
					instrumentedSqlCommand.Parameters.AddWithValue("@ExecutionChanged", flag).SqlDbType = SqlDbType.Bit;
					instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00064324 File Offset: 0x00062524
		internal virtual void GetExecutionOptions(CatalogItemPath reportPath, Guid reportID, out ExecutionSettingEnum executionSetting, out ScheduleDefinitionOrReference schedule)
		{
			schedule = null;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetExecutionOptions", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@Path", reportPath.Value).SqlDbType = SqlDbType.NVarChar;
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						throw new ItemNotFoundException(reportPath.Value);
					}
					this.ExtractExecutionOptionsFromValidReader(dataReader, out executionSetting, out schedule);
				}
			}
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x000643B8 File Offset: 0x000625B8
		protected void ExtractExecutionOptionsFromValidReader(IDataRecord record, out ExecutionSettingEnum executionSetting, out ScheduleDefinitionOrReference schedule)
		{
			schedule = null;
			int @int = record.GetInt32(0);
			executionSetting = ExecutionOptions.ToExecutionSettingEnum(@int);
			if (ExecutionOptions.IsSnapshotExecution(@int))
			{
				if (record.IsDBNull(1))
				{
					schedule = new NoSchedule();
					return;
				}
				Microsoft.ReportingServices.Diagnostics.Task task = new Microsoft.ReportingServices.Diagnostics.Task(record, 1, false);
				if (task.Type == ScheduleType.Shared)
				{
					schedule = ScheduleReference.TaskToThis(task);
					return;
				}
				schedule = ScheduleDefinition.TaskToThis(task);
			}
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00064413 File Offset: 0x00062613
		public Guid SetReportSchedule(Guid reportID, ScheduleDefinitionOrReference scheduleData, ReportScheduleActions reportAction)
		{
			return new ScheduleCoordinator(this.UserContext)
			{
				ConnectionManager = this.ConnectionManager
			}.SetReportSchedule(reportID, scheduleData, reportAction);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00064434 File Offset: 0x00062634
		internal virtual void UpdateSnapshot(CatalogItemContext reportContext, Guid reportID, DateTime executionDate, ReportSnapshot snapshotData)
		{
			string value = reportContext.CatalogItemPath.Value;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("UpdateSnapshot", null))
			{
				instrumentedSqlCommand.Parameters.Add("@Path", SqlDbType.NVarChar).Value = value;
				instrumentedSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier).Value = snapshotData.SnapshotDataID;
				instrumentedSqlCommand.Parameters.Add("@executionDate", SqlDbType.DateTime).Value = executionDate;
				instrumentedSqlCommand.ExecuteNonQuery();
			}
			new EventManager
			{
				ConnectionManager = this.ConnectionManager
			}.FireSnapShotCreatedEvent(reportID);
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x000644EC File Offset: 0x000626EC
		internal void FlushCache(CatalogItemPath reportPath)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("FlushReportFromCache", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@Path", reportPath.Value).SqlDbType = SqlDbType.NVarChar;
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00064548 File Offset: 0x00062748
		internal void FlushCacheById(Guid itemId)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("FlushCacheByID", null))
			{
				instrumentedSqlCommand.Parameters.Add("@ItemID", SqlDbType.UniqueIdentifier).Value = itemId;
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x000645A4 File Offset: 0x000627A4
		protected RSService Service
		{
			get
			{
				return this.m_service;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x000645AC File Offset: 0x000627AC
		protected UserContext UserContext
		{
			get
			{
				return this.Service.UserContext;
			}
		}

		// Token: 0x040008EB RID: 2283
		private RSService m_service;

		// Token: 0x020004D5 RID: 1237
		internal enum ExpirationFlags
		{
			// Token: 0x04001122 RID: 4386
			ExpiresOnTime = 1,
			// Token: 0x04001123 RID: 4387
			ExpiresOnSchedule
		}
	}
}
