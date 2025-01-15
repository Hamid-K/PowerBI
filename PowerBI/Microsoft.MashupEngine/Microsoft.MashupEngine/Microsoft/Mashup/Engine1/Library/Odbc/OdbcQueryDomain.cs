using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000626 RID: 1574
	internal class OdbcQueryDomain : IQueryDomain, INativeQueryDomain
	{
		// Token: 0x060031A8 RID: 12712 RVA: 0x00098400 File Offset: 0x00096600
		public OdbcQueryDomain(OdbcDataSource dataSource, OdbcSqlExpressionGenerator generator, OdbcQueryExpressionVisitorFactory visitorFactory, bool softNumbers, bool hideNativeQuery, bool throwOnFoldingFailure, bool tolerateConcatOverflow, string catalog = null)
		{
			this.dataSource = dataSource;
			this.generator = generator;
			this.visitorFactory = visitorFactory;
			this.optimizingVisitor = new OdbcQueryDomain.OdbcOptimizingQueryVisitor(this, dataSource.SqlSettings);
			this.softNumbers = softNumbers;
			this.hideNativeQuery = hideNativeQuery;
			this.throwOnFoldingFailure = throwOnFoldingFailure;
			this.tolerateConcatOverflow = tolerateConcatOverflow;
			this.catalogName = catalog;
			this.tracingService = new OdbcFoldingTracingService(dataSource);
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x00098470 File Offset: 0x00096670
		public IQueryDomain WithCatalog(string catalog)
		{
			if (catalog.Equals(this.catalogName))
			{
				return this;
			}
			return new OdbcQueryDomain(this.dataSource, this.generator, this.visitorFactory, this.softNumbers, this.hideNativeQuery, this.throwOnFoldingFailure, this.tolerateConcatOverflow, catalog);
		}

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x060031AA RID: 12714 RVA: 0x000984BD File Offset: 0x000966BD
		public OdbcDataSource DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x060031AB RID: 12715 RVA: 0x00002139 File Offset: 0x00000339
		public bool CanIndex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x060031AC RID: 12716 RVA: 0x000984C5 File Offset: 0x000966C5
		public OdbcSqlExpressionGenerator SqlExpressionGenerator
		{
			get
			{
				return this.generator;
			}
		}

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x060031AD RID: 12717 RVA: 0x000984D0 File Offset: 0x000966D0
		public Alias InnerTableAlias
		{
			get
			{
				Alias alias;
				using (this.tracingService.NewScope("InnerTableAlias"))
				{
					if (this.innerTableAlias == null && !Alias.TryNewAssignedAlias("ITBL", this.DataSource.SqlSettings, out this.innerTableAlias))
					{
						throw this.tracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<string, FoldingWarnings.StringFormatter<SqlSettings>>>(OdbcFoldingWarnings.InvalidCharacter("ITBL", this.DataSource.SqlSettings));
					}
					alias = this.innerTableAlias;
				}
				return alias;
			}
		}

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x060031AE RID: 12718 RVA: 0x00098558 File Offset: 0x00096758
		public OdbcFoldingTracingService OdbcFoldingScope
		{
			get
			{
				return this.tracingService;
			}
		}

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x060031AF RID: 12719 RVA: 0x00098560 File Offset: 0x00096760
		protected OptimizingQueryVisitor OptimizingVisitor
		{
			get
			{
				return this.optimizingVisitor;
			}
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x00098568 File Offset: 0x00096768
		public bool IsCompatibleWith(IQueryDomain domain)
		{
			OdbcQueryDomain odbcQueryDomain = domain as OdbcQueryDomain;
			bool flag = odbcQueryDomain != null && odbcQueryDomain.dataSource.Equals(this.dataSource);
			bool flag2 = flag && (odbcQueryDomain.catalogName == this.catalogName || odbcQueryDomain.catalogName == null);
			bool flag3 = flag && this.catalogName == null;
			if (!flag2 && !flag3)
			{
				string text = this.catalogName ?? "<null>";
				string text2 = ((odbcQueryDomain != null) ? odbcQueryDomain.catalogName : null) ?? "<null>";
				using (this.tracingService.NewScope("IsCompatibleWith"))
				{
					this.tracingService.Trace<FoldingWarnings.FoldingWarning<string, string>>(FoldingWarnings.HeterogeneousJoin(this.dataSource.UniqueIdentifier + "/" + text, (((odbcQueryDomain != null) ? odbcQueryDomain.dataSource.UniqueIdentifier : null) ?? "<non-ODBC query>") + "/" + text2));
					if (this.throwOnFoldingFailure)
					{
						throw this.tracingService.NewValueException();
					}
					this.tracingService.FlushTraces();
				}
			}
			return flag2;
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x0009869C File Offset: 0x0009689C
		public void ReportFoldingFailure(NotSupportedException ex)
		{
			using (IHostTrace hostTrace = TracingService.CreatePerformanceTrace(this.dataSource.Host, "OdbcQueryDomain/ReportFoldingFailure", TraceEventType.Information, null))
			{
				FoldingFailureException ex2 = ex as FoldingFailureException;
				if (ex2 != null)
				{
					ex2.Trace(hostTrace);
				}
			}
			if (this.throwOnFoldingFailure)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.FoldingFailure, null, null);
			}
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x00098704 File Offset: 0x00096904
		public virtual Query Optimize(Query query)
		{
			return this.optimizingVisitor.Optimize(query);
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x00098712 File Offset: 0x00096912
		public override bool Equals(object obj)
		{
			return this.Equals(obj as OdbcQueryDomain);
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x00098720 File Offset: 0x00096920
		public bool Equals(OdbcQueryDomain queryDomain)
		{
			return queryDomain != null && queryDomain.dataSource.Equals(this.dataSource);
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x00098738 File Offset: 0x00096938
		public override int GetHashCode()
		{
			return this.dataSource.GetHashCode();
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x00098748 File Offset: 0x00096948
		public bool TryGetNativeQuery(Query query, out IResource resource, out Value nativeQuery, out RecordValue options)
		{
			RetryQuery retryQuery = query as RetryQuery;
			OdbcQuery odbcQuery = query as OdbcQuery;
			if (retryQuery != null)
			{
				odbcQuery = retryQuery.OptimizedQuery as OdbcQuery;
			}
			string text;
			IList<DynamicParameter> list;
			RowRange rowRange;
			if (odbcQuery != null && odbcQuery.TryGetCommand(!this.hideNativeQuery, out text, out list, out rowRange))
			{
				if (list.Count == 0 && rowRange.IsAll && !this.hideNativeQuery)
				{
					nativeQuery = TextValue.New(text);
				}
				else
				{
					nativeQuery = null;
				}
				resource = this.dataSource.Resource;
				options = RecordValue.Empty;
				return true;
			}
			resource = null;
			nativeQuery = null;
			options = RecordValue.Empty;
			return false;
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000987D8 File Offset: 0x000969D8
		public virtual OdbcQueryExpressionVisitor NewQueryExpressionVisitor(IList<SelectItem> selectItems, OdbcQueryColumnInfo[] columnInfos, bool allowAggregates, int[] groupKey)
		{
			return this.visitorFactory.New(this.dataSource, selectItems, columnInfos, allowAggregates, this.softNumbers, this.tolerateConcatOverflow, groupKey);
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000987FC File Offset: 0x000969FC
		public virtual OdbcQuery NewQuery(OdbcQueryColumnInfo[] columns, OdbcQuerySpecification querySpecification, IList<TableKey> tableKeys, bool canInlineFrom, TableSortOrder tableSortOrder, RowRange rowRange)
		{
			return new OdbcQuery(this, columns, querySpecification, tableKeys, canInlineFrom, tableSortOrder, rowRange, this.catalogName);
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x00098814 File Offset: 0x00096A14
		public OdbcQuery NewQuery(OdbcTableInfo table, FromItem[] fromItems = null)
		{
			OdbcQueryColumnInfo[] array = new OdbcQueryColumnInfo[table.Columns.Count];
			SelectItem[] array2 = new SelectItem[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				OdbcColumnInfo odbcColumnInfo = table.Columns[i];
				array[i] = new OdbcQueryColumnInfo(odbcColumnInfo.Name, odbcColumnInfo.TypeValue, new OdbcDerivedColumnTypeInfo(odbcColumnInfo.TypeInfo, odbcColumnInfo.IsNullable, odbcColumnInfo.ColumnSize, odbcColumnInfo.DecimalDigits));
				array2[i] = new SelectItem(new ColumnReference(Alias.NewNativeAlias(odbcColumnInfo.Name)));
			}
			if (fromItems == null)
			{
				fromItems = new FromItem[]
				{
					new FromTable
					{
						Table = table.Identifier.AsSqlReference(this.dataSource.Info)
					}
				};
			}
			IList<TableKey> list;
			if (table.PrimaryKey.Length != 0)
			{
				list = new TableKey[]
				{
					new TableKey(table.PrimaryKey, true)
				};
			}
			else
			{
				list = EmptyArray<TableKey>.Instance;
			}
			return this.NewQuery(array, new OdbcQuerySpecification
			{
				SelectItems = array2,
				FromItems = fromItems
			}, list, true, TableSortOrder.Unknown, RowRange.All);
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x00098924 File Offset: 0x00096B24
		private static bool IsSql92JoinOperatorSupported(OdbcDataSourceInfo dataSourceInfo, JoinOperator joinOperator)
		{
			switch (joinOperator)
			{
			case JoinOperator.InnerJoin:
				return dataSourceInfo.Supports(Odbc32.SQL_SRJO.SQL_SRJO_INNER_JOIN);
			case JoinOperator.LeftOuterJoin:
				return dataSourceInfo.Supports(Odbc32.SQL_SRJO.SQL_SRJO_LEFT_OUTER_JOIN);
			case JoinOperator.RightOuterJoin:
				return dataSourceInfo.Supports(Odbc32.SQL_SRJO.SQL_SRJO_RIGHT_OUTER_JOIN);
			case JoinOperator.FullOuterJoin:
				return dataSourceInfo.Supports(Odbc32.SQL_SRJO.SQL_SRJO_FULL_OUTER_JOIN);
			case JoinOperator.CrossJoin:
				return dataSourceInfo.Supports(Odbc32.SQL_SRJO.SQL_SRJO_CROSS_JOIN);
			default:
				return false;
			}
		}

		// Token: 0x04001619 RID: 5657
		private const string OuterAliasName = "OTBL";

		// Token: 0x0400161A RID: 5658
		private const string InnerAliasName = "ITBL";

		// Token: 0x0400161B RID: 5659
		private readonly OdbcDataSource dataSource;

		// Token: 0x0400161C RID: 5660
		private readonly OdbcSqlExpressionGenerator generator;

		// Token: 0x0400161D RID: 5661
		private readonly OdbcQueryExpressionVisitorFactory visitorFactory;

		// Token: 0x0400161E RID: 5662
		private readonly OdbcQueryDomain.OdbcOptimizingQueryVisitor optimizingVisitor;

		// Token: 0x0400161F RID: 5663
		private readonly bool softNumbers;

		// Token: 0x04001620 RID: 5664
		private readonly bool hideNativeQuery;

		// Token: 0x04001621 RID: 5665
		private readonly bool throwOnFoldingFailure;

		// Token: 0x04001622 RID: 5666
		private readonly bool tolerateConcatOverflow;

		// Token: 0x04001623 RID: 5667
		private readonly OdbcFoldingTracingService tracingService;

		// Token: 0x04001624 RID: 5668
		private readonly string catalogName;

		// Token: 0x04001625 RID: 5669
		private Alias innerTableAlias;

		// Token: 0x02000627 RID: 1575
		private sealed class OdbcOptimizingQueryVisitor : OptimizingQueryVisitor
		{
			// Token: 0x060031BB RID: 12731 RVA: 0x0009897E File Offset: 0x00096B7E
			public OdbcOptimizingQueryVisitor(OdbcQueryDomain domain, SqlSettings sqlSettings)
			{
				this.domain = domain;
				this.sqlSettings = sqlSettings;
			}

			// Token: 0x060031BC RID: 12732 RVA: 0x00098994 File Offset: 0x00096B94
			protected override void ReportOptimizationFailure(NotSupportedException ex)
			{
				this.domain.ReportFoldingFailure(ex);
			}

			// Token: 0x060031BD RID: 12733 RVA: 0x000989A4 File Offset: 0x00096BA4
			protected override Query VisitJoin(JoinQuery joinQuery)
			{
				Query query;
				try
				{
					query = this.VisitJoinCore(joinQuery);
				}
				catch (NotSupportedException ex)
				{
					this.ReportOptimizationFailure(ex);
					query = base.VisitJoin(joinQuery);
				}
				return query;
			}

			// Token: 0x060031BE RID: 12734 RVA: 0x000989E0 File Offset: 0x00096BE0
			private Query VisitJoinCore(JoinQuery joinQuery)
			{
				OdbcFoldingTracingService tracingService = this.domain.tracingService;
				Query query;
				using (tracingService.NewScope("VisitJoinCore"))
				{
					OdbcQuery odbcQuery = this.VisitQuery(joinQuery.LeftQuery) as OdbcQuery;
					OdbcQuery odbcQuery2 = this.VisitQuery(joinQuery.RightQuery) as OdbcQuery;
					Alias alias;
					Alias alias2;
					if (tracingService.When<FoldingWarnings.FoldingWarning<string>>(odbcQuery == null, OdbcFoldingWarnings.NonOdbcQueryAtJoin("left")) || tracingService.When<FoldingWarnings.FoldingWarning<string>>(odbcQuery2 == null, OdbcFoldingWarnings.NonOdbcQueryAtJoin("right")) || !tracingService.RowRangeIsAll(odbcQuery.RowRange) || !tracingService.RowRangeIsAll(odbcQuery2.RowRange) || tracingService.When(!odbcQuery.QueryDomain.Equals(odbcQuery2.QueryDomain), "The left and right queries come from different data sources.") || tracingService.When<FoldingWarnings.FoldingWarning<string, FoldingWarnings.StringFormatter<SqlSettings>>>(!Alias.TryNewAssignedAlias("OTBL", this.sqlSettings, out alias), OdbcFoldingWarnings.InvalidCharacter("OTBL", this.sqlSettings)) || tracingService.When<FoldingWarnings.FoldingWarning<string, FoldingWarnings.StringFormatter<SqlSettings>>>(!Alias.TryNewAssignedAlias("ITBL", this.sqlSettings, out alias2), OdbcFoldingWarnings.InvalidCharacter("ITBL", this.sqlSettings)))
					{
						throw tracingService.NewFoldingFailureException(null);
					}
					FromItem tableReference = OdbcQueryDomain.OdbcOptimizingQueryVisitor.GetTableReference(odbcQuery);
					tableReference.Alias = alias;
					FromItem tableReference2 = OdbcQueryDomain.OdbcOptimizingQueryVisitor.GetTableReference(odbcQuery2);
					tableReference2.Alias = alias2;
					bool flag2;
					UnaryLogicalOperator unaryLogicalOperator;
					bool flag = this.IsAntiSemiJoin(joinQuery.JoinKind, out flag2, out unaryLogicalOperator);
					OdbcQueryExpressionVisitor odbcQueryExpressionVisitor = null;
					if (flag)
					{
						odbcQueryExpressionVisitor = this.domain.NewQueryExpressionVisitor(EmptyArray<SelectItem>.Instance, EmptyArray<OdbcQueryColumnInfo>.Instance, false, null);
					}
					SelectItem[] array = new SelectItem[joinQuery.JoinColumns.Length];
					OdbcQueryColumnInfo[] array2 = new OdbcQueryColumnInfo[joinQuery.JoinColumns.Length];
					OdbcColumnNameGenerator odbcColumnNameGenerator = new OdbcColumnNameGenerator(this.sqlSettings, null);
					for (int i = 0; i < array2.Length; i++)
					{
						JoinColumn joinColumn = joinQuery.JoinColumns[i];
						OdbcQuery odbcQuery3;
						int num;
						Alias alias3;
						if (joinColumn.Left)
						{
							odbcQuery3 = odbcQuery;
							num = joinColumn.LeftColumn;
							alias3 = alias;
						}
						else
						{
							odbcQuery3 = odbcQuery2;
							num = joinColumn.RightColumn;
							alias3 = alias2;
						}
						SelectItem selectItem = odbcQuery3.QuerySpecification.SelectItems[num];
						Alias alias4 = null;
						if (!odbcColumnNameGenerator.TryName(selectItem.Name.Name))
						{
							if (tracingService.When<FoldingWarnings.FoldingWarning<string, string, string>>(!this.domain.dataSource.Info.SupportsColumnAliases, FoldingWarnings.InvalidSqlGetInfo<string, string, string>("Y", "N", "SQL_COLUMN_ALIAS")) || tracingService.When(this.domain.DataSource.Info.IdentifierSpecialCharacters == null, "This ODBC driver doesn't provide an invalid character list."))
							{
								throw tracingService.NewFoldingFailureException(null);
							}
							alias4 = odbcColumnNameGenerator.GetNextName();
						}
						if (flag && flag2 != joinColumn.Left)
						{
							SqlExpression sqlExpression = SqlConstant.Null;
							OdbcTypeMap odbcTypeMap = odbcQuery3.ColumnInfos[num].TypeInfo.OdbcTypeMap;
							if (odbcTypeMap != null)
							{
								sqlExpression = odbcQueryExpressionVisitor.CallConvertOrCast(sqlExpression, odbcTypeMap);
							}
							array[i] = new SelectItem(sqlExpression, alias4 ?? selectItem.Name);
						}
						else
						{
							array[i] = new SelectItem(new ColumnReference(alias3, selectItem.Name), alias4);
						}
						array2[i] = odbcQuery3.ColumnInfos[num];
					}
					Condition condition = null;
					OdbcQueryExpressionVisitor odbcQueryExpressionVisitor2 = this.domain.NewQueryExpressionVisitor(array, array2, false, null);
					for (int j = 0; j < joinQuery.LeftKeyColumns.Length; j++)
					{
						int num2 = joinQuery.LeftKeyColumns[j];
						int num3 = joinQuery.RightKeyColumns[j];
						SelectItem selectItem2 = odbcQuery.QuerySpecification.SelectItems[num2];
						SelectItem selectItem3 = odbcQuery2.QuerySpecification.SelectItems[num3];
						bool flag3 = false;
						if (joinQuery.KeyEqualityComparers != null && joinQuery.KeyEqualityComparers.Length > j)
						{
							if (joinQuery.KeyEqualityComparers[j].FunctionIdentity.Equals(Library._Value.NullableEquals.FunctionIdentity))
							{
								flag3 = true;
							}
							else if (!joinQuery.KeyEqualityComparers[j].FunctionIdentity.Equals(Library._Value.Equals.FunctionIdentity))
							{
								throw tracingService.NewFoldingFailureException("Key comparer function should be Value.Equals or Value.NullableEquals.");
							}
						}
						ColumnReference columnReference = new ColumnReference(alias, selectItem2.Name);
						ColumnReference columnReference2 = new ColumnReference(alias2, selectItem3.Name);
						Condition condition2 = odbcQueryExpressionVisitor2.VisitEquals(new OdbcScalarExpression(odbcQuery.ColumnInfos[num2].TypeInfo, columnReference), new OdbcScalarExpression(odbcQuery2.ColumnInfos[num3].TypeInfo, columnReference2), Precision.Double, flag3);
						if (condition == null)
						{
							condition = condition2;
						}
						else
						{
							condition = new ConditionOperation(condition, ConditionOperator.And, condition2);
						}
					}
					OdbcQuerySpecification odbcQuerySpecification;
					if (condition != null && flag)
					{
						FromItem fromItem = (flag2 ? tableReference : tableReference2);
						FromItem fromItem2 = (flag2 ? tableReference2 : tableReference);
						odbcQuerySpecification = new OdbcQuerySpecification
						{
							SelectItems = array,
							FromItems = new FromItem[] { fromItem },
							WhereClause = new UnaryLogicalOperation(unaryLogicalOperator, new OdbcQuerySpecification
							{
								SelectItems = new SelectItem[]
								{
									new SelectItem(SqlConstant.One, null)
								},
								FromItems = new FromItem[] { fromItem2 },
								WhereClause = condition
							})
						};
					}
					else
					{
						JoinOperator joinOperator = this.GetJoinOperator(joinQuery.JoinKind);
						if (condition == null)
						{
							if (joinOperator != JoinOperator.InnerJoin)
							{
								throw tracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<TableTypeAlgebra.JoinKind>>(FoldingWarnings.InnerJoinOnly(joinQuery.JoinKind));
							}
							joinOperator = JoinOperator.CrossJoin;
						}
						if (!OdbcQueryDomain.IsSql92JoinOperatorSupported(this.domain.dataSource.Info, joinOperator))
						{
							throw tracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<TableTypeAlgebra.JoinKind>>(FoldingWarnings.NotSupportJoinByDriver(joinQuery.JoinKind));
						}
						JoinOperation joinOperation = new JoinOperation
						{
							JoinCondition = condition,
							Left = tableReference,
							Operator = joinOperator,
							Right = tableReference2
						};
						odbcQuerySpecification = new OdbcQuerySpecification
						{
							SelectItems = array,
							FromItems = new FromItem[] { joinOperation }
						};
					}
					query = this.domain.NewQuery(array2, odbcQuerySpecification, joinQuery.TableKeys, false, TableSortOrder.Unknown, RowRange.All).Take(joinQuery.TakeCount);
				}
				return query;
			}

			// Token: 0x060031BF RID: 12735 RVA: 0x00098FD0 File Offset: 0x000971D0
			private static FromItem GetTableReference(OdbcQuery odbcQuery)
			{
				if (odbcQuery.CanInlineFrom)
				{
					return odbcQuery.QuerySpecification.FromItems[0].ShallowCopy();
				}
				OdbcQuerySpecification odbcQuerySpecification = odbcQuery.QuerySpecification.ShallowCopy();
				odbcQuerySpecification.OrderByClause = null;
				return new FromQuery
				{
					Query = odbcQuerySpecification
				};
			}

			// Token: 0x060031C0 RID: 12736 RVA: 0x0009901C File Offset: 0x0009721C
			private JoinOperator GetJoinOperator(TableTypeAlgebra.JoinKind joinKind)
			{
				JoinOperator joinOperator;
				using (this.domain.tracingService.NewScope("GetJoinOperator"))
				{
					switch (joinKind)
					{
					case TableTypeAlgebra.JoinKind.Inner:
						joinOperator = JoinOperator.InnerJoin;
						break;
					case TableTypeAlgebra.JoinKind.LeftOuter:
						joinOperator = JoinOperator.LeftOuterJoin;
						break;
					case TableTypeAlgebra.JoinKind.FullOuter:
						joinOperator = JoinOperator.FullOuterJoin;
						break;
					case TableTypeAlgebra.JoinKind.RightOuter:
						joinOperator = JoinOperator.RightOuterJoin;
						break;
					default:
						throw this.domain.tracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<TableTypeAlgebra.JoinKind>>(FoldingWarnings.UnsupportedJoinKind(joinKind));
					}
				}
				return joinOperator;
			}

			// Token: 0x060031C1 RID: 12737 RVA: 0x0009909C File Offset: 0x0009729C
			private bool IsAntiSemiJoin(TableTypeAlgebra.JoinKind joinKind, out bool left, out UnaryLogicalOperator unaryOperator)
			{
				switch (joinKind)
				{
				case TableTypeAlgebra.JoinKind.LeftAnti:
					left = true;
					unaryOperator = UnaryLogicalOperator.NotExists;
					return true;
				case TableTypeAlgebra.JoinKind.RightAnti:
					left = false;
					unaryOperator = UnaryLogicalOperator.NotExists;
					return true;
				case TableTypeAlgebra.JoinKind.LeftSemi:
					left = true;
					unaryOperator = UnaryLogicalOperator.Exists;
					return true;
				case TableTypeAlgebra.JoinKind.RightSemi:
					left = false;
					unaryOperator = UnaryLogicalOperator.Exists;
					return true;
				default:
					left = false;
					unaryOperator = UnaryLogicalOperator.None;
					return false;
				}
			}

			// Token: 0x04001626 RID: 5670
			private readonly OdbcQueryDomain domain;

			// Token: 0x04001627 RID: 5671
			private readonly SqlSettings sqlSettings;
		}
	}
}
