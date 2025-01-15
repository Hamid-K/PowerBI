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
	// Token: 0x02000034 RID: 52
	internal class SqlAgentScheduler : Storage
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000080D4 File Offset: 0x000062D4
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00008144 File Offset: 0x00006344
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

		// Token: 0x0600013E RID: 318 RVA: 0x00008198 File Offset: 0x00006398
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

		// Token: 0x0600013F RID: 319 RVA: 0x0000821C File Offset: 0x0000641C
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000082D8 File Offset: 0x000064D8
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

		// Token: 0x06000142 RID: 322 RVA: 0x000083F0 File Offset: 0x000065F0
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

		// Token: 0x06000143 RID: 323 RVA: 0x000087E4 File Offset: 0x000069E4
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

		// Token: 0x06000144 RID: 324 RVA: 0x00008990 File Offset: 0x00006B90
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

		// Token: 0x06000145 RID: 325 RVA: 0x00008A4C File Offset: 0x00006C4C
		public void UpdateTask(Task task)
		{
			this.DeleteTask(task.ID);
			this.CreateTask(task);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00008A64 File Offset: 0x00006C64
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

		// Token: 0x04000157 RID: 343
		public const int _ScheduleNotFoundError = 14262;

		// Token: 0x04000158 RID: 344
		private const string _commandFormat = ".dbo.AddEvent @EventType='{0}', @EventData='{1}'";

		// Token: 0x04000159 RID: 345
		protected string m_eventCommandFormat;

		// Token: 0x0400015A RID: 346
		protected const int _scheduleOperationRetries = 10;

		// Token: 0x0400015B RID: 347
		protected static int m_categoryId;

		// Token: 0x0400015C RID: 348
		private readonly TimeSpan _tolerance = new TimeSpan(0, 0, Globals.Configuration.MaxScheduleWait);

		// Token: 0x0400015D RID: 349
		private readonly TimeSpan _waitTime = new TimeSpan(0, 0, 0, 0, 100);
	}
}
