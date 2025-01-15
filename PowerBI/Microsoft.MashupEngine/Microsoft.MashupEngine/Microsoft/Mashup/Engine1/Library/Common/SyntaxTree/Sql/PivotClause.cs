using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011F4 RID: 4596
	internal sealed class PivotClause : RotationClause
	{
		// Token: 0x1700211E RID: 8478
		// (get) Token: 0x0600792C RID: 31020 RVA: 0x001A32BC File Offset: 0x001A14BC
		// (set) Token: 0x0600792D RID: 31021 RVA: 0x001A32C4 File Offset: 0x001A14C4
		public AggregateFunctionCall ValueColumn { get; set; }

		// Token: 0x1700211F RID: 8479
		// (get) Token: 0x0600792E RID: 31022 RVA: 0x001A32CD File Offset: 0x001A14CD
		// (set) Token: 0x0600792F RID: 31023 RVA: 0x001A32D5 File Offset: 0x001A14D5
		public bool RequiresAliasing { get; set; }

		// Token: 0x06007930 RID: 31024 RVA: 0x001A32E0 File Offset: 0x001A14E0
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSpace();
			writer.WriteIdentifier(Alias.PivotSource);
			writer.WriteSpaceBeforeAndAfter(SqlLanguageStrings.PivotSqlString);
			writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
			this.ValueColumn.WriteCreateScript(writer);
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
				if (this.RequiresAliasing)
				{
					writer.WriteLiteral(ConstantType.UnicodeString, alias.Name);
					writer.WriteSpaceBeforeAndAfter(SqlLanguageStrings.AsSqlString);
				}
				writer.WriteIdentifier(alias);
			}
			writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
			writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
		}
	}
}
