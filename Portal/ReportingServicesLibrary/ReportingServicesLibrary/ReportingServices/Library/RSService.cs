using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Xml;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.Library.Soap2010;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.SemanticQueryEngine;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200009F RID: 159
	internal sealed class RSService : IRSService, IRSHostingService, IPathTranslator
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x0001A454 File Offset: 0x00018654
		public RSService(string userName, object userToken, AuthenticationType authType, IRSRequestInspector requestInspector)
		{
			this.m_userContext = new UserContext(userName, userToken, authType);
			this.m_requestInspector = requestInspector;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001A4AC File Offset: 0x000186AC
		public RSService(string userName, object userToken, AuthenticationType authType)
		{
			this.m_userContext = new UserContext(userName, userToken, authType);
			if (authType == AuthenticationType.Windows && userToken == null)
			{
				throw new InternalCatalogException("Expected valid user token");
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001A50E File Offset: 0x0001870E
		public RSService(string userName, AuthenticationType authType, string scopeUrl)
			: this(userName, authType)
		{
			this.InitScope(scopeUrl);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001A520 File Offset: 0x00018720
		internal RSService(UserContext userContext, string scopeUrl)
		{
			RSTrace.CatalogTrace.Assert(userContext != null, "Expect user context");
			this.m_userContext = userContext;
			this.InitScope(scopeUrl);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001A584 File Offset: 0x00018784
		public RSService(bool checkSecurity)
		{
			if (checkSecurity)
			{
				throw new InternalCatalogException("checkSecurity without credentials");
			}
			this.m_checkAccess = false;
			this.m_userContext = new UserContext();
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001A5E8 File Offset: 0x000187E8
		public RSService(string userName, AuthenticationType authType)
		{
			if (userName == null)
			{
				throw new InternalCatalogException("Expected valid user name");
			}
			this.m_userContext = new UserContext(userName, null, authType);
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001A646 File Offset: 0x00018846
		public IRSRequestInspector RequestInspector
		{
			get
			{
				if (this.m_requestInspector == null)
				{
					this.m_requestInspector = new AspNetRequestInspector();
				}
				return this.m_requestInspector;
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001A661 File Offset: 0x00018861
		public void ThrowIfSchedulerNotRunning()
		{
			this.Storage.ThrowIfSchedulerNotRunning();
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001A66E File Offset: 0x0001886E
		public bool IsSchedulerRunning()
		{
			return this.Storage.IsSchedulerRunning();
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00005BF2 File Offset: 0x00003DF2
		private void InitScope(string scopeUrl)
		{
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001A67C File Offset: 0x0001887C
		public RSService GetNewService()
		{
			RSService rsservice;
			if (!this.UserContext.IsInitialized)
			{
				rsservice = new RSService(false);
			}
			else if (this.UserContext.UserToken == null)
			{
				rsservice = new RSService(this.UserName, this.UserContext.AuthenticationType);
			}
			else
			{
				rsservice = new RSService(this.UserName, this.UserContext.UserToken, this.UserContext.AuthenticationType);
			}
			rsservice.UserContext.AdditionalUserToken = this.UserContext.AdditionalUserToken;
			return rsservice;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001A700 File Offset: 0x00018900
		public void ExecuteNestedTransaction(Action<RSService> action)
		{
			this.ExecuteNestedTransaction<int>(delegate(RSService newService)
			{
				action(newService);
				return 0;
			});
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001A730 File Offset: 0x00018930
		public T ExecuteNestedTransaction<T>(Func<RSService, T> func)
		{
			RSService newService = this.GetNewService();
			newService.WillDisconnectStorage();
			T t;
			try
			{
				t = func(newService);
			}
			catch (Exception ex)
			{
				newService.AbortTransaction();
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				newService.DisconnectStorage();
			}
			return t;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001A790 File Offset: 0x00018990
		public void ImpersonateClient()
		{
			RSTrace.CatalogTrace.Assert(this.UserContext.AuthenticationType != AuthenticationType.Federation, "UserContext.AuthenticationType != AuthenticationType.Federation");
			if (this.UserContext.AuthenticationType != AuthenticationType.Windows)
			{
				return;
			}
			new ImpersonationContext(this.UserContext);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001A7D0 File Offset: 0x000189D0
		public IDisposable SetStreamFactory(StreamFactoryBase streamFactory)
		{
			RSTrace.CatalogTrace.Assert(streamFactory != null, "streamFactory");
			StreamManager streamManager = new StreamManager(streamFactory);
			RSService.WorkspaceContext workspaceContext = new RSService.WorkspaceContext(this);
			RSService.StreamManagerDisposalBinding streamManagerDisposalBinding = new RSService.StreamManagerDisposalBinding(streamManager);
			this.m_streamManagerWorkspaces.Push(streamManagerDisposalBinding);
			return workspaceContext;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001A816 File Offset: 0x00018A16
		public void SuspendStreamCleanup()
		{
			this.m_streamManagerWorkspaces.SuspendCleanup();
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001A823 File Offset: 0x00018A23
		public void ResumeStreamCleanup()
		{
			this.m_streamManagerWorkspaces.ResumeCleanup();
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001A830 File Offset: 0x00018A30
		public void EnsureSharePointServicesAccessible()
		{
			this.ServiceHelper.EnsureSharePointServicesAccessible();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001A83D File Offset: 0x00018A3D
		public void PopulateAdditionalToken(string itemPath)
		{
			this.ServiceHelper.PopulateAdditionalToken(this, itemPath);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001A84C File Offset: 0x00018A4C
		public void EnsureSecurityZone(string itemPath)
		{
			this.ServiceHelper.EnsureSecurityZone(itemPath);
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001A85C File Offset: 0x00018A5C
		private int SystemReportTimeout
		{
			get
			{
				if (this.m_SystemReportTimeoutParam == null)
				{
					this.m_SystemReportTimeoutParam = new IntParameter(CachedSystemProperties.Instance, Global.m_Tracer, "SystemReportTimeout", CachedSystemProperties.Instance.GetParameter("SystemReportTimeout"), 1800, "seconds");
					this.m_SystemReportTimeoutParam.TraceSuccess = false;
					this.m_SystemReportTimeoutParam.MinValue = -1;
					this.m_SystemReportTimeout = this.m_SystemReportTimeoutParam.Value;
				}
				return this.m_SystemReportTimeout;
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001A8D4 File Offset: 0x00018AD4
		public int GetReportTimeout(ItemProperties properties)
		{
			string reportTimeout = properties.ReportTimeout;
			if (reportTimeout == null)
			{
				return this.SystemReportTimeout;
			}
			int num;
			try
			{
				num = int.Parse(reportTimeout, CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				throw new InternalCatalogException("unexpected Report Timeout value" + reportTimeout);
			}
			return num;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001A924 File Offset: 0x00018B24
		public ServerDataExtensionConnectionWrapper CreateAndOpenDataExtensionConnectionWrapper(CatalogItemContext dataSourceItemContext, DataSourceInfo dataSourceInfo, IDbConnectionPool connectionPool)
		{
			IAdditionalToken additionalToken = new ServerAdditionalToken(this, dataSourceItemContext);
			IProcessingDataExtensionConnection processingDataExtensionConnection = new ServerDataExtensionConnection(this.HowToCreateDataExtensionInstance, this.UserContext, ReportProcessing.ExecutionType.Live, additionalToken, connectionPool);
			return ServerDataExtensionConnectionWrapper.Open(dataSourceInfo, processingDataExtensionConnection);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001A958 File Offset: 0x00018B58
		public Microsoft.ReportingServices.DataProcessing.IDbConnection OpenDataSourceConnection(DataSourceInfo dataSourceInfo, ReportProcessing.CreateDataExtensionInstance createDataExtensionInstanceFunction)
		{
			string text = null;
			global::System.Data.IDbConnection dbConnection;
			return this.OpenDataSourceConnection(dataSourceInfo, createDataExtensionInstanceFunction, true, false, text, out dbConnection);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001A974 File Offset: 0x00018B74
		public Microsoft.ReportingServices.DataProcessing.IDbConnection OpenDataSourceConnection(DataSourceInfo dataSourceInfo, ReportProcessing.CreateDataExtensionInstance createDataExtensionInstanceFunction, bool isUnattendedExecution, bool unwrapConnection, string requestUserName, out global::System.Data.IDbConnection unwrappedConnection)
		{
			ConnectionContext connectionContext = this.GetConnectionContext(dataSourceInfo, requestUserName);
			return this.OpenDataSourceConnection(dataSourceInfo, createDataExtensionInstanceFunction, isUnattendedExecution, unwrapConnection, connectionContext, out unwrappedConnection);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001A99C File Offset: 0x00018B9C
		private Microsoft.ReportingServices.DataProcessing.IDbConnection OpenDataSourceConnection(DataSourceInfo dataSourceInfo, ReportProcessing.CreateDataExtensionInstance createDataExtensionInstanceFunction, bool isUnattendedExecution, bool unwrapConnection, ConnectionContext connectionContext, out global::System.Data.IDbConnection unwrappedConnection)
		{
			unwrappedConnection = null;
			dataSourceInfo.ThrowIfNotUsable(new ServerDataSourceSettings(Globals.Configuration.IsSurrogatePresent, Global.EnableIntegratedSecurity));
			if (isUnattendedExecution && !DataSourceCatalogItem.GoodForUnattendedExecution(dataSourceInfo))
			{
				throw new InvalidDataSourceCredentialSettingException();
			}
			Microsoft.ReportingServices.DataProcessing.IDbConnection dbConnection2;
			try
			{
				Microsoft.ReportingServices.DataProcessing.IDbConnection dbConnection = new ReportDataSource(dataSourceInfo.Extension, dataSourceInfo.ModelID, createDataExtensionInstanceFunction).CreateConnection();
				IDbConnectionExtension dbConnectionExtension = dbConnection as IDbConnectionExtension;
				ITokenDataExtension tokenDataExtension = dbConnection as ITokenDataExtension;
				dbConnection.ConnectionString = connectionContext.ConnectionString;
				bool flag = false;
				ConnectionSecurity connectionSecurity = connectionContext.ConnectionSecurity;
				ImpersonationContext impersonationContext = null;
				try
				{
					switch (connectionSecurity)
					{
					case ConnectionSecurity.UseIntegratedSecurity:
						if (isUnattendedExecution)
						{
							throw new InvalidDataSourceCredentialSettingException();
						}
						impersonationContext = new ImpersonationContext(this.UserContext);
						if (dbConnectionExtension != null)
						{
							dbConnectionExtension.IntegratedSecurity = true;
						}
						break;
					case ConnectionSecurity.ImpersonateWindowsUser:
						impersonationContext = new ImpersonationContext(connectionContext.UserName, connectionContext.Password, connectionContext.DomainName);
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
						if (Globals.Configuration.IsSurrogatePresent)
						{
							impersonationContext = SurrogateContextFactory.CreateSurrogateContext();
						}
						else if (!flag)
						{
							throw new InvalidDataSourceCredentialSettingException();
						}
						if (flag)
						{
							byte[] array = this.UserContext.AdditionalUserToken;
							if (array == null && Microsoft.ReportingServices.Diagnostics.ProcessingContext.ReqContext != null)
							{
								this.PopulateAdditionalToken(Microsoft.ReportingServices.Diagnostics.ProcessingContext.ReqContext.ReportServerVirtualDirectoryUri.AbsoluteUri);
								array = this.UserContext.AdditionalUserToken;
							}
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
					}
					if (dataSourceInfo.ImpersonateUser && dbConnectionExtension != null && connectionContext.ImpersonateUserName != null)
					{
						try
						{
							dbConnectionExtension.Impersonate = connectionContext.ImpersonateUserName;
						}
						catch (Exception ex)
						{
							throw new DataSourceOpenException(dataSourceInfo.Name, ex);
						}
					}
					if (unwrapConnection)
					{
						IDbConnectionWrapper dbConnectionWrapper = dbConnection as IDbConnectionWrapper;
						if (dbConnectionWrapper == null)
						{
							throw new ServerConfigurationErrorException(string.Format(CultureInfo.InvariantCulture, "Data extension '{0}' is configured for model generation. However it doesn't implement IDbConnectionWrapper interface.", dataSourceInfo.Extension));
						}
						unwrappedConnection = dbConnectionWrapper.Connection;
						if (unwrappedConnection == null)
						{
							throw new ServerConfigurationErrorException(string.Format(CultureInfo.InvariantCulture, "Data extension '{0}' has failed to return IDbConnectionWrapper.Connection for model generation.", dataSourceInfo.Extension));
						}
						try
						{
							unwrappedConnection.Open();
							goto IL_0231;
						}
						catch (Exception ex2)
						{
							throw new DataSourceOpenException(dataSourceInfo.Name, ex2);
						}
					}
					try
					{
						dbConnection.Open();
					}
					catch (Exception ex3)
					{
						throw new DataSourceOpenException(dataSourceInfo.Name, ex3);
					}
					IL_0231:;
				}
				finally
				{
					if (impersonationContext != null)
					{
						impersonationContext.Dispose();
					}
				}
				dbConnection2 = dbConnection;
			}
			catch (Exception ex4)
			{
				if (!(ex4 is RSException))
				{
					throw new CannotPrepareQueryException(ex4);
				}
				throw;
			}
			return dbConnection2;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001AC80 File Offset: 0x00018E80
		private ConnectionContext GetConnectionContext(DataSourceInfo dataSourceInfo, string requestUserName)
		{
			ConnectionContext connectionContext = new ConnectionContext();
			connectionContext.DataSourceType = dataSourceInfo.Extension;
			connectionContext.ConnectionString = dataSourceInfo.GetConnectionString(DataProtection.Instance);
			if (dataSourceInfo.ImpersonateUser)
			{
				connectionContext.ImpersonateUser = true;
				connectionContext.ImpersonateUserName = requestUserName;
			}
			switch (dataSourceInfo.CredentialsRetrieval)
			{
			case DataSourceInfo.CredentialsRetrievalOption.Store:
				connectionContext.UserName = dataSourceInfo.GetUserNameOnly(DataProtection.Instance);
				connectionContext.Password = dataSourceInfo.GetPassword(DataProtection.Instance);
				if (dataSourceInfo.WindowsCredentials)
				{
					connectionContext.ConnectionSecurity = ConnectionSecurity.ImpersonateWindowsUser;
					connectionContext.DomainName = dataSourceInfo.GetDomainOnly(DataProtection.Instance);
					return connectionContext;
				}
				connectionContext.ConnectionSecurity = ConnectionSecurity.UseDataSourceCredentials;
				return connectionContext;
			case DataSourceInfo.CredentialsRetrievalOption.Integrated:
				connectionContext.ConnectionSecurity = ConnectionSecurity.UseIntegratedSecurity;
				return connectionContext;
			case DataSourceInfo.CredentialsRetrievalOption.None:
				connectionContext.ConnectionSecurity = ConnectionSecurity.None;
				return connectionContext;
			case DataSourceInfo.CredentialsRetrievalOption.SecureStore:
				connectionContext.UserName = dataSourceInfo.GetUserNameOnly(DataProtection.Instance);
				connectionContext.Password = dataSourceInfo.GetPassword(DataProtection.Instance);
				this.Tracer.Assert(dataSourceInfo.WindowsCredentials, "Only WindowsCredentials is supported with Secure Store");
				connectionContext.ConnectionSecurity = ConnectionSecurity.ImpersonateWindowsUser;
				connectionContext.DomainName = dataSourceInfo.GetDomainOnly(DataProtection.Instance);
				return connectionContext;
			}
			throw new InvalidDataSourceCredentialSettingException();
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001ADAC File Offset: 0x00018FAC
		public byte[] GetRenderResource(CatalogItemContext itemContext, out string mimeType)
		{
			if (itemContext.RSRequestParameters.FormatParamValue == null || itemContext.RSRequestParameters.FormatParamValue.Length == 0)
			{
				throw new MissingParameterException("Format");
			}
			ReportProcessing processingEngine = Global.GetProcessingEngine();
			this.StreamManager.NeedCacheableStreams = false;
			processingEngine.CallRenderer(itemContext, new ServerExtensionFactory(), new CreateAndRegisterStream(this.StreamManager.GetNewStream));
			this.Tracer.Trace(TraceLevel.Verbose, "Call to CallRenderer completed");
			return this.StreamManager.GetResourceFromPrimaryStream(out mimeType);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001AE30 File Offset: 0x00019030
		private void ProcessReportParameters(ReportProcessing repProc, CatalogItemContext reportContext, IChunkFactory getChunkFactory, ReportProcessing.CreateDataExtensionInstance howToCreateDataExtensionInstance, RuntimeDataSourceInfoCollection allDataSources, RuntimeDataSetInfoCollection allDataSets, ParameterInfoCollection newParameters, DateTime executionTime, bool isSnapshot)
		{
			RSTrace.CatalogTrace.Assert(this.StreamManager != null, "StreamManager");
			ISharedDataSet sharedDataSet = null;
			if (getChunkFactory == null)
			{
				sharedDataSet = null;
			}
			else if (getChunkFactory is ReportSnapshot)
			{
				sharedDataSet = new SharedDataSetExecution(new RSServiceDataProvider(this), (ReportSnapshot)getChunkFactory);
			}
			else if (getChunkFactory is SnapshotManager)
			{
				sharedDataSet = new SharedDataSetExecution(new RSServiceDataProvider(this), (SnapshotManager)getChunkFactory);
			}
			else
			{
				this.Tracer.Assert(false, "if not null, getChunkFactory parameter should be an instance of ReportSnaphot or of SnapshotManager");
			}
			ReportProcessing.ExecutionType executionType;
			using (SurrogateContextFactory.CreateContext(out executionType))
			{
				ReportProcessingContext reportProcessingContext = new ReportProcessingContext(reportContext, this.UserName, newParameters, allDataSources, allDataSets, null, null, getChunkFactory, executionType, Localization.ClientPrimaryCulture, this.GetUserProfileState(Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext.JobType), UserProfileState.None, new ServerDataExtensionConnection(howToCreateDataExtensionInstance, this.UserContext, executionType, new ServerAdditionalToken(this, reportContext)), ReportRuntimeSetup.GetDefault(), new CreateAndRegisterStream(this.StreamManager.GetNewStream), false, ServerJobContext.ConstructJobContext(Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext), new ServerExtensionFactory(), DataProtection.Instance, sharedDataSet);
				bool flag;
				repProc.ProcessReportParameters(executionTime, reportProcessingContext, isSnapshot, out flag);
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001AF48 File Offset: 0x00019148
		public void ProcessingGetResource(ICatalogItemContext reportContextInterface, string imageUrl, out byte[] resource, out string mimeType, out bool registerExternalWarning, out bool registerInvalidSizeWarning)
		{
			this.ProcessingGetResource(reportContextInterface, imageUrl, false, null, out resource, out mimeType, out registerExternalWarning, out registerInvalidSizeWarning);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001AF68 File Offset: 0x00019168
		public void ProcessingGetResource(ICatalogItemContext reportContextInterface, string imageUrl, bool alwaysThrowOnWebException, ExternalResourceAbortHelper abortHelper, out byte[] resource, out string mimeType, out bool registerExternalWarning, out bool registerInvalidSizeWarning)
		{
			CatalogItemContext catalogItemContext = null;
			resource = null;
			mimeType = null;
			registerExternalWarning = false;
			registerInvalidSizeWarning = false;
			int num = ExternalResourceLoader.MaxResourceSizeUnlimited;
			if (Global.ProcessingConfiguration.RdlSandboxing != null)
			{
				num = Global.ProcessingConfiguration.RdlSandboxing.MaxResourceSizeKB * 1024;
			}
			if (reportContextInterface != null)
			{
				CatalogItemContext catalogItemContext2 = reportContextInterface as CatalogItemContext;
				RSTrace.CatalogTrace.Assert(catalogItemContext2 != null);
				catalogItemContext = (CatalogItemContext)catalogItemContext2.Combine(imageUrl, false);
			}
			if (catalogItemContext != null)
			{
				resource = this.GetInternalResource(catalogItemContext.ItemPath, out mimeType);
				if (!ExternalResourceLoader.IsValidResourceSize(num, resource))
				{
					resource = null;
					mimeType = null;
					registerInvalidSizeWarning = true;
					return;
				}
			}
			else
			{
				bool flag;
				try
				{
					flag = this.ServiceHelper.GetExternalResourceFromSite(imageUrl, this.UserContext, out resource, out mimeType);
				}
				catch (Exception ex)
				{
					RSTrace.ProcessingTracer.Trace(TraceLevel.Info, "Error retrieving external resource '{0}' from SharePoint site: {1}", new object[] { imageUrl, ex.Message });
					flag = false;
				}
				if (flag)
				{
					if (!ExternalResourceLoader.IsValidResourceSize(num, resource))
					{
						resource = null;
						mimeType = null;
						registerInvalidSizeWarning = true;
						return;
					}
				}
				else
				{
					string text = null;
					string text2 = null;
					string text3 = null;
					if (Globals.Configuration.IsSurrogatePresent)
					{
						text = Globals.Configuration.SurrogateUserName;
						text2 = Globals.Configuration.SurrogatePassword;
						text3 = Globals.Configuration.SurrogateDomain;
					}
					try
					{
						resource = ExternalResourceLoader.GetExternalResource(imageUrl, false, text, text2, text3, Global.ExternalImagesTimeout, num, abortHelper, out mimeType, out registerInvalidSizeWarning);
					}
					catch (WebException ex2)
					{
						string text4 = string.Format(CultureInfo.CurrentCulture, "An error occurred retrieving the external resource '{0}' : {1}", imageUrl, ex2.Message);
						RSTrace.ProcessingTracer.Trace(TraceLevel.Warning, text4);
						if (alwaysThrowOnWebException || text != null)
						{
							throw;
						}
						registerExternalWarning = true;
					}
				}
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001B114 File Offset: 0x00019314
		public ReportSnapshot AllocateNewReportSnapshot(bool isPermanentSnapshot, ParameterInfoCollection parameters, DateTime createdDate, string description, ReportProcessingFlags processingFlags)
		{
			ReportSnapshot reportSnapshot = ReportSnapshot.Create(isPermanentSnapshot, processingFlags);
			reportSnapshot.WriteNewSnapshotToDB(parameters, createdDate, description);
			return reportSnapshot;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001B128 File Offset: 0x00019328
		public ModelSnapshot AllocateNewModelSnapshot(DateTime createdDate, string description)
		{
			ModelSnapshot modelSnapshot = ModelSnapshot.Create();
			modelSnapshot.WriteNewSnapshotToDB(null, createdDate, description);
			return modelSnapshot;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001B138 File Offset: 0x00019338
		public CatalogItemContext ConstructItemContext(string path, bool allowEditSession)
		{
			return this.ConstructItemContext(path, allowEditSession, null);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001B144 File Offset: 0x00019344
		public CatalogItemContext ConstructItemContext(string path, bool allowEditSession, string parameterName)
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(this);
			ItemPathOptions itemPathOptions = ItemPathOptions.Default;
			if (allowEditSession)
			{
				itemPathOptions |= ItemPathOptions.AllowEditSessionSyntax;
			}
			if (!catalogItemContext.SetPath(path, itemPathOptions))
			{
				throw new InvalidItemPathException(path, parameterName);
			}
			return catalogItemContext;
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001B172 File Offset: 0x00019372
		public GetSystemPropertiesAction GetSystemPropertiesAction
		{
			get
			{
				if (this.m_getSystemPropertiesAction == null)
				{
					this.m_getSystemPropertiesAction = new GetSystemPropertiesAction(this);
				}
				return this.m_getSystemPropertiesAction;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0001B18E File Offset: 0x0001938E
		public GetReportServerConfigInfoAction GetReportServerConfigInfoAction
		{
			get
			{
				if (this.m_getReportServerConfigInfoAction == null)
				{
					this.m_getReportServerConfigInfoAction = new GetReportServerConfigInfoAction(this);
				}
				return this.m_getReportServerConfigInfoAction;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001B1AA File Offset: 0x000193AA
		public SetSystemPropertiesAction SetSystemPropertiesAction
		{
			get
			{
				if (this.m_setSystemPropertiesAction == null)
				{
					this.m_setSystemPropertiesAction = new SetSystemPropertiesAction(this);
				}
				return this.m_setSystemPropertiesAction;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0001B1C6 File Offset: 0x000193C6
		public GetUserSettingsAction GetUserSettingsAction
		{
			get
			{
				if (this.m_getUserSettingsAction == null)
				{
					this.m_getUserSettingsAction = new GetUserSettingsAction(this);
				}
				return this.m_getUserSettingsAction;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001B1E2 File Offset: 0x000193E2
		public SetUserSettingsAction SetUserSettingsAction
		{
			get
			{
				if (this.m_setUserSettingsAction == null)
				{
					this.m_setUserSettingsAction = new SetUserSettingsAction(this);
				}
				return this.m_setUserSettingsAction;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001B1FE File Offset: 0x000193FE
		public GetItemTypeAction GetItemTypeAction
		{
			get
			{
				if (this.m_getItemTypeAction == null)
				{
					this.m_getItemTypeAction = new GetItemTypeAction(this);
				}
				return this.m_getItemTypeAction;
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001B21C File Offset: 0x0001941C
		public ExternalItemPath GetItemLink(ExternalItemPath internalItemPath)
		{
			ExternalItemPath externalItemPath = null;
			if (this.m_willDisconnectStorage)
			{
				externalItemPath = this.InternalGetItemLink(internalItemPath);
			}
			else
			{
				this.WillDisconnectStorage();
				try
				{
					externalItemPath = this.InternalGetItemLink(internalItemPath);
				}
				catch (Exception)
				{
					this.AbortTransaction();
				}
				finally
				{
					this.DisconnectStorage();
				}
			}
			return externalItemPath;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001B27C File Offset: 0x0001947C
		private ExternalItemPath InternalGetItemLink(ExternalItemPath internalItemPath)
		{
			CatalogItemPath catalogItemPath = null;
			ItemType itemType;
			Guid guid;
			int num;
			byte[] array;
			int num2;
			Guid guid2;
			Guid guid3;
			this.Storage.ObjectExists(internalItemPath, out itemType, out guid, out num, out array, out num2, out guid2, out guid3);
			if (itemType == ItemType.LinkedReport)
			{
				catalogItemPath = this.Storage.GetPathById(guid3);
			}
			return this.CatalogToExternal(catalogItemPath);
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001B2BF File Offset: 0x000194BF
		public DeleteItemAction DeleteItemAction
		{
			get
			{
				if (this.m_deleteItemAction == null)
				{
					this.m_deleteItemAction = new DeleteItemAction(this);
				}
				return this.m_deleteItemAction;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001B2DB File Offset: 0x000194DB
		public MoveItemAction MoveItemAction
		{
			get
			{
				if (this.m_moveItemAction == null)
				{
					this.m_moveItemAction = this.ServiceHelper.GetMoveItemActionInternal();
				}
				return this.m_moveItemAction;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0001B2FC File Offset: 0x000194FC
		public GetPropertiesAction GetPropertiesAction
		{
			get
			{
				if (this.m_getPropertiesAction == null)
				{
					this.m_getPropertiesAction = new GetPropertiesAction(this);
				}
				return this.m_getPropertiesAction;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001B318 File Offset: 0x00019518
		public SetPropertiesAction SetPropertiesAction
		{
			get
			{
				if (this.m_setPropertiesAction == null)
				{
					this.m_setPropertiesAction = new SetPropertiesAction(this);
				}
				return this.m_setPropertiesAction;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0001B334 File Offset: 0x00019534
		public CreateReportEditSessionAction CreateEditSessionAction
		{
			get
			{
				if (this.m_createEditSession == null)
				{
					this.m_createEditSession = new CreateReportEditSessionAction(this);
				}
				return this.m_createEditSession;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0001B350 File Offset: 0x00019550
		public CreateComponentAction CreateComponentAction
		{
			get
			{
				if (this.m_createComponentAction == null)
				{
					this.m_createComponentAction = new CreateComponentAction(this);
				}
				return this.m_createComponentAction;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001B36C File Offset: 0x0001956C
		public GetComponentDefinitionAction GetComponentDefinitionAction
		{
			get
			{
				if (this.m_getComponentDefinitionAction == null)
				{
					this.m_getComponentDefinitionAction = new GetComponentDefinitionAction(this);
				}
				return this.m_getComponentDefinitionAction;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001B388 File Offset: 0x00019588
		public SetComponentDefinitionAction SetComponentDefinitionAction
		{
			get
			{
				if (this.m_setComponentDefinitionAction == null)
				{
					this.m_setComponentDefinitionAction = new SetComponentDefinitionAction(this);
				}
				return this.m_setComponentDefinitionAction;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0001B3A4 File Offset: 0x000195A4
		public ListChildrenAction ListChildrenAction
		{
			get
			{
				if (this.m_listChildrenAction == null)
				{
					this.m_listChildrenAction = new ListChildrenAction(this);
				}
				return this.m_listChildrenAction;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0001B3C0 File Offset: 0x000195C0
		public ListFavoriteableItemsAction ListFavoriteableItemsAction
		{
			get
			{
				if (this.m_listFavoriteableItemsAction == null)
				{
					this.m_listFavoriteableItemsAction = new ListFavoriteableItemsAction(this);
				}
				return this.m_listFavoriteableItemsAction;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0001B3DC File Offset: 0x000195DC
		public ListParentsAction ListParentsAction
		{
			get
			{
				if (this.m_listParentsAction == null)
				{
					this.m_listParentsAction = new ListParentsAction(this);
				}
				return this.m_listParentsAction;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001B3F8 File Offset: 0x000195F8
		public AddToFavoritesAction AddToFavoritesAction
		{
			get
			{
				if (this.m_addToFavoritesAction == null)
				{
					this.m_addToFavoritesAction = new AddToFavoritesAction(this);
				}
				return this.m_addToFavoritesAction;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001B414 File Offset: 0x00019614
		public RemoveFromFavoritesAction RemoveFromFavoritesAction
		{
			get
			{
				if (this.m_removeFromFavoritesAction == null)
				{
					this.m_removeFromFavoritesAction = new RemoveFromFavoritesAction(this);
				}
				return this.m_removeFromFavoritesAction;
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001B430 File Offset: 0x00019630
		public CatalogItemList FindItems(string folder, BooleanOperatorEnum operation, Property[] options, Microsoft.ReportingServices.Library.Soap2010.SearchCondition[] properties, ServerCompatLevel compatLevel)
		{
			CatalogItemList catalogItemList2;
			try
			{
				this.WillDisconnectStorage();
				this.SetDatabaseConnectionSettings(ConnectionTransactionType.AutoCommit, ConnectionManager.DefaultIsolationLevel);
				CatalogItemContext catalogItemContext = new CatalogItemContext(this, folder, "folder");
				ItemType itemType;
				byte[] array;
				if (!this.Storage.ObjectExists(catalogItemContext.ItemPath, out itemType, out array))
				{
					throw new ItemNotFoundException(folder);
				}
				RSService.EnsureItemType(itemType, folder, new ItemType[]
				{
					ItemType.Folder,
					ItemType.Site
				});
				ItemSearchOptions itemSearchOptions = new ItemSearchOptions(options);
				itemSearchOptions.CompatLevel = compatLevel;
				if (!itemSearchOptions.ComponentLookup && !this.SecMgr.CheckAccess(itemType, array, CommonOperation.ReadProperties, catalogItemContext.ItemPath))
				{
					throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
				}
				ItemSearchConditions itemSearchConditions = new ItemSearchConditions(properties, itemSearchOptions);
				if (itemSearchOptions.ComponentLookup)
				{
					itemSearchConditions.ThrowIfNotValidComponentLookup(folder, operation);
				}
				CatalogItemList catalogItemList;
				this.Storage.FindObjectsGeneral(catalogItemContext.ItemPath, operation, itemSearchOptions, itemSearchConditions, out catalogItemList, this.SecMgr, this);
				catalogItemList2 = catalogItemList;
			}
			catch (Exception ex)
			{
				this.AbortTransaction();
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				this.DisconnectStorage();
			}
			return catalogItemList2;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001B548 File Offset: 0x00019748
		public CatalogItemList FindItems(string folder, string searchText)
		{
			Microsoft.ReportingServices.Library.Soap2010.SearchCondition[] array = new Microsoft.ReportingServices.Library.Soap2010.SearchCondition[]
			{
				new Microsoft.ReportingServices.Library.Soap2010.SearchCondition()
			};
			array[0].ConditionSpecified = true;
			array[0].Condition = Microsoft.ReportingServices.Library.Soap2010.ConditionEnum.Contains;
			array[0].Name = "All";
			array[0].Values = new List<string>(new string[] { searchText });
			return this.FindItems(folder, BooleanOperatorEnum.And, null, array, ServerCompatLevel.Soap2010);
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001B5A5 File Offset: 0x000197A5
		public CreateFolderAction CreateFolderAction
		{
			get
			{
				if (this.m_createFolderAction == null)
				{
					this.m_createFolderAction = new CreateFolderAction(this);
				}
				return this.m_createFolderAction;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001B5C1 File Offset: 0x000197C1
		public CreateDataSetAction CreateDataSetAction
		{
			get
			{
				if (this.m_createDataSetAction == null)
				{
					this.m_createDataSetAction = new CreateDataSetAction(this);
				}
				return this.m_createDataSetAction;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001B5DD File Offset: 0x000197DD
		public GetDataSetDefinitionAction GetDataSetDefinitionAction
		{
			get
			{
				if (this.m_getDataSetDefinitionAction == null)
				{
					this.m_getDataSetDefinitionAction = new GetDataSetDefinitionAction(this);
				}
				return this.m_getDataSetDefinitionAction;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001B5F9 File Offset: 0x000197F9
		public SetDataSetDefinitionAction SetDataSetDefinitionAction
		{
			get
			{
				if (this.m_setDataSetDefinitionAction == null)
				{
					this.m_setDataSetDefinitionAction = new SetDataSetDefinitionAction(this);
				}
				return this.m_setDataSetDefinitionAction;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0001B615 File Offset: 0x00019815
		public GetDataSetItemReferencesAction GetDataSetItemReferencesAction
		{
			get
			{
				if (this.m_getDataSetItemReferencesAction == null)
				{
					this.m_getDataSetItemReferencesAction = new GetDataSetItemReferencesAction(this);
				}
				return this.m_getDataSetItemReferencesAction;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0001B631 File Offset: 0x00019831
		public SetDataSetItemReferencesAction SetDataSetItemReferencesAction
		{
			get
			{
				if (this.m_setDataSetItemReferencesAction == null)
				{
					this.m_setDataSetItemReferencesAction = new SetDataSetItemReferencesAction(this);
				}
				return this.m_setDataSetItemReferencesAction;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0001B64D File Offset: 0x0001984D
		public GetDataSetParametersAction GetDataSetParametersAction
		{
			get
			{
				if (this.m_getDataSetParametersAction == null)
				{
					this.m_getDataSetParametersAction = new GetDataSetParametersAction(this);
				}
				return this.m_getDataSetParametersAction;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001B669 File Offset: 0x00019869
		public CreateReportAction CreateReportAction
		{
			get
			{
				if (this.m_createReportAction == null)
				{
					this.m_createReportAction = new CreateReportAction(this);
				}
				return this.m_createReportAction;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0001B685 File Offset: 0x00019885
		public CreateRdlxReportAction CreateRdlxReportAction
		{
			get
			{
				if (this.m_createRdlxReportAction == null)
				{
					this.m_createRdlxReportAction = new CreateRdlxReportAction(this);
				}
				return this.m_createRdlxReportAction;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0001B6A1 File Offset: 0x000198A1
		public SetReportDefinitionAction SetReportDefinitionAction
		{
			get
			{
				if (this.m_setReportDefinitionAction == null)
				{
					this.m_setReportDefinitionAction = new SetReportDefinitionAction(this);
				}
				return this.m_setReportDefinitionAction;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001B6BD File Offset: 0x000198BD
		public SetRdlxReportDefinitionAction SetRdlxReportDefinitionAction
		{
			get
			{
				if (this.m_setRdlxReportDefinitionAction == null)
				{
					this.m_setRdlxReportDefinitionAction = new SetRdlxReportDefinitionAction(this);
				}
				return this.m_setRdlxReportDefinitionAction;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001B6D9 File Offset: 0x000198D9
		public GetReportDefinitionAction GetReportDefinitionAction
		{
			get
			{
				if (this.m_getReportDefinitionAction == null)
				{
					this.m_getReportDefinitionAction = new GetReportDefinitionAction(this);
				}
				return this.m_getReportDefinitionAction;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0001B6F5 File Offset: 0x000198F5
		public SetReportParametersAction SetReportParametersAction
		{
			get
			{
				if (this.m_setReportParametersAction == null)
				{
					this.m_setReportParametersAction = new SetReportParametersAction(this);
				}
				return this.m_setReportParametersAction;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001B711 File Offset: 0x00019911
		public GetReportParametersAction GetReportParametersAction
		{
			get
			{
				if (this.m_getReportParametersAction == null)
				{
					this.m_getReportParametersAction = new GetReportParametersAction(this);
				}
				return this.m_getReportParametersAction;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0001B72D File Offset: 0x0001992D
		public SetExecutionOptionsAction SetExecutionOptionsAction
		{
			get
			{
				if (this.m_setExeuctionOptionsAction == null)
				{
					this.m_setExeuctionOptionsAction = new SetExecutionOptionsAction(this);
				}
				return this.m_setExeuctionOptionsAction;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0001B749 File Offset: 0x00019949
		public GetExecutionOptionsAction GetExecutionOptionsAction
		{
			get
			{
				if (this.m_getExeuctionOptionsAction == null)
				{
					this.m_getExeuctionOptionsAction = new GetExecutionOptionsAction(this);
				}
				return this.m_getExeuctionOptionsAction;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001B765 File Offset: 0x00019965
		public SetCacheOptionsAction SetCacheOptionsAction
		{
			get
			{
				if (this.m_setCacheOptionsAction == null)
				{
					this.m_setCacheOptionsAction = new SetCacheOptionsAction(this);
				}
				return this.m_setCacheOptionsAction;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001B781 File Offset: 0x00019981
		public GetCacheOptionsAction GetCacheOptionsAction
		{
			get
			{
				if (this.m_getCacheOptionsAction == null)
				{
					this.m_getCacheOptionsAction = new GetCacheOptionsAction(this);
				}
				return this.m_getCacheOptionsAction;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0001B79D File Offset: 0x0001999D
		public UpdateExecutionSnapshotAction UpdateExecutionSnapshotAction
		{
			get
			{
				if (this.m_updateSnapshotAction == null)
				{
					this.m_updateSnapshotAction = new UpdateExecutionSnapshotAction(this);
				}
				return this.m_updateSnapshotAction;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0001B7B9 File Offset: 0x000199B9
		public FlushCacheAction FlushCacheAction
		{
			get
			{
				if (this.m_flushCacheAction == null)
				{
					this.m_flushCacheAction = new FlushCacheAction(this);
				}
				return this.m_flushCacheAction;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0001B7D5 File Offset: 0x000199D5
		public CreateSnapshotAction CreateSnapshotAction
		{
			get
			{
				if (this.m_createSnapshotAction == null)
				{
					this.m_createSnapshotAction = new CreateSnapshotAction(this);
				}
				return this.m_createSnapshotAction;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0001B7F1 File Offset: 0x000199F1
		public SetReportHistoryOptionsAction SetReportHistoryOptionsAction
		{
			get
			{
				if (this.m_setHistoryOptionsAction == null)
				{
					this.m_setHistoryOptionsAction = new SetReportHistoryOptionsAction(this);
				}
				return this.m_setHistoryOptionsAction;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0001B80D File Offset: 0x00019A0D
		public GetReportHistoryOptionsAction GetReportHistoryOptionsAction
		{
			get
			{
				if (this.m_getHistoryOptionsAction == null)
				{
					this.m_getHistoryOptionsAction = new GetReportHistoryOptionsAction(this);
				}
				return this.m_getHistoryOptionsAction;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001B829 File Offset: 0x00019A29
		public SetSnapshotLimitAction SetSnapshotLimitAction
		{
			get
			{
				if (this.m_setSnapshotLimitAction == null)
				{
					this.m_setSnapshotLimitAction = new SetSnapshotLimitAction(this);
				}
				return this.m_setSnapshotLimitAction;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001B845 File Offset: 0x00019A45
		public GetSnapshotLimitAction GetSnapshotLimitAction
		{
			get
			{
				if (this.m_getSnapshotLimitAction == null)
				{
					this.m_getSnapshotLimitAction = new GetSnapshotLimitAction(this);
				}
				return this.m_getSnapshotLimitAction;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001B861 File Offset: 0x00019A61
		public ListHistoryAction ListHistoryAction
		{
			get
			{
				if (this.m_listHistoryAction == null)
				{
					this.m_listHistoryAction = new ListHistoryAction(this);
				}
				return this.m_listHistoryAction;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0001B87D File Offset: 0x00019A7D
		public ListHistorySnapshotsAction ListHistorySnapshotsAction
		{
			get
			{
				if (this.m_listHistorySnapshotsAction == null)
				{
					this.m_listHistorySnapshotsAction = new ListHistorySnapshotsAction(this);
				}
				return this.m_listHistorySnapshotsAction;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001B899 File Offset: 0x00019A99
		public DeleteSnapshotAction DeleteSnapshotAction
		{
			get
			{
				if (this.m_deleteSnapshotAction == null)
				{
					this.m_deleteSnapshotAction = new DeleteSnapshotAction(this);
				}
				return this.m_deleteSnapshotAction;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0001B8B5 File Offset: 0x00019AB5
		public DeleteHistorySnapshotAction DeleteHistorySnapshotAction
		{
			get
			{
				if (this.m_deleteHistorySnapshotAction == null)
				{
					this.m_deleteHistorySnapshotAction = new DeleteHistorySnapshotAction(this);
				}
				return this.m_deleteHistorySnapshotAction;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001B8D1 File Offset: 0x00019AD1
		public CreateSubscriptionAction CreateSubscriptionAction
		{
			get
			{
				if (this.m_createSubscriptionAction == null)
				{
					this.m_createSubscriptionAction = new CreateSubscriptionAction(this);
				}
				return this.m_createSubscriptionAction;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0001B8ED File Offset: 0x00019AED
		public DisableSubscriptionAction DisableSubscriptionAction
		{
			get
			{
				if (this.m_disableSubscriptionAction == null)
				{
					this.m_disableSubscriptionAction = new DisableSubscriptionAction(this);
				}
				return this.m_disableSubscriptionAction;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001B909 File Offset: 0x00019B09
		public EnableSubscriptionAction EnableSubscriptionAction
		{
			get
			{
				if (this.m_enableSubscriptionAction == null)
				{
					this.m_enableSubscriptionAction = new EnableSubscriptionAction(this);
				}
				return this.m_enableSubscriptionAction;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0001B925 File Offset: 0x00019B25
		public DeleteSubscriptionAction DeleteSubscriptionAction
		{
			get
			{
				if (this.m_deleteSubscriptionAction == null)
				{
					this.m_deleteSubscriptionAction = new DeleteSubscriptionAction(this);
				}
				return this.m_deleteSubscriptionAction;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001B941 File Offset: 0x00019B41
		public SetSubscriptionPropertiesAction SetSubscriptionPropertiesAction
		{
			get
			{
				if (this.m_setSubscriptionPropertiesAction == null)
				{
					this.m_setSubscriptionPropertiesAction = new SetSubscriptionPropertiesAction(this);
				}
				return this.m_setSubscriptionPropertiesAction;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001B95D File Offset: 0x00019B5D
		public GetSubscriptionPropertiesAction GetSubscriptionPropertiesAction
		{
			get
			{
				if (this.m_getSubscriptionPropertiesAction == null)
				{
					this.m_getSubscriptionPropertiesAction = new GetSubscriptionPropertiesAction(this);
				}
				return this.m_getSubscriptionPropertiesAction;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001B979 File Offset: 0x00019B79
		public ListSubscriptionsAction ListSubscriptionsAction
		{
			get
			{
				if (this.m_listSubscriptionsAction == null)
				{
					this.m_listSubscriptionsAction = new ListSubscriptionsAction(this);
				}
				return this.m_listSubscriptionsAction;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0001B995 File Offset: 0x00019B95
		public ChangeSubscriptionOwnerAction ChangeSubscriptionOwnerAction
		{
			get
			{
				if (this.m_changeSubscriptionOwnerAction == null)
				{
					this.m_changeSubscriptionOwnerAction = new ChangeSubscriptionOwnerAction(this);
				}
				return this.m_changeSubscriptionOwnerAction;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0001B9B1 File Offset: 0x00019BB1
		public CreateCacheRefreshPlanAction CreateCacheRefreshPlanAction
		{
			get
			{
				if (this.m_createCacheRefreshPlanAction == null)
				{
					this.m_createCacheRefreshPlanAction = new CreateCacheRefreshPlanAction(this);
				}
				return this.m_createCacheRefreshPlanAction;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0001B9CD File Offset: 0x00019BCD
		public SetCacheRefreshPlanPropertiesAction SetCacheRefreshPlanPropertiesAction
		{
			get
			{
				if (this.m_setCacheRefreshPlanPropertiesAction == null)
				{
					this.m_setCacheRefreshPlanPropertiesAction = new SetCacheRefreshPlanPropertiesAction(this);
				}
				return this.m_setCacheRefreshPlanPropertiesAction;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001B9E9 File Offset: 0x00019BE9
		public GetCacheRefreshPlanPropertiesAction GetCacheRefreshPlanPropertiesAction
		{
			get
			{
				if (this.m_getCacheRefreshPlanPropertiesAction == null)
				{
					this.m_getCacheRefreshPlanPropertiesAction = new GetCacheRefreshPlanPropertiesAction(this);
				}
				return this.m_getCacheRefreshPlanPropertiesAction;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0001BA05 File Offset: 0x00019C05
		public CreateScheduleAction CreateScheduleAction
		{
			get
			{
				if (this.m_createScheduleAction == null)
				{
					this.m_createScheduleAction = new CreateScheduleAction(this);
				}
				return this.m_createScheduleAction;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001BA21 File Offset: 0x00019C21
		public DeleteScheduleAction DeleteScheduleAction
		{
			get
			{
				if (this.m_deleteScheduleAction == null)
				{
					this.m_deleteScheduleAction = new DeleteScheduleAction(this);
				}
				return this.m_deleteScheduleAction;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0001BA3D File Offset: 0x00019C3D
		public ListSchedulesAction ListSchedulesAction
		{
			get
			{
				if (this.m_listSchedulesAction == null)
				{
					this.m_listSchedulesAction = new ListSchedulesAction(this);
				}
				return this.m_listSchedulesAction;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0001BA59 File Offset: 0x00019C59
		public GetSchedulePropertiesAction GetSchedulePropertiesAction
		{
			get
			{
				if (this.m_getSchedulePropertiesAction == null)
				{
					this.m_getSchedulePropertiesAction = new GetSchedulePropertiesAction(this);
				}
				return this.m_getSchedulePropertiesAction;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0001BA75 File Offset: 0x00019C75
		public SetSchedulePropertiesAction SetSchedulePropertiesAction
		{
			get
			{
				if (this.m_setSchedulePropertiesAction == null)
				{
					this.m_setSchedulePropertiesAction = new SetSchedulePropertiesAction(this);
				}
				return this.m_setSchedulePropertiesAction;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001BA91 File Offset: 0x00019C91
		public PauseScheduleAction PauseScheduleAction
		{
			get
			{
				if (this.m_pauseScheduleAction == null)
				{
					this.m_pauseScheduleAction = new PauseScheduleAction(this);
				}
				return this.m_pauseScheduleAction;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0001BAAD File Offset: 0x00019CAD
		public ResumeScheduleAction ResumeScheduleAction
		{
			get
			{
				if (this.m_resumeScheduleAction == null)
				{
					this.m_resumeScheduleAction = new ResumeScheduleAction(this);
				}
				return this.m_resumeScheduleAction;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001BAC9 File Offset: 0x00019CC9
		public ListScheduledReportsAction ListScheduledReportsAction
		{
			get
			{
				if (this.m_listScheduledReportsAction == null)
				{
					this.m_listScheduledReportsAction = new ListScheduledReportsAction(this);
				}
				return this.m_listScheduledReportsAction;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001BAE5 File Offset: 0x00019CE5
		public GetReportItemReferencesAction GetReportItemReferencesAction
		{
			get
			{
				if (this.m_getReportItemReferencesAction == null)
				{
					this.m_getReportItemReferencesAction = new GetReportItemReferencesAction(this);
				}
				return this.m_getReportItemReferencesAction;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001BB01 File Offset: 0x00019D01
		public SetReportItemReferencesAction SetReportItemReferencesAction
		{
			get
			{
				if (this.m_setReportItemReferencesAction == null)
				{
					this.m_setReportItemReferencesAction = new SetReportItemReferencesAction(this);
				}
				return this.m_setReportItemReferencesAction;
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001BB20 File Offset: 0x00019D20
		public ParameterInfoCollection GetReportParameterDefinitions(ClientRequest session, CatalogItemContext reportContext, bool forRendering)
		{
			this.Tracer.Trace(TraceLevel.Verbose, "Call to GetReportParameterDefinitions( '{0}' )", new object[] { reportContext.ItemPath });
			ParameterInfoCollection parameterInfoCollection;
			if (session != null && session.SessionReport.IsAdhocReport)
			{
				parameterInfoCollection = this.GetReportParametersForRendering(reportContext, Guid.Empty, Guid.Empty, session.SessionReport.ExecutionDateTime, new AdHocSessionParameterStorage(session.SessionReport), reportContext.RSRequestParameters.ReportParameters, session.SessionReport.Datasources, session.SessionReport.DataSets, JobType.UserJobType);
			}
			else
			{
				ItemParameterDefinition itemParameterDefinition = ItemParameterDefinition.Load(reportContext, reportContext.RSRequestParameters.SnapshotParamValue, forRendering, this, SecurityRequirements.GenerateForLoadCompiledDefinition(this.SecMgr, this.UserName));
				if (!forRendering)
				{
					ParameterInfoCollection parameterInfoCollection2 = ParameterInfoCollection.DecodeFromXml(itemParameterDefinition.StoredParametersXml);
					parameterInfoCollection2.ValuesAreValid();
					return parameterInfoCollection2;
				}
				RuntimeDataSourceInfoCollection runtimeDataSourceInfoCollection = null;
				RuntimeDataSetInfoCollection runtimeDataSetInfoCollection = null;
				if (session != null)
				{
					runtimeDataSourceInfoCollection = session.SessionReport.Datasources;
				}
				IStoredParameterSource storedParameterSource = new CatalogSessionParameterStorage((session == null) ? null : session.SessionReport, itemParameterDefinition);
				parameterInfoCollection = this.GetReportParametersForRendering(reportContext, itemParameterDefinition.ReportId, itemParameterDefinition.LinkId, itemParameterDefinition.SnapshotExecutionDate, storedParameterSource, reportContext.RSRequestParameters.ReportParameters, runtimeDataSourceInfoCollection, runtimeDataSetInfoCollection, JobType.UserJobType);
			}
			this.Tracer.Trace(TraceLevel.Verbose, "Call to GetReportParameterDefinitions completed.");
			return parameterInfoCollection;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001BC54 File Offset: 0x00019E54
		public ParameterInfoCollection GetReportParametersForRendering(CatalogItemContext reportContext, Guid reportID, Guid linkID, DateTime snapshotExecutionDate, IStoredParameterSource storedParamSource, NameValueCollection values, RuntimeDataSourceInfoCollection allDataSources, RuntimeDataSetInfoCollection allDataSets, JobType jobType)
		{
			RSTrace.CatalogTrace.Assert(this.StreamManager != null, "StreamManager");
			if (Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext != null)
			{
				return this.InternalGetReportParametersForRendering(reportContext, reportID, linkID, snapshotExecutionDate, storedParamSource, values, allDataSources, allDataSets);
			}
			ParameterInfoCollection resultParameters;
			using (ProcessReportParametersCancelableStep processReportParametersCancelableStep = new ProcessReportParametersCancelableStep(this, reportContext, reportID, linkID, snapshotExecutionDate, storedParamSource, values, allDataSources, allDataSets, jobType))
			{
				processReportParametersCancelableStep.ExecuteWrapper();
				resultParameters = processReportParametersCancelableStep.ResultParameters;
			}
			return resultParameters;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001BCD4 File Offset: 0x00019ED4
		public ParameterInfoCollection InternalGetReportParametersForRendering(CatalogItemContext reportContext, Guid reportID, Guid linkID, DateTime snapshotExecutionDate, IStoredParameterSource storedParamSource, NameValueCollection values, RuntimeDataSourceInfoCollection allDataSources, RuntimeDataSetInfoCollection allDataSets)
		{
			bool isSnapshotExecution = storedParamSource.IsSnapshotExecution;
			DateTime dateTime = (isSnapshotExecution ? snapshotExecutionDate : DateTime.Now);
			ReportProcessing processingEngine = Global.GetProcessingEngine();
			ParameterInfoCollection parameterInfoCollection = storedParamSource.RetrieveParameters(processingEngine);
			ParameterInfoCollection parameterInfoCollection2 = ParameterInfoCollection.DecodeFromNameValueCollectionAndUserCulture(values);
			ParameterInfoCollection parameterInfoCollection3 = ParameterInfoCollection.Combine(parameterInfoCollection, parameterInfoCollection2, true, isSnapshotExecution, false, false, Localization.ReportParameterCulture);
			if (parameterInfoCollection3.Count == 0)
			{
				return parameterInfoCollection3;
			}
			ISnapshotTransaction snapshotTransaction = null;
			IChunkFactory chunkFactory = null;
			if (parameterInfoCollection3.IsAnyParameterDynamic)
			{
				if (!isSnapshotExecution && allDataSources == null)
				{
					this.GetAllDataSources(processingEngine, reportContext, storedParamSource.CompiledParameterSource, out allDataSources, out allDataSets);
					allDataSources.SetCredentials(reportContext.RSRequestParameters.DatasourcesCred, DataProtection.Instance);
				}
				snapshotTransaction = storedParamSource.CompiledParameterSource.EnterTransactionContext();
				chunkFactory = storedParamSource.CompiledParameterSource;
			}
			using (snapshotTransaction)
			{
				this.ProcessReportParameters(processingEngine, reportContext, chunkFactory, this.HowToCreateDataExtensionInstance, allDataSources, allDataSets, parameterInfoCollection3, dateTime, isSnapshotExecution);
				if (snapshotTransaction != null)
				{
					snapshotTransaction.Commit();
				}
			}
			bool flag;
			parameterInfoCollection3.ValuesAreValid(out flag, true);
			return parameterInfoCollection3;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001BDD0 File Offset: 0x00019FD0
		public ParameterInfoCollection GetDataSetParameters(ItemParameterDefinition parameterDefinition, NameValueCollection requestParameterValues, JobType jobType)
		{
			RSTrace.CatalogTrace.Assert(this.StreamManager != null, "StreamManager");
			RSTrace.CatalogTrace.Assert(parameterDefinition != null, "parameterDefinition");
			ReportProcessing processingEngine = Global.GetProcessingEngine();
			if (Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext != null)
			{
				return this.InternalGetDataSetParameters(processingEngine, parameterDefinition, requestParameterValues);
			}
			ParameterInfoCollection resultParameters;
			using (ProcessDataSetParametersCancelableStep processDataSetParametersCancelableStep = new ProcessDataSetParametersCancelableStep(processingEngine, this, parameterDefinition, requestParameterValues, jobType))
			{
				processDataSetParametersCancelableStep.ExecuteWrapper();
				resultParameters = processDataSetParametersCancelableStep.ResultParameters;
			}
			return resultParameters;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001BE54 File Offset: 0x0001A054
		public ParameterInfoCollection InternalGetDataSetParameters(ReportProcessing repProc, ItemParameterDefinition parameterDefinition, NameValueCollection requestParameters)
		{
			if (parameterDefinition.Type != ItemType.DataSet)
			{
				throw new WrongItemTypeException(parameterDefinition.ItemContext.OriginalItemPath.Value);
			}
			ParameterInfoCollection parameterInfoCollection = ParameterInfoCollection.DecodeFromNameValueCollectionAndUserCulture(requestParameters, true);
			ParameterInfoCollection parameterInfoCollection2 = ParameterInfoCollection.Combine(ParameterInfoCollection.DecodeFromXml(parameterDefinition.StoredParametersXml), parameterInfoCollection, true, false, false, true, Localization.ReportParameterCulture);
			if (parameterInfoCollection2.Count == 0)
			{
				return parameterInfoCollection2;
			}
			ReportProcessing.ExecutionType executionType;
			using (SurrogateContextFactory.CreateContext(out executionType))
			{
				using (parameterDefinition.DefinitionSnapshot.EnterTransactionContext())
				{
					Microsoft.ReportingServices.DataExtensions.DataSetDefinition dataSetDefinition = new Microsoft.ReportingServices.DataExtensions.DataSetDefinition(ReadOnlyChunkFactory.FromSnapshot(parameterDefinition.DefinitionSnapshot));
					DataSetContext dataSetContext = new DataSetContext(string.Empty, string.Empty, false, null, parameterDefinition.ItemContext, null, this.UserName, DateTime.Now, parameterInfoCollection2, null, executionType, Localization.ClientPrimaryCulture, this.GetUserProfileState(Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext.JobType), UserProfileState.None, new ServerDataExtensionConnection(this.HowToCreateDataExtensionInstance, this.UserContext, executionType, new ServerAdditionalToken(this, parameterDefinition.ItemContext)), new CreateAndRegisterStream(this.StreamManager.GetNewStream), ReportRuntimeSetup.GetDefault(), ServerJobContext.ConstructJobContext(Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext), DataProtection.Instance);
					repProc.ProcessDataSetParameters(dataSetContext, dataSetDefinition);
				}
			}
			bool flag;
			parameterInfoCollection2.ValuesAreValid(out flag, true);
			return parameterInfoCollection2;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001BFA8 File Offset: 0x0001A1A8
		public CreateLinkedReportAction CreateLinkedReportAction
		{
			get
			{
				if (this.m_createLinkedReportAction == null)
				{
					this.m_createLinkedReportAction = new CreateLinkedReportAction(this);
				}
				return this.m_createLinkedReportAction;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001BFC4 File Offset: 0x0001A1C4
		public GetReportLinkAction GetReportLinkAction
		{
			get
			{
				if (this.m_getReportLinkAction == null)
				{
					this.m_getReportLinkAction = new GetReportLinkAction(this);
				}
				return this.m_getReportLinkAction;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001BFE0 File Offset: 0x0001A1E0
		public SetReportLinkAction SetReportLinkAction
		{
			get
			{
				if (this.m_setReportLinkAction == null)
				{
					this.m_setReportLinkAction = new SetReportLinkAction(this);
				}
				return this.m_setReportLinkAction;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001BFFC File Offset: 0x0001A1FC
		public CreateResourceAction CreateResourceAction
		{
			get
			{
				if (this.m_createResourceAction == null)
				{
					this.m_createResourceAction = new CreateResourceAction(this);
				}
				return this.m_createResourceAction;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001C018 File Offset: 0x0001A218
		public GetResourceContentsAction GetResourceContentsAction
		{
			get
			{
				if (this.m_getResourceContentsAction == null)
				{
					this.m_getResourceContentsAction = new GetResourceContentsAction(this);
				}
				return this.m_getResourceContentsAction;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001C034 File Offset: 0x0001A234
		public SetResourceContentsAction SetResourceContentsAction
		{
			get
			{
				if (this.m_setResourceContentsAction == null)
				{
					this.m_setResourceContentsAction = new SetResourceContentsAction(this);
				}
				return this.m_setResourceContentsAction;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001C050 File Offset: 0x0001A250
		public CreateKpiAction CreateKpiAction
		{
			get
			{
				if (this.m_createKpiAction == null)
				{
					this.m_createKpiAction = new CreateKpiAction(this);
				}
				return this.m_createKpiAction;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001C06C File Offset: 0x0001A26C
		public GetKpiAction GetKpiAction
		{
			get
			{
				if (this.m_getKpiAction == null)
				{
					this.m_getKpiAction = new GetKpiAction(this);
				}
				return this.m_getKpiAction;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001C088 File Offset: 0x0001A288
		public UploadPowerBIReportAction UploadPowerBiReportAction
		{
			get
			{
				if (this.m_UploadPowerBiReportAction == null)
				{
					this.m_UploadPowerBiReportAction = new UploadPowerBIReportAction(this);
				}
				return this.m_UploadPowerBiReportAction;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001C0A4 File Offset: 0x0001A2A4
		public GetPowerBIReportContentsAction GetPowerBIReportContentsAction
		{
			get
			{
				if (this.m_getPowerBIReportContentsAction == null)
				{
					this.m_getPowerBIReportContentsAction = new GetPowerBIReportContentsAction(this);
				}
				return this.m_getPowerBIReportContentsAction;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001C0C0 File Offset: 0x0001A2C0
		public SetPowerBIReportContentsAction SetPowerBIReportContentsAction
		{
			get
			{
				if (this.m_setPowerBIReportContentsAction == null)
				{
					this.m_setPowerBIReportContentsAction = new SetPowerBIReportContentsAction(this);
				}
				return this.m_setPowerBIReportContentsAction;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001C0DC File Offset: 0x0001A2DC
		public CreateExcelWorkbookAction CreateExcelAction
		{
			get
			{
				if (this.m_createExcelAction == null)
				{
					this.m_createExcelAction = new CreateExcelWorkbookAction(this);
				}
				return this.m_createExcelAction;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001C0F8 File Offset: 0x0001A2F8
		public GetExcelWorkbookContentsAction GetExcelWorkbookContentsAction
		{
			get
			{
				if (this.m_getExcelWorkbookContentsAction == null)
				{
					this.m_getExcelWorkbookContentsAction = new GetExcelWorkbookContentsAction(this);
				}
				return this.m_getExcelWorkbookContentsAction;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001C114 File Offset: 0x0001A314
		public SetExcelWorkbookContentsAction SetExcelWorkbookContentsAction
		{
			get
			{
				if (this.m_setExcelWorkbookContentsAction == null)
				{
					this.m_setExcelWorkbookContentsAction = new SetExcelWorkbookContentsAction(this);
				}
				return this.m_setExcelWorkbookContentsAction;
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001C130 File Offset: 0x0001A330
		public RSStream ExecuteQuery(ExternalItemPath modelName, string query, NameValueCollection parameters, int timeout, IDbConnectionPool connectionPool)
		{
			Microsoft.ReportingServices.DataProcessing.IDbConnection dbConnection = null;
			ConnectionContext connectionContext = null;
			RSStream rsstream2;
			try
			{
				this.WillDisconnectStorage();
				this.SetDatabaseConnectionSettings(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
				CatalogItemContext catalogItemContext = new CatalogItemContext(this, modelName, "modelName");
				Microsoft.ReportingServices.Library.CatalogItem catalogItem = this.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.Model, true);
				catalogItem.ThrowIfNoAccess(CommonOperation.ReadProperties);
				DataSourceInfo theOnlyDataSource = (catalogItem as ModelCatalogItem).DataSources.GetTheOnlyDataSource();
				theOnlyDataSource.ThrowIfNotUsable(new ServerDataSourceSettings(Globals.Configuration.IsSurrogatePresent, Global.EnableIntegratedSecurity));
				connectionContext = this.GetConnectionContext(theOnlyDataSource, this.UserName);
				if (connectionPool != null)
				{
					dbConnection = connectionPool.GetConnection(connectionContext.CreateConnectionKey());
				}
				if (dbConnection == null)
				{
					global::System.Data.IDbConnection dbConnection2;
					dbConnection = this.OpenDataSourceConnection(theOnlyDataSource, this.HowToCreateDataExtensionInstance, false, false, connectionContext, out dbConnection2);
				}
				Microsoft.ReportingServices.DataProcessing.IDataReader dataReader = null;
				using (Microsoft.ReportingServices.DataProcessing.IDbCommand dbCommand = dbConnection.CreateCommand())
				{
					dbCommand.CommandText = query;
					dbCommand.CommandType = Microsoft.ReportingServices.DataProcessing.CommandType.Text;
					dbCommand.CommandTimeout = timeout;
					if (parameters != null)
					{
						foreach (object obj in parameters)
						{
							string text = (string)obj;
							Microsoft.ReportingServices.DataProcessing.IDataParameter dataParameter = dbCommand.CreateParameter();
							dataParameter.ParameterName = text;
							string[] values = parameters.GetValues(text);
							if (values != null && values.Length > 1)
							{
								IDataMultiValueParameter dataMultiValueParameter = dataParameter as IDataMultiValueParameter;
								if (dataMultiValueParameter == null)
								{
									throw new InvalidParameterException(dataParameter.ParameterName);
								}
								IDataMultiValueParameter dataMultiValueParameter2 = dataMultiValueParameter;
								object[] array = values;
								dataMultiValueParameter2.Values = array;
							}
							else
							{
								dataParameter.Value = parameters[text];
							}
							dbCommand.Parameters.Add(dataParameter);
						}
					}
					if (Microsoft.ReportingServices.Diagnostics.ProcessingContext.ThreadContext != null)
					{
						Microsoft.ReportingServices.Diagnostics.ProcessingContext.ThreadContext.AddCommand(dbCommand);
					}
					dataReader = dbCommand.ExecuteReader(Microsoft.ReportingServices.DataProcessing.CommandBehavior.SingleResult);
				}
				RSStream rsstream;
				using (dataReader)
				{
					string text2;
					string text3;
					CatalogItemNameUtility.SplitPath(modelName.Value, out text2, out text3);
					rsstream = this.ProcessCommandResults(dataReader, text2);
				}
				rsstream2 = rsstream;
			}
			finally
			{
				if (dbConnection != null)
				{
					bool flag = true;
					if (connectionPool != null)
					{
						IDbPoolableConnection dbPoolableConnection = dbConnection as IDbPoolableConnection;
						if (dbPoolableConnection != null)
						{
							flag = !connectionPool.PoolConnection(dbPoolableConnection, connectionContext.CreateConnectionKey());
						}
					}
					if (flag)
					{
						dbConnection.Close();
						dbConnection.Dispose();
					}
				}
				this.DisconnectStorage();
			}
			return rsstream2;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001C3B0 File Offset: 0x0001A5B0
		private RSStream ProcessCommandResults(Microsoft.ReportingServices.DataProcessing.IDataReader iDataReader, string modelItemName)
		{
			IDataReaderExtension dataReaderExtension = iDataReader as IDataReaderExtension;
			DataTable resultSchema = this.GetResultSchema(iDataReader);
			Stream newStream = this.StreamManager.GetNewStream(modelItemName + "_result", "xml", Encoding.UTF8, "text/xml", false, StreamOper.CreateAndRegister);
			XmlWriter xmlWriter = new XmlTextWriter(new StreamWriter(newStream, Encoding.UTF8));
			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement("Data");
			resultSchema.WriteXmlSchema(xmlWriter);
			xmlWriter.WriteStartElement("DataSet");
			while (iDataReader.Read())
			{
				xmlWriter.WriteStartElement(XmlConvert.EncodeLocalName(resultSchema.TableName));
				int fieldCount = iDataReader.FieldCount;
				if (dataReaderExtension != null)
				{
					StringBuilder stringBuilder = new StringBuilder(dataReaderExtension.IsAggregateRow ? "aggrow" : "");
					for (int i = 0; i < fieldCount; i++)
					{
						if (dataReaderExtension != null && dataReaderExtension.IsAggregationField(i))
						{
							if (stringBuilder.Length > 0)
							{
								stringBuilder.Append(",");
							}
							stringBuilder.Append(i.ToString(CultureInfo.InvariantCulture));
						}
					}
					if (stringBuilder.Length > 0)
					{
						xmlWriter.WriteAttributeString(XmlConvert.EncodeLocalName(resultSchema.Columns[resultSchema.Columns.Count - 1].ColumnName), stringBuilder.ToString());
					}
				}
				for (int j = 0; j < fieldCount; j++)
				{
					this.WriteColumn(xmlWriter, resultSchema.Columns[j].ColumnName, iDataReader.GetValue(j));
				}
				xmlWriter.WriteEndElement();
			}
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndElement();
			xmlWriter.Flush();
			RSTrace.CatalogTrace.Assert(this.StreamManager.PrimaryStream == newStream, "StreamManager.PrimaryStream. == stream");
			return this.StreamManager.PrimaryStream;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001C566 File Offset: 0x0001A766
		private void WriteColumn(XmlWriter writer, string name, object value)
		{
			if (value != null && value != DBNull.Value)
			{
				writer.WriteStartElement(XmlConvert.EncodeLocalName(name));
				writer.WriteValue(value);
				writer.WriteEndElement();
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001C58C File Offset: 0x0001A78C
		private DataTable GetResultSchema(Microsoft.ReportingServices.DataProcessing.IDataReader iDataReader)
		{
			DataTable dataTable = new DataTable("Row");
			string text = "AggInfo";
			for (int i = 0; i < iDataReader.FieldCount; i++)
			{
				DataColumn dataColumn = dataTable.Columns.Add(iDataReader.GetName(i), iDataReader.GetFieldType(i));
				dataColumn.AllowDBNull = true;
				if (string.Compare(dataColumn.ColumnName, text, StringComparison.OrdinalIgnoreCase) == 0)
				{
					text += i.ToString(CultureInfo.InvariantCulture);
				}
			}
			DataColumn dataColumn2 = dataTable.Columns.Add(text, typeof(string));
			dataColumn2.AllowDBNull = true;
			dataColumn2.ColumnMapping = MappingType.Attribute;
			return dataTable;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001C620 File Offset: 0x0001A820
		public RSStream GetUserModelStreamable(string modelPath, string perspectiveID)
		{
			GetUserModelAction getUserModelAction = this.GetUserModelAction;
			getUserModelAction.ActionParameters.ItemPath = modelPath;
			getUserModelAction.ActionParameters.PerspectiveID = perspectiveID;
			getUserModelAction.Execute();
			SemanticModel userModel = getUserModelAction.GetUserModel();
			Encoding utf = Encoding.UTF8;
			Stream newStream = this.StreamManager.GetNewStream(modelPath, "smdl", utf, "text/xml", false, StreamOper.CreateAndRegister);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(newStream, utf);
			userModel.WriteTo(xmlTextWriter, ModelingSerializationOptions.OmitBindings);
			RSTrace.CatalogTrace.Assert(this.StreamManager.PrimaryStream == newStream, "StreamManager.PrimaryStream == stream");
			return this.StreamManager.PrimaryStream;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001C6B0 File Offset: 0x0001A8B0
		public RuntimeDataSourceInfoCollection GetAllDataSources(BaseReportCatalogItem report)
		{
			ReportSnapshot reportSnapshot;
			RuntimeDataSourceInfoCollection runtimeDataSourceInfoCollection;
			RuntimeDataSetInfoCollection runtimeDataSetInfoCollection;
			this.GetAllDataSources(report, false, false, out reportSnapshot, out runtimeDataSourceInfoCollection, out runtimeDataSetInfoCollection);
			return runtimeDataSourceInfoCollection;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001C6D0 File Offset: 0x0001A8D0
		public void GetAllDataSources(BaseReportCatalogItem report, bool checkIfUsable, bool useServiceConnectionForRepublishing, out ReportSnapshot compiledDefinition, out RuntimeDataSourceInfoCollection runtimeDataSources, out RuntimeDataSetInfoCollection runtimeDataSets)
		{
			CatalogItemContext itemContext = report.ItemContext;
			RSTrace.CatalogTrace.Assert(itemContext != null, "reportContext != null");
			ReportCompiledDefinition reportCompiledDefinition = ReportCompiledDefinition.Load(itemContext, this, SecurityRequirements.None, null, useServiceConnectionForRepublishing);
			compiledDefinition = reportCompiledDefinition.DefinitionSnapshot;
			this.GetAllDataSources(null, report, compiledDefinition, checkIfUsable, out runtimeDataSources, out runtimeDataSets);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001C720 File Offset: 0x0001A920
		public void GetAllDataSources(ReportProcessing repProc, CatalogItemContext reportContext, ReportSnapshot intermediateSnapshot, DataSourceInfoCollection thisReportDataSources, DataSetInfoCollection thisReportDataSets, out RuntimeDataSourceInfoCollection runtimeDataSources, out RuntimeDataSetInfoCollection runtimeDataSets)
		{
			this.ProcessingGetAllDataSources(repProc, reportContext, intermediateSnapshot, thisReportDataSources, thisReportDataSets, true, out runtimeDataSources, out runtimeDataSets);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001C740 File Offset: 0x0001A940
		private void GetAllDataSources(ReportProcessing repProc, BaseReportCatalogItem report, ReportSnapshot intermediateSnapshot, bool checkIfUsable, out RuntimeDataSourceInfoCollection runtimeDataSources, out RuntimeDataSetInfoCollection runtimeDataSets)
		{
			RSTrace.CatalogTrace.Assert(report != null);
			CatalogItemContext itemContext = report.ItemContext;
			this.ProcessingGetAllDataSources(repProc, itemContext, intermediateSnapshot, report.DataSources, report.SharedDataSets, checkIfUsable, out runtimeDataSources, out runtimeDataSets);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001C780 File Offset: 0x0001A980
		private void GetAllDataSources(ReportProcessing repProc, CatalogItemContext reportContext, ReportSnapshot intermediateSnapshot, out RuntimeDataSourceInfoCollection runtimeDataSources, out RuntimeDataSetInfoCollection runtimeDataSets)
		{
			BaseReportCatalogItem baseReportCatalogItem = this.CatalogItemFactory.GetCatalogItem(reportContext) as BaseReportCatalogItem;
			RSTrace.CatalogTrace.Assert(baseReportCatalogItem != null);
			this.GetAllDataSources(repProc, baseReportCatalogItem, intermediateSnapshot, true, out runtimeDataSources, out runtimeDataSets);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001C7BC File Offset: 0x0001A9BC
		private void ProcessingGetAllDataSources(ReportProcessing repProc, CatalogItemContext reportContext, ReportSnapshot intermediateSnapshot, DataSourceInfoCollection thisReportDataSources, DataSetInfoCollection thisReportDataSets, bool checkIfUsable, out RuntimeDataSourceInfoCollection runtimeDataSources, out RuntimeDataSetInfoCollection runtimeDataSets)
		{
			thisReportDataSources = this.CombineDataSources(thisReportDataSets, thisReportDataSources);
			if (repProc == null)
			{
				repProc = Global.GetProcessingEngine();
			}
			using (ISnapshotTransaction snapshotTransaction = intermediateSnapshot.EnterTransactionContext())
			{
				using (ISubreportRetrieval subreportRetrieval = SubreportRetrieval.Create(intermediateSnapshot, this))
				{
					for (int i = 0; i < 100; i++)
					{
						try
						{
							repProc.GetAllDataSources(reportContext, ReadOnlyChunkFactory.FromSnapshot(intermediateSnapshot), new ReportProcessing.OnDemandSubReportDataSourcesCallback(subreportRetrieval.GetSubreportDataSources), thisReportDataSources, thisReportDataSets, checkIfUsable, new ServerDataSourceSettings(Globals.Configuration.IsSurrogatePresent, Global.EnableIntegratedSecurity), out runtimeDataSources, out runtimeDataSets);
							snapshotTransaction.Commit();
							return;
						}
						catch (VersionMismatchException ex)
						{
							if (!(ex.ReportID != Guid.Empty))
							{
								throw new InternalCatalogException(ex, "empty report ID in version mismatch exception");
							}
							ReportSnapshot reportSnapshot = ReportSnapshot.Create(ex.ReportID, ex.IsPermanentSnapshot, intermediateSnapshot.DependsOnUser, ReportProcessingFlags.NotSet);
							intermediateSnapshot.ShareTransactionContext(reportSnapshot);
							SnapshotConverter.ConvertFromV1(reportContext, reportSnapshot, false);
						}
					}
				}
			}
			throw new InternalCatalogException("exceeded limit of subsequent upgrades for report {" + intermediateSnapshot.SnapshotDataID.ToString() + "}");
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001C904 File Offset: 0x0001AB04
		public void GetAllDataSources(ClientRequest session, ReportProcessing repProc, CatalogItemContext reportContext, ReportSnapshot intermediateSnapshot, DataSourceInfoCollection thisReportDataSources, DataSetInfoCollection thisReportDataSets, out RuntimeDataSourceInfoCollection runtimeDataSources, out RuntimeDataSetInfoCollection runtimeDataSets)
		{
			if (session.SessionReport.Datasources != null)
			{
				runtimeDataSources = session.SessionReport.Datasources;
				runtimeDataSets = session.SessionReport.DataSets;
				return;
			}
			this.ProcessingGetAllDataSources(repProc, reportContext, intermediateSnapshot, thisReportDataSources, thisReportDataSets, true, out runtimeDataSources, out runtimeDataSets);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001C950 File Offset: 0x0001AB50
		public DataSourceInfoCollection CombineDataSources(DataSetInfoCollection dataSets, DataSourceInfoCollection existingDataSources)
		{
			DataSourceInfoCollection dataSourceInfoCollection = new DataSourceInfoCollection();
			foreach (object obj in existingDataSources)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				dataSourceInfoCollection.Add(dataSourceInfo);
			}
			foreach (DataSetInfo dataSetInfo in dataSets)
			{
				if (dataSetInfo.IsValidReference())
				{
					CatalogItemContext catalogItemContext = new CatalogItemContext(this, new CatalogItemPath(dataSetInfo.AbsolutePath), "");
					foreach (object obj2 in (this.CatalogItemFactory.GetCatalogItem(catalogItemContext, true) as DataSetCatalogItem).DataSources)
					{
						DataSourceInfo dataSourceInfo2 = (DataSourceInfo)obj2;
						dataSourceInfo2.OriginalName = dataSourceInfo2.ID.ToString();
						dataSourceInfoCollection.Add(dataSourceInfo2);
						dataSetInfo.DataSourceId = dataSourceInfo2.ID;
					}
				}
			}
			return dataSourceInfoCollection;
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001CA94 File Offset: 0x0001AC94
		public ReportProcessing.CreateDataExtensionInstance HowToCreateDataExtensionInstance
		{
			get
			{
				return new ReportProcessing.CreateDataExtensionInstance(this.CreateDataExtensionInstance);
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001CAA4 File Offset: 0x0001ACA4
		private Microsoft.ReportingServices.DataProcessing.IDbConnection CreateDataExtensionInstance(string extensionName, Guid modelID)
		{
			Microsoft.ReportingServices.DataProcessing.IDbConnection dbConnection;
			string text;
			if (modelID != Guid.Empty)
			{
				SemanticModel semanticModel;
				SemanticModel userModelForSemanticQuery = this.GetUserModelForSemanticQuery(modelID, out semanticModel);
				if (userModelForSemanticQuery == null || semanticModel == null)
				{
					throw new ItemNotFoundException("modelID");
				}
				dbConnection = ExtensionClassFactory.GetSemanticQueryEngine() as Microsoft.ReportingServices.DataProcessing.IDbConnection;
				if (!(dbConnection is SemanticQueryConnection))
				{
					throw new DataExtensionNotFoundException(Globals.Configuration.Extensions.SemanticQueryEngine.Name);
				}
				((SemanticQueryConnection)dbConnection).Initialize(userModelForSemanticQuery, semanticModel, extensionName, this.UserName, Localization.ClientPrimaryCulture.Name);
				text = "Model." + extensionName;
			}
			else
			{
				text = extensionName;
				dbConnection = ExtensionClassFactory.GetNewInstanceExtensionClass(extensionName, "Data") as Microsoft.ReportingServices.DataProcessing.IDbConnection;
			}
			if (Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext != null && Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext.ExecutionInfo != null)
			{
				Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext.ExecutionInfo.AdditionalInfo.IncrementDataExtensionOperationCounter(text);
			}
			return dbConnection;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001CB70 File Offset: 0x0001AD70
		private SemanticModel GetUserModelForSemanticQuery(Guid modelID, out SemanticModel fullModel)
		{
			object modelSync = this.m_modelSync;
			SemanticModel semanticModel2;
			lock (modelSync)
			{
				CatalogItemPath pathById = this.Storage.GetPathById(modelID);
				if (pathById == null)
				{
					throw new ServerConfigurationErrorException("Can not find model by guid " + modelID.ToString());
				}
				ExternalItemPath externalItemPath = this.CatalogToExternal(pathById);
				CatalogItemContext catalogItemContext = new CatalogItemContext(this, externalItemPath, "modelID");
				ModelCatalogItem modelCatalogItem = this.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.Model) as ModelCatalogItem;
				SemanticModel semanticModel = modelCatalogItem.LoadUserModel(null);
				fullModel = modelCatalogItem.Model;
				semanticModel2 = semanticModel;
			}
			return semanticModel2;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001CC18 File Offset: 0x0001AE18
		public void GetBatchSettingsForScheduleDefinition(ScheduleDefinitionOrReference scheduleData, out string typeString, out string stringScheduleData)
		{
			typeString = null;
			stringScheduleData = null;
			if (scheduleData != null && !(scheduleData is NoSchedule))
			{
				if (scheduleData is ScheduleDefinition)
				{
					typeString = typeof(ScheduleDefinition).ToString();
					stringScheduleData = ScheduleDefinition.DefinitionToXml((ScheduleDefinition)scheduleData);
					return;
				}
				typeString = typeof(ScheduleReference).ToString();
				stringScheduleData = ((ScheduleReference)scheduleData).ScheduleID;
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001CC7C File Offset: 0x0001AE7C
		public ScheduleDefinitionOrReference RetriveScheduleFromBatchStrings(string typeString, string scheduleData)
		{
			ScheduleDefinitionOrReference scheduleDefinitionOrReference = new NoSchedule();
			if (typeString != null)
			{
				if (typeString == typeof(ScheduleDefinition).ToString())
				{
					scheduleDefinitionOrReference = ScheduleDefinition.XmlToDefinition(scheduleData);
				}
				else if (typeString == typeof(ScheduleReference).ToString())
				{
					scheduleDefinitionOrReference = new ScheduleReference
					{
						ScheduleID = scheduleData
					};
				}
			}
			return scheduleDefinitionOrReference;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001CCD8 File Offset: 0x0001AED8
		public Guid CreateBatch()
		{
			Guid guid2;
			try
			{
				this.WillDisconnectStorage();
				this.Tracer.Trace(TraceLevel.Verbose, "Call to CreateBatch()");
				Guid guid = Guid.NewGuid();
				this.Storage.AddBatchRecord(guid, this.UserName, CatalogCommand.BatchStart, null, null, null, null, null, null, false, null, null);
				this.Tracer.Trace(TraceLevel.Verbose, "Call to CreateBatch completed, returning {0}", new object[] { guid });
				guid2 = guid;
			}
			finally
			{
				this.DisconnectStorage();
			}
			return guid2;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001CD5C File Offset: 0x0001AF5C
		public void ExecuteBatch(Guid batchId)
		{
			try
			{
				this.WillDisconnectStorage();
				this.m_useBatchConnectionManager = true;
				this.Tracer.Trace(TraceLevel.Verbose, "Call to ExecuteBatch( {0} )", new object[] { batchId });
				base.GetType();
				ArrayList batchRecords = this.Storage.GetBatchRecords(batchId);
				if (batchRecords == null)
				{
					throw new BatchNotFoundException(batchId.ToString());
				}
				foreach (object obj in batchRecords)
				{
					CallParameters callParameters = (CallParameters)obj;
					switch ((CatalogCommand)Enum.Parse(typeof(CatalogCommand), callParameters.Action))
					{
					case CatalogCommand.BatchStart:
						this.BatchStart(callParameters);
						break;
					case CatalogCommand.DeleteItem:
						this.DeleteItemAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.MoveItem:
						this.MoveItemAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetProperties:
						this.SetPropertiesAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.CreateFolder:
						this.CreateFolderAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.CreateReport:
						this.CreateReportAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetReportDefinition:
						this.SetReportDefinitionAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetReportParameters:
						this.SetReportParametersAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.CreateLinkedReport:
						this.CreateLinkedReportAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetReportLink:
						this.SetReportLinkAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.CreateResource:
						this.CreateResourceAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetResourceContents:
						this.SetResourceContentsAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.CreateSnapshot:
						this.CreateSnapshotAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetSnapshotLimit:
						this.SetSnapshotLimitAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.DeleteSnapshot:
						this.DeleteSnapshotAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.CreateSchedule:
						this.CreateScheduleAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.DeleteSchedule:
						this.DeleteScheduleAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetScheduleProperties:
						this.SetSchedulePropertiesAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.PauseSchedule:
						this.PauseScheduleAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.ResumeSchedule:
						this.ResumeScheduleAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.CreateSubscription:
					case CatalogCommand.CreateDataDrivenSubscription:
						this.CreateSubscriptionAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetSubscriptionProperties:
					case CatalogCommand.SetDataDrivenSubscription:
						this.SetSubscriptionPropertiesAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.DisableSubscription:
						this.DisableSubscriptionAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.EnableSubscription:
						this.EnableSubscriptionAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.DeleteSubscription:
						this.DeleteSubscriptionAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetExecutionOptions:
						this.SetExecutionOptionsAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetCacheOptions:
						this.SetCacheOptionsAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.UpdateSnapshot:
						this.UpdateExecutionSnapshotAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.FlushCache:
						this.FlushCacheAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetReportHistoryOptions:
						this.SetReportHistoryOptionsAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.FireEvent:
						this.FireEventAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.CreateRole:
						this.CreateRoleAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.DeleteRole:
						this.DeleteRoleAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetRoleProperties:
						this.SetRolePropertiesAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetPolicies:
						this.SetPoliciesAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetSystemPolicies:
						this.SetSystemPoliciesAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.DeletePolicies:
						this.DeletePoliciesAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.CreateDataSource:
						this.CreateDataSourceAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetDataSourceContents:
						this.SetDataSourceContentsAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.ChangeStateOfDataSource:
						this.ChangeDataSourceStateAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetItemDataSources:
						this.SetItemDataSourcesAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.CreateModel:
						this.CreateModelAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetModelDefinition:
						this.SetModelDefinitionAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetModelItemPolicies:
						this.SetModelItemPoliciesAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.RemoveAllModelItemPolicies:
						this.RemoveAllModelItemPoliciesAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetDrillthroughReports:
						this.SetDrillthroughReportsAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.GenerateModel:
						this.GenerateModelAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.RegenerateModel:
						this.RegenerateModelAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.CreateRdlxReport:
						this.CreateRdlxReportAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.SetRdlxReportDefinition:
						this.SetRdlxReportDefinitionAction.PerformActionInBatch(callParameters);
						break;
					case CatalogCommand.CreateKpi:
						this.CreateKpiAction.PerformActionInBatch(callParameters);
						break;
					default:
						throw new InternalCatalogException("Unknown catalog command in batching");
					}
				}
				this.Storage.DeleteBatchRecords(batchId);
				this.Tracer.Trace(TraceLevel.Verbose, "Call to ExecuteBatch[{0}] completed", new object[] { batchId });
			}
			catch (Exception)
			{
				this.AbortTransaction();
				this.Storage.DeleteBatchRecords(batchId);
				throw;
			}
			finally
			{
				this.DisconnectStorage();
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001D2D4 File Offset: 0x0001B4D4
		public void CancelBatch(Guid batchId)
		{
			try
			{
				this.WillDisconnectStorage();
				this.Tracer.Trace(TraceLevel.Verbose, "Call to DeleteBatch( {0} )", new object[] { batchId });
				if (!this.Storage.DeleteBatchRecords(batchId))
				{
					throw new BatchNotFoundException(batchId.ToString());
				}
				this.Tracer.Trace(TraceLevel.Verbose, "Call to DeleteBatch completed");
			}
			finally
			{
				this.DisconnectStorage();
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001D354 File Offset: 0x0001B554
		private void BatchStart(CallParameters parameters)
		{
			this.Tracer.Trace(TraceLevel.Verbose, "Starting batch execution.");
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001D368 File Offset: 0x0001B568
		public void InvalidateSubscription(Guid subscriptionID, InActiveFlags inactiveFlag, string status)
		{
			try
			{
				this.SubscriptionManager.ConnectionManager.BeginTransaction();
				this.SubscriptionManager.InvalidateSubscription(subscriptionID, inactiveFlag, status);
				this.SubscriptionManager.ConnectionManager.CommitTransaction();
			}
			catch
			{
				this.SubscriptionManager.ConnectionManager.AbortTransaction();
				throw;
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001D3C8 File Offset: 0x0001B5C8
		public void ValidateSubscriptionParameters(ExternalItemPath path, ParameterValueOrFieldReference[] subscriptionParameters, bool isDataDriven)
		{
			try
			{
				this.SubscriptionManager.ConnectionManager.BeginTransaction();
				JobType jobType = SubscriptionJobType.CreateSubscriptionJobType(isDataDriven, JobTypeEnum.System);
				this.SubscriptionManager.ValidateSubscriptionParameters(path, subscriptionParameters, jobType);
				this.SubscriptionManager.ConnectionManager.CommitTransaction();
			}
			catch
			{
				this.SubscriptionManager.ConnectionManager.AbortTransaction();
				throw;
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001D434 File Offset: 0x0001B634
		public void UpdateSubscriptionLastRunInfo(Guid subscriptionID, InActiveFlags stateFlag, DateTime lastRunTime, string status)
		{
			try
			{
				this.SubscriptionManager.ConnectionManager.BeginTransaction();
				this.SubscriptionManager.UpdateSubscriptionLastRunInfo(subscriptionID, stateFlag, lastRunTime, status);
				this.SubscriptionManager.ConnectionManager.CommitTransaction();
			}
			catch
			{
				this.SubscriptionManager.ConnectionManager.AbortTransaction();
				throw;
			}
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001D498 File Offset: 0x0001B698
		public void UpdateSubscriptionStatus(Guid subscriptionID, string status)
		{
			try
			{
				this.SubscriptionManager.ConnectionManager.BeginTransaction();
				this.SubscriptionManager.UpdateSubscriptionStatus(subscriptionID, status);
				this.SubscriptionManager.ConnectionManager.CommitTransaction();
			}
			catch
			{
				this.SubscriptionManager.ConnectionManager.AbortTransaction();
				throw;
			}
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001D4F8 File Offset: 0x0001B6F8
		public string GetSubscriptionBatchXmlBlob(string id, string eventType, string subscriptionData, string description, string parameters, string extensionSettings, string dataSettings, string isDataDriven)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("Subscription");
			xmlDocument.AppendChild(xmlElement);
			XmlElement xmlElement2 = xmlDocument.CreateElement("id");
			xmlElement2.InnerText = id;
			xmlElement.AppendChild(xmlElement2);
			xmlElement2 = xmlDocument.CreateElement("EventType");
			xmlElement2.InnerText = eventType;
			xmlElement.AppendChild(xmlElement2);
			xmlElement2 = xmlDocument.CreateElement("SubscriptionData");
			xmlElement2.InnerText = subscriptionData;
			xmlElement.AppendChild(xmlElement2);
			xmlElement2 = xmlDocument.CreateElement("Description");
			xmlElement2.InnerText = description;
			xmlElement.AppendChild(xmlElement2);
			xmlElement2 = xmlDocument.CreateElement("Parameters");
			xmlElement2.InnerText = parameters;
			xmlElement.AppendChild(xmlElement2);
			xmlElement2 = xmlDocument.CreateElement("ExtensionSettings");
			xmlElement2.InnerText = extensionSettings;
			xmlElement.AppendChild(xmlElement2);
			xmlElement2 = xmlDocument.CreateElement("DataSettings");
			xmlElement2.InnerText = dataSettings;
			xmlElement.AppendChild(xmlElement2);
			xmlElement2 = xmlDocument.CreateElement("IsDataDriven");
			xmlElement2.InnerText = isDataDriven;
			xmlElement.AppendChild(xmlElement2);
			return xmlDocument.OuterXml;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001D600 File Offset: 0x0001B800
		public Microsoft.ReportingServices.Library.Soap.DataSetDefinition PrepareQuery(Microsoft.ReportingServices.Library.Soap2005.DataSource dataSource, Microsoft.ReportingServices.Library.Soap.DataSetDefinition dataSet, out ReportParameter[] parameters, out bool changed)
		{
			Microsoft.ReportingServices.Library.Soap.DataSetDefinition dataSetDefinition2;
			try
			{
				this.WillDisconnectStorage();
				this.Storage.ConnectionManager.VerifyConnection(true);
				if (this.Tracer.IsTraceLevelEnabled(TraceLevel.Verbose))
				{
					this.Tracer.Trace(TraceLevel.Verbose, "Call to PrepareQuery: dataSource ({0}), dataSet({1}).", new object[]
					{
						Microsoft.ReportingServices.Library.Soap2005.DataSource.ThisToXml(dataSource),
						Microsoft.ReportingServices.Library.Soap.DataSetDefinition.ThisToXml(dataSet)
					});
				}
				Microsoft.ReportingServices.Library.Soap.DataSetDefinition dataSetDefinition = this.SubscriptionManager.PrepareQuery(dataSource, dataSet, out parameters, out changed);
				this.Tracer.Trace(TraceLevel.Verbose, "Call to PrepareQuery completed. Returned ({0}), changed={1}", new object[] { dataSetDefinition, changed });
				dataSetDefinition2 = dataSetDefinition;
			}
			catch (Exception ex)
			{
				this.AbortTransaction();
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				this.DisconnectStorage();
			}
			return dataSetDefinition2;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001D6D4 File Offset: 0x0001B8D4
		public ExtensionParameter[] ValidateExtensionSettings(string extension, ParameterValueOrFieldReference[] settings, string path)
		{
			ExtensionParameter[] array2;
			try
			{
				if (extension == null)
				{
					throw new InvalidParameterException("Extension");
				}
				this.WillDisconnectStorage();
				IExtension newInstanceExtensionClass = ExtensionClassFactory.GetNewInstanceExtensionClass(extension, "Delivery");
				Settings settings2 = new Settings();
				settings2.FromSoapParameterValueArray(settings);
				if (!(newInstanceExtensionClass is IDeliveryExtension))
				{
					throw new InvalidParameterException("Extension");
				}
				IDeliveryExtension deliveryExtension = (IDeliveryExtension)newInstanceExtensionClass;
				ExternalItemPath externalItemPath = new ExternalItemPath(path);
				ProviderManager.InitDeliveryExtension(deliveryExtension, true, this, externalItemPath);
				Setting[] array = deliveryExtension.ValidateUserData(settings2.SettingsArray);
				array2 = this.CLRSettingToSoapSettingArray(array, false);
			}
			catch (Exception ex)
			{
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				this.DisconnectStorage();
			}
			return array2;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001D784 File Offset: 0x0001B984
		private ExtensionParameter[] CLRSettingToSoapSettingArray(Setting[] settings, bool doReturnValue)
		{
			if (settings == null)
			{
				return null;
			}
			ExtensionParameter[] array = new ExtensionParameter[settings.Length];
			for (int i = 0; i < settings.Length; i++)
			{
				ExtensionParameter extensionParameter = new ExtensionParameter();
				Setting setting = settings[i];
				extensionParameter.Name = setting.Name;
				extensionParameter.DisplayName = setting.DisplayName;
				if (doReturnValue)
				{
					if (setting.Field != null && setting.Field != string.Empty)
					{
						throw new ServerConfigurationErrorException("Settings returned from an extension should not contain fields");
					}
					extensionParameter.Value = setting.Value;
				}
				if (setting.ValidValues != null && setting.ValidValues.Length != 0)
				{
					extensionParameter.ValidValues = new Microsoft.ReportingServices.Library.Soap.ValidValue[setting.ValidValues.Length];
					int num = 0;
					foreach (Microsoft.ReportingServices.Interfaces.ValidValue validValue in setting.ValidValues)
					{
						extensionParameter.ValidValues[num] = new Microsoft.ReportingServices.Library.Soap.ValidValue();
						extensionParameter.ValidValues[num].Value = validValue.Value;
						extensionParameter.ValidValues[num++].Label = validValue.Label;
					}
				}
				extensionParameter.RequiredSpecified = true;
				extensionParameter.Required = setting.Required;
				extensionParameter.ReadOnly = setting.ReadOnly;
				extensionParameter.Encrypted = setting.Encrypted;
				extensionParameter.IsPassword = setting.IsPassword;
				extensionParameter.Error = setting.Error;
				array[i] = extensionParameter;
			}
			return array;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001D8D8 File Offset: 0x0001BAD8
		public ExtensionParameter[] GetExtensionSettings(string extension)
		{
			ExtensionParameter[] array;
			try
			{
				array = this.CLRSettingToSoapSettingArray(this.GetExtensionSettingsInternal(extension), true);
			}
			catch (Exception ex)
			{
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			return array;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001D918 File Offset: 0x0001BB18
		public Setting[] GetExtensionSettingsInternal(string extension)
		{
			Setting[] array;
			try
			{
				if (extension == null)
				{
					throw new InvalidParameterException("Extension");
				}
				IDeliveryExtension deliveryExtension = ExtensionClassFactory.GetNewInstanceExtensionClass(extension, "Delivery") as IDeliveryExtension;
				if (deliveryExtension == null)
				{
					throw new InvalidParameterException("Extension");
				}
				array = ProviderManager.InitDeliveryExtension(deliveryExtension, true, this, null);
			}
			catch (Exception ex)
			{
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			return array;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001D980 File Offset: 0x0001BB80
		public SubscriptionImpl[] ListSubscriptionsUsingDataSource(string name)
		{
			SubscriptionImpl[] array2;
			try
			{
				this.WillDisconnectStorage();
				this.Tracer.Trace(TraceLevel.Verbose, "Call to ListSubscriptionsUsingDataSource: Name({0}).", new object[] { name });
				this.SetDatabaseConnectionSettings(ConnectionTransactionType.AutoCommit, ConnectionManager.DefaultIsolationLevel);
				CatalogItemContext catalogItemContext = new CatalogItemContext(this, name, "name");
				ItemType itemType;
				byte[] array;
				if (!this.Storage.ObjectExists(catalogItemContext.ItemPath, out itemType, out array))
				{
					throw new ItemNotFoundException(name);
				}
				RSService.EnsureItemType(itemType, name, new ItemType[] { ItemType.DataSource });
				if (!this.SecMgr.CheckAccess(itemType, array, DatasourceOperation.ReadProperties, catalogItemContext.ItemPath))
				{
					throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
				}
				List<SubscriptionImpl> list;
				if (!Sku.IsFeatureEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.DataDrivenSubscriptions))
				{
					list = new List<SubscriptionImpl>();
				}
				else
				{
					list = this.SubscriptionManager.ListSubscriptionsUsingDataSource(catalogItemContext.ItemPath, this);
				}
				this.Tracer.Trace(TraceLevel.Verbose, "Call to ListSubscriptionsUsingDataSource completed");
				array2 = list.ToArray();
			}
			catch (Exception ex)
			{
				this.AbortTransaction();
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				this.DisconnectStorage();
			}
			return array2;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001DAA4 File Offset: 0x0001BCA4
		public Microsoft.ReportingServices.Library.Soap2005.Extension[] ListExtensions(ExtensionTypeEnum type)
		{
			Microsoft.ReportingServices.Library.Soap2005.Extension[] array2;
			try
			{
				this.WillDisconnectStorage();
				this.Tracer.Trace(TraceLevel.Verbose, "Call to ListProviders: type ({0}).", new object[] { type });
				Microsoft.ReportingServices.Library.Soap2005.Extension[] array = ProviderManager.ListProviders(type);
				this.Tracer.Trace(TraceLevel.Verbose, "Call to ListProviders completed.");
				array2 = array;
			}
			catch (Exception ex)
			{
				this.AbortTransaction();
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				this.DisconnectStorage();
			}
			return array2;
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0001DB2C File Offset: 0x0001BD2C
		public ListEventsAction ListEventsAction
		{
			get
			{
				if (this.m_listEventsAction == null)
				{
					this.m_listEventsAction = new ListEventsAction(this);
				}
				return this.m_listEventsAction;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001DB48 File Offset: 0x0001BD48
		public FireEventAction FireEventAction
		{
			get
			{
				if (this.m_fireEventAction == null)
				{
					this.m_fireEventAction = new FireEventAction(this);
				}
				return this.m_fireEventAction;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001DB64 File Offset: 0x0001BD64
		public ListTasksAction ListTasksAction
		{
			get
			{
				if (this.m_listTasksAction == null)
				{
					this.m_listTasksAction = new ListTasksAction(this);
				}
				return this.m_listTasksAction;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001DB80 File Offset: 0x0001BD80
		public ListRolesAction ListRolesAction
		{
			get
			{
				if (this.m_listRolesAction == null)
				{
					this.m_listRolesAction = new ListRolesAction(this);
				}
				return this.m_listRolesAction;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0001DB9C File Offset: 0x0001BD9C
		public CreateRoleAction CreateRoleAction
		{
			get
			{
				if (this.m_createRoleAction == null)
				{
					this.m_createRoleAction = new CreateRoleAction(this);
				}
				return this.m_createRoleAction;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001DBB8 File Offset: 0x0001BDB8
		public DeleteRoleAction DeleteRoleAction
		{
			get
			{
				if (this.m_deleteRoleAction == null)
				{
					this.m_deleteRoleAction = new DeleteRoleAction(this);
				}
				return this.m_deleteRoleAction;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001DBD4 File Offset: 0x0001BDD4
		public GetRolePropertiesAction GetRolePropertiesAction
		{
			get
			{
				if (this.m_getRolePropertiesAction == null)
				{
					this.m_getRolePropertiesAction = new GetRolePropertiesAction(this);
				}
				return this.m_getRolePropertiesAction;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0001DBF0 File Offset: 0x0001BDF0
		public SetRolePropertiesAction SetRolePropertiesAction
		{
			get
			{
				if (this.m_setRolePropertiesAction == null)
				{
					this.m_setRolePropertiesAction = new SetRolePropertiesAction(this);
				}
				return this.m_setRolePropertiesAction;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001DC0C File Offset: 0x0001BE0C
		public GetPoliciesAction GetPoliciesAction
		{
			get
			{
				if (this.m_getPoliciesAction == null)
				{
					this.m_getPoliciesAction = new GetPoliciesAction(this);
				}
				return this.m_getPoliciesAction;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0001DC28 File Offset: 0x0001BE28
		public GetSystemPoliciesAction GetSystemPoliciesAction
		{
			get
			{
				if (this.m_getSystemPoliciesAction == null)
				{
					this.m_getSystemPoliciesAction = new GetSystemPoliciesAction(this);
				}
				return this.m_getSystemPoliciesAction;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001DC44 File Offset: 0x0001BE44
		public SetPoliciesAction SetPoliciesAction
		{
			get
			{
				if (this.m_setPoliciesAction == null)
				{
					this.m_setPoliciesAction = new SetPoliciesAction(this);
				}
				return this.m_setPoliciesAction;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0001DC60 File Offset: 0x0001BE60
		public SetSystemPoliciesAction SetSystemPoliciesAction
		{
			get
			{
				if (this.m_setSystemPoliciesAction == null)
				{
					this.m_setSystemPoliciesAction = new SetSystemPoliciesAction(this);
				}
				return this.m_setSystemPoliciesAction;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001DC7C File Offset: 0x0001BE7C
		public DeletePoliciesAction DeletePoliciesAction
		{
			get
			{
				if (this.m_deletePoliciesAction == null)
				{
					this.m_deletePoliciesAction = new DeletePoliciesAction(this);
				}
				return this.m_deletePoliciesAction;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0001DC98 File Offset: 0x0001BE98
		public GetPermissionsAction GetPermissionsAction
		{
			get
			{
				if (this.m_getPermissionsAction == null)
				{
					this.m_getPermissionsAction = new GetPermissionsAction(this);
				}
				return this.m_getPermissionsAction;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0001DCB4 File Offset: 0x0001BEB4
		public GetSystemPermissionsAction GetSystemPermissionsAction
		{
			get
			{
				if (this.m_getSystemPermissionsAction == null)
				{
					this.m_getSystemPermissionsAction = new GetSystemPermissionsAction(this);
				}
				return this.m_getSystemPermissionsAction;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001DCD0 File Offset: 0x0001BED0
		public CreateDataSourceAction CreateDataSourceAction
		{
			get
			{
				if (this.m_createDataSourceAction == null)
				{
					this.m_createDataSourceAction = new CreateDataSourceAction(this);
				}
				return this.m_createDataSourceAction;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0001DCEC File Offset: 0x0001BEEC
		public ChangeDataSourceStateAction ChangeDataSourceStateAction
		{
			get
			{
				if (this.m_changeDataSourceStateAction == null)
				{
					this.m_changeDataSourceStateAction = new ChangeDataSourceStateAction(this);
				}
				return this.m_changeDataSourceStateAction;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001DD08 File Offset: 0x0001BF08
		public GetDataSourceContentsAction GetDataSourceContentsAction
		{
			get
			{
				if (this.m_getDataSourceContentsAction == null)
				{
					this.m_getDataSourceContentsAction = new GetDataSourceContentsAction(this);
				}
				return this.m_getDataSourceContentsAction;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0001DD24 File Offset: 0x0001BF24
		public GetItemDataSourcesAction GetItemDataSourcesAction
		{
			get
			{
				if (this.m_getItemDataSourcesAction == null)
				{
					this.m_getItemDataSourcesAction = new GetItemDataSourcesAction(this);
				}
				return this.m_getItemDataSourcesAction;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001DD40 File Offset: 0x0001BF40
		public GetItemDataSourcePromptsAction GetItemDataSourcePromptsAction
		{
			get
			{
				if (this.m_getItemDataSourcePromptsAction == null)
				{
					this.m_getItemDataSourcePromptsAction = new GetItemDataSourcePromptsAction(this);
				}
				return this.m_getItemDataSourcePromptsAction;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001DD5C File Offset: 0x0001BF5C
		public SetDataSourceContentsAction SetDataSourceContentsAction
		{
			get
			{
				if (this.m_setDataSourceContentsAction == null)
				{
					this.m_setDataSourceContentsAction = new SetDataSourceContentsAction(this);
				}
				return this.m_setDataSourceContentsAction;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001DD78 File Offset: 0x0001BF78
		public SetItemDataSourcesAction SetItemDataSourcesAction
		{
			get
			{
				if (this.m_setItemDataSourcesAction == null)
				{
					this.m_setItemDataSourcesAction = new SetItemDataSourcesAction(this);
				}
				return this.m_setItemDataSourcesAction;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0001DD94 File Offset: 0x0001BF94
		public ListDependentItemsAction ListDependentItemsAction
		{
			get
			{
				if (this.m_listDependentItemsAction == null)
				{
					this.m_listDependentItemsAction = new ListDependentItemsAction(this);
				}
				return this.m_listDependentItemsAction;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001DDB0 File Offset: 0x0001BFB0
		public TestConnectForItemDataSourceAction TestConnectForItemDataSourceAction
		{
			get
			{
				if (this.m_testConnectForItemDataSourceAction == null)
				{
					this.m_testConnectForItemDataSourceAction = new TestConnectForItemDataSourceAction(this);
				}
				return this.m_testConnectForItemDataSourceAction;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001DDCC File Offset: 0x0001BFCC
		public TestConnectForDataSourceDefinitionAction TestConnectForDataSourceDefinitionAction
		{
			get
			{
				if (this.m_testConnectForDataSourceDefinitionAction == null)
				{
					this.m_testConnectForDataSourceDefinitionAction = new TestConnectForDataSourceDefinitionAction(this);
				}
				return this.m_testConnectForDataSourceDefinitionAction;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001DDE8 File Offset: 0x0001BFE8
		public ListRunningJobsAction ListRunningJobsAction
		{
			get
			{
				if (this.m_listRunningJobsAction == null)
				{
					this.m_listRunningJobsAction = new ListRunningJobsAction(this);
				}
				return this.m_listRunningJobsAction;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001DE04 File Offset: 0x0001C004
		public CancelJobAction CancelJobAction
		{
			get
			{
				if (this.m_cancelJobAction == null)
				{
					this.m_cancelJobAction = new CancelJobAction(this);
				}
				return this.m_cancelJobAction;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001DE20 File Offset: 0x0001C020
		public CreateModelAction CreateModelAction
		{
			get
			{
				if (this.m_createModelAction == null)
				{
					this.m_createModelAction = new CreateModelAction(this);
				}
				return this.m_createModelAction;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001DE3C File Offset: 0x0001C03C
		public GetModelDefinitionAction GetModelDefinitionAction
		{
			get
			{
				if (this.m_getModelDefinitionAction == null)
				{
					this.m_getModelDefinitionAction = new GetModelDefinitionAction(this);
				}
				return this.m_getModelDefinitionAction;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001DE58 File Offset: 0x0001C058
		public SetModelDefinitionAction SetModelDefinitionAction
		{
			get
			{
				if (this.m_setModelDefinitionAction == null)
				{
					this.m_setModelDefinitionAction = new SetModelDefinitionAction(this);
				}
				return this.m_setModelDefinitionAction;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001DE74 File Offset: 0x0001C074
		public ListModelPerspectivesAction ListModelPerspectivesAction
		{
			get
			{
				if (this.m_listModelPerspectivesAction == null)
				{
					this.m_listModelPerspectivesAction = this.ServiceHelper.GetListModelPerspectivesActionInternal();
				}
				return this.m_listModelPerspectivesAction;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001DE95 File Offset: 0x0001C095
		public ListModelItemChildrenAction ListModelItemChildrenAction
		{
			get
			{
				if (this.m_listModelItemChildrenAction == null)
				{
					this.m_listModelItemChildrenAction = new ListModelItemChildrenAction(this);
				}
				return this.m_listModelItemChildrenAction;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001DEB1 File Offset: 0x0001C0B1
		public GetUserModelAction GetUserModelAction
		{
			get
			{
				if (this.m_getUserModelAction == null)
				{
					this.m_getUserModelAction = new GetUserModelAction(this);
				}
				return this.m_getUserModelAction;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001DECD File Offset: 0x0001C0CD
		public GetModelItemPermissionsAction GetModelItemPermissionsAction
		{
			get
			{
				if (this.m_getModelItemPermissionsAction == null)
				{
					this.m_getModelItemPermissionsAction = new GetModelItemPermissionsAction(this);
				}
				return this.m_getModelItemPermissionsAction;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001DEE9 File Offset: 0x0001C0E9
		public GetModelItemPoliciesAction GetModelItemPoliciesAction
		{
			get
			{
				if (this.m_getModelItemPoliciesAction == null)
				{
					this.m_getModelItemPoliciesAction = new GetModelItemPoliciesAction(this);
				}
				return this.m_getModelItemPoliciesAction;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001DF05 File Offset: 0x0001C105
		public SetModelItemPoliciesAction SetModelItemPoliciesAction
		{
			get
			{
				if (this.m_setModelItemPoliciesAction == null)
				{
					this.m_setModelItemPoliciesAction = new SetModelItemPoliciesAction(this);
				}
				return this.m_setModelItemPoliciesAction;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001DF21 File Offset: 0x0001C121
		public RemoveAllModelItemPoliciesAction RemoveAllModelItemPoliciesAction
		{
			get
			{
				if (this.m_removeAllModelItemPoliciesAction == null)
				{
					this.m_removeAllModelItemPoliciesAction = new RemoveAllModelItemPoliciesAction(this);
				}
				return this.m_removeAllModelItemPoliciesAction;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001DF3D File Offset: 0x0001C13D
		public SetDrillthroughReportsAction SetDrillthroughReportsAction
		{
			get
			{
				if (this.m_setDrillthroughReportsAction == null)
				{
					this.m_setDrillthroughReportsAction = new SetDrillthroughReportsAction(this);
				}
				return this.m_setDrillthroughReportsAction;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001DF59 File Offset: 0x0001C159
		public GetModelItemReferencesAction GetModelItemReferencesAction
		{
			get
			{
				if (this.m_getModelItemReferencesAction == null)
				{
					this.m_getModelItemReferencesAction = new GetModelItemReferencesAction(this);
				}
				return this.m_getModelItemReferencesAction;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001DF75 File Offset: 0x0001C175
		public SetModelItemReferencesAction SetModelItemReferencesAction
		{
			get
			{
				if (this.m_setModelItemReferencesAction == null)
				{
					this.m_setModelItemReferencesAction = new SetModelItemReferencesAction(this);
				}
				return this.m_setModelItemReferencesAction;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001DF91 File Offset: 0x0001C191
		public GenerateModelAction GenerateModelAction
		{
			get
			{
				if (this.m_generateModelAction == null)
				{
					this.m_generateModelAction = new GenerateModelAction(this);
				}
				return this.m_generateModelAction;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001DFAD File Offset: 0x0001C1AD
		public RegenerateModelAction RegenerateModelAction
		{
			get
			{
				if (this.m_regenerateModelAction == null)
				{
					this.m_regenerateModelAction = new RegenerateModelAction(this);
				}
				return this.m_regenerateModelAction;
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001DFCC File Offset: 0x0001C1CC
		public void GetReportServerConfigInfo(bool scaleOut, out string[] machineNames, out string[] instanceNames, out string[] serviceAccountNames, out string[] reportServerUrlItems)
		{
			string[] machineNamesTemp = null;
			string[] instanceNamesTemp = null;
			string[] serviceAccountNamesTemp = null;
			string[] reportServerUrlItemsTemp = null;
			RevertImpersonationContext.Run(delegate
			{
				string text = Globals.Configuration.InstallationID.ToString();
				string[] array;
				byte[] array2;
				Activation.ListReportServersInDB(out machineNamesTemp, out instanceNamesTemp, out array, out array2);
				serviceAccountNamesTemp = new string[machineNamesTemp.Length];
				reportServerUrlItemsTemp = new string[machineNamesTemp.Length];
				for (int i = 0; i < machineNamesTemp.Length; i++)
				{
					if (string.Compare(array[i], text, StringComparison.OrdinalIgnoreCase) == 0)
					{
						using (WindowsIdentity current = WindowsIdentity.GetCurrent())
						{
							serviceAccountNamesTemp[i] = current.Name;
						}
						reportServerUrlItemsTemp[i] = Globals.Configuration.ReportServerVirtualDirectory;
						return;
					}
				}
			});
			machineNames = machineNamesTemp;
			instanceNames = instanceNamesTemp;
			serviceAccountNames = serviceAccountNamesTemp;
			reportServerUrlItems = reportServerUrlItemsTemp;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001E030 File Offset: 0x0001C230
		public void ConvertToIntermediate(byte[] definition, bool usePermanentSnapshot, ItemProperties properties, CatalogItemContext reportContext, DateTime currentDate, bool checkAccessForSharedDatasources, ReportProcessingFlags previousProcessingFlags, bool isInternalRepublish, bool isRdlx, out ReportSnapshot intermediateSnapshot, out ParameterInfoCollection parameters, out Warning[] warnings, out DataSourceInfoCollection dataSources, out DataSetInfoCollection dataSets, out PageProperties pageProperties, out byte[] dataCacheHash)
		{
			this.ConvertToIntermediate(definition, usePermanentSnapshot, properties, reportContext, currentDate, checkAccessForSharedDatasources, false, null, null, previousProcessingFlags, isInternalRepublish, isRdlx, out intermediateSnapshot, out parameters, out warnings, out dataSources, out dataSets, out pageProperties, out dataCacheHash);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001E064 File Offset: 0x0001C264
		public void ConvertToIntermediate(byte[] definition, bool usePermanentSnapshot, ItemProperties properties, CatalogItemContext reportContext, DateTime currentDate, bool checkAccessForSharedDatasources, bool resolveTemporaryDataSource, DataSourceInfoCollection originalDataSources, DataSetInfoCollection originalDataSets, ReportProcessingFlags previousProcessingFlags, bool isInternalRepublish, bool isRdlx, out ReportSnapshot intermediateSnapshot, out ParameterInfoCollection parameters, out Warning[] warnings, out DataSourceInfoCollection dataSources, out DataSetInfoCollection dataSets, out PageProperties pageProperties, out byte[] dataCacheHash)
		{
			ReportProcessing processingEngine = Global.GetProcessingEngine();
			intermediateSnapshot = this.AllocateNewReportSnapshot(usePermanentSnapshot, null, currentDate, null, ReportProcessingFlags.OnDemandEngine);
			ReportProcessing.CheckSharedDataSource checkSharedDataSource;
			ReportProcessing.CheckSharedDataSet checkSharedDataSet;
			if (checkAccessForSharedDatasources)
			{
				checkSharedDataSource = new ReportProcessing.CheckSharedDataSource(this.CheckDataSourcePublishingCallback);
				checkSharedDataSet = new ReportProcessing.CheckSharedDataSet(this.CheckDataSetPublishingCallback);
			}
			else
			{
				checkSharedDataSource = new ReportProcessing.CheckSharedDataSource(this.NoAccessCheckDataSourcePublishingCallback);
				checkSharedDataSet = new ReportProcessing.CheckSharedDataSet(this.NoAccessCheckDataSetPublishingCallback);
			}
			ReportProcessing.ResolveTemporaryDataSource resolveTemporaryDataSource2 = null;
			ReportProcessing.ResolveTemporaryDataSet resolveTemporaryDataSet = null;
			if (resolveTemporaryDataSource)
			{
				resolveTemporaryDataSource2 = new ReportProcessing.ResolveTemporaryDataSource(RSService.ResolveTemporaryDataSource);
				resolveTemporaryDataSet = new ReportProcessing.ResolveTemporaryDataSet(this.ResolveTemporaryDataSet);
			}
			try
			{
				PublishingResult publishingResult = null;
				using (ISnapshotTransaction snapshotTransaction = intermediateSnapshot.EnterTransactionContext())
				{
					PublishingContext publishingContext = new PublishingContext(reportContext, definition, intermediateSnapshot, null, false, previousProcessingFlags, checkSharedDataSource, resolveTemporaryDataSource2, originalDataSources, checkSharedDataSet, resolveTemporaryDataSet, originalDataSets, processingEngine.Configuration, DataProtection.Instance, isInternalRepublish, isRdlx, isRdlx);
					publishingResult = processingEngine.CreateIntermediateFormat(publishingContext);
					snapshotTransaction.Commit();
				}
				if (ReportProcessingFlags.OnDemandEngine != publishingResult.ReportProcessingFlags)
				{
					intermediateSnapshot.SetSnapshotProcessingFlags(publishingResult.ReportProcessingFlags, null);
				}
				string text = publishingResult.ReportLanguage;
				if (text == null)
				{
					text = CultureInfo.CurrentUICulture.Name;
				}
				if (properties != null)
				{
					if (properties.Description == null && publishingResult.ReportDescription != null)
					{
						if (publishingResult.ReportDescription.Length > 512)
						{
							throw new InvalidElementException("Description");
						}
						properties.Description = publishingResult.ReportDescription;
					}
					properties.Language = text;
					properties.HasUserProfileQueryDependencies = publishingResult.HasUserProfileQueryDependencies.ToString();
					properties.HasUserProfileReportDependencies = publishingResult.HasUserProfileReportDependencies.ToString();
				}
				warnings = Warning.ProcessingMessagesToWarningArray(publishingResult.Warnings);
				parameters = publishingResult.Parameters;
				dataSources = publishingResult.DataSources;
				dataSets = publishingResult.SharedDataSets;
				pageProperties = publishingResult.PageProperties;
				dataCacheHash = publishingResult.DataSetsHash;
			}
			catch (Exception)
			{
				intermediateSnapshot.DeleteSnapshotAndChunks();
				throw;
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001E268 File Offset: 0x0001C468
		public static void ResolveTemporaryDataSource(DataSourceInfo dataSourceInfo, DataSourceInfoCollection originalDataSources)
		{
			DataSourceInfo byOriginalName = originalDataSources.GetByOriginalName(dataSourceInfo.OriginalName);
			if (byOriginalName != null)
			{
				dataSourceInfo.ID = byOriginalName.ID;
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001E294 File Offset: 0x0001C494
		public DataSourceInfo CheckDataSourcePublishingCallback(string itemPath, out Guid catalogItemId)
		{
			byte[] array;
			ItemType itemType;
			DataSourceInfo dataSourceInfo = ProcessingPublishing.CheckDataSourcePublishingCallback(this, itemPath, out catalogItemId, out array, out itemType);
			if (dataSourceInfo == null)
			{
				return null;
			}
			if (!this.SecMgr.CheckAccess(itemType, array, CommonOperation.ReadProperties, this.CatalogToExternal(itemPath)))
			{
				return null;
			}
			return dataSourceInfo;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001E2D0 File Offset: 0x0001C4D0
		private DataSourceInfo NoAccessCheckDataSourcePublishingCallback(string itemPath, out Guid catalogItemId)
		{
			byte[] array;
			ItemType itemType;
			return ProcessingPublishing.CheckDataSourcePublishingCallback(this, itemPath, out catalogItemId, out array, out itemType);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001E2EC File Offset: 0x0001C4EC
		private bool CheckDataSetPublishingCallback(string itemPath, out Guid catalogItemId)
		{
			byte[] array;
			return this.InternalCheckDataSetPublishingCallback(itemPath, out catalogItemId, out array) && this.SecMgr.CheckAccess(ItemType.DataSet, array, CommonOperation.ReadProperties, this.CatalogToExternal(itemPath));
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001E324 File Offset: 0x0001C524
		private bool NoAccessCheckDataSetPublishingCallback(string itemPath, out Guid catalogItemId)
		{
			byte[] array;
			return this.InternalCheckDataSetPublishingCallback(itemPath, out catalogItemId, out array);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001E33C File Offset: 0x0001C53C
		private bool InternalCheckDataSetPublishingCallback(string catalogPath, out Guid catalogItemId, out byte[] secDesc)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.SharedDataset);
			ExternalItemPath externalItemPath = this.CatalogToExternal(catalogPath);
			catalogItemId = Guid.Empty;
			secDesc = null;
			try
			{
				this.ServiceHelper.SyncToRSCatalog(externalItemPath);
			}
			catch (ItemNotFoundException)
			{
				return false;
			}
			catch (AccessDeniedException)
			{
			}
			CatalogItemContext catalogItemContext = new CatalogItemContext(this);
			if (!catalogItemContext.SetPath(externalItemPath))
			{
				return false;
			}
			RSTrace.CatalogTrace.Assert(!catalogItemContext.ItemPath.IsEditSession, "!dataSetContext.ItemPath.IsEditSession");
			ItemType itemType;
			return this.Storage.ObjectExists(catalogItemContext.ItemPath, out itemType, out catalogItemId, out secDesc) && itemType == ItemType.DataSet;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001E3F4 File Offset: 0x0001C5F4
		private void ResolveTemporaryDataSet(DataSetInfo dataSetInfo, DataSetInfoCollection originalDataSets)
		{
			DataSetInfo byName = originalDataSets.GetByName(dataSetInfo.DataSetName);
			if (byName != null)
			{
				dataSetInfo.ID = byName.ID;
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001E420 File Offset: 0x0001C620
		private UserProfileState GetUserProfileState(JobType type)
		{
			UserProfileState userProfileState = UserProfileState.None;
			if (Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext.Type != JobTypeEnum.System || Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext.SubType == JobSubTypeEnum.Subscription)
			{
				userProfileState = UserProfileState.Both;
			}
			return userProfileState;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001E44C File Offset: 0x0001C64C
		public void AddDataSources(Guid itemID, DataSourceInfoCollection dataSources, string editSessionID)
		{
			foreach (object obj in dataSources)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				Guid guid;
				ItemType itemType;
				byte[] array;
				this.Storage.AddDataSource(itemID, Guid.Empty, dataSourceInfo, this, editSessionID, out guid, out itemType, out array);
				if (dataSourceInfo.ReferenceByPath)
				{
					if (guid == Guid.Empty)
					{
						throw new ItemNotFoundException(dataSourceInfo.DataSourceReference);
					}
					if (itemType != ItemType.DataSource && itemType != ItemType.Model)
					{
						throw new WrongItemTypeException(dataSourceInfo.DataSourceReference);
					}
					if (!this.SecMgr.CheckAccess(itemType, array, CommonOperation.ReadProperties, this.CatalogToExternal(dataSourceInfo.DataSourceReference)))
					{
						throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
					}
				}
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001E51C File Offset: 0x0001C71C
		public void AddDataSets(Guid itemID, DataSetInfoCollection dataSets, string editSessionID)
		{
			foreach (DataSetInfo dataSetInfo in dataSets)
			{
				Guid guid;
				byte[] array;
				this.Storage.AddDataSet(itemID, dataSetInfo, this, editSessionID, out guid, out array);
				if (dataSetInfo.IsValidReference() && dataSetInfo.LinkedSharedDataSetID == Guid.Empty)
				{
					if (guid == Guid.Empty)
					{
						throw new ItemNotFoundException(dataSetInfo.AbsolutePath);
					}
					if (!this.SecMgr.CheckAccess(ItemType.DataSet, array, CommonOperation.ReadProperties, this.CatalogToExternal(dataSetInfo.AbsolutePath)))
					{
						throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
					}
				}
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001E5D0 File Offset: 0x0001C7D0
		private byte[] GetInternalResource(ExternalItemPath resourcePath, out string mimeType)
		{
			byte[] array = null;
			mimeType = null;
			try
			{
				GetResourceContentsAction getResourceContentsAction = this.GetResourceContentsAction;
				getResourceContentsAction.ActionParameters.ItemPath = resourcePath.Value;
				getResourceContentsAction.PerformActionNow();
				mimeType = getResourceContentsAction.ActionParameters.MimeType;
				array = getResourceContentsAction.ActionParameters.Content;
			}
			catch (ItemNotFoundException)
			{
			}
			catch (AccessDeniedException)
			{
			}
			return array;
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001E640 File Offset: 0x0001C840
		public string PhysicalMyReportsPath
		{
			get
			{
				if (this.m_physicalMyReportsPath == null)
				{
					this.m_physicalMyReportsPath = Global.AllUsersFolderPathSlash + this.UserNameToFolderName(this.UserName) + CatalogItemNameUtility.PathSeparatorString + Global.PhysicalMyReportsName;
				}
				return this.m_physicalMyReportsPath;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001E676 File Offset: 0x0001C876
		public string PhysicalMyReportsPathSlash
		{
			get
			{
				if (this.m_physicalMyReportsPathSlash == null)
				{
					this.m_physicalMyReportsPathSlash = this.PhysicalMyReportsPath + CatalogItemNameUtility.PathSeparatorString;
				}
				return this.m_physicalMyReportsPathSlash;
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001E69C File Offset: 0x0001C89C
		public string PathToInternal(string source)
		{
			if (!this.MyReportsEnabled)
			{
				return source;
			}
			if (Localization.CatalogCultureCompare(source, Global.VirtualMyReportsPath) == 0)
			{
				if (this.RequestInspector.IsAnonymous())
				{
					throw new ItemNotFoundException(source);
				}
				this.EnsureMyReportsExists();
				return this.PhysicalMyReportsPath;
			}
			else
			{
				if (!StringSupport.StartsWith(source, Global.VirtualMyReportsPathSlash, true, Localization.CatalogCulture))
				{
					return source;
				}
				if (this.RequestInspector.IsAnonymous())
				{
					throw new ItemNotFoundException(source);
				}
				return this.PhysicalMyReportsPathSlash + source.Substring(Global.VirtualMyReportsPathSlash.Length);
			}
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001E72C File Offset: 0x0001C92C
		public string PathToExternal(string source)
		{
			if (!this.MyReportsEnabled)
			{
				return null;
			}
			if (string.Compare(source, this.PhysicalMyReportsPath, true, Localization.CatalogCulture) == 0)
			{
				return Global.VirtualMyReportsPath;
			}
			if (StringSupport.StartsWith(source, this.PhysicalMyReportsPathSlash, true, Localization.CatalogCulture))
			{
				return Global.VirtualMyReportsPath + CatalogItemNameUtility.PathSeparatorString + source.Substring(this.PhysicalMyReportsPathSlash.Length);
			}
			return null;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001E793 File Offset: 0x0001C993
		public void SetExternalRoot(string path)
		{
			this.ServiceHelper.SetExternalRoot(path);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001E7A1 File Offset: 0x0001C9A1
		public void SetExternalRoot(CatalogItemPath path)
		{
			this.SetExternalRoot(path, 0);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001E7AB File Offset: 0x0001C9AB
		public void SetExternalRoot(CatalogItemPath path, int zone)
		{
			this.ServiceHelper.SetExternalRoot(path, zone);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001E7BA File Offset: 0x0001C9BA
		public Uri GetExternalRoot()
		{
			return this.ServiceHelper.GetExternalRoot();
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001E7C7 File Offset: 0x0001C9C7
		public int GetExternalRootZone(ExternalItemPath path)
		{
			return this.ServiceHelper.GetExternalRootZone(path);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001E7D5 File Offset: 0x0001C9D5
		public ExternalItemPath CatalogToExternal(string source)
		{
			return this.CatalogToExternal(source, false);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001E7DF File Offset: 0x0001C9DF
		public ExternalItemPath CatalogToExternal(string source, bool noThrow)
		{
			if (source == null)
			{
				return null;
			}
			return new ExternalItemPath(this.ServiceHelper.CatalogToExternal(source, noThrow));
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001E7F8 File Offset: 0x0001C9F8
		public ExternalItemPath CatalogToExternal(CatalogItemPath source)
		{
			return this.CatalogToExternal(source, false);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001E802 File Offset: 0x0001CA02
		public ExternalItemPath CatalogToExternal(CatalogItemPath source, bool noThrow)
		{
			if (source == null)
			{
				return null;
			}
			return this.CatalogToExternal(source.Value, noThrow);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001E816 File Offset: 0x0001CA16
		public string GetPublicUrl(string url, bool noThrow)
		{
			return this.ServiceHelper.GetPublicUrl(url, noThrow);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001E825 File Offset: 0x0001CA25
		public ExternalItemPath CatalogToExternal(CatalogItemPath source, int externalRootZone)
		{
			return this.CatalogToExternal(source, externalRootZone, false);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001E830 File Offset: 0x0001CA30
		public ExternalItemPath CatalogToExternal(CatalogItemPath source, int externalRootZone, bool noThrow)
		{
			if (source == null)
			{
				return null;
			}
			return new ExternalItemPath(this.ServiceHelper.CatalogToExternal(source.Value, externalRootZone, noThrow));
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001E84F File Offset: 0x0001CA4F
		public string ExternalToCatalog(string source)
		{
			return this.ServiceHelper.ExternalToCatalog(source, false);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001E85E File Offset: 0x0001CA5E
		public string ExternalToCatalog(string source, bool noThrow)
		{
			return this.ServiceHelper.ExternalToCatalog(source, noThrow);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001E86D File Offset: 0x0001CA6D
		public CatalogItemPath ExternalToCatalogItemPath(ExternalItemPath source)
		{
			if (source == null)
			{
				return null;
			}
			return new CatalogItemPath(this.ServiceHelper.ExternalToCatalog(source.Value, false), source.EditSessionID);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001E891 File Offset: 0x0001CA91
		private static bool IsRoot(ExternalItemPath item)
		{
			return ItemPathBase.IsNullOrEmpty(item) || item.Value == "/";
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001E8B0 File Offset: 0x0001CAB0
		public void EnsureAllowedToEditItem(ItemType itemType, byte[] secDesc, ExternalItemPath itemPath, string itemName)
		{
			if (this.MyReportsEnabled && Localization.CatalogCultureCompare(itemName, Global.VirtualMyReportsName) == 0 && itemPath.Value.ToLower().StartsWith(Global.AllUsersFolderPath.ToLower()))
			{
				throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
			}
			this.ServiceHelper.EnsureAllowedToEditproperties(itemType, secDesc, itemPath);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001E90B File Offset: 0x0001CB0B
		public void EnsureAllowedAsSubitem(ItemType parentType, ItemType childType, byte[] secDesc, ExternalItemPath parent, string item)
		{
			if (this.MyReportsEnabled && RSService.IsRoot(parent) && Localization.CatalogCultureCompare(item, Global.VirtualMyReportsName) == 0)
			{
				throw new ItemAlreadyExistsException(item);
			}
			this.ServiceHelper.EnsureAllowedAsSubitem(parentType, childType, secDesc, parent, item);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001E946 File Offset: 0x0001CB46
		public static void EnsureItemType(ItemType actualType, string path, params ItemType[] expectedTypes)
		{
			if (Array.IndexOf<ItemType>(expectedTypes, actualType) < 0)
			{
				throw new WrongItemTypeException(path);
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001E959 File Offset: 0x0001CB59
		public static void EnsureItemTypeIsReport(ItemType actualType, string path)
		{
			RSService.EnsureItemType(actualType, path, new ItemType[]
			{
				ItemType.LinkedReport,
				ItemType.Report
			});
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001E970 File Offset: 0x0001CB70
		public static void EnsureItemTypeIsReportOrDataSet(ItemType actualType, string path)
		{
			RSService.EnsureItemType(actualType, path, new ItemType[]
			{
				ItemType.Report,
				ItemType.LinkedReport,
				ItemType.DataSet
			});
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001E98A File Offset: 0x0001CB8A
		public void EnsureValidMimeType(string mimeType)
		{
			if (mimeType == null)
			{
				return;
			}
			if (mimeType.Length > 260)
			{
				throw new InvalidParameterException("MimeType");
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001E9A8 File Offset: 0x0001CBA8
		public bool IsTrustedFileType(string fileName)
		{
			IEnumerable<string> enumerable = (from x in this.GetOnConfigurationInfo("TrustedFileFormat").Split(new char[] { ',' })
				select x.Trim() into x
				where !string.IsNullOrEmpty(x)
				select x).ToArray<string>();
			string extension = Path.GetExtension(fileName);
			if (!string.IsNullOrEmpty(extension))
			{
				extension = extension.Substring(1);
			}
			return enumerable.Any((string x) => x.Equals(extension, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001EA60 File Offset: 0x0001CC60
		public bool IsTrustedContentType(string contentType)
		{
			string onConfigurationInfo = this.GetOnConfigurationInfo("RestrictedResourceMimeTypeForUpload");
			return this.VerifyValidMimeType(contentType, onConfigurationInfo);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001EA84 File Offset: 0x0001CC84
		public bool IsCommentAttachment(string parentPath, ItemType childType, string subType)
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(this, parentPath, "parent");
			Microsoft.ReportingServices.Library.CatalogItem catalogItem = this.CatalogItemFactory.GetCatalogItem(catalogItemContext);
			return (catalogItem.ThisItemType.Equals(ItemType.Report) || catalogItem.ThisItemType.Equals(ItemType.LinkedReport) || catalogItem.ThisItemType.Equals(ItemType.PowerBIReport) || catalogItem.ThisItemType.Equals(ItemType.ExcelWorkbook)) && childType.Equals(ItemType.Resource);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001EB38 File Offset: 0x0001CD38
		public void ThrowIfExcelFileExtensionChanged(string itemName, string origItemPath)
		{
			string extension = Path.GetExtension(itemName);
			string extension2 = Path.GetExtension(origItemPath);
			if (extension.ToLower() != extension2.ToLower())
			{
				throw new ExcelFileExtensionChangeNotAllowedException(RepLibRes.DisallowedExcelFileExtensionChange);
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001EB70 File Offset: 0x0001CD70
		public void ThrowIfInvalidFileFormat(string itemName)
		{
			string onConfigurationInfo = this.GetOnConfigurationInfo("AllowedResourceExtensionsForUpload");
			string extension = Path.GetExtension(itemName);
			if (!this.VerifyValidFileExtension(extension, onConfigurationInfo))
			{
				throw new ResourceFileFormatNotAllowedException(RepLibRes.DisallowedResourceExtensionError(extension));
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001EBA8 File Offset: 0x0001CDA8
		public void ThrowIfResctrictedMimeType(string mimeType)
		{
			string onConfigurationInfo = this.GetOnConfigurationInfo("RestrictedResourceMimeTypeForUpload");
			if (!this.VerifyValidMimeType(mimeType, onConfigurationInfo))
			{
				throw new ResourceMimeTypeNotAllowedException(RepLibRes.DisallowedResourceMimeType(mimeType));
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001EBD8 File Offset: 0x0001CDD8
		private string GetOnConfigurationInfo(string key)
		{
			string text;
			if (this.m_willDisconnectStorage && this.m_connManager != null)
			{
				text = this.Storage.GetOneConfigurationInfo(key);
			}
			else
			{
				try
				{
					this.WillDisconnectStorage();
					text = this.Storage.GetOneConfigurationInfo(key);
				}
				finally
				{
					this.DisconnectStorage();
				}
			}
			return text;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001EC34 File Offset: 0x0001CE34
		private bool VerifyValidFileExtension(string fileExtension, string allowedFileFormats)
		{
			if (allowedFileFormats.Contains("*.*"))
			{
				return true;
			}
			string[] array = (from x in allowedFileFormats.Replace("*", string.Empty).Split(new char[] { ',' })
				select x.Trim()).ToArray<string>();
			return fileExtension != null && array.Contains(fileExtension, StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001ECAC File Offset: 0x0001CEAC
		private bool VerifyValidMimeType(string mimeType, string restrictedMimeTypes)
		{
			if (string.IsNullOrEmpty(mimeType))
			{
				return true;
			}
			return !(from x in restrictedMimeTypes.Split(new char[] { ',' })
				select x.Trim()).Contains(mimeType, StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001ED08 File Offset: 0x0001CF08
		public void EnsureCachingIsEnabled(CatalogItemContext itemContext)
		{
			bool flag;
			ExpirationDefinition expirationDefinition;
			this.ExecCacheDb.GetCacheOptions(itemContext.CatalogItemPath, out flag, out expirationDefinition);
			if (!flag)
			{
				throw new CachingNotEnabledException(itemContext.OriginalItemPath.Value);
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001ED40 File Offset: 0x0001CF40
		public Microsoft.ReportingServices.Library.CatalogItem EnsureCacheRefreshPlanIsAllowed(string itemPath)
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(this, itemPath, "item");
			Microsoft.ReportingServices.Library.CatalogItem catalogItem = this.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.LoadProperties();
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.LinkedReport,
				ItemType.DataSet,
				ItemType.PowerBIReport
			});
			catalogItem.ThrowIfNoAccess(ReportOperation.ReadPolicy);
			catalogItem.ThrowIfNoAccess(ReportOperation.UpdatePolicy);
			if (catalogItem.ThisItemType != ItemType.PowerBIReport)
			{
				BaseExecutableCatalogItem baseExecutableCatalogItem = catalogItem as BaseExecutableCatalogItem;
				BaseReportCatalogItem baseReportCatalogItem = catalogItem as BaseReportCatalogItem;
				this.EnsureCachingIsEnabled(catalogItemContext);
				baseExecutableCatalogItem.ThrowIfNotGoodForUnattended(false);
				if (baseReportCatalogItem != null)
				{
					baseReportCatalogItem.ThrowIfNotSubscribableByProperties(false);
				}
			}
			else
			{
				bool flag = false;
				bool.TryParse(catalogItem.Properties["ModelRefreshAllowed"], out flag);
				if (!flag)
				{
					throw new InvalidDataSourceCredentialSettingException();
				}
			}
			return catalogItem;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001EDEC File Offset: 0x0001CFEC
		public string UserNameToFolderName(string user)
		{
			char[] array = new char[] { '&', '@', '$', ' ' };
			if (user.IndexOfAny(array) >= 0)
			{
				user = user.Replace(" ", "[ ]");
				user = user.Replace("&", "[amp]");
				user = user.Replace("@", "[at]");
				user = user.Replace("$", "[dollar]");
			}
			return user.Replace("\\", " ");
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001EE70 File Offset: 0x0001D070
		public void EnsureMyReportsExists()
		{
			if (this.m_willDisconnectStorage)
			{
				this.InternalEnsureMyReportsExists();
				return;
			}
			try
			{
				this.WillDisconnectStorage();
				this.InternalEnsureMyReportsExists();
			}
			catch (Exception)
			{
				this.AbortTransaction();
				throw;
			}
			finally
			{
				this.DisconnectStorage();
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001EEC8 File Offset: 0x0001D0C8
		private void InternalEnsureMyReportsExists()
		{
			string text = this.UserNameToFolderName(this.UserName);
			ExternalItemPath externalItemPath = new ExternalItemPath(Global.AllUsersFolderPathSlash + text);
			ExternalItemPath externalItemPath2 = new ExternalItemPath(externalItemPath.Value + "/" + Global.PhysicalMyReportsName);
			if (!this.Storage.ObjectExists(externalItemPath2))
			{
				ItemType itemType;
				Guid guid;
				byte[] array;
				if (!this.Storage.ObjectExists(new ExternalItemPath(Global.AllUsersFolderPath), out itemType, out guid, out array))
				{
					throw new InternalCatalogException("EnsureMyReportsExists: Users folder not found when My Reports accessed.");
				}
				DateTime now = DateTime.Now;
				Guid guid2 = this.Storage.CreateObject(Guid.NewGuid(), text, externalItemPath.NativeCatalogItemPath, new ExternalItemPath(Global.AllUsersFolderPath), guid, ItemType.Folder, null, Guid.Empty, Guid.Empty, null, null, null, Global.GetSystemSid(this.UserContext.AuthenticationType), Global.GetSystemUserName(this.UserContext.AuthenticationType), now, now, null);
				if (guid2 == Guid.Empty)
				{
					ItemType itemType2;
					byte[] array2;
					if (!this.Storage.ObjectExists(externalItemPath, out itemType2, out guid2, out array2))
					{
						throw new InternalCatalogException("EnsureMyReportsExists: Creation of users home folder failed but folder doesn't exist.");
					}
					RSService.EnsureItemType(itemType2, externalItemPath.Value, new ItemType[] { ItemType.Folder });
				}
				bool flag = this.Storage.CreateObject(Guid.NewGuid(), Global.PhysicalMyReportsName, externalItemPath2.NativeCatalogItemPath, externalItemPath, guid2, ItemType.Folder, null, Guid.Empty, Guid.Empty, null, null, null, Global.GetSystemSid(this.UserContext.AuthenticationType), Global.GetSystemUserName(this.UserContext.AuthenticationType), now, now, null) != Guid.Empty;
				if (!flag)
				{
					ItemType itemType3;
					if (!this.Storage.ObjectExists(externalItemPath2, out itemType3))
					{
						throw new InternalCatalogException("EnsureMyReportsExists: Users home folder does not exist but can not be created.");
					}
					RSService.EnsureItemType(itemType3, externalItemPath2.Value, new ItemType[] { ItemType.Folder });
				}
				if (flag)
				{
					StringWriter stringWriter = new StringWriter(Localization.CatalogCulture);
					XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
					xmlTextWriter.WriteStartElement("Policies");
					xmlTextWriter.WriteStartElement("Policy");
					xmlTextWriter.WriteElementString("GroupUserName", this.UserName);
					xmlTextWriter.WriteStartElement("Roles");
					xmlTextWriter.WriteStartElement("Role");
					xmlTextWriter.WriteElementString("Name", this.MyReportsRole);
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.Flush();
					this.SecMgr.SetCatalogItemPolicies(externalItemPath2, ItemType.Folder, stringWriter.ToString());
					this.Tracer.Trace(TraceLevel.Info, "EnsureMyReportsExists: created My Reports folder '{0}'", new object[] { externalItemPath2.Value });
				}
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001F138 File Offset: 0x0001D338
		public static bool IsRequestFromMemberOfAdministratorsGroup()
		{
			return Microsoft.ReportingServices.Diagnostics.ProcessingContext.ReqContext.IsMemberOfWindowsAdminGroup;
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0001F144 File Offset: 0x0001D344
		public int SystemSnapshotLimit
		{
			get
			{
				if (this.m_SystemSnapshotLimitParam == null)
				{
					this.m_SystemSnapshotLimitParam = new IntParameter(CachedSystemProperties.Instance, Global.m_Tracer, "SystemSnapshotLimit", CachedSystemProperties.Instance.GetParameter("SystemSnapshotLimit"), -1, "");
					this.m_SystemSnapshotLimitParam.TraceSuccess = false;
					this.m_SystemSnapshotLimitParam.MinValue = -2;
					this.m_SystemSnapshotLimit = this.m_SystemSnapshotLimitParam.Value;
					if (this.m_SystemSnapshotLimit == -2)
					{
						this.m_SystemSnapshotLimit = -1;
					}
				}
				return this.m_SystemSnapshotLimit;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001F1CC File Offset: 0x0001D3CC
		public bool MyReportsEnabled
		{
			get
			{
				if (this.m_MyReportsEnabledParam == null)
				{
					this.m_MyReportsEnabledParam = new BooleanParameter(CachedSystemProperties.Instance, Global.m_Tracer, "EnableMyReports", CachedSystemProperties.Instance.GetParameter("EnableMyReports"), false, "");
					this.m_MyReportsEnabledParam.TraceSuccess = false;
					this.m_MyReportsEnabled = this.m_MyReportsEnabledParam.Value;
				}
				return this.m_MyReportsEnabled;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0001F233 File Offset: 0x0001D433
		public string MyReportsRole
		{
			get
			{
				return CachedSystemProperties.Instance.GetParameter("MyReportsRole");
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x000110FC File Offset: 0x0000F2FC
		private RSTrace Tracer
		{
			get
			{
				return Global.m_Tracer;
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001F244 File Offset: 0x0001D444
		public void SetDatabaseConnectionSettings(ConnectionTransactionType transactionType, IsolationLevel defaultIsolationLevel)
		{
			if (this.m_connManager == null)
			{
				this.m_connManager = new ConnectionManager(transactionType, defaultIsolationLevel);
				if (this.m_willDisconnectStorage)
				{
					this.m_connManager.WillDisconnectStorage();
					return;
				}
			}
			else
			{
				IsolationLevel isolationLevel = this.m_connManager.GetIsolationLevel();
				ConnectionTransactionType transactionType2 = this.m_connManager.GetTransactionType();
				Global.m_Tracer.Assert(defaultIsolationLevel == isolationLevel, "Attempting to set connection isolation level more then once");
				Global.m_Tracer.Assert(transactionType == transactionType2, "Attempting to set connection Transaction Type more then once");
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001F2B8 File Offset: 0x0001D4B8
		private void ConnectDatabase()
		{
			if (this.m_connManager == null)
			{
				if (this.m_useBatchConnectionManager)
				{
					this.m_connManager = ConnectionManager.CreateBatchConnection();
				}
				else
				{
					this.m_connManager = new ConnectionManager();
				}
				if (this.m_willDisconnectStorage)
				{
					this.m_connManager.WillDisconnectStorage();
				}
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001F2F8 File Offset: 0x0001D4F8
		public void EnsureValidDatabase()
		{
			try
			{
				this.WillDisconnectStorage();
				this.ConnectDatabase();
				this.m_connManager.VerifyConnection(true);
			}
			finally
			{
				this.DisconnectStorage();
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001F338 File Offset: 0x0001D538
		public void EnsureSupportedEditionForSharePoint()
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Sharepoint);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001F34B File Offset: 0x0001D54B
		public IRSStorage GetScopedStorage()
		{
			if (this.m_willDisconnectStorage)
			{
				return this.Storage;
			}
			return null;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0001F35D File Offset: 0x0001D55D
		public IDBInterface Storage
		{
			get
			{
				if (!this.m_willDisconnectStorage)
				{
					throw new InternalCatalogException("Storage access outside guarded area.");
				}
				if (this.m_Storage == null)
				{
					this.m_Storage = this.ServiceHelper.GetStorageInternal();
					this.ConnectStorage(this.m_Storage);
				}
				return this.m_Storage;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0001F39D File Offset: 0x0001D59D
		public RSPropertyProvider PropertyProvider
		{
			get
			{
				if (this.m_propertyProvider == null)
				{
					this.m_propertyProvider = this.ServiceHelper.GetPropertyProviderInternal();
				}
				return this.m_propertyProvider;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0001F3BE File Offset: 0x0001D5BE
		public CatalogItemFactory CatalogItemFactory
		{
			get
			{
				if (this.m_catalogItemFactory == null)
				{
					this.m_catalogItemFactory = new CatalogItemFactory(this);
				}
				return this.m_catalogItemFactory;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0001F3DA File Offset: 0x0001D5DA
		public RSServiceHelper ServiceHelper
		{
			get
			{
				if (this.m_serviceHelper == null)
				{
					this.m_serviceHelper = new RSServiceHelper(this);
				}
				return this.m_serviceHelper;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0001F3F6 File Offset: 0x0001D5F6
		public ScheduleCoordinator SchedCoordinator
		{
			get
			{
				if (this.m_schedCoordinator == null)
				{
					this.m_schedCoordinator = new ScheduleCoordinator(this);
					this.ConnectStorage(this.m_schedCoordinator);
				}
				return this.m_schedCoordinator;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0001F41E File Offset: 0x0001D61E
		public Security SecMgr
		{
			get
			{
				if (this.m_Security == null)
				{
					this.m_Security = this.ServiceHelper.GetSecurityManager(this.UserContext, this.m_checkAccess);
					this.ConnectStorage(this.m_Security);
				}
				return this.m_Security;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0001F457 File Offset: 0x0001D657
		public ReportExecutionCacheDb ExecCacheDb
		{
			get
			{
				if (this.m_execCacheDb == null)
				{
					this.m_execCacheDb = this.ServiceHelper.GetExecutionCacheDbInternal();
					this.ConnectStorage(this.m_execCacheDb);
				}
				return this.m_execCacheDb;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0001F484 File Offset: 0x0001D684
		public SubscriptionManager SubscriptionManager
		{
			get
			{
				if (this.m_subscriptionManager == null)
				{
					this.m_subscriptionManager = new SubscriptionManager(this);
				}
				return this.m_subscriptionManager;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0001F4A0 File Offset: 0x0001D6A0
		public EventManager EventManager
		{
			get
			{
				if (this.m_eventManager == null)
				{
					this.m_eventManager = new EventManager();
					this.ConnectStorage(this.m_eventManager);
				}
				return this.m_eventManager;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0001F4C7 File Offset: 0x0001D6C7
		public SystemResourceManager SystemResourceManager
		{
			get
			{
				if (this.m_systemResourceManager == null)
				{
					this.m_systemResourceManager = new SystemResourceManager(this);
				}
				return this.m_systemResourceManager;
			}
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001F4E3 File Offset: 0x0001D6E3
		public void ConnectStorage(Storage storage)
		{
			if (this.m_connManager == null)
			{
				this.ConnectDatabase();
			}
			storage.ConnectionManager = this.m_connManager;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001F4FF File Offset: 0x0001D6FF
		public void WillDisconnectStorage()
		{
			this.m_willDisconnectStorage = true;
			if (this.m_connManager != null)
			{
				this.m_connManager.WillDisconnectStorage();
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001F51C File Offset: 0x0001D71C
		public void WillDisconnectStorage(ConnectionManager connectionManager)
		{
			Global.m_Tracer.Assert(connectionManager != null, "Attempting to set connection manager to null");
			Global.m_Tracer.Assert(this.m_connManager == null, "Attempting to set connection level more then once");
			this.m_connManager = connectionManager;
			this.WillDisconnectStorage();
			this.m_Storage = this.ServiceHelper.GetStorageInternal();
			this.ConnectStorage(this.m_Storage);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001F580 File Offset: 0x0001D780
		public void DisconnectStorage()
		{
			try
			{
				if (this.m_connManager != null)
				{
					try
					{
						this.m_connManager.CommitTransaction();
					}
					finally
					{
						this.m_connManager.DisconnectStorage();
						this.m_connManager = null;
					}
				}
			}
			finally
			{
				this.m_willDisconnectStorage = false;
				this.ResetStorageClasses();
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001F5E4 File Offset: 0x0001D7E4
		private void ResetStorageClasses()
		{
			this.m_Storage = null;
			this.m_schedCoordinator = null;
			this.m_subscriptionManager = null;
			this.m_Security = null;
			this.m_execCacheDb = null;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001F60C File Offset: 0x0001D80C
		public void AbortTransaction()
		{
			try
			{
				if (this.m_connManager != null)
				{
					try
					{
						this.m_connManager.AbortTransaction();
					}
					finally
					{
						this.m_connManager.DisconnectStorage();
						this.m_connManager = null;
					}
				}
			}
			finally
			{
				this.ResetStorageClasses();
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001F668 File Offset: 0x0001D868
		public string UserName
		{
			get
			{
				if (this.UserContext == null)
				{
					throw new InternalCatalogException("No user context");
				}
				if (this.m_checkAccess)
				{
					this.Tracer.Assert(this.UserContext.IsInitialized, "User Context is not initialized");
				}
				if (!string.IsNullOrEmpty(this.UserContext.UserName))
				{
					return this.UserContext.UserName;
				}
				return null;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001F6CA File Offset: 0x0001D8CA
		public UserContext UserContext
		{
			get
			{
				return this.m_userContext;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0001F6D4 File Offset: 0x0001D8D4
		public StreamManager StreamManager
		{
			get
			{
				if (this.m_streamManagerWorkspaces.Count > 0)
				{
					return this.m_streamManagerWorkspaces.Peek().StreamManager;
				}
				return null;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001F704 File Offset: 0x0001D904
		public RdlChunkMapper RdlChunkMapper
		{
			get
			{
				return this.m_rdlChunkMapper;
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001F70C File Offset: 0x0001D90C
		public void InitializeRdlChunkMapping()
		{
			RSTrace.CatalogTrace.Assert(this.m_rdlChunkMapper == null, "already initialized");
			this.m_rdlChunkMapper = new RdlChunkMapper();
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001F731 File Offset: 0x0001D931
		public void ResetRdlChunkMapping()
		{
			this.m_rdlChunkMapper = null;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0001F73A File Offset: 0x0001D93A
		public bool StoreRdlChunks
		{
			get
			{
				return this.m_rdlChunkMapper != null;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0001F745 File Offset: 0x0001D945
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x0001F74D File Offset: 0x0001D94D
		public bool AllowEditSessionItemPaths
		{
			get
			{
				return this.m_allowEditSessionItemPaths;
			}
			set
			{
				this.m_allowEditSessionItemPaths = value;
			}
		}

		// Token: 0x0400035B RID: 859
		private IRSRequestInspector m_requestInspector;

		// Token: 0x0400035C RID: 860
		private IntParameter m_SystemReportTimeoutParam;

		// Token: 0x0400035D RID: 861
		private int m_SystemReportTimeout = 1800;

		// Token: 0x0400035E RID: 862
		private GetSystemPropertiesAction m_getSystemPropertiesAction;

		// Token: 0x0400035F RID: 863
		private GetReportServerConfigInfoAction m_getReportServerConfigInfoAction;

		// Token: 0x04000360 RID: 864
		private SetSystemPropertiesAction m_setSystemPropertiesAction;

		// Token: 0x04000361 RID: 865
		private GetUserSettingsAction m_getUserSettingsAction;

		// Token: 0x04000362 RID: 866
		private SetUserSettingsAction m_setUserSettingsAction;

		// Token: 0x04000363 RID: 867
		private GetItemTypeAction m_getItemTypeAction;

		// Token: 0x04000364 RID: 868
		private DeleteItemAction m_deleteItemAction;

		// Token: 0x04000365 RID: 869
		private MoveItemAction m_moveItemAction;

		// Token: 0x04000366 RID: 870
		private GetPropertiesAction m_getPropertiesAction;

		// Token: 0x04000367 RID: 871
		private SetPropertiesAction m_setPropertiesAction;

		// Token: 0x04000368 RID: 872
		private CreateReportEditSessionAction m_createEditSession;

		// Token: 0x04000369 RID: 873
		private CreateComponentAction m_createComponentAction;

		// Token: 0x0400036A RID: 874
		private GetComponentDefinitionAction m_getComponentDefinitionAction;

		// Token: 0x0400036B RID: 875
		private SetComponentDefinitionAction m_setComponentDefinitionAction;

		// Token: 0x0400036C RID: 876
		private ListChildrenAction m_listChildrenAction;

		// Token: 0x0400036D RID: 877
		private ListFavoriteableItemsAction m_listFavoriteableItemsAction;

		// Token: 0x0400036E RID: 878
		private ListParentsAction m_listParentsAction;

		// Token: 0x0400036F RID: 879
		private AddToFavoritesAction m_addToFavoritesAction;

		// Token: 0x04000370 RID: 880
		private RemoveFromFavoritesAction m_removeFromFavoritesAction;

		// Token: 0x04000371 RID: 881
		private CreateFolderAction m_createFolderAction;

		// Token: 0x04000372 RID: 882
		private CreateDataSetAction m_createDataSetAction;

		// Token: 0x04000373 RID: 883
		private GetDataSetDefinitionAction m_getDataSetDefinitionAction;

		// Token: 0x04000374 RID: 884
		private SetDataSetDefinitionAction m_setDataSetDefinitionAction;

		// Token: 0x04000375 RID: 885
		private GetDataSetItemReferencesAction m_getDataSetItemReferencesAction;

		// Token: 0x04000376 RID: 886
		private SetDataSetItemReferencesAction m_setDataSetItemReferencesAction;

		// Token: 0x04000377 RID: 887
		private GetDataSetParametersAction m_getDataSetParametersAction;

		// Token: 0x04000378 RID: 888
		private CreateReportAction m_createReportAction;

		// Token: 0x04000379 RID: 889
		private CreateRdlxReportAction m_createRdlxReportAction;

		// Token: 0x0400037A RID: 890
		private SetReportDefinitionAction m_setReportDefinitionAction;

		// Token: 0x0400037B RID: 891
		private SetRdlxReportDefinitionAction m_setRdlxReportDefinitionAction;

		// Token: 0x0400037C RID: 892
		private GetReportDefinitionAction m_getReportDefinitionAction;

		// Token: 0x0400037D RID: 893
		private SetReportParametersAction m_setReportParametersAction;

		// Token: 0x0400037E RID: 894
		private GetReportParametersAction m_getReportParametersAction;

		// Token: 0x0400037F RID: 895
		private SetExecutionOptionsAction m_setExeuctionOptionsAction;

		// Token: 0x04000380 RID: 896
		private GetExecutionOptionsAction m_getExeuctionOptionsAction;

		// Token: 0x04000381 RID: 897
		private SetCacheOptionsAction m_setCacheOptionsAction;

		// Token: 0x04000382 RID: 898
		private GetCacheOptionsAction m_getCacheOptionsAction;

		// Token: 0x04000383 RID: 899
		private UpdateExecutionSnapshotAction m_updateSnapshotAction;

		// Token: 0x04000384 RID: 900
		private FlushCacheAction m_flushCacheAction;

		// Token: 0x04000385 RID: 901
		private CreateSnapshotAction m_createSnapshotAction;

		// Token: 0x04000386 RID: 902
		private SetReportHistoryOptionsAction m_setHistoryOptionsAction;

		// Token: 0x04000387 RID: 903
		private GetReportHistoryOptionsAction m_getHistoryOptionsAction;

		// Token: 0x04000388 RID: 904
		private SetSnapshotLimitAction m_setSnapshotLimitAction;

		// Token: 0x04000389 RID: 905
		private GetSnapshotLimitAction m_getSnapshotLimitAction;

		// Token: 0x0400038A RID: 906
		private ListHistoryAction m_listHistoryAction;

		// Token: 0x0400038B RID: 907
		private ListHistorySnapshotsAction m_listHistorySnapshotsAction;

		// Token: 0x0400038C RID: 908
		private DeleteSnapshotAction m_deleteSnapshotAction;

		// Token: 0x0400038D RID: 909
		private DeleteHistorySnapshotAction m_deleteHistorySnapshotAction;

		// Token: 0x0400038E RID: 910
		private CreateSubscriptionAction m_createSubscriptionAction;

		// Token: 0x0400038F RID: 911
		private DisableSubscriptionAction m_disableSubscriptionAction;

		// Token: 0x04000390 RID: 912
		private EnableSubscriptionAction m_enableSubscriptionAction;

		// Token: 0x04000391 RID: 913
		private DeleteSubscriptionAction m_deleteSubscriptionAction;

		// Token: 0x04000392 RID: 914
		private SetSubscriptionPropertiesAction m_setSubscriptionPropertiesAction;

		// Token: 0x04000393 RID: 915
		private GetSubscriptionPropertiesAction m_getSubscriptionPropertiesAction;

		// Token: 0x04000394 RID: 916
		private ListSubscriptionsAction m_listSubscriptionsAction;

		// Token: 0x04000395 RID: 917
		private ChangeSubscriptionOwnerAction m_changeSubscriptionOwnerAction;

		// Token: 0x04000396 RID: 918
		private CreateCacheRefreshPlanAction m_createCacheRefreshPlanAction;

		// Token: 0x04000397 RID: 919
		private SetCacheRefreshPlanPropertiesAction m_setCacheRefreshPlanPropertiesAction;

		// Token: 0x04000398 RID: 920
		private GetCacheRefreshPlanPropertiesAction m_getCacheRefreshPlanPropertiesAction;

		// Token: 0x04000399 RID: 921
		private CreateScheduleAction m_createScheduleAction;

		// Token: 0x0400039A RID: 922
		private DeleteScheduleAction m_deleteScheduleAction;

		// Token: 0x0400039B RID: 923
		private ListSchedulesAction m_listSchedulesAction;

		// Token: 0x0400039C RID: 924
		private GetSchedulePropertiesAction m_getSchedulePropertiesAction;

		// Token: 0x0400039D RID: 925
		private SetSchedulePropertiesAction m_setSchedulePropertiesAction;

		// Token: 0x0400039E RID: 926
		private PauseScheduleAction m_pauseScheduleAction;

		// Token: 0x0400039F RID: 927
		private ResumeScheduleAction m_resumeScheduleAction;

		// Token: 0x040003A0 RID: 928
		private ListScheduledReportsAction m_listScheduledReportsAction;

		// Token: 0x040003A1 RID: 929
		private GetReportItemReferencesAction m_getReportItemReferencesAction;

		// Token: 0x040003A2 RID: 930
		private SetReportItemReferencesAction m_setReportItemReferencesAction;

		// Token: 0x040003A3 RID: 931
		private CreateLinkedReportAction m_createLinkedReportAction;

		// Token: 0x040003A4 RID: 932
		private GetReportLinkAction m_getReportLinkAction;

		// Token: 0x040003A5 RID: 933
		private SetReportLinkAction m_setReportLinkAction;

		// Token: 0x040003A6 RID: 934
		private CreateResourceAction m_createResourceAction;

		// Token: 0x040003A7 RID: 935
		private GetResourceContentsAction m_getResourceContentsAction;

		// Token: 0x040003A8 RID: 936
		private SetResourceContentsAction m_setResourceContentsAction;

		// Token: 0x040003A9 RID: 937
		private CreateKpiAction m_createKpiAction;

		// Token: 0x040003AA RID: 938
		private GetKpiAction m_getKpiAction;

		// Token: 0x040003AB RID: 939
		private UploadPowerBIReportAction m_UploadPowerBiReportAction;

		// Token: 0x040003AC RID: 940
		private GetPowerBIReportContentsAction m_getPowerBIReportContentsAction;

		// Token: 0x040003AD RID: 941
		private SetPowerBIReportContentsAction m_setPowerBIReportContentsAction;

		// Token: 0x040003AE RID: 942
		private CreateExcelWorkbookAction m_createExcelAction;

		// Token: 0x040003AF RID: 943
		private GetExcelWorkbookContentsAction m_getExcelWorkbookContentsAction;

		// Token: 0x040003B0 RID: 944
		private SetExcelWorkbookContentsAction m_setExcelWorkbookContentsAction;

		// Token: 0x040003B1 RID: 945
		private readonly object m_modelSync = new object();

		// Token: 0x040003B2 RID: 946
		private ListEventsAction m_listEventsAction;

		// Token: 0x040003B3 RID: 947
		private FireEventAction m_fireEventAction;

		// Token: 0x040003B4 RID: 948
		private ListTasksAction m_listTasksAction;

		// Token: 0x040003B5 RID: 949
		private ListRolesAction m_listRolesAction;

		// Token: 0x040003B6 RID: 950
		private CreateRoleAction m_createRoleAction;

		// Token: 0x040003B7 RID: 951
		private DeleteRoleAction m_deleteRoleAction;

		// Token: 0x040003B8 RID: 952
		private GetRolePropertiesAction m_getRolePropertiesAction;

		// Token: 0x040003B9 RID: 953
		private SetRolePropertiesAction m_setRolePropertiesAction;

		// Token: 0x040003BA RID: 954
		private GetPoliciesAction m_getPoliciesAction;

		// Token: 0x040003BB RID: 955
		private GetSystemPoliciesAction m_getSystemPoliciesAction;

		// Token: 0x040003BC RID: 956
		private SetPoliciesAction m_setPoliciesAction;

		// Token: 0x040003BD RID: 957
		private SetSystemPoliciesAction m_setSystemPoliciesAction;

		// Token: 0x040003BE RID: 958
		private DeletePoliciesAction m_deletePoliciesAction;

		// Token: 0x040003BF RID: 959
		private GetPermissionsAction m_getPermissionsAction;

		// Token: 0x040003C0 RID: 960
		private GetSystemPermissionsAction m_getSystemPermissionsAction;

		// Token: 0x040003C1 RID: 961
		private CreateDataSourceAction m_createDataSourceAction;

		// Token: 0x040003C2 RID: 962
		private ChangeDataSourceStateAction m_changeDataSourceStateAction;

		// Token: 0x040003C3 RID: 963
		private GetDataSourceContentsAction m_getDataSourceContentsAction;

		// Token: 0x040003C4 RID: 964
		private GetItemDataSourcesAction m_getItemDataSourcesAction;

		// Token: 0x040003C5 RID: 965
		private GetItemDataSourcePromptsAction m_getItemDataSourcePromptsAction;

		// Token: 0x040003C6 RID: 966
		private SetDataSourceContentsAction m_setDataSourceContentsAction;

		// Token: 0x040003C7 RID: 967
		private SetItemDataSourcesAction m_setItemDataSourcesAction;

		// Token: 0x040003C8 RID: 968
		private ListDependentItemsAction m_listDependentItemsAction;

		// Token: 0x040003C9 RID: 969
		private TestConnectForItemDataSourceAction m_testConnectForItemDataSourceAction;

		// Token: 0x040003CA RID: 970
		private TestConnectForDataSourceDefinitionAction m_testConnectForDataSourceDefinitionAction;

		// Token: 0x040003CB RID: 971
		private ListRunningJobsAction m_listRunningJobsAction;

		// Token: 0x040003CC RID: 972
		private CancelJobAction m_cancelJobAction;

		// Token: 0x040003CD RID: 973
		private CreateModelAction m_createModelAction;

		// Token: 0x040003CE RID: 974
		private GetModelDefinitionAction m_getModelDefinitionAction;

		// Token: 0x040003CF RID: 975
		private SetModelDefinitionAction m_setModelDefinitionAction;

		// Token: 0x040003D0 RID: 976
		private ListModelPerspectivesAction m_listModelPerspectivesAction;

		// Token: 0x040003D1 RID: 977
		private ListModelItemChildrenAction m_listModelItemChildrenAction;

		// Token: 0x040003D2 RID: 978
		private GetUserModelAction m_getUserModelAction;

		// Token: 0x040003D3 RID: 979
		private GetModelItemPermissionsAction m_getModelItemPermissionsAction;

		// Token: 0x040003D4 RID: 980
		private GetModelItemPoliciesAction m_getModelItemPoliciesAction;

		// Token: 0x040003D5 RID: 981
		private SetModelItemPoliciesAction m_setModelItemPoliciesAction;

		// Token: 0x040003D6 RID: 982
		private RemoveAllModelItemPoliciesAction m_removeAllModelItemPoliciesAction;

		// Token: 0x040003D7 RID: 983
		private SetDrillthroughReportsAction m_setDrillthroughReportsAction;

		// Token: 0x040003D8 RID: 984
		private GetModelItemReferencesAction m_getModelItemReferencesAction;

		// Token: 0x040003D9 RID: 985
		private SetModelItemReferencesAction m_setModelItemReferencesAction;

		// Token: 0x040003DA RID: 986
		private GenerateModelAction m_generateModelAction;

		// Token: 0x040003DB RID: 987
		private RegenerateModelAction m_regenerateModelAction;

		// Token: 0x040003DC RID: 988
		private string m_physicalMyReportsPath;

		// Token: 0x040003DD RID: 989
		private string m_physicalMyReportsPathSlash;

		// Token: 0x040003DE RID: 990
		private bool m_checkAccess = true;

		// Token: 0x040003DF RID: 991
		private ConnectionManager m_connManager;

		// Token: 0x040003E0 RID: 992
		private bool m_useBatchConnectionManager;

		// Token: 0x040003E1 RID: 993
		private DBInterface m_Storage;

		// Token: 0x040003E2 RID: 994
		private RSServiceHelper m_serviceHelper;

		// Token: 0x040003E3 RID: 995
		private ScheduleCoordinator m_schedCoordinator;

		// Token: 0x040003E4 RID: 996
		private Security m_Security;

		// Token: 0x040003E5 RID: 997
		private ReportExecutionCacheDb m_execCacheDb;

		// Token: 0x040003E6 RID: 998
		private SubscriptionManager m_subscriptionManager;

		// Token: 0x040003E7 RID: 999
		private EventManager m_eventManager;

		// Token: 0x040003E8 RID: 1000
		private SystemResourceManager m_systemResourceManager;

		// Token: 0x040003E9 RID: 1001
		private WorkspaceManager<RSService.StreamManagerDisposalBinding> m_streamManagerWorkspaces = new WorkspaceManager<RSService.StreamManagerDisposalBinding>();

		// Token: 0x040003EA RID: 1002
		private IntParameter m_SystemSnapshotLimitParam;

		// Token: 0x040003EB RID: 1003
		private int m_SystemSnapshotLimit = -1;

		// Token: 0x040003EC RID: 1004
		private RdlChunkMapper m_rdlChunkMapper;

		// Token: 0x040003ED RID: 1005
		private bool m_allowEditSessionItemPaths;

		// Token: 0x040003EE RID: 1006
		private BooleanParameter m_MyReportsEnabledParam;

		// Token: 0x040003EF RID: 1007
		private bool m_MyReportsEnabled;

		// Token: 0x040003F0 RID: 1008
		private RSPropertyProvider m_propertyProvider;

		// Token: 0x040003F1 RID: 1009
		private CatalogItemFactory m_catalogItemFactory;

		// Token: 0x040003F2 RID: 1010
		private bool m_willDisconnectStorage;

		// Token: 0x040003F3 RID: 1011
		private readonly UserContext m_userContext;

		// Token: 0x0200045C RID: 1116
		private struct WorkspaceContext : IDisposable
		{
			// Token: 0x0600233C RID: 9020 RVA: 0x00083E24 File Offset: 0x00082024
			public WorkspaceContext(RSService service)
			{
				this.m_service = service;
			}

			// Token: 0x0600233D RID: 9021 RVA: 0x00083E2D File Offset: 0x0008202D
			void IDisposable.Dispose()
			{
				this.m_service.m_streamManagerWorkspaces.Pop();
			}

			// Token: 0x04000FA8 RID: 4008
			private readonly RSService m_service;
		}

		// Token: 0x0200045D RID: 1117
		private struct StreamManagerDisposalBinding : IDisposable
		{
			// Token: 0x0600233E RID: 9022 RVA: 0x00083E3F File Offset: 0x0008203F
			public StreamManagerDisposalBinding(StreamManager manager)
			{
				this.m_manager = manager;
			}

			// Token: 0x17000A66 RID: 2662
			// (get) Token: 0x0600233F RID: 9023 RVA: 0x00083E48 File Offset: 0x00082048
			public StreamManager StreamManager
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_manager;
				}
			}

			// Token: 0x06002340 RID: 9024 RVA: 0x00083E50 File Offset: 0x00082050
			void IDisposable.Dispose()
			{
				RSTrace.CatalogTrace.Assert(this.StreamManager.StreamFactory != null, "StreamManager.StreamFactory");
				this.StreamManager.StreamFactory.Dispose();
			}

			// Token: 0x04000FA9 RID: 4009
			private readonly StreamManager m_manager;
		}
	}
}
