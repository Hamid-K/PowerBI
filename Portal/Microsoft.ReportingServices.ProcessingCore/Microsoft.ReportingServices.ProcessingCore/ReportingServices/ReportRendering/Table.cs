using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200003D RID: 61
	internal sealed class Table : DataRegion
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x0001156E File Offset: 0x0000F76E
		internal Table(int intUniqueName, Table reportItemDef, TableInstance reportItemInstance, RenderingContext renderingContext)
			: base(intUniqueName, reportItemDef, reportItemInstance, renderingContext)
		{
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0001157C File Offset: 0x0000F77C
		public override bool PageBreakAtEnd
		{
			get
			{
				return ((Table)base.ReportItemDef).PageBreakAtEnd || ((((Table)base.ReportItemDef).FooterRows == null || ((Table)base.ReportItemDef).FooterRepeatOnNewPage) && ((Table)base.ReportItemDef).PropagatedPageBreakAtEnd);
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x000115D4 File Offset: 0x0000F7D4
		public override bool PageBreakAtStart
		{
			get
			{
				return ((Table)base.ReportItemDef).PageBreakAtStart || ((((Table)base.ReportItemDef).HeaderRows == null || ((Table)base.ReportItemDef).HeaderRepeatOnNewPage) && ((Table)base.ReportItemDef).PropagatedPageBreakAtStart);
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001162B File Offset: 0x0000F82B
		public bool GroupBreakAtStart
		{
			get
			{
				return ((Table)base.ReportItemDef).GroupBreakAtStart;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0001163D File Offset: 0x0000F83D
		public bool GroupBreakAtEnd
		{
			get
			{
				return ((Table)base.ReportItemDef).GroupBreakAtEnd;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00011650 File Offset: 0x0000F850
		public TableColumnCollection Columns
		{
			get
			{
				TableColumnCollection tableColumnCollection = this.m_tableColumns;
				if (this.m_tableColumns == null)
				{
					tableColumnCollection = new TableColumnCollection(this, ((Table)base.ReportItemDef).TableColumns);
					if (base.RenderingContext.CacheState)
					{
						this.m_tableColumns = tableColumnCollection;
					}
				}
				return tableColumnCollection;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00011698 File Offset: 0x0000F898
		public TableGroupCollection TableGroups
		{
			get
			{
				TableGroupCollection tableGroupCollection = this.m_tableGroups;
				if (this.m_tableGroups == null && ((Table)base.ReportItemDef).TableGroups != null)
				{
					tableGroupCollection = new TableGroupCollection(this, null, ((Table)base.ReportItemDef).TableGroups, (base.ReportItemInstance == null) ? null : ((TableInstance)base.ReportItemInstance).TableGroupInstances);
					if (base.RenderingContext.CacheState)
					{
						this.m_tableGroups = tableGroupCollection;
					}
				}
				return tableGroupCollection;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00011710 File Offset: 0x0000F910
		public TableHeaderFooterRows TableHeader
		{
			get
			{
				Table table = (Table)base.ReportItemDef;
				TableHeaderFooterRows tableHeaderFooterRows = this.m_headerRows;
				if (this.m_headerRows == null && table.HeaderRows != null)
				{
					tableHeaderFooterRows = new TableHeaderFooterRows(this, table.HeaderRepeatOnNewPage, table.HeaderRows, (base.ReportItemInstance == null) ? null : ((TableInstance)base.ReportItemInstance).HeaderRowInstances);
					if (base.RenderingContext.CacheState)
					{
						this.m_headerRows = tableHeaderFooterRows;
					}
				}
				return tableHeaderFooterRows;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00011784 File Offset: 0x0000F984
		public TableHeaderFooterRows TableFooter
		{
			get
			{
				Table table = (Table)base.ReportItemDef;
				TableHeaderFooterRows tableHeaderFooterRows = this.m_footerRows;
				if (this.m_footerRows == null && table.FooterRows != null)
				{
					tableHeaderFooterRows = new TableHeaderFooterRows(this, table.FooterRepeatOnNewPage, table.FooterRows, (base.ReportItemInstance == null) ? null : ((TableInstance)base.ReportItemInstance).FooterRowInstances);
					if (base.RenderingContext.CacheState)
					{
						this.m_footerRows = tableHeaderFooterRows;
					}
				}
				return tableHeaderFooterRows;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x000117F8 File Offset: 0x0000F9F8
		public TableRowsCollection DetailRows
		{
			get
			{
				Table table = (Table)base.ReportItemDef;
				TableRowsCollection tableRowsCollection = this.m_detailRows;
				if (this.m_detailRows == null && table.TableGroups == null && table.TableDetail != null)
				{
					tableRowsCollection = new TableRowsCollection(this, table.TableDetail, (base.ReportItemInstance == null) ? null : ((TableInstance)base.ReportItemInstance).TableDetailInstances);
					if (base.RenderingContext.CacheState)
					{
						this.m_detailRows = tableRowsCollection;
					}
				}
				return tableRowsCollection;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0001186D File Offset: 0x0000FA6D
		public string DetailDataElementName
		{
			get
			{
				return ((Table)base.ReportItemDef).DetailDataElementName;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0001187F File Offset: 0x0000FA7F
		public string DetailDataCollectionName
		{
			get
			{
				return ((Table)base.ReportItemDef).DetailDataCollectionName;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00011891 File Offset: 0x0000FA91
		public DataElementOutputTypes DetailDataElementOutput
		{
			get
			{
				return ((Table)base.ReportItemDef).DetailDataElementOutput;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x000118A4 File Offset: 0x0000FAA4
		public SharedHiddenState DetailSharedHidden
		{
			get
			{
				if (((Table)base.ReportItemDef).DetailGroup == null)
				{
					return Visibility.GetSharedHidden(((Table)base.ReportItemDef).TableDetail.Visibility);
				}
				return Visibility.GetSharedHidden(((Table)base.ReportItemDef).DetailGroup.Visibility);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x000118F8 File Offset: 0x0000FAF8
		public override bool NoRows
		{
			get
			{
				TableInstance tableInstance = (TableInstance)base.ReportItemInstance;
				return tableInstance == null || (tableInstance.TableGroupInstances != null && tableInstance.TableGroupInstances.Count == 0) || (tableInstance.TableDetailInstances != null && tableInstance.TableDetailInstances.Count == 0);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00011941 File Offset: 0x0000FB41
		public bool UseOWC
		{
			get
			{
				return ((Table)base.ReportItemDef).UseOWC;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00011953 File Offset: 0x0000FB53
		public bool ContainsNonSharedStyles
		{
			get
			{
				return ((Table)base.ReportItemDef).OWCNonSharedStyles;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00011965 File Offset: 0x0000FB65
		internal override string InstanceInfoNoRowMessage
		{
			get
			{
				if (base.InstanceInfo != null)
				{
					return ((TableInstanceInfo)base.InstanceInfo).NoRows;
				}
				return null;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00011981 File Offset: 0x0000FB81
		public bool FixedHeader
		{
			get
			{
				return ((Table)base.ReportItemDef).FixedHeader;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00011994 File Offset: 0x0000FB94
		internal bool HasFixedColumnHeaders
		{
			get
			{
				if (!this.FixedHeader)
				{
					return false;
				}
				Table table = (Table)base.ReportItemDef;
				if (!this.m_calculatedFixedColumnHeaders)
				{
					table.HasFixedColumnHeaders = false;
					if (table.FixedHeader)
					{
						int count = table.TableColumns.Count;
						table.HasFixedColumnHeaders = table.TableColumns[0].FixedHeader || table.TableColumns[count - 1].FixedHeader;
					}
					this.m_calculatedFixedColumnHeaders = true;
				}
				return table.HasFixedColumnHeaders;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00011A16 File Offset: 0x0000FC16
		internal Table TableDefinition
		{
			get
			{
				return (Table)base.ReportItemDef;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00011A23 File Offset: 0x0000FC23
		internal override bool Search(SearchContext searchContext)
		{
			if (base.SkipSearch)
			{
				return false;
			}
			if (this.NoRows && base.NoRowMessage != null)
			{
				return false;
			}
			if (searchContext.ItemStartPage == searchContext.ItemEndPage)
			{
				return Table.SearchFullTable(this, searchContext);
			}
			return this.SearchPartialTable(this, searchContext);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00011A60 File Offset: 0x0000FC60
		private static int TableWithVisibleColumns(TableColumnCollection columns)
		{
			int num = columns.Count;
			for (int i = 0; i < columns.Count; i++)
			{
				if (columns[i].Hidden)
				{
					num--;
				}
			}
			return num;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00011A98 File Offset: 0x0000FC98
		private static bool SearchFullTable(Table table, SearchContext searchContext)
		{
			bool flag = false;
			TableColumnCollection columns = table.Columns;
			if (Table.TableWithVisibleColumns(columns) == 0)
			{
				return flag;
			}
			flag = Table.SearchTableRows(table.TableHeader, columns, searchContext);
			if (!table.NoRows && !flag)
			{
				TableGroupCollection tableGroups = table.TableGroups;
				if (tableGroups != null)
				{
					int num = 0;
					while (!flag)
					{
						if (num >= tableGroups.Count)
						{
							break;
						}
						flag = Table.SearchFullTableGroup(tableGroups[num], columns, searchContext);
						num++;
					}
				}
				else
				{
					TableRowsCollection detailRows = table.DetailRows;
					if (detailRows != null)
					{
						int num2 = 0;
						while (!flag && num2 < detailRows.Count)
						{
							flag = Table.SearchTableRows(detailRows[num2], columns, searchContext);
							num2++;
						}
					}
				}
			}
			if (!flag)
			{
				flag = Table.SearchTableRows(table.TableFooter, columns, searchContext);
			}
			return flag;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00011B48 File Offset: 0x0000FD48
		private bool SearchPartialTable(Table table, SearchContext searchContext)
		{
			bool flag = false;
			SearchContext searchContext2 = new SearchContext(searchContext);
			TableColumnCollection columns = table.Columns;
			TableHeaderFooterRows tableHeaderFooterRows = table.TableHeader;
			if (tableHeaderFooterRows != null && (tableHeaderFooterRows.RepeatOnNewPage || searchContext.ItemStartPage == searchContext.SearchPage))
			{
				flag = Table.SearchTableRows(tableHeaderFooterRows, columns, searchContext2);
				if (flag)
				{
					return true;
				}
			}
			TableGroupCollection tableGroups = table.TableGroups;
			if (tableGroups != null)
			{
				int num = 0;
				int num2 = 0;
				int num3 = -1;
				int num4 = -1;
				this.GetTableGroupsOnPage(searchContext.SearchPage, out num, out num2);
				if (num >= 0)
				{
					SearchContext searchContext3 = new SearchContext(searchContext);
					this.IsGroupOnThisPage(num, searchContext.SearchPage, out num3, out num4);
					TableGroup tableGroup = tableGroups[num];
					if (num3 != num4)
					{
						searchContext3.ItemStartPage = num3;
						searchContext3.ItemEndPage = num4;
						flag = Table.SearchPartialTableGroup(tableGroup, columns, searchContext3);
					}
					else
					{
						flag = Table.SearchFullTableGroup(tableGroup, columns, searchContext2);
					}
					num++;
					while (!flag && num < num2)
					{
						tableGroup = tableGroups[num];
						flag = Table.SearchFullTableGroup(tableGroup, columns, searchContext2);
						num++;
					}
					if (!flag && num == num2)
					{
						this.IsGroupOnThisPage(num, searchContext.SearchPage, out num3, out num4);
						tableGroup = tableGroups[num];
						if (num3 != num4)
						{
							searchContext3.ItemStartPage = num3;
							searchContext3.ItemEndPage = num4;
							flag = Table.SearchPartialTableGroup(tableGroup, columns, searchContext3);
						}
						else
						{
							flag = Table.SearchFullTableGroup(tableGroup, columns, searchContext2);
						}
					}
				}
			}
			else
			{
				TableRowsCollection detailRows = table.DetailRows;
				if (detailRows != null)
				{
					int num5 = 0;
					int num6 = 0;
					this.GetDetailsOnThisPage(searchContext.SearchPage - searchContext.ItemStartPage, out num5, out num6);
					if (num5 >= 0)
					{
						int num7 = num5 + num6 - 1;
						while (!flag && num5 <= num7)
						{
							flag = Table.SearchTableRows(detailRows[num5], columns, searchContext2);
							num5++;
						}
					}
				}
			}
			if (flag)
			{
				return true;
			}
			tableHeaderFooterRows = table.TableFooter;
			if (tableHeaderFooterRows != null && (tableHeaderFooterRows.RepeatOnNewPage || searchContext.ItemEndPage == searchContext.SearchPage))
			{
				flag = Table.SearchTableRows(tableHeaderFooterRows, columns, searchContext2);
			}
			return flag;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00011D34 File Offset: 0x0000FF34
		private static bool SearchFullTableGroup(TableGroup tableGroup, TableColumnCollection columns, SearchContext searchContext)
		{
			bool flag = false;
			if (tableGroup == null || tableGroup.Hidden)
			{
				return flag;
			}
			flag = Table.SearchTableRows(tableGroup.GroupHeader, columns, searchContext);
			if (!flag)
			{
				TableGroupCollection subGroups = tableGroup.SubGroups;
				if (subGroups != null)
				{
					int num = 0;
					while (!flag)
					{
						if (num >= subGroups.Count)
						{
							break;
						}
						flag = Table.SearchFullTableGroup(subGroups[num], columns, searchContext);
						num++;
					}
				}
				else
				{
					TableRowsCollection detailRows = tableGroup.DetailRows;
					if (detailRows != null)
					{
						int num2 = 0;
						while (!flag && num2 < detailRows.Count)
						{
							flag = Table.SearchTableRows(detailRows[num2], columns, searchContext);
							num2++;
						}
					}
				}
			}
			if (!flag)
			{
				flag = Table.SearchTableRows(tableGroup.GroupFooter, columns, searchContext);
			}
			return flag;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00011DD4 File Offset: 0x0000FFD4
		private static bool SearchPartialTableGroup(TableGroup group, TableColumnCollection columns, SearchContext searchContext)
		{
			bool flag = false;
			SearchContext searchContext2 = new SearchContext(searchContext);
			TableHeaderFooterRows tableHeaderFooterRows = group.GroupHeader;
			if (tableHeaderFooterRows != null)
			{
				if (searchContext.SearchPage == searchContext.ItemStartPage || tableHeaderFooterRows.RepeatOnNewPage)
				{
					flag = Table.SearchTableRows(tableHeaderFooterRows, columns, searchContext2);
				}
				if (flag)
				{
					return true;
				}
			}
			TableGroupCollection subGroups = group.SubGroups;
			if (subGroups != null)
			{
				int num = 0;
				int num2 = 0;
				int num3 = -1;
				int num4 = -1;
				group.GetSubGroupsOnPage(searchContext.SearchPage, out num, out num2);
				if (num >= 0)
				{
					SearchContext searchContext3 = new SearchContext(searchContext);
					group.IsGroupOnThisPage(num, searchContext.SearchPage, out num3, out num4);
					TableGroup tableGroup = subGroups[num];
					if (num3 != num4)
					{
						searchContext3.ItemStartPage = num3;
						searchContext3.ItemEndPage = num4;
						flag = Table.SearchPartialTableGroup(tableGroup, columns, searchContext3);
					}
					else
					{
						flag = Table.SearchFullTableGroup(tableGroup, columns, searchContext2);
					}
					num++;
					while (!flag && num < num2)
					{
						tableGroup = subGroups[num];
						flag = Table.SearchFullTableGroup(tableGroup, columns, searchContext2);
						num++;
					}
					if (!flag && num == num2)
					{
						tableGroup = subGroups[num];
						group.IsGroupOnThisPage(num, searchContext.SearchPage, out num3, out num4);
						if (num3 != num4)
						{
							searchContext3.ItemStartPage = num3;
							searchContext3.ItemEndPage = num4;
							flag = Table.SearchPartialTableGroup(tableGroup, columns, searchContext3);
						}
						else
						{
							flag = Table.SearchFullTableGroup(tableGroup, columns, searchContext2);
						}
					}
				}
			}
			else
			{
				TableRowsCollection detailRows = group.DetailRows;
				if (detailRows != null)
				{
					int num5 = 0;
					int num6 = 0;
					group.GetDetailsOnThisPage(searchContext.SearchPage - searchContext.ItemStartPage, out num5, out num6);
					if (num5 >= 0)
					{
						int num7 = num5 + num6 - 1;
						while (!flag && num5 <= num7)
						{
							flag = Table.SearchTableRows(detailRows[num5], columns, searchContext2);
							num5++;
						}
					}
				}
			}
			if (flag)
			{
				return true;
			}
			tableHeaderFooterRows = group.GroupFooter;
			if (tableHeaderFooterRows != null && (tableHeaderFooterRows.RepeatOnNewPage || searchContext.ItemEndPage == searchContext.SearchPage))
			{
				flag = Table.SearchTableRows(tableHeaderFooterRows, columns, searchContext2);
			}
			return flag;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00011FB4 File Offset: 0x000101B4
		private static bool SearchTableRows(TableRowCollection tableRows, TableColumnCollection columns, SearchContext searchContext)
		{
			bool flag = false;
			if (tableRows == null)
			{
				return flag;
			}
			int num = 0;
			while (!flag && num < tableRows.Count)
			{
				TableRow tableRow = tableRows[num];
				if (!tableRow.Hidden)
				{
					flag = Table.SearchRowCells(tableRow.TableCellCollection, columns, searchContext);
				}
				num++;
			}
			return flag;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00012000 File Offset: 0x00010200
		private static bool SearchRowCells(TableCellCollection rowCells, TableColumnCollection columns, SearchContext searchContext)
		{
			bool flag = false;
			if (rowCells == null)
			{
				return flag;
			}
			int num = 0;
			while (!flag && num < rowCells.Count)
			{
				if (!columns[num].Hidden)
				{
					flag = rowCells[num].ReportItem.Search(searchContext);
				}
				num++;
			}
			return flag;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001204C File Offset: 0x0001024C
		public bool IsGroupOnThisPage(int groupIndex, int pageNumber, out int startPage, out int endPage)
		{
			startPage = -1;
			endPage = -1;
			RenderingPagesRangesList childrenStartAndEndPages = ((TableInstance)base.ReportItemInstance).ChildrenStartAndEndPages;
			if (childrenStartAndEndPages == null)
			{
				return true;
			}
			Global.Tracer.Assert(groupIndex >= 0 && groupIndex < childrenStartAndEndPages.Count);
			if (groupIndex >= childrenStartAndEndPages.Count)
			{
				return false;
			}
			RenderingPagesRanges renderingPagesRanges = childrenStartAndEndPages[groupIndex];
			startPage = renderingPagesRanges.StartPage;
			endPage = renderingPagesRanges.EndPage;
			return pageNumber >= startPage && pageNumber <= endPage;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000120C8 File Offset: 0x000102C8
		public void GetDetailsOnThisPage(int pageIndex, out int start, out int numberOfDetails)
		{
			start = 0;
			numberOfDetails = 0;
			RenderingPagesRangesList childrenStartAndEndPages = ((TableInstance)base.ReportItemInstance).ChildrenStartAndEndPages;
			if (childrenStartAndEndPages == null)
			{
				return;
			}
			Global.Tracer.Assert(pageIndex >= 0 && pageIndex < childrenStartAndEndPages.Count);
			RenderingPagesRanges renderingPagesRanges = childrenStartAndEndPages[pageIndex];
			start = renderingPagesRanges.StartRow;
			numberOfDetails = renderingPagesRanges.NumberOfDetails;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00012124 File Offset: 0x00010324
		public void GetTableGroupsOnPage(int page, out int startGroup, out int endGroup)
		{
			startGroup = -1;
			endGroup = -1;
			if (base.ReportItemInstance == null)
			{
				return;
			}
			RenderingPagesRangesList childrenStartAndEndPages = ((TableInstance)base.ReportItemInstance).ChildrenStartAndEndPages;
			if (childrenStartAndEndPages == null)
			{
				return;
			}
			RenderingContext.FindRange(childrenStartAndEndPages, 0, childrenStartAndEndPages.Count - 1, page, ref startGroup, ref endGroup);
		}

		// Token: 0x04000129 RID: 297
		private TableGroupCollection m_tableGroups;

		// Token: 0x0400012A RID: 298
		private TableRowsCollection m_detailRows;

		// Token: 0x0400012B RID: 299
		private TableHeaderFooterRows m_headerRows;

		// Token: 0x0400012C RID: 300
		private TableHeaderFooterRows m_footerRows;

		// Token: 0x0400012D RID: 301
		private TableColumnCollection m_tableColumns;

		// Token: 0x0400012E RID: 302
		private bool m_calculatedFixedColumnHeaders;
	}
}
