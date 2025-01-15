using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011FF RID: 4607
	internal sealed class SqlColumnUpdate
	{
		// Token: 0x0600797B RID: 31099 RVA: 0x001A3F28 File Offset: 0x001A2128
		public SqlColumnUpdate(ColumnReference column, SqlExpression expression)
		{
			this.column = column;
			this.expression = expression;
		}

		// Token: 0x17002129 RID: 8489
		// (get) Token: 0x0600797C RID: 31100 RVA: 0x001A3F3E File Offset: 0x001A213E
		public ColumnReference Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x1700212A RID: 8490
		// (get) Token: 0x0600797D RID: 31101 RVA: 0x001A3F46 File Offset: 0x001A2146
		public SqlExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x0600797E RID: 31102 RVA: 0x001A3F4E File Offset: 0x001A214E
		public void WriteCreateScript(ScriptWriter writer)
		{
			this.column.WriteCreateScript(writer);
			writer.WriteSpace();
			writer.WriteSpaceAfter(SqlLanguageSymbols.EqualsSqlString);
			this.expression.WriteCreateScript(writer);
		}

		// Token: 0x0400422D RID: 16941
		private readonly ColumnReference column;

		// Token: 0x0400422E RID: 16942
		private readonly SqlExpression expression;
	}
}
