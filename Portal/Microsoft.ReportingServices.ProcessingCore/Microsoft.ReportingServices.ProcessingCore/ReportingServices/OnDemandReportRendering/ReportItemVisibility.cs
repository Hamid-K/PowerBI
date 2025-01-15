using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000379 RID: 889
	internal sealed class ReportItemVisibility : Visibility
	{
		// Token: 0x0600222C RID: 8748 RVA: 0x0008365B File Offset: 0x0008185B
		public ReportItemVisibility(ReportItem owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x0008366C File Offset: 0x0008186C
		public override ReportBoolProperty Hidden
		{
			get
			{
				if (this.m_startHidden == null)
				{
					if (this.m_owner.IsOldSnapshot)
					{
						this.m_startHidden = Visibility.GetStartHidden(this.m_owner.RenderReportItem.ReportItemDef.Visibility);
					}
					else
					{
						this.m_startHidden = Visibility.GetStartHidden(this.m_owner.ReportItemDef.Visibility);
					}
				}
				return this.m_startHidden;
			}
		}

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x000836D4 File Offset: 0x000818D4
		public override string ToggleItem
		{
			get
			{
				if (this.m_owner.IsOldSnapshot)
				{
					if (this.m_owner.RenderReportItem.ReportItemDef.Visibility != null)
					{
						return this.m_owner.RenderReportItem.ReportItemDef.Visibility.Toggle;
					}
				}
				else if (this.m_owner.ReportItemDef.Visibility != null)
				{
					return this.m_owner.ReportItemDef.Visibility.Toggle;
				}
				return null;
			}
		}

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x0600222F RID: 8751 RVA: 0x00083749 File Offset: 0x00081949
		public override SharedHiddenState HiddenState
		{
			get
			{
				if (this.m_owner.IsOldSnapshot)
				{
					return Visibility.GetHiddenState(this.m_owner.RenderReportItem.ReportItemDef.Visibility);
				}
				return Visibility.GetHiddenState(this.m_owner.ReportItemDef.Visibility);
			}
		}

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06002230 RID: 8752 RVA: 0x00083788 File Offset: 0x00081988
		public override bool RecursiveToggleReceiver
		{
			get
			{
				if (this.m_owner.IsOldSnapshot)
				{
					if (this.m_owner.RenderReportItem.ReportItemDef.Visibility != null)
					{
						return this.m_owner.RenderReportItem.ReportItemDef.Visibility.RecursiveReceiver;
					}
				}
				else if (this.m_owner.ReportItemDef.Visibility != null)
				{
					return this.m_owner.ReportItemDef.Visibility.RecursiveReceiver;
				}
				return false;
			}
		}

		// Token: 0x040010EC RID: 4332
		private ReportItem m_owner;
	}
}
