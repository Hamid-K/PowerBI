using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x02000035 RID: 53
	internal sealed class SqlSelectUnionedQuery : ISelectList, ISqlSnippet
	{
		// Token: 0x06000242 RID: 578 RVA: 0x0000B038 File Offset: 0x00009238
		internal SqlSelectUnionedQuery()
		{
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000B04C File Offset: 0x0000924C
		void ISqlSnippet.Compile(FormattedStringWriter fsw)
		{
			for (int i = 0; i < this.m_selectStatements.Count; i++)
			{
				if (i > 0)
				{
					fsw.WriteLineIndent();
					fsw.WriteLineIndent("UNION ALL");
				}
				this.m_selectStatements[i].Compile(fsw);
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000B096 File Offset: 0x00009296
		SqlSelectExpression ISelectList.GetSelectExpression(Expression expressionKey)
		{
			if (this.m_selectStatements.Count == 0)
			{
				throw SQEAssert.AssertFalseAndThrow("There are no select statements inside of this query.", Array.Empty<object>());
			}
			return this.m_selectStatements[0].GetSelectExpression(expressionKey);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000B0C7 File Offset: 0x000092C7
		SqlSelectExpression ISelectList.GetSelectExpression(DsvColumn expressionKey)
		{
			if (this.m_selectStatements.Count == 0)
			{
				throw SQEAssert.AssertFalseAndThrow("There are no select statements inside of this query.", Array.Empty<object>());
			}
			return this.m_selectStatements[0].GetSelectExpression(expressionKey);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000B0F8 File Offset: 0x000092F8
		SqlSelectExpression ISelectList.GetAggregationFlagExpression(Expression expressionKey)
		{
			if (this.m_selectStatements.Count == 0)
			{
				throw SQEAssert.AssertFalseAndThrow("There are no select statements inside of this query.", Array.Empty<object>());
			}
			return this.m_selectStatements[0].GetAggregationFlagExpression(expressionKey);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000B129 File Offset: 0x00009329
		SqlSelectExpression ISelectList.GetAggregationFieldCountExpression()
		{
			if (this.m_selectStatements.Count == 0)
			{
				throw SQEAssert.AssertFalseAndThrow("There are no select statements inside of this query.", Array.Empty<object>());
			}
			return this.m_selectStatements[0].GetAggregationFieldCountExpression();
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00004B5D File Offset: 0x00002D5D
		bool ISelectList.UseParensWhenNested
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000B159 File Offset: 0x00009359
		internal void AddSelect(SqlSelectQuery sqlSelect)
		{
			this.m_selectStatements.Add(sqlSelect);
		}

		// Token: 0x040000D5 RID: 213
		private readonly ReadOnlyItemCollectionBase<ISelectList> m_selectStatements = new ReadOnlyItemCollectionBase<ISelectList>();
	}
}
