using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200027E RID: 638
	internal sealed class DatabaseSessionStorage
	{
		// Token: 0x060016AF RID: 5807 RVA: 0x000025F4 File Offset: 0x000007F4
		private DatabaseSessionStorage()
		{
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x0005A29B File Offset: 0x0005849B
		public static DatabaseSessionStorage Current
		{
			get
			{
				return DatabaseSessionStorage.m_current;
			}
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0005A2A2 File Offset: 0x000584A2
		private void PreQueue(object args)
		{
			((SessionReportItem)args).AddThreadUsingThisSession();
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0005A2AF File Offset: 0x000584AF
		public void SetSessionData(SessionReportItem sessReport, bool async)
		{
			if (async)
			{
				ReportServerThreadPool.TryQueueWorkItem(new ThreadWorkItem(new WaitCallback(DatabaseSessionStorage.ThreadFuncSetSessionData), sessReport, new PreQueueDelegate(this.PreQueue), sessReport, false));
				return;
			}
			DatabaseSessionStorage.SetSessionData(sessReport);
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0005A2E0 File Offset: 0x000584E0
		public SessionReportItem GetSessionData(string sessionId, UserContext userContext)
		{
			ConnectionManager connectionManager = new ConnectionManager();
			connectionManager.WillDisconnectStorage();
			SessionReportItem sessionReportItem;
			try
			{
				connectionManager.BeginTransaction();
				SessionReportItem sessionData = this.GetSessionData(sessionId, userContext, connectionManager);
				connectionManager.CommitTransaction();
				sessionReportItem = sessionData;
			}
			catch (Exception)
			{
				connectionManager.AbortTransaction();
				throw;
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
			return sessionReportItem;
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0005A340 File Offset: 0x00058540
		private SessionReportItem GetSessionData(string sessionId, UserContext userContext, ConnectionManager connMgr)
		{
			if (RSTrace.SessionTrace.TraceVerbose)
			{
				RSTrace.SessionTrace.Trace(TraceLevel.Verbose, "Getting snapshot data: session:'{0}'", new object[] { sessionId });
			}
			SessionReportItem sessionReportItem;
			try
			{
				using (connMgr.EnterThreadSafeContext())
				{
					SqlConnection connection = connMgr.Connection;
					using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("GetSessionData", connection, connMgr.Transaction)))
					{
						instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
						instrumentedSqlCommand.CommandTimeout = Global.SessionAccessTimeout;
						instrumentedSqlCommand.Parameters.Add("@SessionID", SqlDbType.VarChar, 32).Value = sessionId;
						instrumentedSqlCommand.Parameters.Add("@OwnerSid", SqlDbType.VarBinary, 85).Value = Global.NameToSid(userContext);
						instrumentedSqlCommand.Parameters.Add("@OwnerName", SqlDbType.NVarChar, 260).Value = userContext.UserName;
						instrumentedSqlCommand.Parameters.Add("@AuthType", SqlDbType.Int).Value = (int)userContext.AuthenticationType;
						instrumentedSqlCommand.Parameters.Add("@SnapshotTimeoutMinutes", SqlDbType.Int).Value = 1440;
						using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
						{
							if (!dataReader.Read())
							{
								sessionReportItem = null;
							}
							else
							{
								Guid guid = Guid.Empty;
								if (!dataReader.IsDBNull(0))
								{
									guid = dataReader.GetGuid(0);
								}
								byte[] array = null;
								if (!dataReader.IsDBNull(1))
								{
									array = DataReaderHelper.ReadAllBytes(dataReader, 1);
								}
								byte[] array2 = null;
								if (!dataReader.IsDBNull(2))
								{
									array2 = DataReaderHelper.ReadAllBytes(dataReader, 2);
								}
								string text = null;
								if (!dataReader.IsDBNull(3))
								{
									text = dataReader.GetString(3);
								}
								string text2 = null;
								if (!dataReader.IsDBNull(4))
								{
									text2 = dataReader.GetString(4);
								}
								DateTime dateTime = DateTime.MinValue;
								if (!dataReader.IsDBNull(5))
								{
									dateTime = dataReader.GetDateTime(5);
								}
								bool flag = false;
								if (!dataReader.IsDBNull(6))
								{
									flag = dataReader.GetBoolean(6);
								}
								DateTime dateTime2 = dataReader.GetDateTime(7);
								bool flag2 = true;
								if (!dataReader.IsDBNull(8))
								{
									flag2 = dataReader.GetBoolean(8);
								}
								int @int = dataReader.GetInt32(9);
								DateTime dateTime3 = DateTime.MinValue;
								if (!dataReader.IsDBNull(10))
								{
									dateTime3 = dataReader.GetDateTime(10);
								}
								string text3 = null;
								if (!dataReader.IsDBNull(11))
								{
									text3 = dataReader.GetString(11);
								}
								DateTime dateTime4 = DateTime.MinValue;
								if (!dataReader.IsDBNull(12))
								{
									dateTime4 = dataReader.GetDateTime(12);
								}
								Guid guid2 = Guid.Empty;
								if (!dataReader.IsDBNull(13))
								{
									guid2 = dataReader.GetGuid(13);
								}
								int num = 0;
								if (!dataReader.IsDBNull(14))
								{
									num = dataReader.GetInt32(14);
								}
								bool flag3 = false;
								if (!dataReader.IsDBNull(15))
								{
									flag3 = dataReader.GetBoolean(15);
								}
								DateTime dateTime5 = dataReader.GetDateTime(16);
								string text4 = null;
								if (!dataReader.IsDBNull(17))
								{
									text4 = dataReader.GetString(17);
								}
								PageProperties pageProperties = null;
								if (!dataReader.IsDBNull(18))
								{
									if (dataReader.IsDBNull(19) || dataReader.IsDBNull(20) || dataReader.IsDBNull(21) || dataReader.IsDBNull(22) || dataReader.IsDBNull(23))
									{
										throw new InternalCatalogException("Session contains only partial page properties");
									}
									double @double = dataReader.GetDouble(18);
									double double2 = dataReader.GetDouble(19);
									double double3 = dataReader.GetDouble(20);
									double double4 = dataReader.GetDouble(21);
									double double5 = dataReader.GetDouble(22);
									double double6 = dataReader.GetDouble(23);
									pageProperties = new PageProperties(@double, double2, double3, double4, double5, double6);
								}
								int num2 = 0;
								if (!dataReader.IsDBNull(24))
								{
									num2 = dataReader.GetInt32(24);
								}
								bool flag4 = true;
								if (!dataReader.IsDBNull(25))
								{
									flag4 = dataReader.GetBoolean(25);
								}
								bool flag5 = false;
								if (!dataReader.IsDBNull(26))
								{
									flag5 = dataReader.GetBoolean(26);
								}
								short num3 = 0;
								if (!dataReader.IsDBNull(27))
								{
									num3 = dataReader.GetInt16(27);
								}
								ReportProcessingFlags reportProcessingFlags = ReportProcessingFlags.NotSet;
								ReportProcessingFlags reportProcessingFlags2 = ReportProcessingFlags.NotSet;
								if (!dataReader.IsDBNull(28))
								{
									reportProcessingFlags = (ReportProcessingFlags)dataReader.GetInt32(28);
								}
								if (!dataReader.IsDBNull(29))
								{
									reportProcessingFlags2 = (ReportProcessingFlags)dataReader.GetInt32(29);
								}
								bool boolean = dataReader.GetBoolean(30);
								string text5 = null;
								if (!dataReader.IsDBNull(31))
								{
									text5 = dataReader.GetString(31);
								}
								int int2 = dataReader.GetInt32(32);
								byte[] array3 = null;
								if (!dataReader.IsDBNull(33))
								{
									array3 = DataReaderHelper.ReadAllBytes(dataReader, 33);
								}
								ExternalItemPath externalItemPath2;
								ExternalItemPath externalItemPath = (externalItemPath2 = ExternalItemPath.ConstructFromEditSessionPath(text3));
								if (!dataReader.IsDBNull(34))
								{
									externalItemPath2 = ExternalItemPath.ConstructFromEditSessionPath(dataReader.GetString(34));
								}
								ReportItem reportItem = new ReportItem(externalItemPath);
								int num4 = 0;
								if (!dataReader.IsDBNull(35))
								{
									num4 = dataReader.GetInt32(35);
								}
								if (guid != Guid.Empty)
								{
									reportItem.SnapshotData = ReportSnapshot.Create(guid, flag, flag5, reportProcessingFlags);
								}
								if (guid2 != Guid.Empty)
								{
									reportItem.CompiledDefinition = ReportSnapshot.Create(guid2, false, false, reportProcessingFlags2);
								}
								reportItem.EffectiveParams = ParameterInfoCollection.DecodeFromXml(text2);
								reportItem.ParametersOnSnapshot = ParameterInfoCollection.DecodeFromXml(text4);
								reportItem.Description = text;
								reportItem.HistoryDate = dateTime4;
								reportItem.ReportDefinitionPath = externalItemPath2;
								SessionReportItem sessionReportItem2 = new SessionReportItem(this, sessionId, userContext, reportItem, dateTime2, flag2);
								sessionReportItem2.EventInfo = EventInformation.Deserialize(array);
								sessionReportItem2.Datasources = RuntimeDataSourceInfoCollection.Deserialize(array2);
								sessionReportItem2.DataSets = RuntimeDataSetInfoCollection.Deserialize(array3);
								sessionReportItem2.ExecutionDateTime = dateTime;
								sessionReportItem2.ExpirationDateTime = dateTime5;
								sessionReportItem2.Timeout = @int;
								sessionReportItem2.AutoRefreshSeconds = num2;
								sessionReportItem2.PageProperties = pageProperties;
								sessionReportItem2.FoundInCache = boolean;
								sessionReportItem2.LockVersion = num4;
								if (text5 != null)
								{
									sessionReportItem2.SitePath = new CatalogItemPath(text5);
								}
								sessionReportItem2.SiteZone = int2;
								if (dateTime3 != DateTime.MinValue)
								{
									sessionReportItem2.SnapshotExpirationDateTime = dateTime3;
								}
								sessionReportItem2.AwaitingFirstExecution = flag4;
								sessionReportItem2.PageCount = num;
								sessionReportItem2.HasDocumentMap = flag3;
								sessionReportItem2.PaginationMode = SessionReportItem.PaginationModeFromShort(num3);
								sessionReportItem2.IsDBInstance = true;
								sessionReportItem = sessionReportItem2;
							}
						}
					}
				}
			}
			catch (ReportServerStorageException ex)
			{
				if (!ex.IsSqlException || ex.SqlErrorNumber != Native.SqlAdHocErrorCode)
				{
					string text6 = ex.ToString();
					if (RSTrace.SessionTrace.TraceError)
					{
						RSTrace.SessionTrace.Trace(TraceLevel.Error, "Error in GetSnapshotData: {0}", new object[] { text6 });
					}
					throw;
				}
				connMgr.AbortTransaction();
				if (RSTrace.SessionTrace.TraceError)
				{
					RSTrace.SessionTrace.Trace(TraceLevel.Error, "Error in getting session data: {0}", new object[] { ex.SqlErrorMessage });
				}
				sessionReportItem = null;
			}
			return sessionReportItem;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0005AA18 File Offset: 0x00058C18
		internal void WriteLockSession(SessionReportItem sessReport, bool bCheckVersion)
		{
			ConnectionManager sessionConnectionManager = sessReport.SessionConnectionManager;
			using (sessionConnectionManager.EnterThreadSafeContext())
			{
				using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("WriteLockSession", sessionConnectionManager.Connection, sessionConnectionManager.Transaction)))
				{
					instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
					instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCommandTimeoutSeconds;
					instrumentedSqlCommand.Parameters.Add("@SessionID", SqlDbType.VarChar, 32).Value = sessReport.SessionId;
					instrumentedSqlCommand.Parameters.Add("@Persisted", SqlDbType.Bit).Value = sessReport.IsDBInstance;
					instrumentedSqlCommand.Parameters.Add("@CheckLockVersion", SqlDbType.Bit).Value = bCheckVersion;
					instrumentedSqlCommand.Parameters.Add("@LockVersion", SqlDbType.Int).Value = sessReport.LockVersion;
					if (instrumentedSqlCommand.ExecuteNonQuery() == 0)
					{
						throw new ExecutionNotFoundException(sessReport.SessionId);
					}
				}
			}
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0005AB24 File Offset: 0x00058D24
		public void DeleteSession(SessionReportItem sessReport, bool async)
		{
			if (async)
			{
				ReportServerThreadPool.QueueUserWorkItem(new WaitCallback(DatabaseSessionStorage.ThreadFuncDeleteSession), sessReport);
				return;
			}
			DatabaseSessionStorage.DeleteSession(sessReport);
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0005AB44 File Offset: 0x00058D44
		private static void ThreadFuncSetSessionData(object o)
		{
			SessionReportItem sessionReportItem = null;
			try
			{
				sessionReportItem = o as SessionReportItem;
				if (sessionReportItem == null)
				{
					throw new InternalCatalogException("Wrong object type.");
				}
				DatabaseSessionStorage.SetSessionData(sessionReportItem);
				RSTrace.SessionTrace.Trace(TraceLevel.Verbose, "Saved report snapshot to session in a background thread. Key={0}", new object[] { sessionReportItem.Key });
			}
			catch (Exception ex)
			{
				string text = ex.ToString();
				if (RSTrace.CatalogTrace.TraceError)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Error, "error saving session: " + text);
				}
			}
			finally
			{
				sessionReportItem.ThreadNoLongerUseThisSession();
			}
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0005ABDC File Offset: 0x00058DDC
		private static void SetSessionData(SessionReportItem sessionReport)
		{
			ConnectionManager connectionManager = null;
			bool flag = false;
			try
			{
				if (sessionReport.SessionConnectionManager == null)
				{
					connectionManager = new ConnectionManager();
					connectionManager.WillDisconnectStorage();
					flag = true;
					connectionManager.BeginTransaction();
				}
				else
				{
					connectionManager = sessionReport.SessionConnectionManager;
				}
				DatabaseSessionStorage.SetSessionData(sessionReport, connectionManager);
				sessionReport.FirePersistedEvent(connectionManager);
				if (flag)
				{
					connectionManager.CommitTransaction();
				}
			}
			catch (Exception)
			{
				try
				{
					connectionManager.AbortTransaction();
					throw;
				}
				finally
				{
					connectionManager.DisconnectStorage();
					connectionManager = null;
					sessionReport.SessionConnectionManager = null;
				}
			}
			finally
			{
				if (flag && connectionManager != null)
				{
					connectionManager.DisconnectStorage();
				}
			}
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0005AC7C File Offset: 0x00058E7C
		private static void SetSessionData(SessionReportItem sessionReport, ConnectionManager connectionManager)
		{
			if (RSTrace.SessionTrace.TraceVerbose)
			{
				RSTrace.SessionTrace.Trace(TraceLevel.Verbose, "Adding report with path '{0}' to session DB", new object[] { sessionReport.Report.ItemPath.FullEditSessionIdentifier });
			}
			if (sessionReport.SaveFlags == SessionReportItem.SaveAction.SaveSnapshot)
			{
				using (ISnapshotTransaction snapshotTransaction = sessionReport.SnapshotTransactionFactory.EnterTransactionContext())
				{
					if (sessionReport.ProcessingResult != null)
					{
						sessionReport.ProcessingResult.Save();
					}
					snapshotTransaction.Commit();
				}
			}
			if (RSTrace.SessionTrace.TraceVerbose)
			{
				RSTrace.SessionTrace.Trace(TraceLevel.Verbose, "Finished serializing report");
			}
			try
			{
				ReportItem report = sessionReport.Report;
				using (connectionManager.EnterThreadSafeContext())
				{
					SqlConnection connection = connectionManager.Connection;
					SqlTransaction transaction = connectionManager.Transaction;
					using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("SetSessionData", connection, transaction)))
					{
						instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
						instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCommandTimeoutSeconds;
						instrumentedSqlCommand.Parameters.Add("@SessionID", SqlDbType.VarChar, 32).Value = sessionReport.SessionId;
						instrumentedSqlCommand.Parameters.Add("@ReportPath", SqlDbType.NVarChar, 440).Value = sessionReport.Report.ItemPath.FullEditSessionIdentifier;
						if (sessionReport.Report.HistoryDate != DateTime.MinValue)
						{
							instrumentedSqlCommand.Parameters.Add("@HistoryDate", SqlDbType.DateTime).Value = sessionReport.Report.HistoryDate;
						}
						int timeout = sessionReport.Timeout;
						if (timeout == 0)
						{
							throw new InternalCatalogException("unexpected value for session timeout: 0");
						}
						instrumentedSqlCommand.Parameters.Add("@Timeout", SqlDbType.Int).Value = timeout;
						instrumentedSqlCommand.Parameters.Add("@AutoRefreshSeconds", SqlDbType.Int).Value = sessionReport.AutoRefreshSeconds;
						instrumentedSqlCommand.Parameters.Add("@OwnerSid", SqlDbType.VarBinary, 85).Value = Global.NameToSid(sessionReport.UserContext);
						instrumentedSqlCommand.Parameters.Add("@OwnerName", SqlDbType.NVarChar, 260).Value = sessionReport.UserContext.UserName;
						instrumentedSqlCommand.Parameters.Add("@AuthType", SqlDbType.Int).Value = (int)sessionReport.UserContext.AuthenticationType;
						instrumentedSqlCommand.Parameters.Add("@HasInteractivity", SqlDbType.Bit).Value = sessionReport.HasInteractivity;
						if (report.EffectiveParams != null)
						{
							instrumentedSqlCommand.Parameters.Add("@EffectiveParams", SqlDbType.NText).Value = report.EffectiveParamsXml;
						}
						if (sessionReport.EventInfo != null)
						{
							instrumentedSqlCommand.Parameters.Add("@ShowHideInfo", SqlDbType.Image).Value = sessionReport.EventInfo.Serialize();
						}
						if (sessionReport.Datasources != null)
						{
							instrumentedSqlCommand.Parameters.Add("@DataSourceInfo", SqlDbType.Image).Value = sessionReport.Datasources.Serialize();
						}
						if (sessionReport.DataSets != null)
						{
							instrumentedSqlCommand.Parameters.Add("@DataSetInfo", SqlDbType.Image).Value = sessionReport.DataSets.Serialize();
						}
						if (sessionReport.Report.SnapshotData != null)
						{
							instrumentedSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier).Value = sessionReport.Report.SnapshotData.SnapshotDataID;
							instrumentedSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = sessionReport.Report.SnapshotData.IsPermanentSnapshot;
							instrumentedSqlCommand.Parameters.Add("@SnapshotTimeoutSeconds", SqlDbType.Int).Value = 1440;
						}
						if (sessionReport.SnapshotExpirationDateTime != DateTime.MinValue)
						{
							instrumentedSqlCommand.Parameters.Add("@SnapshotExpirationDate", SqlDbType.DateTime).Value = sessionReport.SnapshotExpirationDateTime;
						}
						instrumentedSqlCommand.Parameters.Add("@AwaitingFirstExecution", SqlDbType.Bit).Value = sessionReport.AwaitingFirstExecution;
						if (sessionReport.Report.ItemPath.IsEditSession)
						{
							instrumentedSqlCommand.Parameters.Add("@EditSessionID", SqlDbType.VarChar, 32).Value = sessionReport.Report.ItemPath.EditSessionID;
						}
						instrumentedSqlCommand.ExecuteNonQuery();
						sessionReport.IsDBInstance = true;
					}
				}
			}
			catch (Exception ex)
			{
				string text = ex.ToString();
				if (RSTrace.SessionTrace.TraceError)
				{
					RSTrace.SessionTrace.Trace(TraceLevel.Error, "Sql Error in AddReportToDB: {0}", new object[] { text });
				}
				throw;
			}
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0005B150 File Offset: 0x00059350
		private static void ThreadFuncDeleteSession(object o)
		{
			try
			{
				SessionReportItem sessionReportItem = o as SessionReportItem;
				if (sessionReportItem == null)
				{
					throw new InternalCatalogException("Wrong object type.");
				}
				DatabaseSessionStorage.DeleteSession(sessionReportItem);
			}
			catch (Exception ex)
			{
				string text = ex.ToString();
				if (RSTrace.CatalogTrace.TraceError)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Error, "error deleting session: " + text);
				}
			}
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0005B1B4 File Offset: 0x000593B4
		private static void DeleteSession(SessionReportItem sessionReport)
		{
			if (RSTrace.SessionTrace.TraceVerbose)
			{
				RSTrace.SessionTrace.Trace(TraceLevel.Verbose, "Removing report with path '{0}' from session DB", new object[] { sessionReport.Report.ItemPath.FullEditSessionIdentifier });
			}
			ConnectionManager connectionManager = new ConnectionManager();
			connectionManager.WillDisconnectStorage();
			try
			{
				using (connectionManager.EnterThreadSafeContext())
				{
					SqlConnection connection = connectionManager.Connection;
					connectionManager.BeginTransaction(IsolationLevel.ReadCommitted);
					SqlTransaction transaction = connectionManager.Transaction;
					using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("RemoveReportFromSession", connection, transaction)))
					{
						instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
						instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCommandTimeoutSeconds;
						instrumentedSqlCommand.Parameters.Add("@SessionID", SqlDbType.VarChar, 32).Value = sessionReport.SessionId;
						instrumentedSqlCommand.Parameters.Add("@ReportPath", SqlDbType.NVarChar, 440).Value = sessionReport.Report.ItemPath.FullEditSessionIdentifier;
						instrumentedSqlCommand.Parameters.Add("@OwnerSid", SqlDbType.VarBinary, 85).Value = Global.NameToSid(sessionReport.UserContext);
						instrumentedSqlCommand.Parameters.Add("@OwnerName", SqlDbType.NVarChar, 260).Value = sessionReport.UserContext.UserName;
						instrumentedSqlCommand.Parameters.Add("@AuthType", SqlDbType.Int).Value = (int)sessionReport.UserContext.AuthenticationType;
						instrumentedSqlCommand.ExecuteNonQuery();
					}
				}
				connectionManager.CommitTransaction();
			}
			catch (Exception ex)
			{
				string text = ex.ToString();
				if (RSTrace.SessionTrace.TraceError)
				{
					RSTrace.SessionTrace.Trace(TraceLevel.Error, "Sql Error in RemoveReportFromDB: {0}", new object[] { text });
				}
				connectionManager.AbortTransaction();
				throw;
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0005B3D8 File Offset: 0x000595D8
		internal static void AddNewSession(SessionReportItem sessionReport)
		{
			ConnectionManager connectionManager = new ConnectionManager();
			connectionManager.WillDisconnectStorage();
			try
			{
				connectionManager.BeginTransaction();
				using (connectionManager.EnterThreadSafeContext())
				{
					ReportItem report = sessionReport.Report;
					SqlConnection connection = connectionManager.Connection;
					SqlTransaction transaction = connectionManager.Transaction;
					using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("CreateSession", connection, transaction)))
					{
						instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
						instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCommandTimeoutSeconds;
						instrumentedSqlCommand.Parameters.Add("@SessionID", SqlDbType.VarChar, 32).Value = sessionReport.SessionId;
						instrumentedSqlCommand.Parameters.Add("@ReportPath", SqlDbType.NVarChar, 464).Value = sessionReport.Report.ItemPath.FullEditSessionIdentifier;
						if (sessionReport.Report.ItemPath != sessionReport.Report.ReportDefinitionPath && sessionReport.Report.ReportDefinitionPath != null)
						{
							instrumentedSqlCommand.Parameters.Add("@ReportDefinitionPath", SqlDbType.NVarChar, 464).Value = sessionReport.Report.ReportDefinitionPath.FullEditSessionIdentifier;
						}
						int timeout = sessionReport.Timeout;
						if (timeout == 0)
						{
							throw new InternalCatalogException("unexpected value for session timeout: 0");
						}
						instrumentedSqlCommand.Parameters.Add("@Timeout", SqlDbType.Int).Value = timeout;
						instrumentedSqlCommand.Parameters.Add("@AutoRefreshSeconds", SqlDbType.Int).Value = sessionReport.AutoRefreshSeconds;
						instrumentedSqlCommand.Parameters.Add("@OwnerSid", SqlDbType.VarBinary, 85).Value = Global.NameToSid(sessionReport.UserContext);
						instrumentedSqlCommand.Parameters.Add("@OwnerName", SqlDbType.NVarChar, 260).Value = sessionReport.UserContext.UserName;
						instrumentedSqlCommand.Parameters.Add("@AuthType", SqlDbType.Int).Value = (int)sessionReport.UserContext.AuthenticationType;
						if (sessionReport.SitePath != null)
						{
							instrumentedSqlCommand.Parameters.Add("@SitePath", SqlDbType.NVarChar, 440).Value = sessionReport.SitePath.Value;
						}
						instrumentedSqlCommand.Parameters.Add("@SiteZone", SqlDbType.Int).Value = sessionReport.SiteZone;
						if (sessionReport.Report.CompiledDefinition != null)
						{
							instrumentedSqlCommand.Parameters.Add("@CompiledDefinition", SqlDbType.UniqueIdentifier).Value = sessionReport.Report.CompiledDefinition.SnapshotDataID;
						}
						if (sessionReport.Report.SnapshotData != null)
						{
							instrumentedSqlCommand.Parameters.Add("@SnapshotDataID", SqlDbType.UniqueIdentifier).Value = sessionReport.Report.SnapshotData.SnapshotDataID;
							instrumentedSqlCommand.Parameters.Add("@IsPermanentSnapshot", SqlDbType.Bit).Value = sessionReport.Report.SnapshotData.IsPermanentSnapshot;
						}
						if (sessionReport.Report.HistoryDate != DateTime.MinValue)
						{
							instrumentedSqlCommand.Parameters.Add("@HistoryDate", SqlDbType.DateTime).Value = sessionReport.Report.HistoryDate;
						}
						if (sessionReport.Datasources != null)
						{
							instrumentedSqlCommand.Parameters.Add("@DataSourceInfo", SqlDbType.Image).Value = sessionReport.Datasources.Serialize();
						}
						if (sessionReport.DataSets != null)
						{
							instrumentedSqlCommand.Parameters.Add("@DataSetInfo", SqlDbType.Image).Value = sessionReport.DataSets.Serialize();
						}
						if (sessionReport.Report.EffectiveParams != null)
						{
							instrumentedSqlCommand.Parameters.Add("@EffectiveParams", SqlDbType.NText).Value = sessionReport.Report.EffectiveParamsXml;
						}
						if (sessionReport.PageProperties != null)
						{
							instrumentedSqlCommand.Parameters.Add("@PageHeight", SqlDbType.Float).Value = sessionReport.PageProperties.PageHeight;
							instrumentedSqlCommand.Parameters.Add("@PageWidth", SqlDbType.Float).Value = sessionReport.PageProperties.PageWidth;
							instrumentedSqlCommand.Parameters.Add("@TopMargin", SqlDbType.Float).Value = sessionReport.PageProperties.TopMargin;
							instrumentedSqlCommand.Parameters.Add("@BottomMargin", SqlDbType.Float).Value = sessionReport.PageProperties.BottomMargin;
							instrumentedSqlCommand.Parameters.Add("@LeftMargin", SqlDbType.Float).Value = sessionReport.PageProperties.LeftMargin;
							instrumentedSqlCommand.Parameters.Add("@RightMargin", SqlDbType.Float).Value = sessionReport.PageProperties.RightMargin;
						}
						instrumentedSqlCommand.Parameters.Add("@AwaitingFirstExecution", SqlDbType.Bit).Value = sessionReport.AwaitingFirstExecution;
						if (sessionReport.Report.ItemPath.IsEditSession)
						{
							instrumentedSqlCommand.Parameters.Add("@EditSessionID", SqlDbType.VarChar, 32).Value = sessionReport.Report.ItemPath.EditSessionID;
						}
						instrumentedSqlCommand.ExecuteNonQuery();
					}
				}
				connectionManager.CommitTransaction();
			}
			catch (Exception)
			{
				connectionManager.AbortTransaction();
				throw;
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0005B94C File Offset: 0x00059B4C
		internal static void SetCredentials(SessionReportItem sessionReport)
		{
			ConnectionManager connectionManager = new ConnectionManager();
			connectionManager.WillDisconnectStorage();
			try
			{
				connectionManager.BeginTransaction();
				using (connectionManager.EnterThreadSafeContext())
				{
					ReportItem report = sessionReport.Report;
					SqlConnection connection = connectionManager.Connection;
					SqlTransaction transaction = connectionManager.Transaction;
					using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("SetSessionCredentials", connection, transaction)))
					{
						instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
						instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCommandTimeoutSeconds;
						instrumentedSqlCommand.Parameters.Add("@SessionID", SqlDbType.VarChar, 32).Value = sessionReport.SessionId;
						instrumentedSqlCommand.Parameters.Add("@OwnerSid", SqlDbType.VarBinary, 85).Value = Global.NameToSid(sessionReport.UserContext);
						instrumentedSqlCommand.Parameters.Add("@OwnerName", SqlDbType.NVarChar, 260).Value = sessionReport.UserContext.UserName;
						instrumentedSqlCommand.Parameters.Add("@AuthType", SqlDbType.Int).Value = (int)sessionReport.UserContext.AuthenticationType;
						instrumentedSqlCommand.Parameters.Add("@Expiration", SqlDbType.DateTime).Value = sessionReport.ExpirationDateTime;
						if (sessionReport.Report.EffectiveParams != null)
						{
							instrumentedSqlCommand.Parameters.Add("@EffectiveParams", SqlDbType.NText).Value = sessionReport.Report.EffectiveParamsXml;
						}
						if (sessionReport.Datasources != null)
						{
							instrumentedSqlCommand.Parameters.Add("@DataSourceInfo", SqlDbType.Image).Value = sessionReport.Datasources.Serialize();
						}
						instrumentedSqlCommand.ExecuteNonQuery();
					}
				}
				connectionManager.CommitTransaction();
			}
			catch (Exception)
			{
				connectionManager.AbortTransaction();
				throw;
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0005BB5C File Offset: 0x00059D5C
		internal static void SetParameters(SessionReportItem sessionReport)
		{
			ConnectionManager connectionManager = new ConnectionManager();
			connectionManager.WillDisconnectStorage();
			try
			{
				connectionManager.BeginTransaction();
				using (connectionManager.EnterThreadSafeContext())
				{
					ReportItem report = sessionReport.Report;
					SqlConnection connection = connectionManager.Connection;
					SqlTransaction transaction = connectionManager.Transaction;
					using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("SetSessionParameters", connection, transaction)))
					{
						instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
						instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCommandTimeoutSeconds;
						instrumentedSqlCommand.Parameters.Add("@SessionID", SqlDbType.VarChar, 32).Value = sessionReport.SessionId;
						instrumentedSqlCommand.Parameters.Add("@OwnerSid", SqlDbType.VarBinary, 85).Value = Global.NameToSid(sessionReport.UserContext);
						instrumentedSqlCommand.Parameters.Add("@OwnerName", SqlDbType.NVarChar, 260).Value = sessionReport.UserContext.UserName;
						instrumentedSqlCommand.Parameters.Add("@AuthType", SqlDbType.Int).Value = (int)sessionReport.UserContext.AuthenticationType;
						if (sessionReport.Report.EffectiveParams != null)
						{
							instrumentedSqlCommand.Parameters.Add("@EffectiveParams", SqlDbType.NText).Value = sessionReport.Report.EffectiveParamsXml;
						}
						instrumentedSqlCommand.ExecuteNonQuery();
					}
				}
				connectionManager.CommitTransaction();
			}
			catch (Exception)
			{
				connectionManager.AbortTransaction();
				throw;
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0005BD20 File Offset: 0x00059F20
		internal static void ClearSnapshot(SessionReportItem sessionReport)
		{
			ConnectionManager connectionManager = new ConnectionManager();
			connectionManager.WillDisconnectStorage();
			try
			{
				connectionManager.BeginTransaction();
				using (connectionManager.EnterThreadSafeContext())
				{
					ReportItem report = sessionReport.Report;
					SqlConnection connection = connectionManager.Connection;
					SqlTransaction transaction = connectionManager.Transaction;
					using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("ClearSessionSnapshot", connection, transaction)))
					{
						instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
						instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCommandTimeoutSeconds;
						instrumentedSqlCommand.Parameters.Add("@SessionID", SqlDbType.VarChar, 32).Value = sessionReport.SessionId;
						instrumentedSqlCommand.Parameters.Add("@OwnerSid", SqlDbType.VarBinary, 85).Value = Global.NameToSid(sessionReport.UserContext);
						instrumentedSqlCommand.Parameters.Add("@OwnerName", SqlDbType.NVarChar, 260).Value = sessionReport.UserContext.UserName;
						instrumentedSqlCommand.Parameters.Add("@AuthType", SqlDbType.Int).Value = (int)sessionReport.UserContext.AuthenticationType;
						instrumentedSqlCommand.Parameters.Add("@Expiration", SqlDbType.DateTime).Value = sessionReport.ExpirationDateTime;
						instrumentedSqlCommand.ExecuteNonQuery();
					}
				}
				connectionManager.CommitTransaction();
			}
			catch (Exception)
			{
				connectionManager.AbortTransaction();
				throw;
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0005BED8 File Offset: 0x0005A0D8
		public bool TryAcquireCleanupLock()
		{
			ConnectionManager connectionManager = new ConnectionManager();
			connectionManager.WillDisconnectStorage();
			try
			{
				using (connectionManager.EnterThreadSafeContext())
				{
					SqlConnection connection = connectionManager.Connection;
					connectionManager.BeginTransaction(IsolationLevel.ReadCommitted);
					SqlTransaction transaction = connectionManager.Transaction;
					using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("TryAcquireCleanupLock", connection, transaction)))
					{
						instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCleanupTimeoutSeconds;
						instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
						instrumentedSqlCommand.AddParameter("@MachineName", SqlDbType.NVarChar, Environment.MachineName);
						using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
						{
							if (dataReader.Read())
							{
								return dataReader.GetBoolean(0);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				connectionManager.AbortTransaction();
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in CleanExpiredSessions: {0}", new object[] { ex });
				if (!(ex is RSException))
				{
					throw;
				}
				throw new InternalCatalogException("Error acquiring cleanup lock: no results");
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
			throw new InternalCatalogException("Error acquiring cleanup lock: no results");
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0005C024 File Offset: 0x0005A224
		internal int CleanExpiredSessions()
		{
			RSTrace.CleanupTracer.Trace(TraceLevel.Verbose, "Cleaning expired sessions from DB");
			ConnectionManager connectionManager = new ConnectionManager();
			connectionManager.WillDisconnectStorage();
			int num3;
			try
			{
				using (connectionManager.EnterThreadSafeContext())
				{
					SqlConnection connection = connectionManager.Connection;
					connectionManager.BeginTransaction(IsolationLevel.ReadCommitted);
					SqlTransaction transaction = connectionManager.Transaction;
					SystemProperties configurationFromCatalog = Global.ConfigurationFromCatalog;
					int num = int.Parse(configurationFromCatalog.CleanupMaxLimit);
					int num2 = int.Parse(configurationFromCatalog.CleanupBatchSize);
					using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("CleanExpiredSessions", connection, transaction)))
					{
						RSTrace.CleanupTracer.Trace(TraceLevel.Verbose, "Cleaning expired sessions from DB with params: {0}, {1}", new object[] { num, num2 });
						SqlParameter sqlParameter = new SqlParameter("@SessionsCleaned", SqlDbType.Int);
						sqlParameter.Direction = ParameterDirection.Output;
						instrumentedSqlCommand.Parameters.Add(sqlParameter);
						SqlParameter sqlParameter2 = new SqlParameter("@maxCleanCount", SqlDbType.Int);
						sqlParameter2.Direction = ParameterDirection.Input;
						sqlParameter2.Value = num;
						instrumentedSqlCommand.Parameters.Add(sqlParameter2);
						SqlParameter sqlParameter3 = new SqlParameter("@cleanBatchSize", SqlDbType.Int);
						sqlParameter3.Direction = ParameterDirection.Input;
						sqlParameter3.Value = num;
						instrumentedSqlCommand.Parameters.Add(sqlParameter3);
						instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
						instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCleanupTimeoutSeconds;
						instrumentedSqlCommand.ExecuteNonQuery();
						connectionManager.CommitTransaction();
						num3 = (int)sqlParameter.Value;
					}
				}
			}
			catch (Exception ex)
			{
				connectionManager.AbortTransaction();
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in CleanExpiredSessions: {0}", new object[] { ex });
				if (!(ex is RSException))
				{
					throw;
				}
				num3 = 0;
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
			return num3;
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0005C238 File Offset: 0x0005A438
		internal int CleanExpiredCache()
		{
			RSTrace.CleanupTracer.Trace(TraceLevel.Verbose, "Cleaning expired cache from DB");
			ConnectionManager connectionManager = new ConnectionManager();
			connectionManager.WillDisconnectStorage();
			int num2;
			try
			{
				using (connectionManager.EnterThreadSafeContext())
				{
					SqlConnection connection = connectionManager.Connection;
					connectionManager.BeginTransaction(IsolationLevel.ReadCommitted);
					SqlTransaction transaction = connectionManager.Transaction;
					using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("CleanExpiredCache", connection, transaction)))
					{
						instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
						instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCleanupTimeoutSeconds;
						int num = instrumentedSqlCommand.ExecuteNonQuery();
						connectionManager.CommitTransaction();
						num2 = num;
					}
				}
			}
			catch (Exception ex)
			{
				connectionManager.AbortTransaction();
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in CleanExpiredCache: {0}", new object[] { ex });
				if (!(ex is RSException))
				{
					throw;
				}
				num2 = 0;
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
			return num2;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0005C340 File Offset: 0x0005A540
		internal int CleanOrphanedSnapshots(out int chunksCleaned, out int mappingsCleaned, out int segmentsCleaned)
		{
			RSTrace.CleanupTracer.Trace(TraceLevel.Verbose, "Cleaning orphaned snapshots from DB");
			chunksCleaned = 0;
			mappingsCleaned = 0;
			segmentsCleaned = 0;
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			List<Guid> list = new List<Guid>();
			int num3;
			try
			{
				using (connectionManager.EnterThreadSafeContext())
				{
					int num = int.Parse(Global.ConfigurationFromCatalog.CleanupBatchSize);
					using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("CleanOrphanedSnapshots", connectionManager.Connection, connectionManager.Transaction)))
					{
						instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
						instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCleanupTimeoutSeconds;
						RSTrace.CleanupTracer.Trace(TraceLevel.Verbose, "Cleaning orphaned snapshots from DB with param: {1}", new object[] { num });
						instrumentedSqlCommand.Parameters.Add("@Machine", SqlDbType.NVarChar).Value = Environment.MachineName;
						instrumentedSqlCommand.Parameters.Add("@PermanentSnapshotCount", SqlDbType.Int).Value = DatabaseSessionStorage.CleanupStorage.SnapshotCountPerBatch;
						instrumentedSqlCommand.Parameters.Add("@TemporarySnapshotCount", SqlDbType.Int).Value = DatabaseSessionStorage.CleanupStorage.SnapshotCountPerBatch;
						instrumentedSqlCommand.Parameters.Add("@PermanentChunkCount", SqlDbType.Int).Value = DatabaseSessionStorage.CleanupStorage.ChunkCountPerBatch;
						instrumentedSqlCommand.Parameters.Add("@TemporaryChunkCount", SqlDbType.Int).Value = DatabaseSessionStorage.CleanupStorage.ChunkCountPerBatch;
						instrumentedSqlCommand.Parameters.Add("@PermanentMappingCount", SqlDbType.Int).Value = DatabaseSessionStorage.CleanupStorage.ChunkCountPerBatch;
						instrumentedSqlCommand.Parameters.Add("@TemporaryMappingCount", SqlDbType.Int).Value = DatabaseSessionStorage.CleanupStorage.ChunkCountPerBatch;
						instrumentedSqlCommand.Parameters.Add("@PermanentSegmentCount", SqlDbType.Int).Value = DatabaseSessionStorage.CleanupStorage.SegmentCountPerBatch;
						instrumentedSqlCommand.Parameters.Add("@TemporarySegmentCount", SqlDbType.Int).Value = DatabaseSessionStorage.CleanupStorage.SegmentCountPerBatch;
						instrumentedSqlCommand.Parameters.Add("@CleanChunksBatchSize", SqlDbType.Int).Value = num;
						SqlParameter sqlParameter = instrumentedSqlCommand.Parameters.Add("@SnapshotsCleaned", SqlDbType.Int);
						sqlParameter.Direction = ParameterDirection.Output;
						SqlParameter sqlParameter2 = instrumentedSqlCommand.Parameters.Add("@ChunksCleaned", SqlDbType.Int);
						sqlParameter2.Direction = ParameterDirection.Output;
						SqlParameter sqlParameter3 = instrumentedSqlCommand.Parameters.Add("@MappingsCleaned", SqlDbType.Int);
						sqlParameter3.Direction = ParameterDirection.Output;
						SqlParameter sqlParameter4 = instrumentedSqlCommand.Parameters.Add("@SegmentsCleaned", SqlDbType.Int);
						sqlParameter4.Direction = ParameterDirection.Output;
						using (IDataReader dataReader = instrumentedSqlCommand.ExecuteReader())
						{
							while (dataReader.Read())
							{
								if (!dataReader.IsDBNull(0))
								{
									Guid guid = dataReader.GetGuid(0);
									list.Add(guid);
								}
							}
						}
						int num2 = 0;
						if (sqlParameter.Value != null && sqlParameter.Value != DBNull.Value)
						{
							num2 = (int)sqlParameter.Value;
						}
						if (sqlParameter2.Value != null && sqlParameter2.Value != DBNull.Value)
						{
							chunksCleaned = (int)sqlParameter2.Value;
						}
						if (sqlParameter3.Value != null && sqlParameter3.Value != DBNull.Value)
						{
							mappingsCleaned = (int)sqlParameter3.Value;
						}
						if (sqlParameter4.Value != null && sqlParameter4.Value != DBNull.Value)
						{
							segmentsCleaned = (int)sqlParameter4.Value;
						}
						num3 = num2;
					}
				}
			}
			catch (Exception ex)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in CleanOrphanedSnapshots: {0}", new object[] { ex });
				if (!(ex is RSException))
				{
					throw;
				}
				num3 = 0;
			}
			finally
			{
				try
				{
					connectionManager.DisconnectStorage();
				}
				finally
				{
					foreach (Guid guid2 in list)
					{
						try
						{
							Global.PartitionManager.DeleteChunkFile(guid2);
						}
						catch (Exception ex2)
						{
							if (!PartitionManager.HandleFileCleanupException(ex2))
							{
								throw;
							}
						}
					}
				}
			}
			return num3;
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0005C7D0 File Offset: 0x0005A9D0
		internal bool CleanBrokenSnapshots(out int chunksCleaned, out int snapshotsCleaned)
		{
			if (RSTrace.CleanupTracer.TraceVerbose)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Verbose, "Cleaning broken snapshots from DB");
			}
			chunksCleaned = 0;
			snapshotsCleaned = 0;
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			bool flag;
			try
			{
				using (connectionManager.EnterThreadSafeContext())
				{
					connectionManager.BeginTransaction();
					using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("CleanBrokenSnapshots", connectionManager.Connection, connectionManager.Transaction)))
					{
						instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
						instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCleanupTimeoutSeconds;
						instrumentedSqlCommand.Parameters.Add("@Machine", SqlDbType.NVarChar).Value = Environment.MachineName;
						SqlParameter sqlParameter = instrumentedSqlCommand.Parameters.Add("@SnapshotsCleaned", SqlDbType.Int);
						sqlParameter.Direction = ParameterDirection.Output;
						SqlParameter sqlParameter2 = instrumentedSqlCommand.Parameters.Add("@ChunksCleaned", SqlDbType.Int);
						sqlParameter2.Direction = ParameterDirection.Output;
						SqlParameter sqlParameter3 = instrumentedSqlCommand.Parameters.Add("@TempSnapshotID", SqlDbType.UniqueIdentifier);
						sqlParameter3.Direction = ParameterDirection.Output;
						instrumentedSqlCommand.ExecuteNonQuery();
						if (sqlParameter3.Value != DBNull.Value)
						{
							Guid guid = (Guid)sqlParameter3.Value;
							try
							{
								Global.PartitionManager.DeleteFolder(guid.ToString());
							}
							catch (Exception ex)
							{
								if (!PartitionManager.HandleFileCleanupException(ex))
								{
									throw;
								}
							}
						}
						snapshotsCleaned = (int)sqlParameter.Value;
						chunksCleaned = (int)sqlParameter2.Value;
						flag = true;
					}
				}
			}
			catch (Exception ex2)
			{
				connectionManager.CommitTransaction();
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in CleanBrokenSnapshots: {0}", new object[] { ex2 });
				if (!(ex2 is RSException))
				{
					throw;
				}
				flag = false;
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
			return flag;
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0005C9EC File Offset: 0x0005ABEC
		internal int CleanSegmentMappings()
		{
			if (RSTrace.CleanupTracer.TraceVerbose)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Verbose, "Cleaning unused segment mappings from DB");
			}
			int num = 0;
			DatabaseSessionStorage.CleanupStorage cleanupStorage = new DatabaseSessionStorage.CleanupStorage();
			cleanupStorage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			try
			{
				cleanupStorage.ConnectionManager.WillDisconnectStorage();
				foreach (DatabaseSessionStorage.CleanupStorage.DeletedChunkInfo deletedChunkInfo in cleanupStorage.RemoveSegmentedMapping())
				{
					num++;
					if (!deletedChunkInfo.IsPermanent)
					{
						try
						{
							Global.PartitionManager.DeleteChunkFile(deletedChunkInfo.ChunkId);
						}
						catch (Exception ex)
						{
							if (!PartitionManager.HandleFileCleanupException(ex))
							{
								throw;
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in CleanSegmentMappings: {0}", new object[] { ex2 });
				if (!(ex2 is RSException))
				{
					throw;
				}
				return 0;
			}
			finally
			{
				cleanupStorage.ConnectionManager.DisconnectStorage();
			}
			return num;
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0005CB04 File Offset: 0x0005AD04
		internal int CleanupSegments()
		{
			if (RSTrace.CleanupTracer.TraceVerbose)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Verbose, "Cleaning up unused chunk segments from DB");
			}
			int num = 0;
			DatabaseSessionStorage.CleanupStorage cleanupStorage = new DatabaseSessionStorage.CleanupStorage();
			cleanupStorage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			try
			{
				cleanupStorage.ConnectionManager.WillDisconnectStorage();
				num = cleanupStorage.RemoveSegments();
			}
			catch (Exception ex)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in CleanupSegments: {0}", new object[] { ex });
				if (!(ex is RSException))
				{
					throw;
				}
				return 0;
			}
			finally
			{
				cleanupStorage.ConnectionManager.DisconnectStorage();
			}
			return num;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0005CBB0 File Offset: 0x0005ADB0
		internal int CleanupOrphanedFileChunks(IEnumerable<Guid> chunkIds)
		{
			if (RSTrace.CleanupTracer.TraceVerbose)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Verbose, "Cleaning up unused chunk segments from DB");
			}
			int num = 0;
			DatabaseSessionStorage.CleanupStorage cleanupStorage = new DatabaseSessionStorage.CleanupStorage();
			cleanupStorage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			try
			{
				cleanupStorage.ConnectionManager.WillDisconnectStorage();
				foreach (Guid guid in chunkIds)
				{
					if (!cleanupStorage.TempChunkExists(guid))
					{
						try
						{
							Global.PartitionManager.DeleteChunkFile(guid);
						}
						catch (Exception ex)
						{
							if (!PartitionManager.HandleFileCleanupException(ex))
							{
								throw;
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in CleanupOrphanedFileChunks: {0}", new object[] { ex2 });
				if (!(ex2 is RSException))
				{
					throw;
				}
			}
			finally
			{
				cleanupStorage.ConnectionManager.DisconnectStorage();
			}
			return num;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0005CCB0 File Offset: 0x0005AEB0
		internal int CleanExpiredParametersValues()
		{
			if (RSTrace.CleanupTracer.TraceVerbose)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Verbose, "Cleaning expired parameters values from DB");
			}
			int num = 0;
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			int num2;
			try
			{
				connectionManager.BeginTransaction();
				using (connectionManager.EnterThreadSafeContext())
				{
					using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("CleanExpiredServerParameters", connectionManager.Connection, connectionManager.Transaction)))
					{
						instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
						SqlParameter sqlParameter = instrumentedSqlCommand.Parameters.Add("@ParametersCleaned", SqlDbType.Int);
						sqlParameter.Direction = ParameterDirection.Output;
						instrumentedSqlCommand.ExecuteNonQuery();
						num = (int)sqlParameter.Value;
					}
				}
				connectionManager.CommitTransaction();
				num2 = num;
			}
			catch (Exception ex)
			{
				connectionManager.CommitTransaction();
				RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Error in CleanExpiredParametersValues: {0}", new object[] { ex });
				if (!(ex is RSException))
				{
					throw;
				}
				num2 = 0;
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
			return num2;
		}

		// Token: 0x04000843 RID: 2115
		private static readonly DatabaseSessionStorage m_current = new DatabaseSessionStorage();

		// Token: 0x020004C5 RID: 1221
		private sealed class CleanupStorage : Storage
		{
			// Token: 0x17000A95 RID: 2709
			// (get) Token: 0x0600243C RID: 9276 RVA: 0x00085E5E File Offset: 0x0008405E
			private static int CleanupBatchFactor
			{
				get
				{
					return Globals.Configuration.DBCleanupBatchFactor;
				}
			}

			// Token: 0x17000A96 RID: 2710
			// (get) Token: 0x0600243D RID: 9277 RVA: 0x00085E6A File Offset: 0x0008406A
			public static int ChunkCountPerBatch
			{
				get
				{
					return DatabaseSessionStorage.CleanupStorage.CleanupBatchFactor;
				}
			}

			// Token: 0x17000A97 RID: 2711
			// (get) Token: 0x0600243E RID: 9278 RVA: 0x00085E71 File Offset: 0x00084071
			public static int SegmentCountPerBatch
			{
				get
				{
					return DatabaseSessionStorage.CleanupStorage.CleanupBatchFactor / 10;
				}
			}

			// Token: 0x17000A98 RID: 2712
			// (get) Token: 0x0600243F RID: 9279 RVA: 0x00085E7B File Offset: 0x0008407B
			public static int SnapshotCountPerBatch
			{
				get
				{
					return DatabaseSessionStorage.CleanupStorage.CleanupBatchFactor / 200;
				}
			}

			// Token: 0x06002440 RID: 9280 RVA: 0x00085E88 File Offset: 0x00084088
			public IEnumerable<DatabaseSessionStorage.CleanupStorage.DeletedChunkInfo> RemoveSegmentedMapping()
			{
				using (InstrumentedSqlCommand command = this.NewStandardSqlCommand("RemoveSegmentedMapping", null))
				{
					command.CommandTimeout = ConnectionManager.SqlCleanupTimeoutSeconds;
					command.AddParameter("@DeleteCountPermanentChunk", SqlDbType.Int, DatabaseSessionStorage.CleanupStorage.ChunkCountPerBatch);
					command.AddParameter("@DeleteCountPermanentMapping", SqlDbType.Int, DatabaseSessionStorage.CleanupStorage.ChunkCountPerBatch);
					command.AddParameter("@DeleteCountTempChunk", SqlDbType.Int, DatabaseSessionStorage.CleanupStorage.ChunkCountPerBatch);
					command.AddParameter("@DeleteCountTempMapping", SqlDbType.Int, DatabaseSessionStorage.CleanupStorage.ChunkCountPerBatch);
					command.AddParameter("@MachineName", SqlDbType.NVarChar, Environment.MachineName);
					using (IDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							DatabaseSessionStorage.CleanupStorage.DeletedChunkInfo deletedChunkInfo = new DatabaseSessionStorage.CleanupStorage.DeletedChunkInfo(reader.GetGuid(0), reader.GetBoolean(1));
							yield return deletedChunkInfo;
						}
					}
					IDataReader reader = null;
				}
				InstrumentedSqlCommand command = null;
				yield break;
				yield break;
			}

			// Token: 0x06002441 RID: 9281 RVA: 0x00085E98 File Offset: 0x00084098
			public int RemoveSegments()
			{
				int num;
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("RemoveSegment", null))
				{
					instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCleanupTimeoutSeconds;
					instrumentedSqlCommand.AddParameter("@DeleteCountPermanent", SqlDbType.Int, DatabaseSessionStorage.CleanupStorage.SegmentCountPerBatch);
					instrumentedSqlCommand.AddParameter("@DeleteCountTemp", SqlDbType.Int, DatabaseSessionStorage.CleanupStorage.SegmentCountPerBatch);
					num = (int)instrumentedSqlCommand.ExecuteScalar();
				}
				return num;
			}

			// Token: 0x06002442 RID: 9282 RVA: 0x00085F14 File Offset: 0x00084114
			public bool TempChunkExists(Guid chunkId)
			{
				bool flag;
				using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("TempChunkExists", null))
				{
					instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCleanupTimeoutSeconds;
					instrumentedSqlCommand.AddParameter("@ChunkId", SqlDbType.UniqueIdentifier, chunkId);
					flag = (int)instrumentedSqlCommand.ExecuteScalar() > 0;
				}
				return flag;
			}

			// Token: 0x0200053B RID: 1339
			public struct DeletedChunkInfo
			{
				// Token: 0x0600255A RID: 9562 RVA: 0x00088065 File Offset: 0x00086265
				public DeletedChunkInfo(Guid chunkId, bool isPermanent)
				{
					this.m_chunkId = chunkId;
					this.m_isPermanent = isPermanent;
				}

				// Token: 0x17000AC2 RID: 2754
				// (get) Token: 0x0600255B RID: 9563 RVA: 0x00088075 File Offset: 0x00086275
				public Guid ChunkId
				{
					[DebuggerStepThrough]
					get
					{
						return this.m_chunkId;
					}
				}

				// Token: 0x17000AC3 RID: 2755
				// (get) Token: 0x0600255C RID: 9564 RVA: 0x0008807D File Offset: 0x0008627D
				public bool IsPermanent
				{
					[DebuggerStepThrough]
					get
					{
						return this.m_isPermanent;
					}
				}

				// Token: 0x0400137A RID: 4986
				private readonly Guid m_chunkId;

				// Token: 0x0400137B RID: 4987
				private readonly bool m_isPermanent;
			}
		}
	}
}
