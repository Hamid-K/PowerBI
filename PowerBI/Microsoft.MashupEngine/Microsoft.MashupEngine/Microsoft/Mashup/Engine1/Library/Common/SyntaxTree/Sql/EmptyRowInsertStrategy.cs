using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011CC RID: 4556
	internal abstract class EmptyRowInsertStrategy
	{
		// Token: 0x0600785A RID: 30810
		public abstract void WriteEmptyColumnList(ScriptWriter writer);

		// Token: 0x0600785B RID: 30811
		public abstract void WriteEmptyValuesClause(ScriptWriter writer);

		// Token: 0x040041A9 RID: 16809
		public static readonly EmptyRowInsertStrategy SqlServer = new EmptyRowInsertStrategy.SqlServerStrategy();

		// Token: 0x040041AA RID: 16810
		public static readonly EmptyRowInsertStrategy Legacy = new EmptyRowInsertStrategy.LegacyStrategy();

		// Token: 0x020011CD RID: 4557
		private sealed class SqlServerStrategy : EmptyRowInsertStrategy
		{
			// Token: 0x0600785E RID: 30814 RVA: 0x001A12F7 File Offset: 0x0019F4F7
			public override void WriteEmptyColumnList(ScriptWriter writer)
			{
				writer.WriteSpace();
			}

			// Token: 0x0600785F RID: 30815 RVA: 0x001A12FF File Offset: 0x0019F4FF
			public override void WriteEmptyValuesClause(ScriptWriter writer)
			{
				writer.Write(SqlLanguageStrings.DefaultValuesSqlString);
				writer.WriteLine();
			}
		}

		// Token: 0x020011CE RID: 4558
		private sealed class LegacyStrategy : EmptyRowInsertStrategy
		{
			// Token: 0x06007861 RID: 30817 RVA: 0x001A131A File Offset: 0x0019F51A
			public override void WriteEmptyColumnList(ScriptWriter writer)
			{
				writer.WriteSpace();
				writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
				writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
			}

			// Token: 0x06007862 RID: 30818 RVA: 0x001A1338 File Offset: 0x0019F538
			public override void WriteEmptyValuesClause(ScriptWriter writer)
			{
				writer.Write(SqlLanguageStrings.ValuesSqlString);
				writer.WriteLine();
				writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
				writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
			}
		}
	}
}
