using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Common.QueryCache;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x0200045E RID: 1118
	internal class ELinqQueryState : ObjectQueryState
	{
		// Token: 0x060036C8 RID: 14024 RVA: 0x000B1014 File Offset: 0x000AF214
		internal ELinqQueryState(Type elementType, ObjectContext context, Expression expression, ObjectQueryExecutionPlanFactory objectQueryExecutionPlanFactory = null)
			: base(elementType, context, null, null)
		{
			this._expression = expression;
			this._useCSharpNullComparisonBehavior = context.ContextOptions.UseCSharpNullComparisonBehavior;
			this._disableFilterOverProjectionSimplificationForCustomFunctions = context.ContextOptions.DisableFilterOverProjectionSimplificationForCustomFunctions;
			this._objectQueryExecutionPlanFactory = objectQueryExecutionPlanFactory ?? new ObjectQueryExecutionPlanFactory(null);
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000B1066 File Offset: 0x000AF266
		internal ELinqQueryState(Type elementType, ObjectQuery query, Expression expression, ObjectQueryExecutionPlanFactory objectQueryExecutionPlanFactory = null)
			: base(elementType, query)
		{
			this._expression = expression;
			this._objectQueryExecutionPlanFactory = objectQueryExecutionPlanFactory ?? new ObjectQueryExecutionPlanFactory(null);
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x000B1089 File Offset: 0x000AF289
		protected override TypeUsage GetResultType()
		{
			return this.CreateExpressionConverter().Convert().ResultType;
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x000B109C File Offset: 0x000AF29C
		internal override ObjectQueryExecutionPlan GetExecutionPlan(MergeOption? forMergeOption)
		{
			ObjectQueryExecutionPlan objectQueryExecutionPlan = this._cachedPlan;
			if (objectQueryExecutionPlan != null)
			{
				MergeOption? mergeOption = ObjectQueryState.GetMergeOption(new MergeOption?[] { forMergeOption, base.UserSpecifiedMergeOption });
				if ((mergeOption != null && mergeOption.Value != objectQueryExecutionPlan.MergeOption) || this._recompileRequired() || base.ObjectContext.ContextOptions.UseCSharpNullComparisonBehavior != this._useCSharpNullComparisonBehavior || base.ObjectContext.ContextOptions.DisableFilterOverProjectionSimplificationForCustomFunctions != this._disableFilterOverProjectionSimplificationForCustomFunctions)
				{
					objectQueryExecutionPlan = null;
				}
			}
			if (objectQueryExecutionPlan == null)
			{
				this._recompileRequired = null;
				this.ResetParameters();
				ExpressionConverter expressionConverter = this.CreateExpressionConverter();
				DbExpression dbExpression = expressionConverter.Convert();
				this._recompileRequired = expressionConverter.RecompileRequired;
				MergeOption mergeOption2 = ObjectQueryState.EnsureMergeOption(new MergeOption?[] { forMergeOption, base.UserSpecifiedMergeOption, expressionConverter.PropagatedMergeOption });
				this._useCSharpNullComparisonBehavior = base.ObjectContext.ContextOptions.UseCSharpNullComparisonBehavior;
				this._disableFilterOverProjectionSimplificationForCustomFunctions = base.ObjectContext.ContextOptions.DisableFilterOverProjectionSimplificationForCustomFunctions;
				this._linqParameters = expressionConverter.GetParameters();
				if (this._linqParameters != null && this._linqParameters.Any<Tuple<ObjectParameter, QueryParameterExpression>>())
				{
					ObjectParameterCollection objectParameterCollection = base.EnsureParameters();
					objectParameterCollection.SetReadOnly(false);
					foreach (Tuple<ObjectParameter, QueryParameterExpression> tuple in this._linqParameters)
					{
						ObjectParameter item = tuple.Item1;
						objectParameterCollection.Add(item);
					}
					objectParameterCollection.SetReadOnly(true);
				}
				QueryCacheManager queryCacheManager = null;
				LinqQueryCacheKey linqQueryCacheKey = null;
				string text;
				if (base.PlanCachingEnabled && !this._recompileRequired() && ExpressionKeyGen.TryGenerateKey(dbExpression, out text))
				{
					linqQueryCacheKey = new LinqQueryCacheKey(text, (base.Parameters == null) ? 0 : base.Parameters.Count, (base.Parameters == null) ? null : base.Parameters.GetCacheKey(), (expressionConverter.PropagatedSpan == null) ? null : expressionConverter.PropagatedSpan.GetCacheKey(), mergeOption2, base.EffectiveStreamingBehavior, this._useCSharpNullComparisonBehavior, base.ElementType);
					queryCacheManager = base.ObjectContext.MetadataWorkspace.GetQueryCacheManager();
					ObjectQueryExecutionPlan objectQueryExecutionPlan2 = null;
					if (queryCacheManager.TryCacheLookup<LinqQueryCacheKey, ObjectQueryExecutionPlan>(linqQueryCacheKey, out objectQueryExecutionPlan2))
					{
						objectQueryExecutionPlan = objectQueryExecutionPlan2;
					}
				}
				if (objectQueryExecutionPlan == null)
				{
					DbQueryCommandTree dbQueryCommandTree = DbQueryCommandTree.FromValidExpression(base.ObjectContext.MetadataWorkspace, DataSpace.CSpace, dbExpression, !this._useCSharpNullComparisonBehavior, this._disableFilterOverProjectionSimplificationForCustomFunctions);
					objectQueryExecutionPlan = this._objectQueryExecutionPlanFactory.Prepare(base.ObjectContext, dbQueryCommandTree, base.ElementType, mergeOption2, base.EffectiveStreamingBehavior, expressionConverter.PropagatedSpan, null, expressionConverter.AliasGenerator);
					if (linqQueryCacheKey != null)
					{
						QueryCacheEntry queryCacheEntry = new QueryCacheEntry(linqQueryCacheKey, objectQueryExecutionPlan);
						QueryCacheEntry queryCacheEntry2 = null;
						if (queryCacheManager.TryLookupAndAdd(queryCacheEntry, out queryCacheEntry2))
						{
							objectQueryExecutionPlan = (ObjectQueryExecutionPlan)queryCacheEntry2.GetTarget();
						}
					}
				}
				this._cachedPlan = objectQueryExecutionPlan;
			}
			if (this._linqParameters != null)
			{
				foreach (Tuple<ObjectParameter, QueryParameterExpression> tuple2 in this._linqParameters)
				{
					ObjectParameter item2 = tuple2.Item1;
					QueryParameterExpression item3 = tuple2.Item2;
					if (item3 != null)
					{
						item2.Value = item3.EvaluateParameter(null);
					}
				}
			}
			return objectQueryExecutionPlan;
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000B13D8 File Offset: 0x000AF5D8
		internal override ObjectQueryState Include<TElementType>(ObjectQuery<TElementType> sourceQuery, string includePath)
		{
			MethodInfo includeMethod = ELinqQueryState.GetIncludeMethod<TElementType>(sourceQuery);
			Expression expression = Expression.Call(Expression.Constant(sourceQuery), includeMethod, new Expression[] { Expression.Constant(includePath, typeof(string)) });
			ObjectQueryState objectQueryState = new ELinqQueryState(base.ElementType, base.ObjectContext, expression, null);
			base.ApplySettingsTo(objectQueryState);
			return objectQueryState;
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x000B142E File Offset: 0x000AF62E
		internal static MethodInfo GetIncludeMethod<TElementType>(ObjectQuery<TElementType> sourceQuery)
		{
			return sourceQuery.GetType().GetOnlyDeclaredMethod("Include");
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x000B1440 File Offset: 0x000AF640
		internal override bool TryGetCommandText(out string commandText)
		{
			commandText = null;
			return false;
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x000B1446 File Offset: 0x000AF646
		internal override bool TryGetExpression(out Expression expression)
		{
			expression = this.Expression;
			return true;
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060036D0 RID: 14032 RVA: 0x000B1451 File Offset: 0x000AF651
		internal virtual Expression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x000B1459 File Offset: 0x000AF659
		protected virtual ExpressionConverter CreateExpressionConverter()
		{
			return new ExpressionConverter(Funcletizer.CreateQueryFuncletizer(base.ObjectContext), this._expression);
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x000B1474 File Offset: 0x000AF674
		private void ResetParameters()
		{
			if (base.Parameters != null)
			{
				bool isReadOnly = ((ICollection<ObjectParameter>)base.Parameters).IsReadOnly;
				if (isReadOnly)
				{
					base.Parameters.SetReadOnly(false);
				}
				base.Parameters.Clear();
				if (isReadOnly)
				{
					base.Parameters.SetReadOnly(true);
				}
			}
			this._linqParameters = null;
		}

		// Token: 0x040011C9 RID: 4553
		private readonly Expression _expression;

		// Token: 0x040011CA RID: 4554
		private Func<bool> _recompileRequired;

		// Token: 0x040011CB RID: 4555
		private IEnumerable<Tuple<ObjectParameter, QueryParameterExpression>> _linqParameters;

		// Token: 0x040011CC RID: 4556
		private bool _useCSharpNullComparisonBehavior;

		// Token: 0x040011CD RID: 4557
		private bool _disableFilterOverProjectionSimplificationForCustomFunctions;

		// Token: 0x040011CE RID: 4558
		private readonly ObjectQueryExecutionPlanFactory _objectQueryExecutionPlanFactory;
	}
}
