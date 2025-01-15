using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007CC RID: 1996
	internal class ODataOptimizingQueryVisitor : OptimizingQueryVisitor
	{
		// Token: 0x06003A13 RID: 14867 RVA: 0x000BBA9A File Offset: 0x000B9C9A
		public ODataOptimizingQueryVisitor()
		{
			this.expandedColumns = EmptyArray<ODataOptimizingQueryVisitor.ExpandedColumnInfo>.Instance;
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x000BBAB0 File Offset: 0x000B9CB0
		protected override Query VisitDataSource(DataSourceQuery query)
		{
			Query query2 = base.VisitDataSource(query);
			ODataQuery odataQuery = query2 as ODataQuery;
			if (odataQuery == null)
			{
				return query2;
			}
			List<ODataExpandedColumn> list = this.expandedColumns.Select(new Func<ODataOptimizingQueryVisitor.ExpandedColumnInfo, ODataExpandedColumn>(ODataOptimizingQueryVisitor.BuildExpandedColumn)).ToList<ODataExpandedColumn>();
			if (list.Count > 0)
			{
				return odataQuery.ExpandColumns(list);
			}
			return odataQuery;
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x000BBB00 File Offset: 0x000B9D00
		protected override Query VisitSelectColumns(ProjectColumnsQuery query)
		{
			List<ODataOptimizingQueryVisitor.ExpandedColumnInfo> list = this.expandedColumns.Select(delegate(ODataOptimizingQueryVisitor.ExpandedColumnInfo expansion)
			{
				int column = query.ColumnSelection.GetColumn(expansion.ColumnToExpand);
				return new ODataOptimizingQueryVisitor.ExpandedColumnInfo(column, query.InnerQuery.Columns[column], expansion.FieldsToProject, expansion.InnerExpandedColumns, expansion.SelectRowsCondition);
			}).ToList<ODataOptimizingQueryVisitor.ExpandedColumnInfo>();
			return this.WithExpansions(list, () => this.<>n__0(query));
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x000BBB54 File Offset: 0x000B9D54
		protected override Query VisitSelectRows(SelectRowsQuery query)
		{
			List<ODataOptimizingQueryVisitor.ExpandedColumnInfo> list = this.expandedColumns.Select(delegate(ODataOptimizingQueryVisitor.ExpandedColumnInfo expansion)
			{
				if (query.QueryExpression.AllAccess(ArgumentAccess.Deny, (int column) => column != expansion.ColumnToExpand))
				{
					return expansion;
				}
				return ODataOptimizingQueryVisitor.SelectAll(expansion, query.InnerQuery);
			}).ToList<ODataOptimizingQueryVisitor.ExpandedColumnInfo>();
			return this.WithExpansions(list, () => this.<>n__1(query));
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x000BBBA8 File Offset: 0x000B9DA8
		protected override Query VisitSkipTake(SkipTakeQuery query)
		{
			return this.WithExpansions(this.expandedColumns, () => this.<>n__2(query));
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000BBBE4 File Offset: 0x000B9DE4
		protected override Query VisitAddColumns(AddColumnsQuery query)
		{
			List<ODataOptimizingQueryVisitor.ExpandedColumnInfo> list = this.expandedColumns.Where((ODataOptimizingQueryVisitor.ExpandedColumnInfo expansion) => expansion.ColumnToExpand < query.InnerQuery.Columns.Length).Select(delegate(ODataOptimizingQueryVisitor.ExpandedColumnInfo expansion)
			{
				Func<int, bool> <>9__4;
				if (query.QueryExpressions.All(delegate(QueryExpression expression)
				{
					Func<InvocationQueryExpression, bool> deny = ArgumentAccess.Deny;
					Func<int, bool> func;
					if ((func = <>9__4) == null)
					{
						func = (<>9__4 = (int column) => column != expansion.ColumnToExpand);
					}
					return expression.AllAccess(deny, func);
				}))
				{
					return expansion;
				}
				return ODataOptimizingQueryVisitor.SelectAll(expansion, query.InnerQuery);
			}).ToList<ODataOptimizingQueryVisitor.ExpandedColumnInfo>();
			return this.WithExpansions(list, () => this.<>n__3(query));
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x000BBC48 File Offset: 0x000B9E48
		protected override Query VisitDistinct(DistinctQuery query)
		{
			int[] array;
			IEnumerable<ODataOptimizingQueryVisitor.ExpandedColumnInfo> enumerable;
			if (query.DistinctCriteria.TryGetColumns(QueryTableValue.NewRowType(query), out array))
			{
				HashSet<int> distinctColumnsSet = new HashSet<int>(array);
				enumerable = this.expandedColumns.Select(delegate(ODataOptimizingQueryVisitor.ExpandedColumnInfo expansion)
				{
					if (!distinctColumnsSet.Contains(expansion.ColumnToExpand))
					{
						return expansion;
					}
					return ODataOptimizingQueryVisitor.SelectAll(expansion, query);
				}).ToList<ODataOptimizingQueryVisitor.ExpandedColumnInfo>();
			}
			else
			{
				enumerable = this.expandedColumns.Select((ODataOptimizingQueryVisitor.ExpandedColumnInfo expansion) => ODataOptimizingQueryVisitor.SelectAll(expansion, query.InnerQuery)).ToList<ODataOptimizingQueryVisitor.ExpandedColumnInfo>();
			}
			return this.WithExpansions(enumerable, () => this.<>n__4(query));
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x000BBCF0 File Offset: 0x000B9EF0
		protected override Query VisitSort(SortQuery query)
		{
			QueryExpression[] selectors;
			bool[] array;
			IEnumerable<ODataOptimizingQueryVisitor.ExpandedColumnInfo> enumerable;
			if (SortQuery.TryGetSelectors(query.SortOrder, QueryTableValue.NewRowType(query), out selectors, out array))
			{
				enumerable = this.expandedColumns.Select(delegate(ODataOptimizingQueryVisitor.ExpandedColumnInfo expansion)
				{
					Func<int, bool> <>9__4;
					if (selectors.All(delegate(QueryExpression expression)
					{
						Func<InvocationQueryExpression, bool> deny = ArgumentAccess.Deny;
						Func<int, bool> func;
						if ((func = <>9__4) == null)
						{
							func = (<>9__4 = (int column) => column != expansion.ColumnToExpand);
						}
						return expression.AllAccess(deny, func);
					}))
					{
						return expansion;
					}
					return ODataOptimizingQueryVisitor.SelectAll(expansion, query);
				}).ToList<ODataOptimizingQueryVisitor.ExpandedColumnInfo>();
			}
			else
			{
				enumerable = this.expandedColumns.Select((ODataOptimizingQueryVisitor.ExpandedColumnInfo expansion) => ODataOptimizingQueryVisitor.SelectAll(expansion, query.InnerQuery)).ToList<ODataOptimizingQueryVisitor.ExpandedColumnInfo>();
			}
			return this.WithExpansions(enumerable, () => this.<>n__5(query));
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000BBD88 File Offset: 0x000B9F88
		protected override Query VisitGroup(GroupQuery query)
		{
			return this.WithExpansions(EmptyArray<ODataOptimizingQueryVisitor.ExpandedColumnInfo>.Instance, () => this.<>n__6(query));
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x000BBDC0 File Offset: 0x000B9FC0
		protected override Query VisitCombine(CombineQuery query)
		{
			return this.WithExpansions(EmptyArray<ODataOptimizingQueryVisitor.ExpandedColumnInfo>.Instance, () => this.<>n__7(query));
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x000BBDF8 File Offset: 0x000B9FF8
		protected override Query VisitExpandRecordColumn(ExpandRecordColumnQuery query)
		{
			return this.VisitExpandRecordColumn(query, null);
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x000BBE04 File Offset: 0x000BA004
		private Query VisitExpandRecordColumn(ExpandRecordColumnQuery query, FunctionValue selectRowsCondition)
		{
			List<ODataOptimizingQueryVisitor.ExpandedColumnInfo> list = new List<ODataOptimizingQueryVisitor.ExpandedColumnInfo>();
			List<ODataOptimizingQueryVisitor.ExpandedColumnInfo> list2 = new List<ODataOptimizingQueryVisitor.ExpandedColumnInfo>();
			foreach (ODataOptimizingQueryVisitor.ExpandedColumnInfo expandedColumnInfo in this.expandedColumns)
			{
				if (query.ColumnToExpand <= expandedColumnInfo.ColumnToExpand && expandedColumnInfo.ColumnToExpand < query.ColumnToExpand + query.FieldsToProject.Length)
				{
					int num = expandedColumnInfo.ColumnToExpand - query.ColumnToExpand;
					list2.Add(new ODataOptimizingQueryVisitor.ExpandedColumnInfo(num, query.FieldsToProject[num], expandedColumnInfo.FieldsToProject, expandedColumnInfo.InnerExpandedColumns, expandedColumnInfo.SelectRowsCondition));
				}
				else
				{
					int num2 = ((expandedColumnInfo.ColumnToExpand < query.ColumnToExpand) ? expandedColumnInfo.ColumnToExpand : (expandedColumnInfo.ColumnToExpand - query.FieldsToProject.Length + 1));
					list.Add(new ODataOptimizingQueryVisitor.ExpandedColumnInfo(num2, expandedColumnInfo.ColumnToExpandName, expandedColumnInfo.FieldsToProject, expandedColumnInfo.InnerExpandedColumns, expandedColumnInfo.SelectRowsCondition));
				}
			}
			list.Add(new ODataOptimizingQueryVisitor.ExpandedColumnInfo(query.ColumnToExpand, query.InnerQuery.Columns[query.ColumnToExpand], query.FieldsToProject, list2, selectRowsCondition));
			return this.WithExpansions(list, () => this.<>n__8(query));
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x000BBFBC File Offset: 0x000BA1BC
		protected override Query VisitExpandListColumn(ExpandListColumnQuery query)
		{
			return this.WithExpansions(this.expandedColumns, () => this.<>n__9(query));
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000BBFF8 File Offset: 0x000BA1F8
		protected override Query VisitJoin(JoinQuery query)
		{
			List<ODataOptimizingQueryVisitor.ExpandedColumnInfo> list = new List<ODataOptimizingQueryVisitor.ExpandedColumnInfo>();
			List<ODataOptimizingQueryVisitor.ExpandedColumnInfo> list2 = new List<ODataOptimizingQueryVisitor.ExpandedColumnInfo>();
			HashSet<int> hashSet = new HashSet<int>(query.LeftKeyColumns);
			HashSet<int> hashSet2 = new HashSet<int>(query.RightKeyColumns);
			foreach (ODataOptimizingQueryVisitor.ExpandedColumnInfo expandedColumnInfo in this.expandedColumns)
			{
				JoinColumn joinColumn = query.JoinColumns[expandedColumnInfo.ColumnToExpand];
				if (joinColumn.Left)
				{
					ODataOptimizingQueryVisitor.ExpandedColumnInfo expandedColumnInfo2 = new ODataOptimizingQueryVisitor.ExpandedColumnInfo(joinColumn.LeftColumn, query.LeftQuery.Columns[joinColumn.LeftColumn], expandedColumnInfo.FieldsToProject, expandedColumnInfo.InnerExpandedColumns, expandedColumnInfo.SelectRowsCondition);
					if (hashSet.Contains(expandedColumnInfo2.ColumnToExpand))
					{
						expandedColumnInfo2 = ODataOptimizingQueryVisitor.SelectAll(expandedColumnInfo2, query.LeftQuery);
					}
					list.Add(expandedColumnInfo2);
				}
				if (joinColumn.Right)
				{
					ODataOptimizingQueryVisitor.ExpandedColumnInfo expandedColumnInfo3 = new ODataOptimizingQueryVisitor.ExpandedColumnInfo(joinColumn.RightColumn, query.RightQuery.Columns[joinColumn.RightColumn], expandedColumnInfo.FieldsToProject, expandedColumnInfo.InnerExpandedColumns, expandedColumnInfo.SelectRowsCondition);
					if (hashSet2.Contains(expandedColumnInfo3.ColumnToExpand))
					{
						expandedColumnInfo3 = ODataOptimizingQueryVisitor.SelectAll(expandedColumnInfo3, query.RightQuery);
					}
					list2.Add(expandedColumnInfo3);
				}
			}
			Query query2 = this.WithExpansions(list, () => this.VisitQuery(query.LeftQuery));
			Query query3 = this.WithExpansions(list2, () => this.VisitQuery(query.RightQuery));
			if (query2 == query.LeftQuery && query3 == query.RightQuery)
			{
				return query;
			}
			return new JoinQuery(query.TakeCount, query2, query.LeftKeyColumns, query3, query.RightKeyColumns, query.JoinKind, query.JoinKeys, query.JoinColumns, query.JoinAlgorithm, query.KeyEqualityComparers);
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000BC24C File Offset: 0x000BA44C
		protected override Query VisitNestedJoin(NestedJoinQuery query)
		{
			HashSet<int> keyColumns = new HashSet<int>(query.LeftKeyColumns);
			List<ODataOptimizingQueryVisitor.ExpandedColumnInfo> list = this.expandedColumns.Where((ODataOptimizingQueryVisitor.ExpandedColumnInfo expansion) => expansion.ColumnToExpand < query.LeftQuery.Columns.Length).Select(delegate(ODataOptimizingQueryVisitor.ExpandedColumnInfo expansion)
			{
				if (!keyColumns.Contains(expansion.ColumnToExpand))
				{
					return expansion;
				}
				return ODataOptimizingQueryVisitor.SelectAll(expansion, query.LeftQuery);
			}).ToList<ODataOptimizingQueryVisitor.ExpandedColumnInfo>();
			return this.WithExpansions(list, () => this.<>n__10(query));
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x000BC2C4 File Offset: 0x000BA4C4
		protected override Query VisitPivot(PivotQuery query)
		{
			return this.WithExpansions(EmptyArray<ODataOptimizingQueryVisitor.ExpandedColumnInfo>.Instance, () => this.<>n__11(query));
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x000BC2FC File Offset: 0x000BA4FC
		protected override Query VisitUnpivot(UnpivotQuery query)
		{
			return this.WithExpansions(EmptyArray<ODataOptimizingQueryVisitor.ExpandedColumnInfo>.Instance, () => this.<>n__12(query));
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x000BC334 File Offset: 0x000BA534
		private Query WithExpansions(IEnumerable<ODataOptimizingQueryVisitor.ExpandedColumnInfo> expansions, Func<Query> operation)
		{
			IEnumerable<ODataOptimizingQueryVisitor.ExpandedColumnInfo> enumerable = this.expandedColumns;
			this.expandedColumns = expansions;
			Query query;
			try
			{
				query = operation();
			}
			finally
			{
				this.expandedColumns = enumerable;
			}
			return query;
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x000BC374 File Offset: 0x000BA574
		private static ODataOptimizingQueryVisitor.ExpandedColumnInfo SelectAll(ODataOptimizingQueryVisitor.ExpandedColumnInfo expansion, Query query)
		{
			TypeValue columnType = query.GetColumnType(expansion.ColumnToExpand);
			return ODataOptimizingQueryVisitor.SelectAll(expansion, columnType);
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x000BC398 File Offset: 0x000BA598
		private static ODataOptimizingQueryVisitor.ExpandedColumnInfo SelectAll(ODataOptimizingQueryVisitor.ExpandedColumnInfo expansion, TypeValue columnType)
		{
			RecordTypeValue recordType = (columnType.IsTableType ? columnType.AsTableType.ItemType : columnType.AsRecordType);
			return new ODataOptimizingQueryVisitor.ExpandedColumnInfo(expansion.ColumnToExpand, expansion.ColumnToExpandName, recordType.Fields.Keys, expansion.InnerExpandedColumns.Select((ODataOptimizingQueryVisitor.ExpandedColumnInfo column) => ODataOptimizingQueryVisitor.SelectAll(column, recordType.Fields[column.ColumnToExpand]["Type"].AsType)), null);
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x000BC405 File Offset: 0x000BA605
		private static ODataExpandedColumn BuildExpandedColumn(ODataOptimizingQueryVisitor.ExpandedColumnInfo expandedColumnInfo)
		{
			return new ODataExpandedColumn(expandedColumnInfo.ColumnToExpandName, expandedColumnInfo.FieldsToProject, expandedColumnInfo.InnerExpandedColumns.Select(new Func<ODataOptimizingQueryVisitor.ExpandedColumnInfo, ODataExpandedColumn>(ODataOptimizingQueryVisitor.BuildExpandedColumn)).ToList<ODataExpandedColumn>(), expandedColumnInfo.SelectRowsCondition);
		}

		// Token: 0x04001E24 RID: 7716
		private IEnumerable<ODataOptimizingQueryVisitor.ExpandedColumnInfo> expandedColumns;

		// Token: 0x020007CD RID: 1997
		private class ExpandedColumnInfo
		{
			// Token: 0x06003A35 RID: 14901 RVA: 0x000BC4AF File Offset: 0x000BA6AF
			public ExpandedColumnInfo(int columnToExpand, string columnToExpandName, Keys fieldsToProject, IEnumerable<ODataOptimizingQueryVisitor.ExpandedColumnInfo> innerExpandedColumns, FunctionValue selectRowsCondition)
			{
				this.columnToExpand = columnToExpand;
				this.columnToExpandName = columnToExpandName;
				this.fieldsToProject = fieldsToProject;
				this.innerExpandedColumns = innerExpandedColumns;
				this.selectRowsCondition = selectRowsCondition;
			}

			// Token: 0x17001393 RID: 5011
			// (get) Token: 0x06003A36 RID: 14902 RVA: 0x000BC4DC File Offset: 0x000BA6DC
			public int ColumnToExpand
			{
				get
				{
					return this.columnToExpand;
				}
			}

			// Token: 0x17001394 RID: 5012
			// (get) Token: 0x06003A37 RID: 14903 RVA: 0x000BC4E4 File Offset: 0x000BA6E4
			public string ColumnToExpandName
			{
				get
				{
					return this.columnToExpandName;
				}
			}

			// Token: 0x17001395 RID: 5013
			// (get) Token: 0x06003A38 RID: 14904 RVA: 0x000BC4EC File Offset: 0x000BA6EC
			public Keys FieldsToProject
			{
				get
				{
					return this.fieldsToProject;
				}
			}

			// Token: 0x17001396 RID: 5014
			// (get) Token: 0x06003A39 RID: 14905 RVA: 0x000BC4F4 File Offset: 0x000BA6F4
			public IEnumerable<ODataOptimizingQueryVisitor.ExpandedColumnInfo> InnerExpandedColumns
			{
				get
				{
					return this.innerExpandedColumns;
				}
			}

			// Token: 0x17001397 RID: 5015
			// (get) Token: 0x06003A3A RID: 14906 RVA: 0x000BC4FC File Offset: 0x000BA6FC
			public FunctionValue SelectRowsCondition
			{
				get
				{
					return this.selectRowsCondition;
				}
			}

			// Token: 0x04001E25 RID: 7717
			private readonly int columnToExpand;

			// Token: 0x04001E26 RID: 7718
			private readonly string columnToExpandName;

			// Token: 0x04001E27 RID: 7719
			private readonly Keys fieldsToProject;

			// Token: 0x04001E28 RID: 7720
			private readonly IEnumerable<ODataOptimizingQueryVisitor.ExpandedColumnInfo> innerExpandedColumns;

			// Token: 0x04001E29 RID: 7721
			private readonly FunctionValue selectRowsCondition;
		}
	}
}
