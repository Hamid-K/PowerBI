using System;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000376 RID: 886
	internal sealed class ShimTableMember : ShimTablixMember
	{
		// Token: 0x060021E8 RID: 8680 RVA: 0x00082848 File Offset: 0x00080A48
		internal ShimTableMember(IDefinitionPath parentDefinitionPath, Tablix owner, ShimTableMember parent, int parentCollectionIndex, TableRow staticRow, KeepWithGroup keepWithGroup, bool isFixedTableHeader)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex, false)
		{
			this.m_innerStaticRow = staticRow;
			this.m_rowDefinitionStartIndex = owner.GetAndIncrementMemberCellDefinitionIndex();
			this.m_rowDefinitionEndIndex = owner.GetCurrentMemberCellDefinitionIndex();
			this.m_keepWithGroup = keepWithGroup;
			this.m_isFixedHeader = isFixedTableHeader;
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x000828A0 File Offset: 0x00080AA0
		internal ShimTableMember(IDefinitionPath parentDefinitionPath, Tablix owner, ShimTableMember parent, int parentCollectionIndex, ShimRenderGroups renderGroups)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex, false)
		{
			this.m_rowDefinitionStartIndex = owner.GetCurrentMemberCellDefinitionIndex();
			if (renderGroups != null)
			{
				this.m_children = new ShimTableMemberCollection(this, (Tablix)this.m_owner, this, (TableGroup)renderGroups[0]);
			}
			this.m_group = new Group(owner, renderGroups, this);
			this.m_rowDefinitionEndIndex = owner.GetCurrentMemberCellDefinitionIndex();
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x00082918 File Offset: 0x00080B18
		internal ShimTableMember(IDefinitionPath parentDefinitionPath, Tablix owner, ShimTableMember parent, int parentCollectionIndex, TableRowsCollection renderRows)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex, false)
		{
			this.m_rowDefinitionStartIndex = owner.GetCurrentMemberCellDefinitionIndex();
			this.m_isDetailGroup = true;
			this.m_renderDetails = renderRows;
			this.m_children = new ShimTableMemberCollection(this, (Tablix)this.m_owner, this, renderRows[0]);
			this.m_group = new Group(owner, this);
			this.m_rowDefinitionEndIndex = owner.GetCurrentMemberCellDefinitionIndex();
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x00082994 File Offset: 0x00080B94
		internal ShimTableMember(IDefinitionPath parentDefinitionPath, Tablix owner, int columnIndex, TableColumnCollection columns)
			: base(parentDefinitionPath, owner, null, columnIndex, true)
		{
			this.m_column = columns[columnIndex];
			this.m_isFixedHeader = this.m_column.FixedHeader;
			this.m_rowDefinitionEndIndex = columnIndex;
			this.m_rowDefinitionStartIndex = columnIndex;
		}

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x060021EC RID: 8684 RVA: 0x000829E9 File Offset: 0x00080BE9
		public override KeepWithGroup KeepWithGroup
		{
			get
			{
				return this.m_keepWithGroup;
			}
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x000829F1 File Offset: 0x00080BF1
		public override bool RepeatOnNewPage
		{
			get
			{
				return this.m_keepWithGroup > KeepWithGroup.None;
			}
		}

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x000829FC File Offset: 0x00080BFC
		public override string DataElementName
		{
			get
			{
				if (this.m_isDetailGroup)
				{
					return base.OwnerTablix.RenderTable.DetailDataCollectionName;
				}
				return base.DataElementName;
			}
		}

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x00082A1D File Offset: 0x00080C1D
		public override TablixMemberCollection Children
		{
			get
			{
				if (this.IsColumn)
				{
					return null;
				}
				return this.m_children;
			}
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x00082A2F File Offset: 0x00080C2F
		public override bool FixedData
		{
			get
			{
				return this.m_isFixedHeader;
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x00082A37 File Offset: 0x00080C37
		public override bool IsStatic
		{
			get
			{
				return this.IsColumn || (!this.m_isDetailGroup && (this.m_group == null || this.m_group.RenderGroups == null));
			}
		}

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x00082A63 File Offset: 0x00080C63
		public override bool IsColumn
		{
			get
			{
				return this.m_column != null;
			}
		}

		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x00082A6E File Offset: 0x00080C6E
		internal override int RowSpan
		{
			get
			{
				if (this.IsColumn)
				{
					return 0;
				}
				if (this.IsStatic)
				{
					return 1;
				}
				return this.m_rowDefinitionEndIndex - this.m_rowDefinitionStartIndex;
			}
		}

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x00082A91 File Offset: 0x00080C91
		internal override int ColSpan
		{
			get
			{
				if (this.IsColumn)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x00082A9E File Offset: 0x00080C9E
		public override int MemberCellIndex
		{
			get
			{
				return this.m_rowDefinitionStartIndex;
			}
		}

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x00082AA6 File Offset: 0x00080CA6
		public override TablixHeader TablixHeader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x00082AA9 File Offset: 0x00080CA9
		public override bool IsTotal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x060021F8 RID: 8696 RVA: 0x00082AAC File Offset: 0x00080CAC
		public override Visibility Visibility
		{
			get
			{
				if (this.m_visibility == null)
				{
					if (this.IsColumn && this.m_column.ColumnDefinition.Visibility != null)
					{
						this.m_visibility = new ShimTableMemberVisibility(this, ShimTableMemberVisibility.Mode.StaticColumn);
					}
					else if (!this.IsColumn && this.m_group != null)
					{
						if (this.m_isDetailGroup && this.m_renderDetails.DetailDefinition.Visibility != null)
						{
							this.m_visibility = new ShimTableMemberVisibility(this, ShimTableMemberVisibility.Mode.TableDetails);
						}
						else if (!this.m_isDetailGroup && this.m_group.CurrentShimRenderGroup.m_visibilityDef != null)
						{
							this.m_visibility = new ShimTableMemberVisibility(this, ShimTableMemberVisibility.Mode.TableGroup);
						}
					}
					else if (!this.IsColumn && this.m_innerStaticRow != null && this.m_innerStaticRow.m_rowDef.Visibility != null)
					{
						this.m_visibility = new ShimTableMemberVisibility(this, ShimTableMemberVisibility.Mode.StaticRow);
					}
				}
				return this.m_visibility;
			}
		}

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x060021F9 RID: 8697 RVA: 0x00082B87 File Offset: 0x00080D87
		internal override PageBreakLocation PropagatedGroupBreak
		{
			get
			{
				if (this.IsStatic)
				{
					return PageBreakLocation.None;
				}
				return this.m_propagatedPageBreak;
			}
		}

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x060021FA RID: 8698 RVA: 0x00082B99 File Offset: 0x00080D99
		public override bool HideIfNoRows
		{
			get
			{
				return !this.IsStatic || this.m_parent != null;
			}
		}

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x060021FB RID: 8699 RVA: 0x00082BB0 File Offset: 0x00080DB0
		public override TablixMemberInstance Instance
		{
			get
			{
				if (base.OwnerTablix.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					if (this.IsStatic)
					{
						this.m_instance = new TablixMemberInstance(base.OwnerTablix, this);
					}
					else
					{
						TablixDynamicMemberInstance tablixDynamicMemberInstance = new TablixDynamicMemberInstance(base.OwnerTablix, this, new InternalShimDynamicMemberLogic(this));
						this.m_owner.RenderingContext.AddDynamicInstance(tablixDynamicMemberInstance);
						this.m_instance = tablixDynamicMemberInstance;
					}
				}
				return this.m_instance;
			}
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x00082C28 File Offset: 0x00080E28
		internal override bool SetNewContext(int index)
		{
			base.ResetContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_isDetailGroup)
			{
				if (base.OwnerTablix.RenderTable.NoRows)
				{
					return false;
				}
				if (this.m_renderDetails == null || index < 0 || index >= this.m_renderDetails.Count)
				{
					return false;
				}
				bool flag = this.m_group.CurrentRenderGroupIndex == -1 && index == 0;
				this.m_group.CurrentRenderGroupIndex = index;
				if (!flag)
				{
					((ShimTableMemberCollection)this.m_children).UpdateDetails(this.m_renderDetails[index]);
				}
				return true;
			}
			else
			{
				if (this.m_group == null || this.m_group.RenderGroups == null)
				{
					return index <= 1;
				}
				if (base.OwnerTablix.RenderTable.NoRows)
				{
					return false;
				}
				if (index < 0 || index >= this.m_group.RenderGroups.Count)
				{
					return false;
				}
				TableGroup tableGroup = this.m_group.RenderGroups[index] as TableGroup;
				if (tableGroup.InstanceInfo == null)
				{
					return false;
				}
				this.m_group.CurrentRenderGroupIndex = index;
				((ShimTableMemberCollection)this.m_children).UpdateHeaderFooter(tableGroup.GroupHeader, tableGroup.GroupFooter);
				((ShimTableMemberCollection)this.m_children).ResetContext(tableGroup);
				return true;
			}
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x00082D72 File Offset: 0x00080F72
		internal void UpdateRow(TableRow newTableRow)
		{
			this.m_innerStaticRow = newTableRow;
			((ShimTableRow)((ShimTableRowCollection)base.OwnerTablix.Body.RowCollection)[this.m_rowDefinitionStartIndex]).UpdateCells(newTableRow);
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00082DA6 File Offset: 0x00080FA6
		internal override void ResetContext()
		{
			base.ResetContext();
			if (this.m_group.CurrentRenderGroupIndex >= 0)
			{
				this.ResetContext(null, null);
			}
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x00082DC4 File Offset: 0x00080FC4
		internal void ResetContext(TableGroupCollection newRenderSubGroups, TableRowsCollection newRenderDetails)
		{
			if (this.m_isDetailGroup)
			{
				this.m_group.CurrentRenderGroupIndex = -1;
				int rowDefinitionEndIndex = this.m_rowDefinitionEndIndex;
				int rowDefinitionStartIndex = this.m_rowDefinitionStartIndex;
				if (newRenderDetails != null)
				{
					this.m_renderDetails = newRenderDetails;
				}
				((ShimTableMemberCollection)this.m_children).UpdateDetails(this.m_renderDetails[0]);
			}
			else if (this.m_group != null && this.m_group.RenderGroups != null)
			{
				this.m_group.CurrentRenderGroupIndex = -1;
				if (newRenderSubGroups != null)
				{
					this.m_group.RenderGroups = new ShimRenderGroups(newRenderSubGroups);
				}
				if (this.m_children != null)
				{
					TableGroup tableGroup = this.m_group.CurrentShimRenderGroup as TableGroup;
					((ShimTableMemberCollection)this.m_children).UpdateHeaderFooter(tableGroup.GroupHeader, tableGroup.GroupFooter);
					((ShimTableMemberCollection)this.m_children).ResetContext(null);
				}
			}
			this.SetNewContext(true);
		}

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x00082E9D File Offset: 0x0008109D
		internal int RowDefinitionEndIndex
		{
			get
			{
				return this.m_rowDefinitionEndIndex;
			}
		}

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x00082EA8 File Offset: 0x000810A8
		internal string DetailInstanceUniqueName
		{
			get
			{
				if (!this.m_isDetailGroup)
				{
					return null;
				}
				if (this.m_group.CurrentRenderGroupIndex < 0)
				{
					return null;
				}
				string uniqueName = this.m_renderDetails[this.m_group.CurrentRenderGroupIndex][0].UniqueName;
				return this.m_owner.RenderingContext.GenerateShimUniqueName(uniqueName);
			}
		}

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x00082F02 File Offset: 0x00081102
		internal TableRowsCollection RenderTableDetails
		{
			get
			{
				if (!this.IsColumn)
				{
					return this.m_renderDetails;
				}
				return null;
			}
		}

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x00082F14 File Offset: 0x00081114
		internal TableGroup RenderTableGroup
		{
			get
			{
				if (!this.IsColumn && !this.m_isDetailGroup && this.m_group != null)
				{
					return (TableGroup)this.m_group.CurrentShimRenderGroup;
				}
				return null;
			}
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x00082F40 File Offset: 0x00081140
		internal TableRow RenderTableRow
		{
			get
			{
				if (!this.IsColumn)
				{
					return this.m_innerStaticRow;
				}
				return null;
			}
		}

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06002205 RID: 8709 RVA: 0x00082F52 File Offset: 0x00081152
		internal TableColumn RenderTableColumn
		{
			get
			{
				if (this.IsColumn)
				{
					return this.m_column;
				}
				return null;
			}
		}

		// Token: 0x040010DD RID: 4317
		private bool m_isDetailGroup;

		// Token: 0x040010DE RID: 4318
		private bool m_isFixedHeader;

		// Token: 0x040010DF RID: 4319
		private KeepWithGroup m_keepWithGroup;

		// Token: 0x040010E0 RID: 4320
		private int m_rowDefinitionStartIndex = -1;

		// Token: 0x040010E1 RID: 4321
		private int m_rowDefinitionEndIndex = -1;

		// Token: 0x040010E2 RID: 4322
		private TableColumn m_column;

		// Token: 0x040010E3 RID: 4323
		private TableRowsCollection m_renderDetails;

		// Token: 0x040010E4 RID: 4324
		private TableRow m_innerStaticRow;
	}
}
