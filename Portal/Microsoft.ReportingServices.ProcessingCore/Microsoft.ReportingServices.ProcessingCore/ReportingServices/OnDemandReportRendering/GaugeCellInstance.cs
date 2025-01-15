using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000114 RID: 276
	public sealed class GaugeCellInstance : BaseInstance, IReportScopeInstance
	{
		// Token: 0x06000C37 RID: 3127 RVA: 0x000350F7 File Offset: 0x000332F7
		internal GaugeCellInstance(GaugeCell gaugeCellDef)
			: base(gaugeCellDef)
		{
			this.m_gaugeCellDef = gaugeCellDef;
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0003510E File Offset: 0x0003330E
		string IReportScopeInstance.UniqueName
		{
			get
			{
				return this.m_gaugeCellDef.GaugeCellDef.UniqueName;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00035120 File Offset: 0x00033320
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x00035128 File Offset: 0x00033328
		bool IReportScopeInstance.IsNewContext
		{
			get
			{
				return this.m_isNewContext;
			}
			set
			{
				this.m_isNewContext = value;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00035131 File Offset: 0x00033331
		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00035139 File Offset: 0x00033339
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0003513B File Offset: 0x0003333B
		internal override void SetNewContext()
		{
			if (this.m_isNewContext)
			{
				return;
			}
			this.m_isNewContext = true;
			base.SetNewContext();
		}

		// Token: 0x04000546 RID: 1350
		private GaugeCell m_gaugeCellDef;

		// Token: 0x04000547 RID: 1351
		private bool m_isNewContext = true;
	}
}
