using System;
using System.Collections;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000220 RID: 544
	internal class SchedulingDBInterface : Storage
	{
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x000066AB File Offset: 0x000048AB
		// (set) Token: 0x06001357 RID: 4951 RVA: 0x00045872 File Offset: 0x00043A72
		public override ConnectionManager ConnectionManager
		{
			get
			{
				return base.ConnectionManager;
			}
			set
			{
				base.ConnectionManager = value;
				this.m_sqlSchedule.ConnectionManager = value;
			}
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00045888 File Offset: 0x00043A88
		public void CreateTask(Task task)
		{
			this.ThrowIfSchedulerNotRunning();
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CreateTask", null))
			{
				if (task.Name == null || task.Name.Length == 0)
				{
					Global.m_Tracer.Assert(task.Type == ScheduleType.Scoped, "Only scoped schedules can not have names");
					task.Name = Guid.NewGuid().ToString();
				}
				RSTrace.CatalogTrace.Assert(task.Creator != null);
				instrumentedSqlCommand.AddParameter("@Name", SqlDbType.NVarChar, task.Name);
				instrumentedSqlCommand.Parameters.AddWithValue("@Type", task.Type);
				instrumentedSqlCommand.Parameters.AddWithValue("@UserSid", Global.NameToSid(task.Creator)).SqlDbType = SqlDbType.VarBinary;
				instrumentedSqlCommand.AddParameter("@UserName", SqlDbType.NVarChar, task.Creator.UserName);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)task.Creator.AuthenticationType);
				instrumentedSqlCommand.AddParameter("@EventData", SqlDbType.NVarChar, task.EventData);
				instrumentedSqlCommand.AddParameter("@EventType", SqlDbType.NVarChar, task.EventType);
				instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, ItemPathBase.SafeValue(task.Path));
				this.SetSqlParameters(instrumentedSqlCommand, task, true);
				try
				{
					instrumentedSqlCommand.ExecuteNonQuery();
				}
				catch (ReportServerStorageException ex)
				{
					if (ex.IsSqlException && ex.SqlErrorMessage == "Schedule already exists")
					{
						throw new ScheduleAlreadyExists(task.Name);
					}
					throw;
				}
			}
			this.m_sqlSchedule.CreateTask(task);
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00045A54 File Offset: 0x00043C54
		public Task GetTask(Guid id)
		{
			bool isSchedulerRunning = this.m_sqlSchedule.IsSchedulerRunning;
			Task task = null;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetTaskProperties", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ScheduleID", id);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (!dataReader.Read())
					{
						throw new ScheduleNotFoundException(id.ToString());
					}
					task = new Task(dataReader, 0, true);
					if (!isSchedulerRunning)
					{
						task.IsFailing = true;
					}
				}
			}
			this.CheckTasksNextRunTime(task);
			return task;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00045B04 File Offset: 0x00043D04
		private void CheckTasksNextRunTime(Task t)
		{
			if (t == null)
			{
				return;
			}
			if (t.ScheduleState == TaskState.Paused)
			{
				return;
			}
			if (!(t.NextRunTime == SchedulingDBInterface._UnknowNextRunTime))
			{
				if (t.NextRunTime == SchedulingDBInterface._NoNextRunTime)
				{
					t.NextRunTime = DateTime.MinValue;
				}
				return;
			}
			DateTime dateTime = this.SetSchedulesNextRunTime(t);
			if (dateTime == SchedulingDBInterface._NoNextRunTime)
			{
				t.NextRunTime = DateTime.MinValue;
				return;
			}
			t.NextRunTime = dateTime;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00045B78 File Offset: 0x00043D78
		public DateTime SetSchedulesNextRunTime(Task t)
		{
			DateTime dateTime = SchedulingDBInterface._NoNextRunTime;
			if (t.Trigger.EndDate == DateTime.MinValue || DateTime.Now.Date < t.Trigger.EndDate)
			{
				dateTime = this.m_sqlSchedule.GetNextRunTime(t.ID);
			}
			else
			{
				dateTime = SchedulingDBInterface._NoNextRunTime;
			}
			if (dateTime == DateTime.MinValue)
			{
				t.NextRunTime = SchedulingDBInterface._NoNextRunTime;
			}
			else
			{
				t.NextRunTime = dateTime;
			}
			this.UpdateTaskProperties(t, false);
			return t.NextRunTime;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00045C0A File Offset: 0x00043E0A
		public bool IsSchedulerRunning()
		{
			return this.m_sqlSchedule.IsSchedulerRunning;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00045C17 File Offset: 0x00043E17
		public void ThrowIfSchedulerNotRunning()
		{
			if (!this.m_sqlSchedule.IsSchedulerRunning)
			{
				throw new SchedulerNotRespondingException();
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00045C2C File Offset: 0x00043E2C
		public Task GetCacheSchedule(Guid reportID, out ReportScheduleActions action)
		{
			action = ReportScheduleActions.None;
			bool flag = this.IsSchedulerRunning();
			Task task = null;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetCacheSchedule", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ReportID", reportID);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (dataReader.Read())
					{
						task = new Task(dataReader, 0, true);
						if (!flag)
						{
							task.IsFailing = true;
						}
						action = (ReportScheduleActions)dataReader.GetInt32(25);
					}
				}
			}
			this.CheckTasksNextRunTime(task);
			return task;
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00045CD0 File Offset: 0x00043ED0
		public Task GetSnapShotSchedule(Guid reportID)
		{
			bool isSchedulerRunning = this.m_sqlSchedule.IsSchedulerRunning;
			Task task = null;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetSnapShotSchedule", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ReportID", reportID);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (dataReader.Read())
					{
						task = new Task(dataReader, 0, true);
						if (!isSchedulerRunning)
						{
							task.IsFailing = true;
						}
					}
				}
			}
			this.CheckTasksNextRunTime(task);
			return task;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00045D6C File Offset: 0x00043F6C
		public DateTime CheckNextRunTime(Guid scheduleID, DateTime nextRunTime)
		{
			DateTime dateTime = DateTime.MinValue;
			if (nextRunTime == SchedulingDBInterface._UnknowNextRunTime)
			{
				dateTime = this.m_sqlSchedule.GetNextRunTime(scheduleID);
				DateTime dateTime2 = DateTime.MinValue;
				if (dateTime == DateTime.MinValue)
				{
					dateTime2 = SchedulingDBInterface._NoNextRunTime;
				}
				else
				{
					dateTime2 = dateTime;
				}
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("UpdateScheduleNextRunTime", null))
				{
					SqlParameter sqlParameter = new SqlParameter("@ScheduleID", SqlDbType.UniqueIdentifier);
					sqlParameter.Value = scheduleID;
					instrumentedSqlCommand.Parameters.Add(sqlParameter);
					sqlParameter = new SqlParameter("@NextRunTime", SqlDbType.DateTime);
					sqlParameter.Value = dateTime2;
					instrumentedSqlCommand.Parameters.Add(sqlParameter);
					instrumentedSqlCommand.ExecuteNonQuery();
					return dateTime;
				}
			}
			if (nextRunTime != SchedulingDBInterface._NoNextRunTime)
			{
				dateTime = nextRunTime;
			}
			return dateTime;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00045E44 File Offset: 0x00044044
		public void RemoveReportFromSchedule(Guid scheduleID, Guid reportID, Guid subscriptionID, ReportScheduleActions action)
		{
			this.ThrowIfSchedulerNotRunning();
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteReportSchedule", null))
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@ReportID", reportID);
					instrumentedSqlCommand.Parameters.AddWithValue("@ScheduleID", scheduleID);
					instrumentedSqlCommand.Parameters.AddWithValue("@ReportAction", (int)action);
					if (subscriptionID != Guid.Empty)
					{
						instrumentedSqlCommand.Parameters.AddWithValue("@SubscriptionID", subscriptionID);
					}
					instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			catch (ReportServerStorageException ex)
			{
				if (!ex.IsSqlException || ex.SqlErrorNumber != 14262)
				{
					throw;
				}
			}
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00045F1C File Offset: 0x0004411C
		public void AddReportToSchedule(ItemScheduleAction ra)
		{
			this.ThrowIfSchedulerNotRunning();
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("AddReportSchedule", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ScheduleID", ra.ScheduleID);
				instrumentedSqlCommand.Parameters.AddWithValue("@ReportID", ra.ItemID);
				if (ra.SubscriptionID != Guid.Empty)
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@SubscriptionID", ra.SubscriptionID);
				}
				instrumentedSqlCommand.Parameters.AddWithValue("@Action", ra.Action);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00045FE4 File Offset: 0x000441E4
		public void CreateTimeBasedSubscriptionSchedule(CatalogItemPath reportName, Guid subscriptionID, ReportScheduleActions action, Task task)
		{
			this.ThrowIfSchedulerNotRunning();
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("CreateTimeBasedSubscriptionSchedule", null))
			{
				instrumentedSqlCommand.AddParameter("@ItemPath", SqlDbType.NVarChar, reportName.Value);
				instrumentedSqlCommand.Parameters.AddWithValue("@UserSid", Global.NameToSid(task.Creator));
				instrumentedSqlCommand.AddParameter("@UserName", SqlDbType.NVarChar, task.Creator.UserName);
				instrumentedSqlCommand.Parameters.AddWithValue("@AuthType", (int)task.Creator.AuthenticationType);
				instrumentedSqlCommand.AddParameter("@Schedule_Name", SqlDbType.NVarChar, Guid.NewGuid().ToString());
				instrumentedSqlCommand.AddParameter("@EventData", SqlDbType.NVarChar, task.EventData);
				instrumentedSqlCommand.AddParameter("@EventType", SqlDbType.NVarChar, task.EventType);
				instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, ItemPathBase.SafeValue(task.Path));
				instrumentedSqlCommand.AddParameter("@Action", SqlDbType.Int, action);
				instrumentedSqlCommand.Parameters.AddWithValue("@SubscriptionID", subscriptionID);
				this.SetSqlParameters(instrumentedSqlCommand, task, true);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
			this.m_sqlSchedule.CreateTask(task);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00046148 File Offset: 0x00044348
		public Task GetTimeBasedSubscriptionSchedule(Guid subscriptionID)
		{
			bool flag = this.IsSchedulerRunning();
			Task task = null;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("GetTimeBasedSubscriptionSchedule", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@SubscriptionID", subscriptionID);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (dataReader.Read())
					{
						task = new Task(dataReader, 0, true);
						if (!flag)
						{
							task.IsFailing = true;
						}
					}
				}
			}
			this.CheckTasksNextRunTime(task);
			return task;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x000461E0 File Offset: 0x000443E0
		public void DeleteTimeBasedSubscriptionSchedule(Guid SubscriptionID)
		{
			try
			{
				this.ThrowIfSchedulerNotRunning();
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteTimeBasedSubscriptionSchedule", null))
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@SubscriptionID", SubscriptionID);
					instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			catch (ReportServerStorageException ex)
			{
				if (!ex.IsSqlException || ex.SqlErrorNumber != 14262)
				{
					throw;
				}
			}
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00046264 File Offset: 0x00044464
		public void DeleteTask(Guid id)
		{
			this.ThrowIfSchedulerNotRunning();
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("DeleteTask", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ScheduleID", id);
				try
				{
					if (instrumentedSqlCommand.ExecuteNonQuery() == 0)
					{
						throw new ScheduleNotFoundException(id.ToString());
					}
				}
				catch (ReportServerStorageException ex)
				{
					if (!ex.IsSqlException || ex.SqlErrorNumber != 14262)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x000462F8 File Offset: 0x000444F8
		public void UpdateTaskProperties(Task task, bool updateSqlAgentSchedule)
		{
			this.ThrowIfSchedulerNotRunning();
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("UpdateTask", null))
			{
				instrumentedSqlCommand.AddParameter("@Name", SqlDbType.NVarChar, task.Name);
				this.SetSqlParameters(instrumentedSqlCommand, task, updateSqlAgentSchedule);
				try
				{
					instrumentedSqlCommand.ExecuteNonQuery();
				}
				catch (ReportServerStorageException ex)
				{
					if (ex.IsSqlException && ex.Message == "Schedule already exists")
					{
						throw new ScheduleAlreadyExists(task.Name);
					}
					throw;
				}
			}
			if (updateSqlAgentSchedule && task.ScheduleState != TaskState.Paused)
			{
				this.m_sqlSchedule.UpdateTask(task);
			}
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000463A8 File Offset: 0x000445A8
		public void PauseSchedule(Task task)
		{
			this.ThrowIfSchedulerNotRunning();
			Global.m_Tracer.Assert(task.ScheduleState == TaskState.Paused, "Call to pause schedule without a paused schedule.");
			this.UpdateTaskProperties(task, false);
			this.m_sqlSchedule.DeleteTask(task.ID);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x000463E1 File Offset: 0x000445E1
		public void ResumeSchedule(Task task)
		{
			this.ThrowIfSchedulerNotRunning();
			Global.m_Tracer.Assert(task.ScheduleState != TaskState.Paused, "Call to resume schedule without a ready schedule.");
			this.UpdateTaskProperties(task, false);
			this.m_sqlSchedule.CreateTask(task);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00046418 File Offset: 0x00044618
		public ArrayList ListScheduledReports(Guid scheduleID, bool allowDuplicates)
		{
			ArrayList arrayList = new ArrayList();
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ListScheduledReports", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ScheduleID", scheduleID);
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					Hashtable hashtable = new Hashtable();
					while (dataReader.Read())
					{
						ItemScheduleAction itemScheduleAction = new ItemScheduleAction(dataReader);
						if (allowDuplicates || !this.IsDuplicateReport(hashtable, itemScheduleAction.ItemID))
						{
							arrayList.Add(itemScheduleAction);
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x000464C0 File Offset: 0x000446C0
		private bool IsDuplicateReport(Hashtable list, Guid scheduleID)
		{
			bool flag = false;
			if (list[scheduleID] == null)
			{
				list.Add(scheduleID, scheduleID);
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x000464F4 File Offset: 0x000446F4
		public ArrayList ListTasks(CatalogItemPath path)
		{
			bool flag = this.IsSchedulerRunning();
			ArrayList arrayList = new ArrayList();
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ListTasks", null))
			{
				if (path != null)
				{
					instrumentedSqlCommand.AddParameter("@Path", SqlDbType.NVarChar, path.Value);
					instrumentedSqlCommand.AddParameter("@Prefix", SqlDbType.NVarChar, Storage.EncodeForLike(path.Value) + "/%");
				}
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						Task task = new Task(dataReader, 0, true);
						if (!flag)
						{
							task.IsFailing = true;
						}
						task.ReportsCount = dataReader.GetInt32(25);
						arrayList.Add(task);
					}
				}
			}
			foreach (object obj in arrayList)
			{
				Task task2 = (Task)obj;
				if (task2.DatabaseState == TaskState.Ready)
				{
					this.CheckTasksNextRunTime(task2);
				}
			}
			return arrayList;
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0004661C File Offset: 0x0004481C
		public ArrayList ListTasksForMaintenance()
		{
			ArrayList arrayList = new ArrayList();
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("ListTasksForMaintenance", null))
			{
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						Task task = new Task(dataReader, 0, false);
						arrayList.Add(task);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00046694 File Offset: 0x00044894
		public int GetScheduleCount()
		{
			int num = 0;
			using (InstrumentedSqlCommand instrumentedSqlCommand = Storage.NewSqlCommand("select count(*) from Schedule", CommandType.Text, base.Connection, base.Transaction, base.SqlCommandTimeout))
			{
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					if (dataReader.Read())
					{
						num = dataReader.GetInt32(0);
					}
				}
			}
			return num;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0004670C File Offset: 0x0004490C
		private void SetSqlParameters(InstrumentedSqlCommand command, Task task, bool resetNextRuntime)
		{
			command.Parameters.AddWithValue("@ScheduleID", task.ID);
			command.Parameters.AddWithValue("@StartDate", task.Trigger.StartDate);
			command.Parameters.AddWithValue("@Flags", task.Flags);
			command.Parameters.AddWithValue("@RecurrenceType", task.Trigger.RecurrenceType);
			command.Parameters.AddWithValue("@State", task.DatabaseState);
			command.AddParameter("@LastRunStatus", SqlDbType.NVarChar, task.LastRunStatus);
			command.Parameters.AddWithValue("@ScheduledRunTimeout", task.MaxRunTime);
			if (task.Trigger.EndDate != DateTime.MinValue)
			{
				command.Parameters.AddWithValue("@EndDate", task.Trigger.EndDate.Date);
			}
			if (task.NextRunTime != DateTime.MinValue && !resetNextRuntime)
			{
				command.Parameters.AddWithValue("@NextRunTime", task.NextRunTime);
			}
			if (task.LastRunTime != DateTime.MinValue)
			{
				command.Parameters.AddWithValue("@LastRunTime", task.LastRunTime);
			}
			switch (task.Trigger.RecurrenceType)
			{
			case RecurrenceType.Minutes:
			{
				Minutes minutes = (Minutes)task.Trigger.TriggerData;
				command.Parameters.AddWithValue("@MinutesInterval", minutes.MinutesInterval);
				return;
			}
			case RecurrenceType.Daily:
			{
				Daily daily = (Daily)task.Trigger.TriggerData;
				command.Parameters.AddWithValue("@DaysInterval", daily.DaysInterval);
				return;
			}
			case RecurrenceType.Weekly:
			{
				Weekly weekly = (Weekly)task.Trigger.TriggerData;
				command.Parameters.AddWithValue("@WeeksInterval", weekly.WeeksInterval);
				command.Parameters.AddWithValue("@DaysOfWeek", (int)weekly.DaysOfWeek);
				return;
			}
			case RecurrenceType.MonthlyDate:
			{
				Monthly monthly = (Monthly)task.Trigger.TriggerData;
				command.Parameters.AddWithValue("@DaysOfMonth", (int)monthly.DaysOfMonth);
				command.Parameters.AddWithValue("@Month", (int)monthly.Months);
				return;
			}
			case RecurrenceType.MonthlyDOW:
			{
				MonthlyDOW monthlyDOW = (MonthlyDOW)task.Trigger.TriggerData;
				command.Parameters.AddWithValue("@MonthlyWeek", (int)monthlyDOW.Week);
				command.Parameters.AddWithValue("@DaysOfWeek", (int)monthlyDOW.DaysOfWeek);
				command.Parameters.AddWithValue("@Month", (int)monthlyDOW.Months);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x040006E6 RID: 1766
		private SqlAgentScheduler m_sqlSchedule = new SqlAgentScheduler();

		// Token: 0x040006E7 RID: 1767
		private const string schedule_already_exists = "Schedule already exists";

		// Token: 0x040006E8 RID: 1768
		private static readonly DateTime _NoNextRunTime = new DateTime(1970, 1, 1);

		// Token: 0x040006E9 RID: 1769
		private static readonly DateTime _UnknowNextRunTime = DateTime.MinValue;
	}
}
