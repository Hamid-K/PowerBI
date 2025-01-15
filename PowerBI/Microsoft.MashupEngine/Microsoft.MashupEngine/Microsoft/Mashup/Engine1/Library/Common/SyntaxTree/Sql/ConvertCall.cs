using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011C9 RID: 4553
	internal sealed class ConvertCall : ScalarExpression
	{
		// Token: 0x170020EB RID: 8427
		// (get) Token: 0x06007848 RID: 30792 RVA: 0x0014025A File Offset: 0x0013E45A
		public static int ConvertParameterPrecedence
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170020EC RID: 8428
		// (get) Token: 0x06007849 RID: 30793 RVA: 0x001A109A File Offset: 0x0019F29A
		// (set) Token: 0x0600784A RID: 30794 RVA: 0x001A10A2 File Offset: 0x0019F2A2
		public SqlExpression Expression { get; set; }

		// Token: 0x170020ED RID: 8429
		// (get) Token: 0x0600784B RID: 30795 RVA: 0x00002105 File Offset: 0x00000305
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170020EE RID: 8430
		// (get) Token: 0x0600784C RID: 30796 RVA: 0x001A10AB File Offset: 0x0019F2AB
		// (set) Token: 0x0600784D RID: 30797 RVA: 0x001A10B3 File Offset: 0x0019F2B3
		public int? Style { get; set; }

		// Token: 0x170020EF RID: 8431
		// (get) Token: 0x0600784E RID: 30798 RVA: 0x001A10BC File Offset: 0x0019F2BC
		// (set) Token: 0x0600784F RID: 30799 RVA: 0x001A10C4 File Offset: 0x0019F2C4
		public SqlDataType Type { get; set; }

		// Token: 0x06007850 RID: 30800 RVA: 0x001A10D0 File Offset: 0x0019F2D0
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.Write(SqlLanguageStrings.ConvertSqlString);
			writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
			this.Type.WriteCreateScript(writer);
			writer.WriteSpaceAfter(SqlLanguageSymbols.CommaSqlString);
			writer.WriteSubexpression(ConvertCall.ConvertParameterPrecedence, this.Expression);
			if (this.Style != null)
			{
				writer.WriteSpaceAfter(SqlLanguageSymbols.CommaSqlString);
				writer.WriteInt(this.Style.Value);
			}
			writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
		}
	}
}
