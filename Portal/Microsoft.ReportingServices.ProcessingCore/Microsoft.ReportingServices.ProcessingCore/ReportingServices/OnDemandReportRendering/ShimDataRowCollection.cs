using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200027E RID: 638
	internal sealed class ShimDataRowCollection : DataRowCollection
	{
		// Token: 0x060018E7 RID: 6375 RVA: 0x0006645F File Offset: 0x0006465F
		internal ShimDataRowCollection(CustomReportItem owner)
			: base(owner)
		{
			this.m_dataRows = new List<ShimDataRow>();
			this.AppendDataRows(null, owner.CustomData.DataRowHierarchy.MemberCollection as ShimDataMemberCollection);
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x00066490 File Offset: 0x00064690
		private void AppendDataRows(ShimDataMember rowParentMember, ShimDataMemberCollection rowMembers)
		{
			if (rowMembers == null)
			{
				this.m_dataRows.Add(new ShimDataRow(this.m_owner, this.m_dataRows.Count, rowParentMember));
				return;
			}
			int count = rowMembers.Count;
			for (int i = 0; i < count; i++)
			{
				ShimDataMember shimDataMember = rowMembers[i] as ShimDataMember;
				this.AppendDataRows(shimDataMember, shimDataMember.Children as ShimDataMemberCollection);
			}
		}

		// Token: 0x17000E44 RID: 3652
		public override DataRow this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_dataRows[index];
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x0006654F File Offset: 0x0006474F
		public override int Count
		{
			get
			{
				return this.m_dataRows.Count;
			}
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x0006655C File Offset: 0x0006475C
		internal void UpdateCells(ShimDataMember innermostMember)
		{
			if (innermostMember == null || innermostMember.Children != null)
			{
				return;
			}
			if (!innermostMember.IsColumn)
			{
				int memberCellIndex = innermostMember.MemberCellIndex;
				int count = this.m_dataRows[memberCellIndex].Count;
				for (int i = 0; i < count; i++)
				{
					((ShimDataCell)this.m_dataRows[memberCellIndex][i]).SetNewContext();
				}
				return;
			}
			int memberCellIndex2 = innermostMember.MemberCellIndex;
			int count2 = this.m_dataRows.Count;
			for (int j = 0; j < count2; j++)
			{
				((ShimDataCell)this.m_dataRows[j][memberCellIndex2]).SetNewContext();
			}
		}

		// Token: 0x04000C8F RID: 3215
		private List<ShimDataRow> m_dataRows;
	}
}
