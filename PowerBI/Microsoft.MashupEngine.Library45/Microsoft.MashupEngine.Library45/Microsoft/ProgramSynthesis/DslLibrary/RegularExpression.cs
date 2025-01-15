using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x020007F3 RID: 2035
	[Parseable("TryParseFromXML", ParseHumanReadableString = "TryParseHumanReadable")]
	public class RegularExpression : IRenderableLiteral
	{
		// Token: 0x06002B61 RID: 11105 RVA: 0x00079308 File Offset: 0x00077508
		public RegularExpression(IEnumerable<Token> tokens, int exampleCount = 0)
		{
			this.Tokens = tokens.ToArray<Token>();
			this.ExampleCount = exampleCount;
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x00079323 File Offset: 0x00077523
		public RegularExpression(int exampleCount = 0)
		{
			this.Tokens = new Token[0];
			this.ExampleCount = exampleCount;
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x0007933E File Offset: 0x0007753E
		public Token[] Tokens { get; }

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x00079346 File Offset: 0x00077546
		public int Count
		{
			get
			{
				return this.Tokens.Length;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06002B65 RID: 11109 RVA: 0x00079350 File Offset: 0x00077550
		// (set) Token: 0x06002B66 RID: 11110 RVA: 0x00079358 File Offset: 0x00077558
		public int ExampleCount { get; private set; }

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x00079364 File Offset: 0x00077564
		public int Score
		{
			get
			{
				switch (this.Count)
				{
				case 0:
					return 500;
				case 1:
					return 3 * this.Tokens[0].Score;
				case 2:
					return this.Tokens.Sum((Token t) => t.Score);
				case 3:
					return this.Tokens.Sum((Token t) => t.Score) / 2;
				default:
					return this.Tokens.Sum((Token t) => t.Score) / this.Count;
				}
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06002B68 RID: 11112 RVA: 0x00079430 File Offset: 0x00077630
		public Regex Regex
		{
			get
			{
				if (this._regex != null)
				{
					return this._regex;
				}
				string text = string.Join(string.Empty, (from AbstractRegexToken t in this.Tokens
					select t.Regex.ToString()).ToArray<string>());
				this._regex = new Regex(text, RegexOptions.ExplicitCapture | RegexOptions.Compiled);
				return this._regex;
			}
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x000794A0 File Offset: 0x000776A0
		public string RenderHumanReadable()
		{
			string text = this.ToString().Replace("\\", "\\\\").Replace("\"", "\\\"");
			return FormattableString.Invariant(FormattableStringFactory.Create("\"{0}\"", new object[] { text }));
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x000794EC File Offset: 0x000776EC
		public XElement RenderXML()
		{
			XElement xelement = new XElement("RegularExpression");
			for (int i = 0; i < this.Count; i++)
			{
				xelement.Add(this.Tokens[i].ToXml());
			}
			return xelement;
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x00079530 File Offset: 0x00077730
		public PositionMatch[] Run(LearningCacheSubstring s)
		{
			if (s == null)
			{
				return new PositionMatch[0];
			}
			if (this.Count == 0)
			{
				return (from i in Enumerable.Range((int)s.Start, (int)(s.End - s.Start + 1U))
					select new PositionMatch((uint)i, 0U)).ToArray<PositionMatch>();
			}
			CachedList cachedList;
			if (!s.Cache.TryGetMatchPositionsFor(this.Tokens[0], out cachedList))
			{
				return new PositionMatch[0];
			}
			List<PositionMatch> list = new List<PositionMatch>();
			Record<int, int>? values = cachedList.GetValues(s.Start, s.End);
			if (values == null)
			{
				return list.ToArray();
			}
			for (int j = values.Value.Item1; j <= values.Value.Item2; j++)
			{
				PositionMatch positionMatch = cachedList[j];
				uint num = positionMatch.Right;
				int num2 = 1;
				PositionMatch positionMatch2;
				while (num2 < this.Count && num <= s.End && s.Cache.TryGetTokenMatchStartingAt(num, this.Tokens[num2], out positionMatch2))
				{
					num += positionMatch2.Length;
					num2++;
				}
				if (num2 == this.Count && num <= s.End)
				{
					list.Add(new PositionMatch(positionMatch.Position, num - positionMatch.Position));
				}
			}
			return list.ToArray();
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x0007968F File Offset: 0x0007788F
		public bool MatchesAt(LearningCacheSubstring s, uint position)
		{
			return this.MatchesAtRecursive(s, position, 0U);
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x0007969C File Offset: 0x0007789C
		private bool MatchesAtRecursive(LearningCacheSubstring s, uint position, uint startingToken)
		{
			if (s.Start > position || position > s.End)
			{
				return false;
			}
			if ((long)this.Count == (long)((ulong)startingToken))
			{
				return true;
			}
			Token token = this.Tokens[(int)startingToken];
			PositionMatch positionMatch;
			return s.Cache.TryGetTokenMatchStartingAt(position, token, out positionMatch) && this.MatchesAtRecursive(s, positionMatch.Length + position, startingToken + 1U);
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x000796F8 File Offset: 0x000778F8
		public bool LeftMatchesAt(LearningCacheSubstring s, uint position)
		{
			return this.LeftMatchesAtRecursive(s, position, 0U);
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x00079704 File Offset: 0x00077904
		private bool LeftMatchesAtRecursive(LearningCacheSubstring s, uint position, uint startingToken)
		{
			if (s.Start > position || position > s.End)
			{
				return false;
			}
			if ((long)this.Count == (long)((ulong)startingToken))
			{
				return true;
			}
			Token token;
			checked
			{
				token = this.Tokens[(int)((IntPtr)(unchecked((long)this.Count - (long)((ulong)startingToken) - 1L)))];
			}
			PositionMatch positionMatch;
			return s.Cache.TryGetTokenMatchEndingAt(position, token, out positionMatch) && this.LeftMatchesAtRecursive(s, position - positionMatch.Length, startingToken + 1U);
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x00079770 File Offset: 0x00077970
		public override bool Equals(object other)
		{
			if (!(other is RegularExpression))
			{
				return false;
			}
			RegularExpression regularExpression = (RegularExpression)other;
			return this.Equals(regularExpression);
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x00079798 File Offset: 0x00077998
		private bool Equals(RegularExpression other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (this.Tokens.Length != other.Tokens.Length)
			{
				return false;
			}
			for (int i = 0; i < this.Tokens.Length; i++)
			{
				if (!this.Tokens[i].Equals(other.Tokens[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000797F4 File Offset: 0x000779F4
		public override string ToString()
		{
			if (this.Count != 0)
			{
				return string.Join("◦", this.Tokens.Select((Token t) => t.ToString()));
			}
			return "ε";
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x00079843 File Offset: 0x00077A43
		public override int GetHashCode()
		{
			return this.Tokens.Aggregate(19, (int current, Token token) => current * 31 + token.GetHashCode());
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x00079874 File Offset: 0x00077A74
		public static Optional<RegularExpression> TryParseFromXML(XElement literal, DeserializationContext context)
		{
			Optional<RegularExpression> optional;
			try
			{
				if (literal.Name != "RegularExpression")
				{
					optional = Optional<RegularExpression>.Nothing;
				}
				else
				{
					optional = RegularExpression.Create((from x in literal.Elements()
						select Token.TryParse(x, context)).ToArray<Token>(), 0).Some<RegularExpression>();
				}
			}
			catch
			{
				optional = Optional<RegularExpression>.Nothing;
			}
			return optional;
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000798F4 File Offset: 0x00077AF4
		internal static RegularExpression TryParseHumanReadable(string literal)
		{
			if (literal[0] != '"' || literal.Last<char>() != '"')
			{
				return null;
			}
			literal = literal.Slice(new int?(1), new int?(literal.Length - 1), 1);
			string[] array = literal.Split(new char[] { '◦' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Replace("\\\"", "\"").Replace("\\\\", "\\");
			}
			if (array.Length == 0 || (array.Length == 1 && array[0] == "ε"))
			{
				return new RegularExpression(0);
			}
			IEnumerable<string> enumerable = array;
			Func<string, Optional<Token>> func;
			if ((func = RegularExpression.<>O.<0>__TryParse) == null)
			{
				func = (RegularExpression.<>O.<0>__TryParse = new Func<string, Optional<Token>>(Token.TryParse));
			}
			return (from seq in enumerable.Select(func).WholeSequenceOfValues<Token>()
				select RegularExpression.Create(seq, 0)).OrElse(null);
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000799EC File Offset: 0x00077BEC
		public string[] ToRegexJsonArray()
		{
			return (from AbstractRegexToken t in this.Tokens
				select t.Regex.ToString()).ToArray<string>();
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x00079A24 File Offset: 0x00077C24
		public static RegularExpression Create(IEnumerable<Token> tokens, int exampleCount = 0)
		{
			Token[] array = tokens.ToArray<Token>();
			switch (array.Length)
			{
			case 0:
				return RegularExpression.Create(exampleCount);
			case 1:
				return RegularExpression.Create(array[0], exampleCount);
			case 2:
				return RegularExpression.Create(array[0], array[1], exampleCount);
			case 3:
				return RegularExpression.Create(array[0], array[1], array[2], exampleCount);
			default:
				return new RegularExpression(array, exampleCount);
			}
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x00079A89 File Offset: 0x00077C89
		public static IEnumerable<RegularExpression> LearnFullMatches(LearningCacheSubstring s, int maxTokenCount, int exampleCount = 0)
		{
			return RegularExpression.LearnRightMatches(s, s.Start, maxTokenCount, true, exampleCount);
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x00079A9A File Offset: 0x00077C9A
		public static IEnumerable<RegularExpression> LearnRightMatches(LearningCacheSubstring s, uint pos, int maxTokenCount, int exampleCount = 0)
		{
			return RegularExpression.LearnRightMatches(s, pos, maxTokenCount, false, exampleCount);
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x00079AA8 File Offset: 0x00077CA8
		public static IEnumerable<RegularExpression> LearnRightMatches(IEnumerable<Record<LearningCacheSubstring, uint>> positivePosInSubstrings, IEnumerable<Record<LearningCacheSubstring, uint>> negativePosInSubstrings, int maxTokenCount, int exampleCount = 0)
		{
			HashSet<RegularExpression> hashSet = negativePosInSubstrings.SelectMany((Record<LearningCacheSubstring, uint> posInSub) => RegularExpression.LearnRightMatches(posInSub.Item1, posInSub.Item2, maxTokenCount, exampleCount)).ConvertToHashSet<RegularExpression>();
			HashSet<RegularExpression> hashSet2 = null;
			foreach (Record<LearningCacheSubstring, uint> record in positivePosInSubstrings)
			{
				IEnumerable<RegularExpression> enumerable = RegularExpression.LearnRightMatches(record.Item1, record.Item2, maxTokenCount, exampleCount);
				if (hashSet2 == null)
				{
					hashSet2 = enumerable.ConvertToHashSet<RegularExpression>();
				}
				else
				{
					hashSet2.IntersectWith(enumerable);
				}
				hashSet2.ExceptWith(hashSet);
			}
			IEnumerable<RegularExpression> enumerable2 = hashSet2;
			return enumerable2 ?? Enumerable.Empty<RegularExpression>();
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x00079B64 File Offset: 0x00077D64
		private static IEnumerable<RegularExpression> LearnRightMatches(LearningCacheSubstring s, uint pos, int maxTokenCount, bool fullMatch, int exampleCount = 0)
		{
			if (pos < s.Start || pos > s.End || maxTokenCount < 1)
			{
				return Enumerable.Empty<RegularExpression>();
			}
			List<RegularExpression> list3 = new List<RegularExpression>();
			if (!fullMatch)
			{
				list3.Add(RegularExpression.Create(exampleCount));
			}
			UnboundedCache<Token, TokenMatch> unboundedCache;
			if (!s.Cache.TryGetAllMatchesStartingAt(pos, out unboundedCache) || unboundedCache == null)
			{
				return list3;
			}
			Func<List<Token>, RegularExpression> <>9__0;
			foreach (KeyValuePair<Token, TokenMatch> keyValuePair in unboundedCache.Mappings)
			{
				Token token;
				TokenMatch tokenMatch;
				keyValuePair.Deconstruct(out token, out tokenMatch);
				Token token2 = token;
				TokenMatch tokenMatch2 = tokenMatch;
				if (pos != 0U || exampleCount >= 2 || !(token2.Name == "Line Separator"))
				{
					IEnumerable<List<Token>> enumerable = RegularExpression.LearnRightMatchAt(tokenMatch2, s, pos, maxTokenCount, fullMatch);
					List<RegularExpression> list2 = list3;
					IEnumerable<List<Token>> enumerable2 = enumerable;
					Func<List<Token>, RegularExpression> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (List<Token> list) => new RegularExpression(list, exampleCount));
					}
					list2.AddRange(enumerable2.Select(func));
				}
			}
			return list3;
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x00079C70 File Offset: 0x00077E70
		private static List<List<Token>> LearnRightMatchAt(TokenMatch match, LearningCacheSubstring s, uint pos, int maxTokenCount, bool fullMatch)
		{
			uint num = pos + match.Length;
			List<List<Token>> list = new List<List<Token>>();
			List<List<Token>> list2 = new List<List<Token>>
			{
				new List<Token>()
			};
			if (maxTokenCount == 1)
			{
				if (fullMatch && num != s.End)
				{
					return list;
				}
				if (!fullMatch && num > s.End)
				{
					return list2;
				}
				return new List<List<Token>>
				{
					new List<Token> { match.Token }
				};
			}
			else if (num > s.End)
			{
				if (!fullMatch)
				{
					return list2;
				}
				return list;
			}
			else
			{
				if (num == s.End)
				{
					return new List<List<Token>>
					{
						new List<Token> { match.Token }
					};
				}
				List<List<Token>> list3 = new List<List<Token>>();
				UnboundedCache<Token, TokenMatch> unboundedCache;
				if (s.Cache.TryGetAllMatchesStartingAt(num, out unboundedCache))
				{
					foreach (TokenMatch tokenMatch in unboundedCache.Values)
					{
						list3.AddRange(RegularExpression.LearnRightMatchAt(tokenMatch, s, num, maxTokenCount - 1, fullMatch));
					}
				}
				foreach (List<Token> list4 in list3)
				{
					list4.Insert(0, match.Token);
				}
				if (!fullMatch)
				{
					list3.Insert(0, new List<Token> { match.Token });
				}
				return list3;
			}
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x00079DE0 File Offset: 0x00077FE0
		public static IEnumerable<RegularExpression> LearnLeftMatches(IEnumerable<Record<LearningCacheSubstring, uint>> positivePosInSubstrings, IEnumerable<Record<LearningCacheSubstring, uint>> negativePosInSubstrings, int maxTokenCount, int exampleCount = 0)
		{
			HashSet<RegularExpression> hashSet = negativePosInSubstrings.SelectMany((Record<LearningCacheSubstring, uint> posInSub) => RegularExpression.LearnLeftMatches(posInSub.Item1, posInSub.Item2, maxTokenCount, exampleCount)).ConvertToHashSet<RegularExpression>();
			HashSet<RegularExpression> hashSet2 = null;
			foreach (Record<LearningCacheSubstring, uint> record in positivePosInSubstrings)
			{
				IEnumerable<RegularExpression> enumerable = RegularExpression.LearnLeftMatches(record.Item1, record.Item2, maxTokenCount, exampleCount);
				if (hashSet2 == null)
				{
					hashSet2 = enumerable.ConvertToHashSet<RegularExpression>();
				}
				else
				{
					hashSet2.IntersectWith(enumerable);
				}
				hashSet2.ExceptWith(hashSet);
			}
			IEnumerable<RegularExpression> enumerable2 = hashSet2;
			return enumerable2 ?? Enumerable.Empty<RegularExpression>();
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x00079E9C File Offset: 0x0007809C
		public static IEnumerable<RegularExpression> LearnLeftMatches(LearningCacheSubstring s, uint pos, int maxTokenCount, int exampleCount = 0)
		{
			if (pos < s.Start || pos > s.End || maxTokenCount < 1)
			{
				return Enumerable.Empty<RegularExpression>();
			}
			List<RegularExpression> list3 = new List<RegularExpression> { RegularExpression.Create(exampleCount) };
			UnboundedCache<Token, TokenMatch> unboundedCache;
			if (!s.Cache.TryGetAllMatchesEndingAt(pos, out unboundedCache) || unboundedCache == null)
			{
				return list3;
			}
			Func<List<Token>, RegularExpression> <>9__0;
			foreach (KeyValuePair<Token, TokenMatch> keyValuePair in unboundedCache.Mappings)
			{
				Token token;
				TokenMatch tokenMatch;
				keyValuePair.Deconstruct(out token, out tokenMatch);
				Token token2 = token;
				TokenMatch tokenMatch2 = tokenMatch;
				if (pos != 0U || exampleCount >= 2 || !(token2.Name == "Line Separator"))
				{
					IEnumerable<List<Token>> enumerable = RegularExpression.LearnLeftMatchAt(tokenMatch2, s, pos, maxTokenCount);
					List<RegularExpression> list2 = list3;
					IEnumerable<List<Token>> enumerable2 = enumerable;
					Func<List<Token>, RegularExpression> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (List<Token> list) => new RegularExpression(list, exampleCount));
					}
					list2.AddRange(enumerable2.Select(func));
				}
			}
			return list3;
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x00079FA4 File Offset: 0x000781A4
		private static List<List<Token>> LearnLeftMatchAt(TokenMatch match, LearningCacheSubstring s, uint pos, int maxTokenCount)
		{
			uint num = pos - match.Length;
			if (maxTokenCount == 1)
			{
				List<Token> list = new List<Token>();
				if (num >= s.Start)
				{
					list.Add(match.Token);
				}
				return new List<List<Token>> { list };
			}
			if (num < s.Start)
			{
				return new List<List<Token>>
				{
					new List<Token>()
				};
			}
			List<List<Token>> list2 = new List<List<Token>>();
			UnboundedCache<Token, TokenMatch> unboundedCache;
			if (s.Cache.TryGetAllMatchesEndingAt(num, out unboundedCache))
			{
				foreach (TokenMatch tokenMatch in unboundedCache.Values)
				{
					list2.AddRange(RegularExpression.LearnLeftMatchAt(tokenMatch, s, num, maxTokenCount - 1));
				}
			}
			foreach (List<Token> list3 in list2)
			{
				list3.Add(match.Token);
			}
			list2.Insert(0, new List<Token> { match.Token });
			return list2;
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x0007A0C4 File Offset: 0x000782C4
		private static RegularExpression Create(int exampleCount)
		{
			return new RegularExpression(exampleCount);
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x0007A0CC File Offset: 0x000782CC
		private static RegularExpression Create(Token token, int exampleCount)
		{
			if (token == null)
			{
				return RegularExpression.Create(exampleCount);
			}
			RegularExpression regularExpression;
			if (!RegularExpression.RegexPool1.TryGetValue(token, out regularExpression))
			{
				return RegularExpression.RegexPool1.GetOrAdd(token, new RegularExpression(new Token[] { token }, exampleCount));
			}
			regularExpression.ExampleCount = exampleCount;
			return regularExpression;
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x0007A11C File Offset: 0x0007831C
		private static RegularExpression Create(Token token1, Token token2, int exampleCount)
		{
			if (token1 == null)
			{
				return RegularExpression.Create(exampleCount);
			}
			if (token2 == null)
			{
				return RegularExpression.Create(token1, exampleCount);
			}
			ConcurrentDictionary<Token, RegularExpression> concurrentDictionary;
			if (!RegularExpression.RegexPool2.TryGetValue(token1, out concurrentDictionary))
			{
				return RegularExpression.RegexPool2.GetOrAdd(token1, new ConcurrentDictionary<Token, RegularExpression>()).GetOrAdd(token2, new RegularExpression(new Token[] { token1, token2 }, exampleCount));
			}
			RegularExpression regularExpression;
			if (!concurrentDictionary.TryGetValue(token2, out regularExpression))
			{
				return concurrentDictionary[token2] = new RegularExpression(new Token[] { token1, token2 }, exampleCount);
			}
			regularExpression.ExampleCount = exampleCount;
			return regularExpression;
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x0007A1B8 File Offset: 0x000783B8
		private static RegularExpression Create(Token token1, Token token2, Token token3, int exampleCount)
		{
			if (token1 == null)
			{
				return RegularExpression.Create(exampleCount);
			}
			if (token2 == null)
			{
				return RegularExpression.Create(token1, exampleCount);
			}
			if (token3 == null)
			{
				return RegularExpression.Create(token1, token2, exampleCount);
			}
			ConcurrentDictionary<Token, ConcurrentDictionary<Token, RegularExpression>> concurrentDictionary;
			if (!RegularExpression.RegexPool3.TryGetValue(token1, out concurrentDictionary))
			{
				return RegularExpression.RegexPool3.GetOrAdd(token1, new ConcurrentDictionary<Token, ConcurrentDictionary<Token, RegularExpression>>()).GetOrAdd(token2, new ConcurrentDictionary<Token, RegularExpression>()).GetOrAdd(token3, new RegularExpression(new Token[] { token1, token2, token3 }, exampleCount));
			}
			ConcurrentDictionary<Token, RegularExpression> concurrentDictionary2;
			if (!concurrentDictionary.TryGetValue(token2, out concurrentDictionary2))
			{
				concurrentDictionary[token2] = new ConcurrentDictionary<Token, RegularExpression>();
				return concurrentDictionary[token2][token3] = new RegularExpression(new Token[] { token1, token2, token3 }, exampleCount);
			}
			RegularExpression regularExpression;
			if (concurrentDictionary2.TryGetValue(token3, out regularExpression))
			{
				regularExpression.ExampleCount = exampleCount;
				return regularExpression;
			}
			return concurrentDictionary2[token3] = new RegularExpression(new Token[] { token1, token2, token3 }, exampleCount);
		}

		// Token: 0x040014C2 RID: 5314
		public static int DefaultTokenCount = 3;

		// Token: 0x040014C3 RID: 5315
		private Regex _regex;

		// Token: 0x040014C6 RID: 5318
		private static readonly RegularExpression EmptyRegularExpression = new RegularExpression(0);

		// Token: 0x040014C7 RID: 5319
		private static readonly ConcurrentDictionary<Token, RegularExpression> RegexPool1 = new ConcurrentDictionary<Token, RegularExpression>();

		// Token: 0x040014C8 RID: 5320
		private static readonly ConcurrentDictionary<Token, ConcurrentDictionary<Token, RegularExpression>> RegexPool2 = new ConcurrentDictionary<Token, ConcurrentDictionary<Token, RegularExpression>>();

		// Token: 0x040014C9 RID: 5321
		private static readonly ConcurrentDictionary<Token, ConcurrentDictionary<Token, ConcurrentDictionary<Token, RegularExpression>>> RegexPool3 = new ConcurrentDictionary<Token, ConcurrentDictionary<Token, ConcurrentDictionary<Token, RegularExpression>>>();

		// Token: 0x020007F4 RID: 2036
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040014CA RID: 5322
			public static Func<string, Optional<Token>> <0>__TryParse;
		}
	}
}
