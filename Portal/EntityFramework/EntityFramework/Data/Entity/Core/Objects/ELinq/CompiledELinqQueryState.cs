using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.QueryCache;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.Internal;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x0200045D RID: 1117
	internal sealed class CompiledELinqQueryState : ELinqQueryState
	{
		// Token: 0x060036C3 RID: 14019 RVA: 0x000B0C4C File Offset: 0x000AEE4C
		internal CompiledELinqQueryState(Type elementType, ObjectContext context, LambdaExpression lambda, Guid cacheToken, object[] parameterValues, ObjectQueryExecutionPlanFactory objectQueryExecutionPlanFactory = null)
			: base(elementType, context, lambda, null)
		{
			this._cacheToken = cacheToken;
			this._parameterValues = parameterValues;
			base.EnsureParameters();
			base.Parameters.SetReadOnly(true);
			this._objectQueryExecutionPlanFactory = objectQueryExecutionPlanFactory ?? new ObjectQueryExecutionPlanFactory(null);
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x000B0C98 File Offset: 0x000AEE98
		internal override ObjectQueryExecutionPlan GetExecutionPlan(MergeOption? forMergeOption)
		{
			ObjectQueryExecutionPlan objectQueryExecutionPlan = null;
			CompiledQueryCacheEntry compiledQueryCacheEntry = this._cacheEntry;
			bool useCSharpNullComparisonBehavior = base.ObjectContext.ContextOptions.UseCSharpNullComparisonBehavior;
			bool disableFilterOverProjectionSimplificationForCustomFunctions = base.ObjectContext.ContextOptions.DisableFilterOverProjectionSimplificationForCustomFunctions;
			if (compiledQueryCacheEntry != null)
			{
				MergeOption mergeOption = ObjectQueryState.EnsureMergeOption(new MergeOption?[] { forMergeOption, base.UserSpecifiedMergeOption, compiledQueryCacheEntry.PropagatedMergeOption });
				objectQueryExecutionPlan = compiledQueryCacheEntry.GetExecutionPlan(mergeOption, useCSharpNullComparisonBehavior);
				if (objectQueryExecutionPlan == null)
				{
					ExpressionConverter expressionConverter = this.CreateExpressionConverter();
					DbExpression dbExpression = expressionConverter.Convert();
					IEnumerable<Tuple<ObjectParameter, QueryParameterExpression>> parameters = expressionConverter.GetParameters();
					DbQueryCommandTree dbQueryCommandTree = DbQueryCommandTree.FromValidExpression(base.ObjectContext.MetadataWorkspace, DataSpace.CSpace, dbExpression, !useCSharpNullComparisonBehavior, disableFilterOverProjectionSimplificationForCustomFunctions);
					objectQueryExecutionPlan = this._objectQueryExecutionPlanFactory.Prepare(base.ObjectContext, dbQueryCommandTree, base.ElementType, mergeOption, base.EffectiveStreamingBehavior, expressionConverter.PropagatedSpan, parameters, expressionConverter.AliasGenerator);
					objectQueryExecutionPlan = compiledQueryCacheEntry.SetExecutionPlan(objectQueryExecutionPlan, useCSharpNullComparisonBehavior);
				}
			}
			else
			{
				QueryCacheManager queryCacheManager = base.ObjectContext.MetadataWorkspace.GetQueryCacheManager();
				CompiledQueryCacheKey compiledQueryCacheKey = new CompiledQueryCacheKey(this._cacheToken);
				if (queryCacheManager.TryCacheLookup<CompiledQueryCacheKey, CompiledQueryCacheEntry>(compiledQueryCacheKey, out compiledQueryCacheEntry))
				{
					this._cacheEntry = compiledQueryCacheEntry;
					MergeOption mergeOption2 = ObjectQueryState.EnsureMergeOption(new MergeOption?[] { forMergeOption, base.UserSpecifiedMergeOption, compiledQueryCacheEntry.PropagatedMergeOption });
					objectQueryExecutionPlan = compiledQueryCacheEntry.GetExecutionPlan(mergeOption2, useCSharpNullComparisonBehavior);
				}
				if (objectQueryExecutionPlan == null)
				{
					ExpressionConverter expressionConverter2 = this.CreateExpressionConverter();
					DbExpression dbExpression2 = expressionConverter2.Convert();
					IEnumerable<Tuple<ObjectParameter, QueryParameterExpression>> parameters2 = expressionConverter2.GetParameters();
					DbQueryCommandTree dbQueryCommandTree2 = DbQueryCommandTree.FromValidExpression(base.ObjectContext.MetadataWorkspace, DataSpace.CSpace, dbExpression2, !useCSharpNullComparisonBehavior, disableFilterOverProjectionSimplificationForCustomFunctions);
					if (compiledQueryCacheEntry == null)
					{
						compiledQueryCacheEntry = new CompiledQueryCacheEntry(compiledQueryCacheKey, expressionConverter2.PropagatedMergeOption);
						QueryCacheEntry queryCacheEntry;
						if (queryCacheManager.TryLookupAndAdd(compiledQueryCacheEntry, out queryCacheEntry))
						{
							compiledQueryCacheEntry = (CompiledQueryCacheEntry)queryCacheEntry;
						}
						this._cacheEntry = compiledQueryCacheEntry;
					}
					MergeOption mergeOption3 = ObjectQueryState.EnsureMergeOption(new MergeOption?[] { forMergeOption, base.UserSpecifiedMergeOption, compiledQueryCacheEntry.PropagatedMergeOption });
					objectQueryExecutionPlan = compiledQueryCacheEntry.GetExecutionPlan(mergeOption3, useCSharpNullComparisonBehavior);
					if (objectQueryExecutionPlan == null)
					{
						objectQueryExecutionPlan = this._objectQueryExecutionPlanFactory.Prepare(base.ObjectContext, dbQueryCommandTree2, base.ElementType, mergeOption3, base.EffectiveStreamingBehavior, expressionConverter2.PropagatedSpan, parameters2, expressionConverter2.AliasGenerator);
						objectQueryExecutionPlan = compiledQueryCacheEntry.SetExecutionPlan(objectQueryExecutionPlan, useCSharpNullComparisonBehavior);
					}
				}
			}
			ObjectParameterCollection objectParameterCollection = base.EnsureParameters();
			if (objectQueryExecutionPlan.CompiledQueryParameters != null && objectQueryExecutionPlan.CompiledQueryParameters.Any<Tuple<ObjectParameter, QueryParameterExpression>>())
			{
				objectParameterCollection.SetReadOnly(false);
				objectParameterCollection.Clear();
				foreach (Tuple<ObjectParameter, QueryParameterExpression> tuple in objectQueryExecutionPlan.CompiledQueryParameters)
				{
					ObjectParameter objectParameter = tuple.Item1.ShallowCopy();
					QueryParameterExpression item = tuple.Item2;
					objectParameterCollection.Add(objectParameter);
					if (item != null)
					{
						objectParameter.Value = item.EvaluateParameter(this._parameterValues);
					}
				}
			}
			objectParameterCollection.SetReadOnly(true);
			return objectQueryExecutionPlan;
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x000B0F7C File Offset: 0x000AF17C
		protected override TypeUsage GetResultType()
		{
			CompiledQueryCacheEntry cacheEntry = this._cacheEntry;
			TypeUsage typeUsage;
			if (cacheEntry != null && cacheEntry.TryGetResultType(out typeUsage))
			{
				return typeUsage;
			}
			return base.GetResultType();
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x060036C6 RID: 14022 RVA: 0x000B0FA5 File Offset: 0x000AF1A5
		internal override Expression Expression
		{
			get
			{
				return CompiledELinqQueryState.CreateDonateableExpressionVisitor.Replace((LambdaExpression)base.Expression, base.ObjectContext, this._parameterValues);
			}
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x000B0FC4 File Offset: 0x000AF1C4
		protected override ExpressionConverter CreateExpressionConverter()
		{
			LambdaExpression lambdaExpression = (LambdaExpression)base.Expression;
			return new ExpressionConverter(Funcletizer.CreateCompiledQueryEvaluationFuncletizer(base.ObjectContext, lambdaExpression.Parameters.First<ParameterExpression>(), new ReadOnlyCollection<ParameterExpression>(lambdaExpression.Parameters.Skip(1).ToList<ParameterExpression>())), lambdaExpression.Body);
		}

		// Token: 0x040011C5 RID: 4549
		private readonly Guid _cacheToken;

		// Token: 0x040011C6 RID: 4550
		private readonly object[] _parameterValues;

		// Token: 0x040011C7 RID: 4551
		private CompiledQueryCacheEntry _cacheEntry;

		// Token: 0x040011C8 RID: 4552
		private readonly ObjectQueryExecutionPlanFactory _objectQueryExecutionPlanFactory;

		// Token: 0x02000A6F RID: 2671
		private sealed class CreateDonateableExpressionVisitor : EntityExpressionVisitor
		{
			// Token: 0x060061D3 RID: 25043 RVA: 0x001535A0 File Offset: 0x001517A0
			private CreateDonateableExpressionVisitor(Dictionary<ParameterExpression, object> parameterToValueLookup)
			{
				this._parameterToValueLookup = parameterToValueLookup;
			}

			// Token: 0x060061D4 RID: 25044 RVA: 0x001535B0 File Offset: 0x001517B0
			internal static Expression Replace(LambdaExpression query, ObjectContext objectContext, object[] parameterValues)
			{
				Dictionary<ParameterExpression, object> dictionary = query.Parameters.Skip(1).Zip(parameterValues).ToDictionary((KeyValuePair<ParameterExpression, object> pair) => pair.Key, (KeyValuePair<ParameterExpression, object> pair) => pair.Value);
				dictionary.Add(query.Parameters.First<ParameterExpression>(), objectContext);
				return new CompiledELinqQueryState.CreateDonateableExpressionVisitor(dictionary).Visit(query.Body);
			}

			// Token: 0x060061D5 RID: 25045 RVA: 0x00153634 File Offset: 0x00151834
			internal override Expression VisitParameter(ParameterExpression p)
			{
				object obj;
				Expression expression;
				if (this._parameterToValueLookup.TryGetValue(p, out obj))
				{
					expression = Expression.Constant(obj, p.Type);
				}
				else
				{
					expression = base.VisitParameter(p);
				}
				return expression;
			}

			// Token: 0x04002B17 RID: 11031
			private readonly Dictionary<ParameterExpression, object> _parameterToValueLookup;
		}
	}
}
