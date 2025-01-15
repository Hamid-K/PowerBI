using System;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000029 RID: 41
	internal sealed class ExpressionLexerUtils
	{
		// Token: 0x0600010A RID: 266 RVA: 0x0000529D File Offset: 0x0000349D
		internal static bool IsNumeric(ExpressionTokenKind id)
		{
			return id == ExpressionTokenKind.IntegerLiteral || id == ExpressionTokenKind.DecimalLiteral || id == ExpressionTokenKind.DoubleLiteral || id == ExpressionTokenKind.Int64Literal || id == ExpressionTokenKind.SingleLiteral;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000052B8 File Offset: 0x000034B8
		internal static bool IsInfinityOrNaNDouble(string tokenText)
		{
			if (tokenText.Length == 3)
			{
				if (tokenText.get_Chars(0) == "INF".get_Chars(0))
				{
					return ExpressionLexerUtils.IsInfinityLiteralDouble(tokenText);
				}
				if (tokenText.get_Chars(0) == "NaN".get_Chars(0))
				{
					return string.CompareOrdinal(tokenText, 0, "NaN", 0, 3) == 0;
				}
			}
			return false;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005310 File Offset: 0x00003510
		internal static bool IsInfinityLiteralDouble(string text)
		{
			return string.CompareOrdinal(text, 0, "INF", 0, text.Length) == 0;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005328 File Offset: 0x00003528
		internal static bool IsInfinityOrNanSingle(string tokenText)
		{
			if (tokenText.Length == 4)
			{
				if (tokenText.get_Chars(0) == "INF".get_Chars(0))
				{
					return ExpressionLexerUtils.IsInfinityLiteralSingle(tokenText);
				}
				if (tokenText.get_Chars(0) == "NaN".get_Chars(0))
				{
					return (tokenText.get_Chars(3) == 'f' || tokenText.get_Chars(3) == 'F') && string.CompareOrdinal(tokenText, 0, "NaN", 0, 3) == 0;
				}
			}
			return false;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005398 File Offset: 0x00003598
		internal static bool IsInfinityLiteralSingle(string text)
		{
			return text.Length == 4 && (text.get_Chars(3) == 'f' || text.get_Chars(3) == 'F') && string.CompareOrdinal(text, 0, "INF", 0, 3) == 0;
		}

		// Token: 0x04000055 RID: 85
		private const char SingleSuffixLower = 'f';

		// Token: 0x04000056 RID: 86
		private const char SingleSuffixUpper = 'F';
	}
}
