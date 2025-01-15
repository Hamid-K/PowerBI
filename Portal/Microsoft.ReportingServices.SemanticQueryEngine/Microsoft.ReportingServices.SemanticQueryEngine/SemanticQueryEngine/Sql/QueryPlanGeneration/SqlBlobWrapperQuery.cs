using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x0200005B RID: 91
	internal sealed class SqlBlobWrapperQuery : SqlNestedQuery
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x00012D25 File Offset: 0x00010F25
		internal SqlBlobWrapperQuery(SqlQueryOnBaseEntity primaryQuery)
			: base(primaryQuery.QueryPlanBuilder, primaryQuery.Key)
		{
			this.m_primaryQuery = primaryQuery;
			if (this.PerformAggregation)
			{
				throw SQEAssert.AssertFalseAndThrow("this.PerformAggregation can not be set on SqlBlobWrapperQuery.", Array.Empty<object>());
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00012D64 File Offset: 0x00010F64
		internal override SqlNestedQuery BuildQueryPlanSubtree()
		{
			foreach (ExpressionProcessInfo expressionProcessInfo in this.m_primaryQuery.SelectExpressions)
			{
				if (expressionProcessInfo.AggregationContext != AggregationContext.Scalar)
				{
					throw SQEAssert.AssertFalseAndThrow("SqlBlobWrapper can not process primary expressions in non-scalar aggregation context.", Array.Empty<object>());
				}
				this.SelectExpression(ExpressionProcessInfo.CreateForWrappedQuery(expressionProcessInfo, expressionProcessInfo.AggregationContext), this.m_primaryQuery.KeyExpressions.Contains(expressionProcessInfo));
			}
			if (base.BuildQueryPlanSubtree() != null)
			{
				throw SQEAssert.AssertFalseAndThrow("Current query can not be replaced during query construction.", Array.Empty<object>());
			}
			return null;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00012E04 File Offset: 0x00011004
		internal override bool IsDetailsKey(Expression keyExpressionObjKey)
		{
			return this.m_primaryQuery.IsDetailsKey(keyExpressionObjKey);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00012E12 File Offset: 0x00011012
		internal override SqlTableSource Join(SqlSelectQuery parentQuery, SqlSelectQuery nestedQuery)
		{
			this.SelectJoinKeys(nestedQuery);
			return this.m_primaryQuery.Join(parentQuery, nestedQuery);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00012E28 File Offset: 0x00011028
		internal override void SelectJoinKeys(SqlSelectQuery selectQuery)
		{
			if (this.m_primarySelectQuery == null)
			{
				throw SQEAssert.AssertFalseAndThrow("Select statement for the primary query has not been created.", Array.Empty<object>());
			}
			this.m_primaryQuery.SelectJoinKeys(this.m_primarySelectQuery);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00012E54 File Offset: 0x00011054
		internal override void Trace(FormattedStringWriter qpTracer)
		{
			base.Trace(qpTracer);
			qpTracer.IndentWriteLine("Primary query:");
			int num = qpTracer.IndentationLevel + 1;
			qpTracer.IndentationLevel = num;
			this.m_primaryQuery.Trace(qpTracer);
			num = qpTracer.IndentationLevel - 1;
			qpTracer.IndentationLevel = num;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00004B5D File Offset: 0x00002D5D
		internal override bool IsWrapperQuery
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00012E9F File Offset: 0x0001109F
		protected override void BuildQueryPlanSubtreeExt()
		{
			base.BuildQueryPlanSubtreeExt();
			if (this.m_primaryQuery.BuildQueryPlanSubtree() != null)
			{
				throw SQEAssert.AssertFalseAndThrow("Primary query can not be replaced during query construction.", Array.Empty<object>());
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00003FB8 File Offset: 0x000021B8
		protected override void FinalizeExpressionProcessInfo(ExpressionProcessInfo info)
		{
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00003FB8 File Offset: 0x000021B8
		protected override void PreprocessExpression(ExpressionProcessInfo info)
		{
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00012EC4 File Offset: 0x000110C4
		protected override void ProcessExpression(ExpressionProcessInfo info)
		{
			if (this.m_primaryQuery.SelectExpressions.Contains(info.ObjKey))
			{
				info.NestedQuery = this.m_primaryQuery;
				return;
			}
			if (info.GroupingExpressionObjKey == null)
			{
				throw SQEAssert.AssertFalseAndThrow("SqlBlobWrapper can not process non-(grouping/details) expressions.", Array.Empty<object>());
			}
			ExpressionProcessInfo expressionProcessInfo = this.m_primaryQuery.SelectExpressions[info.GroupingExpressionObjKey];
			if (expressionProcessInfo == null)
			{
				throw SQEAssert.AssertFalseAndThrow("Can not find grouping expression in the primary query.", Array.Empty<object>());
			}
			if (expressionProcessInfo.NodeAsEntityRef == null)
			{
				throw SQEAssert.AssertFalseAndThrow("Grouping expression node must be entity ref.", Array.Empty<object>());
			}
			int length = expressionProcessInfo.Path.Length;
			info.AdjustPathPointIndex(length);
			SqlQueryOnEntity sqlQueryOnEntity;
			if (!this.m_blobQueries.TryGetValue(expressionProcessInfo.NodeAsEntityRef.Entity, out sqlQueryOnEntity))
			{
				sqlQueryOnEntity = new SqlQueryOnEntity(base.QueryPlanBuilder, null, expressionProcessInfo.NodeAsEntityRef.Entity);
				ExpressionProcessInfo expressionProcessInfo2 = ExpressionProcessInfo.CreateForWrappedQuery(expressionProcessInfo, expressionProcessInfo.AggregationContext);
				expressionProcessInfo2.AdjustPathPointIndex(length);
				sqlQueryOnEntity.SelectExpression(expressionProcessInfo2, true);
				base.NestedQueries.Add(sqlQueryOnEntity);
				this.m_blobQueries.Add(expressionProcessInfo.NodeAsEntityRef.Entity, sqlQueryOnEntity);
			}
			info.NestedQuery = sqlQueryOnEntity;
			if (info.AggregationContext != AggregationContext.Scalar)
			{
				throw SQEAssert.AssertFalseAndThrow("SqlBlobWrapper can not process expressions in non-scalar aggregation context.", Array.Empty<object>());
			}
			ExpressionProcessInfo expressionProcessInfo3 = ExpressionProcessInfo.CreateForWrappedQuery(info, info.AggregationContext);
			if (base.KeyExpressions.Contains(info))
			{
				throw SQEAssert.AssertFalseAndThrow("Blob expression must not be selected as a key.", Array.Empty<object>());
			}
			sqlQueryOnEntity.SelectExpression(expressionProcessInfo3, false);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001302C File Offset: 0x0001122C
		protected override SqlSelectQuery CreateSqlSelectQuery(SqlBatch sqlBatch)
		{
			this.m_primarySelectQuery = this.m_primaryQuery.BuildSql(sqlBatch, false);
			SqlSelectQuery sqlSelectQuery = sqlBatch.CreateSelectQuery(this.m_primarySelectQuery);
			this.m_primaryQuery.TableSourceInParentQuery = sqlSelectQuery.PrimaryTableSource;
			return sqlSelectQuery;
		}

		// Token: 0x040001E1 RID: 481
		private readonly SqlQueryOnBaseEntity m_primaryQuery;

		// Token: 0x040001E2 RID: 482
		private SqlSelectQuery m_primarySelectQuery;

		// Token: 0x040001E3 RID: 483
		private readonly Dictionary<IQueryEntity, SqlQueryOnEntity> m_blobQueries = new Dictionary<IQueryEntity, SqlQueryOnEntity>();
	}
}
