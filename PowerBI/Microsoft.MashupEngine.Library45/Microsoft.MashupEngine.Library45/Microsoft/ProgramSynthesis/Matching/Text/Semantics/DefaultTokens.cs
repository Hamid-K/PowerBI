using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Matching.Text.Semantics
{
	// Token: 0x0200122E RID: 4654
	public static class DefaultTokens
	{
		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x06008C27 RID: 35879 RVA: 0x001D5B12 File Offset: 0x001D3D12
		public static ConstantToken EmptyToken { get; } = new ConstantToken(string.Empty, new double?(0.0));

		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x06008C28 RID: 35880 RVA: 0x001D5B19 File Offset: 0x001D3D19
		public static CharClassToken Space { get; } = new CharClassToken("Space", CharClass.SpecialClass("\\s"), 1U, null);

		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x06008C29 RID: 35881 RVA: 0x001D5B20 File Offset: 0x001D3D20
		public static CharClassToken BinDigit { get; } = new CharClassToken("BinDigit", CharClass.From(new char[] { '0', '1' }), 2U, null);

		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x06008C2A RID: 35882 RVA: 0x001D5B27 File Offset: 0x001D3D27
		public static CharClassToken Digit { get; } = new CharClassToken("Digit", CharClass.Range('0', '9'), 10U, null);

		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x06008C2B RID: 35883 RVA: 0x001D5B2E File Offset: 0x001D3D2E
		public static CharClassToken Lower { get; } = new CharClassToken("Lower", CharClass.Range('a', 'z'), 26U, null);

		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x06008C2C RID: 35884 RVA: 0x001D5B35 File Offset: 0x001D3D35
		public static CharClassToken Upper { get; } = new CharClassToken("Upper", CharClass.Range('A', 'Z'), 26U, null);

		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x06008C2D RID: 35885 RVA: 0x001D5B3C File Offset: 0x001D3D3C
		public static CharClassToken HexDigit { get; } = CharClassToken.FromUnionOf(new CharClassToken[]
		{
			DefaultTokens.Digit,
			new CharClassToken("HexDigitAlpha", CharClass.UnionOf(new CharClass[]
			{
				CharClass.Range('a', 'f'),
				CharClass.Range('A', 'F')
			}), 12U, null)
		}, new double?((double)2), "HexDigit");

		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x06008C2E RID: 35886 RVA: 0x001D5B43 File Offset: 0x001D3D43
		public static CharClassToken Alpha { get; } = CharClassToken.FromUnionOf(new CharClassToken[]
		{
			DefaultTokens.Lower,
			DefaultTokens.Upper
		}, new double?((double)4), "Alpha");

		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x06008C2F RID: 35887 RVA: 0x001D5B4A File Offset: 0x001D3D4A
		public static CharClassToken AlphaSpace { get; } = CharClassToken.FromUnionOf(new CharClassToken[]
		{
			DefaultTokens.Alpha,
			DefaultTokens.Space
		}, null, "AlphaSpace");

		// Token: 0x17001803 RID: 6147
		// (get) Token: 0x06008C30 RID: 35888 RVA: 0x001D5B51 File Offset: 0x001D3D51
		public static CharClassToken AlphaDigit { get; } = CharClassToken.FromUnionOf(new CharClassToken[]
		{
			DefaultTokens.Alpha,
			DefaultTokens.Digit
		}, new double?((double)8), "AlphaDigit");

		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x06008C31 RID: 35889 RVA: 0x001D5B58 File Offset: 0x001D3D58
		public static CharClassToken AlphaDigitSpace { get; } = CharClassToken.FromUnionOf(new CharClassToken[]
		{
			DefaultTokens.AlphaDigit,
			DefaultTokens.Space
		}, new double?((double)2), "AlphaDigitSpace");

		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x06008C32 RID: 35890 RVA: 0x001D5B5F File Offset: 0x001D3D5F
		public static CharClassToken AlphaDash { get; } = CharClassToken.FromUnionOf(new CharClassToken[]
		{
			DefaultTokens.Alpha,
			new CharClassToken("Dash", CharClass.Character('-'), 1U, null)
		}, null, "AlphaDash");

		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x06008C33 RID: 35891 RVA: 0x001D5B66 File Offset: 0x001D3D66
		public static CharClassToken Base64 { get; } = CharClassToken.FromUnionOf(new CharClassToken[]
		{
			DefaultTokens.AlphaDigit,
			new CharClassToken("Base64Symbols", CharClass.From(new char[] { '+', '/', '=' }), 3U, null)
		}, null, "Base64");

		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x06008C34 RID: 35892 RVA: 0x001D5B6D File Offset: 0x001D3D6D
		public static CharClassToken Any { get; } = new CharClassToken("Any", CharClass.Any(), 65536U, new double?(12.0 * DefaultTokens.Base64.Score));

		// Token: 0x17001808 RID: 6152
		// (get) Token: 0x06008C35 RID: 35893 RVA: 0x001D5B74 File Offset: 0x001D3D74
		public static CharClassToken DotDash { get; } = new CharClassToken("DotDash", CharClass.From(new char[] { '.', '-' }), 2U, null);

		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x06008C36 RID: 35894 RVA: 0x001D5B7B File Offset: 0x001D3D7B
		public static CharClassToken Punctuation { get; } = CharClassToken.FromUnionOf(new CharClassToken[]
		{
			DefaultTokens.DotDash,
			new CharClassToken("ExtraPunct", CharClass.From(new char[] { ',', ':', '?', '/' }), 4U, null)
		}, null, "Punctuation");

		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x06008C37 RID: 35895 RVA: 0x001D5B82 File Offset: 0x001D3D82
		public static CharClassToken Symbol { get; } = CharClassToken.FromUnionOf(new CharClassToken[]
		{
			DefaultTokens.Punctuation,
			new CharClassToken("MoreSymbols", CharClass.From("`~!@#$%^&*_=+\\|".ToCharArray()), 15U, null)
		}, null, "Symbol");

		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x06008C38 RID: 35896 RVA: 0x001D5B89 File Offset: 0x001D3D89
		public static CharClassToken AlphaDigitSpaceSymbol { get; } = CharClassToken.FromUnionOf(new CharClassToken[]
		{
			DefaultTokens.AlphaDigit,
			DefaultTokens.Space,
			DefaultTokens.Symbol
		}, new double?((double)2), "AlphaDigitSpaceSymbol");

		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x06008C39 RID: 35897 RVA: 0x001D5B90 File Offset: 0x001D3D90
		public static Token TitleWord { get; } = new ConcatToken("TitleWord", -2.0, new CharClassToken[]
		{
			DefaultTokens.Upper.GetTokenForLength(1U),
			DefaultTokens.Lower
		});

		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x06008C3A RID: 35898 RVA: 0x001D5B97 File Offset: 0x001D3D97
		public static IReadOnlyDictionary<IToken, string> AllTokensPythonNames { get; }

		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x06008C3B RID: 35899 RVA: 0x001D5B9E File Offset: 0x001D3D9E
		public static List<Token> AllTokens { get; }

		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x06008C3C RID: 35900 RVA: 0x001D5BA5 File Offset: 0x001D3DA5
		public static Dictionary<IToken, HashSet<IToken>> NonDisjointTokens
		{
			get
			{
				return DefaultTokens.NonDisjointTokensLazy.Value;
			}
		}

		// Token: 0x06008C3D RID: 35901 RVA: 0x001D5BB4 File Offset: 0x001D3DB4
		private static Dictionary<IToken, HashSet<IToken>> BuildNonDisjointTokenSets()
		{
			Dictionary<IToken, HashSet<IToken>> dictionary = new Dictionary<IToken, HashSet<IToken>>();
			IToken space = DefaultTokens.Space;
			dictionary[space] = new HashSet<IToken>
			{
				DefaultTokens.AlphaSpace,
				DefaultTokens.AlphaDigitSpace,
				DefaultTokens.Any
			};
			IToken binDigit = DefaultTokens.BinDigit;
			dictionary[binDigit] = new HashSet<IToken>
			{
				DefaultTokens.Digit,
				DefaultTokens.HexDigit,
				DefaultTokens.AlphaDigit,
				DefaultTokens.AlphaDigitSpace,
				DefaultTokens.Base64,
				DefaultTokens.Any
			};
			IToken digit = DefaultTokens.Digit;
			dictionary[digit] = new HashSet<IToken>
			{
				DefaultTokens.BinDigit,
				DefaultTokens.HexDigit,
				DefaultTokens.AlphaDigit,
				DefaultTokens.AlphaDigitSpace,
				DefaultTokens.Base64,
				DefaultTokens.Any
			};
			IToken lower = DefaultTokens.Lower;
			dictionary[lower] = new HashSet<IToken>
			{
				DefaultTokens.HexDigit,
				DefaultTokens.Alpha,
				DefaultTokens.AlphaDash,
				DefaultTokens.AlphaDigit,
				DefaultTokens.AlphaSpace,
				DefaultTokens.AlphaDigitSpace,
				DefaultTokens.Base64,
				DefaultTokens.Any
			};
			IToken upper = DefaultTokens.Upper;
			dictionary[upper] = new HashSet<IToken>
			{
				DefaultTokens.HexDigit,
				DefaultTokens.Alpha,
				DefaultTokens.AlphaDash,
				DefaultTokens.AlphaDigit,
				DefaultTokens.AlphaSpace,
				DefaultTokens.AlphaDigitSpace,
				DefaultTokens.Base64,
				DefaultTokens.Any,
				DefaultTokens.TitleWord
			};
			IToken hexDigit = DefaultTokens.HexDigit;
			dictionary[hexDigit] = new HashSet<IToken>
			{
				DefaultTokens.BinDigit,
				DefaultTokens.Digit,
				DefaultTokens.Lower,
				DefaultTokens.Upper,
				DefaultTokens.Alpha,
				DefaultTokens.AlphaDash,
				DefaultTokens.AlphaDigit,
				DefaultTokens.AlphaSpace,
				DefaultTokens.AlphaDigitSpace,
				DefaultTokens.Base64,
				DefaultTokens.Any,
				DefaultTokens.TitleWord
			};
			IToken alpha = DefaultTokens.Alpha;
			dictionary[alpha] = new HashSet<IToken>
			{
				DefaultTokens.Lower,
				DefaultTokens.Upper,
				DefaultTokens.HexDigit,
				DefaultTokens.AlphaDash,
				DefaultTokens.AlphaDigit,
				DefaultTokens.AlphaSpace,
				DefaultTokens.AlphaDigitSpace,
				DefaultTokens.Base64,
				DefaultTokens.Any,
				DefaultTokens.TitleWord
			};
			IToken alphaDash = DefaultTokens.AlphaDash;
			dictionary[alphaDash] = new HashSet<IToken>
			{
				DefaultTokens.Lower,
				DefaultTokens.Upper,
				DefaultTokens.HexDigit,
				DefaultTokens.Alpha,
				DefaultTokens.AlphaSpace,
				DefaultTokens.AlphaDigit,
				DefaultTokens.AlphaDigitSpace,
				DefaultTokens.Base64,
				DefaultTokens.DotDash,
				DefaultTokens.Punctuation,
				DefaultTokens.Symbol,
				DefaultTokens.Any,
				DefaultTokens.TitleWord
			};
			IToken alphaDigit = DefaultTokens.AlphaDigit;
			dictionary[alphaDigit] = new HashSet<IToken>
			{
				DefaultTokens.BinDigit,
				DefaultTokens.Digit,
				DefaultTokens.Lower,
				DefaultTokens.Upper,
				DefaultTokens.HexDigit,
				DefaultTokens.Alpha,
				DefaultTokens.AlphaDash,
				DefaultTokens.AlphaSpace,
				DefaultTokens.AlphaDigitSpace,
				DefaultTokens.Base64,
				DefaultTokens.Any,
				DefaultTokens.TitleWord
			};
			IToken alphaSpace = DefaultTokens.AlphaSpace;
			dictionary[alphaSpace] = new HashSet<IToken>
			{
				DefaultTokens.Space,
				DefaultTokens.Lower,
				DefaultTokens.Upper,
				DefaultTokens.HexDigit,
				DefaultTokens.Alpha,
				DefaultTokens.AlphaDash,
				DefaultTokens.AlphaDigit,
				DefaultTokens.AlphaDigitSpace,
				DefaultTokens.Base64,
				DefaultTokens.Any,
				DefaultTokens.TitleWord
			};
			IToken alphaDigitSpace = DefaultTokens.AlphaDigitSpace;
			dictionary[alphaDigitSpace] = new HashSet<IToken>
			{
				DefaultTokens.Space,
				DefaultTokens.BinDigit,
				DefaultTokens.Digit,
				DefaultTokens.Lower,
				DefaultTokens.Upper,
				DefaultTokens.HexDigit,
				DefaultTokens.Alpha,
				DefaultTokens.AlphaDash,
				DefaultTokens.AlphaDigit,
				DefaultTokens.AlphaSpace,
				DefaultTokens.Base64,
				DefaultTokens.Any,
				DefaultTokens.TitleWord
			};
			IToken @base = DefaultTokens.Base64;
			dictionary[@base] = new HashSet<IToken>
			{
				DefaultTokens.BinDigit,
				DefaultTokens.Digit,
				DefaultTokens.Lower,
				DefaultTokens.Upper,
				DefaultTokens.HexDigit,
				DefaultTokens.Alpha,
				DefaultTokens.AlphaDash,
				DefaultTokens.AlphaDigit,
				DefaultTokens.AlphaSpace,
				DefaultTokens.AlphaDigitSpace,
				DefaultTokens.Symbol,
				DefaultTokens.Punctuation,
				DefaultTokens.Any,
				DefaultTokens.TitleWord
			};
			IToken dotDash = DefaultTokens.DotDash;
			dictionary[dotDash] = new HashSet<IToken>
			{
				DefaultTokens.AlphaDash,
				DefaultTokens.Symbol,
				DefaultTokens.Punctuation,
				DefaultTokens.Any
			};
			IToken punctuation = DefaultTokens.Punctuation;
			dictionary[punctuation] = new HashSet<IToken>
			{
				DefaultTokens.AlphaDash,
				DefaultTokens.DotDash,
				DefaultTokens.Base64,
				DefaultTokens.Symbol,
				DefaultTokens.Any
			};
			IToken symbol = DefaultTokens.Symbol;
			dictionary[symbol] = new HashSet<IToken>
			{
				DefaultTokens.AlphaDash,
				DefaultTokens.DotDash,
				DefaultTokens.Base64,
				DefaultTokens.Punctuation,
				DefaultTokens.Any
			};
			IToken any = DefaultTokens.Any;
			dictionary[any] = new HashSet<IToken>(DefaultTokens.AllTokens);
			IToken titleWord = DefaultTokens.TitleWord;
			dictionary[titleWord] = new HashSet<IToken>
			{
				DefaultTokens.Lower,
				DefaultTokens.Upper,
				DefaultTokens.HexDigit,
				DefaultTokens.Alpha,
				DefaultTokens.AlphaDash,
				DefaultTokens.AlphaDigit,
				DefaultTokens.AlphaSpace,
				DefaultTokens.AlphaDigitSpace,
				DefaultTokens.Base64,
				DefaultTokens.Any
			};
			return dictionary;
		}

		// Token: 0x06008C3E RID: 35902 RVA: 0x001D63B4 File Offset: 0x001D45B4
		public static bool AreDisjoint(IToken token1, IToken token2)
		{
			ConstantToken constantToken = token1 as ConstantToken;
			ConstantToken constantToken2 = token2 as ConstantToken;
			if (constantToken != null && constantToken2 != null)
			{
				return constantToken.Constant != constantToken2.Constant && !constantToken.Constant.StartsWith(constantToken2.Constant) && !constantToken2.Constant.StartsWith(constantToken.Constant);
			}
			CharClassToken charClassToken = token1 as CharClassToken;
			CharClassToken charClassToken2 = token2 as CharClassToken;
			ConstantToken constantToken3 = constantToken ?? constantToken2;
			CharClassToken charClassToken3 = charClassToken ?? charClassToken2;
			if (charClassToken3 != null && constantToken3 != null)
			{
				return charClassToken3.UnrestrictedToken.PrefixMatchLength(constantToken3.Constant) == 0U;
			}
			HashSet<IToken> hashSet;
			return !(charClassToken == null) && !(charClassToken2 == null) && !charClassToken.UnrestrictedToken.Equals(charClassToken2.UnrestrictedToken) && DefaultTokens.NonDisjointTokens.TryGetValue(charClassToken.UnrestrictedToken, out hashSet) && !hashSet.Contains(charClassToken2.UnrestrictedToken);
		}

		// Token: 0x06008C3F RID: 35903 RVA: 0x001D64B8 File Offset: 0x001D46B8
		// Note: this type is marked as 'beforefieldinit'.
		static DefaultTokens()
		{
			Dictionary<IToken, string> dictionary = new Dictionary<IToken, string>();
			IToken space = DefaultTokens.Space;
			dictionary[space] = "whitespace";
			IToken binDigit = DefaultTokens.BinDigit;
			dictionary[binDigit] = "bindigit";
			IToken digit = DefaultTokens.Digit;
			dictionary[digit] = "digit";
			IToken lower = DefaultTokens.Lower;
			dictionary[lower] = "lower";
			IToken upper = DefaultTokens.Upper;
			dictionary[upper] = "upper";
			IToken hexDigit = DefaultTokens.HexDigit;
			dictionary[hexDigit] = "hexdigit";
			IToken alpha = DefaultTokens.Alpha;
			dictionary[alpha] = "alpha";
			IToken alphaDash = DefaultTokens.AlphaDash;
			dictionary[alphaDash] = "alphadash";
			IToken alphaDigit = DefaultTokens.AlphaDigit;
			dictionary[alphaDigit] = "alphanum";
			IToken alphaSpace = DefaultTokens.AlphaSpace;
			dictionary[alphaSpace] = "alphaspace";
			IToken alphaDigitSpace = DefaultTokens.AlphaDigitSpace;
			dictionary[alphaDigitSpace] = "alphadigitspace";
			IToken @base = DefaultTokens.Base64;
			dictionary[@base] = "base64";
			IToken dotDash = DefaultTokens.DotDash;
			dictionary[dotDash] = "dotdash";
			IToken punctuation = DefaultTokens.Punctuation;
			dictionary[punctuation] = "punctuation";
			IToken symbol = DefaultTokens.Symbol;
			dictionary[symbol] = "symbol";
			IToken any = DefaultTokens.Any;
			dictionary[any] = "any";
			IToken titleWord = DefaultTokens.TitleWord;
			dictionary[titleWord] = "titleword";
			DefaultTokens.AllTokensPythonNames = dictionary;
			DefaultTokens.AllTokens = new List<Token>
			{
				DefaultTokens.Space,
				DefaultTokens.BinDigit,
				DefaultTokens.Digit,
				DefaultTokens.Lower,
				DefaultTokens.Upper,
				DefaultTokens.HexDigit,
				DefaultTokens.Alpha,
				DefaultTokens.AlphaDash,
				DefaultTokens.AlphaDigit,
				DefaultTokens.AlphaSpace,
				DefaultTokens.AlphaDigitSpace,
				DefaultTokens.Base64,
				DefaultTokens.DotDash,
				DefaultTokens.Punctuation,
				DefaultTokens.Symbol,
				DefaultTokens.Any,
				DefaultTokens.TitleWord
			};
			DefaultTokens.NonDisjointTokensLazy = new Lazy<Dictionary<IToken, HashSet<IToken>>>(new Func<Dictionary<IToken, HashSet<IToken>>>(DefaultTokens.BuildNonDisjointTokenSets));
		}

		// Token: 0x0400395E RID: 14686
		private static readonly Lazy<Dictionary<IToken, HashSet<IToken>>> NonDisjointTokensLazy;
	}
}
