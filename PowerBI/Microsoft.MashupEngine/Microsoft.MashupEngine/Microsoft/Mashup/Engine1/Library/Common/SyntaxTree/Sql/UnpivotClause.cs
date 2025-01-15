using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011F5 RID: 4597
	internal sealed class UnpivotClause : RotationClause
	{
		// Token: 0x17002120 RID: 8480
		// (get) Token: 0x06007932 RID: 31026 RVA: 0x001A33E0 File Offset: 0x001A15E0
		// (set) Token: 0x06007933 RID: 31027 RVA: 0x001A33E8 File Offset: 0x001A15E8
		public Alias ValueColumn { get; set; }

		// Token: 0x06007934 RID: 31028 RVA: 0x001A33F4 File Offset: 0x001A15F4
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSpace();
			writer.WriteIdentifier(Alias.PivotSource);
			writer.WriteSpaceBeforeAndAfter(SqlLanguageStrings.UnpivotSqlString);
			writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
			writer.WriteIdentifier(this.ValueColumn);
			writer.WriteSpaceBeforeAndAfter(SqlLanguageStrings.ForSqlString);
			writer.WriteIdentifier(base.AttributeColumn);
			writer.WriteSpaceBeforeAndAfter(SqlLanguageStrings.InSqlString);
			writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
			bool flag = false;
			foreach (Alias alias in base.PivotValues)
			{
				if (flag)
				{
					writer.WriteSpaceAfter(SqlLanguageSymbols.CommaSqlString);
				}
				flag = true;
				writer.WriteIdentifier(alias);
			}
			writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
			writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
		}
	}
}
