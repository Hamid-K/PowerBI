using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Common.QueryCache;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Utilities;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200040C RID: 1036
	internal sealed class EntitySqlQueryState : ObjectQueryState
	{
		// Token: 0x06003110 RID: 12560 RVA: 0x0009C6A8 File Offset: 0x0009A8A8
		internal EntitySqlQueryState(Type elementType, string commandText, bool allowsLimit, ObjectContext context, ObjectParameterCollection parameters, Span span)
			: this(elementType, commandText, null, allowsLimit, context, parameters, span, null)
		{
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x0009C6C8 File Offset: 0x0009A8C8
		internal EntitySqlQueryState(Type elementType, string commandText, DbExpression expression, bool allowsLimit, ObjectContext context, ObjectParameterCollection parameters, Span span, ObjectQueryExecutionPlanFactory objectQueryExecutionPlanFactory = null)
			: base(elementType, context, parameters, span)
		{
			Check.NotEmpty(commandText, "commandText");
			this._queryText = commandText;
			this._queryExpression = expression;
			this._allowsLimit = allowsLimit;
			this._objectQueryExecutionPlanFactory = objectQueryExecutionPlanFactory ?? new ObjectQueryExecutionPlanFactory(null);
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x0009C716 File Offset: 0x0009A916
		internal bool AllowsLimitSubclause
		{
			get
			{
				return this._allowsLimit;
			}
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x0009C71E File Offset: 0x0009A91E
		internal override bool TryGetCommandText(out string commandText)
		{
			commandText = this._queryText;
			return true;
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x0009C729 File Offset: 0x0009A929
		internal override bool TryGetExpression(out Expression expression)
		{
			expression = null;
			return false;
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x0009C72F File Offset: 0x0009A92F
		protected override TypeUsage GetResultType()
		{
			return this.Parse().ResultType;
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x0009C73C File Offset: 0x0009A93C
		internal override ObjectQueryState Include<TElementType>(ObjectQuery<TElementType> sourceQuery, string includePath)
		{
			ObjectQueryState objectQueryState = new EntitySqlQueryState(base.ElementType, this._queryText, this._queryExpression, this._allowsLimit, base.ObjectContext, ObjectParameterCollection.DeepCopy(base.Parameters), Span.IncludeIn(base.Span, includePath), null);
			base.ApplySettingsTo(objectQueryState);
			return objectQueryState;
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x0009C790 File Offset: 0x0009A990
		internal override ObjectQueryExecutionPlan GetExecutionPlan(MergeOption? forMergeOption)
		{
			MergeOption mergeOption = ObjectQueryState.EnsureMergeOption(new MergeOption?[] { forMergeOption, base.UserSpecifiedMergeOption });
			ObjectQueryExecutionPlan objectQueryExecutionPlan = this._cachedPlan;
			if (objectQueryExecutionPlan != null)
			{
				if (objectQueryExecutionPlan.MergeOption == mergeOption && objectQueryExecutionPlan.Streaming == base.EffectiveStreamingBehavior)
				{
					return objectQueryExecutionPlan;
				}
				objectQueryExecutionPlan = null;
			}
			QueryCacheManager queryCacheManager = null;
			EntitySqlQueryCacheKey entitySqlQueryCacheKey = null;
			if (base.PlanCachingEnabled)
			{
				entitySqlQueryCacheKey = new EntitySqlQueryCacheKey(base.ObjectContext.DefaultContainerName, this._queryText, (base.Parameters == null) ? 0 : base.Parameters.Count, (base.Parameters == null) ? null : base.Parameters.GetCacheKey(), (base.Span == null) ? null : base.Span.GetCacheKey(), mergeOption, base.EffectiveStreamingBehavior, base.ElementType);
				queryCacheManager = base.ObjectContext.MetadataWorkspace.GetQueryCacheManager();
				ObjectQueryExecutionPlan objectQueryExecutionPlan2 = null;
				if (queryCacheManager.TryCacheLookup<EntitySqlQueryCacheKey, ObjectQueryExecutionPlan>(entitySqlQueryCacheKey, out objectQueryExecutionPlan2))
				{
					objectQueryExecutionPlan = objectQueryExecutionPlan2;
				}
			}
			if (objectQueryExecutionPlan == null)
			{
				DbExpression dbExpression = this.Parse();
				DbQueryCommandTree dbQueryCommandTree = DbQueryCommandTree.FromValidExpression(base.ObjectContext.MetadataWorkspace, DataSpace.CSpace, dbExpression, true, false);
				objectQueryExecutionPlan = this._objectQueryExecutionPlanFactory.Prepare(base.ObjectContext, dbQueryCommandTree, base.ElementType, mergeOption, base.EffectiveStreamingBehavior, base.Span, null, DbExpressionBuilder.AliasGenerator);
				if (entitySqlQueryCacheKey != null)
				{
					QueryCacheEntry queryCacheEntry = new QueryCacheEntry(entitySqlQueryCacheKey, objectQueryExecutionPlan);
					QueryCacheEntry queryCacheEntry2 = null;
					if (queryCacheManager.TryLookupAndAdd(queryCacheEntry, out queryCacheEntry2))
					{
						objectQueryExecutionPlan = (ObjectQueryExecutionPlan)queryCacheEntry2.GetTarget();
					}
				}
			}
			if (base.Parameters != null)
			{
				base.Parameters.SetReadOnly(true);
			}
			this._cachedPlan = objectQueryExecutionPlan;
			return objectQueryExecutionPlan;
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x0009C910 File Offset: 0x0009AB10
		internal DbExpression Parse()
		{
			if (this._queryExpression != null)
			{
				return this._queryExpression;
			}
			List<DbParameterReferenceExpression> list = null;
			if (base.Parameters != null)
			{
				list = new List<DbParameterReferenceExpression>(base.Parameters.Count);
				foreach (ObjectParameter objectParameter in base.Parameters)
				{
					TypeUsage typeUsage = objectParameter.TypeUsage;
					if (typeUsage == null)
					{
						base.ObjectContext.Perspective.TryGetTypeByName(objectParameter.MappableType.FullNameWithNesting(), false, out typeUsage);
					}
					list.Add(typeUsage.Parameter(objectParameter.Name));
				}
			}
			return CqlQuery.CompileQueryCommandLambda(this._queryText, base.ObjectContext.Perspective, null, list, null).Body;
		}

		// Token: 0x04001030 RID: 4144
		private readonly string _queryText;

		// Token: 0x04001031 RID: 4145
		private readonly DbExpression _queryExpression;

		// Token: 0x04001032 RID: 4146
		private readonly bool _allowsLimit;

		// Token: 0x04001033 RID: 4147
		private readonly ObjectQueryExecutionPlanFactory _objectQueryExecutionPlanFactory;
	}
}
