using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Simplification
{
	// Token: 0x02001DA3 RID: 7587
	internal class SimplifyRegex : TTextAlternativeSelector
	{
		// Token: 0x0600FEA5 RID: 65189 RVA: 0x00364E7E File Offset: 0x0036307E
		private SimplifyRegex()
		{
		}

		// Token: 0x17002A6B RID: 10859
		// (get) Token: 0x0600FEA6 RID: 65190 RVA: 0x003666A7 File Offset: 0x003648A7
		private static IReadOnlyDictionary<string, Token[]> Alternatives { get; }

		// Token: 0x0600FEA7 RID: 65191 RVA: 0x003666B0 File Offset: 0x003648B0
		protected override IEnumerable<ProgramNode> GetAlternatives(ProgramNode p)
		{
			r r2;
			if (Language.Build.Node.Is.r(p, out r2))
			{
				RegularExpression r = r2.Value;
				if (r != null)
				{
					return r.Tokens.SelectMany(delegate(Token token, int index)
					{
						AbstractRegexToken abstractRegexToken = token as AbstractRegexToken;
						Token[] array;
						if (abstractRegexToken == null || !SimplifyRegex.Alternatives.TryGetValue(abstractRegexToken.Name, out array))
						{
							return Enumerable.Empty<ProgramNode>();
						}
						return array.Select((Token x) => Language.Build.Node.Rule.r(new RegularExpression(r.Tokens.Select(delegate(Token t, int i)
						{
							if (i != index)
							{
								return t;
							}
							return x;
						}), 0)).Node);
					});
				}
			}
			return null;
		}

		// Token: 0x17002A6C RID: 10860
		// (get) Token: 0x0600FEA8 RID: 65192 RVA: 0x0036670F File Offset: 0x0036490F
		public static SimplifyRegex Instance { get; }

		// Token: 0x0600FEA9 RID: 65193 RVA: 0x00366718 File Offset: 0x00364918
		// Note: this type is marked as 'beforefieldinit'.
		static SimplifyRegex()
		{
			Dictionary<string, Token[]> dictionary = new Dictionary<string, Token[]>();
			Dictionary<string, Token[]> dictionary2 = dictionary;
			string text = "Number";
			Token[] array = new RegexToken[]
			{
				SimplifyRegex.SimpleNumber,
				SimplifyRegex.NumberDotNumber,
				SimplifyRegex.NumberWithComma
			};
			dictionary2[text] = array;
			Dictionary<string, Token[]> dictionary3 = dictionary;
			string text2 = "SignedNumber";
			array = new RegexToken[] { SimplifyRegex.SimpleSignedNumber };
			dictionary3[text2] = array;
			Dictionary<string, Token[]> dictionary4 = dictionary;
			string text3 = "Line Separator";
			array = new RegexToken[] { SimplifyRegex.SimpleLineSeparator };
			dictionary4[text3] = array;
			Dictionary<string, Token[]> dictionary5 = dictionary;
			string text4 = "Date";
			array = new RegexToken[]
			{
				SimplifyRegex.SimpleDateDash,
				SimplifyRegex.SimpleDateDot,
				SimplifyRegex.SimpleDateSlash
			};
			dictionary5[text4] = array;
			Dictionary<string, Token[]> dictionary6 = dictionary;
			string text5 = "Alphanumeric";
			array = new RegexToken[]
			{
				SimplifyRegex.SimpleNumber,
				SimplifyRegex.SimpleWord
			};
			dictionary6[text5] = array;
			Dictionary<string, Token[]> dictionary7 = dictionary;
			string text6 = "Line Separator";
			array = new RegexToken[] { SimplifyRegex.EndOfLine };
			dictionary7[text6] = array;
			dictionary["',' or 'and'"] = new Token[]
			{
				Token.NonDisjunctiveTokens["Comma"],
				SimplifyRegex.SpaceComma,
				SimplifyRegex.WsComma,
				SimplifyRegex.CommaSpace,
				SimplifyRegex.CommaWs
			};
			SimplifyRegex.Alternatives = dictionary;
			SimplifyRegex.Instance = new SimplifyRegex();
		}

		// Token: 0x04005F74 RID: 24436
		private static readonly RegexToken SimpleNumber = (RegexToken)Token.Tokens["Digits"];

		// Token: 0x04005F75 RID: 24437
		private static readonly RegexToken NumberDotNumber = new RegexToken("[0-9]+(\\.[0-9]+)?", "NumberDotNumber", 0, 1.0, null, true, true, null);

		// Token: 0x04005F76 RID: 24438
		private static readonly RegexToken NumberWithComma = new RegexToken("[0-9]+(\\,[0-9]{3})*", "NumberWithComma", 0, 1.0, null, true, true, null);

		// Token: 0x04005F77 RID: 24439
		private static readonly RegexToken SimpleSignedNumber = new RegexToken("-?[0-9]+", "SimpleSignedNumber", 0, 1.0, null, true, true, null);

		// Token: 0x04005F78 RID: 24440
		private static readonly RegexToken SimpleLineSeparator = new RegexToken("(\\r)?\\n", "SimpleLineSeparator", 0, 1.0, null, true, true, null);

		// Token: 0x04005F79 RID: 24441
		private static readonly RegexToken SimpleDateDash = new RegexToken("(\\d?\\d)-(\\d?\\d)-(\\d?\\d?\\d\\d)", "SimpleDateDash", 0, 1.0, null, true, true, null);

		// Token: 0x04005F7A RID: 24442
		private static readonly RegexToken SimpleDateDot = new RegexToken("(\\d?\\d)\\.(\\d?\\d)\\.(\\d?\\d?\\d\\d)", "SimpleDateDot", 0, 1.0, null, true, true, null);

		// Token: 0x04005F7B RID: 24443
		private static readonly RegexToken SimpleDateSlash = new RegexToken("(\\d?\\d)\\/(\\d?\\d)\\/(\\d?\\d?\\d\\d)", "SimpleDateSlash", 0, 1.0, null, true, true, null);

		// Token: 0x04005F7C RID: 24444
		private static readonly RegexToken SimpleWord = (RegexToken)Token.NonDisjunctiveTokens["Alphabet"];

		// Token: 0x04005F7D RID: 24445
		private static readonly StringToken SpaceComma = new StringToken(" ,", 0, 1.0, "SpaceComma", true, true, null);

		// Token: 0x04005F7E RID: 24446
		private static readonly RegexToken WsComma = new RegexToken("(\\p{Zs})+,", "WsComma", 0, 1.0, null, true, true, null);

		// Token: 0x04005F7F RID: 24447
		private static readonly StringToken CommaSpace = new StringToken(", ", 0, 1.0, "CommaSpace", true, true, null);

		// Token: 0x04005F80 RID: 24448
		private static readonly RegexToken CommaWs = new RegexToken(",(\\p{Zs})+", "CommaWs", 0, 1.0, null, true, true, null);

		// Token: 0x04005F81 RID: 24449
		private static readonly RegexToken EndOfLine = new RegexToken("$", "EndOfLine", 0, 1.0, null, true, true, null);
	}
}
