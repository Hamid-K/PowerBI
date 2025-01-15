using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x0200121F RID: 4639
	internal sealed class SqlUpdateStatement : SqlStatement
	{
		// Token: 0x06007A9C RID: 31388 RVA: 0x001A7558 File Offset: 0x001A5758
		public SqlUpdateStatement(TableReference table, List<SqlColumnUpdate> updates, OutputClause outputClause, Condition whereClause = null)
		{
			this.table = table;
			this.updates = updates;
			this.whereClause = whereClause;
			this.outputClause = outputClause;
		}

		// Token: 0x1700218E RID: 8590
		// (get) Token: 0x06007A9D RID: 31389 RVA: 0x001A757D File Offset: 0x001A577D
		public TableReference Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x1700218F RID: 8591
		// (get) Token: 0x06007A9E RID: 31390 RVA: 0x001A7585 File Offset: 0x001A5785
		public Condition WhereClause
		{
			get
			{
				return this.whereClause;
			}
		}

		// Token: 0x17002190 RID: 8592
		// (get) Token: 0x06007A9F RID: 31391 RVA: 0x001A758D File Offset: 0x001A578D
		public IList<SqlColumnUpdate> Updates
		{
			get
			{
				return this.updates;
			}
		}

		// Token: 0x17002191 RID: 8593
		// (get) Token: 0x06007AA0 RID: 31392 RVA: 0x001A7595 File Offset: 0x001A5795
		public OutputClause OutputClause
		{
			get
			{
				return this.outputClause;
			}
		}

		// Token: 0x06007AA1 RID: 31393 RVA: 0x001A75A0 File Offset: 0x001A57A0
		public override void WriteCreateScript(ScriptWriter writer)
		{
			this.outputClause.WritePrefixScript(writer);
			writer.WriteSpaceAfter(SqlLanguageStrings.UpdateSqlString);
			this.table.WriteCreateScript(writer);
			writer.WriteSpace();
			writer.WriteSpaceAfter(SqlLanguageStrings.SetSqlString);
			bool flag = false;
			foreach (SqlColumnUpdate sqlColumnUpdate in this.updates)
			{
				flag = writer.WriteCommaIfNeeded(flag);
				sqlColumnUpdate.WriteCreateScript(writer);
			}
			this.outputClause.WriteCreateScript(writer);
			if (this.whereClause != null)
			{
				writer.WriteLine();
				writer.WriteSpaceAfter(SqlLanguageStrings.WhereSqlString);
				this.whereClause.WriteCreateScript(writer);
			}
			this.outputClause.WriteSuffixScript(writer);
		}

		// Token: 0x040043F0 RID: 17392
		private readonly TableReference table;

		// Token: 0x040043F1 RID: 17393
		private readonly List<SqlColumnUpdate> updates;

		// Token: 0x040043F2 RID: 17394
		private readonly Condition whereClause;

		// Token: 0x040043F3 RID: 17395
		private readonly OutputClause outputClause;
	}
}
