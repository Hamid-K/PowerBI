using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001E3 RID: 483
	internal sealed class ExpressionLexerUtils
	{
		// Token: 0x060011B5 RID: 4533 RVA: 0x0003FDB4 File Offset: 0x0003DFB4
		internal static bool IsNumeric(ExpressionTokenKind id)
		{
			return id == ExpressionTokenKind.IntegerLiteral || id == ExpressionTokenKind.DecimalLiteral || id == ExpressionTokenKind.DoubleLiteral || id == ExpressionTokenKind.Int64Literal || id == ExpressionTokenKind.SingleLiteral;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0003FDD0 File Offset: 0x0003DFD0
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

		// Token: 0x060011B7 RID: 4535 RVA: 0x0003FE28 File Offset: 0x0003E028
		internal static bool IsInfinityLiteralDouble(string text)
		{
			return string.CompareOrdinal(text, 0, "INF", 0, text.Length) == 0;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0003FE40 File Offset: 0x0003E040
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

		// Token: 0x060011B9 RID: 4537 RVA: 0x0003FEB0 File Offset: 0x0003E0B0
		internal static bool IsInfinityLiteralSingle(string text)
		{
			return text.Length == 4 && (text.get_Chars(3) == 'f' || text.get_Chars(3) == 'F') && string.CompareOrdinal(text, 0, "INF", 0, 3) == 0;
		}

		// Token: 0x040007A1 RID: 1953
		private const char SingleSuffixLower = 'f';

		// Token: 0x040007A2 RID: 1954
		private const char SingleSuffixUpper = 'F';
	}
}
