using System;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000043 RID: 67
	internal sealed class TableGroup : Group, IDocumentMapEntry
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x000128A0 File Offset: 0x00010AA0
		internal TableGroup(Table owner, TableGroup parent, TableGroup groupDef, TableGroupInstance groupInstance)
			: base(owner, groupDef.Grouping, groupDef.Visibility)
		{
			this.m_parent = parent;
			this.m_groupDef = groupDef;
			this.m_groupInstance = groupInstance;
			if (this.m_groupInstance != null)
			{
				this.m_uniqueName = this.m_groupInstance.UniqueName;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x000128F0 File Offset: 0x00010AF0
		public override string ID
		{
			get
			{
				if (this.m_groupDef.RenderingModelID == null)
				{
					this.m_groupDef.RenderingModelID = this.m_groupDef.ID.ToString(CultureInfo.InvariantCulture);
				}
				return this.m_groupDef.RenderingModelID;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00012938 File Offset: 0x00010B38
		public override string Label
		{
			get
			{
				string text = null;
				if (this.m_groupingDef != null && this.m_groupingDef.GroupLabel != null)
				{
					if (this.m_groupingDef.GroupLabel.Type == ExpressionInfo.Types.Constant)
					{
						text = this.m_groupingDef.GroupLabel.Value;
					}
					else if (this.m_groupInstance == null)
					{
						text = null;
					}
					else
					{
						text = this.InstanceInfo.Label;
					}
				}
				return text;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0001299B File Offset: 0x00010B9B
		public bool InDocumentMap
		{
			get
			{
				return this.m_groupInstance != null && this.m_groupingDef != null && this.m_groupingDef.GroupLabel != null;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x000129C0 File Offset: 0x00010BC0
		public TableHeaderFooterRows GroupHeader
		{
			get
			{
				TableHeaderFooterRows tableHeaderFooterRows = this.m_headerRows;
				if (this.m_headerRows == null && this.m_groupDef.HeaderRows != null)
				{
					tableHeaderFooterRows = new TableHeaderFooterRows((Table)base.OwnerDataRegion, this.m_groupDef.HeaderRepeatOnNewPage, this.m_groupDef.HeaderRows, (this.m_groupInstance == null) ? null : this.m_groupInstance.HeaderRowInstances);
					if (base.OwnerDataRegion.RenderingContext.CacheState)
					{
						this.m_headerRows = tableHeaderFooterRows;
					}
				}
				return tableHeaderFooterRows;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00012A40 File Offset: 0x00010C40
		public TableHeaderFooterRows GroupFooter
		{
			get
			{
				TableHeaderFooterRows tableHeaderFooterRows = this.m_footerRows;
				if (this.m_footerRows == null && this.m_groupDef.FooterRows != null)
				{
					tableHeaderFooterRows = new TableHeaderFooterRows((Table)base.OwnerDataRegion, this.m_groupDef.FooterRepeatOnNewPage, this.m_groupDef.FooterRows, (this.m_groupInstance == null) ? null : this.m_groupInstance.FooterRowInstances);
					if (base.OwnerDataRegion.RenderingContext.CacheState)
					{
						this.m_footerRows = tableHeaderFooterRows;
					}
				}
				return tableHeaderFooterRows;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00012AC0 File Offset: 0x00010CC0
		public override bool PageBreakAtStart
		{
			get
			{
				return this.m_groupDef.Grouping.PageBreakAtStart || ((this.m_groupDef.HeaderRows == null || this.m_groupDef.HeaderRepeatOnNewPage) && this.m_groupDef.PropagatedPageBreakAtStart);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00012AFD File Offset: 0x00010CFD
		public override bool PageBreakAtEnd
		{
			get
			{
				return this.m_groupDef.Grouping.PageBreakAtEnd || ((this.m_groupDef.FooterRows == null || this.m_groupDef.FooterRepeatOnNewPage) && this.m_groupDef.PropagatedPageBreakAtEnd);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00012B3C File Offset: 0x00010D3C
		public TableGroupCollection SubGroups
		{
			get
			{
				TableGroupCollection tableGroupCollection = this.m_subGroups;
				if (this.m_subGroups == null && this.m_groupDef.SubGroup != null)
				{
					tableGroupCollection = new TableGroupCollection((Table)base.OwnerDataRegion, this, this.m_groupDef.SubGroup, (this.m_groupInstance == null) ? null : this.m_groupInstance.SubGroupInstances);
					if (base.OwnerDataRegion.RenderingContext.CacheState)
					{
						this.m_subGroups = tableGroupCollection;
					}
				}
				return tableGroupCollection;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00012BB4 File Offset: 0x00010DB4
		public TableRowsCollection DetailRows
		{
			get
			{
				TableRowsCollection tableRowsCollection = this.m_detailRows;
				Table table = (Table)base.OwnerDataRegion.ReportItemDef;
				if (this.m_detailRows == null && this.m_groupDef.SubGroup == null && table.TableDetail != null)
				{
					tableRowsCollection = new TableRowsCollection((Table)base.OwnerDataRegion, table.TableDetail, (this.m_groupInstance == null) ? null : this.m_groupInstance.TableDetailInstances);
					if (base.OwnerDataRegion.RenderingContext.CacheState)
					{
						this.m_detailRows = tableRowsCollection;
					}
				}
				return tableRowsCollection;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00012C40 File Offset: 0x00010E40
		public override bool Hidden
		{
			get
			{
				if (this.m_groupInstance == null)
				{
					return true;
				}
				if (this.m_groupDef.Visibility == null)
				{
					return false;
				}
				if (this.m_groupDef.Visibility.Toggle != null)
				{
					return base.OwnerDataRegion.RenderingContext.IsItemHidden(this.m_groupInstance.UniqueName, false);
				}
				return this.InstanceInfo.StartHidden;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00012CA0 File Offset: 0x00010EA0
		public override CustomPropertyCollection CustomProperties
		{
			get
			{
				CustomPropertyCollection customPropertyCollection = this.m_customProperties;
				if (this.m_customProperties == null)
				{
					if (this.m_groupDef.Grouping == null || this.m_groupDef.Grouping.CustomProperties == null)
					{
						return null;
					}
					if (this.m_groupInstance == null)
					{
						customPropertyCollection = new CustomPropertyCollection(this.m_groupDef.Grouping.CustomProperties, null);
					}
					else
					{
						customPropertyCollection = new CustomPropertyCollection(this.m_groupDef.Grouping.CustomProperties, this.InstanceInfo.CustomPropertyInstances);
					}
					if (base.OwnerDataRegion.RenderingContext.CacheState)
					{
						this.m_customProperties = customPropertyCollection;
					}
				}
				return customPropertyCollection;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00012D39 File Offset: 0x00010F39
		public TableGroup Parent
		{
			get
			{
				return this.m_parent;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00012D41 File Offset: 0x00010F41
		internal TableGroupInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_groupInstance == null)
				{
					return null;
				}
				if (this.m_groupInstanceInfo == null)
				{
					this.m_groupInstanceInfo = this.m_groupInstance.GetInstanceInfo(base.OwnerDataRegion.RenderingContext.ChunkManager);
				}
				return this.m_groupInstanceInfo;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00012D7C File Offset: 0x00010F7C
		internal TableGroup GroupDefinition
		{
			get
			{
				return this.m_groupDef;
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00012D84 File Offset: 0x00010F84
		public void GetDetailsOnThisPage(int pageIndex, out int start, out int numberOfDetails)
		{
			start = 0;
			numberOfDetails = 0;
			if (this.m_groupInstance.ChildrenStartAndEndPages == null)
			{
				return;
			}
			Global.Tracer.Assert(pageIndex >= 0 && pageIndex < this.m_groupInstance.ChildrenStartAndEndPages.Count);
			RenderingPagesRanges renderingPagesRanges = this.m_groupInstance.ChildrenStartAndEndPages[pageIndex];
			start = renderingPagesRanges.StartRow;
			numberOfDetails = renderingPagesRanges.NumberOfDetails;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00012DF0 File Offset: 0x00010FF0
		public bool IsGroupOnThisPage(int groupIndex, int pageNumber, out int startPage, out int endPage)
		{
			startPage = -1;
			endPage = -1;
			if (this.m_groupInstance.ChildrenStartAndEndPages == null || groupIndex >= this.m_groupInstance.ChildrenStartAndEndPages.Count)
			{
				return false;
			}
			RenderingPagesRanges renderingPagesRanges = this.m_groupInstance.ChildrenStartAndEndPages[groupIndex];
			startPage = renderingPagesRanges.StartPage;
			endPage = renderingPagesRanges.EndPage;
			return pageNumber >= startPage && pageNumber <= endPage;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00012E5C File Offset: 0x0001105C
		public void GetSubGroupsOnPage(int page, out int startGroup, out int endGroup)
		{
			startGroup = -1;
			endGroup = -1;
			if (this.m_groupInstance == null)
			{
				return;
			}
			RenderingPagesRangesList childrenStartAndEndPages = this.m_groupInstance.ChildrenStartAndEndPages;
			if (childrenStartAndEndPages == null)
			{
				return;
			}
			RenderingContext.FindRange(childrenStartAndEndPages, 0, childrenStartAndEndPages.Count - 1, page, ref startGroup, ref endGroup);
		}

		// Token: 0x04000144 RID: 324
		private TableGroup m_groupDef;

		// Token: 0x04000145 RID: 325
		private TableGroupInstance m_groupInstance;

		// Token: 0x04000146 RID: 326
		private TableGroupInstanceInfo m_groupInstanceInfo;

		// Token: 0x04000147 RID: 327
		private TableGroupCollection m_subGroups;

		// Token: 0x04000148 RID: 328
		private TableRowsCollection m_detailRows;

		// Token: 0x04000149 RID: 329
		private TableHeaderFooterRows m_headerRows;

		// Token: 0x0400014A RID: 330
		private TableHeaderFooterRows m_footerRows;

		// Token: 0x0400014B RID: 331
		private TableGroup m_parent;
	}
}
