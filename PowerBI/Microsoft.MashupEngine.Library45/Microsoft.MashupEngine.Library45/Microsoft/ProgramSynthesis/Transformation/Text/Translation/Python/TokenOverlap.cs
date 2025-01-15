using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Python
{
	// Token: 0x02001DCC RID: 7628
	internal class TokenOverlap
	{
		// Token: 0x0600FFE3 RID: 65507 RVA: 0x0036F564 File Offset: 0x0036D764
		internal static bool MayOverlap(Token xToken, Token yToken)
		{
			return TokenOverlap.MayOverlap(xToken.Name, yToken.Name);
		}

		// Token: 0x0600FFE4 RID: 65508 RVA: 0x0036F578 File Offset: 0x0036D778
		internal static bool MayOverlap(string x, string y)
		{
			TokenOverlap.TokenProperties tokenProperties;
			TokenOverlap.TokenProperties tokenProperties2;
			return !TokenOverlap.TokenPropertiesMap.TryGetValue(x, out tokenProperties) || !TokenOverlap.TokenPropertiesMap.TryGetValue(y, out tokenProperties2) || tokenProperties.MayOverlap(tokenProperties2);
		}

		// Token: 0x0600FFE5 RID: 65509 RVA: 0x0036F5AC File Offset: 0x0036D7AC
		internal static bool MaySelfOverlap(Token xToken)
		{
			return TokenOverlap.MaySelfOverlap(xToken.Name);
		}

		// Token: 0x0600FFE6 RID: 65510 RVA: 0x0036F5BC File Offset: 0x0036D7BC
		internal static bool MaySelfOverlap(string x)
		{
			TokenOverlap.TokenProperties tokenProperties;
			return !TokenOverlap.TokenPropertiesMap.TryGetValue(x, out tokenProperties) || tokenProperties.SelfOverlaps;
		}

		// Token: 0x0600FFE8 RID: 65512 RVA: 0x0036F5E0 File Offset: 0x0036D7E0
		// Note: this type is marked as 'beforefieldinit'.
		static TokenOverlap()
		{
			Dictionary<string, TokenOverlap.TokenProperties> dictionary = new Dictionary<string, TokenOverlap.TokenProperties>();
			dictionary["',' or 'and'"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[]
			{
				TokenOverlap.CharacterClass.Alpha,
				TokenOverlap.CharacterClass.WhiteSpace,
				TokenOverlap.CharacterClass.Comma,
				TokenOverlap.CharacterClass.NewLine
			}, null);
			dictionary["Camel Case"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[1], null);
			dictionary["Lowercase word"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[1], null);
			dictionary["ALL CAPS"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[1], null);
			dictionary["Number"] = new TokenOverlap.TokenProperties(true, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Digit }, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Dot });
			dictionary["SignedNumber"] = new TokenOverlap.TokenProperties(true, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Digit }, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Dot });
			dictionary["Digits"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Digit }, null);
			dictionary["NumberDotNumber"] = new TokenOverlap.TokenProperties(true, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Digit }, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Dot });
			dictionary["NumberWithComma"] = new TokenOverlap.TokenProperties(true, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Digit }, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Comma });
			dictionary["SimpleSignedNumber"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Digit }, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers });
			dictionary["SimpleLineSeparator"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.NewLine }, null);
			dictionary["SimpleDateDash"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Digit }, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Hyphen });
			dictionary["SimpleDateDot"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Digit }, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Dot });
			dictionary["SimpleDateSlash"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Digit }, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers });
			dictionary["SpaceComma"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Comma }, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.WhiteSpace });
			dictionary["CommaWs"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.WhiteSpace }, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Comma });
			dictionary["EndOfLine"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Words/dots/hyphens"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[]
			{
				TokenOverlap.CharacterClass.Alpha,
				TokenOverlap.CharacterClass.Dot,
				TokenOverlap.CharacterClass.Hyphen
			}, null);
			dictionary["Alphabet"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[1], null);
			dictionary["Alphanumeric"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[]
			{
				TokenOverlap.CharacterClass.Alpha,
				TokenOverlap.CharacterClass.Digit,
				TokenOverlap.CharacterClass.Hyphen,
				TokenOverlap.CharacterClass.Dot
			}, null);
			dictionary["Alphanum"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[]
			{
				TokenOverlap.CharacterClass.Alpha,
				TokenOverlap.CharacterClass.Digit
			}, null);
			dictionary["WhiteSpace"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.WhiteSpace }, null);
			dictionary["Tab"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.WhiteSpace }, null);
			dictionary["Comma"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Comma }, null);
			dictionary["Dot"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Dot }, null);
			dictionary["Colon"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Semicolon"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Exclamation"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Right Parenthesis"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Left Parenthesis"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Double Quote"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Single Quote"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Forward Slash"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Backward Slash"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Hyphen"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Hyphen }, null);
			dictionary["Star"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Plus"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Underscore"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Equal"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Greater-than"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Left-than"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Less-than"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Right Bracket"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Left Bracket"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Right Brace"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Left Brace"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Bar"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Ampersand"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Hash"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Dollar"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Hat"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["At"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Percentage"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Question Mark"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Tilde"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Back Prime"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["RightArrow"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["LeftArrow"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.AllOthers }, null);
			dictionary["Empty Line"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.NewLine }, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.WhiteSpace });
			dictionary["Line Separator"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.NewLine }, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.WhiteSpace });
			dictionary["Date"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Digit }, new TokenOverlap.CharacterClass[]
			{
				TokenOverlap.CharacterClass.Hyphen,
				TokenOverlap.CharacterClass.Dot,
				TokenOverlap.CharacterClass.AllOthers
			});
			Dictionary<string, TokenOverlap.TokenProperties> dictionary2 = dictionary;
			string text = "Time";
			bool flag = false;
			TokenOverlap.CharacterClass[] array = new TokenOverlap.CharacterClass[2];
			array[0] = TokenOverlap.CharacterClass.Digit;
			dictionary2[text] = new TokenOverlap.TokenProperties(flag, array, new TokenOverlap.CharacterClass[]
			{
				TokenOverlap.CharacterClass.Dot,
				TokenOverlap.CharacterClass.AllOthers
			});
			dictionary["IP"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Digit }, new TokenOverlap.CharacterClass[] { TokenOverlap.CharacterClass.Dot });
			dictionary["Email"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[1], new TokenOverlap.CharacterClass[]
			{
				TokenOverlap.CharacterClass.Digit,
				TokenOverlap.CharacterClass.Dot,
				TokenOverlap.CharacterClass.Hyphen,
				TokenOverlap.CharacterClass.AllOthers
			});
			dictionary["MAC"] = new TokenOverlap.TokenProperties(false, new TokenOverlap.CharacterClass[]
			{
				TokenOverlap.CharacterClass.Alpha,
				TokenOverlap.CharacterClass.Digit
			}, new TokenOverlap.CharacterClass[]
			{
				TokenOverlap.CharacterClass.Hyphen,
				TokenOverlap.CharacterClass.AllOthers
			});
			TokenOverlap.TokenPropertiesMap = dictionary;
		}

		// Token: 0x0400602A RID: 24618
		private static readonly Dictionary<string, TokenOverlap.TokenProperties> TokenPropertiesMap;

		// Token: 0x02001DCD RID: 7629
		internal enum CharacterClass
		{
			// Token: 0x0400602C RID: 24620
			Alpha,
			// Token: 0x0400602D RID: 24621
			Digit,
			// Token: 0x0400602E RID: 24622
			WhiteSpace,
			// Token: 0x0400602F RID: 24623
			Dot,
			// Token: 0x04006030 RID: 24624
			Hyphen,
			// Token: 0x04006031 RID: 24625
			Comma,
			// Token: 0x04006032 RID: 24626
			NewLine,
			// Token: 0x04006033 RID: 24627
			AllOthers
		}

		// Token: 0x02001DCE RID: 7630
		internal class TokenProperties
		{
			// Token: 0x17002A7D RID: 10877
			// (get) Token: 0x0600FFE9 RID: 65513 RVA: 0x0036FDC2 File Offset: 0x0036DFC2
			internal bool SelfOverlaps { get; }

			// Token: 0x0600FFEA RID: 65514 RVA: 0x0036FDCC File Offset: 0x0036DFCC
			internal TokenProperties(bool selfOverlaps, IEnumerable<TokenOverlap.CharacterClass> endChars, IEnumerable<TokenOverlap.CharacterClass> allOtherChars)
			{
				this.SelfOverlaps = selfOverlaps;
				this._endCharacter = endChars.ConvertToHashSet<TokenOverlap.CharacterClass>();
				this._allCharacters = ((allOtherChars != null) ? allOtherChars.ConvertToHashSet<TokenOverlap.CharacterClass>() : null) ?? new HashSet<TokenOverlap.CharacterClass>();
				this._allCharacters.AddRange(this._endCharacter);
			}

			// Token: 0x0600FFEB RID: 65515 RVA: 0x0036FE20 File Offset: 0x0036E020
			internal bool MayOverlap(TokenOverlap.TokenProperties yProperties)
			{
				return this._endCharacter.Any((TokenOverlap.CharacterClass c) => yProperties._allCharacters.Contains(c)) || yProperties._allCharacters.All((TokenOverlap.CharacterClass c) => this._allCharacters.Contains(c)) || this._allCharacters.All((TokenOverlap.CharacterClass c) => yProperties._allCharacters.Contains(c));
			}

			// Token: 0x04006034 RID: 24628
			private readonly HashSet<TokenOverlap.CharacterClass> _allCharacters;

			// Token: 0x04006035 RID: 24629
			private readonly HashSet<TokenOverlap.CharacterClass> _endCharacter;
		}
	}
}
