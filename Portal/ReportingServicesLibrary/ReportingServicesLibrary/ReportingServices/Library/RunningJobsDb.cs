using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002BE RID: 702
	internal sealed class RunningJobsDb
	{
		// Token: 0x06001937 RID: 6455 RVA: 0x00065690 File Offset: 0x00063890
		internal static int AddRunningJobs(Hashtable runningJobs)
		{
			int num = 0;
			if (runningJobs.Count == 0)
			{
				return num;
			}
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			try
			{
				foreach (object obj in runningJobs.Values)
				{
					RunningJobContext runningJobContext = (RunningJobContext)obj;
					SqlCommand sqlCommand = new SqlCommand("AddRunningJob", connectionManager.Connection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.AddWithValue("@JobID", runningJobContext.JobId).SqlDbType = SqlDbType.NVarChar;
					sqlCommand.Parameters.AddWithValue("@StartDate", runningJobContext.StartDate).SqlDbType = SqlDbType.DateTime;
					sqlCommand.Parameters.AddWithValue("@ComputerName", runningJobContext.Machine).SqlDbType = SqlDbType.NVarChar;
					sqlCommand.Parameters.AddWithValue("@RequestName", runningJobContext.Name).SqlDbType = SqlDbType.NVarChar;
					sqlCommand.Parameters.AddWithValue("@RequestPath", runningJobContext.Path.Value).SqlDbType = SqlDbType.NVarChar;
					sqlCommand.Parameters.AddWithValue("@UserSid", Global.NameToSid(runningJobContext.UserContext)).SqlDbType = SqlDbType.VarBinary;
					sqlCommand.Parameters.AddWithValue("@UserName", runningJobContext.UserContext.UserName).SqlDbType = SqlDbType.NVarChar;
					sqlCommand.Parameters.AddWithValue("@AuthType", (int)runningJobContext.UserContext.AuthenticationType).SqlDbType = SqlDbType.Int;
					if (runningJobContext.Description != null)
					{
						sqlCommand.Parameters.AddWithValue("@Description", runningJobContext.Description).SqlDbType = SqlDbType.NText;
					}
					sqlCommand.Parameters.AddWithValue("@Timeout", runningJobContext.Timeout).SqlDbType = SqlDbType.Int;
					sqlCommand.Parameters.AddWithValue("@JobAction", runningJobContext.Action).SqlDbType = SqlDbType.SmallInt;
					sqlCommand.Parameters.AddWithValue("@JobType", runningJobContext.Type).SqlDbType = SqlDbType.SmallInt;
					sqlCommand.Parameters.AddWithValue("@JobStatus", JobStatusEnum.Running).SqlDbType = SqlDbType.SmallInt;
					num += sqlCommand.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				connectionManager.AbortTransaction();
				throw new ReportServerDatabaseUnavailableException(ex);
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
			return num;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00065938 File Offset: 0x00063B38
		internal static int RemoveRunningJob(string jobId)
		{
			int num = 0;
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			try
			{
				SqlCommand sqlCommand = new SqlCommand("RemoveRunningJob", connectionManager.Connection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.Parameters.AddWithValue("@JobID", jobId).SqlDbType = SqlDbType.NVarChar;
				num += sqlCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				connectionManager.AbortTransaction();
				throw new ReportServerDatabaseUnavailableException(ex);
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
			return num;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x000659C4 File Offset: 0x00063BC4
		internal static int RemoveRunningJobs(Hashtable runningJobs)
		{
			int num = 0;
			if (runningJobs.Count == 0)
			{
				return num;
			}
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			try
			{
				foreach (object obj in runningJobs.Values)
				{
					RunningJobContext runningJobContext = (RunningJobContext)obj;
					SqlCommand sqlCommand = new SqlCommand("RemoveRunningJob", connectionManager.Connection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.AddWithValue("@JobID", runningJobContext.JobId).SqlDbType = SqlDbType.NVarChar;
					num += sqlCommand.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				connectionManager.AbortTransaction();
				throw new ReportServerDatabaseUnavailableException(ex);
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
			return num;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00065AA8 File Offset: 0x00063CA8
		internal static int MarkForCancelRequest(string jobId)
		{
			int num = 0;
			if (ProcessingContext.JobsTracer.TraceVerbose)
			{
				ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "RunningJobsDb.MarkForCancelRequest job: " + jobId);
			}
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			try
			{
				SqlCommand sqlCommand = new SqlCommand("UpdateRunningJob", connectionManager.Connection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.Parameters.AddWithValue("@JobID", jobId).SqlDbType = SqlDbType.NVarChar;
				sqlCommand.Parameters.AddWithValue("@JobStatus", JobStatusEnum.CancelRequested).SqlDbType = SqlDbType.SmallInt;
				num = sqlCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				connectionManager.AbortTransaction();
				throw new ReportServerDatabaseUnavailableException(ex);
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
			if (ProcessingContext.JobsTracer.TraceVerbose)
			{
				ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "RunningJobsDb.MarkForCancelRequest job: " + jobId + "was done");
			}
			return num;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00065B98 File Offset: 0x00063D98
		internal static Hashtable GetMyRunningJobs(JobTypeEnum runningJobType)
		{
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			Hashtable hashtable2;
			try
			{
				Hashtable hashtable = new Hashtable();
				SqlCommand sqlCommand = new SqlCommand("GetMyRunningJobs", connectionManager.Connection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.Parameters.AddWithValue("@ComputerName", Environment.MachineName).SqlDbType = SqlDbType.NVarChar;
				sqlCommand.Parameters.AddWithValue("@JobType", runningJobType).SqlDbType = SqlDbType.SmallInt;
				using (IDataReader dataReader = sqlCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						RunningJobContext runningJobContext = new RunningJobContext(dataReader);
						hashtable.Add(runningJobContext.JobId, runningJobContext);
					}
				}
				hashtable2 = hashtable;
			}
			catch (Exception ex)
			{
				connectionManager.AbortTransaction();
				throw new ReportServerDatabaseUnavailableException(ex);
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
			return hashtable2;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00065C80 File Offset: 0x00063E80
		internal static ICollection<RunningJobContext> ListRunningJobs()
		{
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			ICollection<RunningJobContext> collection;
			try
			{
				List<RunningJobContext> list = new List<RunningJobContext>();
				using (IDataReader dataReader = new SqlCommand("ListRunningJobs", connectionManager.Connection)
				{
					CommandType = CommandType.StoredProcedure
				}.ExecuteReader())
				{
					while (dataReader.Read())
					{
						RunningJobContext runningJobContext = new RunningJobContext(dataReader);
						list.Add(runningJobContext);
					}
				}
				Hashtable newLocalJobs = RunningJobList.Current.GetNewLocalJobs();
				RunningJobsDb.MergeLocalAndDbJobs(list, newLocalJobs);
				collection = list;
			}
			catch (Exception ex)
			{
				connectionManager.AbortTransaction();
				throw new ReportServerDatabaseUnavailableException(ex);
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
			return collection;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00065D3C File Offset: 0x00063F3C
		private static void MergeLocalAndDbJobs(ICollection<RunningJobContext> dbJobs, Hashtable newLocalJobs)
		{
			foreach (RunningJobContext runningJobContext in dbJobs)
			{
				if (newLocalJobs.ContainsKey(runningJobContext.JobId))
				{
					if (ProcessingContext.JobsTracer.TraceWarning)
					{
						ProcessingContext.JobsTracer.Trace(TraceLevel.Warning, "Found duplicate job, ID: " + runningJobContext.JobId);
					}
					newLocalJobs.Remove(runningJobContext.JobId);
				}
			}
			foreach (object obj in newLocalJobs.Values)
			{
				RunningJobContext runningJobContext2 = (RunningJobContext)obj;
				dbJobs.Add(runningJobContext2);
			}
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00065E0C File Offset: 0x0006400C
		internal int CleanExpiredJobs()
		{
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			int num;
			try
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("CleanExpiredJobs", connectionManager.Connection, connectionManager.Transaction)))
				{
					instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
					instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCleanupTimeoutSeconds;
					num = instrumentedSqlCommand.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				connectionManager.AbortTransaction();
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in CleanExpiredJobs: {0}", new object[] { ex });
				if (!(ex is RSException))
				{
					throw;
				}
				num = 0;
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
			return num;
		}
	}
}
