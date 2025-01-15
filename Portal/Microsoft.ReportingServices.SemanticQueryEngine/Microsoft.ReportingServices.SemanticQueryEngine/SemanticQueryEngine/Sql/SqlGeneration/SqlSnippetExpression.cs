using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x0200002D RID: 45
	internal sealed class SqlSnippetExpression : SqlExpression
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x000080A9 File Offset: 0x000062A9
		internal SqlSnippetExpression(ISqlSnippet sqlSnippet, bool nullable)
			: base(nullable)
		{
			if (sqlSnippet is SqlExpression)
			{
				throw SQEAssert.AssertFalseAndThrow("sqlSnippet can not be a SqlExpression in this context.", Array.Empty<object>());
			}
			this.m_sqlSnippet = sqlSnippet;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00004555 File Offset: 0x00002755
		internal override bool CanGroupBy
		{
			[DebuggerStepThrough]
			get
			{
				return false;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000080D1 File Offset: 0x000062D1
		protected override void InitValues()
		{
			base.Values.Add(this.m_sqlSnippet);
		}

		// Token: 0x040000C4 RID: 196
		private readonly ISqlSnippet m_sqlSnippet;
	}
}
