using System;
using System.Globalization;
using System.Linq;

namespace Microsoft.ProgramSynthesis
{
	// Token: 0x02000081 RID: 129
	public static class StringExtensions
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0000AE50 File Offset: 0x00009050
		public static string ToAnonymizedString(this string source)
		{
			if (source == null)
			{
				return "<null>";
			}
			return new string(source.Select(delegate(char c)
			{
				switch (CharUnicodeInfo.GetUnicodeCategory(c))
				{
				case UnicodeCategory.UppercaseLetter:
					return 'A';
				case UnicodeCategory.LowercaseLetter:
					return 'a';
				case UnicodeCategory.DecimalDigitNumber:
				case UnicodeCategory.LetterNumber:
				case UnicodeCategory.OtherNumber:
					return '9';
				case UnicodeCategory.SpaceSeparator:
				case UnicodeCategory.LineSeparator:
				case UnicodeCategory.ParagraphSeparator:
				case UnicodeCategory.ConnectorPunctuation:
				case UnicodeCategory.DashPunctuation:
				case UnicodeCategory.OpenPunctuation:
				case UnicodeCategory.ClosePunctuation:
				case UnicodeCategory.InitialQuotePunctuation:
				case UnicodeCategory.FinalQuotePunctuation:
				case UnicodeCategory.OtherPunctuation:
				case UnicodeCategory.MathSymbol:
				case UnicodeCategory.CurrencySymbol:
				case UnicodeCategory.ModifierSymbol:
				case UnicodeCategory.OtherSymbol:
					return c;
				}
				bool flag = c == '\n' || c == '\r';
				char c2;
				if (flag)
				{
					c2 = c;
				}
				else
				{
					c2 = 'x';
				}
				return c2;
			}).ToArray<char>()).Replace("\r", "\\r").Replace("\n", "\\n");
		}
	}
}
