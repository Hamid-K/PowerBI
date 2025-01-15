using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000353 RID: 851
	internal sealed class ShimMatrixRowCollection : TablixRowCollection
	{
		// Token: 0x060020A6 RID: 8358 RVA: 0x0007EE8C File Offset: 0x0007D08C
		internal ShimMatrixRowCollection(Tablix owner)
			: base(owner)
		{
			this.m_rows = new List<ShimMatrixRow>();
			this.AppendMatrixRows(null, owner.RowHierarchy.MemberCollection as ShimMatrixMemberCollection, owner.InSubtotal);
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x0007EEC0 File Offset: 0x0007D0C0
		private void AppendMatrixRows(ShimMatrixMember rowParentMember, ShimMatrixMemberCollection rowMembers, bool inSubtotalRow)
		{
			if (rowMembers == null)
			{
				this.m_rows.Add(new ShimMatrixRow(this.m_owner, this.m_rows.Count, rowParentMember, inSubtotalRow));
				return;
			}
			int count = rowMembers.Count;
			for (int i = 0; i < count; i++)
			{
				ShimMatrixMember shimMatrixMember = rowMembers[i] as ShimMatrixMember;
				this.AppendMatrixRows(shimMatrixMember, shimMatrixMember.Children as ShimMatrixMemberCollection, inSubtotalRow || shimMatrixMember.CurrentRenderMatrixMember.IsTotal);
			}
		}

		// Token: 0x1700126B RID: 4715
		public override TablixRow this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_rows[index];
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x0007EF8F File Offset: 0x0007D18F
		public override int Count
		{
			get
			{
				return this.m_rows.Count;
			}
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x0007EF9C File Offset: 0x0007D19C
		internal void UpdateCells(ShimMatrixMember innermostMember)
		{
			if (innermostMember == null || innermostMember.Children != null)
			{
				return;
			}
			if (!innermostMember.IsColumn)
			{
				int memberCellIndex = innermostMember.MemberCellIndex;
				int count = this.m_rows[memberCellIndex].Count;
				for (int i = 0; i < count; i++)
				{
					((ShimMatrixCell)this.m_rows[memberCellIndex][i]).ResetCellContents();
				}
				return;
			}
			int memberCellIndex2 = innermostMember.MemberCellIndex;
			int count2 = this.m_rows.Count;
			for (int j = 0; j < count2; j++)
			{
				((ShimMatrixCell)this.m_rows[j][memberCellIndex2]).ResetCellContents();
			}
		}

		// Token: 0x04001069 RID: 4201
		private List<ShimMatrixRow> m_rows;
	}
}
