using System;
using System.Globalization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000D2 RID: 210
	internal static class ExpressionTextUtils
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x0000BF0C File Offset: 0x0000A10C
		internal static bool IsValidIdentifier(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return false;
			}
			if (!ExpressionTextUtils.IsValidIdentifierStartChar(str[0]))
			{
				return false;
			}
			for (int i = 1; i < str.Length; i++)
			{
				if (!ExpressionTextUtils.IsValidIdentifierChar(str[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0000BF58 File Offset: 0x0000A158
		internal static bool IsValidIdentifierStartChar(char c)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
			return unicodeCategory <= UnicodeCategory.OtherLetter || unicodeCategory == UnicodeCategory.LetterNumber;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0000BF78 File Offset: 0x0000A178
		internal static bool IsValidIdentifierChar(char c)
		{
			switch (char.GetUnicodeCategory(c))
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.SpacingCombiningMark:
			case UnicodeCategory.DecimalDigitNumber:
			case UnicodeCategory.LetterNumber:
			case UnicodeCategory.Format:
			case UnicodeCategory.ConnectorPunctuation:
				return true;
			}
			return false;
		}
	}
}
