using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Transformation.Text
{
	// Token: 0x02001B72 RID: 7026
	public static class DynamicTokenExtractor
	{
		// Token: 0x0600E67F RID: 59007 RVA: 0x0030CF4A File Offset: 0x0030B14A
		private static StringRegion AsStringRegion(this PositionMatch positionMatch, StringLearningCache cache)
		{
			return new StringRegion(cache).Slice(positionMatch.Position, positionMatch.Right);
		}

		// Token: 0x0600E680 RID: 59008 RVA: 0x0030CF64 File Offset: 0x0030B164
		private static CachedList GetMatchPositionsFor(this StringLearningCache cache, Token token)
		{
			CachedList cachedList;
			cache.TryGetMatchPositionsFor(token, out cachedList);
			return cachedList;
		}

		// Token: 0x0600E681 RID: 59009 RVA: 0x0030CF7C File Offset: 0x0030B17C
		private static IEnumerable<DynamicTokenExtractor.PositionedConcreteToken> GetCommonTokensOfType(Token tokenType, IReadOnlyList<StringLearningCache> inputCaches)
		{
			List<DynamicTokenExtractor.PositionedConcreteToken> list = (from positionMatch in inputCaches[0].GetMatchPositionsFor(tokenType)
				select new DynamicTokenExtractor.PositionedConcreteToken(tokenType, inputCaches[0], positionMatch)).ToList<DynamicTokenExtractor.PositionedConcreteToken>();
			if (list.Count == 0)
			{
				return Enumerable.Empty<DynamicTokenExtractor.PositionedConcreteToken>();
			}
			HashSet<string> commonConcreteTokens = list.Select((DynamicTokenExtractor.PositionedConcreteToken p) => p.StringRegion.Value).ConvertToHashSet<string>();
			using (IEnumerator<StringLearningCache> enumerator = inputCaches.Skip(1).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					StringLearningCache cache = enumerator.Current;
					commonConcreteTokens.IntersectWith(from pm in cache.GetMatchPositionsFor(tokenType)
						select pm.AsStringRegion(cache).Value);
					if (commonConcreteTokens.Count == 0)
					{
						return Enumerable.Empty<DynamicTokenExtractor.PositionedConcreteToken>();
					}
				}
			}
			return list.Where((DynamicTokenExtractor.PositionedConcreteToken match) => commonConcreteTokens.Contains(match.StringRegion.Value));
		}

		// Token: 0x0600E682 RID: 59010 RVA: 0x0030D0AC File Offset: 0x0030B2AC
		private static IReadOnlyList<DynamicTokenExtractor.PositionedConcreteToken> GetCommonTokens(IReadOnlyList<StringLearningCache> inputCaches)
		{
			return (from token in inputCaches.Select((StringLearningCache cache) => cache.MatchedTokens).Intersect<Token>()
				where token.UseForLearning && token.Name != "Line Separator"
				select token).SelectMany((Token tokenType) => DynamicTokenExtractor.GetCommonTokensOfType(tokenType, inputCaches)).ToList<DynamicTokenExtractor.PositionedConcreteToken>();
		}

		// Token: 0x0600E683 RID: 59011 RVA: 0x0030D130 File Offset: 0x0030B330
		private static bool IsStringRegionAToken(StringLearningCache cache, uint pos, uint length)
		{
			UnboundedCache<Token, TokenMatch> unboundedCache;
			return cache.TryGetAllMatchesStartingAt(pos, out unboundedCache) && unboundedCache.Mappings.Any((KeyValuePair<Token, TokenMatch> kv) => kv.Value.Length == length);
		}

		// Token: 0x0600E684 RID: 59012 RVA: 0x0030D170 File Offset: 0x0030B370
		private static bool IsTokenInStringAt(StringLearningCache cache, uint pos, DynamicTokenExtractor.PositionedConcreteToken token)
		{
			uint length = token.StringRegion.Length;
			return (ulong)(pos + length) <= (ulong)((long)cache.Content.Length) && cache.Content.Substring((int)pos, (int)length) == token.StringRegion.Value && DynamicTokenExtractor.IsStringRegionAToken(cache, pos, length);
		}

		// Token: 0x0600E685 RID: 59013 RVA: 0x0030D1CC File Offset: 0x0030B3CC
		private static DynamicTokenExtractor.TokenList GetCommonTokensStartingAt(DynamicTokenExtractor.PositionedConcreteToken matchInFirstString, IReadOnlyList<StringLearningCache> inputCaches, MultiValueDictionary<uint, DynamicTokenExtractor.PositionedConcreteToken> matchPositionsByStart)
		{
			List<DynamicTokenExtractor.PositionedConcreteToken> list = new List<DynamicTokenExtractor.PositionedConcreteToken> { matchInFirstString };
			uint num = matchInFirstString.StringRegion.Length;
			List<uint>[] array = null;
			int num2;
			for (;;)
			{
				IReadOnlyCollection<DynamicTokenExtractor.PositionedConcreteToken> readOnlyCollection;
				if (!matchPositionsByStart.TryGetValue(list.Last<DynamicTokenExtractor.PositionedConcreteToken>().StringRegion.End, out readOnlyCollection))
				{
					break;
				}
				if (readOnlyCollection.All((DynamicTokenExtractor.PositionedConcreteToken token) => token.StringRegion.Length == 0U))
				{
					break;
				}
				DynamicTokenExtractor.PositionedConcreteToken nextToken = readOnlyCollection.Where((DynamicTokenExtractor.PositionedConcreteToken token) => token.StringRegion.Length > 0U).Min(DynamicTokenExtractor.PositionedConcreteTokenLengthComparer);
				string value = nextToken.StringRegion.Value;
				uint nextTokenLength = nextToken.StringRegion.Length;
				uint tokenListLength = num;
				num += nextTokenLength;
				if (array == null)
				{
					StringRegion tokenListWithNextStringRegion = DynamicTokenExtractor.GetTokenListStringRegion(inputCaches, list, nextToken);
					array = new List<uint>[inputCaches.Count];
					for (int i = 1; i < inputCaches.Count; i++)
					{
						StringLearningCache otherCache = inputCaches[i];
						CachedList cachedList;
						otherCache.TryGetMatchPositionsFor(list[0].Token, out cachedList);
						IEnumerable<uint> enumerable = from pm in cachedList
							select pm.Position into pos
							where (ulong)(pos + tokenListWithNextStringRegion.Length) <= (ulong)((long)otherCache.Content.Length) && otherCache.Content.Substring((int)pos, (int)tokenListWithNextStringRegion.Length) == tokenListWithNextStringRegion.Value && DynamicTokenExtractor.IsStringRegionAToken(otherCache, pos + tokenListLength, nextTokenLength)
							select pos;
						array[i] = enumerable.ToList<uint>();
						if (array[i].Count == 0)
						{
							goto Block_6;
						}
					}
				}
				else
				{
					num2 = array.Skip(1).Max((List<uint> matches) => matches.Count);
					for (int j = 1; j < inputCaches.Count; j++)
					{
						StringLearningCache input = inputCaches[j];
						array[j] = array[j].Where((uint pos) => DynamicTokenExtractor.IsTokenInStringAt(input, pos + tokenListLength, nextToken)).ToList<uint>();
						if (array[j].Count == 0)
						{
							goto Block_9;
						}
					}
				}
				list.Add(nextToken);
			}
			return new DynamicTokenExtractor.TokenList
			{
				Tokens = list
			};
			Block_6:
			return new DynamicTokenExtractor.TokenList
			{
				Tokens = list
			};
			Block_9:
			return new DynamicTokenExtractor.TokenList
			{
				Tokens = list,
				MaxMatches = new int?(num2)
			};
		}

		// Token: 0x0600E686 RID: 59014 RVA: 0x0030D46F File Offset: 0x0030B66F
		private static StringRegion GetTokenListStringRegion(IReadOnlyList<StringLearningCache> inputCaches, IReadOnlyList<DynamicTokenExtractor.PositionedConcreteToken> tokenList, DynamicTokenExtractor.PositionedConcreteToken nextToken = null)
		{
			return new StringRegion(inputCaches[0]).Slice(tokenList[0].StringRegion.Start, (nextToken ?? tokenList.Last<DynamicTokenExtractor.PositionedConcreteToken>()).StringRegion.End);
		}

		// Token: 0x0600E687 RID: 59015 RVA: 0x0030D4A8 File Offset: 0x0030B6A8
		private static ICollection<Record<IReadOnlyList<DynamicTokenExtractor.PositionedConcreteToken>, int?>> GetConcatenatedCommonTokens(IReadOnlyList<StringLearningCache> inputCaches, ISet<DynamicTokenExtractor.PositionedConcreteToken> commonTokensInFirstString)
		{
			if (commonTokensInFirstString.Count <= 1)
			{
				return commonTokensInFirstString.Select((DynamicTokenExtractor.PositionedConcreteToken t) => new Record<IReadOnlyList<DynamicTokenExtractor.PositionedConcreteToken>, int?>(new List<DynamicTokenExtractor.PositionedConcreteToken> { t }, null)).ToList<Record<IReadOnlyList<DynamicTokenExtractor.PositionedConcreteToken>, int?>>();
			}
			Dictionary<string, Record<IReadOnlyList<DynamicTokenExtractor.PositionedConcreteToken>, int?>> dictionary = new Dictionary<string, Record<IReadOnlyList<DynamicTokenExtractor.PositionedConcreteToken>, int?>>();
			MultiValueDictionary<uint, DynamicTokenExtractor.PositionedConcreteToken> multiValueDictionary = (from m in commonTokensInFirstString
				group m by m.StringRegion.Start).ToMultiValueDictionary(null);
			uint num = commonTokensInFirstString.Max((DynamicTokenExtractor.PositionedConcreteToken t) => t.StringRegion.End);
			uint num2 = 0U;
			uint num3 = 0U;
			foreach (DynamicTokenExtractor.PositionedConcreteToken positionedConcreteToken in commonTokensInFirstString.OrderBy((DynamicTokenExtractor.PositionedConcreteToken m) => m.StringRegion.Start))
			{
				if (positionedConcreteToken.StringRegion.Start >= num3)
				{
					DynamicTokenExtractor.TokenList commonTokensStartingAt = DynamicTokenExtractor.GetCommonTokensStartingAt(positionedConcreteToken, inputCaches, multiValueDictionary);
					uint end = commonTokensStartingAt.Tokens.Last<DynamicTokenExtractor.PositionedConcreteToken>().StringRegion.End;
					if (end > num2)
					{
						num2 = end;
						string value = DynamicTokenExtractor.GetTokenListStringRegion(inputCaches, commonTokensStartingAt.Tokens, null).Value;
						int num4 = (commonTokensStartingAt.MaxMatches ?? 1) + ((from t in dictionary.MaybeGet(value)
							select t.Item2).OrElse(new int?(0)) ?? 1);
						dictionary[value] = new Record<IReadOnlyList<DynamicTokenExtractor.PositionedConcreteToken>, int?>(commonTokensStartingAt.Tokens, new int?(num4));
						if (num2 >= num)
						{
							break;
						}
						if (!multiValueDictionary.ContainsKey(num2))
						{
							num3 = num2;
						}
					}
				}
			}
			return dictionary.Values;
		}

		// Token: 0x0600E688 RID: 59016 RVA: 0x0030D6AC File Offset: 0x0030B8AC
		public static IReadOnlyList<Token> ExtractDynamicTokens(IReadOnlyList<string> inputColumn)
		{
			return DynamicTokenExtractor.ExtractDynamicTokens(inputColumn.Select((string str) => new StringLearningCache(str, Semantics.Tokens)));
		}

		// Token: 0x0600E689 RID: 59017 RVA: 0x0030D6D8 File Offset: 0x0030B8D8
		internal static IReadOnlyList<Token> ExtractDynamicTokens(IEnumerable<StringLearningCache> inputColumn)
		{
			List<StringLearningCache> inputColumnList = inputColumn.Where((StringLearningCache c) => c != null).Distinct<StringLearningCache>().Take(1000)
				.ToList<StringLearningCache>();
			if (inputColumnList.Count <= 1)
			{
				return new List<Token>();
			}
			IEnumerable<DynamicTokenExtractor.PositionedConcreteToken> enumerable = from t in DynamicTokenExtractor.GetCommonTokens(inputColumnList)
				group t by t.StringRegion into g
				select g.FirstOrDefault((DynamicTokenExtractor.PositionedConcreteToken t) => t.Token.IsSymbol) ?? g.First<DynamicTokenExtractor.PositionedConcreteToken>();
			IEnumerable<Record<IReadOnlyList<DynamicTokenExtractor.PositionedConcreteToken>, int?>> concatenatedCommonTokens = DynamicTokenExtractor.GetConcatenatedCommonTokens(inputColumnList, enumerable.ConvertToHashSet<DynamicTokenExtractor.PositionedConcreteToken>());
			int baseScore = (int)((double)(100 * Math.Min(10, inputColumnList.Count)) / 5.0);
			return (from <>h__TransparentIdentifier3 in (from tl in concatenatedCommonTokens
					let tokenList = tl.Item1
					let maxCount = tl.Item2
					where tokenList.Any((DynamicTokenExtractor.PositionedConcreteToken t) => !t.Token.IsSymbol)
					let str = DynamicTokenExtractor.GetTokenListStringRegion(inputColumnList, tokenList, null)
					where (ulong)str.Length >= (ulong)((long)DynamicTokenExtractor.MinDynamicTokenLength)
					select <>h__TransparentIdentifier2).Select(delegate(<>h__TransparentIdentifier2)
				{
					int num = baseScore - (int)<>h__TransparentIdentifier2.str.Length;
					int? maxCount = <>h__TransparentIdentifier2.<>h__TransparentIdentifier1.maxCount;
					int num2 = 1;
					return new
					{
						<>h__TransparentIdentifier2 = <>h__TransparentIdentifier2,
						score = num - (((maxCount.GetValueOrDefault() == num2) & (maxCount != null)) ? 0 : 50)
					};
				})
				select Token.BuildDynamic(<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.str.Value, <>h__TransparentIdentifier3.score)).ToList<StringToken>();
		}

		// Token: 0x0600E68A RID: 59018 RVA: 0x0030D8A8 File Offset: 0x0030BAA8
		private static IReadOnlyList<Token> ExtractDynamicTokens(IReadOnlyList<IReadOnlyList<ValueSubstring>> allInputs)
		{
			if (allInputs.Count <= 1)
			{
				return new List<Token>();
			}
			HashSet<Token> hashSet = new HashSet<Token>();
			int column;
			int column2;
			for (column = 0; column < allInputs[0].Count; column = column2 + 1)
			{
				IReadOnlyList<Token> readOnlyList = DynamicTokenExtractor.ExtractDynamicTokens((from s in allInputs.Select((IReadOnlyList<ValueSubstring> input) => input[column]).ToList<ValueSubstring>()
					select s.Cache).ToList<StringLearningCache>());
				hashSet.UnionWith(readOnlyList);
				column2 = column;
			}
			return hashSet.ToList<Token>();
		}

		// Token: 0x0600E68B RID: 59019 RVA: 0x0030D950 File Offset: 0x0030BB50
		internal static void InitializeLearningCache(IReadOnlyList<IReadOnlyList<ValueSubstring>> allInputs)
		{
			IReadOnlyList<Token> readOnlyList = DynamicTokenExtractor.ExtractDynamicTokens(allInputs);
			foreach (ValueSubstring valueSubstring in allInputs.SelectMany((IReadOnlyList<ValueSubstring> input) => input))
			{
				valueSubstring.Cache.AddTokens(readOnlyList);
			}
		}

		// Token: 0x0600E68C RID: 59020 RVA: 0x0030D9C8 File Offset: 0x0030BBC8
		internal static void InitializeLearningCache(IReadOnlyList<ValueSubstringRow> allInputs, IEnumerable<string> columns)
		{
			using (IEnumerator<string> enumerator = columns.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string column = enumerator.Current;
					IReadOnlyList<Token> readOnlyList = DynamicTokenExtractor.ExtractDynamicTokens(from ss in allInputs.Select((ValueSubstringRow row) => row[column]).OfType<LearningCacheSubstring>()
						select ss.Cache);
					Func<ValueSubstringRow, object> func;
					Func<ValueSubstringRow, object> <>9__2;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (ValueSubstringRow input) => input[column]);
					}
					foreach (LearningCacheSubstring learningCacheSubstring in allInputs.Select(func).OfType<LearningCacheSubstring>())
					{
						if (learningCacheSubstring != null)
						{
							learningCacheSubstring.Cache.AddTokens(readOnlyList);
						}
					}
				}
			}
		}

		// Token: 0x04005784 RID: 22404
		private const int MaxInputsToConsider = 1000;

		// Token: 0x04005785 RID: 22405
		private const int NonUniquePenalty = 50;

		// Token: 0x04005786 RID: 22406
		private static readonly int MinDynamicTokenLength = 4;

		// Token: 0x04005787 RID: 22407
		private static readonly IComparer<DynamicTokenExtractor.PositionedConcreteToken> PositionedConcreteTokenLengthComparer = Comparer<DynamicTokenExtractor.PositionedConcreteToken>.Create((DynamicTokenExtractor.PositionedConcreteToken x, DynamicTokenExtractor.PositionedConcreteToken y) => x.StringRegion.Length.CompareTo(y.StringRegion.Length));

		// Token: 0x02001B73 RID: 7027
		private struct TokenList
		{
			// Token: 0x04005788 RID: 22408
			public IReadOnlyList<DynamicTokenExtractor.PositionedConcreteToken> Tokens;

			// Token: 0x04005789 RID: 22409
			public int? MaxMatches;
		}

		// Token: 0x02001B74 RID: 7028
		private class PositionedConcreteToken : Tuple<Token, StringRegion>
		{
			// Token: 0x0600E68E RID: 59022 RVA: 0x0030DAEA File Offset: 0x0030BCEA
			private PositionedConcreteToken(Token token, StringRegion value)
				: base(token, value)
			{
			}

			// Token: 0x0600E68F RID: 59023 RVA: 0x0030DAF4 File Offset: 0x0030BCF4
			public PositionedConcreteToken(Token token, StringLearningCache cache, PositionMatch positionMatch)
				: this(token, positionMatch.AsStringRegion(cache))
			{
			}

			// Token: 0x17002681 RID: 9857
			// (get) Token: 0x0600E690 RID: 59024 RVA: 0x0030DB04 File Offset: 0x0030BD04
			public Token Token
			{
				get
				{
					return base.Item1;
				}
			}

			// Token: 0x17002682 RID: 9858
			// (get) Token: 0x0600E691 RID: 59025 RVA: 0x0030DB0C File Offset: 0x0030BD0C
			public StringRegion StringRegion
			{
				get
				{
					return base.Item2;
				}
			}

			// Token: 0x0600E692 RID: 59026 RVA: 0x0030DB14 File Offset: 0x0030BD14
			public override string ToString()
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("PositionedConcreteToken(StringRegion=\"{0}\", Token={1})", new object[] { this.StringRegion, this.Token }));
			}
		}
	}
}
