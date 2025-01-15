using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x0200005A RID: 90
	internal sealed class QueryPlanBuilder
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x00012A00 File Offset: 0x00010C00
		internal QueryPlanBuilder(SemanticQuery smqlQuery, Predicate<DsvColumn> isBlob)
		{
			if (smqlQuery == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("smqlQuery"));
			}
			if (isBlob == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("isBlob"));
			}
			this.m_smqlQuery = new ReducedSemanticQuery(smqlQuery);
			this.m_isBlob = isBlob;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00012A4C File Offset: 0x00010C4C
		internal SqlQueryPlan Build()
		{
			if (this.UseUnionedQueries)
			{
				return this.BuildUnionedQueriesSqlPlan();
			}
			return this.BuildSingleQuerySqlPlan();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00012A64 File Offset: 0x00010C64
		internal int GetNextQueryID()
		{
			int num = this.m_queryIDCounter + 1;
			this.m_queryIDCounter = num;
			return num;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00012A84 File Offset: 0x00010C84
		internal static IList<DsvColumn> GetPrimaryKeyColumns(Binding entityBinding)
		{
			if (entityBinding == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("entityBinding"));
			}
			if (entityBinding is TableBinding)
			{
				DsvTable table = ((TableBinding)entityBinding).GetTable();
				if (table == null || table.PrimaryKey == null || table.PrimaryKey.Count == 0)
				{
					throw SQEAssert.AssertFalseAndThrow("Invalid table binding.", Array.Empty<object>());
				}
				return table.PrimaryKey;
			}
			else
			{
				if (!(entityBinding is ColumnBinding))
				{
					throw SQEAssert.AssertFalseAndThrow("Unknown type of the entityBinding: {0}.", new object[] { entityBinding.GetType().Name });
				}
				DsvColumn column = ((ColumnBinding)entityBinding).GetColumn();
				if (column == null)
				{
					throw SQEAssert.AssertFalseAndThrow("Invalid column binding.", Array.Empty<object>());
				}
				return new DsvColumn[] { column };
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00012B38 File Offset: 0x00010D38
		internal static Type[] GetEntityKeyPartTypes(IQueryEntity entity)
		{
			if (entity.ModelEntity != null && entity.ModelEntity.Binding != null)
			{
				IList<DsvColumn> primaryKeyColumns = QueryPlanBuilder.GetPrimaryKeyColumns(entity.ModelEntity.Binding);
				Type[] array = new Type[primaryKeyColumns.Count];
				for (int i = 0; i < primaryKeyColumns.Count; i++)
				{
					array[i] = primaryKeyColumns[i].DataType;
				}
				return array;
			}
			throw new NotImplementedException("Calculated entities are not implemented in SQL 2005 (entityRef.ModelEntity != null && entityRef.ModelEntity.Binding != null).");
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00012BA3 File Offset: 0x00010DA3
		internal ReducedSemanticQuery SemanticQuery
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_smqlQuery;
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00012BAB File Offset: 0x00010DAB
		internal bool IsBlob(ColumnBinding binding)
		{
			return this.m_isBlob(binding.GetColumn());
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00012BBE File Offset: 0x00010DBE
		private bool UseUnionedQueries
		{
			get
			{
				return this.m_smqlQuery.MeasureGroup != null && this.m_smqlQuery.MeasureGroup.SubtotalSets.Count > 0;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00012BE7 File Offset: 0x00010DE7
		private bool UseTotalQuery
		{
			get
			{
				return this.m_smqlQuery.MeasureGroup != null;
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00012BF8 File Offset: 0x00010DF8
		private SqlQueryPlan BuildUnionedQueriesSqlPlan()
		{
			if (!this.UseTotalQuery)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			if (this.m_smqlQuery.MeasureGroup == null || this.m_smqlQuery.MeasureGroup.SubtotalSets.Count == 0)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			SqlQueryPlan sqlQueryPlan = new SqlQueryPlan();
			SqlTotalQuery sqlTotalQuery = new SqlTotalQuery(this);
			if (new QueryPlanBuilder.QueryConstructionHelper(this.m_smqlQuery, sqlTotalQuery).ConstructSqlQuery() != sqlTotalQuery)
			{
				throw SQEAssert.AssertFalseAndThrow("Details query (SqlTotalQuery) can not be replaced during query construction.", Array.Empty<object>());
			}
			sqlQueryPlan.AddSqlQuery(sqlTotalQuery);
			for (int i = 0; i < this.m_smqlQuery.MeasureGroup.SubtotalSets.Count; i++)
			{
				SqlTotalQuery sqlTotalQuery2 = new SqlTotalQuery(this, sqlTotalQuery);
				QueryPlanBuilder.UnionedQueryInfo unionedQueryInfo = new QueryPlanBuilder.UnionedQueryInfo(this.m_smqlQuery.MeasureGroup.SubtotalSets[i]);
				if (new QueryPlanBuilder.QueryConstructionHelper(this.m_smqlQuery, sqlTotalQuery2, unionedQueryInfo).ConstructSqlQuery() != sqlTotalQuery2)
				{
					throw SQEAssert.AssertFalseAndThrow("Total query (SqlTotalQuery) can not be replaced during query construction.", Array.Empty<object>());
				}
				sqlQueryPlan.AddSqlQuery(sqlTotalQuery2);
			}
			return sqlQueryPlan;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00012CE8 File Offset: 0x00010EE8
		private SqlQueryPlan BuildSingleQuerySqlPlan()
		{
			SqlQuery sqlQuery;
			if (this.UseTotalQuery)
			{
				sqlQuery = new SqlTotalQuery(this);
			}
			else
			{
				sqlQuery = new SqlQueryOnBaseEntity(this);
			}
			sqlQuery = new QueryPlanBuilder.QueryConstructionHelper(this.m_smqlQuery, sqlQuery).ConstructSqlQuery();
			return new SqlQueryPlan(sqlQuery);
		}

		// Token: 0x040001DE RID: 478
		private readonly ReducedSemanticQuery m_smqlQuery;

		// Token: 0x040001DF RID: 479
		private readonly Predicate<DsvColumn> m_isBlob;

		// Token: 0x040001E0 RID: 480
		private int m_queryIDCounter;

		// Token: 0x020000D1 RID: 209
		private sealed class UnionedQueryInfo
		{
			// Token: 0x06000753 RID: 1875 RVA: 0x0001C5FC File Offset: 0x0001A7FC
			internal UnionedQueryInfo(SubtotalSet subtotalSet)
			{
				this.m_includedMeasures = subtotalSet.SubtotalMeasures;
				this.m_includedGroupings = subtotalSet.SubtotalGroupings;
			}

			// Token: 0x06000754 RID: 1876 RVA: 0x0001C61C File Offset: 0x0001A81C
			internal bool IsMeasureIncluded(Expression measure)
			{
				return this.m_includedMeasures.Count == 0 || this.m_includedMeasures.Contains(measure);
			}

			// Token: 0x06000755 RID: 1877 RVA: 0x0001C639 File Offset: 0x0001A839
			internal bool IsGroupingIncluded(Grouping grouping)
			{
				return this.m_includedGroupings.Contains(grouping);
			}

			// Token: 0x040003AE RID: 942
			private readonly SubtotalSet.SubtotalMeasureCollection m_includedMeasures;

			// Token: 0x040003AF RID: 943
			private readonly SubtotalSet.SubtotalGroupingCollection m_includedGroupings;
		}

		// Token: 0x020000D2 RID: 210
		private sealed class QueryConstructionHelper
		{
			// Token: 0x06000756 RID: 1878 RVA: 0x0001C647 File Offset: 0x0001A847
			internal QueryConstructionHelper(ReducedSemanticQuery smqlQuery, SqlQuery sqlQuery)
				: this(smqlQuery, sqlQuery, null)
			{
			}

			// Token: 0x06000757 RID: 1879 RVA: 0x0001C652 File Offset: 0x0001A852
			internal QueryConstructionHelper(ReducedSemanticQuery smqlQuery, SqlQuery sqlQuery, QueryPlanBuilder.UnionedQueryInfo unionedQueryInfo)
			{
				this.m_smqlQuery = smqlQuery;
				this.m_sqlQuery = sqlQuery;
				this.m_unionedQueryInfo = unionedQueryInfo;
			}

			// Token: 0x06000758 RID: 1880 RVA: 0x0001C66F File Offset: 0x0001A86F
			internal SqlQuery ConstructSqlQuery()
			{
				if (this.m_smqlQuery.MeasureGroup != null)
				{
					this.AddMeasures();
				}
				if (this.m_smqlQuery.Hierarchy != null)
				{
					this.AddGroupings();
				}
				return this.m_sqlQuery.BuildQueryPlanSubtree() ?? this.m_sqlQuery;
			}

			// Token: 0x06000759 RID: 1881 RVA: 0x0001C6AC File Offset: 0x0001A8AC
			private void AddMeasures()
			{
				for (int i = 0; i < this.m_smqlQuery.MeasureGroup.Measures.Count; i++)
				{
					Expression expression = this.m_smqlQuery.MeasureGroup.Measures[i];
					if (this.m_unionedQueryInfo == null || this.m_unionedQueryInfo.IsMeasureIncluded(expression))
					{
						this.SqlTotalQuery.SelectMeasure(expression);
					}
					else
					{
						this.SqlTotalQuery.SelectExpressionAsNull(expression, false);
					}
				}
			}

			// Token: 0x0600075A RID: 1882 RVA: 0x0001C724 File Offset: 0x0001A924
			private void AddGroupings()
			{
				for (int i = 0; i < this.m_smqlQuery.Hierarchy.Groupings.Count; i++)
				{
					Grouping grouping = this.m_smqlQuery.Hierarchy.Groupings[i];
					bool flag = this.m_unionedQueryInfo == null || this.m_unionedQueryInfo.IsGroupingIncluded(grouping);
					if (flag)
					{
						this.m_sqlQuery.SelectExpression(ExpressionProcessInfo.CreateForGroupingExpression(grouping, this.m_smqlQuery.BaseEntity), true);
					}
					else
					{
						this.SqlTotalQuery.SelectExpressionAsNull(grouping.Expression, true);
					}
					if (grouping.IsEntityGrouping)
					{
						for (int j = 0; j < grouping.Details.Count; j++)
						{
							Expression expression = grouping.Details[j];
							if (flag)
							{
								this.m_sqlQuery.SelectExpression(ExpressionProcessInfo.CreateForGroupingDetail(grouping, expression, this.m_smqlQuery.BaseEntity), false);
							}
							else
							{
								this.SqlTotalQuery.SelectExpressionAsNull(expression, false);
							}
						}
					}
					else if (grouping.Details.Count != 0)
					{
						throw SQEAssert.AssertFalseAndThrow();
					}
				}
			}

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x0600075B RID: 1883 RVA: 0x0001C82D File Offset: 0x0001AA2D
			private SqlTotalQuery SqlTotalQuery
			{
				[DebuggerStepThrough]
				get
				{
					return (SqlTotalQuery)this.m_sqlQuery;
				}
			}

			// Token: 0x040003B0 RID: 944
			private readonly ReducedSemanticQuery m_smqlQuery;

			// Token: 0x040003B1 RID: 945
			private readonly SqlQuery m_sqlQuery;

			// Token: 0x040003B2 RID: 946
			private readonly QueryPlanBuilder.UnionedQueryInfo m_unionedQueryInfo;
		}
	}
}
