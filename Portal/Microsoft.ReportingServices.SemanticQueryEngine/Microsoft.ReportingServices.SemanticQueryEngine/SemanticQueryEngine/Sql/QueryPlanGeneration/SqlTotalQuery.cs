using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000069 RID: 105
	internal sealed class SqlTotalQuery : SqlQuery
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x00014AA8 File Offset: 0x00012CA8
		internal SqlTotalQuery(QueryPlanBuilder qpBuilder)
			: base(qpBuilder)
		{
			this.m_totalAggregationPathItem = new TotalAggregationPathItem(base.QueryPlanBuilder.SemanticQuery.MeasureGroup.BaseEntity);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00014AFD File Offset: 0x00012CFD
		internal SqlTotalQuery(QueryPlanBuilder qpBuilder, SqlTotalQuery detailsQuery)
			: this(qpBuilder)
		{
			this.m_detailsQuery = detailsQuery;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00014B10 File Offset: 0x00012D10
		internal void SelectMeasure(Expression expression)
		{
			ExpressionProcessInfo expressionProcessInfo = ExpressionProcessInfo.CreateForMeasure(this.m_totalAggregationPathItem, expression);
			this.m_measures.Add(expressionProcessInfo);
			this.SelectExpression(expressionProcessInfo, false);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00014B3E File Offset: 0x00012D3E
		internal override void SelectExpression(ExpressionProcessInfo info, bool keyExpression)
		{
			base.SelectExpression(info, keyExpression);
			if (keyExpression)
			{
				this.m_keyExprAggregationFlags.Add(info, true);
			}
			if (info.IsBlob(base.QueryPlanBuilder))
			{
				this.m_selectingBlobs = true;
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00014B70 File Offset: 0x00012D70
		internal void SelectExpressionAsNull(Expression expression, bool keyExpression)
		{
			if (this.m_detailsQuery == null)
			{
				throw SQEAssert.AssertFalseAndThrow("Only non-details total queries can select expressions as nulls.", Array.Empty<object>());
			}
			ExpressionProcessInfo expressionProcessInfo = ExpressionProcessInfo.CreateForNullSelect(expression);
			this.m_selectAsNullExpressions.Add(expressionProcessInfo);
			base.SelectExpressions.Add(expressionProcessInfo);
			expressionProcessInfo.SetOwner(this);
			if (keyExpression)
			{
				this.m_keyExprAggregationFlags.Add(expressionProcessInfo, false);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00004555 File Offset: 0x00002755
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x00014BCB File Offset: 0x00012DCB
		internal override bool PerformAggregation
		{
			get
			{
				return false;
			}
			set
			{
				throw SQEAssert.AssertFalseAndThrow("SqlTotalQuery must always perform aggregation.", Array.Empty<object>());
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00014BDC File Offset: 0x00012DDC
		internal override void ApplySort(SqlSelectQuery selectQuery)
		{
			if (this.m_detailsQuery == null)
			{
				if (this.PrimaryNestedQuery != null)
				{
					this.PrimaryNestedQuery.ApplySort(selectQuery);
					return;
				}
			}
			else
			{
				this.m_detailsQuery.ApplySort(selectQuery);
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00014C07 File Offset: 0x00012E07
		internal override bool IsDetailsKey(Expression keyExpressionObjKey)
		{
			throw SQEAssert.AssertFalseAndThrow("IsDetailsKey is called on a total query.", Array.Empty<object>());
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00014C18 File Offset: 0x00012E18
		protected override void FinalizeExpressionProcessInfo(ExpressionProcessInfo info)
		{
			if (info.PathPointIndex != 0)
			{
				throw SQEAssert.AssertFalseAndThrow("info.PathPointIndex must be 0", Array.Empty<object>());
			}
			if (this.m_measures.Contains(info))
			{
				if (info.PathPointIndex < info.Path.Length)
				{
					info.CreateAndSetNestedQueryKey();
					return;
				}
			}
			else if (info.Path.Length != 0 && info.Path[info.PathPointIndex].ExpressionPathItem is TotalAggregationPathItem)
			{
				throw SQEAssert.AssertFalseAndThrow("Only measure expressions are allowed to have TotalAggregationPathItem.", Array.Empty<object>());
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00014CA0 File Offset: 0x00012EA0
		protected override void PreprocessExpression(ExpressionProcessInfo info)
		{
			if (this.m_measures.Contains(info))
			{
				base.PreprocessExpression(info);
				if (info.Path.Length > 1 && info.Path[1].Evaluate)
				{
					IQueryEntity sourceEntity = info.Path[1].SourceEntity;
					for (int i = 0; i < base.KeyExpressions.Count; i++)
					{
						EntityRefNode nodeAsEntityRef = base.KeyExpressions[i].NodeAsEntityRef;
						if (nodeAsEntityRef != null && nodeAsEntityRef.Entity == sourceEntity)
						{
							info.Path[1].Evaluate = false;
							return;
						}
					}
				}
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00014D40 File Offset: 0x00012F40
		protected override ExpressionProcessInfo CreateExpressionInfoForFunctionArgument(ExpressionProcessInfo functionInfo, Expression functionArgument)
		{
			ExpressionProcessInfo expressionProcessInfo = ExpressionProcessInfo.CreateForMeasure(this.m_totalAggregationPathItem, functionArgument);
			this.m_measures.Add(expressionProcessInfo);
			return expressionProcessInfo;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00014D68 File Offset: 0x00012F68
		protected override void ProcessExpression(ExpressionProcessInfo info)
		{
			if (this.m_measures.Contains(info))
			{
				base.ProcessExpression(info);
				return;
			}
			SqlNestedQuery orCreatePrimaryNestedQuery = this.GetOrCreatePrimaryNestedQuery();
			if (base.KeyExpressions.Contains(info))
			{
				for (int i = 0; i < base.NestedQueries.Count; i++)
				{
					ExpressionProcessInfo expressionProcessInfo = this.CreateExpressionProcessInfoForGroupingOrDetail(info);
					base.NestedQueries[i].SelectExpression(expressionProcessInfo, true);
				}
			}
			else
			{
				ExpressionProcessInfo expressionProcessInfo2 = this.CreateExpressionProcessInfoForGroupingOrDetail(info);
				orCreatePrimaryNestedQuery.SelectExpression(expressionProcessInfo2, false);
			}
			info.NestedQuery = orCreatePrimaryNestedQuery;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00014DEC File Offset: 0x00012FEC
		protected override SqlSelectQuery CreateSqlSelectQuery(SqlBatch sqlBatch)
		{
			SqlSelectQuery sqlSelectQuery;
			if (base.NestedQueries.Count == 0)
			{
				sqlSelectQuery = sqlBatch.CreateSelectQuery();
			}
			else
			{
				this.m_primaryNestedQueryIndex = new int?(this.m_primaryNestedQueryIndex ?? 0);
				ISelectList selectList = this.PrimaryNestedQuery.BuildSql(sqlBatch, false);
				sqlSelectQuery = sqlBatch.CreateSelectQuery(selectList);
				this.PrimaryNestedQuery.TableSourceInParentQuery = sqlSelectQuery.PrimaryTableSource;
			}
			int num = 0;
			foreach (KeyValuePair<ExpressionProcessInfo, bool> keyValuePair in this.m_keyExprAggregationFlags)
			{
				sqlSelectQuery.SelectAggregationFlag(keyValuePair.Key, keyValuePair.Value);
				if (keyValuePair.Value)
				{
					num++;
				}
			}
			sqlSelectQuery.SelectAggregationFieldCount(num);
			if (this.m_detailsQuery == null)
			{
				sqlBatch.AssociateQPSqlQueryWithISelectList(this, sqlSelectQuery);
			}
			return sqlSelectQuery;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00014ED8 File Offset: 0x000130D8
		protected override void JoinNestedQuery(SqlNestedQuery nestedQuery, SqlSelectQuery selectQuery, SqlBatch sqlBatch)
		{
			if (nestedQuery != this.PrimaryNestedQuery)
			{
				base.JoinNestedQuery(nestedQuery, selectQuery, sqlBatch);
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00014EEC File Offset: 0x000130EC
		protected override SqlExpression CreateSqlExpression(ExpressionProcessInfo info, FunctionContext functionContext, SqlSelectQuery selectQuery)
		{
			if (this.m_selectAsNullExpressions.Contains(info))
			{
				if (this.m_detailsQuery == null)
				{
					throw SQEAssert.AssertFalseAndThrow();
				}
				ISelectList associatedISelectList = selectQuery.SqlBatch.GetAssociatedISelectList(this.m_detailsQuery);
				if (associatedISelectList == null)
				{
					throw SQEAssert.AssertFalseAndThrow();
				}
				SqlSelectExpression selectExpression = associatedISelectList.GetSelectExpression(((IQPExpressionInfo)info).Expression);
				if (selectExpression == null)
				{
					throw SQEAssert.AssertFalseAndThrow();
				}
				return selectQuery.CreateSqlExpressionAsNull(selectExpression.SqlExpression, info);
			}
			else
			{
				if (this.m_measures.Contains(info))
				{
					return base.CreateSqlExpression(info, functionContext, selectQuery);
				}
				return selectQuery.CreateSqlPassThruExpression(info, info.NestedQuery.TableSourceInParentQuery);
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00014F7C File Offset: 0x0001317C
		protected override string TraceExpressionType(ExpressionProcessInfo info)
		{
			string text = base.TraceExpressionType(info).Trim();
			if (this.m_measures.Contains(info))
			{
				text += "M";
			}
			if (this.m_selectAsNullExpressions.Contains(info))
			{
				text += "N";
			}
			if (text.Length == 0)
			{
				text += " ";
			}
			return text;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00014FDF File Offset: 0x000131DF
		private ExpressionProcessInfo CreateExpressionProcessInfoForGroupingOrDetail(ExpressionProcessInfo info)
		{
			return ExpressionProcessInfo.CreateForWrappedQuery(info, info.AggregationContext);
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00014FED File Offset: 0x000131ED
		private SqlNestedQuery PrimaryNestedQuery
		{
			get
			{
				if (this.m_primaryNestedQueryIndex == null)
				{
					return null;
				}
				return base.NestedQueries[this.m_primaryNestedQueryIndex.Value];
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00015014 File Offset: 0x00013214
		private SqlNestedQuery GetOrCreatePrimaryNestedQuery()
		{
			if (this.m_primaryNestedQueryIndex == null)
			{
				if (!this.m_selectingBlobs)
				{
					for (int i = base.NestedQueries.Count - 1; i >= 0; i--)
					{
						if (base.NestedQueries[i].Key.FilteredPathItem.FilterCondition == null)
						{
							this.m_primaryNestedQueryIndex = new int?(i);
							break;
						}
					}
				}
				if (this.m_primaryNestedQueryIndex == null)
				{
					SqlQueryOnEntityBase sqlQueryOnEntityBase = new SqlQueryOnBaseEntity(base.QueryPlanBuilder);
					for (int j = 0; j < base.KeyExpressions.Count; j++)
					{
						if (base.KeyExpressions[j].NestedQuery != null)
						{
							throw SQEAssert.AssertFalseAndThrow("None of key expression should be processed and assigned to a nested query at this point.", Array.Empty<object>());
						}
					}
					this.m_primaryNestedQueryIndex = new int?(base.NestedQueries.Add(sqlQueryOnEntityBase));
				}
			}
			if (this.PrimaryNestedQuery == null)
			{
				throw SQEAssert.AssertFalseAndThrow("this.PrimaryNestedQuery must not be null at this point.", Array.Empty<object>());
			}
			return this.PrimaryNestedQuery;
		}

		// Token: 0x040001FB RID: 507
		private readonly SqlTotalQuery m_detailsQuery;

		// Token: 0x040001FC RID: 508
		private readonly ExpressionProcessInfoCollection m_selectAsNullExpressions = new ExpressionProcessInfoCollection();

		// Token: 0x040001FD RID: 509
		private readonly ExpressionProcessInfoCollection m_measures = new ExpressionProcessInfoCollection();

		// Token: 0x040001FE RID: 510
		private readonly Dictionary<ExpressionProcessInfo, bool> m_keyExprAggregationFlags = new Dictionary<ExpressionProcessInfo, bool>();

		// Token: 0x040001FF RID: 511
		private TotalAggregationPathItem m_totalAggregationPathItem;

		// Token: 0x04000200 RID: 512
		private int? m_primaryNestedQueryIndex;

		// Token: 0x04000201 RID: 513
		private bool m_selectingBlobs;
	}
}
