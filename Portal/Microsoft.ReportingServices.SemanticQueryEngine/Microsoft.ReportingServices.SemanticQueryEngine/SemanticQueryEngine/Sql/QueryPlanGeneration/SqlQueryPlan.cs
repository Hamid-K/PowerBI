using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000068 RID: 104
	internal sealed class SqlQueryPlan
	{
		// Token: 0x060004BA RID: 1210 RVA: 0x0001491B File Offset: 0x00012B1B
		internal SqlQueryPlan()
		{
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001492E File Offset: 0x00012B2E
		internal SqlQueryPlan(SqlQuery sqlQuery)
		{
			this.AddSqlQuery(sqlQuery);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00014948 File Offset: 0x00012B48
		internal void AddSqlQuery(SqlQuery sqlQuery)
		{
			this.m_sqlQueries.Add(sqlQuery);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00014958 File Offset: 0x00012B58
		internal CompiledSql CompileSql(SqlBatch sqlBatch, IEnumerable<Expression> queryResultExpressions)
		{
			if (this.m_sqlQueries.Count <= 0)
			{
				throw SQEAssert.AssertFalseAndThrow("Query plan has no queries. Sql batch can not be generated.", Array.Empty<object>());
			}
			ISelectList selectList;
			if (this.m_sqlQueries.Count == 1)
			{
				SqlQuery sqlQuery = this.m_sqlQueries[0];
				sqlBatch.TopLevelSelectExpressions = sqlQuery.SelectExpressions;
				selectList = sqlQuery.BuildSql(sqlBatch, true);
			}
			else
			{
				sqlBatch.TopLevelSelectExpressions = this.m_sqlQueries[0].SelectExpressions;
				SqlSelectUnionedQuery sqlSelectUnionedQuery = sqlBatch.CreateSelectUnionedQuery();
				for (int i = 0; i < this.m_sqlQueries.Count; i++)
				{
					sqlSelectUnionedQuery.AddSelect(this.m_sqlQueries[i].BuildSql(sqlBatch, i == this.m_sqlQueries.Count - 1));
				}
				selectList = sqlSelectUnionedQuery;
			}
			sqlBatch.Statements.Add(selectList);
			return sqlBatch.Compile(selectList, queryResultExpressions);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00014A28 File Offset: 0x00012C28
		internal string Trace()
		{
			FormattedStringWriter formattedStringWriter = new FormattedStringWriter(CultureInfo.InvariantCulture);
			formattedStringWriter.IndentWriteLine("SqlQueryPlan trace v1.2");
			for (int i = 0; i < this.m_sqlQueries.Count; i++)
			{
				if (i > 0)
				{
					formattedStringWriter.IndentWriteLine("UNION ALL");
				}
				FormattedStringWriter formattedStringWriter2 = formattedStringWriter;
				int num = formattedStringWriter2.IndentationLevel + 1;
				formattedStringWriter2.IndentationLevel = num;
				this.m_sqlQueries[i].Trace(formattedStringWriter);
				FormattedStringWriter formattedStringWriter3 = formattedStringWriter;
				num = formattedStringWriter3.IndentationLevel - 1;
				formattedStringWriter3.IndentationLevel = num;
			}
			return formattedStringWriter.ToString();
		}

		// Token: 0x040001FA RID: 506
		private readonly SqlQueryCollection m_sqlQueries = new SqlQueryCollection();
	}
}
