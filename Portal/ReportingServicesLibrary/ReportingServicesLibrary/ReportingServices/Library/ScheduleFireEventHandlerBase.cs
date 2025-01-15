using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Extensions;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200021F RID: 543
	internal abstract class ScheduleFireEventHandlerBase
	{
		// Token: 0x0600134D RID: 4941 RVA: 0x000454B8 File Offset: 0x000436B8
		private Guid StartSchedule(ICatalogQuery catalogQuery, string eventData, out ArrayList reports)
		{
			SchedulingDBInterface schedulingDBInterface = new SchedulingDBInterface();
			schedulingDBInterface.ConnectionManager = ((CatalogQuery)catalogQuery).ConnectionManager;
			this.m_startTime = DateTime.Now;
			reports = new ArrayList();
			ScheduleFireEventHandlerBase.RetrievalCommand retrievalCommand = this.ReportActionRetrievalCommand(eventData);
			using (IDataReader dataReader = catalogQuery.ExecuteReader(retrievalCommand.SqlCommand, retrievalCommand.Parameters, retrievalCommand.CommandType))
			{
				while (dataReader.Read())
				{
					ItemScheduleAction itemScheduleAction = new ItemScheduleAction(dataReader, true);
					reports.Add(itemScheduleAction);
				}
			}
			catalogQuery.Commit();
			Guid guid = this.ScheduleID(reports, eventData);
			if (guid != Guid.Empty)
			{
				Task task = schedulingDBInterface.GetTask(guid);
				task.IsRunning = true;
				schedulingDBInterface.UpdateTaskProperties(task, false);
				catalogQuery.Commit();
				if (Global.m_Tracer.TraceInfo)
				{
					Global.m_Tracer.Trace(TraceLevel.Info, "Schedule {0} executed at {1}.", new object[]
					{
						task.ID,
						DateTime.Now
					});
				}
			}
			return guid;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x000455C4 File Offset: 0x000437C4
		private void FinishSchedule(ICatalogQuery catalogQuery, Guid scheduleID)
		{
			SchedulingDBInterface schedulingDBInterface = new SchedulingDBInterface();
			schedulingDBInterface.ConnectionManager = ((CatalogQuery)catalogQuery).ConnectionManager;
			Task task = schedulingDBInterface.GetTask(scheduleID);
			task.IsRunning = false;
			task.LastRunTime = this.m_startTime;
			task.LastRunStatus = "";
			task.NextRunTime = DateTime.MinValue;
			task.ScheduleState = TaskState.Ready;
			schedulingDBInterface.UpdateTaskProperties(task, false);
			if (Global.m_Tracer.TraceInfo)
			{
				Global.m_Tracer.Trace(TraceLevel.Info, "Schedule {0} execution completed at {1}.", new object[]
				{
					scheduleID,
					DateTime.Now
				});
			}
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00045660 File Offset: 0x00043860
		private void FailSchedule(ICatalogQuery catalogQuery, Guid scheduleID, string errorMessage)
		{
			SchedulingDBInterface schedulingDBInterface = new SchedulingDBInterface();
			schedulingDBInterface.ConnectionManager = ((CatalogQuery)catalogQuery).ConnectionManager;
			Task task = null;
			try
			{
				task = schedulingDBInterface.GetTask(scheduleID);
			}
			catch (ItemNotFoundException)
			{
				return;
			}
			task.LastRunTime = this.m_startTime;
			task.IsRunning = false;
			task.LastRunStatus = "Fail";
			if (Global.m_Tracer.TraceError)
			{
				Global.m_Tracer.Trace(TraceLevel.Error, "Schedule {0} failed. Error {1}", new object[] { task.ID, errorMessage });
			}
			task.NextRunTime = DateTime.MinValue;
			schedulingDBInterface.UpdateTaskProperties(task, false);
		}

		// Token: 0x06001350 RID: 4944
		protected abstract ScheduleFireEventHandlerBase.RetrievalCommand ReportActionRetrievalCommand(string eventData);

		// Token: 0x06001351 RID: 4945 RVA: 0x00045708 File Offset: 0x00043908
		protected virtual Guid ScheduleID(ArrayList reportActions, string eventData)
		{
			if (reportActions.Count > 0)
			{
				return ((ItemScheduleAction)reportActions[0]).ScheduleID;
			}
			return Guid.Empty;
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0004572C File Offset: 0x0004392C
		protected void HandleScheduleEvent(ICatalogQuery catalogQuery, string eventData, ScheduleFireEventHandlerBase.PerformEventActions handleFunction)
		{
			this.InternalHandleScheduleEvent(catalogQuery, eventData, delegate(ICatalogQuery q, ArrayList ra, string ed)
			{
				handleFunction(q, ra);
			});
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0004575C File Offset: 0x0004395C
		protected void HandleScheduleEvent(ICatalogQuery catalogQuery, string eventData, ScheduleFireEventHandlerBase.PerformEventActions2 handleFunction)
		{
			this.InternalHandleScheduleEvent(catalogQuery, eventData, delegate(ICatalogQuery q, ArrayList ra, string ed)
			{
				handleFunction(q, ra, ed);
			});
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0004578C File Offset: 0x0004398C
		private void InternalHandleScheduleEvent(ICatalogQuery catalogQuery, string eventData, Action<ICatalogQuery, ArrayList, string> handleFunction)
		{
			Guid guid = Guid.Empty;
			try
			{
				ArrayList arrayList;
				guid = this.StartSchedule(catalogQuery, eventData, out arrayList);
				if (guid == Guid.Empty)
				{
					if (RSTrace.ScheduleTracer.TraceWarning)
					{
						RSTrace.ScheduleTracer.Trace(TraceLevel.Warning, "An event schedule fired that does not exists in the report server database");
					}
				}
				else
				{
					handleFunction(catalogQuery, arrayList, eventData);
					this.FinishSchedule(catalogQuery, guid);
				}
			}
			catch (ScheduleNotFoundException)
			{
			}
			catch (ReportServerStorageException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (!(guid != Guid.Empty))
				{
					throw;
				}
				this.FailSchedule(catalogQuery, guid, Global.m_Tracer.TraceError ? ex.ToString() : ex.Message);
			}
		}

		// Token: 0x040006E5 RID: 1765
		private DateTime m_startTime = DateTime.MinValue;

		// Token: 0x0200047C RID: 1148
		// (Invoke) Token: 0x060023A2 RID: 9122
		protected delegate void PerformEventActions(ICatalogQuery catalogQuery, ArrayList reportActions);

		// Token: 0x0200047D RID: 1149
		// (Invoke) Token: 0x060023A6 RID: 9126
		protected delegate void PerformEventActions2(ICatalogQuery catalogQuery, ArrayList reportActions, string catalogId);

		// Token: 0x0200047E RID: 1150
		protected class RetrievalCommand
		{
			// Token: 0x04000FF7 RID: 4087
			public string SqlCommand = "";

			// Token: 0x04000FF8 RID: 4088
			public Hashtable Parameters = new Hashtable();

			// Token: 0x04000FF9 RID: 4089
			public CommandType CommandType = CommandType.StoredProcedure;
		}
	}
}
