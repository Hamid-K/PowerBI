using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000060 RID: 96
	internal abstract class SqlQueryOnEntityBase : SqlNestedQuery
	{
		// Token: 0x06000483 RID: 1155 RVA: 0x00013D24 File Offset: 0x00011F24
		protected SqlQueryOnEntityBase(QueryPlanBuilder qpBuilder, NestedQueryKey key)
			: base(qpBuilder, key)
		{
			if (base.Key != null)
			{
				this.PerformAggregation = base.Key.PerformAggregation;
				if (base.Key.FilteredPathItem.FilterCondition != null)
				{
					this.m_filterConditionExpressionInfo = ExpressionProcessInfo.CreateForEntityFilter(base.Key.FilteredPathItem.FilterCondition, base.Key.FilteredPathItem.TargetEntity);
				}
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00013D9D File Offset: 0x00011F9D
		internal virtual IQueryEntity Entity
		{
			[DebuggerStepThrough]
			get
			{
				if (base.Key == null)
				{
					return null;
				}
				return base.Key.FilteredPathItem.TargetEntity;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00013DB9 File Offset: 0x00011FB9
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x00013DC1 File Offset: 0x00011FC1
		internal int CollapseAggregationOptimizationOwnerID
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_collapseAggregationOptimizationOwnerID;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_collapseAggregationOptimizationOwnerID = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00013DCA File Offset: 0x00011FCA
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x00013DD2 File Offset: 0x00011FD2
		internal int CollapseAggregationOptimizationTargetID
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_collapseAggregationOptimizationTargetID;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_collapseAggregationOptimizationTargetID = value;
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00013DDB File Offset: 0x00011FDB
		internal override SqlNestedQuery BuildQueryPlanSubtree()
		{
			if (this.m_filterConditionExpressionInfo != null)
			{
				base.AddExpressionToProcess(this.m_filterConditionExpressionInfo);
			}
			if (base.BuildQueryPlanSubtree() != null)
			{
				throw SQEAssert.AssertFalseAndThrow("Current query can not be replaced during query construction.", Array.Empty<object>());
			}
			new SqlQueryOnEntityBase.SuppressAggregationOptimizer(this).PerformOptimization();
			return null;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00013E18 File Offset: 0x00012018
		internal override bool IsDetailsKey(Expression keyExpressionObjKey)
		{
			ExpressionProcessInfo expressionProcessInfo = base.KeyExpressions[keyExpressionObjKey];
			EntityRefNode nodeAsEntityRef = expressionProcessInfo.NodeAsEntityRef;
			return nodeAsEntityRef != null && nodeAsEntityRef.Entity == this.Entity && ((IQPExpressionInfo)expressionProcessInfo).IsInnerMost;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00013E55 File Offset: 0x00012055
		protected override void OptimizePathPointIndex(ExpressionProcessInfo info)
		{
			if (info.PathPointIndex < info.Path.Length && (!info.CheckAggregateExpression(false) || info.IsInvokedAggregate))
			{
				this.OptimizePathPointIndexToProcessAt(info);
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00013E82 File Offset: 0x00012082
		protected override void FinalizeExpressionProcessInfo(ExpressionProcessInfo info)
		{
			if (info.PathPointIndex < info.Path.Length)
			{
				info.CreateAndSetNestedQueryKey();
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00013EA0 File Offset: 0x000120A0
		protected override SqlSelectQuery CreateSqlSelectQuery(SqlBatch sqlBatch)
		{
			if (this.Entity.ModelEntity != null && this.Entity.ModelEntity.Binding != null)
			{
				return sqlBatch.CreateSelectQuery(this.Entity.ModelEntity);
			}
			throw new NotImplementedException("Calculated entities are not supported in SQL 2005 (this.Entity.ModelEntity != null && this.Entity.ModelEntity.Binding != null).");
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00013EEC File Offset: 0x000120EC
		protected override void BuildFilterSql(SqlSelectQuery selectQuery)
		{
			base.BuildFilterSql(selectQuery);
			if (this.m_filterConditionExpressionInfo != null)
			{
				FunctionContext functionContext = FunctionContext.CreateForFilterCondition(((IQPExpressionInfo)this.m_filterConditionExpressionInfo).Expression);
				selectQuery.AddFilterExpression(this.CreateSqlExpression(this.m_filterConditionExpressionInfo, functionContext, selectQuery));
				if (functionContext.Count != 1)
				{
					throw SQEAssert.AssertFalseAndThrow();
				}
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00013F3C File Offset: 0x0001213C
		protected override void ApplyGroupBy(SqlSelectQuery selectQuery)
		{
			if (base.Key != null)
			{
				base.ApplyGroupBy(selectQuery);
				return;
			}
			foreach (ExpressionProcessInfo expressionProcessInfo in base.KeyExpressions)
			{
				if (this.IsDetailsKey(expressionProcessInfo.ObjKey))
				{
					return;
				}
			}
			selectQuery.GroupBy = true;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00013FAC File Offset: 0x000121AC
		protected override void TraceQueryExtInfo(FormattedStringWriter qpTracer)
		{
			qpTracer.IndentWriteLine("ENTITY={0}{1}{2}", new object[]
			{
				this.Entity,
				(this.m_collapseAggregationOptimizationOwnerID > 0) ? (" CAO_OwnerID=" + this.m_collapseAggregationOptimizationOwnerID) : null,
				(this.m_collapseAggregationOptimizationTargetID > 0) ? (" CAO_TargetID=" + this.m_collapseAggregationOptimizationTargetID) : null
			});
			base.TraceQueryExtInfo(qpTracer);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00014024 File Offset: 0x00012224
		private void OptimizePathPointIndexToProcessAt(ExpressionProcessInfo info)
		{
			FilteredPathItem filteredPathItem = info.Path[info.PathPointIndex];
			if (filteredPathItem.Cardinality == Cardinality.Many || filteredPathItem.FilterCondition != null)
			{
				return;
			}
			if (filteredPathItem.ExpressionPathItem is RolePathItem && SqlQueryOnRolePathItem.CanSkipPathPoint((RolePathItem)filteredPathItem.ExpressionPathItem))
			{
				info.AdjustPathPointIndex(info.PathPointIndex + 1);
			}
		}

		// Token: 0x040001F0 RID: 496
		private readonly ExpressionProcessInfo m_filterConditionExpressionInfo;

		// Token: 0x040001F1 RID: 497
		private int m_collapseAggregationOptimizationOwnerID = -1;

		// Token: 0x040001F2 RID: 498
		private int m_collapseAggregationOptimizationTargetID = -1;

		// Token: 0x020000D5 RID: 213
		private sealed class SuppressAggregationOptimizer
		{
			// Token: 0x0600075F RID: 1887 RVA: 0x0001C8E6 File Offset: 0x0001AAE6
			internal SuppressAggregationOptimizer(SqlQueryOnEntityBase sqlQuery)
			{
				if (sqlQuery == null)
				{
					throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("sqlQuery"));
				}
				this.m_sqlQuery = sqlQuery;
			}

			// Token: 0x06000760 RID: 1888 RVA: 0x0001C908 File Offset: 0x0001AB08
			internal void PerformOptimization()
			{
				if (this.m_sqlQuery.Key == null || this.m_sqlQuery.Key.FilteredPathItem.Cardinality == Cardinality.One || this.m_sqlQuery.NestedQueries.Count == 0)
				{
					return;
				}
				int i = 0;
				IL_00B0:
				while (i < this.m_sqlQuery.SelectExpressions.Count)
				{
					ExpressionProcessInfo expressionProcessInfo = this.m_sqlQuery.SelectExpressions[i];
					while (expressionProcessInfo.NestedQuery != null)
					{
						if (expressionProcessInfo.NestedQuery.Key == null)
						{
							return;
						}
						if (expressionProcessInfo.NestedQuery.Key.FilteredPathItem.Cardinality == Cardinality.Many)
						{
							if (!this.HandleToManyQuery(expressionProcessInfo.NestedQuery))
							{
								return;
							}
							IL_00AC:
							i++;
							goto IL_00B0;
						}
						else
						{
							expressionProcessInfo = expressionProcessInfo.NestedQuery.SelectExpressions[expressionProcessInfo.ObjKey];
						}
					}
					if (expressionProcessInfo.AggregationContext != AggregationContext.Scalar)
					{
						return;
					}
					goto IL_00AC;
				}
				if (this.m_queryToSuppress != null && this.m_queryToSuppress is SqlQueryOnEntityBase)
				{
					SqlQueryOnEntityBase sqlQueryOnEntityBase = (SqlQueryOnEntityBase)this.m_queryToSuppress;
					sqlQueryOnEntityBase.PerformAggregation = false;
					sqlQueryOnEntityBase.CollapseAggregationOptimizationOwnerID = this.m_sqlQuery.QueryID;
					this.m_sqlQuery.CollapseAggregationOptimizationTargetID = sqlQueryOnEntityBase.QueryID;
					return;
				}
			}

			// Token: 0x06000761 RID: 1889 RVA: 0x0001CA26 File Offset: 0x0001AC26
			private bool HandleToManyQuery(SqlNestedQuery nq)
			{
				if (!nq.Key.PerformAggregationOptional)
				{
					return false;
				}
				if (this.m_queryToSuppress == null)
				{
					this.m_queryToSuppress = nq;
					return true;
				}
				return nq == this.m_queryToSuppress;
			}

			// Token: 0x040003B6 RID: 950
			private readonly SqlQueryOnEntityBase m_sqlQuery;

			// Token: 0x040003B7 RID: 951
			private SqlNestedQuery m_queryToSuppress;
		}
	}
}
