using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x0200121E RID: 4638
	internal sealed class SqlStatementList : SqlStatement
	{
		// Token: 0x06007A98 RID: 31384 RVA: 0x001A74E0 File Offset: 0x001A56E0
		public SqlStatementList(params SqlStatement[] statements)
			: this(statements)
		{
		}

		// Token: 0x06007A99 RID: 31385 RVA: 0x001A74E9 File Offset: 0x001A56E9
		public SqlStatementList(IList<SqlStatement> statements)
		{
			this.statements = statements;
		}

		// Token: 0x1700218D RID: 8589
		// (get) Token: 0x06007A9A RID: 31386 RVA: 0x001A74F8 File Offset: 0x001A56F8
		public IList<SqlStatement> Statements
		{
			get
			{
				return this.statements;
			}
		}

		// Token: 0x06007A9B RID: 31387 RVA: 0x001A7500 File Offset: 0x001A5700
		public override void WriteCreateScript(ScriptWriter writer)
		{
			bool flag = true;
			foreach (SqlStatement sqlStatement in this.statements)
			{
				if (!flag)
				{
					writer.WriteLine();
				}
				flag = false;
				sqlStatement.WriteCreateScript(writer);
			}
		}

		// Token: 0x040043EF RID: 17391
		private readonly IList<SqlStatement> statements;
	}
}
