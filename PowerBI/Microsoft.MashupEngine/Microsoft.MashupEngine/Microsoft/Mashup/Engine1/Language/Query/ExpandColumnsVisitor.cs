using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001895 RID: 6293
	internal class ExpandColumnsVisitor : QueryVisitor
	{
		// Token: 0x06009FAA RID: 40874 RVA: 0x0020F91D File Offset: 0x0020DB1D
		public static Query ExpandColumns(Query query)
		{
			return new ExpandColumnsVisitor().VisitQuery(query);
		}

		// Token: 0x06009FAB RID: 40875 RVA: 0x0020F92A File Offset: 0x0020DB2A
		private ExpandColumnsVisitor()
		{
			this.columns = ExpandColumnsVisitor.emptyColumns;
		}

		// Token: 0x06009FAC RID: 40876 RVA: 0x0020F940 File Offset: 0x0020DB40
		protected override Query VisitDataSource(DataSourceQuery query)
		{
			TableQuery tableQuery = (TableQuery)query;
			if (this.columns.Length != 0)
			{
				return new TableQuery(tableQuery.Table, this.columns, query.EngineHost);
			}
			return query;
		}

		// Token: 0x06009FAD RID: 40877 RVA: 0x0020F978 File Offset: 0x0020DB78
		protected override Query VisitSelectColumns(ProjectColumnsQuery query)
		{
			if (this.columns.Length == 0)
			{
				return base.VisitSelectColumns(query);
			}
			int[] array = this.columns;
			this.columns = query.ColumnSelection.GetColumns(this.columns);
			Query query2 = base.VisitSelectColumns(query);
			this.columns = array;
			return query2;
		}

		// Token: 0x06009FAE RID: 40878 RVA: 0x0020F9C4 File Offset: 0x0020DBC4
		protected override Query VisitExpandRecordColumn(ExpandRecordColumnQuery query)
		{
			List<int> list = new List<int>(this.columns.Length + 1);
			for (int i = 0; i < this.columns.Length; i++)
			{
				int num = this.columns[i];
				if (num >= query.ColumnToExpand && num < query.ColumnToExpand + query.FieldsToProject.Length)
				{
					num = query.ColumnToExpand;
				}
				else if (num >= query.ColumnToExpand + query.FieldsToProject.Length)
				{
					num = num - query.FieldsToProject.Length + 1;
				}
				if (!list.Contains(num))
				{
					list.Add(num);
				}
			}
			if (!list.Contains(query.ColumnToExpand))
			{
				list.Add(query.ColumnToExpand);
			}
			int[] array = this.columns;
			this.columns = list.ToArray();
			Query query2 = base.VisitExpandRecordColumn(query);
			this.columns = array;
			return query2;
		}

		// Token: 0x06009FAF RID: 40879 RVA: 0x0020FA94 File Offset: 0x0020DC94
		protected override Query VisitExpandListColumn(ExpandListColumnQuery query)
		{
			if (this.columns.Contains(query.ColumnIndex))
			{
				return base.VisitExpandListColumn(query);
			}
			int[] array = new int[this.columns.Length + 1];
			Array.Copy(this.columns, array, this.columns.Length);
			array[array.Length - 1] = query.ColumnIndex;
			int[] array2 = this.columns;
			this.columns = array;
			Query query2 = base.VisitExpandListColumn(query);
			this.columns = array2;
			return query2;
		}

		// Token: 0x06009FB0 RID: 40880 RVA: 0x0020FB0C File Offset: 0x0020DD0C
		protected override Query VisitGroup(GroupQuery query)
		{
			List<int> list = new List<int>();
			bool[] accessed = new bool[query.InnerQuery.Columns.Length];
			Func<int, bool> <>9__0;
			for (int i = 0; i < this.columns.Length; i++)
			{
				int num = this.columns[i];
				if (num < query.Grouping.KeyColumns.Length)
				{
					list.Add(num);
				}
				else
				{
					ColumnConstructor columnConstructor = query.Grouping.Constructors[num - query.Grouping.KeyColumns.Length];
					QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewTableType(query.InnerQuery), columnConstructor.Function);
					Func<InvocationQueryExpression, bool> allow = ArgumentAccess.Allow;
					Func<int, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = delegate(int c)
						{
							accessed[c] = true;
							return true;
						});
					}
					if (!queryExpression.AllAccess(allow, func))
					{
						throw new InvalidOperationException();
					}
				}
			}
			for (int j = 0; j < accessed.Length; j++)
			{
				if (accessed[j])
				{
					list.Add(j);
				}
			}
			int[] array = this.columns;
			this.columns = list.ToArray();
			Query query2 = base.VisitGroup(query);
			this.columns = array;
			return query2;
		}

		// Token: 0x06009FB1 RID: 40881 RVA: 0x0020FC34 File Offset: 0x0020DE34
		protected override Query VisitJoin(JoinQuery query)
		{
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			JoinColumn[] joinColumns = query.JoinColumns;
			for (int i = 0; i < this.columns.Length; i++)
			{
				int num = this.columns[i];
				if (joinColumns[num].Left)
				{
					list.Add(joinColumns[num].LeftColumn);
				}
				else
				{
					list2.Add(joinColumns[num].RightColumn);
				}
			}
			int[] array = this.columns;
			this.columns = list.ToArray();
			Query query2 = this.VisitQuery(query.LeftQuery);
			this.columns = list2.ToArray();
			Query query3 = this.VisitQuery(query.RightQuery);
			this.columns = array;
			if (query2 == query.LeftQuery && query3 == query.RightQuery)
			{
				return query;
			}
			return new JoinQuery(query.TakeCount, query2, query.LeftKeyColumns, query3, query.RightKeyColumns, query.JoinKind, query.JoinKeys, query.JoinColumns, query.JoinAlgorithm, query.KeyEqualityComparers);
		}

		// Token: 0x06009FB2 RID: 40882 RVA: 0x0020FD40 File Offset: 0x0020DF40
		protected override Query VisitNestedJoin(NestedJoinQuery query)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.columns.Length; i++)
			{
				int num = this.columns[i];
				if (num < query.LeftQuery.Columns.Length)
				{
					list.Add(num);
				}
			}
			int[] array = this.columns;
			this.columns = list.ToArray();
			Query query2 = this.VisitQuery(query.LeftQuery);
			this.columns = array;
			if (query2 == query.LeftQuery)
			{
				return query;
			}
			return new NestedJoinQuery(query2, query.LeftKeyColumns, query.RightTable, query.RightKey, query.JoinKind, query.NewColumnName, query.JoinKeys, query.KeyEqualityComparers, query.ColumnType);
		}

		// Token: 0x06009FB3 RID: 40883 RVA: 0x0020FDF4 File Offset: 0x0020DFF4
		protected override Query VisitPivot(PivotQuery query)
		{
			int[] array = this.columns;
			this.columns = ExpandColumnsVisitor.emptyColumns;
			Query query2 = base.VisitPivot(query);
			this.columns = array;
			return query2;
		}

		// Token: 0x06009FB4 RID: 40884 RVA: 0x0020FE24 File Offset: 0x0020E024
		protected override Query VisitUnpivot(UnpivotQuery query)
		{
			int[] array = this.columns;
			this.columns = ExpandColumnsVisitor.emptyColumns;
			Query query2 = base.VisitUnpivot(query);
			this.columns = array;
			return query2;
		}

		// Token: 0x06009FB5 RID: 40885 RVA: 0x0020FE54 File Offset: 0x0020E054
		protected override Query VisitAddColumns(AddColumnsQuery query)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.columns.Length; i++)
			{
				int num = this.columns[i];
				if (num < query.InnerQuery.Columns.Length)
				{
					list.Add(num);
				}
			}
			IList<QueryExpression> list2 = AddColumnsQuery.CreateQueryExpressions(query.ColumnsConstructor.Function, QueryTableValue.NewRowType(query.InnerQuery));
			if (list2 != null)
			{
				bool[] accessed = new bool[query.InnerQuery.Columns.Length];
				Func<int, bool> <>9__0;
				for (int j = 0; j < list2.Count; j++)
				{
					QueryExpression queryExpression = list2[j];
					Func<InvocationQueryExpression, bool> allow = ArgumentAccess.Allow;
					Func<int, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = delegate(int column)
						{
							accessed[column] = true;
							return true;
						});
					}
					if (!queryExpression.AllAccess(allow, func))
					{
						throw new InvalidOperationException();
					}
				}
				for (int k = 0; k < accessed.Length; k++)
				{
					if (accessed[k])
					{
						list.Add(k);
					}
				}
			}
			int[] array = this.columns;
			this.columns = list.ToArray();
			Query query2 = base.VisitAddColumns(query);
			this.columns = array;
			return query2;
		}

		// Token: 0x040053B6 RID: 21430
		private static readonly int[] emptyColumns = new int[0];

		// Token: 0x040053B7 RID: 21431
		private int[] columns;
	}
}
