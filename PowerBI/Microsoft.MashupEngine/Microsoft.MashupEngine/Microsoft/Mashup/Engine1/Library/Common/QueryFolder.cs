using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001109 RID: 4361
	internal class QueryFolder
	{
		// Token: 0x0600720B RID: 29195 RVA: 0x00187F04 File Offset: 0x00186104
		public static Query Fold(Query query)
		{
			query = ExpandColumnsVisitor.ExpandColumns(query);
			query = new QueryFolder.FloatEmptyColumnSelectionsVisitor().VisitQuery(query);
			QueryFolder.ExpressionTableValue expressionTableValue = new QueryFolder().VisitQuery(query);
			return expressionTableValue.CantFold(expressionTableValue.Query).Query;
		}

		// Token: 0x0600720C RID: 29196 RVA: 0x00187F48 File Offset: 0x00186148
		private QueryFolder.ExpressionTableValue VisitQuery(Query query)
		{
			switch (query.Kind)
			{
			case QueryKind.DataSource:
				return this.VisitTable((DataSourceQuery)query);
			case QueryKind.ProjectColumns:
				return this.VisitSelectColumns((ProjectColumnsQuery)query);
			case QueryKind.SelectRows:
				return this.VisitSelectRows((SelectRowsQuery)query);
			case QueryKind.AddColumns:
				return this.VisitAddColumns((AddColumnsQuery)query);
			case QueryKind.SkipTake:
				return this.VisitSkipTake((SkipTakeQuery)query);
			case QueryKind.Sort:
				return this.VisitSort((SortQuery)query);
			case QueryKind.Distinct:
				return this.VisitDistinct((DistinctQuery)query);
			case QueryKind.Combine:
				return this.VisitCombine((CombineQuery)query);
			case QueryKind.Group:
				return this.VisitGroup((GroupQuery)query);
			case QueryKind.Join:
				return this.VisitJoin((JoinQuery)query);
			case QueryKind.NestedJoin:
				return this.VisitNestedJoin((NestedJoinQuery)query);
			case QueryKind.ExpandListColumn:
				return this.VisitExpandListColumn((ExpandListColumnQuery)query);
			case QueryKind.ExpandRecordColumn:
				return this.VisitExpandRecordColumn((ExpandRecordColumnQuery)query);
			case QueryKind.Unpivot:
				return this.VisitUnpivot((UnpivotQuery)query);
			case QueryKind.Pivot:
				return this.VisitPivot((PivotQuery)query);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600720D RID: 29197 RVA: 0x0018806C File Offset: 0x0018626C
		private QueryFolder.ExpressionTableValue VisitTable(DataSourceQuery _query)
		{
			TableQuery tableQuery = (TableQuery)_query;
			QueryFolder.ExpressionTableValue expressionTableValue = new QueryFolder.ExpressionTableValue(new ConstantExpressionSyntaxNode(tableQuery.Table), tableQuery);
			int[] expandColumnns = tableQuery.ExpandColumnns;
			if (expandColumnns != null)
			{
				expressionTableValue = expressionTableValue.ForceColumns(expandColumnns);
			}
			return expressionTableValue;
		}

		// Token: 0x0600720E RID: 29198 RVA: 0x001880A8 File Offset: 0x001862A8
		private QueryFolder.ExpressionTableValue VisitAddColumns(AddColumnsQuery query)
		{
			return this.VisitQuery(query.InnerQuery).AddColumns(query);
		}

		// Token: 0x0600720F RID: 29199 RVA: 0x001880CC File Offset: 0x001862CC
		private QueryFolder.ExpressionTableValue VisitSelectColumns(ProjectColumnsQuery query)
		{
			return this.VisitQuery(query.InnerQuery).SelectColumns(query);
		}

		// Token: 0x06007210 RID: 29200 RVA: 0x001880F0 File Offset: 0x001862F0
		private QueryFolder.ExpressionTableValue VisitSkipTake(SkipTakeQuery query)
		{
			return this.VisitQuery(query.InnerQuery).SkipTake(query);
		}

		// Token: 0x06007211 RID: 29201 RVA: 0x00188114 File Offset: 0x00186314
		private QueryFolder.ExpressionTableValue VisitSelectRows(SelectRowsQuery query)
		{
			return this.VisitQuery(query.InnerQuery).SelectRows(query);
		}

		// Token: 0x06007212 RID: 29202 RVA: 0x00188138 File Offset: 0x00186338
		private QueryFolder.ExpressionTableValue VisitSort(SortQuery query)
		{
			return this.VisitQuery(query.InnerQuery).Sort(query);
		}

		// Token: 0x06007213 RID: 29203 RVA: 0x0018815C File Offset: 0x0018635C
		private QueryFolder.ExpressionTableValue VisitDistinct(DistinctQuery query)
		{
			return this.VisitQuery(query.InnerQuery).Distinct(query);
		}

		// Token: 0x06007214 RID: 29204 RVA: 0x00188180 File Offset: 0x00186380
		private QueryFolder.ExpressionTableValue VisitCombine(CombineQuery query)
		{
			QueryFolder.ExpressionTableValue[] array = new QueryFolder.ExpressionTableValue[query.Queries.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.VisitQuery(query.Queries[i]);
			}
			return QueryFolder.ExpressionTableValue.Combine(query, array);
		}

		// Token: 0x06007215 RID: 29205 RVA: 0x001881C8 File Offset: 0x001863C8
		private QueryFolder.ExpressionTableValue VisitGroup(GroupQuery query)
		{
			return this.VisitQuery(query.InnerQuery).Group(query);
		}

		// Token: 0x06007216 RID: 29206 RVA: 0x001881EC File Offset: 0x001863EC
		private QueryFolder.ExpressionTableValue VisitJoin(JoinQuery query)
		{
			QueryFolder.ExpressionTableValue expressionTableValue = this.VisitQuery(query.LeftQuery);
			QueryFolder.ExpressionTableValue expressionTableValue2 = this.VisitQuery(query.RightQuery);
			return QueryFolder.ExpressionTableValue.Join(query, expressionTableValue, expressionTableValue2);
		}

		// Token: 0x06007217 RID: 29207 RVA: 0x0018821C File Offset: 0x0018641C
		private QueryFolder.ExpressionTableValue VisitNestedJoin(NestedJoinQuery query)
		{
			return this.VisitQuery(query.LeftQuery).NestedJoin(query);
		}

		// Token: 0x06007218 RID: 29208 RVA: 0x00188240 File Offset: 0x00186440
		private QueryFolder.ExpressionTableValue VisitExpandListColumn(ExpandListColumnQuery query)
		{
			return this.VisitQuery(query.InnerQuery).ExpandListColumn(query);
		}

		// Token: 0x06007219 RID: 29209 RVA: 0x00188264 File Offset: 0x00186464
		private QueryFolder.ExpressionTableValue VisitExpandRecordColumn(ExpandRecordColumnQuery query)
		{
			return this.VisitQuery(query.InnerQuery).ExpandRecordColumn(query);
		}

		// Token: 0x0600721A RID: 29210 RVA: 0x00188288 File Offset: 0x00186488
		private QueryFolder.ExpressionTableValue VisitPivot(PivotQuery query)
		{
			return this.VisitQuery(query.InnerQuery).Pivot(query);
		}

		// Token: 0x0600721B RID: 29211 RVA: 0x001882AC File Offset: 0x001864AC
		private QueryFolder.ExpressionTableValue VisitUnpivot(UnpivotQuery query)
		{
			return this.VisitQuery(query.InnerQuery).Unpivot(query);
		}

		// Token: 0x0200110A RID: 4362
		private struct ExpressionTableValue
		{
			// Token: 0x0600721D RID: 29213 RVA: 0x001882D0 File Offset: 0x001864D0
			public ExpressionTableValue(IExpression expression, Query query)
			{
				if (expression != null)
				{
					this.expression = (IConstantExpression)expression;
					QueryProcessor queryProcessor;
					if (this.expression.Value.TryGetProcessor(out queryProcessor))
					{
						this.processor = queryProcessor;
					}
					else
					{
						this.processor = null;
						this.expression = null;
					}
				}
				else
				{
					this.processor = null;
					this.expression = null;
				}
				this.query = query;
			}

			// Token: 0x0600721E RID: 29214 RVA: 0x0018832E File Offset: 0x0018652E
			public QueryFolder.ExpressionTableValue CantFold(Query query)
			{
				return new QueryFolder.ExpressionTableValue(null, query);
			}

			// Token: 0x17001FF3 RID: 8179
			// (get) Token: 0x0600721F RID: 29215 RVA: 0x00188337 File Offset: 0x00186537
			public bool Folds
			{
				get
				{
					return this.expression != null;
				}
			}

			// Token: 0x17001FF4 RID: 8180
			// (get) Token: 0x06007220 RID: 29216 RVA: 0x00188342 File Offset: 0x00186542
			public Query Query
			{
				get
				{
					if (this.Folds)
					{
						return this.expression.Value.AsTable.Query;
					}
					return this.query;
				}
			}

			// Token: 0x06007221 RID: 29217 RVA: 0x00188368 File Offset: 0x00186568
			public QueryFolder.ExpressionTableValue SelectColumns(ProjectColumnsQuery query)
			{
				if (query.RenameReorder)
				{
					return this.SelectColumns(query.ColumnSelection, this.Query.RenameReorderColumns(query.ColumnSelection), query);
				}
				return this.SelectColumns(query.ColumnSelection, this.Query.SelectColumns(query.ColumnSelection), query);
			}

			// Token: 0x06007222 RID: 29218 RVA: 0x001883BC File Offset: 0x001865BC
			private QueryFolder.ExpressionTableValue SelectColumns(ColumnSelection columnSelection, Query newQuery, ProjectColumnsQuery query)
			{
				if (this.processor != null)
				{
					IExpression expression = new ExclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore);
					bool flag = true;
					VariableInitializer[] array = new VariableInitializer[columnSelection.Keys.Length];
					for (int i = 0; i < array.Length; i++)
					{
						string text = this.query.Columns[columnSelection.GetColumn(i)];
						string text2 = columnSelection.Keys[i];
						if (text != text2)
						{
							flag = false;
						}
						array[i] = new VariableInitializer(Identifier.New(text2), new RequiredFieldAccessExpressionSyntaxNode(expression, Identifier.New(text)));
					}
					IExpression expression2;
					if (flag)
					{
						Identifier[] array2 = new Identifier[columnSelection.Keys.Length];
						for (int j = 0; j < array2.Length; j++)
						{
							array2[j] = Identifier.New(columnSelection.Keys[j]);
						}
						expression2 = new RequiredMultiFieldRecordProjectionExpressionSyntaxNode(this.expression, array2);
					}
					else
					{
						expression2 = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(Library.ListRuntime.Transform), new IExpression[]
						{
							this.expression,
							new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, new RecordExpressionSyntaxNode(array))
						});
					}
					IExpression expression3;
					if (this.processor.TryFold(query, expression2, out expression3))
					{
						return new QueryFolder.ExpressionTableValue(expression3, newQuery);
					}
				}
				return this.CantFold(newQuery);
			}

			// Token: 0x06007223 RID: 29219 RVA: 0x00188500 File Offset: 0x00186700
			public QueryFolder.ExpressionTableValue AddColumns(AddColumnsQuery query)
			{
				ColumnsConstructor columnsConstructor = query.ColumnsConstructor;
				IList<QueryExpression> queryExpressions = query.QueryExpressions;
				Query query2 = this.Query.AddColumns(columnsConstructor);
				if (this.processor != null && queryExpressions != null)
				{
					Keys columns = this.query.Columns;
					IExpression expression = new ExclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore);
					VariableInitializer[] array = new VariableInitializer[columns.Length + columnsConstructor.Names.Length];
					for (int i = 0; i < columns.Length; i++)
					{
						Identifier identifier = Identifier.New(columns[i]);
						array[i] = new VariableInitializer(identifier, new RequiredFieldAccessExpressionSyntaxNode(expression, Identifier.New(identifier)));
					}
					for (int j = 0; j < columnsConstructor.Names.Length; j++)
					{
						IFunctionExpression functionExpression = FunctionExpressionBuilder.ToExpression(columns, Identifier.Underscore, queryExpressions[j]);
						array[columns.Length + j] = new VariableInitializer(Identifier.New(columnsConstructor.Names[j]), functionExpression.Expression);
					}
					IExpression expression2 = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(Library.ListRuntime.Transform), new IExpression[]
					{
						this.expression,
						new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, new RecordExpressionSyntaxNode(array))
					});
					IExpression expression3;
					if (this.processor.TryFold(query, expression2, out expression3))
					{
						return new QueryFolder.ExpressionTableValue(expression3, query2);
					}
				}
				return this.CantFold(query2);
			}

			// Token: 0x06007224 RID: 29220 RVA: 0x00188664 File Offset: 0x00186864
			public QueryFolder.ExpressionTableValue NestedJoin(NestedJoinQuery query)
			{
				Query query2 = this.Query.NestedJoin(query.LeftKeyColumns, query.DelayedRightTable, query.RightKey, query.JoinKind, query.NewColumnName, query.JoinKeys, query.KeyEqualityComparers);
				return this.CantFold(query2);
			}

			// Token: 0x06007225 RID: 29221 RVA: 0x001886B0 File Offset: 0x001868B0
			public QueryFolder.ExpressionTableValue SkipTake(SkipTakeQuery query)
			{
				return this.Skip(query).Take(query, query.RowRange.TakeCount);
			}

			// Token: 0x06007226 RID: 29222 RVA: 0x001886DC File Offset: 0x001868DC
			public QueryFolder.ExpressionTableValue SelectRows(SelectRowsQuery query)
			{
				QueryExpression queryExpression = query.QueryExpression;
				Query query2 = this.Query.SelectRows(query.Condition);
				if (this.processor != null && queryExpression != null)
				{
					Identifier identifier = Identifier.New(query.Condition.Type.AsFunctionType.ParameterName(0));
					IExpression expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.SelectRows), new IExpression[]
					{
						this.expression,
						FunctionExpressionBuilder.ToExpression(this.query.Columns, identifier, queryExpression)
					});
					IExpression expression2;
					if (this.processor.TryFold(query, expression, out expression2))
					{
						return new QueryFolder.ExpressionTableValue(expression2, query2);
					}
				}
				return this.CantFold(query2);
			}

			// Token: 0x06007227 RID: 29223 RVA: 0x00188780 File Offset: 0x00186980
			public QueryFolder.ExpressionTableValue Sort(SortQuery query)
			{
				TableSortOrder sortOrder = query.SortOrder;
				Query query2 = this.Query.Sort(sortOrder);
				if (this.processor != null)
				{
					IExpression expression = sortOrder.ToExpression();
					if (expression != null)
					{
						IExpression expression2 = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.Sort), new IExpression[] { this.expression, expression });
						IExpression expression3;
						if (this.processor.TryFold(query, expression2, out expression3))
						{
							return new QueryFolder.ExpressionTableValue(expression3, query2).Take(query, query.TakeCount);
						}
					}
				}
				return this.CantFold(query2.Take(query.TakeCount));
			}

			// Token: 0x06007228 RID: 29224 RVA: 0x00188814 File Offset: 0x00186A14
			public QueryFolder.ExpressionTableValue Distinct(DistinctQuery query)
			{
				TableDistinct distinctCriteria = query.DistinctCriteria;
				Query query2 = this.Query.Distinct(distinctCriteria);
				if (this.processor != null && distinctCriteria.AllColumns(QueryTableValue.NewRowType(query)))
				{
					IExpression expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.Distinct), new IExpression[]
					{
						this.expression,
						ConstantExpressionSyntaxNode.Null
					});
					IExpression expression2;
					if (this.processor.TryFold(query, expression, out expression2))
					{
						return new QueryFolder.ExpressionTableValue(expression2, query2);
					}
				}
				return this.CantFold(query2);
			}

			// Token: 0x06007229 RID: 29225 RVA: 0x00188894 File Offset: 0x00186A94
			public static QueryFolder.ExpressionTableValue Combine(CombineQuery query, QueryFolder.ExpressionTableValue[] tables)
			{
				Query[] array = new Query[tables.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = tables[i].Query;
				}
				Query query2 = Query.Combine(array, query.ColumnTypes, query.SortOrder, query.DisjointColumn);
				QueryProcessor queryProcessor = tables[0].processor;
				int num = 1;
				while (queryProcessor != null && num < tables.Length)
				{
					if (queryProcessor != tables[num].processor)
					{
						queryProcessor = null;
					}
					num++;
				}
				if (queryProcessor != null && query.SortOrder == null)
				{
					IExpression[] array2 = new IExpression[tables.Length];
					for (int j = 0; j < array2.Length; j++)
					{
						array2[j] = tables[j].expression;
					}
					IExpression expression = new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(Library.List.Combine), new ListExpressionSyntaxNode(array2));
					IExpression expression2;
					if (queryProcessor.TryFold(query, expression, out expression2))
					{
						return new QueryFolder.ExpressionTableValue(expression2, query2);
					}
				}
				return new QueryFolder.ExpressionTableValue(null, query2);
			}

			// Token: 0x0600722A RID: 29226 RVA: 0x00188983 File Offset: 0x00186B83
			public QueryFolder.ExpressionTableValue ExpandListColumn(ExpandListColumnQuery query)
			{
				return new QueryFolder.ExpressionTableValue(null, this.Query.ExpandListColumn(query.ColumnIndex, query.SingleOrDefault));
			}

			// Token: 0x0600722B RID: 29227 RVA: 0x001889A2 File Offset: 0x00186BA2
			public QueryFolder.ExpressionTableValue ExpandRecordColumn(ExpandRecordColumnQuery query)
			{
				return new QueryFolder.ExpressionTableValue(null, this.Query.ExpandRecordColumn(query.ColumnToExpand, query.FieldsToProject, query.NewColumns));
			}

			// Token: 0x0600722C RID: 29228 RVA: 0x001889C8 File Offset: 0x00186BC8
			public QueryFolder.ExpressionTableValue ForceColumns(int[] columnIndices)
			{
				Query query = this.Query;
				if (this.processor != null)
				{
					IExpression[] array = new IExpression[columnIndices.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = new RequiredFieldAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), Identifier.New(this.Query.Columns[columnIndices[i]]));
					}
					IExpression expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.ForceColumns), this.expression, new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { Identifier.Underscore }), new ListExpressionSyntaxNode(array)));
					IExpression expression2;
					if (this.processor.TryFold(this.query, expression, out expression2))
					{
						return new QueryFolder.ExpressionTableValue(expression2, query);
					}
				}
				return this;
			}

			// Token: 0x0600722D RID: 29229 RVA: 0x00188A88 File Offset: 0x00186C88
			public QueryFolder.ExpressionTableValue Group(GroupQuery query)
			{
				Grouping grouping = query.Grouping;
				Query query2 = this.Query.Group(grouping);
				if (this.processor != null && grouping.Comparer == null && !grouping.Adjacent)
				{
					IExpression expression;
					if (!grouping.CompareRecords)
					{
						expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, new RequiredFieldAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), Identifier.New(this.query.Columns[grouping.KeyColumns[0]])));
					}
					else
					{
						Identifier[] array = new Identifier[grouping.KeyColumns.Length];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = Identifier.New(this.query.Columns[grouping.KeyColumns[i]]);
						}
						expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, new RequiredMultiFieldRecordProjectionExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), array));
					}
					Identifier identifier = Identifier.New("rows");
					VariableInitializer[] array2 = new VariableInitializer[grouping.ResultKeys.Length];
					for (int j = 0; j < grouping.KeyColumns.Length; j++)
					{
						Identifier identifier2 = Identifier.New(this.query.Columns[grouping.KeyColumns[j]]);
						array2[j] = new VariableInitializer(identifier2, new RequiredFieldAccessExpressionSyntaxNode(new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.First), new IExpression[]
						{
							new ExclusiveIdentifierExpressionSyntaxNode(identifier),
							ConstantExpressionSyntaxNode.Null
						}), identifier2));
					}
					for (int k = 0; k < grouping.Constructors.Length; k++)
					{
						ColumnConstructor columnConstructor = grouping.Constructors[k];
						QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewTableType(this.query), columnConstructor.Function);
						IExpression expression2 = FunctionExpressionBuilder.ToExpression(this.query.Columns, identifier, queryExpression).Expression;
						expression2 = QueryFolder.ExpressionTableValue.ComposeListCountExpression(expression2);
						array2[grouping.KeyColumns.Length + k] = new VariableInitializer(Identifier.New(columnConstructor.Name), expression2);
					}
					IExpression expression3 = new FunctionExpressionSyntaxNode(new FunctionTypeSyntaxNode(null, new IParameter[]
					{
						new ParameterSyntaxNode(identifier, null)
					}, 1), new RecordExpressionSyntaxNode(array2));
					IExpression expression4 = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.Group), new IExpression[] { this.expression, expression, expression3 });
					IExpression expression5;
					if (this.processor.TryFold(query, expression4, out expression5))
					{
						return new QueryFolder.ExpressionTableValue(expression5, query2);
					}
				}
				return this.CantFold(query2);
			}

			// Token: 0x0600722E RID: 29230 RVA: 0x00188CF0 File Offset: 0x00186EF0
			private static IExpression ComposeListCountExpression(IExpression expression)
			{
				bool flag = false;
				QueryFolder.ExpressionTableValue.SelectionPredicate selectionPredicate = QueryFolder.ExpressionTableValue.SelectionPredicate.None;
				IList<IExpression> list;
				if (expression.TryGetInvocation(LanguageLibrary.List.Count, 1, out list))
				{
					expression = list[0];
					for (;;)
					{
						IList<IExpression> list2;
						if (expression.TryGetInvocation(LanguageLibrary.List.Distinct, 1, out list2))
						{
							expression = list2[0];
							flag = true;
						}
						else
						{
							IList<IExpression> list3;
							IFunctionExpression functionExpression;
							QueryFolder.ExpressionTableValue.SelectionPredicate selectionPredicate2;
							if (!expression.TryGetInvocation(LanguageLibrary.List.Select, 2, out list3) || !QueryFolder.ExpressionTableValue.TryGetFunction(list3[1], out functionExpression) || !QueryFolder.ExpressionTableValue.TryGetIsNullOrIsNotNullFilterExpression(functionExpression, out selectionPredicate2))
							{
								goto IL_00F9;
							}
							if (selectionPredicate != QueryFolder.ExpressionTableValue.SelectionPredicate.None && selectionPredicate != selectionPredicate2)
							{
								break;
							}
							selectionPredicate = selectionPredicate2;
							expression = list3[0];
						}
					}
					return new ConstantExpressionSyntaxNode(NumberValue.Zero);
					IL_00F9:
					FunctionValue functionValue = null;
					switch (selectionPredicate)
					{
					case QueryFolder.ExpressionTableValue.SelectionPredicate.None:
						functionValue = (flag ? Library.List.CountOfDistinct : LanguageLibrary.List.Count);
						break;
					case QueryFolder.ExpressionTableValue.SelectionPredicate.IsNull:
						functionValue = (flag ? Library.List.CountOfDistinctNull : Library.List.CountOfNull);
						break;
					case QueryFolder.ExpressionTableValue.SelectionPredicate.IsNotNull:
						functionValue = (flag ? Library.List.CountOfDistinctNotNull : Library.List.CountOfNotNull);
						break;
					}
					if (functionValue != null)
					{
						expression = new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(functionValue), expression);
					}
					return expression;
				}
				ExpressionKind kind = expression.Kind;
				if (kind == ExpressionKind.Binary)
				{
					IBinaryExpression binaryExpression = expression as IBinaryExpression;
					return BinaryExpressionSyntaxNode.New(binaryExpression.Operator, QueryFolder.ExpressionTableValue.ComposeListCountExpression(binaryExpression.Left), QueryFolder.ExpressionTableValue.ComposeListCountExpression(binaryExpression.Right), binaryExpression.Range);
				}
				if (kind == ExpressionKind.Unary)
				{
					IUnaryExpression unaryExpression = expression as IUnaryExpression;
					return UnaryExpressionSyntaxNode.New(unaryExpression.Operator, QueryFolder.ExpressionTableValue.ComposeListCountExpression(unaryExpression.Expression), unaryExpression.Range);
				}
				return expression;
			}

			// Token: 0x0600722F RID: 29231 RVA: 0x00188E54 File Offset: 0x00187054
			private static bool TryGetFunction(IExpression expression, out IFunctionExpression function)
			{
				function = expression as IFunctionExpression;
				Value value;
				if (function == null && expression.TryGetConstant(out value))
				{
					function = value.Expression as IFunctionExpression;
				}
				return function != null;
			}

			// Token: 0x06007230 RID: 29232 RVA: 0x00188E8C File Offset: 0x0018708C
			private static bool TryGetIsNullOrIsNotNullFilterExpression(IFunctionExpression filter, out QueryFolder.ExpressionTableValue.SelectionPredicate predicate)
			{
				bool flag;
				if (filter.TryGetIsNullOrIsNotNullFilter(out flag))
				{
					predicate = (flag ? QueryFolder.ExpressionTableValue.SelectionPredicate.IsNull : QueryFolder.ExpressionTableValue.SelectionPredicate.IsNotNull);
					return true;
				}
				predicate = QueryFolder.ExpressionTableValue.SelectionPredicate.None;
				return false;
			}

			// Token: 0x06007231 RID: 29233 RVA: 0x00188EB4 File Offset: 0x001870B4
			public static QueryFolder.ExpressionTableValue Join(JoinQuery query, QueryFolder.ExpressionTableValue leftTable, QueryFolder.ExpressionTableValue rightTable)
			{
				int[] leftKeyColumns = query.LeftKeyColumns;
				int[] rightKeyColumns = query.RightKeyColumns;
				Query query2 = Query.Join(query.TakeCount, leftTable.Query, leftKeyColumns, rightTable.Query, rightKeyColumns, query.JoinKind, query.JoinKeys, query.JoinColumns, query.JoinAlgorithm, query.KeyEqualityComparers);
				NumberValue numberValue;
				if (leftTable.processor == rightTable.processor && leftTable.processor != null && Library.JoinKind.Type.TryLookupEnum(query.JoinKind, out numberValue))
				{
					IExpression expression;
					if (query.KeyEqualityComparers != null)
					{
						expression = new ListExpressionSyntaxNode(query.KeyEqualityComparers.Select((FunctionValue kec) => new ConstantExpressionSyntaxNode(kec)).ToArray<ConstantExpressionSyntaxNode>());
					}
					else
					{
						expression = ConstantExpressionSyntaxNode.Null;
					}
					IExpression expression2 = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.Join), new IExpression[]
					{
						leftTable.expression,
						QueryFolder.ExpressionTableValue.GetJoinKeys(query.LeftQuery.Columns, leftKeyColumns),
						rightTable.expression,
						QueryFolder.ExpressionTableValue.GetJoinKeys(query.RightQuery.Columns, rightKeyColumns),
						new ConstantExpressionSyntaxNode(numberValue),
						ConstantExpressionSyntaxNode.Null,
						expression
					});
					IExpression expression3;
					if (leftTable.processor.TryFold(query, expression2, out expression3))
					{
						return new QueryFolder.ExpressionTableValue(expression3, query2).Take(query, query.TakeCount);
					}
				}
				return new QueryFolder.ExpressionTableValue(null, query2);
			}

			// Token: 0x06007232 RID: 29234 RVA: 0x0018901C File Offset: 0x0018721C
			private static IExpression GetJoinKeys(Keys keys, int[] columns)
			{
				IExpression[] array = new IExpression[columns.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new ConstantExpressionSyntaxNode(TextValue.New(keys[columns[i]]));
				}
				return new ListExpressionSyntaxNode(array);
			}

			// Token: 0x06007233 RID: 29235 RVA: 0x0018905C File Offset: 0x0018725C
			private QueryFolder.ExpressionTableValue Skip(SkipTakeQuery query)
			{
				RowCount skipCount = query.RowRange.SkipCount;
				if (skipCount.IsZero)
				{
					return this;
				}
				Query query2 = this.Query.Skip(skipCount);
				if (this.processor != null)
				{
					IExpression expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.Skip), new IExpression[]
					{
						this.expression,
						new ConstantExpressionSyntaxNode(NumberValue.New(skipCount.Value))
					});
					IExpression expression2;
					if (this.processor.TryFold(query, expression, out expression2))
					{
						return new QueryFolder.ExpressionTableValue(expression2, query2);
					}
				}
				return this.CantFold(query2);
			}

			// Token: 0x06007234 RID: 29236 RVA: 0x001890F4 File Offset: 0x001872F4
			private QueryFolder.ExpressionTableValue Take(Query query, RowCount count)
			{
				if (count.IsInfinite)
				{
					return this;
				}
				Query query2 = SkipTakeQuery.New(RowRange.All.Take(count), this.Query, false);
				if (this.processor != null)
				{
					IExpression expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.FirstN), new IExpression[]
					{
						this.expression,
						new ConstantExpressionSyntaxNode(NumberValue.New(count.Value))
					});
					IExpression expression2;
					if (this.processor.TryFold(query, expression, out expression2))
					{
						return new QueryFolder.ExpressionTableValue(expression2, query2);
					}
				}
				return this.CantFold(query2);
			}

			// Token: 0x06007235 RID: 29237 RVA: 0x00189188 File Offset: 0x00187388
			public QueryFolder.ExpressionTableValue Pivot(PivotQuery query)
			{
				Query query2 = this.Query.Pivot(query.InputType, query.OutputType, query.PivotValues, query.AttributeColumn, query.ValueColumn, query.AggregateFunction);
				if (this.processor != null)
				{
					IExpression expression = new ConstantExpressionSyntaxNode(TableModule.Table.Pivot);
					IExpression[] array = new IExpression[query.PivotValues.Length];
					for (int i = 0; i < query.PivotValues.Length; i++)
					{
						array[i] = new ConstantExpressionSyntaxNode(TextValue.New(query.PivotValues[i]));
					}
					IExpression expression2 = new ListExpressionSyntaxNode(array);
					IExpression expression3 = new ConstantExpressionSyntaxNode(TextValue.New(query.AttributeColumn));
					IExpression expression4 = new ConstantExpressionSyntaxNode(TextValue.New(query.ValueColumn));
					IExpression expression5 = new ConstantExpressionSyntaxNode(query.AggregateFunction);
					IExpression expression6 = new InvocationExpressionSyntaxNodeN(expression, new IExpression[] { this.expression, expression2, expression3, expression4, expression5 });
					IExpression expression7;
					if (this.processor.TryFold(query, expression6, out expression7))
					{
						return new QueryFolder.ExpressionTableValue(expression7, query2);
					}
				}
				return this.CantFold(query2);
			}

			// Token: 0x06007236 RID: 29238 RVA: 0x0018929C File Offset: 0x0018749C
			public QueryFolder.ExpressionTableValue Unpivot(UnpivotQuery query)
			{
				Query query2 = this.Query.Unpivot(query.InputType, query.OutputType, query.PivotValues, query.AttributeColumn, query.ValueColumn);
				if (this.processor != null)
				{
					IExpression expression = new ConstantExpressionSyntaxNode(TableModule.Table.Unpivot);
					IExpression[] array = new IExpression[query.PivotValues.Length];
					for (int i = 0; i < query.PivotValues.Length; i++)
					{
						array[i] = new ConstantExpressionSyntaxNode(TextValue.New(query.PivotValues[i]));
					}
					IExpression expression2 = new ListExpressionSyntaxNode(array);
					IExpression expression3 = new ConstantExpressionSyntaxNode(TextValue.New(query.AttributeColumn));
					IExpression expression4 = new ConstantExpressionSyntaxNode(TextValue.New(query.ValueColumn));
					IExpression expression5 = new InvocationExpressionSyntaxNodeN(expression, new IExpression[] { this.expression, expression2, expression3, expression4 });
					IExpression expression6;
					if (this.processor.TryFold(query, expression5, out expression6))
					{
						return new QueryFolder.ExpressionTableValue(expression6, query2);
					}
				}
				return this.CantFold(query2);
			}

			// Token: 0x04003EFE RID: 16126
			private QueryProcessor processor;

			// Token: 0x04003EFF RID: 16127
			private IConstantExpression expression;

			// Token: 0x04003F00 RID: 16128
			private Query query;

			// Token: 0x0200110B RID: 4363
			private enum SelectionPredicate
			{
				// Token: 0x04003F02 RID: 16130
				None,
				// Token: 0x04003F03 RID: 16131
				IsNull,
				// Token: 0x04003F04 RID: 16132
				IsNotNull
			}
		}

		// Token: 0x0200110D RID: 4365
		private sealed class FloatEmptyColumnSelectionsVisitor : FoldingQueryVisitor
		{
			// Token: 0x0600723A RID: 29242 RVA: 0x001893AB File Offset: 0x001875AB
			protected override Query VisitSelectColumns(ProjectColumnsQuery query)
			{
				if (query.ColumnSelection.Keys.Length == 0)
				{
					return FloatingSelectColumnsQuery.New(query.ColumnSelection, query.InnerQuery);
				}
				return base.VisitSelectColumns(query);
			}
		}
	}
}
