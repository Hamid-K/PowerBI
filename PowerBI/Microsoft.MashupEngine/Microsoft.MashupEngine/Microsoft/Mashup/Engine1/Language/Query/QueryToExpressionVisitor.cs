using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017BE RID: 6078
	internal abstract class QueryToExpressionVisitor
	{
		// Token: 0x060099C5 RID: 39365 RVA: 0x001FCF19 File Offset: 0x001FB119
		public static IExpression ToResourceExpression(Query query)
		{
			return QueryToExpressionVisitor.simpleVisitor.VisitQuery(query);
		}

		// Token: 0x060099C6 RID: 39366 RVA: 0x001FCF26 File Offset: 0x001FB126
		public static IExpression ToExpression(Query query)
		{
			return new QueryToExpressionVisitor.QueryToFullExpressionVisitor().VisitQuery(query);
		}

		// Token: 0x060099C7 RID: 39367
		public abstract IExpression VisitQuery(Query query);

		// Token: 0x060099C8 RID: 39368
		protected abstract IExpression VisitDataSource(DataSourceQuery query);

		// Token: 0x060099C9 RID: 39369
		protected abstract IExpression VisitProjectColumns(ProjectColumnsQuery query);

		// Token: 0x060099CA RID: 39370 RVA: 0x001FCF34 File Offset: 0x001FB134
		protected bool TryVisitProjectColumns(ProjectColumnsQuery query, out IExpression expression)
		{
			if (query.RenameReorder)
			{
				Keys columns = query.InnerQuery.Columns;
				ColumnSelection columnSelection = query.ColumnSelection;
				ArrayBuilder<Value> arrayBuilder = default(ArrayBuilder<Value>);
				for (int i = 0; i < columnSelection.Keys.Length; i++)
				{
					string text = columnSelection.Keys[i];
					string text2 = columns[columnSelection.GetColumn(i)];
					if (text != text2)
					{
						arrayBuilder.Add(ListValue.New(new Value[]
						{
							TextValue.New(text2),
							TextValue.New(text)
						}));
					}
				}
				expression = this.VisitQuery(query.InnerQuery);
				if (arrayBuilder.Count > 0)
				{
					expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.RenameColumns), new IExpression[]
					{
						expression,
						new ConstantExpressionSyntaxNode(ListValue.New(arrayBuilder.ToArray()))
					});
				}
				if (!columnSelection.Ordered)
				{
					expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.ReorderColumns), new IExpression[]
					{
						expression,
						new ConstantExpressionSyntaxNode(ListValue.New(columnSelection.Keys.ToArray<string>()))
					});
				}
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x060099CB RID: 39371 RVA: 0x001FD055 File Offset: 0x001FB255
		public static IExpression ExpressionOrConstant(Value value)
		{
			if (value.IsFunction)
			{
				return new ConstantExpressionSyntaxNode(value.AsFunction);
			}
			return value.Expression ?? new ConstantExpressionSyntaxNode(value);
		}

		// Token: 0x060099CC RID: 39372 RVA: 0x001FD07B File Offset: 0x001FB27B
		public static IExpression NewNativeQueryExpression(Value value)
		{
			if (value == null || value.IsNull)
			{
				return new NotImplementedExpressionSyntaxNode();
			}
			return new ConstantExpressionSyntaxNode(value);
		}

		// Token: 0x04005156 RID: 20822
		public static readonly Keys ResourceKeys = Keys.New("Kind", "Path");

		// Token: 0x04005157 RID: 20823
		private static readonly QueryToExpressionVisitor simpleVisitor = new QueryToExpressionVisitor.QueryToResourceExpressionVisitor();

		// Token: 0x020017BF RID: 6079
		private class QueryToResourceExpressionVisitor : QueryToExpressionVisitor
		{
			// Token: 0x060099CF RID: 39375 RVA: 0x001FD0B4 File Offset: 0x001FB2B4
			public override IExpression VisitQuery(Query query)
			{
				QueryKind kind = query.Kind;
				if (kind == QueryKind.DataSource)
				{
					return this.VisitDataSource((DataSourceQuery)query);
				}
				if (kind != QueryKind.ProjectColumns)
				{
					return new ConstantExpressionSyntaxNode(new QueryTableValue(query));
				}
				return this.VisitProjectColumns((ProjectColumnsQuery)query);
			}

			// Token: 0x060099D0 RID: 39376 RVA: 0x001FD0F8 File Offset: 0x001FB2F8
			protected override IExpression VisitProjectColumns(ProjectColumnsQuery query)
			{
				IExpression expression;
				if (base.TryVisitProjectColumns(query, out expression))
				{
					return expression;
				}
				return new ConstantExpressionSyntaxNode(new QueryTableValue(query));
			}

			// Token: 0x060099D1 RID: 39377 RVA: 0x001FD120 File Offset: 0x001FB320
			protected override IExpression VisitDataSource(DataSourceQuery query)
			{
				INativeQueryDomain nativeQueryDomain = query.QueryDomain as INativeQueryDomain;
				IResource resource;
				Value value;
				RecordValue recordValue;
				if (nativeQueryDomain != null && nativeQueryDomain.TryGetNativeQuery(query, out resource, out value, out recordValue))
				{
					RecordValue recordValue2 = RecordValue.New(QueryToExpressionVisitor.ResourceKeys, new Value[]
					{
						TextValue.NewOrNull(resource.Kind),
						TextValue.NewOrNull(resource.Path)
					});
					return new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(ResourceModule.Resource.Access), new IExpression[]
					{
						new ConstantExpressionSyntaxNode(recordValue2),
						QueryToExpressionVisitor.NewNativeQueryExpression(value),
						new ConstantExpressionSyntaxNode(recordValue)
					});
				}
				return new ConstantExpressionSyntaxNode(new QueryTableValue(query));
			}
		}

		// Token: 0x020017C0 RID: 6080
		private class QueryToFullExpressionVisitor : QueryToExpressionVisitor
		{
			// Token: 0x060099D4 RID: 39380 RVA: 0x001FD1C0 File Offset: 0x001FB3C0
			public override IExpression VisitQuery(Query query)
			{
				if (this.currentDepth > 1050)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.DocumentReader_ParseDepth, null, null);
				}
				this.currentDepth++;
				IExpression expression;
				try
				{
					switch (query.Kind)
					{
					case QueryKind.DataSource:
						expression = this.VisitDataSource((DataSourceQuery)query);
						break;
					case QueryKind.ProjectColumns:
						expression = this.VisitProjectColumns((ProjectColumnsQuery)query);
						break;
					case QueryKind.SelectRows:
						expression = this.VisitSelectRows((SelectRowsQuery)query);
						break;
					case QueryKind.AddColumns:
						expression = this.VisitAddColumns((AddColumnsQuery)query);
						break;
					case QueryKind.SkipTake:
						expression = this.VisitSkipTake((SkipTakeQuery)query);
						break;
					case QueryKind.Sort:
						expression = this.VisitSort((SortQuery)query);
						break;
					case QueryKind.Distinct:
						expression = this.VisitDistinct((DistinctQuery)query);
						break;
					case QueryKind.Combine:
						expression = this.VisitCombine((CombineQuery)query);
						break;
					case QueryKind.Group:
						expression = this.VisitGroup((GroupQuery)query);
						break;
					case QueryKind.Join:
						expression = this.VisitJoin((JoinQuery)query);
						break;
					case QueryKind.NestedJoin:
						expression = this.VisitNestedJoin((NestedJoinQuery)query);
						break;
					case QueryKind.ExpandListColumn:
						expression = this.VisitExpandListColumn((ExpandListColumnQuery)query);
						break;
					case QueryKind.ExpandRecordColumn:
						expression = this.VisitExpandRecordColumn((ExpandRecordColumnQuery)query);
						break;
					case QueryKind.Unpivot:
						expression = this.VisitUnpivot((UnpivotQuery)query);
						break;
					case QueryKind.Pivot:
						expression = this.VisitPivot((PivotQuery)query);
						break;
					default:
						throw new InvalidOperationException();
					}
				}
				finally
				{
					this.currentDepth--;
				}
				return expression;
			}

			// Token: 0x060099D5 RID: 39381 RVA: 0x001FD36C File Offset: 0x001FB56C
			protected override IExpression VisitProjectColumns(ProjectColumnsQuery query)
			{
				IExpression expression;
				if (!base.TryVisitProjectColumns(query, out expression))
				{
					return new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.SelectColumns), new IExpression[]
					{
						this.VisitQuery(query.InnerQuery),
						new ConstantExpressionSyntaxNode(ListValue.New(query.ColumnSelection.Keys.ToArray<string>()))
					});
				}
				return expression;
			}

			// Token: 0x060099D6 RID: 39382 RVA: 0x001FD3CC File Offset: 0x001FB5CC
			protected override IExpression VisitDataSource(DataSourceQuery query)
			{
				IExpression expression;
				if (query.TryGetExpression(out expression))
				{
					return expression;
				}
				return new ConstantExpressionSyntaxNode(new QueryTableValue(query));
			}

			// Token: 0x060099D7 RID: 39383 RVA: 0x001FD3F0 File Offset: 0x001FB5F0
			private IExpression VisitSelectRows(SelectRowsQuery query)
			{
				return new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.SelectRows), this.VisitQuery(query.InnerQuery), QueryToExpressionVisitor.ExpressionOrConstant(query.Condition));
			}

			// Token: 0x060099D8 RID: 39384 RVA: 0x001FD418 File Offset: 0x001FB618
			private IExpression VisitAddColumns(AddColumnsQuery query)
			{
				IExpression expression = this.FunctionToExpression("Table.AddColumn");
				IExpression expression2 = this.VisitQuery(query.InnerQuery);
				IList<FunctionValue> list = QueryToExpressionVisitor.QueryToFullExpressionVisitor.ExtractColumnConstructors(query.ColumnsConstructor.Function, QueryTableValue.NewRowType(query.InnerQuery), (QueryExpression queryExpression) => QueryExpressionAssembler.Assemble(query.InnerQuery.Columns, queryExpression));
				for (int i = 0; i < query.ColumnsConstructor.Names.Count<string>(); i++)
				{
					FunctionValue functionValue = ((list == null || list[i] == null) ? new QueryToExpressionVisitor.QueryToFullExpressionVisitor.OpaqueColumnAccessFunction(i, query.ColumnsConstructor.Function) : list[i]);
					ArrayBuilder<IExpression> arrayBuilder = default(ArrayBuilder<IExpression>);
					arrayBuilder.Add(expression2);
					arrayBuilder.Add(new ConstantExpressionSyntaxNode(TextValue.New(query.ColumnsConstructor.Names[i])));
					arrayBuilder.Add(new ConstantExpressionSyntaxNode(functionValue));
					if (query.ColumnsConstructor.Types[i].Value != null)
					{
						arrayBuilder.Add(new ConstantExpressionSyntaxNode(query.ColumnsConstructor.Types[i].Value));
					}
					expression2 = new InvocationExpressionSyntaxNodeN(expression, arrayBuilder.ToArray());
				}
				return expression2;
			}

			// Token: 0x060099D9 RID: 39385 RVA: 0x001FD570 File Offset: 0x001FB770
			private static IList<FunctionValue> ExtractColumnConstructors(FunctionValue columnGeneratorFunction, RecordTypeValue rowType, Func<QueryExpression, FunctionValue> functionAssembler)
			{
				IFunctionExpression functionExpression = columnGeneratorFunction.Expression as IFunctionExpression;
				if (functionExpression != null && functionExpression.FunctionType.Parameters.Count == 1)
				{
					IConstantExpression constantExpression = functionExpression.Expression as IConstantExpression;
					if (constantExpression != null && constantExpression.Value.IsList)
					{
						ListValue asList = constantExpression.Value.AsList;
						QueryExpression[] array = new QueryExpression[asList.Count];
						int num = 0;
						foreach (IValueReference valueReference in asList)
						{
							array[num++] = new ConstantQueryExpression(valueReference.Value);
						}
						return array.Select((QueryExpression queryExpression) => functionAssembler(queryExpression)).ToList<FunctionValue>();
					}
					IListExpression listExpression = functionExpression.Expression as IListExpression;
					if (listExpression != null)
					{
						FunctionValue[] array2 = new FunctionValue[listExpression.Members.Count];
						for (int i = 0; i < array2.Length; i++)
						{
							IFunctionExpression functionExpression2;
							if (!AddColumnsQuery.TryGetInnerFunction(listExpression.Members[i], functionExpression.FunctionType.Parameters[0].Identifier, out functionExpression2))
							{
								functionExpression2 = new FunctionExpressionSyntaxNode(functionExpression.FunctionType, listExpression.Members[i]);
							}
							QueryExpression queryExpression2;
							if (QueryExpressionBuilder.TryToQueryExpression(rowType, functionExpression2, out queryExpression2))
							{
								array2[i] = functionAssembler(queryExpression2);
							}
							else
							{
								IInvocationExpression invocationExpression = functionExpression2.Expression as IInvocationExpression;
								if (invocationExpression != null)
								{
									IConstantExpression constantExpression2 = invocationExpression.Function as IConstantExpression;
									if (constantExpression2 != null && constantExpression2.Value.IsFunction)
									{
										array2[i] = constantExpression2.Value.AsFunction;
									}
								}
							}
							FunctionValue functionValue;
							if (array2[i] == null && AddColumnsQuery.TryGetInnerFunction(listExpression.Members[i], functionExpression.FunctionType.Parameters[0].Identifier, out functionValue))
							{
								array2[i] = functionValue;
							}
						}
						return array2;
					}
				}
				return null;
			}

			// Token: 0x060099DA RID: 39386 RVA: 0x001FD778 File Offset: 0x001FB978
			private IExpression VisitSkipTake(SkipTakeQuery query)
			{
				IExpression expression = this.VisitQuery(query.InnerQuery);
				if (!query.RowRange.SkipCount.IsZero)
				{
					expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.Skip), expression, new ConstantExpressionSyntaxNode(NumberValue.New(query.RowRange.SkipCount.Value)));
				}
				if (!query.RowRange.TakeCount.IsInfinite)
				{
					expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.FirstN), expression, new ConstantExpressionSyntaxNode(NumberValue.New(query.RowRange.TakeCount.Value)));
				}
				return expression;
			}

			// Token: 0x060099DB RID: 39387 RVA: 0x001FD828 File Offset: 0x001FBA28
			private IExpression VisitSort(SortQuery query)
			{
				TableSortOrder sortOrder = query.SortOrder;
				Value[] array = null;
				QueryExpression[] array2;
				bool[] array3;
				if (!sortOrder.IsEmpty && SortQuery.TryGetSelectors(sortOrder, QueryTableValue.NewRowType(query.InnerQuery), out array2, out array3))
				{
					array = new Value[array2.Length];
					for (int i = 0; i < array.Length; i++)
					{
						Value value = (array3[i] ? Library.Order.Ascending : Library.Order.Descending);
						int num;
						Value value2;
						if (array2[i].TryGetColumnAccess(out num))
						{
							value2 = TextValue.New(query.Columns[num]);
						}
						else
						{
							value2 = QueryExpressionAssembler.Assemble(query.InnerQuery.Columns, array2[i]);
						}
						array[i] = ListValue.New(new Value[] { value2, value });
					}
				}
				ListValue listValue = ((array == null) ? ListValue.Empty : ListValue.New(array));
				return new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.Sort), this.VisitQuery(query.InnerQuery), new ConstantExpressionSyntaxNode(listValue));
			}

			// Token: 0x060099DC RID: 39388 RVA: 0x001FD918 File Offset: 0x001FBB18
			private IExpression VisitDistinct(DistinctQuery query)
			{
				RecordTypeValue recordTypeValue = QueryTableValue.NewRowType(query.InnerQuery);
				int num = query.DistinctCriteria.Distincts.Length;
				Value value;
				Value value2;
				if (num == 1 && !QueryToExpressionVisitor.QueryToFullExpressionVisitor.TryGetColumnOrSelector(recordTypeValue, query.DistinctCriteria.Distincts[0], query.Columns, out value))
				{
					value2 = QueryToExpressionVisitor.QueryToFullExpressionVisitor.ConvertComparerToFunctionValue(query.DistinctCriteria.Distincts[0].Comparer);
				}
				else
				{
					Value[] array = new Value[num];
					Value[] array2 = new Value[num];
					for (int i = 0; i < num; i++)
					{
						Distinct distinct = query.DistinctCriteria.Distincts[i];
						if (!QueryToExpressionVisitor.QueryToFullExpressionVisitor.TryGetColumnOrSelector(recordTypeValue, distinct, query.Columns, out value))
						{
							value = new QueryToExpressionVisitor.QueryToFullExpressionVisitor.OpaqueColumnAccessFunction(i, null);
						}
						array[i] = value;
						array2[i] = QueryToExpressionVisitor.QueryToFullExpressionVisitor.ConvertComparerToFunctionValue(query.DistinctCriteria.Distincts[i].Comparer);
					}
					if (array2.All((Value comparer) => comparer == Value.Null))
					{
						value2 = ListValue.New(array);
					}
					else
					{
						Value[] array3 = new Value[num];
						for (int j = 0; j < num; j++)
						{
							array3[j] = ListValue.New(new Value[]
							{
								array[j],
								array2[j]
							});
						}
						value2 = ListValue.New(array3);
					}
				}
				return new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.Distinct), this.VisitQuery(query.InnerQuery), new ConstantExpressionSyntaxNode(value2));
			}

			// Token: 0x060099DD RID: 39389 RVA: 0x001FDA90 File Offset: 0x001FBC90
			private static bool TryGetColumnOrSelector(RecordTypeValue rowType, Distinct distinct, Keys columns, out Value value)
			{
				if (distinct.Selector != null)
				{
					QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(rowType, distinct.Selector);
					int num;
					value = (queryExpression.TryGetColumnAccess(out num) ? TextValue.New(columns[num]) : distinct.Selector);
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x060099DE RID: 39390 RVA: 0x001FDADC File Offset: 0x001FBCDC
			private static Value ConvertComparerToFunctionValue(IEqualityComparer<Value> comparer)
			{
				if (comparer == null)
				{
					return Value.Null;
				}
				if (comparer == _ValueComparer.StrictDefault)
				{
					return Library.Comparer.Ordinal;
				}
				if (comparer == _ValueComparer.StrictDefaultIgnoreCase)
				{
					return Library.Comparer.OrdinalIgnoreCase;
				}
				if (comparer == _ValueComparer.LaxDefaultIgnoreCase)
				{
					return Library.Comparer.LaxOrdinalIgnoreCase;
				}
				_ValueComparer valueComparer = comparer as _ValueComparer;
				if (valueComparer != null)
				{
					return new Library.Comparer.ComparerFunctionValue(valueComparer);
				}
				return new QueryToExpressionVisitor.QueryToFullExpressionVisitor.OpaqueComparer(comparer);
			}

			// Token: 0x060099DF RID: 39391 RVA: 0x001FDB34 File Offset: 0x001FBD34
			private IExpression VisitCombine(CombineQuery query)
			{
				if (query.DisjointColumn == -1)
				{
					IExpression expression = new ConstantExpressionSyntaxNode(TableModule.Table.Combine);
					IExpression expression2 = new ListExpressionSyntaxNode(query.Queries.Select((Query q) => this.VisitQuery(q)).ToList<IExpression>());
					Value[] array = query.Columns.Select((string c) => TextValue.New(c)).ToArray<TextValue>();
					return new InvocationExpressionSyntaxNode2(expression, expression2, new ConstantExpressionSyntaxNode(ListValue.New(array)));
				}
				return new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.FromPartitions), new ConstantExpressionSyntaxNode(TextValue.New(query.Columns[query.DisjointColumn])), new ListExpressionSyntaxNode(query.Queries.Select((Query q) => this.VisitQuery(q)).ToList<IExpression>()));
			}

			// Token: 0x060099E0 RID: 39392 RVA: 0x001FDC04 File Offset: 0x001FBE04
			private IExpression VisitGroup(GroupQuery query)
			{
				List<Value> list = new List<Value>(query.Grouping.Constructors.Length);
				foreach (ColumnConstructor columnConstructor in query.Grouping.Constructors)
				{
					list.Add(ListValue.New(new Value[]
					{
						TextValue.New(columnConstructor.Name),
						columnConstructor.Function,
						columnConstructor.Type.Value
					}));
				}
				List<IExpression> list2 = new List<IExpression>();
				list2.Add(this.VisitQuery(query.InnerQuery));
				Value[] array = query.Grouping.KeyKeys.Select((string c) => TextValue.New(c)).ToArray<TextValue>();
				list2.Add(new ConstantExpressionSyntaxNode(ListValue.New(array)));
				list2.Add(new ConstantExpressionSyntaxNode(ListValue.New(list.ToArray())));
				List<IExpression> list3 = list2;
				if (query.Grouping.Adjacent)
				{
					list3.Add(new ConstantExpressionSyntaxNode(Library.GroupKind.Local));
				}
				else if (query.Grouping.Comparer != null)
				{
					list3.Add(new ConstantExpressionSyntaxNode(Library.GroupKind.Global));
				}
				if (query.Grouping.Comparer != null)
				{
					list3.Add(QueryToExpressionVisitor.ExpressionOrConstant(query.Grouping.Comparer));
				}
				return new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.Group), list3.ToArray());
			}

			// Token: 0x060099E1 RID: 39393 RVA: 0x001FDD68 File Offset: 0x001FBF68
			private IExpression VisitJoin(JoinQuery query)
			{
				Keys columns = query.LeftQuery.Columns;
				ArrayBuilder<Value> arrayBuilder = default(ArrayBuilder<Value>);
				for (int i = 0; i < query.LeftKeyColumns.Length; i++)
				{
					string text = columns[query.LeftKeyColumns[i]];
					arrayBuilder.Add(TextValue.New(text));
				}
				Keys columns2 = query.RightQuery.Columns;
				ArrayBuilder<Value> arrayBuilder2 = default(ArrayBuilder<Value>);
				for (int j = 0; j < query.RightKeyColumns.Length; j++)
				{
					string text2 = columns2[query.RightKeyColumns[j]];
					arrayBuilder2.Add(TextValue.New(text2));
				}
				List<IExpression> list = new List<IExpression>
				{
					this.VisitQuery(query.LeftQuery),
					new ConstantExpressionSyntaxNode(ListValue.New(arrayBuilder.ToArray())),
					this.VisitQuery(query.RightQuery),
					new ConstantExpressionSyntaxNode(ListValue.New(arrayBuilder2.ToArray())),
					new ConstantExpressionSyntaxNode(TableTypeAlgebra.GetValue(query.JoinKind))
				};
				NumberValue numberValue;
				if (TableModule.JoinAlgorithmEnum.Type.TryLookupEnum(query.JoinAlgorithm, out numberValue))
				{
					list.Add(new ConstantExpressionSyntaxNode(numberValue));
				}
				if (query.KeyEqualityComparers != null)
				{
					List<IExpression> list2 = list;
					Value[] array = query.KeyEqualityComparers.Select((FunctionValue keyComparer) => keyComparer).ToArray<FunctionValue>();
					list2.Add(new ConstantExpressionSyntaxNode(ListValue.New(array)));
				}
				return new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.Join), list.ToArray());
			}

			// Token: 0x060099E2 RID: 39394 RVA: 0x001FDEFC File Offset: 0x001FC0FC
			private IExpression VisitNestedJoin(NestedJoinQuery query)
			{
				ArrayBuilder<Value> arrayBuilder = default(ArrayBuilder<Value>);
				for (int i = 0; i < query.LeftKeyColumns.Length; i++)
				{
					string text = query.JoinKeys[query.LeftKeyColumns[i]];
					arrayBuilder.Add(TextValue.New(text));
				}
				ArrayBuilder<Value> arrayBuilder2 = default(ArrayBuilder<Value>);
				for (int j = 0; j < query.RightKey.Length; j++)
				{
					string text2 = query.RightKey[j];
					arrayBuilder2.Add(TextValue.New(text2));
				}
				List<IExpression> list = new List<IExpression>
				{
					this.VisitQuery(query.LeftQuery),
					new ConstantExpressionSyntaxNode(ListValue.New(arrayBuilder.ToArray())),
					new ConstantExpressionSyntaxNode(query.RightTable),
					new ConstantExpressionSyntaxNode(ListValue.New(arrayBuilder2.ToArray())),
					new ConstantExpressionSyntaxNode(TextValue.New(query.NewColumnName)),
					new ConstantExpressionSyntaxNode(TableTypeAlgebra.GetValue(query.JoinKind))
				};
				if (query.KeyEqualityComparers != null)
				{
					List<IExpression> list2 = list;
					Value[] keyEqualityComparers = query.KeyEqualityComparers;
					list2.Add(new ConstantExpressionSyntaxNode(ListValue.New(keyEqualityComparers)));
				}
				return new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.NestedJoin), list.ToArray());
			}

			// Token: 0x060099E3 RID: 39395 RVA: 0x001FE040 File Offset: 0x001FC240
			private IExpression VisitExpandListColumn(ExpandListColumnQuery query)
			{
				return new InvocationExpressionSyntaxNode2(this.FunctionToExpression("Table.ExpandListColumn"), this.VisitQuery(query.InnerQuery), new ConstantExpressionSyntaxNode(TextValue.New(query.Columns[query.ColumnIndex])));
			}

			// Token: 0x060099E4 RID: 39396 RVA: 0x001FE07C File Offset: 0x001FC27C
			private IExpression VisitExpandRecordColumn(ExpandRecordColumnQuery query)
			{
				ArrayBuilder<Value> arrayBuilder = default(ArrayBuilder<Value>);
				ArrayBuilder<Value> arrayBuilder2 = default(ArrayBuilder<Value>);
				for (int i = 0; i < query.FieldsToProject.Length; i++)
				{
					arrayBuilder2.Add(TextValue.New(query.FieldsToProject[i]));
					arrayBuilder.Add(TextValue.New(query.NewColumns[i]));
				}
				return new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.ExpandRecordColumn), new IExpression[]
				{
					this.VisitQuery(query.InnerQuery),
					new ConstantExpressionSyntaxNode(TextValue.New(query.InnerQuery.Columns[query.ColumnToExpand])),
					new ConstantExpressionSyntaxNode(ListValue.New(arrayBuilder2.ToArray())),
					new ConstantExpressionSyntaxNode(ListValue.New(arrayBuilder.ToArray()))
				});
			}

			// Token: 0x060099E5 RID: 39397 RVA: 0x001FE150 File Offset: 0x001FC350
			private IExpression VisitUnpivot(UnpivotQuery query)
			{
				return new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.Unpivot), new IExpression[]
				{
					this.VisitQuery(query.InnerQuery),
					new ConstantExpressionSyntaxNode(ListValue.New(query.PivotValues)),
					new ConstantExpressionSyntaxNode(TextValue.New(query.AttributeColumn)),
					new ConstantExpressionSyntaxNode(TextValue.New(query.ValueColumn))
				});
			}

			// Token: 0x060099E6 RID: 39398 RVA: 0x001FE1BC File Offset: 0x001FC3BC
			private IExpression VisitPivot(PivotQuery query)
			{
				FunctionValue functionValue = (query.AggregateFunction.IsNull ? Library.List.SingleOrDefault : query.AggregateFunction);
				return new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(TableModule.Table.Pivot), new IExpression[]
				{
					this.VisitQuery(query.InnerQuery),
					new ConstantExpressionSyntaxNode(ListValue.New(query.PivotValues)),
					new ConstantExpressionSyntaxNode(TextValue.New(query.AttributeColumn)),
					new ConstantExpressionSyntaxNode(TextValue.New(query.ValueColumn)),
					QueryToExpressionVisitor.ExpressionOrConstant(functionValue)
				});
			}

			// Token: 0x060099E7 RID: 39399 RVA: 0x001FE24C File Offset: 0x001FC44C
			private IExpression FunctionToExpression(string functionName)
			{
				FunctionValue functionValue;
				if (LibraryHelper.TryGetFunctionValue(functionName, out functionValue))
				{
					return new ConstantExpressionSyntaxNode(functionValue);
				}
				return new ExclusiveIdentifierExpressionSyntaxNode(Identifier.New(functionName));
			}

			// Token: 0x04005158 RID: 20824
			private const int MaxExpressionDepth = 1050;

			// Token: 0x04005159 RID: 20825
			private int currentDepth;

			// Token: 0x020017C1 RID: 6081
			public sealed class OpaqueColumnAccessFunction : NativeFunctionValue0<Value>, IOpaqueFunctionValue, IFunctionValue, IValue
			{
				// Token: 0x060099EA RID: 39402 RVA: 0x001FE27E File Offset: 0x001FC47E
				public OpaqueColumnAccessFunction(int index, FunctionValue columnAccessFunction)
					: base(TypeValue.Any)
				{
					this.Index = index;
					this.ColumnAccessFunction = columnAccessFunction;
				}

				// Token: 0x170027B1 RID: 10161
				// (get) Token: 0x060099EB RID: 39403 RVA: 0x001FE299 File Offset: 0x001FC499
				public int Index { get; }

				// Token: 0x170027B2 RID: 10162
				// (get) Token: 0x060099EC RID: 39404 RVA: 0x001FE2A1 File Offset: 0x001FC4A1
				public FunctionValue ColumnAccessFunction { get; }

				// Token: 0x060099ED RID: 39405 RVA: 0x0004F290 File Offset: 0x0004D490
				public override Value TypedInvoke()
				{
					throw ValueException.NewExpressionError<Message0>(Strings.FunctionCannotBeInvoked, this, null);
				}
			}

			// Token: 0x020017C2 RID: 6082
			public sealed class OpaqueComparer : NativeFunctionValue0<Value>, IOpaqueFunctionValue, IFunctionValue, IValue
			{
				// Token: 0x060099EE RID: 39406 RVA: 0x001FE2A9 File Offset: 0x001FC4A9
				public OpaqueComparer(IEqualityComparer<Value> comparer)
					: base(TypeValue.Any)
				{
					this.Comparer = comparer;
				}

				// Token: 0x170027B3 RID: 10163
				// (get) Token: 0x060099EF RID: 39407 RVA: 0x001FE2BD File Offset: 0x001FC4BD
				public IEqualityComparer<Value> Comparer { get; }

				// Token: 0x060099F0 RID: 39408 RVA: 0x0004F290 File Offset: 0x0004D490
				public override Value TypedInvoke()
				{
					throw ValueException.NewExpressionError<Message0>(Strings.FunctionCannotBeInvoked, this, null);
				}
			}
		}
	}
}
