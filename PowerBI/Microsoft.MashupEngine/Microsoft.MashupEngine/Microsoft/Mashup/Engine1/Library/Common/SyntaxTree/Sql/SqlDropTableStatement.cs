using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001209 RID: 4617
	internal sealed class SqlDropTableStatement : SqlStatement
	{
		// Token: 0x060079C2 RID: 31170 RVA: 0x001A4BA7 File Offset: 0x001A2DA7
		public SqlDropTableStatement(TableReference table)
		{
			this.table = table;
		}

		// Token: 0x17002152 RID: 8530
		// (get) Token: 0x060079C3 RID: 31171 RVA: 0x001A4BB6 File Offset: 0x001A2DB6
		public TableReference Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x060079C4 RID: 31172 RVA: 0x001A4BBE File Offset: 0x001A2DBE
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSpaceAfter(SqlLanguageStrings.DropTableSqlString);
			this.table.WriteCreateScript(writer);
		}

		// Token: 0x04004273 RID: 17011
		private readonly TableReference table;
	}
}
