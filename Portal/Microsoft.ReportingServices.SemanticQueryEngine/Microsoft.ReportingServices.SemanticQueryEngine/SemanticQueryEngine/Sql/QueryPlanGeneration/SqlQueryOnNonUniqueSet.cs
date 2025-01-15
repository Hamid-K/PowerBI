using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000063 RID: 99
	internal sealed class SqlQueryOnNonUniqueSet : SqlNestedQuery
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x00014290 File Offset: 0x00012490
		internal SqlQueryOnNonUniqueSet(SqlNestedQuery queryToWrap)
			: base(queryToWrap.QueryPlanBuilder, queryToWrap.Key)
		{
			if (base.Key == null)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			if (base.Key.HasOuterAggregation || !queryToWrap.PerformAggregation)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			this.m_queryToWrap = queryToWrap;
			FilteredPath nonTransitivePathSegment = base.Key.GetNonTransitivePathSegment();
			if (nonTransitivePathSegment.GetCardinalityContext() != CardinalityContext.NonUniqueSet)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			this.SetupDistinctAggregation(nonTransitivePathSegment);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00014301 File Offset: 0x00012501
		internal override bool IsDetailsKey(Expression keyExpressionObjKey)
		{
			return this.m_queryToWrap.IsDetailsKey(keyExpressionObjKey);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001430F File Offset: 0x0001250F
		internal override SqlTableSource Join(SqlSelectQuery parentQuery, SqlSelectQuery nestedQuery)
		{
			this.SelectJoinKeys(nestedQuery);
			return this.m_queryToWrap.Join(parentQuery, nestedQuery);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00014325 File Offset: 0x00012525
		internal override void SelectJoinKeys(SqlSelectQuery selectQuery)
		{
			if (this.m_wrappedSelectQuery == null)
			{
				throw SQEAssert.AssertFalseAndThrow("Select statement for the wrapped query has not been created.", Array.Empty<object>());
			}
			this.m_queryToWrap.SelectJoinKeys(this.m_wrappedSelectQuery);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00014350 File Offset: 0x00012550
		internal override void Trace(FormattedStringWriter qpTracer)
		{
			base.Trace(qpTracer);
			qpTracer.IndentWriteLine("Wrapped query:");
			int num = qpTracer.IndentationLevel + 1;
			qpTracer.IndentationLevel = num;
			this.m_queryToWrap.Trace(qpTracer);
			num = qpTracer.IndentationLevel - 1;
			qpTracer.IndentationLevel = num;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00004B5D File Offset: 0x00002D5D
		internal override bool IsWrapperQuery
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001439B File Offset: 0x0001259B
		protected override void BuildQueryPlanSubtreeExt()
		{
			base.BuildQueryPlanSubtreeExt();
			if (this.m_queryToWrap.BuildQueryPlanSubtree() != null)
			{
				throw SQEAssert.AssertFalseAndThrow("Wrapped query can not be replaced during query construction.", Array.Empty<object>());
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00003FB8 File Offset: 0x000021B8
		protected override void FinalizeExpressionProcessInfo(ExpressionProcessInfo info)
		{
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00003FB8 File Offset: 0x000021B8
		protected override void PreprocessExpression(ExpressionProcessInfo info)
		{
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000143C0 File Offset: 0x000125C0
		protected override void ProcessExpression(ExpressionProcessInfo info)
		{
			AggregationContext aggregationContext = ((info.AggregationContext == AggregationContext.Scalar) ? AggregationContext.Scalar : AggregationContext.Inner);
			info.NestedQuery = this.m_queryToWrap;
			ExpressionProcessInfo expressionProcessInfo = ExpressionProcessInfo.CreateForWrappedQuery(info, aggregationContext);
			this.m_queryToWrap.SelectExpression(expressionProcessInfo, base.KeyExpressions.Contains(info));
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00014408 File Offset: 0x00012608
		protected override SqlSelectQuery CreateSqlSelectQuery(SqlBatch sqlBatch)
		{
			this.m_wrappedSelectQuery = this.m_queryToWrap.BuildSql(sqlBatch, false);
			this.m_wrappedSelectQuery.GroupBy = true;
			SqlSelectQuery sqlSelectQuery = sqlBatch.CreateSelectQuery(this.m_wrappedSelectQuery);
			this.m_queryToWrap.TableSourceInParentQuery = sqlSelectQuery.PrimaryTableSource;
			return sqlSelectQuery;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00014454 File Offset: 0x00012654
		private void SetupDistinctAggregation(FilteredPath path)
		{
			foreach (ExpressionProcessInfo expressionProcessInfo in ExpressionProcessInfo.CreateForDistinctAggregation(path))
			{
				this.m_queryToWrap.SelectExpression(expressionProcessInfo, false);
			}
			this.m_queryToWrap.PerformAggregation = false;
			this.PerformAggregation = true;
		}

		// Token: 0x040001F4 RID: 500
		private readonly SqlNestedQuery m_queryToWrap;

		// Token: 0x040001F5 RID: 501
		private SqlSelectQuery m_wrappedSelectQuery;
	}
}
