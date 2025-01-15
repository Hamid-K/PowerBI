using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000204 RID: 516
	internal sealed class SessionStarterAction
	{
		// Token: 0x06001250 RID: 4688 RVA: 0x00041874 File Offset: 0x0003FA74
		internal static SessionStarterAction CreateNew(SessionfulClientRequest session, RSService service, string reportPath, string historyId)
		{
			SessionStarterAction sessionStarterAction = new SessionStarterAction(service);
			try
			{
				if (reportPath == null)
				{
					throw new MissingParameterException("Report");
				}
				sessionStarterAction.m_reportContext = service.ConstructItemContext(reportPath, true);
				sessionStarterAction.m_reportContext.RSRequestParameters.SetCatalogParameters(null);
				sessionStarterAction.m_reportContext.RSRequestParameters.SetReportParameters(string.Empty);
				sessionStarterAction.m_reportContext.RSRequestParameters.SetRenderingParameters(null);
				sessionStarterAction.m_reportContext.RSRequestParameters.SnapshotParamValue = historyId;
				sessionStarterAction.m_userContext = service.UserContext;
				sessionStarterAction.m_session = session;
				sessionStarterAction.CreateNew();
			}
			catch (Exception ex)
			{
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			return sessionStarterAction;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0004192C File Offset: 0x0003FB2C
		internal static SessionStarterAction CreateNew(SessionfulClientRequest session, RSService service)
		{
			SessionStarterAction sessionStarterAction = new SessionStarterAction(service);
			try
			{
				sessionStarterAction.m_reportContext = new CatalogItemContext(service);
				sessionStarterAction.m_reportContext.SetPath(new ExternalItemPath("/"));
				sessionStarterAction.m_reportContext.RSRequestParameters.SetCatalogParameters(null);
				sessionStarterAction.m_reportContext.RSRequestParameters.SetReportParameters(string.Empty);
				sessionStarterAction.m_reportContext.RSRequestParameters.SetRenderingParameters(null);
				sessionStarterAction.m_userContext = service.UserContext;
				sessionStarterAction.m_session = session;
				sessionStarterAction.CreateNew();
			}
			catch (Exception ex)
			{
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			return sessionStarterAction;
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x000419D8 File Offset: 0x0003FBD8
		internal static SessionStarterAction CreateExisting(SessionfulClientRequest session, RSService service)
		{
			SessionStarterAction sessionStarterAction = new SessionStarterAction(service);
			try
			{
				sessionStarterAction.m_userContext = sessionStarterAction.m_service.UserContext;
				sessionStarterAction.m_session = session;
				sessionStarterAction.CreateExisting();
			}
			catch (Exception ex)
			{
				if (ex is RSException)
				{
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			return sessionStarterAction;
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00041A30 File Offset: 0x0003FC30
		private SessionStarterAction(RSService service)
		{
			this.m_service = service;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00041A3F File Offset: 0x0003FC3F
		private void CreateExisting()
		{
			if (this.m_session.SessionID == null)
			{
				throw new MissingSessionIdException();
			}
			this.m_session.InitAsExistingSession(this.m_userContext, this.m_service);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00041A6B File Offset: 0x0003FC6B
		private void CreateNew()
		{
			this.m_session.InitForNewSession(this.m_userContext, this.m_reportContext);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00041A84 File Offset: 0x0003FC84
		internal void WriteSessionId(ExecutionInfo execInfo)
		{
			this.m_session.WriteSessionId(execInfo.ExecutionID);
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x00041A97 File Offset: 0x0003FC97
		internal ClientRequest Session
		{
			get
			{
				return this.m_session;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x00041A9F File Offset: 0x0003FC9F
		internal CatalogItemContext ReportContext
		{
			get
			{
				return this.m_reportContext;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00041AA7 File Offset: 0x0003FCA7
		internal string UserName
		{
			get
			{
				return this.m_userContext.UserName;
			}
		}

		// Token: 0x04000694 RID: 1684
		private RSService m_service;

		// Token: 0x04000695 RID: 1685
		private SessionfulClientRequest m_session;

		// Token: 0x04000696 RID: 1686
		private CatalogItemContext m_reportContext;

		// Token: 0x04000697 RID: 1687
		private UserContext m_userContext;
	}
}
