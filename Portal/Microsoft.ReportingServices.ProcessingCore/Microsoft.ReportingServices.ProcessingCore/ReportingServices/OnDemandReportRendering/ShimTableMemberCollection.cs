using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200036F RID: 879
	internal sealed class ShimTableMemberCollection : ShimMemberCollection
	{
		// Token: 0x06002172 RID: 8562 RVA: 0x000814A4 File Offset: 0x0007F6A4
		internal ShimTableMemberCollection(IDefinitionPath parentDefinitionPath, Tablix owner, bool isColumnGroup)
			: base(parentDefinitionPath, owner, isColumnGroup)
		{
			DataRegionMember[] array;
			if (this.m_isColumnGroup)
			{
				int count = owner.RenderTable.Columns.Count;
				array = new ShimTableMember[count];
				this.m_children = array;
				for (int i = 0; i < count; i++)
				{
					this.m_children[i] = new ShimTableMember(this, owner, i, owner.RenderTable.Columns);
				}
				return;
			}
			this.m_rowDefinitionStartIndex = owner.GetCurrentMemberCellDefinitionIndex();
			array = this.CreateInnerHierarchy(owner, null, owner.RenderTable.TableHeader, owner.RenderTable.TableFooter, owner.RenderTable.TableGroups, owner.RenderTable.DetailRows, ref this.m_dynamicSubgroupChildIndex);
			this.m_children = array;
			this.m_rowDefinitionEndIndex = owner.GetCurrentMemberCellDefinitionIndex();
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x00081578 File Offset: 0x0007F778
		internal ShimTableMemberCollection(IDefinitionPath parentDefinitionPath, Tablix owner, ShimTableMember parent, Microsoft.ReportingServices.ReportRendering.TableGroup tableGroup)
			: base(parentDefinitionPath, owner, false)
		{
			this.m_rowDefinitionStartIndex = owner.GetCurrentMemberCellDefinitionIndex();
			DataRegionMember[] array = this.CreateInnerHierarchy(owner, parent, tableGroup.GroupHeader, tableGroup.GroupFooter, tableGroup.SubGroups, tableGroup.DetailRows, ref this.m_dynamicSubgroupChildIndex);
			this.m_children = array;
			this.m_rowDefinitionEndIndex = owner.GetCurrentMemberCellDefinitionIndex();
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000815F0 File Offset: 0x0007F7F0
		internal ShimTableMemberCollection(IDefinitionPath parentDefinitionPath, Tablix owner, ShimTableMember parent, TableDetailRowCollection detailRows)
			: base(parentDefinitionPath, owner, false)
		{
			this.m_rowDefinitionStartIndex = owner.GetCurrentMemberCellDefinitionIndex();
			int count = detailRows.Count;
			DataRegionMember[] array = new ShimTableMember[count];
			this.m_children = array;
			for (int i = 0; i < count; i++)
			{
				this.m_children[i] = new ShimTableMember(this, owner, parent, i, detailRows[i], KeepWithGroup.None, false);
			}
			this.m_rowDefinitionEndIndex = owner.GetCurrentMemberCellDefinitionIndex();
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x00081670 File Offset: 0x0007F870
		private TablixMember[] CreateInnerHierarchy(Tablix owner, ShimTableMember parent, TableHeaderFooterRows headerRows, TableHeaderFooterRows footerRows, TableGroupCollection subGroups, TableRowsCollection detailRows, ref int dynamicSubgroupChildIndex)
		{
			List<ShimTableMember> list = new List<ShimTableMember>();
			bool flag = subGroups == null && detailRows == null;
			this.CreateHeaderFooter(list, headerRows, ShimTableMemberCollection.DetermineKeepWithGroup(true, headerRows, flag), owner, parent, parent == null && owner.RenderTable.FixedHeader);
			if (subGroups != null)
			{
				dynamicSubgroupChildIndex = list.Count;
				this.CreateInnerDynamicGroups(list, subGroups, owner, parent);
			}
			else if (detailRows != null)
			{
				dynamicSubgroupChildIndex = list.Count;
				list.Add(new ShimTableMember(this, owner, parent, dynamicSubgroupChildIndex, detailRows));
			}
			this.CreateHeaderFooter(list, footerRows, ShimTableMemberCollection.DetermineKeepWithGroup(false, footerRows, flag), owner, parent, false);
			return list.ToArray();
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0008170A File Offset: 0x0007F90A
		private static KeepWithGroup DetermineKeepWithGroup(bool isHeader, TableHeaderFooterRows rows, bool noKeepWith)
		{
			if (noKeepWith || rows == null || !rows.RepeatOnNewPage)
			{
				return KeepWithGroup.None;
			}
			if (isHeader)
			{
				return KeepWithGroup.After;
			}
			return KeepWithGroup.Before;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x00081724 File Offset: 0x0007F924
		private void CreateHeaderFooter(List<ShimTableMember> rowGroups, TableHeaderFooterRows headerFooterRows, KeepWithGroup keepWithGroup, Tablix owner, ShimTableMember parent, bool isFixedTableHeader)
		{
			if (headerFooterRows != null)
			{
				int count = headerFooterRows.Count;
				int num = rowGroups.Count;
				for (int i = 0; i < count; i++)
				{
					rowGroups.Add(new ShimTableMember(this, owner, parent, num, headerFooterRows[i], keepWithGroup, isFixedTableHeader));
					num++;
				}
			}
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x00081770 File Offset: 0x0007F970
		private void CreateInnerDynamicGroups(List<ShimTableMember> rowGroups, TableGroupCollection renderGroupCollection, Tablix owner, ShimTableMember parent)
		{
			if (renderGroupCollection != null)
			{
				ShimTableMember shimTableMember = new ShimTableMember(this, owner, parent, rowGroups.Count, new ShimRenderGroups(renderGroupCollection));
				rowGroups.Add(shimTableMember);
			}
		}

		// Token: 0x170012D7 RID: 4823
		public override TablixMember this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return (TablixMember)this.m_children[index];
			}
		}

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x000817F8 File Offset: 0x0007F9F8
		public override int Count
		{
			get
			{
				return this.m_children.Length;
			}
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x00081804 File Offset: 0x0007FA04
		internal void UpdateContext()
		{
			if (this.m_children == null)
			{
				return;
			}
			this.UpdateHeaderFooter(base.OwnerTablix.RenderTable.TableHeader, base.OwnerTablix.RenderTable.TableFooter);
			if (this.m_dynamicSubgroupChildIndex >= 0)
			{
				((ShimTableMember)this.m_children[this.m_dynamicSubgroupChildIndex]).ResetContext(base.OwnerTablix.RenderTable.TableGroups, base.OwnerTablix.RenderTable.DetailRows);
			}
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x00081880 File Offset: 0x0007FA80
		internal void UpdateHeaderFooter(TableHeaderFooterRows headerRows, TableHeaderFooterRows footerRows)
		{
			if (this.m_children == null || (headerRows == null && footerRows == null))
			{
				return;
			}
			int num = ((headerRows != null) ? headerRows.Count : 0);
			int num2 = ((footerRows != null) ? footerRows.Count : 0);
			int num3 = this.m_children.Length;
			for (int i = 0; i < num; i++)
			{
				((ShimTableMember)this.m_children[i]).UpdateRow(headerRows[i]);
			}
			for (int j = num2; j > 0; j--)
			{
				((ShimTableMember)this.m_children[num3 - j]).UpdateRow(footerRows[num2 - j]);
			}
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x00081914 File Offset: 0x0007FB14
		internal void UpdateDetails(TableDetailRowCollection newRenderDetails)
		{
			if (this.m_children == null || newRenderDetails == null)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			for (int i = 0; i < this.m_children.Length; i++)
			{
				((ShimTableMember)this.m_children[i]).UpdateRow(newRenderDetails[i]);
			}
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x00081964 File Offset: 0x0007FB64
		internal void ResetContext(Microsoft.ReportingServices.ReportRendering.TableGroup newRenderGroup)
		{
			if (this.m_children == null)
			{
				return;
			}
			for (int i = 0; i < this.m_children.Length; i++)
			{
				((ShimTableMember)this.m_children[i]).ResetContext((newRenderGroup != null) ? newRenderGroup.SubGroups : null, (newRenderGroup != null) ? newRenderGroup.DetailRows : null);
			}
		}

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x000819B7 File Offset: 0x0007FBB7
		internal PageBreakLocation PropagatedGroupBreakLocation
		{
			get
			{
				if (this.m_dynamicSubgroupChildIndex < 0)
				{
					return PageBreakLocation.None;
				}
				return ((TablixMember)this.m_children[this.m_dynamicSubgroupChildIndex]).PropagatedGroupBreak;
			}
		}

		// Token: 0x040010C7 RID: 4295
		private int m_rowDefinitionStartIndex = -1;

		// Token: 0x040010C8 RID: 4296
		private int m_rowDefinitionEndIndex = -1;

		// Token: 0x040010C9 RID: 4297
		private int m_dynamicSubgroupChildIndex = -1;
	}
}
