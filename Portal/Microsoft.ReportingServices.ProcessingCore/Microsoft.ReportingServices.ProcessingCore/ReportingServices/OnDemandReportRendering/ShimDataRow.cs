using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000281 RID: 641
	internal sealed class ShimDataRow : DataRow
	{
		// Token: 0x060018F1 RID: 6385 RVA: 0x000666FF File Offset: 0x000648FF
		internal ShimDataRow(CustomReportItem owner, int rowIndex, ShimDataMember parentDataMember)
			: base(owner, rowIndex)
		{
			this.m_cells = new List<ShimDataCell>();
			this.GenerateDataCells(parentDataMember, null, owner.CustomData.DataColumnHierarchy.MemberCollection as ShimDataMemberCollection);
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00066734 File Offset: 0x00064934
		private void GenerateDataCells(ShimDataMember rowParentMember, ShimDataMember columnParentMember, ShimDataMemberCollection columnMembers)
		{
			if (columnMembers == null)
			{
				this.m_cells.Add(new ShimDataCell(this.m_owner, this.m_rowIndex, this.m_cells.Count, rowParentMember, columnParentMember));
				return;
			}
			int count = columnMembers.Count;
			for (int i = 0; i < count; i++)
			{
				ShimDataMember shimDataMember = columnMembers[i] as ShimDataMember;
				this.GenerateDataCells(rowParentMember, shimDataMember, shimDataMember.Children as ShimDataMemberCollection);
			}
		}

		// Token: 0x17000E48 RID: 3656
		public override DataCell this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_cells[index];
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x000667FB File Offset: 0x000649FB
		public override int Count
		{
			get
			{
				return this.m_cells.Count;
			}
		}

		// Token: 0x04000C94 RID: 3220
		private List<ShimDataCell> m_cells;
	}
}
