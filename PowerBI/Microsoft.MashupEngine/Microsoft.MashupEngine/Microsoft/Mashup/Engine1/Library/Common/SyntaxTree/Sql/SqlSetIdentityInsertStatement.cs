using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001228 RID: 4648
	internal sealed class SqlSetIdentityInsertStatement : SqlStatement
	{
		// Token: 0x06007AC9 RID: 31433 RVA: 0x001A7AB1 File Offset: 0x001A5CB1
		public SqlSetIdentityInsertStatement(TableReference table, bool on)
		{
			this.table = table;
			this.on = on;
		}

		// Token: 0x170021A1 RID: 8609
		// (get) Token: 0x06007ACA RID: 31434 RVA: 0x001A7AC7 File Offset: 0x001A5CC7
		public TableReference Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x170021A2 RID: 8610
		// (get) Token: 0x06007ACB RID: 31435 RVA: 0x001A7ACF File Offset: 0x001A5CCF
		public bool On
		{
			get
			{
				return this.on;
			}
		}

		// Token: 0x06007ACC RID: 31436 RVA: 0x001A7AD8 File Offset: 0x001A5CD8
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSpaceAfter(SqlLanguageStrings.SetSqlString);
			writer.WriteSpaceAfter(SqlLanguageStrings.IdentityInsertSqlString);
			this.Table.WriteCreateScript(writer);
			writer.WriteSpace();
			writer.Write(this.on ? SqlLanguageStrings.OnSqlString : SqlLanguageStrings.OffSqlString);
		}

		// Token: 0x04004409 RID: 17417
		private readonly TableReference table;

		// Token: 0x0400440A RID: 17418
		private readonly bool on;
	}
}
