using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000622 RID: 1570
	internal class OdbcQuery : DataSourceQuery
	{
		// Token: 0x0600315D RID: 12637 RVA: 0x00096268 File Offset: 0x00094468
		public OdbcQuery(OdbcQueryDomain domain, OdbcQueryColumnInfo[] columns, OdbcQuerySpecification querySpecification, IList<TableKey> tableKeys, bool canInlineFrom, TableSortOrder tableSortOrder, RowRange rowRange, string catalogName = null)
		{
			this.columns = columns;
			this.queryDomain = domain;
			this.rowRange = rowRange;
			this.tableKeys = tableKeys;
			this.canInlineFrom = canInlineFrom;
			this.tableSortOrder = tableSortOrder ?? TableSortOrder.Unknown;
			this.querySpecification = querySpecification;
			this.trace = domain.OdbcFoldingScope;
			this.catalogName = catalogName;
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x000962CD File Offset: 0x000944CD
		public override IList<TableKey> TableKeys
		{
			get
			{
				return this.tableKeys;
			}
		}

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x000962D5 File Offset: 0x000944D5
		public override Keys Columns
		{
			get
			{
				return this.ItemType.Fields.Keys;
			}
		}

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x000962E7 File Offset: 0x000944E7
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.queryDomain;
			}
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x000962EF File Offset: 0x000944EF
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.tableSortOrder;
			}
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x000962F7 File Offset: 0x000944F7
		public RowRange RowRange
		{
			get
			{
				return this.rowRange;
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x000962FF File Offset: 0x000944FF
		public OdbcQuerySpecification QuerySpecification
		{
			get
			{
				return this.querySpecification;
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x06003164 RID: 12644 RVA: 0x00096307 File Offset: 0x00094507
		public bool CanInlineFrom
		{
			get
			{
				return this.canInlineFrom;
			}
		}

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06003165 RID: 12645 RVA: 0x0009630F File Offset: 0x0009450F
		public OdbcQueryColumnInfo[] ColumnInfos
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06003166 RID: 12646 RVA: 0x00096317 File Offset: 0x00094517
		public override IEngineHost EngineHost
		{
			get
			{
				return this.DataSource.Host;
			}
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x00096324 File Offset: 0x00094524
		private RecordTypeValue ItemType
		{
			get
			{
				if (this.itemType == null)
				{
					this.itemType = this.GetTypeFromQuerySpecificationSelectItem((OdbcQueryColumnInfo columnInfo) => columnInfo.AscribedTypeValue, (bool? isNullable) => LogicalValue.False);
				}
				return this.itemType;
			}
		}

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x0009638C File Offset: 0x0009458C
		private RecordTypeValue ReaderItemType
		{
			get
			{
				if (this.readerItemType == null)
				{
					this.readerItemType = this.GetTypeFromQuerySpecificationSelectItem((OdbcQueryColumnInfo columnInfo) => columnInfo.TypeInfo.TypeValue, (bool? isNullable) => LogicalValue.New(isNullable.GetValueOrDefault()));
				}
				return this.readerItemType;
			}
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06003169 RID: 12649 RVA: 0x000963F1 File Offset: 0x000945F1
		private OdbcDataSource DataSource
		{
			get
			{
				return this.queryDomain.DataSource;
			}
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x000963FE File Offset: 0x000945FE
		private OdbcStatementBuilder StatementBuilder
		{
			get
			{
				if (this.statementBuilder == null)
				{
					this.statementBuilder = new OdbcStatementBuilder(new QueryTableValue(this), this.columns);
				}
				return this.statementBuilder;
			}
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x00096425 File Offset: 0x00094625
		public override TypeValue GetColumnType(int column)
		{
			return this.ItemType.Fields[column]["Type"].AsType;
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x00096448 File Offset: 0x00094648
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			Query query;
			using (this.trace.NewScope("SelectColumns"))
			{
				if (columnSelection.Keys.Length == this.Columns.Length)
				{
					query = this;
				}
				else
				{
					if (columnSelection.Keys.Length == 0)
					{
						throw this.trace.NewFoldingFailureException("The collection of selected columns is empty.");
					}
					OdbcQuerySpecification odbcQuerySpecification;
					bool flag;
					if (this.querySpecification.RepeatedRowOption != RepeatedRowOption.Distinct)
					{
						if (this.querySpecification.GroupByClause != null)
						{
							if (this.Hides(columnSelection, this.querySpecification.GroupByClause.Items.Select((GroupByItem g) => ((ColumnReference)g.Expression).Name)))
							{
								goto IL_00F4;
							}
						}
						if (this.querySpecification.OrderByClause != null)
						{
							if (this.Hides(columnSelection, this.querySpecification.OrderByClause.OrderByItems.Select((OrderByItem i) => i.SortColumn.Name)))
							{
								goto IL_00F4;
							}
						}
						odbcQuerySpecification = this.querySpecification.ShallowCopy();
						odbcQuerySpecification.OrderByClause = null;
						flag = false;
						goto IL_0116;
					}
					IL_00F4:
					odbcQuerySpecification = this.PushDownQuerySpecification(false, false);
					flag = true;
					IL_0116:
					SelectItem[] array = odbcQuerySpecification.SelectItems.ToArray<SelectItem>();
					odbcQuerySpecification.SelectItems = new SelectItem[columnSelection.Keys.Length];
					OdbcQueryColumnInfo[] array2 = new OdbcQueryColumnInfo[columnSelection.Keys.Length];
					for (int k = 0; k < columnSelection.Keys.Length; k++)
					{
						int column = columnSelection.GetColumn(k);
						odbcQuerySpecification.SelectItems[k] = array[column];
						array2[k] = this.columns[column];
					}
					List<ColumnReference> list = new List<ColumnReference>();
					if (this.querySpecification.OrderByClause != null)
					{
						HashSet<string> hashSet = new HashSet<string>();
						foreach (SelectItem selectItem in odbcQuerySpecification.SelectItems)
						{
							hashSet.Add(selectItem.Name.Name);
						}
						OrderByClause orderByClause = new OrderByClause();
						foreach (OrderByItem orderByItem in this.querySpecification.OrderByClause.OrderByItems)
						{
							ColumnReference sortColumn = orderByItem.SortColumn;
							if (sortColumn.Qualifier != null)
							{
								if (flag)
								{
									throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, string>>(OdbcFoldingWarnings.QualifierAtOrderByColumn(sortColumn));
								}
								orderByClause.OrderByItems.Add(orderByItem);
							}
							else if (hashSet.Contains(sortColumn.Name.Name))
							{
								orderByClause.OrderByItems.Add(orderByItem);
							}
							else
							{
								ColumnReference columnReference = new ColumnReference(this.queryDomain.InnerTableAlias, sortColumn.Name);
								if (this.DataSource.Info.OrderByColumnsInSelect)
								{
									list.Add(columnReference);
								}
								orderByClause.OrderByItems.Add(new OrderByItem(columnReference, orderByItem.Order));
							}
						}
						odbcQuerySpecification.OrderByClause = orderByClause;
					}
					if (list.Count > 0)
					{
						int count = odbcQuerySpecification.SelectItems.Count;
						SelectItem[] array3 = new SelectItem[count + list.Count];
						Array.Copy(odbcQuerySpecification.SelectItems.ToArray<SelectItem>(), array3, odbcQuerySpecification.SelectItems.Count);
						for (int j = 0; j < list.Count; j++)
						{
							array3[count + j] = new SelectItem(list[j]);
						}
						odbcQuerySpecification.SelectItems = array3;
					}
					Query query2 = ProjectColumnsQuery.New(columnSelection, this);
					Query query3 = this.queryDomain.NewQuery(array2, odbcQuerySpecification, query2.TableKeys, this.canInlineFrom, query2.SortOrder, this.rowRange);
					query = ((list.Count > 0) ? FloatingSelectColumnsQuery.New(new ColumnSelection(columnSelection.Keys), query3) : query3);
				}
			}
			return query;
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x00096864 File Offset: 0x00094A64
		public override Query Take(RowCount count)
		{
			if (count.IsInfinite)
			{
				return this;
			}
			if (count.IsZero)
			{
				return ListValue.Empty.ToTable(TableTypeValue.New(this.ItemType)).Query;
			}
			return this.queryDomain.NewQuery(this.columns, this.QuerySpecification, this.tableKeys, false, this.tableSortOrder, this.rowRange.Take(count));
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000968D4 File Offset: 0x00094AD4
		public override Query Skip(RowCount count)
		{
			if (count.IsZero)
			{
				return this;
			}
			RowRange rowRange = this.RowRange.Skip(count);
			return this.queryDomain.NewQuery(this.columns, this.querySpecification, this.tableKeys, false, this.tableSortOrder, rowRange);
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x00096924 File Offset: 0x00094B24
		public override Query Distinct(TableDistinct distinctCriteria)
		{
			Query query;
			using (this.trace.NewScope("Distinct"))
			{
				int[] array;
				if (this.trace.When<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<Keys>, FoldingWarnings.StringFormatter<TableDistinct>>>(!distinctCriteria.TryGetColumns(QueryTableValue.NewRowType(this), out array), FoldingWarnings.DistinctColumns(distinctCriteria, this.Columns)) || this.trace.When<FoldingWarnings.FoldingWarning<int, int>>(array.Length != this.querySpecification.SelectItems.Count, FoldingWarnings.DistinctColumnsCount(array.Length, this.querySpecification.SelectItems.Count)) || array.Any((int c) => this.trace.When<FoldingWarnings.FoldingWarning<string, Odbc32.SQL_SEARCHABLE>>(!this.columns[c].TypeInfo.CanBeUsedInDistinct, OdbcFoldingWarnings.DataTypeNotSearchable(this.columns[c]))) || !this.trace.RowRangeIsAll(this.rowRange) || !this.trace.DataSourceInfo.Supports(Odbc32.SQL_SC.SQL_SC_SQL92_ENTRY))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcQuerySpecification odbcQuerySpecification = this.querySpecification.ShallowCopy();
				odbcQuerySpecification.RepeatedRowOption = RepeatedRowOption.Distinct;
				query = this.queryDomain.NewQuery(this.columns, odbcQuerySpecification, this.tableKeys, false, this.tableSortOrder, this.rowRange);
			}
			return query;
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x00096A48 File Offset: 0x00094C48
		public override Query Sort(TableSortOrder sortOrder)
		{
			return this.Sort(sortOrder, false);
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x00096A54 File Offset: 0x00094C54
		public override Query Unordered()
		{
			OdbcQuerySpecification odbcQuerySpecification = this.querySpecification.ShallowCopy();
			odbcQuerySpecification.OrderByClause = null;
			return this.queryDomain.NewQuery(this.columns, odbcQuerySpecification, this.tableKeys, this.canInlineFrom, TableSortOrder.Unknown, this.rowRange);
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x00096AA0 File Offset: 0x00094CA0
		private Query Sort(TableSortOrder sortOrder, bool ignoreRange)
		{
			Query query2;
			using (this.trace.NewScope("Sort"))
			{
				QueryExpression[] array;
				bool[] array2;
				if ((!ignoreRange && !this.CanFoldOverRange(this.DataSource.Info, this.rowRange)) || this.trace.When<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<Keys>>>(!SortQuery.TryGetSelectors(sortOrder, QueryTableValue.NewRowType(this), out array, out array2), FoldingWarnings.SortColumns(this.Columns)))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OrderByClause orderByClause = new OrderByClause();
				HashSet<int> hashSet = new HashSet<int>();
				for (int i = 0; i < array2.Length; i++)
				{
					ColumnAccessQueryExpression columnAccessQueryExpression = array[i] as ColumnAccessQueryExpression;
					if (this.trace.When<FoldingWarnings.FoldingWarning<int>>(columnAccessQueryExpression == null, FoldingWarnings.SortInvalidColumn(i)) || this.trace.When<FoldingWarnings.FoldingWarning<string, Odbc32.SQL_SEARCHABLE>>(!this.columns[columnAccessQueryExpression.Column].TypeInfo.CanBeUsedInSort, OdbcFoldingWarnings.DataTypeNotSearchable(this.columns[columnAccessQueryExpression.Column])))
					{
						throw this.trace.NewFoldingFailureException(null);
					}
					if (hashSet.Add(columnAccessQueryExpression.Column))
					{
						OrderByItem orderByItem = new OrderByItem(new ColumnReference(this.querySpecification.SelectItems[columnAccessQueryExpression.Column].Name), array2[i] ? OrderByOption.Ascending : OrderByOption.Descending);
						orderByClause.OrderByItems.Add(orderByItem);
					}
				}
				if (this.querySpecification.OrderByClause != null)
				{
					for (int j = 0; j < this.querySpecification.OrderByClause.OrderByItems.Count; j++)
					{
						OrderByItem orderByItem2 = this.querySpecification.OrderByClause.OrderByItems[j];
						bool flag = false;
						for (int k = 0; k < orderByClause.OrderByItems.Count; k++)
						{
							if (orderByClause.OrderByItems[k].SortColumn.Name.Name == orderByItem2.SortColumn.Name.Name)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							orderByClause.OrderByItems.Add(orderByItem2);
						}
					}
				}
				if (this.DataSource.Info.MaxColumnsInOrderBy > 0 && orderByClause.OrderByItems.Count > this.DataSource.Info.MaxColumnsInOrderBy)
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<int, string, int>>(FoldingWarnings.InvalidSqlGetInfo<int, int, string>(orderByClause.OrderByItems.Count, this.DataSource.Info.MaxColumnsInOrderBy, "SQL_MAX_COLUMNS_IN_ORDER_BY"));
				}
				bool flag2 = false;
				OdbcQuerySpecification odbcQuerySpecification;
				if (!ignoreRange && !this.rowRange.IsAll && !this.rowRange.TakeCount.IsInfinite && this.rowRange.SkipCount.IsZero)
				{
					odbcQuerySpecification = this.PushDownQuerySpecification(false, true);
					flag2 = true;
				}
				else
				{
					odbcQuerySpecification = this.querySpecification.ShallowCopy();
				}
				odbcQuerySpecification.OrderByClause = orderByClause;
				Query query = SortQuery.New(sortOrder, RowCount.Infinite, this);
				query2 = this.queryDomain.NewQuery(this.columns, odbcQuerySpecification, this.tableKeys, this.canInlineFrom, query.SortOrder, flag2 ? RowRange.All : this.rowRange);
			}
			return query2;
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x00096DF0 File Offset: 0x00094FF0
		public override Query SelectRows(FunctionValue function)
		{
			Query query;
			using (this.trace.NewScope("SelectRows"))
			{
				QueryExpression queryExpression;
				if (!this.trace.RowRangeIsAll(this.rowRange) || this.trace.When<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<Keys>, FoldingWarnings.StringFormatter<FunctionValue>>>(!QueryExpressionBuilder.TryToQueryExpression(QueryTableValue.NewRowType(this), function, out queryExpression), FoldingWarnings.SelectRows(this.Columns, function)))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				bool flag;
				if (SelectRowsQuery.TryGetConstantCondition(queryExpression, out flag) && flag)
				{
					query = this;
				}
				else
				{
					OdbcQuerySpecification odbcQuerySpecification;
					if (this.AccessesDerivedColumns(queryExpression))
					{
						odbcQuerySpecification = this.PushDownQuerySpecification(true, false);
					}
					else
					{
						odbcQuerySpecification = this.querySpecification.ShallowCopy();
					}
					Condition condition = odbcQuerySpecification.WhereClause;
					OdbcQueryExpressionVisitor odbcQueryExpressionVisitor = this.queryDomain.NewQueryExpressionVisitor(odbcQuerySpecification.SelectItems, this.columns, false, null);
					List<QueryExpression> conjunctiveNF = SelectRowsQuery.GetConjunctiveNF(queryExpression);
					List<QueryExpression> list = new List<QueryExpression>();
					foreach (QueryExpression queryExpression2 in conjunctiveNF)
					{
						try
						{
							OdbcConditionExpression condition2 = odbcQueryExpressionVisitor.GetCondition(queryExpression2);
							if (condition == null)
							{
								condition = condition2.Expression;
							}
							else
							{
								condition = new ConditionOperation(condition, ConditionOperator.And, condition2.Expression);
							}
						}
						catch (NotSupportedException ex)
						{
							list.Add(queryExpression2);
							this.queryDomain.ReportFoldingFailure(ex);
						}
					}
					odbcQuerySpecification.WhereClause = condition;
					OdbcQuery odbcQuery = this.queryDomain.NewQuery(this.columns, odbcQuerySpecification, this.tableKeys, false, this.tableSortOrder, this.rowRange);
					if (list.Count == 0)
					{
						query = odbcQuery;
					}
					else
					{
						query = new OdbcFilteredTableQuery(new QueryTableValue(odbcQuery), this.EngineHost).SelectRows(QueryExpressionAssembler.Assemble(this.Columns, SelectRowsQuery.CreateConjunctiveNF(list.ToArray())));
					}
				}
			}
			return query;
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x00096FF8 File Offset: 0x000951F8
		public override Query RenameReorderColumns(ColumnSelection columnSelection)
		{
			OdbcQuerySpecification odbcQuerySpecification = this.querySpecification.ShallowCopy();
			odbcQuerySpecification.SelectItems = new SelectItem[columnSelection.Keys.Length];
			OdbcQueryColumnInfo[] array = new OdbcQueryColumnInfo[columnSelection.Keys.Length];
			for (int i = 0; i < columnSelection.Keys.Length; i++)
			{
				int column = columnSelection.GetColumn(i);
				OdbcQueryColumnInfo odbcQueryColumnInfo = this.columns[column];
				SelectItem selectItem = this.querySpecification.SelectItems[column];
				array[i] = new OdbcQueryColumnInfo(columnSelection.Keys[i], odbcQueryColumnInfo.AscribedTypeValue, odbcQueryColumnInfo.TypeInfo);
				odbcQuerySpecification.SelectItems[i] = selectItem;
			}
			Query query = ProjectColumnsQuery.New(columnSelection, this);
			return this.queryDomain.NewQuery(array, odbcQuerySpecification, query.TableKeys, this.canInlineFrom, query.SortOrder, this.rowRange);
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x000970D4 File Offset: 0x000952D4
		public override Query Group(Grouping grouping)
		{
			Query query;
			using (this.trace.NewScope("Group"))
			{
				OdbcDataSourceInfo info = this.DataSource.Info;
				if (!this.CanFoldOverRange(info, this.rowRange) || this.trace.When(this.tableSortOrder.SortOrders.Length != 0, "Order By columns should not exist when doing Group By. Check Table.Sort and/or other related functions in your M code.") || this.trace.When(grouping.Adjacent, "Folding failed because GroupKind is Local. Check groupKind parameter in Table.Group and/or other related functions in your M code.") || this.trace.When(grouping.Comparer != null, "Folding failed due to Compare exists. Check comparer parameter in Table.Group and/or other related functions in your M code.") || this.trace.When<FoldingWarnings.FoldingWarning<int, string, int>>(info.MaxColumnsInGroupBy != 0 && grouping.KeyColumns.Length > info.MaxColumnsInGroupBy, FoldingWarnings.InvalidSqlGetInfo<int, int, string>(grouping.KeyColumns.Length, info.MaxColumnsInGroupBy, "SQL_MAX_COLUMNS_IN_GROUP_BY")) || this.trace.When<FoldingWarnings.FoldingWarning<string>>(info.GroupByCapabilities == Odbc32.SQL_GB.SQL_GB_NOT_SUPPORTED, FoldingWarnings.SqlCapabilities("GroupByCapabilities")) || !this.trace.DataSourceInfo.Supports(Odbc32.SQL_SC.SQL_SC_SQL92_ENTRY))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				QueryExpression[] array = grouping.Constructors.Select((ColumnConstructor c) => QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewRowType(this), c.Function)).ToArray<QueryExpression>();
				bool flag = false;
				OdbcQuerySpecification odbcQuerySpecification;
				if (!this.canInlineFrom || grouping.KeyColumns.Any(new Func<int, bool>(this.IsDerivedColumn)) || array.Any(new Func<QueryExpression, bool>(this.AccessesDerivedColumns)) || (!this.rowRange.IsAll && !this.rowRange.TakeCount.IsInfinite && this.rowRange.SkipCount.IsZero))
				{
					odbcQuerySpecification = this.PushDownQuerySpecification(false, true);
					flag = true;
				}
				else
				{
					odbcQuerySpecification = this.querySpecification.ShallowCopy();
				}
				odbcQuerySpecification.OrderByClause = null;
				IList<SelectItem> selectItems = odbcQuerySpecification.SelectItems;
				odbcQuerySpecification.SelectItems = new SelectItem[grouping.KeyColumns.Length];
				List<OdbcQueryColumnInfo> list = new List<OdbcQueryColumnInfo>(grouping.KeyColumns.Length + grouping.Constructors.Length);
				for (int i = 0; i < grouping.KeyColumns.Length; i++)
				{
					int num = grouping.KeyColumns[i];
					OdbcQueryColumnInfo odbcQueryColumnInfo = this.columns[num];
					if (!odbcQueryColumnInfo.TypeInfo.CanBeUsedInGroupBy)
					{
						throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, Odbc32.SQL_SEARCHABLE>>(OdbcFoldingWarnings.DataTypeNotSearchable(odbcQueryColumnInfo));
					}
					SelectItem selectItem = selectItems[num];
					odbcQuerySpecification.SelectItems[i] = selectItem;
					list.Add(odbcQueryColumnInfo);
					if (odbcQuerySpecification.GroupByClause == null)
					{
						odbcQuerySpecification.GroupByClause = new GroupByClause();
					}
					odbcQuerySpecification.GroupByClause.Items.Add(new GroupByItem
					{
						Expression = selectItem.Expression
					});
				}
				GroupQuery groupQuery = new GroupQuery(grouping, this, false);
				query = this.AddColumns(selectItems, array, Keys.New(grouping.Constructors.Select((ColumnConstructor c) => c.Name).ToArray<string>()), grouping.Constructors.Select((ColumnConstructor c) => c.Type).ToArray<IValueReference>(), list, odbcQuerySpecification, true, grouping.KeyColumns, groupQuery.TableKeys, flag);
			}
			return query;
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x0009743C File Offset: 0x0009563C
		public override Query AddColumns(ColumnsConstructor columnGenerator)
		{
			Query query;
			using (this.trace.NewScope("AddColumns"))
			{
				if (!this.trace.DataSourceInfo.Supports(Odbc32.SQL_SC.SQL_SC_SQL92_INTERMEDIATE))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				IList<QueryExpression> list = AddColumnsQuery.CreateQueryExpressions(columnGenerator.Function, QueryTableValue.NewRowType(this));
				if (list == null)
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<Keys>, FoldingWarnings.StringFormatter<FunctionValue>>>(FoldingWarnings.AddColumnsKind(this.Columns, columnGenerator.Function));
				}
				OdbcQuerySpecification odbcQuerySpecification;
				if (list.Any(new Func<QueryExpression, bool>(this.AccessesDerivedColumns)))
				{
					odbcQuerySpecification = this.PushDownQuerySpecification(true, false);
				}
				else
				{
					odbcQuerySpecification = this.querySpecification.ShallowCopy();
				}
				query = this.AddColumns(odbcQuerySpecification.SelectItems, list, columnGenerator.Names, columnGenerator.Types, new List<OdbcQueryColumnInfo>(this.columns), odbcQuerySpecification, false, null, this.tableKeys, false);
			}
			return query;
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x00097524 File Offset: 0x00095724
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			return (function.FunctionIdentity.Equals(CapabilityModule.DirectQueryCapabilities.From.FunctionIdentity) && this.TryDirectQueryCapabilitiesFrom(arguments, index, out result)) || base.TryInvokeAsArgument(function, arguments, index, out result);
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x00097558 File Offset: 0x00095758
		private bool TryDirectQueryCapabilitiesFrom(Value[] arguments, int index, out Value result)
		{
			if (arguments.Length == 1 && index == 0)
			{
				List<Value> list = OdbcDirectQueryCapabilities.From(this.queryDomain.DataSource.Info);
				TableTypeValue tableType = CapabilityModule.DirectQueryCapabilities.From.Type.AsFunctionType.ReturnType.AsTableType;
				result = ListValue.New(list.ToArray()).ToTable(tableType);
				ListValue listValue;
				if (this.queryDomain.SqlExpressionGenerator.TryGetAdditionalFunctions(out listValue))
				{
					Value value = ListValue.New(from name in listValue.Select(delegate(IValueReference f)
						{
							IIdentifierExpression identifierExpression;
							if (f.Value.Expression.TryGetIdentifier(out identifierExpression))
							{
								return RecordValue.New(tableType.ItemType, new Value[]
								{
									TextValue.New(identifierExpression.Name.Name),
									Value.Null
								});
							}
							return null;
						})
						where name != null
						select name).ToTable(tableType);
					result = result.Concatenate(value);
				}
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x00097632 File Offset: 0x00095832
		public override ActionValue InsertRows(Query rowsToInsert)
		{
			return this.StatementBuilder.InsertRows(rowsToInsert);
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x00097640 File Offset: 0x00095840
		public override ActionValue UpdateRows(ColumnUpdates columnUpdates)
		{
			return this.StatementBuilder.UpdateRows(columnUpdates, ConstantFunctionValue.EachTrue);
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x00097653 File Offset: 0x00095853
		public override ActionValue DeleteRows()
		{
			return this.StatementBuilder.DeleteRows(ConstantFunctionValue.EachTrue);
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x00097668 File Offset: 0x00095868
		public override bool TryGetReader(out IPageReader reader)
		{
			string text;
			IList<DynamicParameter> list;
			RowRange rowRange;
			if (this.TryGetCommand(false, out text, out list, out rowRange))
			{
				reader = this.DataSource.ExecutePageReader(text, this.catalogName, list.Select((DynamicParameter p) => (OdbcParameter)p.Value).ToArray<OdbcParameter>(), rowRange, this.GetLocalColumnNames(), this.GetColumnConversions());
				return true;
			}
			reader = null;
			return false;
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x000976D8 File Offset: 0x000958D8
		public bool TryGetCommand(bool includeColumnNames, out string commandText, out IList<DynamicParameter> parameters, out RowRange localSkipTake)
		{
			bool flag;
			using (this.trace.NewScope("TryGetCommand"))
			{
				OdbcQuerySpecification odbcQuerySpecification;
				if (includeColumnNames)
				{
					if (!this.TryGetQuerySpecificationWithColumnNames(out odbcQuerySpecification))
					{
						commandText = null;
						parameters = null;
						localSkipTake = default(RowRange);
						return false;
					}
				}
				else
				{
					odbcQuerySpecification = this.querySpecification.ShallowCopy();
				}
				OdbcLimitClause odbcLimitClause;
				if (this.queryDomain.SqlExpressionGenerator.TryGetLimitClause(this.RowRange, out odbcLimitClause, out localSkipTake))
				{
					odbcQuerySpecification.LimitClause = odbcLimitClause;
				}
				else
				{
					localSkipTake = this.rowRange;
				}
				if (!this.trace.RowRangeIsAll(localSkipTake))
				{
					this.queryDomain.ReportFoldingFailure(this.trace.NewFoldingFailureException(null));
				}
				using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
				{
					ScriptWriter scriptWriter = new ScriptWriter(stringWriter, this.DataSource.SqlSettings);
					odbcQuerySpecification.WriteCreateScript(scriptWriter);
					commandText = stringWriter.ToString();
					parameters = scriptWriter.Parameters;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x000977F4 File Offset: 0x000959F4
		public override IEnumerable<IValueReference> GetRows()
		{
			return DeferredEnumerable.From<IValueReference>(delegate
			{
				bool flag = true;
				OdbcQuery odbcQuery = this.ApplyStableOrder(this);
				if (odbcQuery == null)
				{
					flag = false;
					odbcQuery = this;
				}
				string text;
				IList<DynamicParameter> list;
				RowRange rowRange;
				if (!odbcQuery.TryGetCommand(false, out text, out list, out rowRange))
				{
					throw new InvalidOperationException();
				}
				IDataReaderWithTableSchema dataReaderWithTableSchema = this.DataSource.Execute(this.DataSource.Host.GetPersistentCache(), text, this.catalogName, list.Select((DynamicParameter p) => (OdbcParameter)p.Value).ToArray<OdbcParameter>(), rowRange, this.GetLocalColumnNames(), flag, this.GetColumnConversions());
				IEnumerator<IValueReference> enumerator;
				try
				{
					enumerator = DbDataReaderEnumerator.New(this.queryDomain.DataSource.Host, new DataReaderDbDataReader(dataReaderWithTableSchema), "ODBC", this.ReaderItemType, this.DataSource.Resource);
				}
				catch
				{
					dataReaderWithTableSchema.Dispose();
					throw;
				}
				return enumerator;
			});
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x00097808 File Offset: 0x00095A08
		private bool TryGetQuerySpecificationWithColumnNames(out OdbcQuerySpecification newQuerySpecification)
		{
			try
			{
				newQuerySpecification = this.querySpecification.ShallowCopy();
				newQuerySpecification.SelectItems = new SelectItem[this.querySpecification.SelectItems.Count];
				for (int i = 0; i < this.querySpecification.SelectItems.Count; i++)
				{
					SelectItem selectItem = this.querySpecification.SelectItems[i];
					string text = this.Columns[i];
					Alias name = selectItem.Name;
					if (name == null || !text.Equals(name.Name, StringComparison.Ordinal))
					{
						Alias alias;
						if (!this.trace.DataSourceInfo.SupportsColumnAliases || this.trace.When(this.DataSource.Info.IdentifierSpecialCharacters == null, "This ODBC driver doesn't provide an invalid character list.") || this.trace.When<FoldingWarnings.FoldingWarning<string, FoldingWarnings.StringFormatter<SqlSettings>>>(!Alias.TryNewAssignedAlias(text, this.DataSource.SqlSettings, out alias), OdbcFoldingWarnings.InvalidCharacter(text, this.DataSource.SqlSettings)) || alias.IsMitigated)
						{
							throw new NotSupportedException();
						}
						newQuerySpecification.SelectItems[i] = new SelectItem(selectItem.Expression, alias);
					}
					else
					{
						newQuerySpecification.SelectItems[i] = this.querySpecification.SelectItems[i];
					}
				}
				return true;
			}
			catch (NotSupportedException)
			{
			}
			newQuerySpecification = null;
			return false;
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x0009797C File Offset: 0x00095B7C
		private string[] GetLocalColumnNames()
		{
			string[] array = new string[this.columns.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.columns[i].LocalName;
			}
			return array;
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x000979B8 File Offset: 0x00095BB8
		private ColumnConversion[] GetColumnConversions()
		{
			IEnumerable<ColumnConversion> enumerable = this.columns.Select((OdbcQueryColumnInfo column) => column.TypeInfo.ColumnConversion);
			if (enumerable.Any((ColumnConversion conversion) => conversion != null))
			{
				return enumerable.ToArray<ColumnConversion>();
			}
			return null;
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x00097A20 File Offset: 0x00095C20
		private Query AddColumns(IList<SelectItem> selectItems, IList<QueryExpression> queryExpressions, Keys keys, IValueReference[] typeValues, List<OdbcQueryColumnInfo> newColumns, OdbcQuerySpecification newQuerySpecification, bool allowAggregates, int[] groupKey, IList<TableKey> tableKeys, bool ignoreRange = false)
		{
			Query query;
			using (this.trace.NewScope("AddColumns"))
			{
				if (this.trace.When<FoldingWarnings.FoldingWarning<string, string, string>>(!this.DataSource.Info.SupportsColumnAliases, FoldingWarnings.InvalidSqlGetInfo<string, string, string>("Y", "N", "SQL_COLUMN_ALIAS")) || this.trace.When<FoldingWarnings.FoldingWarning<int, string, int>>(this.DataSource.Info.MaxColumnsInSelect > 0 && this.DataSource.Info.MaxColumnsInSelect < selectItems.Count + queryExpressions.Count, FoldingWarnings.InvalidSqlGetInfo<int, int, string>(selectItems.Count + queryExpressions.Count, this.DataSource.Info.MaxColumnsInSelect, "SQL_MAX_COLUMNS_IN_SELECT")))
				{
					throw this.trace.NewFoldingFailureException(null);
				}
				OdbcQueryExpressionVisitor odbcQueryExpressionVisitor = this.queryDomain.NewQueryExpressionVisitor(selectItems, this.columns, allowAggregates, groupKey);
				OdbcColumnNameGenerator odbcColumnNameGenerator = new OdbcColumnNameGenerator(this.DataSource.SqlSettings, newQuerySpecification.SelectItems.Select((SelectItem si) => si.Name.Name));
				IList<SelectItem> selectItems2 = newQuerySpecification.SelectItems;
				newQuerySpecification.SelectItems = new SelectItem[newQuerySpecification.SelectItems.Count + queryExpressions.Count];
				selectItems2.CopyTo(newQuerySpecification.SelectItems);
				for (int i = 0; i < queryExpressions.Count; i++)
				{
					QueryExpression queryExpression = queryExpressions[i];
					OdbcSqlExpression odbcSqlExpression = odbcQueryExpressionVisitor.Visit(queryExpression);
					OdbcScalarExpression odbcScalarExpression;
					if (odbcSqlExpression.Kind == OdbcSqlExpressionKind.Condition)
					{
						odbcScalarExpression = new OdbcScalarExpression(odbcQueryExpressionVisitor.NewColumnType(Odbc32.SQL_TYPE.BIT, false), odbcSqlExpression.AsCondition.Expression);
					}
					else
					{
						odbcScalarExpression = odbcSqlExpression.AsScalar;
					}
					Alias nextName = odbcColumnNameGenerator.GetNextName();
					newQuerySpecification.SelectItems[selectItems2.Count + i] = new SelectItem(odbcScalarExpression.Expression, nextName);
					newColumns.Add(new OdbcQueryColumnInfo(keys[i], typeValues[i].Value.AsType, odbcScalarExpression.TypeInfo));
				}
				if (newQuerySpecification.SelectItems.Count == 0)
				{
					throw this.trace.NewFoldingFailureException("The collection of selected columns is empty.");
				}
				query = this.queryDomain.NewQuery(newColumns.ToArray(), newQuerySpecification, tableKeys, false, this.tableSortOrder, ignoreRange ? RowRange.All : this.RowRange);
			}
			return query;
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x00097C9C File Offset: 0x00095E9C
		private bool Hides(ColumnSelection columnSelection, IEnumerable<Alias> columns)
		{
			foreach (Alias alias in columns)
			{
				bool flag = false;
				for (int i = 0; i < columnSelection.Keys.Length; i++)
				{
					int column = columnSelection.GetColumn(i);
					SelectItem selectItem = this.querySpecification.SelectItems[column];
					if (alias.Equals(selectItem.Name))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x00097D34 File Offset: 0x00095F34
		private OdbcQuerySpecification PushDownQuerySpecification(bool liftOrderBy, bool pushLimitClause = false)
		{
			return this.PushDownQuerySpecification(this.rowRange, liftOrderBy, pushLimitClause);
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x00097D44 File Offset: 0x00095F44
		private OdbcQuerySpecification PushDownQuerySpecification(RowRange rowRange, bool liftOrderBy, bool pushLimitClause = false)
		{
			OdbcQuerySpecification odbcQuerySpecification2;
			using (this.trace.NewScope("PushDownQuerySpecification"))
			{
				if (!this.DataSource.Info.SupportsDerivedTable)
				{
					throw this.trace.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string>>(FoldingWarnings.SqlCapabilities("Sql92Conformance"));
				}
				SelectItem[] array = new SelectItem[this.querySpecification.SelectItems.Count];
				for (int i = 0; i < this.querySpecification.SelectItems.Count; i++)
				{
					ColumnReference columnReference = new ColumnReference(this.querySpecification.SelectItems[i].Name);
					array[i] = new SelectItem(columnReference);
				}
				OdbcQuerySpecification odbcQuerySpecification = this.querySpecification.ShallowCopy();
				odbcQuerySpecification.OrderByClause = null;
				if (pushLimitClause)
				{
					OdbcLimitClause odbcLimitClause = null;
					RowRange rowRange2;
					if (this.queryDomain.SqlExpressionGenerator.TryGetLimitClause(rowRange, out odbcLimitClause, out rowRange2))
					{
						odbcQuerySpecification.LimitClause = odbcLimitClause;
					}
					else
					{
						rowRange2 = rowRange;
					}
					if (!this.trace.RowRangeIsAll(rowRange2))
					{
						this.queryDomain.ReportFoldingFailure(this.trace.NewFoldingFailureException(null));
					}
				}
				odbcQuerySpecification2 = new OdbcQuerySpecification();
				odbcQuerySpecification2.SelectItems = array;
				odbcQuerySpecification2.FromItems = new FromItem[]
				{
					new FromQuery
					{
						Query = odbcQuerySpecification,
						Alias = this.queryDomain.InnerTableAlias
					}
				};
				OdbcQuerySpecification odbcQuerySpecification3 = odbcQuerySpecification2;
				if (liftOrderBy)
				{
					odbcQuerySpecification3.OrderByClause = this.querySpecification.OrderByClause;
				}
				odbcQuerySpecification2 = odbcQuerySpecification3;
			}
			return odbcQuerySpecification2;
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x00097ECC File Offset: 0x000960CC
		private bool IsDerivedColumn(int i)
		{
			return this.querySpecification.SelectItems[i].Alias != null;
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x00097EE7 File Offset: 0x000960E7
		private bool AccessesDerivedColumns(QueryExpression expression)
		{
			return !expression.AllAccess((InvocationQueryExpression i) => true, (int i) => !this.IsDerivedColumn(i));
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x00097F20 File Offset: 0x00096120
		private bool CanFoldOverRange(OdbcDataSourceInfo dataSourceInfo, RowRange rowRange)
		{
			return this.trace.RowRangeIsAll(rowRange) || (dataSourceInfo.SupportsTopOrLimit && rowRange.SkipCount.IsZero && !rowRange.TakeCount.IsInfinite);
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x00097F6C File Offset: 0x0009616C
		private OdbcQuery ApplyStableOrder(OdbcQuery query)
		{
			TableKey tableKey = query.tableKeys.Where((TableKey key) => key.Primary).FirstOrDefault<TableKey>();
			if (tableKey != null)
			{
				int[] array = tableKey.Columns;
				if (array.Length != 0)
				{
					SortOrder[] array2 = new SortOrder[query.tableSortOrder.SortOrders.Length + array.Length];
					Array.Copy(query.tableSortOrder.SortOrders, array2, query.tableSortOrder.SortOrders.Length);
					for (int i = 0; i < array.Length; i++)
					{
						string text = query.Columns[array[i]];
						array2[query.tableSortOrder.SortOrders.Length + i] = new SortOrder(new TableValue.ColumnSelectorFunctionValue(text, i), null, true);
					}
					try
					{
						OdbcQuery odbcQuery = query.Sort(new TableSortOrder(array2), true) as OdbcQuery;
						if (odbcQuery != null)
						{
							return odbcQuery;
						}
					}
					catch (NotSupportedException ex)
					{
						this.queryDomain.ReportFoldingFailure(ex);
					}
				}
			}
			return null;
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x0009807C File Offset: 0x0009627C
		private RecordTypeValue GetTypeFromQuerySpecificationSelectItem(Func<OdbcQueryColumnInfo, TypeValue> getTypeValue, Func<bool?, LogicalValue> getOptionalValue)
		{
			RecordBuilder recordBuilder = new RecordBuilder(this.querySpecification.SelectItems.Count);
			for (int i = 0; i < this.querySpecification.SelectItems.Count; i++)
			{
				bool? flag = null;
				string text;
				TypeValue typeValue;
				if (i < this.columns.Length)
				{
					OdbcQueryColumnInfo odbcQueryColumnInfo = this.columns[i];
					text = odbcQueryColumnInfo.LocalName;
					typeValue = getTypeValue(odbcQueryColumnInfo);
					flag = new bool?(odbcQueryColumnInfo.TypeInfo.IsNullable);
				}
				else
				{
					text = this.querySpecification.SelectItems[i].Name.Name;
					typeValue = TypeValue.Any;
				}
				recordBuilder.Add(text, RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					typeValue,
					getOptionalValue(flag)
				}), TypeValue.Record);
			}
			return RecordTypeValue.New(recordBuilder.ToRecord());
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x00098160 File Offset: 0x00096360
		private static PagingClause PagingClauseFromRowRange(RowRange range)
		{
			PagingClause pagingClause = new PagingClause();
			pagingClause.OffsetExpression = range.SkipCount.Value;
			if (!range.TakeCount.IsInfinite)
			{
				pagingClause.FetchExpression = new long?(range.TakeCount.Value);
			}
			return pagingClause;
		}

		// Token: 0x040015F7 RID: 5623
		private const string DataSourceNameString = "ODBC";

		// Token: 0x040015F8 RID: 5624
		private readonly OdbcQueryDomain queryDomain;

		// Token: 0x040015F9 RID: 5625
		private readonly OdbcQueryColumnInfo[] columns;

		// Token: 0x040015FA RID: 5626
		private readonly RowRange rowRange;

		// Token: 0x040015FB RID: 5627
		private readonly OdbcQuerySpecification querySpecification;

		// Token: 0x040015FC RID: 5628
		private readonly IList<TableKey> tableKeys;

		// Token: 0x040015FD RID: 5629
		private readonly bool canInlineFrom;

		// Token: 0x040015FE RID: 5630
		private readonly TableSortOrder tableSortOrder;

		// Token: 0x040015FF RID: 5631
		private readonly OdbcFoldingTracingService trace;

		// Token: 0x04001600 RID: 5632
		private readonly string catalogName;

		// Token: 0x04001601 RID: 5633
		private RecordTypeValue itemType;

		// Token: 0x04001602 RID: 5634
		private RecordTypeValue readerItemType;

		// Token: 0x04001603 RID: 5635
		private OdbcStatementBuilder statementBuilder;
	}
}
