using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x0200120A RID: 4618
	internal sealed class SqlDropViewStatement : SqlStatement
	{
		// Token: 0x060079C5 RID: 31173 RVA: 0x001A4BD7 File Offset: 0x001A2DD7
		public SqlDropViewStatement(TableReference view)
		{
			this.view = view;
		}

		// Token: 0x17002153 RID: 8531
		// (get) Token: 0x060079C6 RID: 31174 RVA: 0x001A4BE6 File Offset: 0x001A2DE6
		public TableReference View
		{
			get
			{
				return this.view;
			}
		}

		// Token: 0x060079C7 RID: 31175 RVA: 0x001A4BEE File Offset: 0x001A2DEE
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSpaceAfter(SqlLanguageStrings.DropViewSqlString);
			this.view.WriteCreateScript(writer);
		}

		// Token: 0x04004274 RID: 17012
		private readonly TableReference view;
	}
}
