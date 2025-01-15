using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000283 RID: 643
	internal sealed class ShimDataCell : Microsoft.ReportingServices.OnDemandReportRendering.DataCell
	{
		// Token: 0x06001900 RID: 6400 RVA: 0x000668A7 File Offset: 0x00064AA7
		internal ShimDataCell(Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem owner, int rowIndex, int colIndex, ShimDataMember rowParentMember, ShimDataMember columnParentMember)
			: base(owner, rowIndex, colIndex)
		{
			this.m_rowParentMember = rowParentMember;
			this.m_columnParentMember = columnParentMember;
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x000668C4 File Offset: 0x00064AC4
		public override DataValueCollection DataValues
		{
			get
			{
				if (this.m_dataValues == null)
				{
					this.m_dataValues = new DataValueCollection(this.m_owner.RenderingContext, this.CachedRenderDataCell);
				}
				else if (this.m_renderDataCell == null)
				{
					this.m_dataValues.UpdateDataCellValues(this.CachedRenderDataCell);
				}
				return this.m_dataValues;
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x00066916 File Offset: 0x00064B16
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.DataCell DataCellDef
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x00066919 File Offset: 0x00064B19
		internal override Microsoft.ReportingServices.ReportRendering.DataCell RenderItem
		{
			get
			{
				return this.CachedRenderDataCell;
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x00066924 File Offset: 0x00064B24
		private Microsoft.ReportingServices.ReportRendering.DataCell CachedRenderDataCell
		{
			get
			{
				if (this.m_renderDataCell == null)
				{
					int memberCellIndex = this.m_rowParentMember.CurrentRenderDataMember.MemberCellIndex;
					int memberCellIndex2 = this.m_columnParentMember.CurrentRenderDataMember.MemberCellIndex;
					this.m_renderDataCell = this.m_owner.RenderCri.CustomData.DataCells[memberCellIndex, memberCellIndex2];
				}
				return this.m_renderDataCell;
			}
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00066983 File Offset: 0x00064B83
		internal override void SetNewContext()
		{
			base.SetNewContext();
			this.m_renderDataCell = null;
		}

		// Token: 0x04000C9A RID: 3226
		private Microsoft.ReportingServices.ReportRendering.DataCell m_renderDataCell;

		// Token: 0x04000C9B RID: 3227
		private ShimDataMember m_rowParentMember;

		// Token: 0x04000C9C RID: 3228
		private ShimDataMember m_columnParentMember;
	}
}
