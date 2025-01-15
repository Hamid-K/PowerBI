using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000FF RID: 255
	internal sealed class ExpressionLexerUtils
	{
		// Token: 0x06000C21 RID: 3105 RVA: 0x000213CC File Offset: 0x0001F5CC
		internal static bool IsNumeric(ExpressionTokenKind id)
		{
			return id == ExpressionTokenKind.IntegerLiteral || id == ExpressionTokenKind.DecimalLiteral || id == ExpressionTokenKind.DoubleLiteral || id == ExpressionTokenKind.Int64Literal || id == ExpressionTokenKind.SingleLiteral;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x000213E8 File Offset: 0x0001F5E8
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

		// Token: 0x06000C23 RID: 3107 RVA: 0x00021440 File Offset: 0x0001F640
		internal static bool IsInfinityLiteralDouble(string text)
		{
			return string.CompareOrdinal(text, 0, "INF", 0, text.Length) == 0;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00021458 File Offset: 0x0001F658
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

		// Token: 0x06000C25 RID: 3109 RVA: 0x000214C8 File Offset: 0x0001F6C8
		internal static bool IsInfinityLiteralSingle(string text)
		{
			return text.Length == 4 && (text.get_Chars(3) == 'f' || text.get_Chars(3) == 'F') && string.CompareOrdinal(text, 0, "INF", 0, 3) == 0;
		}

		// Token: 0x040006A8 RID: 1704
		private const char SingleSuffixLower = 'f';

		// Token: 0x040006A9 RID: 1705
		private const char SingleSuffixUpper = 'F';
	}
}
