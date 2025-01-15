using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000226 RID: 550
	internal sealed class ShimChartSeriesCollection : ChartSeriesCollection
	{
		// Token: 0x060014B0 RID: 5296 RVA: 0x00054731 File Offset: 0x00052931
		internal ShimChartSeriesCollection(Chart owner)
			: base(owner)
		{
			this.m_series = new List<ShimChartSeries>();
			this.AppendChartSeries(null, owner.SeriesHierarchy.MemberCollection as ShimChartMemberCollection);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0005475C File Offset: 0x0005295C
		private void AppendChartSeries(ShimChartMember seriesParentMember, ShimChartMemberCollection seriesMembers)
		{
			if (seriesMembers == null)
			{
				this.m_series.Add(new ShimChartSeries(this.m_owner, this.m_series.Count, seriesParentMember));
				return;
			}
			int count = seriesMembers.Count;
			for (int i = 0; i < count; i++)
			{
				ShimChartMember shimChartMember = seriesMembers[i] as ShimChartMember;
				this.AppendChartSeries(shimChartMember, shimChartMember.Children as ShimChartMemberCollection);
			}
		}

		// Token: 0x17000B04 RID: 2820
		public override ChartSeries this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_series[index];
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0005481B File Offset: 0x00052A1B
		public override int Count
		{
			get
			{
				return this.m_series.Count;
			}
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00054828 File Offset: 0x00052A28
		internal void UpdateCells(ShimChartMember innermostMember)
		{
			if (innermostMember == null || innermostMember.Children != null)
			{
				return;
			}
			if (!innermostMember.IsCategory)
			{
				int memberCellIndex = innermostMember.MemberCellIndex;
				int count = this.m_series[memberCellIndex].Count;
				for (int i = 0; i < count; i++)
				{
					((ShimChartDataPoint)this.m_series[memberCellIndex][i]).SetNewContext();
				}
				return;
			}
			int memberCellIndex2 = innermostMember.MemberCellIndex;
			int count2 = this.m_series.Count;
			for (int j = 0; j < count2; j++)
			{
				((ShimChartDataPoint)this.m_series[j][memberCellIndex2]).SetNewContext();
			}
		}

		// Token: 0x040009C4 RID: 2500
		private List<ShimChartSeries> m_series;
	}
}
