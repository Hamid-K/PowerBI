using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011C0 RID: 4544
	internal sealed class CastCall : ScalarExpression
	{
		// Token: 0x170020DC RID: 8412
		// (get) Token: 0x06007822 RID: 30754 RVA: 0x0014025A File Offset: 0x0013E45A
		public static int ConvertParameterPrecedence
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170020DD RID: 8413
		// (get) Token: 0x06007823 RID: 30755 RVA: 0x001A0DA3 File Offset: 0x0019EFA3
		// (set) Token: 0x06007824 RID: 30756 RVA: 0x001A0DAB File Offset: 0x0019EFAB
		public SqlExpression Expression { get; set; }

		// Token: 0x170020DE RID: 8414
		// (get) Token: 0x06007825 RID: 30757 RVA: 0x00002105 File Offset: 0x00000305
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170020DF RID: 8415
		// (get) Token: 0x06007826 RID: 30758 RVA: 0x001A0DB4 File Offset: 0x0019EFB4
		// (set) Token: 0x06007827 RID: 30759 RVA: 0x001A0DBC File Offset: 0x0019EFBC
		public SqlDataType Type { get; set; }

		// Token: 0x06007828 RID: 30760 RVA: 0x001A0DC8 File Offset: 0x0019EFC8
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.Write(SqlLanguageStrings.CastSqlString);
			writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
			writer.WriteSubexpression(CastCall.ConvertParameterPrecedence, this.Expression);
			writer.WriteSpaceBeforeAndAfter(SqlLanguageStrings.AsSqlString);
			if (this.Type != null)
			{
				this.Type.WriteCreateScript(writer);
				writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
				return;
			}
			throw new InvalidOperationException(Strings.InvalidArgument);
		}
	}
}
