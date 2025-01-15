using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000312 RID: 786
	public sealed class ReportSectionInstance : BaseInstance, IReportScopeInstance
	{
		// Token: 0x06001D12 RID: 7442 RVA: 0x00073371 File Offset: 0x00071571
		internal ReportSectionInstance(ReportSection sectionDef)
			: base(sectionDef)
		{
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x0007337A File Offset: 0x0007157A
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x0007337C File Offset: 0x0007157C
		internal override void SetNewContext()
		{
			this.m_isNewContext = true;
			base.SetNewContext();
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x0007338B File Offset: 0x0007158B
		internal ReportSection SectionDef
		{
			get
			{
				return (ReportSection)this.m_reportScope;
			}
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x00073398 File Offset: 0x00071598
		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x000733A0 File Offset: 0x000715A0
		public string UniqueName
		{
			get
			{
				if (this.SectionDef.IsOldSnapshot)
				{
					return this.SectionDef.Report.RenderReport.UniqueName + "xE";
				}
				ReportSection sectionDef = this.SectionDef.SectionDef;
				return InstancePathItem.GenerateUniqueNameString(sectionDef.ID, sectionDef.InstancePath);
			}
		}

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x000733F7 File Offset: 0x000715F7
		// (set) Token: 0x06001D19 RID: 7449 RVA: 0x000733FF File Offset: 0x000715FF
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

		// Token: 0x04000F33 RID: 3891
		private bool m_isNewContext;
	}
}
