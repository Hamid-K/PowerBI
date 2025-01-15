using System;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;

namespace Microsoft.Mashup.Engine1.Library.Oracle
{
	// Token: 0x02000567 RID: 1383
	internal sealed class OracleExecuteStoredProcedureStatement : SqlStatement
	{
		// Token: 0x06002C2F RID: 11311 RVA: 0x00086B1D File Offset: 0x00084D1D
		public OracleExecuteStoredProcedureStatement(StoredProcedureReference sproc)
		{
			this.sproc = sproc;
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x00086B2C File Offset: 0x00084D2C
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteLine(new ConstantSqlString("BEGIN"));
			this.sproc.WriteIdentifier(writer);
			writer.Write(new ConstantSqlString("("));
			this.sproc.WriteParameters(writer);
			writer.WriteLine(new ConstantSqlString(");"));
			writer.WriteLine(new ConstantSqlString("END;"));
		}

		// Token: 0x0400132D RID: 4909
		private readonly StoredProcedureReference sproc;
	}
}
