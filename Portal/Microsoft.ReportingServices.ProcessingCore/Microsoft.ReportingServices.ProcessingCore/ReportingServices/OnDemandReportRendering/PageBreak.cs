using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002D8 RID: 728
	public sealed class PageBreak
	{
		// Token: 0x06001B36 RID: 6966 RVA: 0x0006C6F0 File Offset: 0x0006A8F0
		internal PageBreak(RenderingContext renderingContext, IReportScope reportScope, IPageBreakOwner pageBreakOwner)
		{
			this.m_renderingContext = renderingContext;
			this.m_reportScope = reportScope;
			this.m_pageBreakOwner = pageBreakOwner;
			this.m_pageBreakDef = this.m_pageBreakOwner.PageBreak;
			if (this.m_pageBreakDef == null)
			{
				this.m_pageBreakDef = new PageBreak();
			}
			this.m_isOldSnapshotOrStaticMember = false;
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0006C743 File Offset: 0x0006A943
		internal PageBreak(RenderingContext renderingContext, IReportScope reportScope, PageBreakLocation pageBreaklocation)
		{
			this.m_renderingContext = renderingContext;
			this.m_reportScope = reportScope;
			this.m_pageBreaklocation = pageBreaklocation;
			this.m_isOldSnapshotOrStaticMember = true;
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x0006C767 File Offset: 0x0006A967
		public PageBreakLocation BreakLocation
		{
			get
			{
				if (this.m_isOldSnapshotOrStaticMember)
				{
					return this.m_pageBreaklocation;
				}
				return this.m_pageBreakDef.BreakLocation;
			}
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x0006C783 File Offset: 0x0006A983
		public ReportBoolProperty Disabled
		{
			get
			{
				if (this.m_disabled == null)
				{
					if (this.m_isOldSnapshotOrStaticMember)
					{
						this.m_disabled = new ReportBoolProperty();
					}
					else
					{
						this.m_disabled = new ReportBoolProperty(this.m_pageBreakDef.Disabled);
					}
				}
				return this.m_disabled;
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0006C7BE File Offset: 0x0006A9BE
		public ReportBoolProperty ResetPageNumber
		{
			get
			{
				if (this.m_resetPageNumber == null)
				{
					if (this.m_isOldSnapshotOrStaticMember)
					{
						this.m_resetPageNumber = new ReportBoolProperty();
					}
					else
					{
						this.m_resetPageNumber = new ReportBoolProperty(this.m_pageBreakDef.ResetPageNumber);
					}
				}
				return this.m_resetPageNumber;
			}
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x0006C7F9 File Offset: 0x0006A9F9
		public PageBreakInstance Instance
		{
			get
			{
				if (this.m_renderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_pageBreakInstance == null)
				{
					this.m_pageBreakInstance = new PageBreakInstance(this.m_reportScope, this);
				}
				return this.m_pageBreakInstance;
			}
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x0006C82A File Offset: 0x0006AA2A
		internal bool IsOldSnapshot
		{
			get
			{
				return this.m_isOldSnapshotOrStaticMember;
			}
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x0006C832 File Offset: 0x0006AA32
		internal IReportScope ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x0006C83A File Offset: 0x0006AA3A
		internal RenderingContext RenderingContext
		{
			get
			{
				return this.m_renderingContext;
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x0006C842 File Offset: 0x0006AA42
		internal IPageBreakOwner PageBreakOwner
		{
			get
			{
				return this.m_pageBreakOwner;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x0006C84A File Offset: 0x0006AA4A
		internal PageBreak PageBreakDef
		{
			get
			{
				return this.m_pageBreakDef;
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x0006C854 File Offset: 0x0006AA54
		internal bool HasEnabledInstance
		{
			get
			{
				PageBreakInstance instance = this.Instance;
				return instance != null && !instance.Disabled;
			}
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x0006C876 File Offset: 0x0006AA76
		internal void SetNewContext()
		{
			if (this.m_pageBreakInstance != null)
			{
				this.m_pageBreakInstance.SetNewContext();
			}
		}

		// Token: 0x04000D6F RID: 3439
		private ReportBoolProperty m_resetPageNumber;

		// Token: 0x04000D70 RID: 3440
		private ReportBoolProperty m_disabled;

		// Token: 0x04000D71 RID: 3441
		private RenderingContext m_renderingContext;

		// Token: 0x04000D72 RID: 3442
		private IReportScope m_reportScope;

		// Token: 0x04000D73 RID: 3443
		private IPageBreakOwner m_pageBreakOwner;

		// Token: 0x04000D74 RID: 3444
		private PageBreak m_pageBreakDef;

		// Token: 0x04000D75 RID: 3445
		private PageBreakLocation m_pageBreaklocation;

		// Token: 0x04000D76 RID: 3446
		private bool m_isOldSnapshotOrStaticMember;

		// Token: 0x04000D77 RID: 3447
		private PageBreakInstance m_pageBreakInstance;
	}
}
