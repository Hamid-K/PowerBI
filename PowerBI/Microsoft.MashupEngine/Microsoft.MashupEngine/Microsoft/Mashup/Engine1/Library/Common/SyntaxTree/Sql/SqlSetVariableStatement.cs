using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x0200121C RID: 4636
	internal sealed class SqlSetVariableStatement : SqlStatement
	{
		// Token: 0x06007A92 RID: 31378 RVA: 0x001A747F File Offset: 0x001A567F
		public SqlSetVariableStatement(Alias name, SqlExpression expression)
		{
			this.name = name;
			this.expression = expression;
		}

		// Token: 0x1700218B RID: 8587
		// (get) Token: 0x06007A93 RID: 31379 RVA: 0x001A7495 File Offset: 0x001A5695
		public Alias Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700218C RID: 8588
		// (get) Token: 0x06007A94 RID: 31380 RVA: 0x001A749D File Offset: 0x001A569D
		public SqlExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x06007A95 RID: 31381 RVA: 0x001A74A5 File Offset: 0x001A56A5
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSpaceAfter(SqlLanguageStrings.SetSqlString);
			new VariableReference(this.name).WriteCreateScript(writer);
			writer.WriteSpace();
			writer.WriteSpaceAfter(SqlLanguageSymbols.EqualsSqlString);
			this.expression.WriteCreateScript(writer);
		}

		// Token: 0x040043ED RID: 17389
		private readonly Alias name;

		// Token: 0x040043EE RID: 17390
		private readonly SqlExpression expression;
	}
}
