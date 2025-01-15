using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000202 RID: 514
	internal abstract class SessionfulClientRequest : ClientRequest
	{
		// Token: 0x06001231 RID: 4657
		protected abstract string GetSessionId();

		// Token: 0x06001232 RID: 4658
		internal abstract void DeleteSessionId(string sessionId);

		// Token: 0x06001233 RID: 4659
		internal abstract void WriteSessionId(string sessionId);

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0004146F File Offset: 0x0003F66F
		internal virtual bool ShouldClearSession
		{
			get
			{
				return this.m_imageName == null && this.m_clearSession;
			}
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00041486 File Offset: 0x0003F686
		protected SessionfulClientRequest(DatabaseSessionStorage sessionDB)
		{
			this.m_sessionDB = sessionDB;
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x000414B8 File Offset: 0x0003F6B8
		private void Init(CatalogItemContext context, UserContext userCtx)
		{
			this.m_userContext = userCtx;
			if (context != null)
			{
				this.m_reportSessionPath = context.ItemPath;
				this.m_snapshotDate = Globals.ParseSnapshotDateParameter(context.RSRequestParameters.SnapshotParamValue, true);
				this.m_imageName = context.RSRequestParameters.ImageIDParamValue;
				this.m_userParams = context.RSRequestParameters.ReportParametersXml;
				bool flag = false;
				if (context.RSRequestParameters.ClearSessionParamValue != null)
				{
					bool.TryParse(context.RSRequestParameters.ClearSessionParamValue, out flag);
				}
				this.m_clearSession = flag;
				bool flag2 = true;
				if (context.RSRequestParameters.AllowNewSessionsParamValue != null)
				{
					bool.TryParse(context.RSRequestParameters.AllowNewSessionsParamValue, out flag2);
				}
				this.m_allowNewSessions = flag2;
			}
			this.m_SessionID = this.GetSessionId();
			if (this.m_snapshotDate != DateTime.MinValue)
			{
				this.m_isSnapshot = true;
			}
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00041590 File Offset: 0x0003F790
		internal void InitForRequest(CatalogItemContext context, UserContext userCtx)
		{
			string sessionID = this.m_SessionID;
			this.Init(context, userCtx);
			if (this.m_SessionID == null)
			{
				this.InitNewSession();
			}
			else if (this.m_imageName != null)
			{
				if (!this.LoadFromDB(context))
				{
					throw new StreamNotFoundException(this.m_imageName);
				}
				this.InitAsImageRequest();
			}
			else if (this.ShouldClearSession)
			{
				this.DeleteExistingSession();
				this.InitNewSession();
			}
			else if (!this.LoadFromDB(context))
			{
				if (!this.m_allowNewSessions)
				{
					throw new ExecutionNotFoundException(this.m_SessionID);
				}
				this.InitNewSession();
			}
			else
			{
				this.InitExistingSession(context.PathTranslator);
			}
			context.RSRequestParameters.SessionId = this.m_SessionID;
			this.RedirectRequired |= string.CompareOrdinal(sessionID, this.m_SessionID) != 0;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00041655 File Offset: 0x0003F855
		internal void InitForNewSession(UserContext userCtx, CatalogItemContext context)
		{
			this.Init(context, userCtx);
			this.InitNewSession();
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00041665 File Offset: 0x0003F865
		internal void InitAsExistingSession(UserContext userContext, IPathTranslator pathTranslator)
		{
			this.Init(null, userContext);
			if (!this.LoadFromDB(null))
			{
				throw new ExecutionNotFoundException(this.m_SessionID);
			}
			this.InitExistingSession(pathTranslator);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0004168B File Offset: 0x0003F88B
		private void InitNewSession()
		{
			this.m_IsNew = true;
			this.m_NeedSession = true;
			this.m_SessionID = UrlFriendlyUIDGenerator.Create();
			this.InitSesionReport(this.m_userParams);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x000416B2 File Offset: 0x0003F8B2
		private void InitExistingSession(IPathTranslator pathTranslator)
		{
			this.m_IsNew = false;
			this.m_NeedSession = true;
			if (this.m_crtItem.SitePath != null)
			{
				pathTranslator.SetExternalRoot(this.m_crtItem.SitePath, this.m_crtItem.SiteZone);
			}
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000416EB File Offset: 0x0003F8EB
		internal void InitAsImageRequest()
		{
			this.m_IsNew = false;
			this.m_NeedSession = true;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x000416FC File Offset: 0x0003F8FC
		internal bool LoadFromDB(CatalogItemContext context)
		{
			DatasourceCredentialsCollection datasourceCredentialsCollection = ((context != null) ? context.RSRequestParameters.DatasourcesCred : null);
			this.m_crtItem = SessionReportItem.Load(this.m_sessionDB, this.m_SessionID, this.m_reportSessionPath, this.m_snapshotDate, this.m_userContext, this.m_userParams, this.m_imageName, datasourceCredentialsCollection);
			if (this.m_crtItem != null)
			{
				this.m_dontCache = this.m_crtItem.HasInteractivity;
			}
			return this.m_crtItem != null;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00041774 File Offset: 0x0003F974
		protected void InitSesionReport(string userParams)
		{
			if (this.m_crtItem != null)
			{
				RSTrace.SessionTrace.Assert(false, "Session already associated with a report");
			}
			this.m_crtItem = new SessionReportItem(this.m_sessionDB, this.m_SessionID, this.m_userContext, this.m_reportSessionPath, userParams);
			this.m_crtItem.Report.HistoryDate = this.m_snapshotDate;
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x000417D3 File Offset: 0x0003F9D3
		private void DeleteExistingSession()
		{
			new SessionReportItem(this.m_sessionDB, this.m_SessionID, this.m_userContext, this.m_reportSessionPath, this.m_userParams).Delete();
			this.DeleteSessionId(this.m_SessionID);
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00041809 File Offset: 0x0003FA09
		public override bool IsNew
		{
			get
			{
				return this.m_IsNew;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x00041811 File Offset: 0x0003FA11
		public override string SessionID
		{
			get
			{
				return this.m_SessionID;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x00041819 File Offset: 0x0003FA19
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x00041821 File Offset: 0x0003FA21
		public override bool NeedSession
		{
			get
			{
				return this.m_NeedSession;
			}
			set
			{
				this.m_NeedSession = value;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x00005BEF File Offset: 0x00003DEF
		// (set) Token: 0x06001245 RID: 4677 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public override bool RedirectRequired
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0004182A File Offset: 0x0003FA2A
		// (set) Token: 0x06001247 RID: 4679 RVA: 0x00041832 File Offset: 0x0003FA32
		public override bool DontCache
		{
			get
			{
				return this.m_dontCache;
			}
			set
			{
				this.m_dontCache = value;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x0004183B File Offset: 0x0003FA3B
		// (set) Token: 0x06001249 RID: 4681 RVA: 0x00041843 File Offset: 0x0003FA43
		public override SessionReportItem SessionReport
		{
			get
			{
				return this.m_crtItem;
			}
			set
			{
				this.m_crtItem = value;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x0004184C File Offset: 0x0003FA4C
		protected bool ClearSession
		{
			get
			{
				return this.m_clearSession;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x00041854 File Offset: 0x0003FA54
		protected string ImageName
		{
			get
			{
				return this.m_imageName;
			}
		}

		// Token: 0x04000685 RID: 1669
		protected string m_SessionID;

		// Token: 0x04000686 RID: 1670
		protected bool m_IsNew = true;

		// Token: 0x04000687 RID: 1671
		protected bool m_NeedSession = true;

		// Token: 0x04000688 RID: 1672
		private bool m_dontCache;

		// Token: 0x04000689 RID: 1673
		protected SessionReportItem m_crtItem;

		// Token: 0x0400068A RID: 1674
		protected DatabaseSessionStorage m_sessionDB;

		// Token: 0x0400068B RID: 1675
		protected UserContext m_userContext;

		// Token: 0x0400068C RID: 1676
		protected ExternalItemPath m_reportSessionPath;

		// Token: 0x0400068D RID: 1677
		protected DateTime m_snapshotDate = DateTime.MinValue;

		// Token: 0x0400068E RID: 1678
		private string m_imageName;

		// Token: 0x0400068F RID: 1679
		protected string m_userParams;

		// Token: 0x04000690 RID: 1680
		private bool m_clearSession;

		// Token: 0x04000691 RID: 1681
		private bool m_allowNewSessions = true;

		// Token: 0x04000692 RID: 1682
		protected bool m_isSnapshot;
	}
}
