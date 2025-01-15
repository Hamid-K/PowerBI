using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000272 RID: 626
	public sealed class ChartHierarchy : MemberHierarchy<ChartMember>
	{
		// Token: 0x0600185E RID: 6238 RVA: 0x000649A6 File Offset: 0x00062BA6
		internal ChartHierarchy(Chart owner, bool isColumn)
			: base(owner, isColumn)
		{
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x000649B0 File Offset: 0x00062BB0
		private Chart OwnerChart
		{
			get
			{
				return this.m_owner as Chart;
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x000649C0 File Offset: 0x00062BC0
		public ChartMemberCollection MemberCollection
		{
			get
			{
				if (this.m_members == null)
				{
					if (this.OwnerChart.IsOldSnapshot)
					{
						this.OwnerChart.ResetMemberCellDefinitionIndex(0);
						this.m_members = new ShimChartMemberCollection(this, this.OwnerChart, this.m_isColumn, null, this.m_isColumn ? this.OwnerChart.RenderChart.CategoryMemberCollection : this.OwnerChart.RenderChart.SeriesMemberCollection);
					}
					else
					{
						this.OwnerChart.ResetMemberCellDefinitionIndex(0);
						this.m_members = new InternalChartMemberCollection(this, this.OwnerChart, null, this.m_isColumn ? this.OwnerChart.ChartDef.CategoryMembers : this.OwnerChart.ChartDef.SeriesMembers);
					}
				}
				return (ChartMemberCollection)this.m_members;
			}
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x00064A8A File Offset: 0x00062C8A
		internal override void ResetContext()
		{
			if (this.m_members != null)
			{
				if (this.OwnerChart.IsOldSnapshot)
				{
					((ShimChartMemberCollection)this.m_members).UpdateContext();
					return;
				}
				((IDataRegionMemberCollection)this.m_members).SetNewContext();
			}
		}
	}
}
