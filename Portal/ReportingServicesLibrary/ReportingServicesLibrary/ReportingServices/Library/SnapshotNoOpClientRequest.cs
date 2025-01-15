using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000235 RID: 565
	internal sealed class SnapshotNoOpClientRequest : ClientRequest
	{
		// Token: 0x0600148E RID: 5262 RVA: 0x0005008C File Offset: 0x0004E28C
		public SnapshotNoOpClientRequest(UserContext userCtx, ExternalItemPath reportPath)
		{
			this.m_userContext = userCtx;
			this.m_reportPath = reportPath;
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x000500A2 File Offset: 0x0004E2A2
		public override string SessionID
		{
			get
			{
				if (this.m_sessionID == null)
				{
					this.m_sessionID = UrlFriendlyUIDGenerator.Create();
				}
				return this.m_sessionID;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool IsNew
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x000053DC File Offset: 0x000035DC
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public override bool NeedSession
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x00005BEF File Offset: 0x00003DEF
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x00005BF2 File Offset: 0x00003DF2
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

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x00005BEF File Offset: 0x00003DEF
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public override bool DontCache
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x000500BD File Offset: 0x0004E2BD
		// (set) Token: 0x06001498 RID: 5272 RVA: 0x000500FA File Offset: 0x0004E2FA
		public override SessionReportItem SessionReport
		{
			get
			{
				if (this.m_sessionReportItem == null)
				{
					this.m_sessionReportItem = new SessionReportItem(DatabaseSessionStorage.Current, this.SessionID, this.m_userContext, new ReportItem(this.m_reportPath), DateTime.MinValue, true);
				}
				return this.m_sessionReportItem;
			}
			set
			{
				this.m_sessionReportItem = value;
			}
		}

		// Token: 0x0400074B RID: 1867
		private UserContext m_userContext;

		// Token: 0x0400074C RID: 1868
		private ExternalItemPath m_reportPath;

		// Token: 0x0400074D RID: 1869
		private string m_sessionID;

		// Token: 0x0400074E RID: 1870
		private SessionReportItem m_sessionReportItem;
	}
}
