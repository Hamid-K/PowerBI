using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x0200120B RID: 4619
	internal sealed class SqlExecuteStoredProcedureStatement : SqlStatement
	{
		// Token: 0x060079C8 RID: 31176 RVA: 0x001A4C07 File Offset: 0x001A2E07
		public SqlExecuteStoredProcedureStatement(StoredProcedureReference sproc)
		{
			this.sproc = sproc;
		}

		// Token: 0x060079C9 RID: 31177 RVA: 0x001A4C16 File Offset: 0x001A2E16
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSpaceAfter(SqlLanguageStrings.ExecSqlString);
			this.sproc.WriteCreateScript(writer);
		}

		// Token: 0x04004275 RID: 17013
		private readonly StoredProcedureReference sproc;
	}
}
