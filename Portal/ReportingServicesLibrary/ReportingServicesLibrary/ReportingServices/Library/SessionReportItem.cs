using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000280 RID: 640
	internal sealed class SessionReportItem
	{
		// Token: 0x060016E4 RID: 5860 RVA: 0x0005CF6C File Offset: 0x0005B16C
		internal SessionReportItem(DatabaseSessionStorage sessionDB, string sessionId, UserContext userCtx, ReportItem rptItem, DateTime creationTime, bool hasInteractivity)
		{
			this.m_sessionDB = sessionDB;
			this.m_sessionId = sessionId;
			this.m_rptItem = rptItem;
			this.m_userContext = userCtx;
			this.m_sessionCreationTime = creationTime;
			this.m_hasInteractivity = hasInteractivity;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0005CFF0 File Offset: 0x0005B1F0
		public SessionReportItem(DatabaseSessionStorage sessionDB, string sessionId, UserContext userCtx, ExternalItemPath itemPath, string userParams)
		{
			this.m_sessionDB = sessionDB;
			this.m_sessionId = sessionId;
			this.m_rptItem = new ReportItem(itemPath);
			this.m_rptItem.UserParams = userParams;
			this.m_userContext = userCtx;
			this.m_sessionCreationTime = DateTime.Now;
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x0005D080 File Offset: 0x0005B280
		public static SessionReportItem Load(DatabaseSessionStorage sessionStorage, string sessionId, ExternalItemPath reportPath, DateTime snapshotDate, UserContext userContext, string userParams, string imageName, DatasourceCredentialsCollection dataSourceCred)
		{
			SessionReportItem sessionData = sessionStorage.GetSessionData(sessionId, userContext);
			if (sessionData == null)
			{
				RSTrace.SessionTrace.Trace(TraceLevel.Info, "LoadSnapshot: Item with session: {0}, reportPath: {1}, userName: {2} not found in the database", new object[] { sessionId, reportPath, userContext.UserName });
				return null;
			}
			RSTrace.SessionTrace.Trace(TraceLevel.Verbose, "Found session for sessionid {0}", new object[] { sessionId });
			if (reportPath != null && Localization.CatalogCultureCompare(sessionData.Report.ItemPath.FullEditSessionIdentifier, reportPath.FullEditSessionIdentifier) != 0)
			{
				if (RSTrace.SessionTrace.TraceVerbose)
				{
					RSTrace.SessionTrace.Trace(TraceLevel.Verbose, "Report path doesn't match - starting new session");
				}
				return null;
			}
			if (reportPath != null && Globals.ToSnapshotDateFormat(sessionData.Report.HistoryDate) != Globals.ToSnapshotDateFormat(snapshotDate))
			{
				if (RSTrace.SessionTrace.TraceVerbose)
				{
					RSTrace.SessionTrace.Trace(TraceLevel.Verbose, "Report history id doesn't match - starting new session");
				}
				return null;
			}
			if (!sessionData.Report.EffectiveParams.SameReportParameters(userParams))
			{
				if (RSTrace.SessionTrace.TraceVerbose)
				{
					RSTrace.SessionTrace.Trace(TraceLevel.Verbose, "Parameter don't match - starting new session");
				}
				return null;
			}
			if (sessionData.Datasources != null && !sessionData.Datasources.CredentialsAreSame(dataSourceCred, true, DataProtection.Instance))
			{
				return null;
			}
			return sessionData;
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0005D1AC File Offset: 0x0005B3AC
		public static string MakeCacheKey(string sessionId, string reportPath, DateTime snapshotDate, string userName, string userParams)
		{
			RSTrace.SessionTrace.Assert(sessionId != null && reportPath != null && userName != null);
			if (sessionId == null || reportPath == null || userName == null)
			{
				throw new InternalCatalogException("Invalid paramters; cannot create cache key");
			}
			string text = string.Concat(new string[] { userName, ";", sessionId, ";", reportPath });
			if (snapshotDate != DateTime.MinValue)
			{
				text = text + ";" + snapshotDate.ToString(DateTimeFormatInfo.InvariantInfo);
			}
			if (userParams != null)
			{
				text = text + ";" + userParams;
			}
			return text;
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0005D244 File Offset: 0x0005B444
		public static string MakeCacheParentKey(string sessionId, string reportPath, DateTime snapshotDate, string userName)
		{
			RSTrace.SessionTrace.Assert(sessionId != null && reportPath != null && userName != null);
			if (sessionId == null || reportPath == null || userName == null)
			{
				throw new InternalCatalogException("Invalid paramters; cannot create cache key");
			}
			string text = string.Concat(new string[] { userName, ";", sessionId, ";", reportPath });
			if (snapshotDate != DateTime.MinValue)
			{
				text = text + ";" + snapshotDate.ToString(DateTimeFormatInfo.InvariantInfo);
			}
			return text;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0005D2CC File Offset: 0x0005B4CC
		public static string MakeCookieKey(string reportPath, DateTime snapshotDate)
		{
			if (reportPath == null)
			{
				return null;
			}
			string text = null;
			if (snapshotDate != DateTime.MinValue)
			{
				text = snapshotDate.ToString(DateTimeFormatInfo.InvariantInfo);
			}
			text += reportPath;
			if (text.Length == 0)
			{
				text = "/";
			}
			return text;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0005D314 File Offset: 0x0005B514
		internal static CatalogItemContext CreateContextFromSession(RSService service, ClientRequest session)
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(service);
			ExternalItemPath itemPath = session.SessionReport.Report.ItemPath;
			ItemPathOptions itemPathOptions = ItemPathOptions.Validate | ItemPathOptions.AllowEditSessionSyntax;
			if (!catalogItemContext.SetPath(itemPath.FullEditSessionIdentifier, itemPathOptions))
			{
				throw new InternalCatalogException("Can not validate report path from session");
			}
			catalogItemContext.SetReportDefinitionPath(session.SessionReport.Report.ReportDefinitionPath);
			if (itemPath.IsEditSession && !CatalogItemNameUtility.IsWebUrl(catalogItemContext.ItemPath.Value))
			{
				service.EnsureSecurityZone(null);
			}
			else
			{
				service.EnsureSecurityZone(catalogItemContext.ItemPath.Value);
			}
			catalogItemContext.RSRequestParameters.SetCatalogParameters(null);
			catalogItemContext.RSRequestParameters.SessionId = session.SessionID;
			if (session.SessionReport.Report.HistoryDate != DateTime.MinValue)
			{
				catalogItemContext.RSRequestParameters.SnapshotParamValue = Globals.ToSnapshotDateFormat(session.SessionReport.Report.HistoryDate);
			}
			catalogItemContext.RSRequestParameters.SetReportParameters(null);
			return catalogItemContext;
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0005D406 File Offset: 0x0005B606
		internal static PaginationMode PaginationModeFromShort(short value)
		{
			switch (value)
			{
			case 0:
				return PaginationMode.TotalPages;
			case 1:
				return PaginationMode.Estimate;
			case 2:
				return PaginationMode.Progressive;
			default:
				throw new InternalCatalogException("Unknown pagination mode value: " + value);
			}
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0005D437 File Offset: 0x0005B637
		internal static short PaginationModeToShort(PaginationMode paginationMode)
		{
			switch (paginationMode)
			{
			case PaginationMode.Progressive:
				return 2;
			case PaginationMode.TotalPages:
				return 0;
			case PaginationMode.Estimate:
				return 1;
			default:
				throw new InternalCatalogException("Unknown pagination mode enum: " + paginationMode.ToString());
			}
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0005D470 File Offset: 0x0005B670
		public RSStream GetImage(string imageId)
		{
			if (this.Report.SnapshotData != null)
			{
				Stream stream = null;
				MemoryThenFileStream memoryThenFileStream = new MemoryThenFileStream();
				string text;
				try
				{
					try
					{
						stream = this.Report.SnapshotData.GetChunk(imageId, ReportProcessing.ReportChunkTypes.Image, out text);
						if (stream == null)
						{
							stream = this.Report.SnapshotData.GetChunk(imageId, ReportProcessing.ReportChunkTypes.StaticImage, out text);
						}
					}
					catch (VersionMismatchException)
					{
						SnapshotConverter.ConvertFromV1(null, this.Report.SnapshotData, true);
						stream = this.Report.SnapshotData.GetChunk(imageId, ReportProcessing.ReportChunkTypes.Image, out text);
					}
					if (stream == null)
					{
						return null;
					}
					StreamSupport.CopyStreamUsingBuffer(stream, memoryThenFileStream, Global.ResponseBufferSizeBytes);
					memoryThenFileStream.Seek(0L, SeekOrigin.Begin);
				}
				finally
				{
					if (stream != null)
					{
						stream.Close();
					}
				}
				return new RSStream(memoryThenFileStream, false)
				{
					MimeType = text,
					Name = imageId
				};
			}
			return null;
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x0005D550 File Offset: 0x0005B750
		public void Delete()
		{
			this.m_sessionDB.DeleteSession(this, false);
			RSTrace.SessionTrace.Trace(TraceLevel.Verbose, "Report has been deleted from session. Key={0}.", new object[] { this.ParentKey });
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x0005D57E File Offset: 0x0005B77E
		internal void Save(SessionReportItem.SaveAction saveFlags)
		{
			this.Save(saveFlags, false);
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x0005D588 File Offset: 0x0005B788
		internal void Save(SessionReportItem.SaveAction saveFlags, bool forceSync)
		{
			this.SaveInteractivityStatus();
			this.m_saveFlags = saveFlags;
			if (saveFlags == SessionReportItem.SaveAction.SaveNone)
			{
				return;
			}
			bool flag = !forceSync && Global.SaveSessionAsync;
			this.m_sessionDB.SetSessionData(this, flag);
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x0005D5BF File Offset: 0x0005B7BF
		private void SaveInteractivityStatus()
		{
			if (this.m_processingResult != null)
			{
				this.m_hasInteractivity = this.m_processingResult.HasInteractivity;
				return;
			}
			this.m_hasInteractivity = false;
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x0005D5E2 File Offset: 0x0005B7E2
		internal void AddNew()
		{
			DatabaseSessionStorage.AddNewSession(this);
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0005D5EA File Offset: 0x0005B7EA
		internal void SetCredentials()
		{
			DatabaseSessionStorage.SetCredentials(this);
			this.Report.SnapshotData = null;
			this.ExecutionDateTime = DateTime.MinValue;
			this.PageCount = 0;
			this.HasDocumentMap = false;
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x0005D617 File Offset: 0x0005B817
		internal void SetParameters()
		{
			DatabaseSessionStorage.SetParameters(this);
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0005D61F File Offset: 0x0005B81F
		internal void ClearSnapshot()
		{
			DatabaseSessionStorage.ClearSnapshot(this);
			this.Report.SnapshotData = null;
			this.ExecutionDateTime = DateTime.MinValue;
			this.PageCount = 0;
			this.HasDocumentMap = false;
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0005D64C File Offset: 0x0005B84C
		private void StartSessionTransaction()
		{
			if (this.m_ConnectionManager == null)
			{
				this.m_ConnectionManager = new ConnectionManager();
				this.m_ConnectionManager.WillDisconnectStorage();
				this.AddThreadUsingThisSession();
				this.m_ConnectionManager.BeginTransaction();
			}
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0005D680 File Offset: 0x0005B880
		internal void WriteLockSession(bool bCheckVersion)
		{
			this.StartSessionTransaction();
			try
			{
				DatabaseSessionStorage.Current.WriteLockSession(this, bCheckVersion);
			}
			catch (Exception)
			{
				this.m_ConnectionManager.DisconnectStorage();
				throw;
			}
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0005D6C0 File Offset: 0x0005B8C0
		internal void WriteLockSession()
		{
			this.WriteLockSession(false);
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x0005D6C9 File Offset: 0x0005B8C9
		internal void AddThreadUsingThisSession()
		{
			Interlocked.Increment(ref this.m_threadsUsingThisObject);
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0005D6D8 File Offset: 0x0005B8D8
		internal void ThreadNoLongerUseThisSession()
		{
			if (Interlocked.Decrement(ref this.m_threadsUsingThisObject) == 0 && this.m_ConnectionManager != null)
			{
				try
				{
					this.m_ConnectionManager.CommitTransaction();
				}
				finally
				{
					ConnectionManager connectionManager = this.m_ConnectionManager;
					this.m_ConnectionManager = null;
					connectionManager.DisconnectStorage();
				}
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x0005D72C File Offset: 0x0005B92C
		public string Key
		{
			get
			{
				RSTrace.SessionTrace.Assert(this.m_sessionId != null && this.m_rptItem != null && this.m_userContext != null && this.m_userContext.UserName != null);
				return SessionReportItem.MakeCacheKey(this.m_sessionId, this.m_rptItem.ItemPath.FullEditSessionIdentifier, this.m_rptItem.CreationDate, this.m_userContext.UserName, this.m_rptItem.UserParams);
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x0005D7A8 File Offset: 0x0005B9A8
		public string ParentKey
		{
			get
			{
				RSTrace.SessionTrace.Assert(this.m_sessionId != null && this.m_rptItem != null && this.m_userContext != null && this.m_userContext.UserName != null);
				return SessionReportItem.MakeCacheParentKey(this.m_sessionId, this.m_rptItem.ItemPath.FullEditSessionIdentifier, this.m_rptItem.CreationDate, this.m_userContext.UserName);
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x0005D819 File Offset: 0x0005BA19
		// (set) Token: 0x060016FE RID: 5886 RVA: 0x0005D821 File Offset: 0x0005BA21
		internal ReportItem Report
		{
			get
			{
				return this.m_rptItem;
			}
			set
			{
				this.m_rptItem = value;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x0005D82A File Offset: 0x0005BA2A
		public string SessionId
		{
			get
			{
				return this.m_sessionId;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x0005D832 File Offset: 0x0005BA32
		public UserContext UserContext
		{
			get
			{
				return this.m_userContext;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x0005D83A File Offset: 0x0005BA3A
		// (set) Token: 0x06001702 RID: 5890 RVA: 0x0005D842 File Offset: 0x0005BA42
		public EventInformation EventInfo
		{
			get
			{
				return this.m_eventInfo;
			}
			set
			{
				this.m_eventInfo = value;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x0005D84B File Offset: 0x0005BA4B
		// (set) Token: 0x06001704 RID: 5892 RVA: 0x0005D853 File Offset: 0x0005BA53
		public RuntimeDataSourceInfoCollection Datasources
		{
			get
			{
				return this.m_dataSources;
			}
			set
			{
				this.m_dataSources = value;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x0005D85C File Offset: 0x0005BA5C
		// (set) Token: 0x06001706 RID: 5894 RVA: 0x0005D864 File Offset: 0x0005BA64
		public RuntimeDataSetInfoCollection DataSets
		{
			get
			{
				return this.m_dataSets;
			}
			set
			{
				this.m_dataSets = value;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x0005D86D File Offset: 0x0005BA6D
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x0005D875 File Offset: 0x0005BA75
		public OnDemandProcessingResult ProcessingResult
		{
			get
			{
				return this.m_processingResult;
			}
			set
			{
				this.m_processingResult = value;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x0005D87E File Offset: 0x0005BA7E
		public DateTime SessionCreationTime
		{
			get
			{
				return this.m_sessionCreationTime;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x0600170A RID: 5898 RVA: 0x0005D886 File Offset: 0x0005BA86
		public bool HasInteractivity
		{
			get
			{
				return this.m_hasInteractivity;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x0005D88E File Offset: 0x0005BA8E
		// (set) Token: 0x0600170C RID: 5900 RVA: 0x0005D896 File Offset: 0x0005BA96
		public DateTime ExecutionDateTime
		{
			get
			{
				return this.m_executionDateTime;
			}
			set
			{
				this.m_executionDateTime = value;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x0005D89F File Offset: 0x0005BA9F
		// (set) Token: 0x0600170E RID: 5902 RVA: 0x0005D8A7 File Offset: 0x0005BAA7
		public DateTime ExpirationDateTime
		{
			get
			{
				return this.m_expirationDateTime;
			}
			set
			{
				this.m_expirationDateTime = value;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x0005D8B0 File Offset: 0x0005BAB0
		// (set) Token: 0x06001710 RID: 5904 RVA: 0x0005D8B8 File Offset: 0x0005BAB8
		public DateTime SnapshotExpirationDateTime
		{
			get
			{
				return this.m_snapshotExpirationDateTime;
			}
			set
			{
				this.m_snapshotExpirationDateTime = value;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x0005D8C1 File Offset: 0x0005BAC1
		// (set) Token: 0x06001712 RID: 5906 RVA: 0x0005D8C9 File Offset: 0x0005BAC9
		public int PageCount
		{
			get
			{
				return this.m_pageCount;
			}
			set
			{
				this.m_pageCount = value;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x0005D8D2 File Offset: 0x0005BAD2
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x0005D8DA File Offset: 0x0005BADA
		public bool HasDocumentMap
		{
			get
			{
				return this.m_hasDocumentMap;
			}
			set
			{
				this.m_hasDocumentMap = value;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x0005D8E3 File Offset: 0x0005BAE3
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x0005D8EB File Offset: 0x0005BAEB
		public PaginationMode PaginationMode
		{
			get
			{
				return this.m_paginationMode;
			}
			set
			{
				this.m_paginationMode = value;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x0005D8F4 File Offset: 0x0005BAF4
		// (set) Token: 0x06001718 RID: 5912 RVA: 0x0005D8FC File Offset: 0x0005BAFC
		public bool AwaitingFirstExecution
		{
			get
			{
				return this.m_awaitingFirstExecution;
			}
			set
			{
				this.m_awaitingFirstExecution = value;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x0005D905 File Offset: 0x0005BB05
		// (set) Token: 0x0600171A RID: 5914 RVA: 0x0005D90D File Offset: 0x0005BB0D
		public bool IsDBInstance
		{
			get
			{
				return this.m_IsDBInstance;
			}
			set
			{
				this.m_IsDBInstance = value;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x0005D916 File Offset: 0x0005BB16
		// (set) Token: 0x0600171C RID: 5916 RVA: 0x0005D91E File Offset: 0x0005BB1E
		public int Timeout
		{
			get
			{
				return this.m_timeout;
			}
			set
			{
				this.m_timeout = value;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x0005D927 File Offset: 0x0005BB27
		// (set) Token: 0x0600171E RID: 5918 RVA: 0x0005D92F File Offset: 0x0005BB2F
		public int AutoRefreshSeconds
		{
			get
			{
				return this.m_autoRefreshSeconds;
			}
			set
			{
				this.m_autoRefreshSeconds = value;
			}
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0005D938 File Offset: 0x0005BB38
		internal void SetAutoRefresh(OnDemandProcessingResult processingResult)
		{
			if (processingResult != null)
			{
				this.AutoRefreshSeconds = processingResult.AutoRefresh;
				return;
			}
			this.AutoRefreshSeconds = 0;
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x0005D951 File Offset: 0x0005BB51
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x0005D959 File Offset: 0x0005BB59
		public bool IsNewExecution
		{
			get
			{
				return this.m_isNewExecution;
			}
			set
			{
				this.m_isNewExecution = value;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x0005D962 File Offset: 0x0005BB62
		public bool HasSnapshotData
		{
			get
			{
				return this.Report != null && this.Report.SnapshotData != null;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0005D97C File Offset: 0x0005BB7C
		public DatabaseSessionStorage SessionDB
		{
			get
			{
				return this.m_sessionDB;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x0005D984 File Offset: 0x0005BB84
		// (set) Token: 0x06001725 RID: 5925 RVA: 0x0005D98C File Offset: 0x0005BB8C
		internal ConnectionManager SessionConnectionManager
		{
			get
			{
				return this.m_ConnectionManager;
			}
			set
			{
				this.m_ConnectionManager = value;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x0005D995 File Offset: 0x0005BB95
		internal SessionReportItem.SaveAction SaveFlags
		{
			get
			{
				return this.m_saveFlags;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x0005D99D File Offset: 0x0005BB9D
		// (set) Token: 0x06001728 RID: 5928 RVA: 0x0005D9A5 File Offset: 0x0005BBA5
		internal bool FoundInCache
		{
			get
			{
				return this.m_foundInCache;
			}
			set
			{
				this.m_foundInCache = value;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x0005D9AE File Offset: 0x0005BBAE
		// (set) Token: 0x0600172A RID: 5930 RVA: 0x0005D9B6 File Offset: 0x0005BBB6
		internal CatalogItemPath SitePath
		{
			get
			{
				return this.m_sitePath;
			}
			set
			{
				this.m_sitePath = value;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x0005D9BF File Offset: 0x0005BBBF
		// (set) Token: 0x0600172C RID: 5932 RVA: 0x0005D9C7 File Offset: 0x0005BBC7
		internal int SiteZone
		{
			get
			{
				return this.m_siteZone;
			}
			set
			{
				this.m_siteZone = value;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x0005D9D0 File Offset: 0x0005BBD0
		// (set) Token: 0x0600172E RID: 5934 RVA: 0x0005D9D8 File Offset: 0x0005BBD8
		internal int LockVersion
		{
			get
			{
				return this.m_LockVersion;
			}
			set
			{
				this.m_LockVersion = value;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x0005D9E1 File Offset: 0x0005BBE1
		internal bool IsAdhocReport
		{
			get
			{
				RSTrace.CatalogTrace.Assert(this.Report != null, "Report != null");
				return this.Report.CompiledDefinition != null;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x0005DA09 File Offset: 0x0005BC09
		// (set) Token: 0x06001731 RID: 5937 RVA: 0x0005DA11 File Offset: 0x0005BC11
		internal PageProperties PageProperties
		{
			get
			{
				return this.m_pageProperties;
			}
			set
			{
				this.m_pageProperties = value;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x0005DA1A File Offset: 0x0005BC1A
		// (set) Token: 0x06001733 RID: 5939 RVA: 0x0005DA4B File Offset: 0x0005BC4B
		internal ISnapshotTransactionFactory SnapshotTransactionFactory
		{
			get
			{
				if (this.m_snapshotTransactionFactory == null)
				{
					RSTrace.SessionTrace.Assert(this.HasSnapshotData, "HasSnapshotData");
					return this.Report.SnapshotData;
				}
				return this.m_snapshotTransactionFactory;
			}
			set
			{
				this.m_snapshotTransactionFactory = value;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06001734 RID: 5940 RVA: 0x0005DA54 File Offset: 0x0005BC54
		// (remove) Token: 0x06001735 RID: 5941 RVA: 0x0005DA8C File Offset: 0x0005BC8C
		internal event SessionReportItem.SessionPersistedHandler Persisted;

		// Token: 0x06001736 RID: 5942 RVA: 0x0005DAC4 File Offset: 0x0005BCC4
		private void OnPersisted(SessionPersistedEventArgs e)
		{
			SessionReportItem.SessionPersistedHandler persisted = this.Persisted;
			if (persisted != null)
			{
				if (RSTrace.SessionTrace.TraceVerbose)
				{
					RSTrace.SessionTrace.Trace(TraceLevel.Verbose, "Firing session persisted events...");
				}
				persisted(this, e);
			}
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0005DAFF File Offset: 0x0005BCFF
		private SessionPersistedEventArgs BuildSessionPersistedEventArgs(ConnectionManager connectionManager)
		{
			return new SessionPersistedEventArgs(connectionManager);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0005DB08 File Offset: 0x0005BD08
		internal void FirePersistedEvent(ConnectionManager connectionManager)
		{
			SessionPersistedEventArgs sessionPersistedEventArgs = this.BuildSessionPersistedEventArgs(connectionManager);
			this.OnPersisted(sessionPersistedEventArgs);
		}

		// Token: 0x04000852 RID: 2130
		private int m_threadsUsingThisObject;

		// Token: 0x04000854 RID: 2132
		private DatabaseSessionStorage m_sessionDB;

		// Token: 0x04000855 RID: 2133
		private string m_sessionId;

		// Token: 0x04000856 RID: 2134
		private ReportItem m_rptItem;

		// Token: 0x04000857 RID: 2135
		private EventInformation m_eventInfo;

		// Token: 0x04000858 RID: 2136
		private RuntimeDataSourceInfoCollection m_dataSources;

		// Token: 0x04000859 RID: 2137
		private RuntimeDataSetInfoCollection m_dataSets;

		// Token: 0x0400085A RID: 2138
		private UserContext m_userContext;

		// Token: 0x0400085B RID: 2139
		private DateTime m_sessionCreationTime = DateTime.MinValue;

		// Token: 0x0400085C RID: 2140
		private bool m_hasInteractivity = true;

		// Token: 0x0400085D RID: 2141
		private DateTime m_executionDateTime = DateTime.MinValue;

		// Token: 0x0400085E RID: 2142
		private DateTime m_expirationDateTime = DateTime.MinValue;

		// Token: 0x0400085F RID: 2143
		private DateTime m_snapshotExpirationDateTime = DateTime.MinValue;

		// Token: 0x04000860 RID: 2144
		private int m_pageCount;

		// Token: 0x04000861 RID: 2145
		private bool m_hasDocumentMap;

		// Token: 0x04000862 RID: 2146
		private bool m_awaitingFirstExecution = true;

		// Token: 0x04000863 RID: 2147
		private bool m_isNewExecution;

		// Token: 0x04000864 RID: 2148
		private OnDemandProcessingResult m_processingResult;

		// Token: 0x04000865 RID: 2149
		private int m_timeout;

		// Token: 0x04000866 RID: 2150
		private int m_autoRefreshSeconds;

		// Token: 0x04000867 RID: 2151
		private bool m_foundInCache;

		// Token: 0x04000868 RID: 2152
		private CatalogItemPath m_sitePath;

		// Token: 0x04000869 RID: 2153
		private int m_siteZone;

		// Token: 0x0400086A RID: 2154
		private ConnectionManager m_ConnectionManager;

		// Token: 0x0400086B RID: 2155
		private SessionReportItem.SaveAction m_saveFlags;

		// Token: 0x0400086C RID: 2156
		private PageProperties m_pageProperties;

		// Token: 0x0400086D RID: 2157
		private PaginationMode m_paginationMode = PaginationMode.TotalPages;

		// Token: 0x0400086E RID: 2158
		private ISnapshotTransactionFactory m_snapshotTransactionFactory;

		// Token: 0x0400086F RID: 2159
		private bool m_IsDBInstance;

		// Token: 0x04000870 RID: 2160
		private int m_LockVersion;

		// Token: 0x020004C6 RID: 1222
		public enum SaveAction
		{
			// Token: 0x04001107 RID: 4359
			SaveNone,
			// Token: 0x04001108 RID: 4360
			SaveSession,
			// Token: 0x04001109 RID: 4361
			SaveSnapshot
		}

		// Token: 0x020004C7 RID: 1223
		// (Invoke) Token: 0x06002445 RID: 9285
		internal delegate void SessionPersistedHandler(object sender, SessionPersistedEventArgs e);
	}
}
