using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010C2 RID: 4290
	internal sealed class ExternalQueryProcessor : QueryProcessor
	{
		// Token: 0x06007067 RID: 28775 RVA: 0x00182384 File Offset: 0x00180584
		private static IExpression ApplyAggregateGroupBy(Query originalQuery, Value input, IExpression keySelector, IExpression resultSelector)
		{
			IQueryResultTableValue queryResultTableValue;
			if (ExternalQueryProcessor.TryGetEnumerator<IQueryResultTableValue>(input, out queryResultTableValue))
			{
				IExpression expression = ExternalQueryProcessor.CreateInvokeQuery(originalQuery, TableModule.Table.Group, new IExpression[] { queryResultTableValue.SyntaxTree, keySelector, resultSelector });
				if (queryResultTableValue.Environment.IsExpressionSupported(expression, queryResultTableValue.Host))
				{
					return new ConstantExpressionSyntaxNode(new QueryResultTableValue(originalQuery, queryResultTableValue.Environment, expression, queryResultTableValue.Host));
				}
			}
			return null;
		}

		// Token: 0x06007068 RID: 28776 RVA: 0x001823EC File Offset: 0x001805EC
		private static IExpression ApplyCount(Value input)
		{
			IQueryResultTableValue queryResultTableValue;
			if (ExternalQueryProcessor.TryGetEnumerator<IQueryResultTableValue>(input, out queryResultTableValue))
			{
				return new ConstantExpressionSyntaxNode(NumberValue.New(queryResultTableValue.ValueBuilder.CreateCountOverEnumerator()));
			}
			return null;
		}

		// Token: 0x06007069 RID: 28777 RVA: 0x0018241C File Offset: 0x0018061C
		private static IExpression ApplyDistinct(Query originalQuery, Value input, IExpression equationCriteria)
		{
			IQueryResultTableValue queryResultTableValue;
			if (ExternalQueryProcessor.TryGetEnumerator<IQueryResultTableValue>(input, out queryResultTableValue))
			{
				IExpression expression = ExternalQueryProcessor.CreateInvokeQuery(originalQuery, TableModule.Table.Distinct, new IExpression[] { queryResultTableValue.SyntaxTree, equationCriteria });
				if (queryResultTableValue.Environment.IsExpressionSupported(expression, queryResultTableValue.Host))
				{
					return new ConstantExpressionSyntaxNode(new QueryResultTableValue(originalQuery, queryResultTableValue.Environment, expression, queryResultTableValue.Host));
				}
			}
			return null;
		}

		// Token: 0x0600706A RID: 28778 RVA: 0x00182480 File Offset: 0x00180680
		private static IExpression ApplyCombine(Query originalQuery, IList<IExpression> tables)
		{
			IQueryResultTableValue queryResultTableValue = null;
			IExpression[] array = new IExpression[tables.Count];
			IQueryResultTableValue[] array2 = new IQueryResultTableValue[tables.Count];
			for (int i = 0; i < tables.Count; i++)
			{
				Value value;
				if (!tables[i].TryGetConstant(out value) || !ExternalQueryProcessor.TryGetEnumerator<IQueryResultTableValue>(value, out queryResultTableValue))
				{
					queryResultTableValue = null;
					break;
				}
				array[i] = queryResultTableValue.SyntaxTree;
				array2[i] = queryResultTableValue;
			}
			if (queryResultTableValue != null)
			{
				array2 = array2.OrderBy((IQueryResultTableValue tv) => tv.Environment, ExternalQueryProcessor.EnvironmentBaseComparer.Instance).ToArray<IQueryResultTableValue>();
				EnvironmentBase environment = array2[0].Environment;
				foreach (IQueryResultTableValue queryResultTableValue2 in array2)
				{
					if (!environment.OtherCanFoldToThis(queryResultTableValue2.Environment))
					{
						return null;
					}
				}
				IExpression expression = ExternalQueryProcessor.CreateInvokeQuery(originalQuery, Library.List.Combine, new IExpression[]
				{
					new ListExpressionSyntaxNode(array)
				});
				if (environment.IsExpressionSupported(expression, queryResultTableValue.Host))
				{
					return new ConstantExpressionSyntaxNode(new QueryResultTableValue(originalQuery, environment, expression, environment.Host));
				}
			}
			return null;
		}

		// Token: 0x0600706B RID: 28779 RVA: 0x00182598 File Offset: 0x00180798
		private static IExpression ApplyFunction(Query originalQuery, QueryResultFunctionValue functionValue, IList<IExpression> arguments)
		{
			IExpression expression = ExternalQueryProcessor.CreateInvokeQuery(originalQuery, functionValue, arguments.ToArray<IExpression>());
			if (functionValue.Environment.IsExpressionSupported(expression, functionValue.Host))
			{
				Value value;
				if (functionValue.Type.AsFunctionType.ReturnType.TypeKind == ValueKind.Table)
				{
					value = new QueryResultTableValue(originalQuery, functionValue.Environment, expression, functionValue.Host);
				}
				else
				{
					ValueBuilderBase valueBuilderBase = functionValue.Environment.Compile(originalQuery, expression);
					if (functionValue.Type.AsFunctionType.ReturnType.TypeKind == ValueKind.List)
					{
						value = ListValue.New(valueBuilderBase.GetValues());
					}
					else
					{
						value = valueBuilderBase.GetSingleValue();
					}
				}
				return new ConstantExpressionSyntaxNode(value);
			}
			return null;
		}

		// Token: 0x0600706C RID: 28780 RVA: 0x0018263C File Offset: 0x0018083C
		private static IExpression ApplyForceColumns(Query originalQuery, Value input, IExpression arguments)
		{
			IQueryResultTableValue queryResultTableValue;
			if (ExternalQueryProcessor.TryGetEnumerator<IQueryResultTableValue>(input, out queryResultTableValue))
			{
				IExpression expression = ExternalQueryProcessor.CreateInvokeQuery(originalQuery, TableModule.Table.ForceColumns, new IExpression[] { queryResultTableValue.SyntaxTree, arguments });
				if (queryResultTableValue.Environment.IsExpressionSupported(expression, queryResultTableValue.Host))
				{
					return new ConstantExpressionSyntaxNode(new QueryResultTableValue(originalQuery, queryResultTableValue.Environment, expression, queryResultTableValue.Host));
				}
			}
			return null;
		}

		// Token: 0x0600706D RID: 28781 RVA: 0x001826A0 File Offset: 0x001808A0
		private static IExpression ApplyJoin(Query originalQuery, Value joinFunction, IList<IExpression> arguments)
		{
			if (arguments[0].Kind != ExpressionKind.Constant)
			{
				return null;
			}
			if (arguments[2].Kind != ExpressionKind.Constant)
			{
				return null;
			}
			Value value = ((IConstantExpression)arguments[0]).Value;
			Value value2 = ((IConstantExpression)arguments[2]).Value;
			IQueryResultTableValue queryResultTableValue = null;
			IQueryResultTableValue queryResultTableValue2;
			if (ExternalQueryProcessor.TryGetEnumerator<IQueryResultTableValue>(value, out queryResultTableValue2) && ExternalQueryProcessor.TryGetEnumerator<IQueryResultTableValue>(value2, out queryResultTableValue))
			{
				IExpression[] array = new IExpression[arguments.Count];
				arguments.CopyTo(array, 0);
				array[0] = queryResultTableValue2.SyntaxTree;
				array[2] = queryResultTableValue.SyntaxTree;
				IExpression expression = ExternalQueryProcessor.CreateInvokeQuery(originalQuery, joinFunction, array);
				EnvironmentBase environmentBase = (queryResultTableValue2.Environment.OtherCanFoldToThis(queryResultTableValue.Environment) ? queryResultTableValue2.Environment : queryResultTableValue.Environment);
				if (environmentBase.IsExpressionSupported(expression, environmentBase.Host))
				{
					return new ConstantExpressionSyntaxNode(new QueryResultTableValue(originalQuery, environmentBase, expression, environmentBase.Host));
				}
			}
			if (queryResultTableValue2 != null || queryResultTableValue != null)
			{
				IQueryResultTableValue queryResultTableValue3 = queryResultTableValue2 ?? queryResultTableValue;
				if (queryResultTableValue3.Host.QueryService<IFoldingFailureService>().ThrowOnFoldingFailure)
				{
					FoldingTracingService foldingTracingService = queryResultTableValue3.Environment.FoldingTracingService;
					using (foldingTracingService.NewScope("ExternalQueryProcessor/ApplyJoin"))
					{
						foldingTracingService.Trace<FoldingWarnings.FoldingWarning<string, string>>(FoldingWarnings.HeterogeneousJoin(((queryResultTableValue2 != null) ? queryResultTableValue2.Environment.Resource.Path : null) ?? "Unknown", ((queryResultTableValue != null) ? queryResultTableValue.Environment.Resource.Path : null) ?? "Unknown"));
						throw foldingTracingService.NewValueException();
					}
				}
			}
			return null;
		}

		// Token: 0x0600706E RID: 28782 RVA: 0x00182838 File Offset: 0x00180A38
		private static IExpression ApplyPivot(Query originalQuery, Value input, IExpression pivotValues, IExpression attributeColumn, IExpression valueColumn, IExpression aggregateFunction)
		{
			IQueryResultTableValue queryResultTableValue;
			if (ExternalQueryProcessor.TryGetEnumerator<IQueryResultTableValue>(input, out queryResultTableValue))
			{
				IExpression expression = ExternalQueryProcessor.CreateInvokeQuery(originalQuery, TableModule.Table.Pivot, new IExpression[] { queryResultTableValue.SyntaxTree, pivotValues, attributeColumn, valueColumn, aggregateFunction });
				if (queryResultTableValue.Environment.IsExpressionSupported(expression, queryResultTableValue.Host))
				{
					return new ConstantExpressionSyntaxNode(new QueryResultTableValue(originalQuery, queryResultTableValue.Environment, expression, queryResultTableValue.Host));
				}
			}
			return null;
		}

		// Token: 0x0600706F RID: 28783 RVA: 0x001828AC File Offset: 0x00180AAC
		private static IExpression ApplySelect(Query originalQuery, Value input, IExpression predicate)
		{
			IQueryResultTableValue queryResultTableValue;
			if (ExternalQueryProcessor.TryGetEnumerator<IQueryResultTableValue>(input, out queryResultTableValue))
			{
				IExpression expression = ExternalQueryProcessor.CreateInvokeQuery(originalQuery, TableModule.Table.SelectRows, new IExpression[] { queryResultTableValue.SyntaxTree, predicate });
				if (queryResultTableValue.Environment.IsExpressionSupported(expression, queryResultTableValue.Host))
				{
					return new ConstantExpressionSyntaxNode(new QueryResultTableValue(originalQuery, queryResultTableValue.Environment, expression, queryResultTableValue.Host));
				}
			}
			return null;
		}

		// Token: 0x06007070 RID: 28784 RVA: 0x00182910 File Offset: 0x00180B10
		private static IExpression ApplySkip(Query originalQuery, Value input, IExpression skipQuery)
		{
			IQueryResultTableValue queryResultTableValue;
			if (ExternalQueryProcessor.TryGetEnumerator<IQueryResultTableValue>(input, out queryResultTableValue))
			{
				if (!queryResultTableValue.Environment.SupportsSkip(queryResultTableValue.Type.AsTableType))
				{
					return null;
				}
				ValueBuilderBase valueBuilderBase;
				if (queryResultTableValue.ValueBuilder.TryApplySkip(originalQuery, LanguageLibrary.Evaluate(skipQuery), out valueBuilderBase))
				{
					return new ConstantExpressionSyntaxNode(new ExternalQueryProcessor.PagingEnumerator(queryResultTableValue.SyntaxTree, queryResultTableValue.Environment, valueBuilderBase, queryResultTableValue.Host));
				}
			}
			return null;
		}

		// Token: 0x06007071 RID: 28785 RVA: 0x00182978 File Offset: 0x00180B78
		private static IExpression ApplySort(Query originalQuery, Value input, IExpression sortCriteria)
		{
			IQueryResultTableValue queryResultTableValue;
			if (ExternalQueryProcessor.TryGetEnumerator<IQueryResultTableValue>(input, out queryResultTableValue))
			{
				IExpression expression = ExternalQueryProcessor.CreateInvokeQuery(originalQuery, TableModule.Table.Sort, new IExpression[] { queryResultTableValue.SyntaxTree, sortCriteria });
				if (queryResultTableValue.Environment.IsExpressionSupported(expression, queryResultTableValue.Host))
				{
					return new ConstantExpressionSyntaxNode(new QueryResultTableValue(originalQuery, queryResultTableValue.Environment, expression, queryResultTableValue.Host));
				}
			}
			return null;
		}

		// Token: 0x06007072 RID: 28786 RVA: 0x001829DC File Offset: 0x00180BDC
		private static IExpression ApplyTake(Query originalQuery, Value input, IExpression takeQuery)
		{
			IQueryResultTableValue queryResultTableValue;
			if (ExternalQueryProcessor.TryGetEnumerator<IQueryResultTableValue>(input, out queryResultTableValue))
			{
				if (!queryResultTableValue.Environment.SupportsTake(queryResultTableValue.Type.AsTableType))
				{
					return null;
				}
				ValueBuilderBase valueBuilderBase;
				if (queryResultTableValue.ValueBuilder.TryApplyTake(originalQuery, LanguageLibrary.Evaluate(takeQuery), out valueBuilderBase))
				{
					return new ConstantExpressionSyntaxNode(new ExternalQueryProcessor.PagingEnumerator(queryResultTableValue.SyntaxTree, queryResultTableValue.Environment, valueBuilderBase, queryResultTableValue.Host));
				}
			}
			return null;
		}

		// Token: 0x06007073 RID: 28787 RVA: 0x00182A44 File Offset: 0x00180C44
		private static IExpression ApplyTransform(Query originalQuery, Value input, IExpression constructor)
		{
			IQueryResultTableValue queryResultTableValue;
			if (ExternalQueryProcessor.TryGetEnumerator<IQueryResultTableValue>(input, out queryResultTableValue))
			{
				IExpression expression = ExternalQueryProcessor.CreateInvokeQuery(originalQuery, Library.ListRuntime.Transform, new IExpression[] { queryResultTableValue.SyntaxTree, constructor });
				if (queryResultTableValue.Environment.IsExpressionSupported(expression, queryResultTableValue.Host))
				{
					return new ConstantExpressionSyntaxNode(new QueryResultTableValue(originalQuery, queryResultTableValue.Environment, expression, queryResultTableValue.Host));
				}
			}
			return null;
		}

		// Token: 0x06007074 RID: 28788 RVA: 0x00182AA8 File Offset: 0x00180CA8
		private static IExpression ApplyUnpivot(Query originalQuery, Value input, IExpression pivotValues, IExpression attributeColumn, IExpression valueColumn)
		{
			IQueryResultTableValue queryResultTableValue;
			if (ExternalQueryProcessor.TryGetEnumerator<IQueryResultTableValue>(input, out queryResultTableValue))
			{
				IExpression expression = ExternalQueryProcessor.CreateInvokeQuery(originalQuery, TableModule.Table.Unpivot, new IExpression[] { queryResultTableValue.SyntaxTree, pivotValues, attributeColumn, valueColumn });
				if (queryResultTableValue.Environment.IsExpressionSupported(expression, queryResultTableValue.Host))
				{
					return new ConstantExpressionSyntaxNode(new QueryResultTableValue(originalQuery, queryResultTableValue.Environment, expression, queryResultTableValue.Host));
				}
			}
			return null;
		}

		// Token: 0x06007075 RID: 28789 RVA: 0x00182B18 File Offset: 0x00180D18
		private static IExpression ApplyInvocation(Query originalQuery, IInvocationExpression invocation)
		{
			IExpression expression = invocation.Function.Simplify();
			if (expression.Kind != ExpressionKind.Constant)
			{
				return null;
			}
			Value value = ((IConstantExpression)expression).Value;
			IList<IExpression> list = invocation.Arguments.Select((IExpression a) => a.Simplify()).ToList<IExpression>();
			QueryResultFunctionValue queryResultFunctionValue = value as QueryResultFunctionValue;
			if (queryResultFunctionValue != null)
			{
				return ExternalQueryProcessor.ApplyFunction(originalQuery, queryResultFunctionValue, list);
			}
			if (value.Equals(TableModule.Table.Join) && list.Count >= 4 && list.Count <= 7)
			{
				return ExternalQueryProcessor.ApplyJoin(originalQuery, value, list);
			}
			if (value.Equals(Library.List.Combine) && list.Count == 1)
			{
				IListExpression listExpression = list[0] as IListExpression;
				if (listExpression != null)
				{
					return ExternalQueryProcessor.ApplyCombine(originalQuery, listExpression.Members);
				}
			}
			if (list[0].Kind != ExpressionKind.Constant)
			{
				return null;
			}
			Value value2 = ((IConstantExpression)list[0]).Value;
			if (value.Equals(TableModule.Table.SelectRows))
			{
				return ExternalQueryProcessor.ApplySelect(originalQuery, value2, list[1]);
			}
			if (value.Equals(Library.ListRuntime.Transform))
			{
				return ExternalQueryProcessor.ApplyTransform(originalQuery, value2, list[1]);
			}
			if (value.Equals(TableModule.Table.RowCount))
			{
				return ExternalQueryProcessor.ApplyCount(value2);
			}
			if (value.Equals(TableModule.Table.Skip))
			{
				return ExternalQueryProcessor.ApplySkip(originalQuery, value2, list[1]);
			}
			if (value.Equals(TableModule.Table.FirstN))
			{
				return ExternalQueryProcessor.ApplyTake(originalQuery, value2, list[1]);
			}
			if (value.Equals(TableModule.Table.Group))
			{
				return ExternalQueryProcessor.ApplyAggregateGroupBy(originalQuery, value2, list[1], list[2]);
			}
			if (value.Equals(TableModule.Table.Sort))
			{
				return ExternalQueryProcessor.ApplySort(originalQuery, value2, list[1]);
			}
			if (value.Equals(TableModule.Table.Distinct))
			{
				return ExternalQueryProcessor.ApplyDistinct(originalQuery, value2, list[1]);
			}
			if (value.Equals(TableModule.Table.ForceColumns))
			{
				return ExternalQueryProcessor.ApplyForceColumns(originalQuery, value2, list[1]);
			}
			if (value.Equals(TableModule.Table.Pivot))
			{
				return ExternalQueryProcessor.ApplyPivot(originalQuery, value2, list[1], list[2], list[3], list[4]);
			}
			if (value.Equals(TableModule.Table.Unpivot))
			{
				return ExternalQueryProcessor.ApplyUnpivot(originalQuery, value2, list[1], list[2], list[3]);
			}
			return null;
		}

		// Token: 0x06007076 RID: 28790 RVA: 0x00182D70 File Offset: 0x00180F70
		private static IExpression ApplyFieldAccess(Query originalQuery, Value input, Identifier memberName, bool optional)
		{
			IQueryResultTableValue queryResultTableValue;
			if (ExternalQueryProcessor.TryGetEnumerator<IQueryResultTableValue>(input, out queryResultTableValue))
			{
				IExpression expression;
				if (optional)
				{
					expression = new OptionalFieldAccessExpressionSyntaxNode(queryResultTableValue.SyntaxTree, memberName);
				}
				else
				{
					expression = new RequiredFieldAccessExpressionSyntaxNode(queryResultTableValue.SyntaxTree, memberName);
				}
				if (queryResultTableValue.Environment.IsExpressionSupported(expression, queryResultTableValue.Host))
				{
					return new ConstantExpressionSyntaxNode(new QueryResultTableValue(originalQuery, queryResultTableValue.Environment, expression, queryResultTableValue.Host));
				}
			}
			return null;
		}

		// Token: 0x06007077 RID: 28791 RVA: 0x00182DD4 File Offset: 0x00180FD4
		private static IExpression ApplyMultiFieldRecordProjection(Query originalQuery, Value input, IList<Identifier> memberNames, bool optional)
		{
			IQueryResultTableValue queryResultTableValue;
			if (ExternalQueryProcessor.TryGetEnumerator<IQueryResultTableValue>(input, out queryResultTableValue))
			{
				IExpression expression;
				if (optional)
				{
					expression = new OptionalMultiFieldRecordProjectionExpressionSyntaxNode(queryResultTableValue.SyntaxTree, memberNames);
				}
				else
				{
					expression = new RequiredMultiFieldRecordProjectionExpressionSyntaxNode(queryResultTableValue.SyntaxTree, memberNames);
				}
				if (queryResultTableValue.Environment.IsExpressionSupported(expression, queryResultTableValue.Host))
				{
					return new ConstantExpressionSyntaxNode(new QueryResultTableValue(originalQuery, queryResultTableValue.Environment, expression, queryResultTableValue.Host));
				}
			}
			return null;
		}

		// Token: 0x06007078 RID: 28792 RVA: 0x00182E38 File Offset: 0x00181038
		public static IExpression CreateInvokeQuery(Query originalQuery, Value function, params IExpression[] args)
		{
			return new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(function), args);
		}

		// Token: 0x06007079 RID: 28793 RVA: 0x00182E48 File Offset: 0x00181048
		public override IExpression Invoke(Query originalQuery, IExpression expression)
		{
			ExpressionKind kind = expression.Kind;
			if (kind != ExpressionKind.FieldAccess)
			{
				if (kind == ExpressionKind.Invocation)
				{
					IInvocationExpression invocationExpression = (IInvocationExpression)expression;
					return ExternalQueryProcessor.ApplyInvocation(originalQuery, invocationExpression);
				}
				if (kind == ExpressionKind.MultiFieldRecordProjection)
				{
					IMultiFieldRecordProjectionExpression multiFieldRecordProjectionExpression = (IMultiFieldRecordProjectionExpression)expression;
					Value value;
					if (multiFieldRecordProjectionExpression.Expression.Simplify().TryGetConstant(out value))
					{
						return ExternalQueryProcessor.ApplyMultiFieldRecordProjection(originalQuery, value, multiFieldRecordProjectionExpression.MemberNames, multiFieldRecordProjectionExpression.IsOptional);
					}
				}
			}
			else
			{
				IFieldAccessExpression fieldAccessExpression = (IFieldAccessExpression)expression;
				Value value2;
				if (fieldAccessExpression.Expression.Simplify().TryGetConstant(out value2))
				{
					return ExternalQueryProcessor.ApplyFieldAccess(originalQuery, value2, fieldAccessExpression.MemberName, fieldAccessExpression.IsOptional);
				}
			}
			return null;
		}

		// Token: 0x0600707A RID: 28794 RVA: 0x00182EDC File Offset: 0x001810DC
		private static bool TryGetEnumerator<T>(Value input, out T enumerator) where T : class
		{
			enumerator = input as T;
			return enumerator != null;
		}

		// Token: 0x020010C3 RID: 4291
		private sealed class PagingEnumerator : TableValue
		{
			// Token: 0x0600707C RID: 28796 RVA: 0x00182F05 File Offset: 0x00181105
			public PagingEnumerator(IExpression syntaxTree, EnvironmentBase environment, ValueBuilderBase pagingValueBuilder, IEngineHost host)
			{
				this.syntaxTree = syntaxTree;
				this.environment = environment;
				this.host = host;
				this.pagingValueBuilder = pagingValueBuilder;
			}

			// Token: 0x0600707D RID: 28797 RVA: 0x00182F2A File Offset: 0x0018112A
			public override bool TryGetProcessor(out QueryProcessor processor)
			{
				processor = ExternalQueryProcessor.PagingEnumerator.pagingQueryProcessor;
				return true;
			}

			// Token: 0x17001F9D RID: 8093
			// (get) Token: 0x0600707E RID: 28798 RVA: 0x00182F34 File Offset: 0x00181134
			public override TypeValue Type
			{
				get
				{
					return this.pagingValueBuilder.Type;
				}
			}

			// Token: 0x17001F9E RID: 8094
			// (get) Token: 0x0600707F RID: 28799 RVA: 0x00182F41 File Offset: 0x00181141
			public override long LargeCount
			{
				get
				{
					return this.pagingValueBuilder.CreateCountOverEnumerator();
				}
			}

			// Token: 0x17001F9F RID: 8095
			// (get) Token: 0x06007080 RID: 28800 RVA: 0x00182F4E File Offset: 0x0018114E
			public override IQueryDomain QueryDomain
			{
				get
				{
					return ExternalQueryProcessor.PagingEnumerator.PagingQueryDomain.Instance;
				}
			}

			// Token: 0x06007081 RID: 28801 RVA: 0x00182F55 File Offset: 0x00181155
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				this.environment.ReportFoldingFailure();
				return this.pagingValueBuilder.GetEnumerator();
			}

			// Token: 0x06007082 RID: 28802 RVA: 0x00182F70 File Offset: 0x00181170
			public override IPageReader GetReader()
			{
				IPageReader pageReader;
				if (this.pagingValueBuilder.TryGetReader(out pageReader))
				{
					return pageReader;
				}
				return base.GetReader();
			}

			// Token: 0x04003E12 RID: 15890
			private static readonly ExternalQueryProcessor.PagingQueryProcessor pagingQueryProcessor = new ExternalQueryProcessor.PagingQueryProcessor();

			// Token: 0x04003E13 RID: 15891
			internal readonly IExpression syntaxTree;

			// Token: 0x04003E14 RID: 15892
			internal readonly EnvironmentBase environment;

			// Token: 0x04003E15 RID: 15893
			internal readonly IEngineHost host;

			// Token: 0x04003E16 RID: 15894
			internal readonly ValueBuilderBase pagingValueBuilder;

			// Token: 0x020010C4 RID: 4292
			private class PagingQueryDomain : INativeQueryDomain, IQueryDomain
			{
				// Token: 0x06007084 RID: 28804 RVA: 0x00182FA0 File Offset: 0x001811A0
				public bool IsCompatibleWith(IQueryDomain domain)
				{
					return domain == ExternalQueryProcessor.PagingEnumerator.PagingQueryDomain.Instance;
				}

				// Token: 0x17001FA0 RID: 8096
				// (get) Token: 0x06007085 RID: 28805 RVA: 0x00002105 File Offset: 0x00000305
				public bool CanIndex
				{
					get
					{
						return false;
					}
				}

				// Token: 0x06007086 RID: 28806 RVA: 0x0000A6A5 File Offset: 0x000088A5
				public Query Optimize(Query query)
				{
					return query;
				}

				// Token: 0x06007087 RID: 28807 RVA: 0x00182FAC File Offset: 0x001811AC
				public bool TryGetNativeQuery(Query query, out IResource resource, out Value nativeQuery, out RecordValue options)
				{
					TableQuery tableQuery = query as TableQuery;
					if (tableQuery != null)
					{
						ExternalQueryProcessor.PagingEnumerator pagingEnumerator = tableQuery.Table as ExternalQueryProcessor.PagingEnumerator;
						if (pagingEnumerator != null)
						{
							DbValueBuilder dbValueBuilder = pagingEnumerator.pagingValueBuilder as DbValueBuilder;
							if (dbValueBuilder != null)
							{
								resource = dbValueBuilder.DbEnvironment.Resource;
								nativeQuery = TextValue.New(dbValueBuilder.CompleteDbQueryPlan.ExternalQuery);
								options = dbValueBuilder.DbEnvironment.OptionsRecord;
								return true;
							}
						}
					}
					resource = null;
					nativeQuery = null;
					options = RecordValue.Empty;
					return false;
				}

				// Token: 0x04003E17 RID: 15895
				public static readonly INativeQueryDomain Instance = new ExternalQueryProcessor.PagingEnumerator.PagingQueryDomain();
			}
		}

		// Token: 0x020010C5 RID: 4293
		private sealed class PagingQueryProcessor : QueryProcessor
		{
			// Token: 0x0600708A RID: 28810 RVA: 0x0018302C File Offset: 0x0018122C
			private static IExpression ApplyCount(Value input)
			{
				ExternalQueryProcessor.PagingEnumerator pagingEnumerator;
				if (ExternalQueryProcessor.TryGetEnumerator<ExternalQueryProcessor.PagingEnumerator>(input, out pagingEnumerator))
				{
					return new ConstantExpressionSyntaxNode(NumberValue.New(pagingEnumerator.pagingValueBuilder.CreateCountOverEnumerator()));
				}
				return null;
			}

			// Token: 0x0600708B RID: 28811 RVA: 0x0018305C File Offset: 0x0018125C
			private static IExpression ApplySkip(Query originalQuery, Value input, IExpression skipQuery)
			{
				ExternalQueryProcessor.PagingEnumerator pagingEnumerator;
				if (ExternalQueryProcessor.TryGetEnumerator<ExternalQueryProcessor.PagingEnumerator>(input, out pagingEnumerator))
				{
					if (!pagingEnumerator.environment.SupportsSkip(pagingEnumerator.Type.AsTableType))
					{
						return null;
					}
					ValueBuilderBase valueBuilderBase;
					if (pagingEnumerator.pagingValueBuilder.TryApplySkip(originalQuery, LanguageLibrary.Evaluate(skipQuery), out valueBuilderBase))
					{
						return new ConstantExpressionSyntaxNode(new ExternalQueryProcessor.PagingEnumerator(pagingEnumerator.syntaxTree, pagingEnumerator.environment, valueBuilderBase, pagingEnumerator.host));
					}
				}
				return null;
			}

			// Token: 0x0600708C RID: 28812 RVA: 0x001830C4 File Offset: 0x001812C4
			private static IExpression ApplyTake(Query originalQuery, Value input, IExpression takeQuery)
			{
				ExternalQueryProcessor.PagingEnumerator pagingEnumerator;
				if (ExternalQueryProcessor.TryGetEnumerator<ExternalQueryProcessor.PagingEnumerator>(input, out pagingEnumerator))
				{
					if (!pagingEnumerator.environment.SupportsTake(pagingEnumerator.Type.AsTableType))
					{
						return null;
					}
					ValueBuilderBase valueBuilderBase;
					if (pagingEnumerator.pagingValueBuilder.TryApplyTake(originalQuery, LanguageLibrary.Evaluate(takeQuery), out valueBuilderBase))
					{
						return new ConstantExpressionSyntaxNode(new ExternalQueryProcessor.PagingEnumerator(pagingEnumerator.syntaxTree, pagingEnumerator.environment, valueBuilderBase, pagingEnumerator.host));
					}
				}
				return null;
			}

			// Token: 0x0600708D RID: 28813 RVA: 0x0018312C File Offset: 0x0018132C
			private static IExpression ApplyTransform(Query originalQuery, Value input, IExpression constructor)
			{
				ExternalQueryProcessor.PagingEnumerator pagingEnumerator;
				if (ExternalQueryProcessor.TryGetEnumerator<ExternalQueryProcessor.PagingEnumerator>(input, out pagingEnumerator))
				{
					IExpression expression = ExternalQueryProcessor.CreateInvokeQuery(originalQuery, Library.ListRuntime.Transform, new IExpression[] { pagingEnumerator.syntaxTree, constructor });
					if (pagingEnumerator.environment.IsExpressionSupported(expression, pagingEnumerator.host))
					{
						IExpression expression2 = new ConstantExpressionSyntaxNode(new QueryResultTableValue(originalQuery, pagingEnumerator.environment, expression, pagingEnumerator.host));
						if (pagingEnumerator.pagingValueBuilder.Skip > 0)
						{
							IExpression expression3 = ExternalQueryProcessor.CreateInvokeQuery(originalQuery, TableModule.Table.Skip, new IExpression[]
							{
								expression2,
								new ConstantExpressionSyntaxNode(NumberValue.New(pagingEnumerator.pagingValueBuilder.Skip))
							});
							expression2 = ExternalQueryProcessor.PagingQueryProcessor.Fold(originalQuery, expression2, expression3);
						}
						if (pagingEnumerator.pagingValueBuilder.Take != null)
						{
							IExpression expression4 = ExternalQueryProcessor.CreateInvokeQuery(originalQuery, TableModule.Table.FirstN, new IExpression[]
							{
								expression2,
								new ConstantExpressionSyntaxNode(NumberValue.New(pagingEnumerator.pagingValueBuilder.Take.Value))
							});
							expression2 = ExternalQueryProcessor.PagingQueryProcessor.Fold(originalQuery, expression2, expression4);
						}
						return expression2;
					}
				}
				return null;
			}

			// Token: 0x0600708E RID: 28814 RVA: 0x00183238 File Offset: 0x00181438
			private static IExpression Fold(Query originalQuery, IExpression source, IExpression invoke)
			{
				IConstantExpression constantExpression = source as IConstantExpression;
				QueryProcessor queryProcessor;
				if (constantExpression != null && constantExpression.Value.IsTable && constantExpression.Value.AsTable.TryGetProcessor(out queryProcessor))
				{
					return queryProcessor.Invoke(originalQuery, invoke);
				}
				return null;
			}

			// Token: 0x0600708F RID: 28815 RVA: 0x0018327C File Offset: 0x0018147C
			public override IExpression Invoke(Query originalQuery, IExpression expression)
			{
				IInvocationExpression invocationExpression = expression as IInvocationExpression;
				if (invocationExpression == null)
				{
					return null;
				}
				Value value;
				if (!invocationExpression.Function.Simplify().TryGetConstant(out value))
				{
					return null;
				}
				IList<IExpression> arguments = invocationExpression.Arguments;
				Value value2;
				if (!arguments[0].Simplify().TryGetConstant(out value2))
				{
					return null;
				}
				if (value.Equals(TableModule.Table.RowCount))
				{
					return ExternalQueryProcessor.PagingQueryProcessor.ApplyCount(value2);
				}
				if (value.Equals(TableModule.Table.Skip))
				{
					return ExternalQueryProcessor.PagingQueryProcessor.ApplySkip(originalQuery, value2, arguments[1]);
				}
				if (value.Equals(TableModule.Table.FirstN))
				{
					return ExternalQueryProcessor.PagingQueryProcessor.ApplyTake(originalQuery, value2, arguments[1]);
				}
				if (value.Equals(Library.ListRuntime.Transform))
				{
					return ExternalQueryProcessor.PagingQueryProcessor.ApplyTransform(originalQuery, value2, arguments[1]);
				}
				return null;
			}
		}

		// Token: 0x020010C6 RID: 4294
		private sealed class EnvironmentBaseComparer : IComparer<EnvironmentBase>
		{
			// Token: 0x06007091 RID: 28817 RVA: 0x000020FD File Offset: 0x000002FD
			private EnvironmentBaseComparer()
			{
			}

			// Token: 0x06007092 RID: 28818 RVA: 0x00183334 File Offset: 0x00181534
			public int Compare(EnvironmentBase x, EnvironmentBase y)
			{
				bool flag = x.OtherCanFoldToThis(y);
				bool flag2 = y.OtherCanFoldToThis(x);
				if (flag && flag2)
				{
					return 0;
				}
				if (!flag)
				{
					return 1;
				}
				return -1;
			}

			// Token: 0x04003E18 RID: 15896
			public static readonly IComparer<EnvironmentBase> Instance = new ExternalQueryProcessor.EnvironmentBaseComparer();
		}
	}
}
