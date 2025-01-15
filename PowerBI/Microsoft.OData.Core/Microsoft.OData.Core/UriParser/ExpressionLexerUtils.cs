using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200013E RID: 318
	internal sealed class ExpressionLexerUtils
	{
		// Token: 0x060010AB RID: 4267 RVA: 0x0002EA08 File Offset: 0x0002CC08
		internal static bool IsNumeric(ExpressionTokenKind id)
		{
			return id == ExpressionTokenKind.IntegerLiteral || id == ExpressionTokenKind.DecimalLiteral || id == ExpressionTokenKind.DoubleLiteral || id == ExpressionTokenKind.Int64Literal || id == ExpressionTokenKind.SingleLiteral;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0002EA24 File Offset: 0x0002CC24
		internal static bool IsInfinityOrNaNDouble(string tokenText)
		{
			if (tokenText.Length == 3)
			{
				if (tokenText[0] == "INF"[0])
				{
					return ExpressionLexerUtils.IsInfinityLiteralDouble(tokenText);
				}
				if (tokenText[0] == "NaN"[0])
				{
					return string.CompareOrdinal(tokenText, 0, "NaN", 0, 3) == 0;
				}
			}
			return false;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0002EA7C File Offset: 0x0002CC7C
		internal static bool IsInfinityLiteralDouble(string text)
		{
			return string.CompareOrdinal(text, 0, "INF", 0, text.Length) == 0;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0002EA94 File Offset: 0x0002CC94
		internal static bool IsInfinityOrNanSingle(string tokenText)
		{
			if (tokenText.Length == 4)
			{
				if (tokenText[0] == "INF"[0])
				{
					return ExpressionLexerUtils.IsInfinityLiteralSingle(tokenText);
				}
				if (tokenText[0] == "NaN"[0])
				{
					return (tokenText[3] == 'f' || tokenText[3] == 'F') && string.CompareOrdinal(tokenText, 0, "NaN", 0, 3) == 0;
				}
			}
			return false;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0002EB04 File Offset: 0x0002CD04
		internal static bool IsInfinityLiteralSingle(string text)
		{
			return text.Length == 4 && (text[3] == 'f' || text[3] == 'F') && string.CompareOrdinal(text, 0, "INF", 0, 3) == 0;
		}

		// Token: 0x040007C0 RID: 1984
		private const char SingleSuffixLower = 'f';

		// Token: 0x040007C1 RID: 1985
		private const char SingleSuffixUpper = 'F';
	}
}
