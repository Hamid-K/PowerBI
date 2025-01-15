using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Threading;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200021B RID: 539
	internal class SqlAgentScheduler : Storage
	{
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x00043A30 File Offset: 0x00041C30
		private string GenerateEventCommandFormat
		{
			get
			{
				string eventCommandFormat;
				lock (this)
				{
					if (this.m_eventCommandFormat == null)
					{
						this.m_eventCommandFormat = "exec " + this.ConnectionManager.EscapeAndBracketDBName(base.Connection.Database) + ".dbo.AddEvent @EventType='{0}', @EventData='{1}'";
					}
					eventCommandFormat = this.m_eventCommandFormat;
				}
				return eventCommandFormat;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x00043AA0 File Offset: 0x00041CA0
		private int CategoryID
		{
			get
			{
				lock (this)
				{
					if (SqlAgentScheduler.m_categoryId == 0)
					{
						this.GetCategoryIDFromMSDB();
					}
					if (SqlAgentScheduler.m_categoryId == 0)
					{
						this.CreateCategoryID();
					}
				}
				return SqlAgentScheduler.m_categoryId;
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00043AF4 File Offset: 0x00041CF4
		private void GetCategoryIDFromMSDB()
		{
			SqlAgentScheduler.m_categoryId = 0;
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("msdb.dbo.sp_help_category", null))
			{
				using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						if (dataReader.GetString(2) == RepLibRes.ReportServiceCategoryName)
						{
							SqlAgentScheduler.m_categoryId = dataReader.GetInt32(0);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00043B78 File Offset: 0x00041D78
		private void CreateCategoryID()
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("msdb.dbo.sp_add_category", null))
			{
				SqlParameter sqlParameter = new SqlParameter("@class", SqlDbType.VarChar, 8);
				sqlParameter.Value = "JOB";
				instrumentedSqlCommand.Parameters.Add(sqlParameter);
				sqlParameter = new SqlParameter("@type", SqlDbType.VarChar, 12);
				sqlParameter.Value = "LOCAL";
				instrumentedSqlCommand.Parameters.Add(sqlParameter);
				sqlParameter = new SqlParameter("@name", SqlDbType.NVarChar, 128);
				sqlParameter.Value = RepLibRes.ReportServiceCategoryName;
				instrumentedSqlCommand.Parameters.Add(sqlParameter);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
			this.GetCategoryIDFromMSDB();
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x00043C34 File Offset: 0x00041E34
		public bool IsSchedulerRunning
		{
			get
			{
				bool flag = true;
				int num;
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("master.dbo.xp_sqlagent_notify", null))
				{
					instrumentedSqlCommand.Parameters.AddWithValue("@op_type", "G");
					instrumentedSqlCommand.Parameters.AddWithValue("@job_id", null);
					instrumentedSqlCommand.Parameters.AddWithValue("@schedule_id", null);
					instrumentedSqlCommand.Parameters.AddWithValue("@alert_id", null);
					instrumentedSqlCommand.Parameters.AddWithValue("@action_type", null);
					SqlParameter sqlParameter = instrumentedSqlCommand.Parameters.Add("@retval", SqlDbType.BigInt);
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					instrumentedSqlCommand.ExecuteNonQuery();
					num = (int)sqlParameter.Value;
				}
				if (num == 1)
				{
					flag = false;
					RSEventLog.Current.WriteError(Event.SqlAgentNotRunning, Array.Empty<object>());
				}
				else
				{
					RSEventLog.Current.ResetWriteOnceEvent(Event.SqlAgentNotRunning);
				}
				return flag;
			}
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00043D4C File Offset: 0x00041F4C
		public void CreateTask(Task task)
		{
			string text = task.ID.ToString().ToUpper(CultureInfo.InvariantCulture);
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("msdb.dbo.sp_add_job", null))
			{
				SqlParameter sqlParameter = new SqlParameter("@job_name", SqlDbType.NVarChar, 128);
				sqlParameter.Value = text;
				instrumentedSqlCommand.Parameters.Add(sqlParameter);
				sqlParameter = new SqlParameter("@description", SqlDbType.NVarChar, 512);
				sqlParameter.Value = RepLibRes.TaskComment;
				instrumentedSqlCommand.Parameters.Add(sqlParameter);
				sqlParameter = new SqlParameter("@category_id", SqlDbType.Int);
				sqlParameter.Value = this.CategoryID;
				instrumentedSqlCommand.Parameters.Add(sqlParameter);
				instrumentedSqlCommand.ExecuteNonQuery();
				instrumentedSqlCommand.CommandText = "msdb.dbo.sp_add_jobserver";
				instrumentedSqlCommand.Parameters.Clear();
				sqlParameter = new SqlParameter("@job_name", SqlDbType.NVarChar, 128);
				sqlParameter.Value = text;
				instrumentedSqlCommand.Parameters.Add(sqlParameter);
				sqlParameter = new SqlParameter("@server_name ", SqlDbType.NVarChar, 128);
				sqlParameter.Value = "(LOCAL)";
				instrumentedSqlCommand.Parameters.Add(sqlParameter);
				instrumentedSqlCommand.ExecuteNonQuery();
				instrumentedSqlCommand.CommandText = "msdb.dbo.sp_add_jobstep";
				instrumentedSqlCommand.Parameters.Clear();
				sqlParameter = new SqlParameter("@job_name", SqlDbType.NVarChar, 128);
				sqlParameter.Value = text;
				instrumentedSqlCommand.Parameters.Add(sqlParameter);
				sqlParameter = new SqlParameter("@step_name", SqlDbType.NVarChar, 128);
				sqlParameter.Value = text + "_step_1";
				instrumentedSqlCommand.Parameters.Add(sqlParameter);
				sqlParameter = new SqlParameter("@command", SqlDbType.NVarChar, 3201);
				sqlParameter.Value = string.Format(CultureInfo.InvariantCulture, this.GenerateEventCommandFormat, task.EventType, task.EventData);
				instrumentedSqlCommand.Parameters.Add(sqlParameter);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
			ArrayList sqlscheduleSet = new SqlScheduleHelper(task).GetSQLScheduleSet();
			int num = 0;
			foreach (object obj in sqlscheduleSet)
			{
				SQLScheduleParameters sqlscheduleParameters = (SQLScheduleParameters)obj;
				num++;
				int num2 = 10;
				for (;;)
				{
					try
					{
						using (InstrumentedSqlCommand instrumentedSqlCommand2 = this.NewStandardSqlCommand("msdb.dbo.sp_add_jobschedule", null))
						{
							SqlParameter sqlParameter2 = new SqlParameter("@job_name", SqlDbType.NVarChar, 128);
							sqlParameter2.Value = text;
							instrumentedSqlCommand2.Parameters.Add(sqlParameter2);
							sqlParameter2 = new SqlParameter("@name", SqlDbType.NVarChar, 128);
							sqlParameter2.Value = "Schedule_" + num.ToString(CultureInfo.InvariantCulture);
							instrumentedSqlCommand2.Parameters.Add(sqlParameter2);
							foreach (object obj2 in sqlscheduleParameters)
							{
								SQLScheduleParameter sqlscheduleParameter = (SQLScheduleParameter)obj2;
								sqlParameter2 = new SqlParameter(sqlscheduleParameter.Name, sqlscheduleParameter.Type);
								sqlParameter2.Value = sqlscheduleParameter.Value;
								instrumentedSqlCommand2.Parameters.Add(sqlParameter2);
							}
							instrumentedSqlCommand2.ExecuteNonQuery();
						}
					}
					catch (ReportServerStorageException ex)
					{
						if (num2 > 0 && Globals.IsRetriableSqlError(ex.SqlErrorNumber))
						{
							num2--;
							Thread.Sleep(100);
							continue;
						}
						throw;
					}
					break;
				}
			}
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00044140 File Offset: 0x00042340
		public DateTime GetNextRunTime(Guid id)
		{
			DateTime now = DateTime.Now;
			bool flag = true;
			DateTime dateTime = DateTime.MinValue;
			try
			{
				while (flag)
				{
					bool flag2;
					using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("Get_sqlagent_job_status", null))
					{
						instrumentedSqlCommand.Transaction = this.ConnectionManager.Transaction;
						instrumentedSqlCommand.Parameters.Clear();
						SqlParameter sqlParameter = new SqlParameter("@job_name", SqlDbType.NVarChar, 128);
						sqlParameter.Value = id.ToString().ToUpper(CultureInfo.InvariantCulture);
						instrumentedSqlCommand.Parameters.Add(sqlParameter);
						flag2 = false;
						using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
						{
							if (dataReader.Read())
							{
								flag2 = true;
								int @int = dataReader.GetInt32(3);
								int int2 = dataReader.GetInt32(4);
								if (@int != 0)
								{
									DateTime sqlDays = SqlScheduleHelper.GetSqlDays(@int);
									DateTime sqlTime = SqlScheduleHelper.GetSqlTime(int2);
									dateTime = new DateTime(sqlDays.Year, sqlDays.Month, sqlDays.Day, sqlTime.Hour, sqlTime.Minute, sqlTime.Second);
								}
								else
								{
									dateTime = DateTime.MinValue;
								}
							}
						}
					}
					if (flag2 || now + this._tolerance < DateTime.Now)
					{
						break;
					}
					Thread.Sleep(this._waitTime);
				}
			}
			catch (ReportServerStorageException ex)
			{
				if (ex.SqlErrorNumber != 14262)
				{
					throw;
				}
			}
			return dateTime;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x000442EC File Offset: 0x000424EC
		public void DeleteTask(Guid id)
		{
			int num = 10;
			for (;;)
			{
				try
				{
					using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("msdb.dbo.sp_delete_job", null))
					{
						SqlParameter sqlParameter = new SqlParameter("@job_name", SqlDbType.NVarChar, 128);
						sqlParameter.Value = id.ToString().ToUpper(CultureInfo.InvariantCulture);
						instrumentedSqlCommand.Parameters.Add(sqlParameter);
						instrumentedSqlCommand.ExecuteNonQuery();
					}
				}
				catch (ReportServerStorageException ex)
				{
					if (num > 0 && Globals.IsRetriableSqlError(ex.SqlErrorNumber))
					{
						num--;
						Thread.Sleep(100);
						continue;
					}
					if (ex.SqlErrorNumber != 14262)
					{
						throw;
					}
				}
				break;
			}
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x000443A8 File Offset: 0x000425A8
		public void UpdateTask(Task task)
		{
			this.DeleteTask(task.ID);
			this.CreateTask(task);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x000443C0 File Offset: 0x000425C0
		public Task GetTask(Guid id)
		{
			Task task = null;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("msdb.dbo.sp_help_jobschedule", null))
				{
					instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
					SqlParameter sqlParameter = new SqlParameter("@job_name", SqlDbType.NVarChar, 128);
					sqlParameter.Value = id.ToString().ToUpper(CultureInfo.InvariantCulture);
					instrumentedSqlCommand.Parameters.Add(sqlParameter);
					using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
					{
						task = new Task(id);
						SqlScheduleHelper sqlScheduleHelper = new SqlScheduleHelper(task);
						sqlScheduleHelper.PopulateSchedule(dataReader);
						task = sqlScheduleHelper.Task;
					}
				}
			}
			catch (ReportServerStorageException ex)
			{
				if (!ex.IsSqlException || ex.SqlErrorNumber != 14262)
				{
					throw;
				}
			}
			return task;
		}

		// Token: 0x040006D6 RID: 1750
		public const int _ScheduleNotFoundError = 14262;

		// Token: 0x040006D7 RID: 1751
		private const string _commandFormat = ".dbo.AddEvent @EventType='{0}', @EventData='{1}'";

		// Token: 0x040006D8 RID: 1752
		protected string m_eventCommandFormat;

		// Token: 0x040006D9 RID: 1753
		protected const int _scheduleOperationRetries = 10;

		// Token: 0x040006DA RID: 1754
		protected static int m_categoryId;

		// Token: 0x040006DB RID: 1755
		private readonly TimeSpan _tolerance = new TimeSpan(0, 0, Globals.Configuration.MaxScheduleWait);

		// Token: 0x040006DC RID: 1756
		private readonly TimeSpan _waitTime = new TimeSpan(0, 0, 0, 0, 100);
	}
}
