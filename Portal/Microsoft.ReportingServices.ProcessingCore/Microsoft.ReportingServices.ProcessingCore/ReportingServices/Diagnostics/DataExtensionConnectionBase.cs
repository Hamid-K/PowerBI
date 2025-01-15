using System;
using System.Diagnostics;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x020005BC RID: 1468
	internal abstract class DataExtensionConnectionBase : IProcessingDataExtensionConnection
	{
		// Token: 0x06005323 RID: 21283 RVA: 0x0015DC14 File Offset: 0x0015BE14
		protected DataExtensionConnectionBase(ReportProcessing.CreateDataExtensionInstance deInstance, UserContext threadUser, ReportProcessing.ExecutionType execType, IDataProtection dataProtection, IAdditionalToken additionalToken)
			: this(deInstance, threadUser, execType, dataProtection, additionalToken, null)
		{
		}

		// Token: 0x06005324 RID: 21284 RVA: 0x0015DC24 File Offset: 0x0015BE24
		protected DataExtensionConnectionBase(ReportProcessing.CreateDataExtensionInstance deInstance, UserContext threadUser, ReportProcessing.ExecutionType execType, IDataProtection dataProtection, IAdditionalToken additionalToken, IDbConnectionPool connectionPool)
			: this(deInstance, threadUser, execType, dataProtection, additionalToken, connectionPool, new DefaultOpenConnectionExtension())
		{
		}

		// Token: 0x06005325 RID: 21285 RVA: 0x0015DC3A File Offset: 0x0015BE3A
		protected DataExtensionConnectionBase(ReportProcessing.CreateDataExtensionInstance deInstance, UserContext threadUser, ReportProcessing.ExecutionType execType, IDataProtection dataProtection, IAdditionalToken additionalToken, IDbConnectionPool connectionPool, IOpenConnectionExtension openConnectionExtension)
		{
			this.m_dataExtInstance = deInstance;
			this.m_threadUser = threadUser;
			this.m_execType = execType;
			this.m_dp = dataProtection;
			this.m_additionalToken = additionalToken;
			this.m_connectionPool = connectionPool;
			this.m_openConnectionExtension = openConnectionExtension;
		}

		// Token: 0x06005326 RID: 21286 RVA: 0x0015DC77 File Offset: 0x0015BE77
		public void DataSetRetrieveForReportInstance(ICatalogItemContext itemContext, ParameterInfoCollection reportParameters)
		{
		}

		// Token: 0x06005327 RID: 21287 RVA: 0x0015DC7C File Offset: 0x0015BE7C
		public void HandleImpersonation(IProcessingDataSource dataSource, DataSourceInfo dataSourceInfo, string datasetName, IDbConnection connection, global::System.Action afterImpersonationAction)
		{
			RSTrace.DataExtensionTracer.Assert(dataSource != null || dataSourceInfo != null, "At least one of dataSourceObj or dataSourceInfo must be non-null");
			RSTrace.DataExtensionTracer.Assert(afterImpersonationAction != null, "afterImpersonationAction must be non-null");
			RSTrace.DataExtensionTracer.Assert(connection != null, "conn must be non-null");
			string text;
			if (dataSource != null)
			{
				text = dataSource.Name;
			}
			else
			{
				text = dataSourceInfo.Name;
			}
			Guid empty = Guid.Empty;
			if (dataSourceInfo != null)
			{
				if (this.m_execType == ReportProcessing.ExecutionType.ServiceAccount)
				{
					if (!this.GoodForExecutionUnderServiceAccount(dataSourceInfo))
					{
						throw new InvalidDataSourceCredentialSettingException();
					}
				}
				else if (this.m_execType == ReportProcessing.ExecutionType.SurrogateAccount && !this.GoodForUnattendedSurrogateExecution(dataSourceInfo))
				{
					throw new InvalidDataSourceCredentialSettingException();
				}
			}
			if (dataSourceInfo != null && dataSourceInfo.DataSourceFaultContext != null)
			{
				throw new FaultedDataSourceException(dataSourceInfo.DataSourceFaultContext.ErrorCode, dataSourceInfo.DataSourceFaultContext.ErrorString);
			}
			ConnectionContext connectionContext = this.GetConnectionContext(dataSource, dataSourceInfo, out empty);
			IDbConnectionExtension dbConnectionExtension = connection as IDbConnectionExtension;
			ITokenDataExtension tokenDataExtension = connection as ITokenDataExtension;
			bool flag = false;
			ConnectionWrapper connectionWrapper = connection as ConnectionWrapper;
			bool flag2 = connectionWrapper != null && connectionWrapper.WrappedManagedProvider;
			ImpersonationContext impersonationContext = null;
			if (dbConnectionExtension == null)
			{
				if (!flag2 && ConnectionSecurity.None != connectionContext.ConnectionSecurity)
				{
					throw new ReportProcessingException(ErrorCode.rsDataExtensionWithoutConnectionExtension, null, new object[] { text });
				}
				if (flag2 && ConnectionSecurity.UseDataSourceCredentials == connectionContext.ConnectionSecurity)
				{
					throw new ReportProcessingException(ErrorCode.rsManagedDataProviderWithoutConnectionExtension, null, new object[] { text });
				}
			}
			try
			{
				switch (connectionContext.ConnectionSecurity)
				{
				case ConnectionSecurity.UseIntegratedSecurity:
					if (this.m_execType != ReportProcessing.ExecutionType.Live)
					{
						throw new InvalidDataSourceCredentialSettingException();
					}
					try
					{
						impersonationContext = new ImpersonationContext(this.m_threadUser);
					}
					catch (Exception ex)
					{
						throw new ReportProcessingException(ErrorCode.rsErrorImpersonatingUser, ex, new object[] { text });
					}
					if (dbConnectionExtension != null)
					{
						dbConnectionExtension.IntegratedSecurity = true;
					}
					break;
				case ConnectionSecurity.ImpersonateWindowsUser:
					try
					{
						impersonationContext = new ImpersonationContext(connectionContext.UserName, connectionContext.Password, connectionContext.DomainName);
					}
					catch (Exception ex2)
					{
						throw new ReportProcessingException(ErrorCode.rsErrorImpersonatingUser, ex2, new object[] { text });
					}
					if (dbConnectionExtension != null)
					{
						dbConnectionExtension.IntegratedSecurity = true;
					}
					break;
				case ConnectionSecurity.UseDataSourceCredentials:
					if (dbConnectionExtension != null)
					{
						dbConnectionExtension.UserName = connectionContext.UserName;
						dbConnectionExtension.Password = connectionContext.DecryptedPassword;
					}
					break;
				case ConnectionSecurity.None:
					if (!this.TryCreateImpersonationContextForNoCredentials(out impersonationContext) && !flag)
					{
						throw new InvalidDataSourceCredentialSettingException();
					}
					if (flag)
					{
						byte[] array = ((this.m_additionalToken != null) ? this.m_additionalToken.GetAdditionalToken() : null);
						if (array != null)
						{
							tokenDataExtension.SetUserToken(array);
						}
						else if (impersonationContext == null)
						{
							throw new InvalidDataSourceCredentialSettingException();
						}
					}
					break;
				case ConnectionSecurity.ImpersonateServiceAccount:
					if (dbConnectionExtension == null || !(dbConnectionExtension is IConnectionRevertToServiceAccount))
					{
						throw new ReportProcessingException(ErrorCode.rsErrorImpersonateServiceAccountNotAllowed, new object[] { text });
					}
					try
					{
						impersonationContext = new ImpersonationContext();
					}
					catch (Exception ex3)
					{
						throw new ReportProcessingException(ErrorCode.rsErrorImpersonatingServiceAccount, ex3, new object[] { text });
					}
					if (dbConnectionExtension != null)
					{
						dbConnectionExtension.IntegratedSecurity = true;
					}
					break;
				}
				if (connectionContext.ImpersonateUser && dbConnectionExtension != null)
				{
					try
					{
						IImpersonationFormat impersonationFormat = dbConnectionExtension as IImpersonationFormat;
						if (impersonationFormat != null && impersonationFormat.GetImpersonationFormat() == ImpersonationFormat.UPN && this.m_threadUser.AuthenticationType == AuthenticationType.Windows)
						{
							using (new ImpersonationContext(this.m_threadUser))
							{
								dbConnectionExtension.Impersonate = UserNameLookup.LookupUsername(ExtendedNameFormat.NameUserPrincipal);
								goto IL_0313;
							}
						}
						dbConnectionExtension.Impersonate = this.m_threadUser.UserName;
						IL_0313:;
					}
					catch (Exception ex4)
					{
						throw new ReportProcessingException(ErrorCode.rsErrorImpersonatingUser, ex4, new object[] { text });
					}
				}
				afterImpersonationAction();
			}
			finally
			{
				if (impersonationContext != null)
				{
					impersonationContext.Dispose();
				}
			}
		}

		// Token: 0x06005328 RID: 21288 RVA: 0x0015E060 File Offset: 0x0015C260
		public IDbConnection OpenDataSourceExtensionConnection(IProcessingDataSource dataSourceObj, string connectString, DataSourceInfo dataSourceInfo, string datasetName)
		{
			Guid empty = Guid.Empty;
			if (DataSourceInfo.HasUseridReference(connectString))
			{
				connectString = DataSourceInfo.ReplaceAllUseridReferences(connectString, this.m_threadUser.UserName);
			}
			ConnectionContext connectionContext = this.GetConnectionContext(dataSourceObj, dataSourceInfo, out empty);
			IDbConnection conn = null;
			if (this.m_connectionPool != null)
			{
				connectionContext.ConnectionString = connectString;
				conn = this.m_connectionPool.GetConnection(connectionContext.CreateConnectionKey());
			}
			if (conn != null)
			{
				this.OnPooledConnectionOpen();
			}
			else
			{
				try
				{
					ReportDataSource reportDataSource = new ReportDataSource(connectionContext.DataSourceType, empty, this.m_dataExtInstance);
					conn = reportDataSource.CreateConnection();
					conn.ConnectionString = connectString;
					this.HandleImpersonation(dataSourceObj, dataSourceInfo, datasetName, conn, delegate
					{
						this.m_openConnectionExtension.OpenConnection(conn, connectString, dataSourceInfo);
						this.OnConnectionOpen();
					});
				}
				catch (RSException)
				{
					throw;
				}
				catch (Exception ex)
				{
					string text;
					if (dataSourceObj != null)
					{
						text = dataSourceObj.Name;
					}
					else
					{
						text = dataSourceInfo.Name;
					}
					this.OnConnectionOpenFailure();
					throw new ReportProcessingException(ErrorCode.rsErrorOpeningConnection, ex, new object[] { text });
				}
			}
			return conn;
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x0015E1C0 File Offset: 0x0015C3C0
		private ConnectionContext GetConnectionContext(IProcessingDataSource dataSourceObj, DataSourceInfo dataSourceInfo)
		{
			Guid guid;
			return this.GetConnectionContext(dataSourceObj, dataSourceInfo, out guid);
		}

		// Token: 0x0600532A RID: 21290 RVA: 0x0015E1D8 File Offset: 0x0015C3D8
		private ConnectionContext GetConnectionContext(IProcessingDataSource dataSourceObj, DataSourceInfo dataSourceInfo, out Guid modelId)
		{
			modelId = Guid.Empty;
			ConnectionContext connectionContext = new ConnectionContext();
			if (dataSourceInfo != null)
			{
				if (DataSourceInfo.CredentialsRetrievalOption.None != dataSourceInfo.CredentialsRetrieval)
				{
					if (DataSourceInfo.CredentialsRetrievalOption.Integrated == dataSourceInfo.CredentialsRetrieval)
					{
						connectionContext.ConnectionSecurity = ConnectionSecurity.UseIntegratedSecurity;
					}
					else if (DataSourceInfo.CredentialsRetrievalOption.ServiceAccount == dataSourceInfo.CredentialsRetrieval)
					{
						connectionContext.ConnectionSecurity = ConnectionSecurity.ImpersonateServiceAccount;
					}
					else
					{
						if (dataSourceInfo.WindowsCredentials)
						{
							connectionContext.ConnectionSecurity = ConnectionSecurity.ImpersonateWindowsUser;
							connectionContext.UserName = dataSourceInfo.GetUserNameOnly(this.m_dp);
							connectionContext.DomainName = dataSourceInfo.GetDomainOnly(this.m_dp);
						}
						else
						{
							connectionContext.ConnectionSecurity = ConnectionSecurity.UseDataSourceCredentials;
							connectionContext.UserName = dataSourceInfo.GetUserName(this.m_dp);
						}
						connectionContext.Password = dataSourceInfo.GetPassword(this.m_dp);
					}
					if (dataSourceInfo.ImpersonateUser)
					{
						connectionContext.ImpersonateUser = true;
						connectionContext.ImpersonateUserName = this.m_threadUser.UserName;
					}
				}
				if (dataSourceObj != null)
				{
					dataSourceObj.Type = dataSourceInfo.Extension;
					dataSourceObj.SharedDataSourceReferencePath = dataSourceInfo.DataSourceReference;
				}
				connectionContext.DataSourceType = dataSourceInfo.Extension;
				modelId = dataSourceInfo.ModelID;
			}
			else
			{
				if (dataSourceObj.IntegratedSecurity)
				{
					connectionContext.ConnectionSecurity = ConnectionSecurity.UseIntegratedSecurity;
				}
				connectionContext.DataSourceType = dataSourceObj.Type;
			}
			return connectionContext;
		}

		// Token: 0x0600532B RID: 21291 RVA: 0x0015E300 File Offset: 0x0015C500
		public void CloseConnection(IDbConnection connection, IProcessingDataSource dataSourceObj, DataSourceInfo dataSourceInfo)
		{
			if (connection != null)
			{
				bool flag = true;
				IDbPoolableConnection dbPoolableConnection = connection as IDbPoolableConnection;
				if (this.m_connectionPool != null && dbPoolableConnection != null)
				{
					if (dbPoolableConnection.IsFromPool)
					{
						this.OnPooledConnectionClose();
					}
					ConnectionContext connectionContext = this.GetConnectionContext(dataSourceObj, dataSourceInfo);
					try
					{
						connectionContext.ConnectionString = dbPoolableConnection.GetConnectionStringForPooling();
						flag = !this.m_connectionPool.PoolConnection(dbPoolableConnection, connectionContext.CreateConnectionKey());
					}
					catch (Exception ex)
					{
						flag = true;
						RSTrace.DataExtensionTracer.Trace(TraceLevel.Error, "Exception occurred pooling the connection: {0}", new object[] { ex.ToString() });
					}
				}
				if (flag)
				{
					this.InternalCloseConnection(connection);
				}
			}
		}

		// Token: 0x0600532C RID: 21292 RVA: 0x0015E3A0 File Offset: 0x0015C5A0
		public void CloseConnectionWithoutPool(IDbConnection connection)
		{
			if (connection != null)
			{
				IDbPoolableConnection dbPoolableConnection = connection as IDbPoolableConnection;
				if (dbPoolableConnection != null && dbPoolableConnection.IsFromPool)
				{
					this.OnPooledConnectionClose();
				}
				this.InternalCloseConnection(connection);
			}
		}

		// Token: 0x0600532D RID: 21293 RVA: 0x0015E3CF File Offset: 0x0015C5CF
		private void InternalCloseConnection(IDbConnection connection)
		{
			connection.Close();
			connection.Dispose();
			this.OnConnectionClose();
		}

		// Token: 0x17001EE4 RID: 7908
		// (get) Token: 0x0600532E RID: 21294 RVA: 0x0015E3E3 File Offset: 0x0015C5E3
		public bool MustResolveSharedDataSources
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x0015E3E6 File Offset: 0x0015C5E6
		protected virtual bool GoodForExecutionUnderServiceAccount(DataSourceInfo dataSourceInfo)
		{
			RSTrace.CatalogTrace.Assert(false, "ExecutionType is not Live and GoodForExecutionUnderServiceAccount is not implemented.");
			return false;
		}

		// Token: 0x06005330 RID: 21296 RVA: 0x0015E3F9 File Offset: 0x0015C5F9
		protected virtual bool GoodForUnattendedSurrogateExecution(DataSourceInfo dataSourceInfo)
		{
			RSTrace.CatalogTrace.Assert(false, "ExecutionType is not Live and GoodForUnattendedSurrogateExecution is not implemented.");
			return false;
		}

		// Token: 0x06005331 RID: 21297 RVA: 0x0015E40C File Offset: 0x0015C60C
		protected virtual bool TryCreateImpersonationContextForNoCredentials(out ImpersonationContext impersonationContext)
		{
			if (ProcessingContext.Configuration.IsSurrogatePresent)
			{
				try
				{
					impersonationContext = new ImpersonationContext(ProcessingContext.Configuration.SurrogateUserName, ProcessingContext.Configuration.SurrogatePassword, ProcessingContext.Configuration.SurrogateDomain);
				}
				catch (LogonFailedException ex)
				{
					throw new ServerConfigurationErrorException(ex, "Failed to impersonate unattended execution account.");
				}
				return true;
			}
			impersonationContext = null;
			return false;
		}

		// Token: 0x06005332 RID: 21298 RVA: 0x0015E470 File Offset: 0x0015C670
		protected virtual void OnPooledConnectionOpen()
		{
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x0015E472 File Offset: 0x0015C672
		protected virtual void OnPooledConnectionClose()
		{
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x0015E474 File Offset: 0x0015C674
		protected virtual void OnConnectionOpen()
		{
		}

		// Token: 0x06005335 RID: 21301 RVA: 0x0015E476 File Offset: 0x0015C676
		protected virtual void OnConnectionOpenFailure()
		{
		}

		// Token: 0x06005336 RID: 21302 RVA: 0x0015E478 File Offset: 0x0015C678
		protected virtual void OnConnectionClose()
		{
		}

		// Token: 0x040029E4 RID: 10724
		private readonly ReportProcessing.CreateDataExtensionInstance m_dataExtInstance;

		// Token: 0x040029E5 RID: 10725
		private readonly UserContext m_threadUser;

		// Token: 0x040029E6 RID: 10726
		private readonly ReportProcessing.ExecutionType m_execType;

		// Token: 0x040029E7 RID: 10727
		private readonly IDataProtection m_dp;

		// Token: 0x040029E8 RID: 10728
		private readonly IAdditionalToken m_additionalToken;

		// Token: 0x040029E9 RID: 10729
		private readonly IDbConnectionPool m_connectionPool;

		// Token: 0x040029EA RID: 10730
		private readonly IOpenConnectionExtension m_openConnectionExtension;
	}
}
