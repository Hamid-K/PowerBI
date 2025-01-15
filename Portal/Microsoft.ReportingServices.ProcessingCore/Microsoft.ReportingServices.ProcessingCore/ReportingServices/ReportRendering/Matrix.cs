using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000037 RID: 55
	internal sealed class Matrix : DataRegion
	{
		// Token: 0x060004DE RID: 1246 RVA: 0x0000F154 File Offset: 0x0000D354
		internal Matrix(int intUniqueName, Matrix reportItemDef, MatrixInstance reportItemInstance, RenderingContext renderingContext)
			: base(intUniqueName, reportItemDef, reportItemInstance, renderingContext)
		{
			if (reportItemInstance != null && reportItemInstance.Cells.Count != 0 && reportItemInstance.Cells[0].Count != 0)
			{
				this.m_rowMemberMapping = Matrix.CalculateMapping(reportItemDef.Rows, reportItemInstance.RowInstances, false);
				this.m_colMemberMapping = Matrix.CalculateMapping(reportItemDef.Columns, reportItemInstance.ColumnInstances, false);
				this.m_noRows = this.m_rowMemberMapping.Count == 0 || this.m_colMemberMapping.Count == 0;
				return;
			}
			this.m_noRows = true;
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000F1F9 File Offset: 0x0000D3F9
		public MatrixLayoutDirection LayoutDirection
		{
			get
			{
				if (((Matrix)base.ReportItemDef).LayoutDirection)
				{
					return MatrixLayoutDirection.RTL;
				}
				return MatrixLayoutDirection.LTR;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0000F210 File Offset: 0x0000D410
		public override bool PageBreakAtEnd
		{
			get
			{
				return ((Matrix)base.ReportItemDef).PageBreakAtEnd || ((Matrix)base.ReportItemDef).PropagatedPageBreakAtEnd;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000F236 File Offset: 0x0000D436
		public override bool PageBreakAtStart
		{
			get
			{
				return ((Matrix)base.ReportItemDef).PageBreakAtStart || ((Matrix)base.ReportItemDef).PropagatedPageBreakAtStart;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0000F25C File Offset: 0x0000D45C
		public bool GroupBreakAtStart
		{
			get
			{
				return ((Matrix)base.ReportItemDef).PropagatedPageBreakAtStart;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0000F26E File Offset: 0x0000D46E
		public bool GroupBreakAtEnd
		{
			get
			{
				return ((Matrix)base.ReportItemDef).PropagatedPageBreakAtEnd;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0000F280 File Offset: 0x0000D480
		public ReportItem Corner
		{
			get
			{
				ReportItem reportItem = this.m_corner;
				if (this.m_corner == null)
				{
					Matrix matrix = (Matrix)base.ReportItemDef;
					if (matrix.CornerReportItems != null && 0 < matrix.CornerReportItems.Count)
					{
						ReportItem reportItem2 = matrix.CornerReportItems[0];
						ReportItemInstance reportItemInstance = null;
						NonComputedUniqueNames nonComputedUniqueNames = null;
						if (base.ReportItemInstance != null)
						{
							reportItemInstance = ((MatrixInstance)base.ReportItemInstance).CornerContent;
							nonComputedUniqueNames = ((MatrixInstanceInfo)base.InstanceInfo).CornerNonComputedNames;
						}
						reportItem = ReportItem.CreateItem(0, reportItem2, reportItemInstance, base.RenderingContext, nonComputedUniqueNames);
						if (base.RenderingContext.CacheState)
						{
							this.m_corner = reportItem;
						}
					}
				}
				return reportItem;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0000F324 File Offset: 0x0000D524
		public MatrixCellCollection CellCollection
		{
			get
			{
				MatrixCellCollection matrixCellCollection = this.m_cells;
				if (this.m_cells == null)
				{
					int num;
					int num2;
					if (!this.m_noRows && base.ReportItemInstance != null && 0 < ((MatrixInstance)base.ReportItemInstance).Cells.Count)
					{
						num = ((MatrixInstance)base.ReportItemInstance).CellRowCount;
						num2 = ((MatrixInstance)base.ReportItemInstance).CellColumnCount;
					}
					else
					{
						num = ((Matrix)base.ReportItemDef).MatrixRows.Count;
						num2 = ((Matrix)base.ReportItemDef).MatrixColumns.Count;
					}
					matrixCellCollection = new MatrixCellCollection(this, num, num2);
					if (base.RenderingContext.CacheState)
					{
						this.m_cells = matrixCellCollection;
					}
				}
				return matrixCellCollection;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
		public MatrixMemberCollection ColumnMemberCollection
		{
			get
			{
				MatrixMemberCollection matrixMemberCollection = this.m_columns;
				if (this.m_columns == null)
				{
					matrixMemberCollection = new MatrixMemberCollection(this, null, ((Matrix)base.ReportItemDef).Columns, (base.ReportItemInstance == null) ? null : ((MatrixInstance)base.ReportItemInstance).ColumnInstances, this.m_colMemberMapping, false);
					if (base.RenderingContext.CacheState)
					{
						this.m_columns = matrixMemberCollection;
					}
				}
				return matrixMemberCollection;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0000F44C File Offset: 0x0000D64C
		public MatrixMemberCollection RowMemberCollection
		{
			get
			{
				MatrixMemberCollection matrixMemberCollection = this.m_rows;
				if (this.m_rows == null)
				{
					matrixMemberCollection = new MatrixMemberCollection(this, null, ((Matrix)base.ReportItemDef).Rows, (base.ReportItemInstance == null) ? null : ((MatrixInstance)base.ReportItemInstance).RowInstances, this.m_rowMemberMapping, false);
					if (base.RenderingContext.CacheState)
					{
						this.m_rows = matrixMemberCollection;
					}
				}
				return matrixMemberCollection;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000F4B7 File Offset: 0x0000D6B7
		public int CellColumns
		{
			get
			{
				if (this.m_noRows || base.ReportItemInstance == null)
				{
					return 0;
				}
				return ((MatrixInstance)base.ReportItemInstance).CellColumnCount;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000F4DB File Offset: 0x0000D6DB
		public int CellRows
		{
			get
			{
				if (this.m_noRows || base.ReportItemInstance == null)
				{
					return 0;
				}
				return ((MatrixInstance)base.ReportItemInstance).CellRowCount;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0000F4FF File Offset: 0x0000D6FF
		public int MatrixPages
		{
			get
			{
				if (this.m_noRows || base.ReportItemInstance == null)
				{
					return 0;
				}
				return ((MatrixInstance)base.ReportItemInstance).InstanceCountOfInnerRowWithPageBreak;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0000F523 File Offset: 0x0000D723
		public int PageBreakRow
		{
			get
			{
				return ((Matrix)base.ReportItemDef).InnerRowLevelWithPageBreak;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0000F535 File Offset: 0x0000D735
		public int Columns
		{
			get
			{
				return ((Matrix)base.ReportItemDef).ColumnCount;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000F547 File Offset: 0x0000D747
		public int Rows
		{
			get
			{
				return ((Matrix)base.ReportItemDef).RowCount;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0000F559 File Offset: 0x0000D759
		public override bool NoRows
		{
			get
			{
				return this.m_noRows;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000F561 File Offset: 0x0000D761
		public int GroupsBeforeRowHeaders
		{
			get
			{
				if (this.m_groupsBeforeRowHeaders < 0)
				{
					this.CalculateGroupsCellsBeforeRowHeaders();
				}
				return this.m_groupsBeforeRowHeaders;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000F578 File Offset: 0x0000D778
		public int CellsBeforeRowHeaders
		{
			get
			{
				if (this.m_cellsBeforeRowHeaders < 0)
				{
					this.CalculateGroupsCellsBeforeRowHeaders();
				}
				return this.m_cellsBeforeRowHeaders;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0000F590 File Offset: 0x0000D790
		public SizeCollection CellWidths
		{
			get
			{
				SizeCollection sizeCollection = this.m_cellWidths;
				if (this.m_cellWidths == null)
				{
					sizeCollection = new SizeCollection(this, true);
					if (base.RenderingContext.CacheState)
					{
						this.m_cellWidths = sizeCollection;
					}
				}
				return sizeCollection;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000F5CC File Offset: 0x0000D7CC
		public SizeCollection CellHeights
		{
			get
			{
				SizeCollection sizeCollection = this.m_cellHeights;
				if (this.m_cellHeights == null)
				{
					sizeCollection = new SizeCollection(this, false);
					if (base.RenderingContext.CacheState)
					{
						this.m_cellHeights = sizeCollection;
					}
				}
				return sizeCollection;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000F605 File Offset: 0x0000D805
		public bool UseOWC
		{
			get
			{
				return ((Matrix)base.ReportItemDef).UseOWC;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000F617 File Offset: 0x0000D817
		internal override string InstanceInfoNoRowMessage
		{
			get
			{
				if (base.InstanceInfo != null)
				{
					return ((MatrixInstanceInfo)base.InstanceInfo).NoRows;
				}
				return null;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000F633 File Offset: 0x0000D833
		public bool RowGroupingFixedHeader
		{
			get
			{
				return ((Matrix)base.ReportItemDef).RowGroupingFixedHeader;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000F645 File Offset: 0x0000D845
		public bool ColumnGroupingFixedHeader
		{
			get
			{
				return ((Matrix)base.ReportItemDef).ColumnGroupingFixedHeader;
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000F658 File Offset: 0x0000D858
		public bool IsRowMemberOnThisPage(int groupIndex, int pageNumber, out int startPage, out int endPage)
		{
			startPage = -1;
			endPage = -1;
			RenderingPagesRangesList childrenStartAndEndPages = ((MatrixInstance)base.ReportItemInstance).ChildrenStartAndEndPages;
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

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000F6D4 File Offset: 0x0000D8D4
		public void GetRowMembersOnPage(int page, out int startMember, out int endMember)
		{
			startMember = -1;
			endMember = -1;
			if (base.ReportItemInstance == null)
			{
				return;
			}
			RenderingPagesRangesList childrenStartAndEndPages = ((MatrixInstance)base.ReportItemInstance).ChildrenStartAndEndPages;
			if (childrenStartAndEndPages == null)
			{
				return;
			}
			RenderingContext.FindRange(childrenStartAndEndPages, 0, childrenStartAndEndPages.Count - 1, page, ref startMember, ref endMember);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000F718 File Offset: 0x0000D918
		internal override bool Search(SearchContext searchContext)
		{
			if (base.SkipSearch || this.NoRows)
			{
				return false;
			}
			IntList intList = null;
			bool flag = Matrix.SearchMatrixColumns(this.ColumnMemberCollection, ref intList, searchContext);
			if (!flag)
			{
				flag = Matrix.SearchMatrixRowsContent(this, null, searchContext, intList);
			}
			return flag;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000F758 File Offset: 0x0000D958
		private static void BuildHiddenColumns(MatrixMember member, ref IntList hiddenColumns)
		{
			if (hiddenColumns == null)
			{
				hiddenColumns = new IntList();
			}
			for (int i = 0; i < member.ColumnSpan; i++)
			{
				hiddenColumns.Add(member.MemberCellIndex + i);
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000F798 File Offset: 0x0000D998
		private static bool HiddenColumn(IntList hiddenColumns, ref int columnIndex, int cellIndex)
		{
			bool flag = false;
			if (hiddenColumns != null && columnIndex < hiddenColumns.Count)
			{
				while (columnIndex < hiddenColumns.Count && cellIndex > hiddenColumns[columnIndex])
				{
					columnIndex++;
				}
				if (cellIndex == hiddenColumns[columnIndex])
				{
					columnIndex++;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000F7E8 File Offset: 0x0000D9E8
		private static bool SearchMatrixRowsContent(Matrix matrix, MatrixMember member, SearchContext searchContext, IntList hiddenColumns)
		{
			bool flag = false;
			int num = 0;
			int num2 = 0;
			MatrixMember matrixMember = null;
			SearchContext searchContext2 = new SearchContext(searchContext);
			bool flag2 = false;
			MatrixMemberCollection matrixMemberCollection;
			if (member == null)
			{
				matrixMemberCollection = matrix.RowMemberCollection;
			}
			else
			{
				matrixMemberCollection = member.Children;
			}
			if (searchContext.ItemStartPage != searchContext.ItemEndPage)
			{
				if (member == null)
				{
					matrix.GetRowMembersOnPage(searchContext.SearchPage, out num, out num2);
				}
				else
				{
					member.GetChildRowMembersOnPage(searchContext.SearchPage, out num, out num2);
				}
				flag2 = true;
			}
			else
			{
				num = 0;
				num2 = matrixMemberCollection.Count - 1;
			}
			int num3 = num2 - num + 1;
			int num4 = num;
			while (!flag && num4 <= num2)
			{
				matrixMember = matrixMemberCollection[num4];
				if (matrixMember.Hidden)
				{
					num3--;
				}
				else
				{
					MatrixMemberCollection matrixMemberCollection2 = matrixMember.Children;
					if (matrixMemberCollection2 != null)
					{
						flag = matrixMember.ReportItem.Search(searchContext2);
						if (!flag)
						{
							if (flag2 && (num4 == num || num4 == num2))
							{
								int num5 = 0;
								int num6 = 0;
								SearchContext searchContext3 = new SearchContext(searchContext);
								if (member == null)
								{
									matrix.IsRowMemberOnThisPage(num4, searchContext.SearchPage, out num5, out num6);
								}
								else
								{
									member.IsRowMemberOnThisPage(num4, searchContext.SearchPage, out num5, out num6);
								}
								searchContext3.ItemStartPage = num5;
								searchContext3.ItemEndPage = num6;
								flag = Matrix.SearchMatrixRowsContent(matrix, matrixMember, searchContext3, hiddenColumns);
							}
							else
							{
								flag = Matrix.SearchMatrixRowsContent(matrix, matrixMember, searchContext2, hiddenColumns);
							}
						}
					}
					else
					{
						flag = matrixMember.ReportItem.Search(searchContext2);
						if (!flag)
						{
							flag = Matrix.SearchRangeCells(matrix, matrixMember.MemberCellIndex, hiddenColumns, searchContext2);
						}
					}
				}
				num4++;
			}
			if (!flag && num3 == 0)
			{
				if (!matrixMember.IsTotal)
				{
					matrixMember = matrixMemberCollection[0];
				}
				MatrixMemberCollection matrixMemberCollection2 = matrixMember.Children;
				if (matrixMemberCollection2 != null)
				{
					flag = Matrix.SearchRowTotal(matrix, matrixMemberCollection2, hiddenColumns, searchContext2);
				}
				else
				{
					flag = Matrix.SearchRangeCells(matrix, matrixMember.MemberCellIndex, hiddenColumns, searchContext2);
				}
			}
			return flag;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000F9AC File Offset: 0x0000DBAC
		private static bool SearchMatrixColumns(MatrixMemberCollection columns, ref IntList hiddenColumns, SearchContext searchContext)
		{
			if (columns == null)
			{
				return false;
			}
			bool flag = false;
			int num = 0;
			int num2 = columns.Count - 1;
			MatrixMember matrixMember = null;
			SearchContext searchContext2 = new SearchContext(searchContext);
			int num3 = 0;
			int num4 = 0;
			int num5 = num2 - num + 1;
			int num6 = num;
			while (!flag && num6 <= num2)
			{
				matrixMember = columns[num6];
				if (matrixMember.Hidden)
				{
					if (matrixMember.IsTotal)
					{
						if (hiddenColumns != null)
						{
							num3 = hiddenColumns.Count;
						}
						num4 = matrixMember.ColumnSpan;
					}
					Matrix.BuildHiddenColumns(matrixMember, ref hiddenColumns);
					num5--;
				}
				else
				{
					flag = matrixMember.ReportItem.Search(searchContext2);
					if (!flag)
					{
						MatrixMemberCollection matrixMemberCollection = matrixMember.Children;
						flag = Matrix.SearchMatrixColumns(matrixMemberCollection, ref hiddenColumns, searchContext2);
					}
				}
				num6++;
			}
			if (num5 == 0)
			{
				hiddenColumns.RemoveRange(num3, num4);
				if (!flag)
				{
					if (!matrixMember.IsTotal)
					{
						matrixMember = columns[0];
					}
					MatrixMemberCollection matrixMemberCollection = matrixMember.Children;
					if (matrixMemberCollection != null)
					{
						int num7 = 0;
						while (!flag && num7 < matrixMemberCollection.Count)
						{
							matrixMember = matrixMemberCollection[num7];
							flag = matrixMember.ReportItem.Search(searchContext2);
							num7++;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000FAC4 File Offset: 0x0000DCC4
		private static bool SearchRangeCells(Matrix matrix, int indexRow, IntList hiddenColumns, SearchContext searchContext)
		{
			int num = 0;
			bool flag = false;
			MatrixCellCollection cellCollection = matrix.CellCollection;
			int num2 = 0;
			while (!flag && num2 < matrix.CellColumns)
			{
				if (!Matrix.HiddenColumn(hiddenColumns, ref num, num2))
				{
					flag = cellCollection[indexRow, num2].ReportItem.Search(searchContext);
				}
				num2++;
			}
			return flag;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000FB14 File Offset: 0x0000DD14
		private static bool SearchRowTotal(Matrix matrix, MatrixMemberCollection rowTotalChildren, IntList hiddenColumns, SearchContext searchContext)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < rowTotalChildren.Count)
			{
				MatrixMember matrixMember = rowTotalChildren[num];
				flag = matrixMember.ReportItem.Search(searchContext);
				if (!flag)
				{
					flag = Matrix.SearchRangeCells(matrix, matrixMember.MemberCellIndex, hiddenColumns, searchContext);
				}
				num++;
			}
			return flag;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000FB64 File Offset: 0x0000DD64
		internal static List<int> CalculateMapping(MatrixHeading headingDef, MatrixHeadingInstanceList headingInstances, bool inParentSubtotal)
		{
			List<int> list = new List<int>();
			if (headingInstances == null)
			{
				return list;
			}
			bool flag = true;
			for (int i = 0; i < headingInstances.Count; i++)
			{
				if (inParentSubtotal || headingInstances[i].IsSubtotal || !Matrix.IsEmpty(headingDef, headingInstances[i]))
				{
					if (!headingInstances[i].IsSubtotal)
					{
						flag = false;
					}
					list.Add(i);
				}
			}
			if (flag && list.Count <= 1)
			{
				list.Clear();
			}
			return list;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000FBDC File Offset: 0x0000DDDC
		private static bool IsEmpty(MatrixHeading headingDef, MatrixHeadingInstance headingInstance)
		{
			if (headingDef == null || headingDef.SubHeading == null)
			{
				return false;
			}
			if (headingInstance.SubHeadingInstances == null || headingInstance.SubHeadingInstances.Count == 0)
			{
				return true;
			}
			int count = headingInstance.SubHeadingInstances.Count;
			bool flag = true;
			int num = 0;
			while (num < count && flag)
			{
				flag = Matrix.IsEmpty(headingDef.SubHeading, headingInstance.SubHeadingInstances[num]);
				num++;
			}
			return flag;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000FC44 File Offset: 0x0000DE44
		private void CalculateGroupsCellsBeforeRowHeaders()
		{
			this.m_groupsBeforeRowHeaders = ((Matrix)base.ReportItemDef).GroupsBeforeRowHeaders;
			if (this.m_groupsBeforeRowHeaders > 0 && base.ReportItemInstance != null)
			{
				MatrixHeadingInstanceList columnInstances = ((MatrixInstance)base.ReportItemInstance).ColumnInstances;
				MatrixMemberCollection matrixMemberCollection = null;
				int num = -1;
				if (columnInstances != null && 0 < columnInstances.Count)
				{
					num = columnInstances.Count - 1;
					if (columnInstances[0].IsSubtotal || (this.m_groupsBeforeRowHeaders == num && columnInstances[num].IsSubtotal))
					{
						this.m_groupsBeforeRowHeaders++;
					}
				}
				this.m_cellsBeforeRowHeaders = 0;
				if (this.m_groupsBeforeRowHeaders > num + 1)
				{
					this.m_groupsBeforeRowHeaders = 0;
				}
				else
				{
					matrixMemberCollection = this.ColumnMemberCollection;
				}
				for (int i = 0; i < this.m_groupsBeforeRowHeaders; i++)
				{
					this.m_cellsBeforeRowHeaders += matrixMemberCollection[i].ColumnSpan;
				}
				return;
			}
			this.m_groupsBeforeRowHeaders = 0;
			this.m_cellsBeforeRowHeaders = 0;
		}

		// Token: 0x040000FA RID: 250
		private ReportItem m_corner;

		// Token: 0x040000FB RID: 251
		private MatrixCellCollection m_cells;

		// Token: 0x040000FC RID: 252
		private MatrixMemberCollection m_columns;

		// Token: 0x040000FD RID: 253
		private MatrixMemberCollection m_rows;

		// Token: 0x040000FE RID: 254
		private int m_groupsBeforeRowHeaders = -1;

		// Token: 0x040000FF RID: 255
		private int m_cellsBeforeRowHeaders = -1;

		// Token: 0x04000100 RID: 256
		private SizeCollection m_cellWidths;

		// Token: 0x04000101 RID: 257
		private SizeCollection m_cellHeights;

		// Token: 0x04000102 RID: 258
		private bool m_noRows;

		// Token: 0x04000103 RID: 259
		private List<int> m_rowMemberMapping;

		// Token: 0x04000104 RID: 260
		private List<int> m_colMemberMapping;
	}
}
