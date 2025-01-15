using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Matching.Text.Semantics
{
	// Token: 0x02001234 RID: 4660
	public static class TokenUtils
	{
		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x06008C64 RID: 35940 RVA: 0x001D6EA6 File Offset: 0x001D50A6
		public static string Separator { get; } = " & ";

		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x06008C65 RID: 35941 RVA: 0x001D6EAD File Offset: 0x001D50AD
		public static string Null { get; } = "<Null>";

		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x06008C66 RID: 35942 RVA: 0x001D6EB4 File Offset: 0x001D50B4
		public static string Empty { get; } = "<Empty>";

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x06008C67 RID: 35943 RVA: 0x001D6EBB File Offset: 0x001D50BB
		public static string UnknownDescription { get; } = "<Unknown>";

		// Token: 0x06008C68 RID: 35944 RVA: 0x001D6EC4 File Offset: 0x001D50C4
		public static string Description(this MatchingLabel matchingLabel)
		{
			switch (matchingLabel.Match)
			{
			case MatchingLabel.MatchType.NullMatch:
				return TokenUtils.Null;
			case MatchingLabel.MatchType.NoMatch:
				return TokenUtils.UnknownDescription;
			case MatchingLabel.MatchType.TokenSequenceMatch:
				if (matchingLabel.GetTokens().IsEmpty<IToken>())
				{
					return TokenUtils.Empty;
				}
				return string.Join(TokenUtils.Separator, from token in matchingLabel.GetTokens()
					select token.Description);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06008C69 RID: 35945 RVA: 0x001D6F48 File Offset: 0x001D5148
		internal static string BaseRegexDescriptionFor(IReadOnlyList<IToken> pattern)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("^");
			for (int i = 0; i < pattern.Count; i++)
			{
				CharClassToken charClassToken = pattern[i] as CharClassToken;
				if (charClassToken == null)
				{
					string text = pattern[i].TryGetRegexPattern();
					if (text == null)
					{
						return null;
					}
					stringBuilder.Append(text);
				}
				else
				{
					bool flag = i == pattern.Count - 1 || DefaultTokens.AreDisjoint(pattern[i], pattern[i + 1]);
					stringBuilder.Append(flag ? charClassToken.GetNonGreedyRegex() : charClassToken.TryGetRegexPattern());
				}
			}
			stringBuilder.Append("$");
			return stringBuilder.ToString();
		}

		// Token: 0x06008C6A RID: 35946 RVA: 0x001D6FF4 File Offset: 0x001D51F4
		private static IReadOnlyList<IToken> Flatten(this IEnumerable<IToken> pattern)
		{
			return pattern.SelectMany(delegate(IToken token)
			{
				ConcatToken concatToken = token as ConcatToken;
				if (concatToken == null)
				{
					return token.Yield<IToken>();
				}
				return concatToken.SubTokens;
			}).ToList<IToken>();
		}

		// Token: 0x06008C6B RID: 35947 RVA: 0x001D7020 File Offset: 0x001D5220
		public static IReadOnlyDictionary<IReadOnlyList<IToken>, RegexProfile> RegexDescriptions(this IReadOnlyList<IReadOnlyList<IToken>> patterns)
		{
			Func<IReadOnlyList<IToken>, IReadOnlyList<IToken>> func;
			if ((func = TokenUtils.<>O.<0>__Flatten) == null)
			{
				func = (TokenUtils.<>O.<0>__Flatten = new Func<IReadOnlyList<IToken>, IReadOnlyList<IToken>>(TokenUtils.Flatten));
			}
			List<IReadOnlyList<IToken>> list = patterns.Select(func).ToList<IReadOnlyList<IToken>>();
			Dictionary<IReadOnlyList<IToken>, RegexProfile> dictionary = new Dictionary<IReadOnlyList<IToken>, RegexProfile>();
			IEnumerable<IReadOnlyList<IToken>> enumerable = list;
			Func<IReadOnlyList<IToken>, string> func2;
			if ((func2 = TokenUtils.<>O.<1>__BaseRegexDescriptionFor) == null)
			{
				func2 = (TokenUtils.<>O.<1>__BaseRegexDescriptionFor = new Func<IReadOnlyList<IToken>, string>(TokenUtils.BaseRegexDescriptionFor));
			}
			List<string> list2 = enumerable.Select(func2).ToList<string>();
			for (int i = 0; i < list.Count; i++)
			{
				IReadOnlyList<IToken> currentPattern = list[i];
				string text = list2[i];
				List<string> list3 = (from otherPattern in list.ZipWith(list2).Take(i)
					where !TokenUtils.IsDisjoint(currentPattern, otherPattern.Item1).Equals(true.Some<bool>())
					select otherPattern.Item2).ToList<string>();
				dictionary[patterns[i]] = new RegexProfile(text, list3);
			}
			return dictionary;
		}

		// Token: 0x06008C6C RID: 35948 RVA: 0x001D7118 File Offset: 0x001D5318
		private static Optional<bool> IsDisjoint(IReadOnlyList<IToken> tokens1, IReadOnlyList<IToken> tokens2)
		{
			IToken token = null;
			IToken token2 = null;
			int num = 0;
			int num2 = 0;
			while ((token != null || num < tokens1.Count) && (token2 != null || num2 < tokens2.Count))
			{
				IToken token3;
				if ((token3 = token) == null)
				{
					token3 = tokens1[num++];
				}
				token = token3;
				IToken token4;
				if ((token4 = token2) == null)
				{
					token4 = tokens2[num2++];
				}
				token2 = token4;
				MatchResult matchResult;
				if (token.Equals(token2))
				{
					matchResult = MatchResult.MatchedWithNoneRemaining();
				}
				else if (DefaultTokens.AreDisjoint(token, token2))
				{
					matchResult = MatchResult.DidNotMatch();
				}
				else
				{
					ConstantToken constantToken = token as ConstantToken;
					if (constantToken != null)
					{
						ConstantToken constantToken2 = token2 as ConstantToken;
						if (constantToken2 != null)
						{
							matchResult = TokenUtils.MatchConstants(constantToken, constantToken2);
							goto IL_0133;
						}
					}
					CharClassToken charClassToken = token as CharClassToken;
					if (charClassToken != null)
					{
						CharClassToken charClassToken2 = token2 as CharClassToken;
						if (charClassToken2 != null)
						{
							matchResult = TokenUtils.MatchClasses(charClassToken, charClassToken2);
							goto IL_0133;
						}
					}
					CharClassToken charClassToken3 = token as CharClassToken;
					if (charClassToken3 != null)
					{
						ConstantToken constantToken3 = token2 as ConstantToken;
						if (constantToken3 != null)
						{
							IToken token5 = ((num2 < tokens2.Count) ? tokens2[num2] : null);
							matchResult = TokenUtils.MatchClassAndConstant(charClassToken3, constantToken3, token5);
							goto IL_0133;
						}
					}
					ConstantToken constantToken4 = token as ConstantToken;
					if (constantToken4 != null)
					{
						CharClassToken charClassToken4 = token2 as CharClassToken;
						if (charClassToken4 != null)
						{
							IToken token6 = ((num < tokens1.Count) ? tokens1[num] : null);
							matchResult = TokenUtils.MatchClassAndConstant(charClassToken4, constantToken4, token6).Invert();
							goto IL_0133;
						}
					}
					matchResult = MatchResult.Unknown(token, token2);
				}
				IL_0133:
				if (!matchResult.IsDisjoint.HasValue)
				{
					break;
				}
				if (matchResult.IsDisjoint.Value)
				{
					return true.Some<bool>();
				}
				token = matchResult.LeftRemaining;
				token2 = matchResult.RightRemaining;
			}
			List<IToken> list;
			if (token != null)
			{
				(list = new List<IToken>()).Add(token);
			}
			else
			{
				list = new List<IToken>();
			}
			List<IToken> list2 = list;
			list2.AddRange(tokens1.Skip(num));
			List<IToken> list3;
			if (token2 != null)
			{
				(list3 = new List<IToken>()).Add(token2);
			}
			else
			{
				list3 = new List<IToken>();
			}
			List<IToken> list4 = list3;
			list4.AddRange(tokens2.Skip(num2));
			return TokenUtils.IsDisjointHeuristics(list2, list4);
		}

		// Token: 0x06008C6D RID: 35949 RVA: 0x001D7304 File Offset: 0x001D5504
		private static Optional<bool> IsDisjointHeuristics(IReadOnlyList<IToken> leftTokens, IReadOnlyList<IToken> rightTokens)
		{
			Func<IToken, bool> func;
			if ((func = TokenUtils.<>O.<2>__AlwaysHasPositiveLength) == null)
			{
				func = (TokenUtils.<>O.<2>__AlwaysHasPositiveLength = new Func<IToken, bool>(TokenUtils.AlwaysHasPositiveLength));
			}
			if (leftTokens.Any(func))
			{
				Func<IToken, bool> func2;
				if ((func2 = TokenUtils.<>O.<3>__AlwaysHasZeroLength) == null)
				{
					func2 = (TokenUtils.<>O.<3>__AlwaysHasZeroLength = new Func<IToken, bool>(TokenUtils.AlwaysHasZeroLength));
				}
				if (rightTokens.All(func2))
				{
					return true.Some<bool>();
				}
			}
			Func<IToken, bool> func3;
			if ((func3 = TokenUtils.<>O.<2>__AlwaysHasPositiveLength) == null)
			{
				func3 = (TokenUtils.<>O.<2>__AlwaysHasPositiveLength = new Func<IToken, bool>(TokenUtils.AlwaysHasPositiveLength));
			}
			if (rightTokens.Any(func3))
			{
				Func<IToken, bool> func4;
				if ((func4 = TokenUtils.<>O.<3>__AlwaysHasZeroLength) == null)
				{
					func4 = (TokenUtils.<>O.<3>__AlwaysHasZeroLength = new Func<IToken, bool>(TokenUtils.AlwaysHasZeroLength));
				}
				if (leftTokens.All(func4))
				{
					return true.Some<bool>();
				}
			}
			if (leftTokens.Any((IToken t) => !(t is CharClassToken) && !(t is ConstantToken)))
			{
				return Optional<bool>.Nothing;
			}
			if (rightTokens.Any((IToken t) => !(t is CharClassToken) && !(t is ConstantToken)))
			{
				return Optional<bool>.Nothing;
			}
			HashSet<char> leftChars = leftTokens.OfType<ConstantToken>().SelectMany((ConstantToken c) => c.Constant).ConvertToHashSet<char>();
			HashSet<char> rightChars = rightTokens.OfType<ConstantToken>().SelectMany((ConstantToken c) => c.Constant).ConvertToHashSet<char>();
			IEnumerable<char> enumerable = leftChars.Except(rightChars);
			IEnumerable<char> enumerable2 = rightChars.Except(leftChars);
			HashSet<CharClassToken> leftClasses = (from c in leftTokens.OfType<CharClassToken>()
				select c.UnrestrictedToken).ConvertToHashSet<CharClassToken>();
			HashSet<CharClassToken> rightClasses = (from c in rightTokens.OfType<CharClassToken>()
				select c.UnrestrictedToken).ConvertToHashSet<CharClassToken>();
			if (enumerable.Any((char ch) => TokenUtils.<IsDisjointHeuristics>g__IsDisjointFromClasses|17_2(ch, rightClasses)))
			{
				return true.Some<bool>();
			}
			if (enumerable2.Any((char ch) => TokenUtils.<IsDisjointHeuristics>g__IsDisjointFromClasses|17_2(ch, leftClasses)))
			{
				return true.Some<bool>();
			}
			if (leftClasses.Any((CharClassToken cl) => TokenUtils.<IsDisjointHeuristics>g__IsDisjointFromCharsAndClasses|17_3(cl, rightChars, rightClasses)))
			{
				return true.Some<bool>();
			}
			if (rightClasses.Any((CharClassToken cl) => TokenUtils.<IsDisjointHeuristics>g__IsDisjointFromCharsAndClasses|17_3(cl, leftChars, leftClasses)))
			{
				return true.Some<bool>();
			}
			return Optional<bool>.Nothing;
		}

		// Token: 0x06008C6E RID: 35950 RVA: 0x001D757C File Offset: 0x001D577C
		private static MatchResult MatchConstants(ConstantToken left, ConstantToken right)
		{
			if (left.Constant.StartsWith(right.Constant))
			{
				return MatchResult.MatchedWithLeftRemaining(new ConstantToken(left.Constant.Substring(right.Length), null));
			}
			if (right.Constant.StartsWith(left.Constant))
			{
				return MatchResult.MatchedWithRightRemaining(new ConstantToken(right.Constant.Substring(left.Length), null));
			}
			return MatchResult.DidNotMatch();
		}

		// Token: 0x06008C6F RID: 35951 RVA: 0x001D7600 File Offset: 0x001D5800
		private static MatchResult MatchClasses(CharClassToken left, CharClassToken right)
		{
			if (left.UnrestrictedToken != right.UnrestrictedToken)
			{
				return MatchResult.Unknown(left, right);
			}
			if (left.RequiredLength != null && right.RequiredLength != null)
			{
				uint? requiredLength = left.RequiredLength;
				uint? requiredLength2 = right.RequiredLength;
				if (!((requiredLength.GetValueOrDefault() == requiredLength2.GetValueOrDefault()) & (requiredLength != null == (requiredLength2 != null))))
				{
					return MatchResult.DidNotMatch();
				}
			}
			return MatchResult.MatchedWithNoneRemaining();
		}

		// Token: 0x06008C70 RID: 35952 RVA: 0x001D7684 File Offset: 0x001D5884
		private static MatchResult MatchClassAndConstant(CharClassToken charClass, ConstantToken constant, IToken tokenAfterConstant)
		{
			if (constant.Length == 0)
			{
				return MatchResult.MatchedWithLeftRemaining(charClass);
			}
			CharClassToken unrestrictedToken = charClass.UnrestrictedToken;
			uint num = unrestrictedToken.PrefixMatchLength(constant.Constant);
			uint? requiredLength = charClass.RequiredLength;
			if (num == 0U)
			{
				return MatchResult.DidNotMatch();
			}
			if ((ulong)num < (ulong)((long)constant.Length))
			{
				if (requiredLength != null)
				{
					uint? num2 = requiredLength;
					uint num3 = num;
					if (!((num2.GetValueOrDefault() == num3) & (num2 != null)))
					{
						return MatchResult.DidNotMatch();
					}
				}
				return MatchResult.MatchedWithRightRemaining(new ConstantToken(constant.Constant.Substring((int)num), null));
			}
			if (requiredLength != null)
			{
				uint? num2 = requiredLength;
				uint num3 = num;
				if ((num2.GetValueOrDefault() < num3) & (num2 != null))
				{
					return MatchResult.DidNotMatch();
				}
			}
			if (requiredLength != null)
			{
				uint? num2 = requiredLength;
				uint num3 = num;
				if ((num2.GetValueOrDefault() > num3) & (num2 != null))
				{
					return MatchResult.MatchedWithLeftRemaining(unrestrictedToken.GetTokenForLength(requiredLength.Value - num));
				}
			}
			if (requiredLength != null)
			{
				uint? num2 = requiredLength;
				uint num3 = num;
				if (!((num2.GetValueOrDefault() == num3) & (num2 != null)))
				{
					goto IL_011E;
				}
			}
			if (tokenAfterConstant == null || DefaultTokens.AreDisjoint(tokenAfterConstant, unrestrictedToken))
			{
				return MatchResult.MatchedWithNoneRemaining();
			}
			IL_011E:
			return MatchResult.Unknown(charClass, constant);
		}

		// Token: 0x06008C71 RID: 35953 RVA: 0x001D77B8 File Offset: 0x001D59B8
		private static bool AlwaysHasPositiveLength(IToken token)
		{
			ConstantToken constantToken = token as ConstantToken;
			if (constantToken != null)
			{
				return constantToken.Length > 0;
			}
			if (!(token is RegexToken))
			{
				throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Unknown token type: {0}", new object[] { token.GetType() })));
			}
			return true;
		}

		// Token: 0x06008C72 RID: 35954 RVA: 0x001D7808 File Offset: 0x001D5A08
		private static bool AlwaysHasZeroLength(IToken token)
		{
			ConstantToken constantToken = token as ConstantToken;
			if (constantToken != null)
			{
				return constantToken.Length == 0;
			}
			if (!(token is RegexToken))
			{
				throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Unknown token type: {0}", new object[] { token.GetType() })));
			}
			return false;
		}

		// Token: 0x06008C74 RID: 35956 RVA: 0x001D7884 File Offset: 0x001D5A84
		[CompilerGenerated]
		internal static bool <IsDisjointHeuristics>g__IsDisjointFromClasses|17_2(char ch, IEnumerable<CharClassToken> classes)
		{
			return classes.All((CharClassToken cl) => !cl.Contains(ch));
		}

		// Token: 0x06008C75 RID: 35957 RVA: 0x001D78B0 File Offset: 0x001D5AB0
		[CompilerGenerated]
		internal static bool <IsDisjointHeuristics>g__IsDisjointFromCharsAndClasses|17_3(CharClassToken cl, IEnumerable<char> chars, IEnumerable<CharClassToken> otherClasses)
		{
			return chars.All((char ch) => !cl.Contains(ch)) && otherClasses.All((CharClassToken other) => DefaultTokens.AreDisjoint(cl, other));
		}

		// Token: 0x02001235 RID: 4661
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400396D RID: 14701
			public static Func<IReadOnlyList<IToken>, IReadOnlyList<IToken>> <0>__Flatten;

			// Token: 0x0400396E RID: 14702
			public static Func<IReadOnlyList<IToken>, string> <1>__BaseRegexDescriptionFor;

			// Token: 0x0400396F RID: 14703
			public static Func<IToken, bool> <2>__AlwaysHasPositiveLength;

			// Token: 0x04003970 RID: 14704
			public static Func<IToken, bool> <3>__AlwaysHasZeroLength;
		}
	}
}
