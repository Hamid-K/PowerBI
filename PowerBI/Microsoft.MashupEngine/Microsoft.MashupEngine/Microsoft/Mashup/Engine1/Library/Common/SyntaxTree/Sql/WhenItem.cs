using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001227 RID: 4647
	internal sealed class WhenItem : SqlExpression
	{
		// Token: 0x1700219E RID: 8606
		// (get) Token: 0x06007AC0 RID: 31424 RVA: 0x0014025A File Offset: 0x0013E45A
		public override int Precedence
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700219F RID: 8607
		// (get) Token: 0x06007AC1 RID: 31425 RVA: 0x001A79F8 File Offset: 0x001A5BF8
		// (set) Token: 0x06007AC2 RID: 31426 RVA: 0x001A7A00 File Offset: 0x001A5C00
		public SqlExpression Then { get; set; }

		// Token: 0x170021A0 RID: 8608
		// (get) Token: 0x06007AC3 RID: 31427 RVA: 0x001A7A09 File Offset: 0x001A5C09
		// (set) Token: 0x06007AC4 RID: 31428 RVA: 0x001A7A11 File Offset: 0x001A5C11
		public SqlExpression When { get; set; }

		// Token: 0x06007AC5 RID: 31429 RVA: 0x001A7A1A File Offset: 0x001A5C1A
		private void WriteCreateSwitchScript(ScriptWriter writer)
		{
			this.When.WriteCreateScript(writer);
			writer.WriteSpaceAfter(SqlLanguageSymbols.CommaSqlString);
			writer.WriteSubexpression(this.Precedence, this.Then);
		}

		// Token: 0x06007AC6 RID: 31430 RVA: 0x001A7A45 File Offset: 0x001A5C45
		public override void WriteCreateScript(ScriptWriter writer)
		{
			if (writer.Settings.SupportsCaseExpression)
			{
				this.WriteCreateCaseScript(writer);
				return;
			}
			this.WriteCreateSwitchScript(writer);
		}

		// Token: 0x06007AC7 RID: 31431 RVA: 0x001A7A64 File Offset: 0x001A5C64
		private void WriteCreateCaseScript(ScriptWriter writer)
		{
			writer.WriteSpaceAfter(SqlLanguageStrings.WhenSqlString);
			this.When.WriteCreateScript(writer);
			writer.WriteLine();
			writer.WriteSpaceAfter(SqlLanguageStrings.ThenSqlString);
			writer.WriteSubexpression(this.Precedence, this.Then);
			writer.WriteLine();
		}
	}
}
