using System;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x0200005F RID: 95
	internal sealed class SqlQueryOnBaseEntity : SqlQueryOnEntity
	{
		// Token: 0x0600047E RID: 1150 RVA: 0x00013B8A File Offset: 0x00011D8A
		internal SqlQueryOnBaseEntity(QueryPlanBuilder qpBuilder)
			: this(qpBuilder, null)
		{
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00013B94 File Offset: 0x00011D94
		internal SqlQueryOnBaseEntity(QueryPlanBuilder qpBuilder, NestedQueryKey key)
			: base(qpBuilder, key, qpBuilder.SemanticQuery.BaseEntity)
		{
			if (base.Key != null && !(base.Key.FilteredPathItem.ExpressionPathItem is TotalAggregationPathItem))
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			if (base.QueryPlanBuilder.SemanticQuery.BaseEntityFilter != null)
			{
				this.m_baseEntityFilterExpressionInfo = ExpressionProcessInfo.CreateForEntityFilter(base.QueryPlanBuilder.SemanticQuery.BaseEntityFilter, this.Entity);
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00013C0C File Offset: 0x00011E0C
		internal override void SelectExpression(ExpressionProcessInfo info, bool keyExpression)
		{
			if (!info.IsBlob(base.QueryPlanBuilder))
			{
				base.SelectExpression(info, keyExpression);
				return;
			}
			if (keyExpression)
			{
				throw new SemanticQueryEngineException(SR.GroupingByBlob(info.ObjKey.Name));
			}
			if (this.m_blobWrapperQuery == null)
			{
				this.m_blobWrapperQuery = new SqlBlobWrapperQuery(this);
			}
			this.m_blobWrapperQuery.SelectExpression(info, keyExpression);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00013C6C File Offset: 0x00011E6C
		internal override SqlNestedQuery BuildQueryPlanSubtree()
		{
			if (this.m_blobWrapperQuery == null)
			{
				if (this.m_baseEntityFilterExpressionInfo != null)
				{
					base.AddExpressionToProcess(this.m_baseEntityFilterExpressionInfo);
				}
				if (base.BuildQueryPlanSubtree() != null)
				{
					throw SQEAssert.AssertFalseAndThrow("Current query can not be replaced during query construction.", Array.Empty<object>());
				}
				return null;
			}
			else
			{
				SqlBlobWrapperQuery blobWrapperQuery = this.m_blobWrapperQuery;
				this.m_blobWrapperQuery = null;
				if (blobWrapperQuery.BuildQueryPlanSubtree() != null)
				{
					throw SQEAssert.AssertFalseAndThrow("Blob wrapper query can not be replaced during query construction.", Array.Empty<object>());
				}
				return blobWrapperQuery;
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00013CD4 File Offset: 0x00011ED4
		protected override void BuildFilterSql(SqlSelectQuery selectQuery)
		{
			base.BuildFilterSql(selectQuery);
			if (this.m_baseEntityFilterExpressionInfo != null)
			{
				FunctionContext functionContext = FunctionContext.CreateForFilterCondition(((IQPExpressionInfo)this.m_baseEntityFilterExpressionInfo).Expression);
				selectQuery.AddFilterExpression(this.CreateSqlExpression(this.m_baseEntityFilterExpressionInfo, functionContext, selectQuery));
				if (functionContext.Count != 1)
				{
					throw SQEAssert.AssertFalseAndThrow();
				}
			}
		}

		// Token: 0x040001EE RID: 494
		private readonly ExpressionProcessInfo m_baseEntityFilterExpressionInfo;

		// Token: 0x040001EF RID: 495
		private SqlBlobWrapperQuery m_blobWrapperQuery;
	}
}
