using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x02000028 RID: 40
	internal sealed class SqlSelectExpression : ISqlSnippet
	{
		// Token: 0x0600019A RID: 410 RVA: 0x000077A0 File Offset: 0x000059A0
		internal SqlSelectExpression(SqlExpression sqlExpression, string[] aliases, SqlBatch sqlBatch)
		{
			if (sqlExpression.Values.Count != aliases.Length)
			{
				throw SQEAssert.AssertFalseAndThrow("Number of aliases does not match number of values in the specified sql expression.", Array.Empty<object>());
			}
			if (sqlBatch == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("sqlBatch"));
			}
			this.m_sqlExpression = sqlExpression;
			this.m_aliases = aliases;
			this.m_sqlBatch = sqlBatch;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000077FC File Offset: 0x000059FC
		void ISqlSnippet.Compile(FormattedStringWriter fsw)
		{
			if (this.m_sqlExpression.Values.Count != this.m_aliases.Length || this.m_sqlExpression.Values.Count == 0)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			for (int i = 0; i < this.m_aliases.Length; i++)
			{
				if (i > 0)
				{
					fsw.Write(", ");
				}
				this.m_sqlExpression.Values[i].Compile(fsw);
				fsw.Write(" {0}", new object[] { this.m_sqlBatch.GetDelimitedIdentifier(this.m_aliases[i]) });
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000789B File Offset: 0x00005A9B
		internal SqlExpression SqlExpression
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_sqlExpression;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000078A3 File Offset: 0x00005AA3
		internal string[] Aliases
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_aliases;
			}
		}

		// Token: 0x04000079 RID: 121
		private readonly SqlExpression m_sqlExpression;

		// Token: 0x0400007A RID: 122
		private readonly string[] m_aliases;

		// Token: 0x0400007B RID: 123
		private readonly SqlBatch m_sqlBatch;
	}
}
