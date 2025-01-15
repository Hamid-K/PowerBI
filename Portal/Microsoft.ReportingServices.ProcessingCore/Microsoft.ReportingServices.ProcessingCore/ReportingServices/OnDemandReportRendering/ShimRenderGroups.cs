using System;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200029B RID: 667
	internal sealed class ShimRenderGroups
	{
		// Token: 0x060019C3 RID: 6595 RVA: 0x00068551 File Offset: 0x00066751
		internal ShimRenderGroups(ListContentCollection renderGroups)
		{
			this.m_type = DataRegion.Type.List;
			this.m_renderListContents = renderGroups;
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00068567 File Offset: 0x00066767
		internal ShimRenderGroups(TableGroupCollection renderGroups)
		{
			this.m_type = DataRegion.Type.Table;
			this.m_renderTableGroups = renderGroups;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0006857D File Offset: 0x0006677D
		internal ShimRenderGroups(MatrixMemberCollection renderGroups, bool beforeSubtotal, bool afterSubtotal)
		{
			this.m_type = DataRegion.Type.Matrix;
			this.m_renderMatrixMembers = renderGroups;
			this.m_beforeSubtotal = beforeSubtotal;
			this.m_afterSubtotal = afterSubtotal;
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x000685A1 File Offset: 0x000667A1
		internal ShimRenderGroups(ChartMemberCollection renderGroups)
		{
			this.m_type = DataRegion.Type.Chart;
			this.m_renderChartMembers = renderGroups;
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x000685B7 File Offset: 0x000667B7
		internal ShimRenderGroups(DataMemberCollection renderGroups)
		{
			this.m_type = DataRegion.Type.CustomReportItem;
			this.m_renderDataMembers = renderGroups;
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x000685D0 File Offset: 0x000667D0
		internal int Count
		{
			get
			{
				switch (this.m_type)
				{
				case DataRegion.Type.List:
					return this.m_renderListContents.Count;
				case DataRegion.Type.Table:
					return this.m_renderTableGroups.Count;
				case DataRegion.Type.Matrix:
					if (this.m_afterSubtotal || this.m_beforeSubtotal)
					{
						return this.m_renderMatrixMembers.Count - 1;
					}
					return this.m_renderMatrixMembers.Count;
				case DataRegion.Type.Chart:
					return this.m_renderChartMembers.Count;
				case DataRegion.Type.CustomReportItem:
					return this.m_renderDataMembers.Count;
				}
				return 0;
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x00068661 File Offset: 0x00066861
		internal int MatrixMemberCollectionCount
		{
			get
			{
				if (this.m_type == DataRegion.Type.Matrix)
				{
					return this.m_renderMatrixMembers.Count;
				}
				return -1;
			}
		}

		// Token: 0x17000ECD RID: 3789
		internal Group this[int index]
		{
			get
			{
				switch (this.m_type)
				{
				case DataRegion.Type.List:
					return this.m_renderListContents[index];
				case DataRegion.Type.Table:
					return this.m_renderTableGroups[index];
				case DataRegion.Type.Matrix:
					if (this.m_beforeSubtotal)
					{
						return this.m_renderMatrixMembers[index + 1];
					}
					return this.m_renderMatrixMembers[index];
				case DataRegion.Type.Chart:
					return this.m_renderChartMembers[index];
				case DataRegion.Type.CustomReportItem:
					return this.m_renderDataMembers[index];
				}
				return null;
			}
		}

		// Token: 0x04000CD9 RID: 3289
		private DataRegion.Type m_type;

		// Token: 0x04000CDA RID: 3290
		private bool m_beforeSubtotal;

		// Token: 0x04000CDB RID: 3291
		private bool m_afterSubtotal;

		// Token: 0x04000CDC RID: 3292
		private ListContentCollection m_renderListContents;

		// Token: 0x04000CDD RID: 3293
		private TableGroupCollection m_renderTableGroups;

		// Token: 0x04000CDE RID: 3294
		private MatrixMemberCollection m_renderMatrixMembers;

		// Token: 0x04000CDF RID: 3295
		private ChartMemberCollection m_renderChartMembers;

		// Token: 0x04000CE0 RID: 3296
		private DataMemberCollection m_renderDataMembers;
	}
}
