using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x020007FB RID: 2043
	public static class RegularExpressionPositions
	{
		// Token: 0x06002B9A RID: 11162 RVA: 0x0007A384 File Offset: 0x00078584
		private static uint? _RegexPositionHelper(LearningCacheSubstring s, Record<RegularExpression, RegularExpression> rr, int k, bool firstIsLeft)
		{
			StringLearningCache cache = s.Cache;
			CachedList cachedList;
			if (!s.Cache.TryGetMatchPositionsFor((firstIsLeft ? rr.Item1 : rr.Item2).Tokens[0], out cachedList))
			{
				return null;
			}
			Record<int, int>? values = cachedList.GetValues(s.Start, s.End);
			if (values == null)
			{
				return null;
			}
			Record<int, int> value = values.Value;
			int num = 0;
			int num2 = Math.Abs(k);
			int num3 = value.Item2 - value.Item1 + 1;
			if (num3 < num2)
			{
				return null;
			}
			if ((firstIsLeft && rr.Item2.Count == 0 && rr.Item1.Count == 1) || (!firstIsLeft && rr.Item2.Count == 1))
			{
				PositionMatch positionMatch = cachedList[(k > 0) ? (value.Item1 + k - 1) : (value.Item2 + k + 1)];
				return new uint?(firstIsLeft ? positionMatch.Right : positionMatch.Position);
			}
			int num4 = ((k > 0) ? 1 : (-1));
			int num5 = ((k > 0) ? value.Item1 : value.Item2);
			while (num5 >= value.Item1 && num5 <= value.Item2)
			{
				uint num6;
				if (!RegularExpressionPositions.TryGetNextPos(rr, firstIsLeft, cachedList, num5, cache, s.End, out num6))
				{
					if (--num3 < num2)
					{
						return null;
					}
				}
				else
				{
					num++;
					if (num == num2)
					{
						return new uint?(num6);
					}
				}
				num5 += num4;
			}
			return null;
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x0007A514 File Offset: 0x00078714
		private static bool TryGetNextPos(Record<RegularExpression, RegularExpression> rr, bool firstIsLeft, CachedList allFirstTokenMatches, int idx, StringLearningCache cache, uint end, out uint pos)
		{
			uint num;
			if (firstIsLeft)
			{
				pos = allFirstTokenMatches[idx].Right;
				for (int i = 1; i < rr.Item1.Count; i++)
				{
					PositionMatch positionMatch;
					if (!cache.TryGetTokenMatchStartingAt(pos, rr.Item1.Tokens[i], out positionMatch))
					{
						return false;
					}
					pos += positionMatch.Length;
					if (pos > end)
					{
						return false;
					}
				}
				num = pos;
			}
			else
			{
				pos = allFirstTokenMatches[idx].Position;
				num = allFirstTokenMatches[idx].Right;
			}
			if (num > end)
			{
				return false;
			}
			for (int j = ((!firstIsLeft) ? 1 : 0); j < rr.Item2.Count; j++)
			{
				PositionMatch positionMatch2;
				if (!cache.TryGetTokenMatchStartingAt(num, rr.Item2.Tokens[j], out positionMatch2))
				{
					return false;
				}
				num += positionMatch2.Length;
				if (num > end)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x0007A5EC File Offset: 0x000787EC
		public static uint? RegexPosition(LearningCacheSubstring s, Record<RegularExpression, RegularExpression> rr, int k)
		{
			if (k == 0)
			{
				return null;
			}
			if (rr.Item1.Count != 0)
			{
				return RegularExpressionPositions._RegexPositionHelper(s, rr, k, true);
			}
			if (rr.Item2.Count != 0)
			{
				return RegularExpressionPositions._RegexPositionHelper(s, rr, k, false);
			}
			if (k > 0)
			{
				if ((long)k <= (long)((ulong)(s.Length + 1U)))
				{
					return new uint?((uint)((ulong)s.Start + (ulong)((long)k) - 1UL));
				}
				return null;
			}
			else
			{
				if ((long)(-(long)k) <= (long)((ulong)(s.Length + 1U)))
				{
					return new uint?((uint)((ulong)s.End + (ulong)((long)k) + 1UL));
				}
				return null;
			}
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x0007A690 File Offset: 0x00078890
		public static IReadOnlyList<uint?> RunRR(LearningCacheSubstring s, Record<RegularExpression, RegularExpression> rr)
		{
			PositionMatch[] array = rr.Item1.Run(s);
			if (array.Length == 0)
			{
				return RegularExpressionPositions.NoMatches;
			}
			List<uint?> list = new List<uint?>(array.Length);
			foreach (PositionMatch positionMatch in array)
			{
				if (rr.Item2.MatchesAt(s, positionMatch.Right))
				{
					list.Add(new uint?(positionMatch.Right));
				}
			}
			return list;
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x0007A700 File Offset: 0x00078900
		private static IEnumerable<Record<RegularExpression, RegularExpression>> GetRegexesAtPosition(LearningCacheSubstring substring, uint pos, int exampleCount, int? tokenCount = null, bool allowRegexPair = true)
		{
			tokenCount = new int?(tokenCount ?? RegularExpression.DefaultTokenCount);
			IEnumerable<RegularExpression> enumerable = RegularExpression.LearnLeftMatches(substring, pos, tokenCount.Value, exampleCount);
			IEnumerable<RegularExpression> rightMatches = RegularExpression.LearnRightMatches(substring, pos, tokenCount.Value, exampleCount);
			if (allowRegexPair)
			{
				return from r1 in enumerable
					from r2 in rightMatches
					where r1.Count != 0 || r2.Count != 0
					select Record.Create<RegularExpression, RegularExpression>(r1, r2);
			}
			return (from r1 in enumerable
				where r1.Count != 0
				select Record.Create<RegularExpression, RegularExpression>(r1, new RegularExpression(0))).Concat(from r2 in rightMatches
				where r2.Count != 0
				select Record.Create<RegularExpression, RegularExpression>(new RegularExpression(0), r2));
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x0007A874 File Offset: 0x00078A74
		public static IEnumerable<Record<RegularExpression, RegularExpression>> GetRegexesAtPositions(LearningCacheSubstring substring, IEnumerable<uint> positions, int exampleCount)
		{
			return from rr in positions.SelectMany((uint pos) => RegularExpressionPositions.GetRegexesAtPosition(substring, pos, exampleCount, null, true)).Distinct<Record<RegularExpression, RegularExpression>>()
				select Record.Create<RegularExpression, RegularExpression>(rr.Item1, rr.Item2);
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x0007A8D0 File Offset: 0x00078AD0
		private static ICollection<Record<Record<RegularExpression, RegularExpression>?, int>> GetRegexesAndIndexesAtPos(LearningCacheSubstring substring, uint pos, int exampleCount, Dictionary<Record<RegularExpression, RegularExpression>, RegularExpressionPositions.RRWithRunRRCache> regexCache, int? tokenCount = null, bool allowRegexPair = true)
		{
			List<Record<Record<RegularExpression, RegularExpression>?, int>> list = new List<Record<Record<RegularExpression, RegularExpression>?, int>>();
			Func<Record<RegularExpression, RegularExpression>, RegularExpressionPositions.RRWithRunRRCache> <>9__0;
			foreach (Record<RegularExpression, RegularExpression> record in RegularExpressionPositions.GetRegexesAtPosition(substring, pos, exampleCount, tokenCount, allowRegexPair))
			{
				Record<RegularExpression, RegularExpression> record2 = record;
				Func<Record<RegularExpression, RegularExpression>, RegularExpressionPositions.RRWithRunRRCache> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (Record<RegularExpression, RegularExpression> rrKey) => new RegularExpressionPositions.RRWithRunRRCache(substring, rrKey));
				}
				RegularExpressionPositions.RRWithRunRRCache orAdd = regexCache.GetOrAdd(record2, func);
				Record<RegularExpression, RegularExpression>? record3 = new Record<RegularExpression, RegularExpression>?(orAdd.RR);
				IReadOnlyList<uint?> matchPositions = orAdd.MatchPositions;
				int? num = matchPositions.IndexOf(new uint?(pos));
				if (num != null)
				{
					list.Add(Record.Create<Record<RegularExpression, RegularExpression>?, int>(record3, num.Value + 1));
					list.Add(Record.Create<Record<RegularExpression, RegularExpression>?, int>(record3, num.Value - matchPositions.Count));
				}
			}
			return list;
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x0007A9C8 File Offset: 0x00078BC8
		public static HashSet<Record<Record<RegularExpression, RegularExpression>?, int>> GetRegexesAndIndexesAtPositions(LearningCacheSubstring substring, IEnumerable<uint> positions, int exampleCount, int? tokenCount = null, bool allowRegexPair = true)
		{
			Dictionary<Record<RegularExpression, RegularExpression>, RegularExpressionPositions.RRWithRunRRCache> regexCache = new Dictionary<Record<RegularExpression, RegularExpression>, RegularExpressionPositions.RRWithRunRRCache>();
			if (substring.Start != 0U || (ulong)substring.End != (ulong)((long)substring.Source.Length))
			{
				return positions.SelectMany((uint pos) => RegularExpressionPositions.GetRegexesAndIndexesAtPos(substring, pos, exampleCount, regexCache, tokenCount, allowRegexPair)).ConvertToHashSet<Record<Record<RegularExpression, RegularExpression>?, int>>();
			}
			return positions.SelectMany(delegate(uint pos)
			{
				IntegerKeyedCache<ICollection<Record<Record<RegularExpression, RegularExpression>?, int>>> specializedCache = substring.Cache.GetSpecializedCache<IntegerKeyedCache<ICollection<Record<Record<RegularExpression, RegularExpression>?, int>>>>((StringLearningCache cache) => new IntegerKeyedCache<ICollection<Record<Record<RegularExpression, RegularExpression>?, int>>>((uint)(cache.Content.Length + 1), null));
				ICollection<Record<Record<RegularExpression, RegularExpression>?, int>> collection = specializedCache[(int)pos];
				if (collection == null)
				{
					collection = RegularExpressionPositions.GetRegexesAndIndexesAtPos(substring, pos, exampleCount, regexCache, tokenCount, allowRegexPair);
					specializedCache.Add((int)pos, collection);
				}
				return collection;
			}).ConvertToHashSet<Record<Record<RegularExpression, RegularExpression>?, int>>();
		}

		// Token: 0x040014DE RID: 5342
		private static readonly IReadOnlyList<uint?> NoMatches = new uint?[0];

		// Token: 0x020007FC RID: 2044
		private struct RRWithRunRRCache
		{
			// Token: 0x06002BA3 RID: 11171 RVA: 0x0007AA6B File Offset: 0x00078C6B
			public RRWithRunRRCache(LearningCacheSubstring s, Record<RegularExpression, RegularExpression> rr)
			{
				this.RR = rr;
				this.MatchPositions = RegularExpressionPositions.RunRR(s, rr);
			}

			// Token: 0x170007AC RID: 1964
			// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x0007AA81 File Offset: 0x00078C81
			public readonly Record<RegularExpression, RegularExpression> RR { get; }

			// Token: 0x170007AD RID: 1965
			// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x0007AA89 File Offset: 0x00078C89
			public readonly IReadOnlyList<uint?> MatchPositions { get; }
		}
	}
}
