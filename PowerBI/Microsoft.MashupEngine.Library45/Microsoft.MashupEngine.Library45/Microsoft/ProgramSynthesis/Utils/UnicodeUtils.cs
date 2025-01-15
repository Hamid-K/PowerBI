using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200051A RID: 1306
	public static class UnicodeUtils
	{
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x0005678A File Offset: 0x0005498A
		public static Regex MatchedQuotedStringRegex
		{
			get
			{
				return UnicodeUtils.MatchedQuotedStringRegexLazy.Value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x00056796 File Offset: 0x00054996
		public static Regex AnchoredMatchedQuotedStringRegex
		{
			get
			{
				return UnicodeUtils.AnchoredMatchedQuotedStringRegexLazy.Value;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x000567A2 File Offset: 0x000549A2
		public static Regex ParenthesizedStringRegex
		{
			get
			{
				return UnicodeUtils.ParenthesizedStringRegexLazy.Value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x000567AE File Offset: 0x000549AE
		public static Regex AnchoredParenthesizedStringRegex
		{
			get
			{
				return UnicodeUtils.AnchoredParenthesizedStringRegexLazy.Value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x000567BA File Offset: 0x000549BA
		public static HashSet<char> MinusChars
		{
			get
			{
				return UnicodeUtils.MinusCharsLazy.Value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x000567C6 File Offset: 0x000549C6
		public static HashSet<char> PlusChars
		{
			get
			{
				return UnicodeUtils.PlusCharsLazy.Value;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x000567D2 File Offset: 0x000549D2
		public static HashSet<char> SignChars
		{
			get
			{
				return UnicodeUtils.SignCharsLazy.Value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x000567DE File Offset: 0x000549DE
		public static Regex MinusRegex
		{
			get
			{
				return UnicodeUtils.MinusRegexLazy.Value;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x000567EA File Offset: 0x000549EA
		public static Regex AnchoredMinusRegex
		{
			get
			{
				return UnicodeUtils.AnchoredMinusRegexLazy.Value;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x000567F6 File Offset: 0x000549F6
		public static Regex PlusRegex
		{
			get
			{
				return UnicodeUtils.PlusRegexLazy.Value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x00056802 File Offset: 0x00054A02
		public static Regex AnchoredPlusRegex
		{
			get
			{
				return UnicodeUtils.AnchoredPlusRegexLazy.Value;
			}
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x00056810 File Offset: 0x00054A10
		public static bool IsQuotedUnicodeString(this string str, out string leftQuote, out string rightQuote)
		{
			leftQuote = string.Empty;
			rightQuote = string.Empty;
			if (!UnicodeUtils.AnchoredMatchedQuotedStringRegex.IsMatch(str))
			{
				return false;
			}
			if (str.StartsWith("``") && str.EndsWith("''"))
			{
				leftQuote = "``";
				rightQuote = "''";
			}
			else
			{
				leftQuote = str.Substring(0, 1);
				rightQuote = str.Substring(str.Length - 1, 1);
			}
			return true;
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x00056880 File Offset: 0x00054A80
		public static bool IsParenthesizedUnicodeString(this string str, out string leftParenthesis, out string rightParenthesis)
		{
			if (UnicodeUtils.AnchoredParenthesizedStringRegex.IsMatch(str))
			{
				leftParenthesis = str.Substring(0, 1);
				rightParenthesis = str.Substring(str.Length - 1, 1);
				return true;
			}
			leftParenthesis = string.Empty;
			rightParenthesis = string.Empty;
			return false;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x000568BC File Offset: 0x00054ABC
		public static string UnparenthesizeStringIfParenthesized(this string str)
		{
			string text;
			string text2;
			return str.UnparenthesizeStringIfParenthesized(out text, out text2);
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x000568D3 File Offset: 0x00054AD3
		public static string UnparenthesizeStringIfParenthesized(this string str, out string leftParenthesis, out string rightParenthesis)
		{
			if (str.IsParenthesizedUnicodeString(out leftParenthesis, out rightParenthesis))
			{
				return str.Substring(leftParenthesis.Length, str.Length - leftParenthesis.Length - rightParenthesis.Length);
			}
			return str;
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x00056904 File Offset: 0x00054B04
		public static string UnquoteStringIfQuoted(this string str)
		{
			string text;
			string text2;
			return str.UnquoteStringIfQuoted(out text, out text2);
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x0005691B File Offset: 0x00054B1B
		public static string UnquoteStringIfQuoted(this string str, out string leftQuote, out string rightQuote)
		{
			if (!str.IsQuotedUnicodeString(out leftQuote, out rightQuote))
			{
				return str;
			}
			return str.Substring(leftQuote.Length, str.Length - leftQuote.Length - rightQuote.Length);
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x0005694C File Offset: 0x00054B4C
		public static string Normalize(this string value, NormalizationForm normalizationForm = NormalizationForm.FormC)
		{
			return (() => value.Normalize(normalizationForm)).DefaultIfException<string, ArgumentException>();
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x00056971 File Offset: 0x00054B71
		public static string NormalizeAndTrim(this string value, NormalizationForm normalizationForm = NormalizationForm.FormC)
		{
			string text = value.Normalize(normalizationForm);
			if (text == null)
			{
				return null;
			}
			return text.Trim();
		}

		// Token: 0x04000E1F RID: 3615
		private const string MatchedQuotedStringRegexPattern = "(\".*\")|(``.*'')|(`.*')|('.*')|(`.*`)|(\\u00AB.*\\u00BB)|(\\u2018.*\\u2019)|(\\u201C.*\\u201D)|(\\u201B.*\\u201B)|(\\u201F.*\\u201F)|(\\u2039.*\\u203A)|(\\u276E.*\\u276F)";

		// Token: 0x04000E20 RID: 3616
		private static readonly Lazy<Regex> MatchedQuotedStringRegexLazy = new Lazy<Regex>(() => new Regex("(\".*\")|(``.*'')|(`.*')|('.*')|(`.*`)|(\\u00AB.*\\u00BB)|(\\u2018.*\\u2019)|(\\u201C.*\\u201D)|(\\u201B.*\\u201B)|(\\u201F.*\\u201F)|(\\u2039.*\\u203A)|(\\u276E.*\\u276F)", RegexOptions.ExplicitCapture | RegexOptions.Compiled), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04000E21 RID: 3617
		private static readonly Lazy<Regex> AnchoredMatchedQuotedStringRegexLazy = new Lazy<Regex>(() => new Regex(FormattableString.Invariant(FormattableStringFactory.Create("^(?:{0})$", new object[] { "(\".*\")|(``.*'')|(`.*')|('.*')|(`.*`)|(\\u00AB.*\\u00BB)|(\\u2018.*\\u2019)|(\\u201C.*\\u201D)|(\\u201B.*\\u201B)|(\\u201F.*\\u201F)|(\\u2039.*\\u203A)|(\\u276E.*\\u276F)" })), RegexOptions.ExplicitCapture | RegexOptions.Compiled), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04000E22 RID: 3618
		private const string ParenthesizedStringRegexPattern = "(\\u0028.*\\u0029)|(\\u2768.*\\u0029)|(\\u276A.*\\u276B)|(\\u27EE.*\\u27EF)|(\\u2985.*\\u2986)|(\\u2E28.*\\u2E29)|(\\uFE59.*\\uFE5A)|(\\uFF08.*\\uFF09)|(\\uFF5F.*\\uFF60)";

		// Token: 0x04000E23 RID: 3619
		private static readonly Lazy<Regex> ParenthesizedStringRegexLazy = new Lazy<Regex>(() => new Regex("(\\u0028.*\\u0029)|(\\u2768.*\\u0029)|(\\u276A.*\\u276B)|(\\u27EE.*\\u27EF)|(\\u2985.*\\u2986)|(\\u2E28.*\\u2E29)|(\\uFE59.*\\uFE5A)|(\\uFF08.*\\uFF09)|(\\uFF5F.*\\uFF60)", RegexOptions.ExplicitCapture | RegexOptions.Compiled), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04000E24 RID: 3620
		private static readonly Lazy<Regex> AnchoredParenthesizedStringRegexLazy = new Lazy<Regex>(() => new Regex(FormattableString.Invariant(FormattableStringFactory.Create("^(?:{0})$", new object[] { "(\\u0028.*\\u0029)|(\\u2768.*\\u0029)|(\\u276A.*\\u276B)|(\\u27EE.*\\u27EF)|(\\u2985.*\\u2986)|(\\u2E28.*\\u2E29)|(\\uFE59.*\\uFE5A)|(\\uFF08.*\\uFF09)|(\\uFF5F.*\\uFF60)" })), RegexOptions.ExplicitCapture | RegexOptions.Compiled), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04000E25 RID: 3621
		private static readonly Lazy<HashSet<char>> MinusCharsLazy = new Lazy<HashSet<char>>(() => new HashSet<char> { '-', '−', '－' });

		// Token: 0x04000E26 RID: 3622
		private static readonly Lazy<HashSet<char>> PlusCharsLazy = new Lazy<HashSet<char>>(() => new HashSet<char> { '+', '﬩', '＋' });

		// Token: 0x04000E27 RID: 3623
		private static readonly Lazy<HashSet<char>> SignCharsLazy = new Lazy<HashSet<char>>(() => UnicodeUtils.MinusChars.Concat(UnicodeUtils.PlusChars).ConvertToHashSet<char>());

		// Token: 0x04000E28 RID: 3624
		private const string MinusRegexPattern = "\\u002D|\\u2212|\\uFF0D";

		// Token: 0x04000E29 RID: 3625
		private static readonly Lazy<Regex> MinusRegexLazy = new Lazy<Regex>(() => new Regex("\\u002D|\\u2212|\\uFF0D", RegexOptions.Compiled));

		// Token: 0x04000E2A RID: 3626
		private static readonly Lazy<Regex> AnchoredMinusRegexLazy = new Lazy<Regex>(() => new Regex(FormattableString.Invariant(FormattableStringFactory.Create("^(?:{0})$", new object[] { "\\u002D|\\u2212|\\uFF0D" })), RegexOptions.Compiled));

		// Token: 0x04000E2B RID: 3627
		private const string PlusRegexPattern = "\\u002B|\\uFB29|\\uFF0B";

		// Token: 0x04000E2C RID: 3628
		private static readonly Lazy<Regex> PlusRegexLazy = new Lazy<Regex>(() => new Regex("\\u002B|\\uFB29|\\uFF0B", RegexOptions.Compiled));

		// Token: 0x04000E2D RID: 3629
		private static readonly Lazy<Regex> AnchoredPlusRegexLazy = new Lazy<Regex>(() => new Regex(FormattableString.Invariant(FormattableStringFactory.Create("^(?:{0})$", new object[] { "\\u002B|\\uFB29|\\uFF0B" })), RegexOptions.Compiled));
	}
}
