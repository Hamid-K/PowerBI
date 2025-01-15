using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x02000025 RID: 37
	internal sealed class CompiledSql
	{
		// Token: 0x06000177 RID: 375 RVA: 0x00007180 File Offset: 0x00005380
		internal CompiledSql(string sqlBatch, ISelectList selectList, IQPExpressionInfoCollection selectExpressions, IEnumerable<Expression> queryResultExpressions)
		{
			this.m_commandText = sqlBatch;
			Dictionary<string, CompiledSql.ExpressionSchema> dictionary = new Dictionary<string, CompiledSql.ExpressionSchema>();
			for (int i = 0; i < selectExpressions.Count; i++)
			{
				Expression expression = selectExpressions[i].Expression;
				SqlSelectExpression selectExpression = selectList.GetSelectExpression(expression);
				if (selectExpression == null || expression.Name == null || expression.Name.Length == 0)
				{
					throw SQEAssert.AssertFalseAndThrow();
				}
				if (dictionary.ContainsKey(expression.Name))
				{
					throw SQEAssert.AssertFalseAndThrow("Expression \"{0}\" is selected twice.", new object[] { expression.Name });
				}
				dictionary.Add(expression.Name, new CompiledSql.ExpressionSchema(expression, selectExpression));
				SqlSelectExpression aggregationFlagExpression = selectList.GetAggregationFlagExpression(expression);
				if (aggregationFlagExpression != null)
				{
					if (aggregationFlagExpression.Aliases.Length != 1)
					{
						throw SQEAssert.AssertFalseAndThrow();
					}
					this.m_aggregationFlags.Add(expression.Name, aggregationFlagExpression.Aliases[0]);
				}
			}
			foreach (Expression expression2 in queryResultExpressions)
			{
				CompiledSql.ExpressionSchema expressionSchema;
				if (!dictionary.TryGetValue(expression2.Name, out expressionSchema))
				{
					throw SQEAssert.AssertFalseAndThrow("Expression \"{0}\" is not selected.", new object[] { expression2.Name });
				}
				this.m_expressionsSchema.Add(expressionSchema);
				dictionary.Remove(expression2.Name);
			}
			if (dictionary.Count > 0)
			{
				string text = "";
				foreach (string text2 in dictionary.Keys)
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text = text + "\"" + text2 + "\"";
				}
				throw SQEAssert.AssertFalseAndThrow("The following expressions are not selected: {0}.", new object[] { text });
			}
			if (this.m_aggregationFlags.Count > 0)
			{
				SqlSelectExpression aggregationFieldCountExpression = selectList.GetAggregationFieldCountExpression();
				if (aggregationFieldCountExpression == null || aggregationFieldCountExpression.Aliases.Length != 1)
				{
					throw SQEAssert.AssertFalseAndThrow();
				}
				this.m_aggregatioFieldCountColumnName = aggregationFieldCountExpression.Aliases[0];
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000073C4 File Offset: 0x000055C4
		internal IList<CompiledSql.ExpressionSchema> Schema
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_expressionsSchema;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000073CC File Offset: 0x000055CC
		internal Dictionary<string, string> AggregationFlags
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_aggregationFlags;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000073D4 File Offset: 0x000055D4
		internal string AggregationFieldCountColumnName
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_aggregatioFieldCountColumnName;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000073DC File Offset: 0x000055DC
		internal string CommandText
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_commandText;
			}
		}

		// Token: 0x0400006E RID: 110
		private readonly string m_commandText;

		// Token: 0x0400006F RID: 111
		private readonly List<CompiledSql.ExpressionSchema> m_expressionsSchema = new List<CompiledSql.ExpressionSchema>();

		// Token: 0x04000070 RID: 112
		private readonly Dictionary<string, string> m_aggregationFlags = new Dictionary<string, string>();

		// Token: 0x04000071 RID: 113
		private readonly string m_aggregatioFieldCountColumnName;

		// Token: 0x020000B4 RID: 180
		internal sealed class ExpressionSchema
		{
			// Token: 0x060006A8 RID: 1704 RVA: 0x0001AC18 File Offset: 0x00018E18
			internal ExpressionSchema(Expression expression, SqlSelectExpression sqlSelectExpression)
			{
				this.Expression = expression;
				this.ColumnAliases = sqlSelectExpression.Aliases;
			}

			// Token: 0x04000349 RID: 841
			internal readonly Expression Expression;

			// Token: 0x0400034A RID: 842
			internal readonly string[] ColumnAliases;
		}

		// Token: 0x020000B5 RID: 181
		internal sealed class ExpressionSchemaDictionary : Dictionary<string, CompiledSql.ExpressionSchema>
		{
		}
	}
}
