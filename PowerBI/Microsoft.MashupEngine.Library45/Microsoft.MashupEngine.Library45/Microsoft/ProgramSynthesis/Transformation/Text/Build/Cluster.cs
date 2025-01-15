using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.CustomExtraction;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build
{
	// Token: 0x02001BEC RID: 7148
	public static class Cluster
	{
		// Token: 0x0600EF68 RID: 61288 RVA: 0x000AB0AF File Offset: 0x000A92AF
		private static Optional<T> CastValue<T>(object obj)
		{
			if (!(obj is Bottom))
			{
				return ((T)((object)obj)).Some<T>();
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x0600EF69 RID: 61289 RVA: 0x0033B337 File Offset: 0x00339537
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<@switch>>> ClusterOnInput(this ProgramSetBuilder<@switch> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<@switch>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<@switch>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF6A RID: 61290 RVA: 0x0033B369 File Offset: 0x00339569
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<ite>>> ClusterOnInput(this ProgramSetBuilder<ite> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<ite>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<ite>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF6B RID: 61291 RVA: 0x0033B39B File Offset: 0x0033959B
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<pred>>> ClusterOnInput(this ProgramSetBuilder<pred> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<pred>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<pred>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF6C RID: 61292 RVA: 0x0033B3CD File Offset: 0x003395CD
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<st>>> ClusterOnInput(this ProgramSetBuilder<st> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<st>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<st>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF6D RID: 61293 RVA: 0x0033B3FF File Offset: 0x003395FF
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<e>>> ClusterOnInput(this ProgramSetBuilder<e> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<e>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<e>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF6E RID: 61294 RVA: 0x0033B431 File Offset: 0x00339631
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<f>>> ClusterOnInput(this ProgramSetBuilder<f> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<f>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<f>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF6F RID: 61295 RVA: 0x0033B463 File Offset: 0x00339663
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<letOptions>>> ClusterOnInput(this ProgramSetBuilder<letOptions> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<letOptions>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<letOptions>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF70 RID: 61296 RVA: 0x0033B495 File Offset: 0x00339695
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<v>>> ClusterOnInput(this ProgramSetBuilder<v> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<v>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<v>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF71 RID: 61297 RVA: 0x0033B4C7 File Offset: 0x003396C7
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<indexInputString>>> ClusterOnInput(this ProgramSetBuilder<indexInputString> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<indexInputString>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<indexInputString>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF72 RID: 61298 RVA: 0x0033B4F9 File Offset: 0x003396F9
		public static IEnumerable<KeyValuePair<Optional<object>, ProgramSetBuilder<lookupInput>>> ClusterOnInput(this ProgramSetBuilder<lookupInput> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<object>, ProgramSetBuilder<lookupInput>>(Cluster.CastValue<object>(kvp.Key), ProgramSetBuilder<lookupInput>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF73 RID: 61299 RVA: 0x0033B52B File Offset: 0x0033972B
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<conv>>> ClusterOnInput(this ProgramSetBuilder<conv> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<conv>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<conv>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF74 RID: 61300 RVA: 0x0033B55D File Offset: 0x0033975D
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<rangeString>>> ClusterOnInput(this ProgramSetBuilder<rangeString> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<rangeString>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<rangeString>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF75 RID: 61301 RVA: 0x0033B58F File Offset: 0x0033978F
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<rangeSubstring>>> ClusterOnInput(this ProgramSetBuilder<rangeSubstring> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<rangeSubstring>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<rangeSubstring>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF76 RID: 61302 RVA: 0x0033B5C1 File Offset: 0x003397C1
		public static IEnumerable<KeyValuePair<Optional<decimal?>, ProgramSetBuilder<rangeNumber>>> ClusterOnInput(this ProgramSetBuilder<rangeNumber> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal?>, ProgramSetBuilder<rangeNumber>>(Cluster.CastValue<decimal?>(kvp.Key), ProgramSetBuilder<rangeNumber>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF77 RID: 61303 RVA: 0x0033B5F3 File Offset: 0x003397F3
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<dtRangeString>>> ClusterOnInput(this ProgramSetBuilder<dtRangeString> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<dtRangeString>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<dtRangeString>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF78 RID: 61304 RVA: 0x0033B625 File Offset: 0x00339825
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<dtRangeSubstring>>> ClusterOnInput(this ProgramSetBuilder<dtRangeSubstring> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<dtRangeSubstring>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<dtRangeSubstring>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF79 RID: 61305 RVA: 0x0033B657 File Offset: 0x00339857
		public static IEnumerable<KeyValuePair<Optional<PartialDateTime>, ProgramSetBuilder<rangeDateTime>>> ClusterOnInput(this ProgramSetBuilder<rangeDateTime> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<PartialDateTime>, ProgramSetBuilder<rangeDateTime>>(Cluster.CastValue<PartialDateTime>(kvp.Key), ProgramSetBuilder<rangeDateTime>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF7A RID: 61306 RVA: 0x0033B689 File Offset: 0x00339889
		public static IEnumerable<KeyValuePair<Optional<PartialDateTime>, ProgramSetBuilder<datetime>>> ClusterOnInput(this ProgramSetBuilder<datetime> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<PartialDateTime>, ProgramSetBuilder<datetime>>(Cluster.CastValue<PartialDateTime>(kvp.Key), ProgramSetBuilder<datetime>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF7B RID: 61307 RVA: 0x0033B6BB File Offset: 0x003398BB
		public static IEnumerable<KeyValuePair<Optional<PartialDateTime>, ProgramSetBuilder<inputDateTime>>> ClusterOnInput(this ProgramSetBuilder<inputDateTime> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<PartialDateTime>, ProgramSetBuilder<inputDateTime>>(Cluster.CastValue<PartialDateTime>(kvp.Key), ProgramSetBuilder<inputDateTime>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF7C RID: 61308 RVA: 0x0033B6ED File Offset: 0x003398ED
		public static IEnumerable<KeyValuePair<Optional<PartialDateTime>, ProgramSetBuilder<parsedDateTime>>> ClusterOnInput(this ProgramSetBuilder<parsedDateTime> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<PartialDateTime>, ProgramSetBuilder<parsedDateTime>>(Cluster.CastValue<PartialDateTime>(kvp.Key), ProgramSetBuilder<parsedDateTime>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF7D RID: 61309 RVA: 0x0033B71F File Offset: 0x0033991F
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<SS>>> ClusterOnInput(this ProgramSetBuilder<SS> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<SS>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<SS>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF7E RID: 61310 RVA: 0x0033B751 File Offset: 0x00339951
		public static IEnumerable<KeyValuePair<Optional<Record<uint?, uint?>?>, ProgramSetBuilder<PP>>> ClusterOnInput(this ProgramSetBuilder<PP> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Record<uint?, uint?>?>, ProgramSetBuilder<PP>>(Cluster.CastValue<Record<uint?, uint?>?>(kvp.Key), ProgramSetBuilder<PP>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF7F RID: 61311 RVA: 0x0033B783 File Offset: 0x00339983
		public static IEnumerable<KeyValuePair<Optional<uint?>, ProgramSetBuilder<pos>>> ClusterOnInput(this ProgramSetBuilder<pos> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<uint?>, ProgramSetBuilder<pos>>(Cluster.CastValue<uint?>(kvp.Key), ProgramSetBuilder<pos>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF80 RID: 61312 RVA: 0x0033B7B5 File Offset: 0x003399B5
		public static IEnumerable<KeyValuePair<Optional<Record<RegularExpression, RegularExpression>?>, ProgramSetBuilder<regexPair>>> ClusterOnInput(this ProgramSetBuilder<regexPair> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Record<RegularExpression, RegularExpression>?>, ProgramSetBuilder<regexPair>>(Cluster.CastValue<Record<RegularExpression, RegularExpression>?>(kvp.Key), ProgramSetBuilder<regexPair>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF81 RID: 61313 RVA: 0x0033B7E7 File Offset: 0x003399E7
		public static IEnumerable<KeyValuePair<Optional<decimal?>, ProgramSetBuilder<number>>> ClusterOnInput(this ProgramSetBuilder<number> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal?>, ProgramSetBuilder<number>>(Cluster.CastValue<decimal?>(kvp.Key), ProgramSetBuilder<number>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF82 RID: 61314 RVA: 0x0033B819 File Offset: 0x00339A19
		public static IEnumerable<KeyValuePair<Optional<decimal?>, ProgramSetBuilder<castToNumber>>> ClusterOnInput(this ProgramSetBuilder<castToNumber> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal?>, ProgramSetBuilder<castToNumber>>(Cluster.CastValue<decimal?>(kvp.Key), ProgramSetBuilder<castToNumber>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF83 RID: 61315 RVA: 0x0033B84B File Offset: 0x00339A4B
		public static IEnumerable<KeyValuePair<Optional<decimal?>, ProgramSetBuilder<inputNumber>>> ClusterOnInput(this ProgramSetBuilder<inputNumber> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal?>, ProgramSetBuilder<inputNumber>>(Cluster.CastValue<decimal?>(kvp.Key), ProgramSetBuilder<inputNumber>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF84 RID: 61316 RVA: 0x0033B87D File Offset: 0x00339A7D
		public static IEnumerable<KeyValuePair<Optional<decimal?>, ProgramSetBuilder<parsedNumber>>> ClusterOnInput(this ProgramSetBuilder<parsedNumber> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal?>, ProgramSetBuilder<parsedNumber>>(Cluster.CastValue<decimal?>(kvp.Key), ProgramSetBuilder<parsedNumber>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF85 RID: 61317 RVA: 0x0033B8AF File Offset: 0x00339AAF
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<b>>> ClusterOnInput(this ProgramSetBuilder<b> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<b>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<b>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF86 RID: 61318 RVA: 0x0033B8E1 File Offset: 0x00339AE1
		public static IEnumerable<KeyValuePair<Optional<LearningCacheSubstring>, ProgramSetBuilder<y>>> ClusterOnInput(this ProgramSetBuilder<y> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<LearningCacheSubstring>, ProgramSetBuilder<y>>(Cluster.CastValue<LearningCacheSubstring>(kvp.Key), ProgramSetBuilder<y>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF87 RID: 61319 RVA: 0x0033B913 File Offset: 0x00339B13
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<k>>> ClusterOnInput(this ProgramSetBuilder<k> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<k>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<k>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF88 RID: 61320 RVA: 0x0033B945 File Offset: 0x00339B45
		public static IEnumerable<KeyValuePair<Optional<CustomExtractor>, ProgramSetBuilder<externalExtractor>>> ClusterOnInput(this ProgramSetBuilder<externalExtractor> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<CustomExtractor>, ProgramSetBuilder<externalExtractor>>(Cluster.CastValue<CustomExtractor>(kvp.Key), ProgramSetBuilder<externalExtractor>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF89 RID: 61321 RVA: 0x0033B977 File Offset: 0x00339B77
		public static IEnumerable<KeyValuePair<Optional<RegularExpression>, ProgramSetBuilder<r>>> ClusterOnInput(this ProgramSetBuilder<r> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<RegularExpression>, ProgramSetBuilder<r>>(Cluster.CastValue<RegularExpression>(kvp.Key), ProgramSetBuilder<r>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF8A RID: 61322 RVA: 0x0033B9A9 File Offset: 0x00339BA9
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<s>>> ClusterOnInput(this ProgramSetBuilder<s> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<s>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<s>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF8B RID: 61323 RVA: 0x0033B9DB File Offset: 0x00339BDB
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<name>>> ClusterOnInput(this ProgramSetBuilder<name> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<name>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<name>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF8C RID: 61324 RVA: 0x0033BA0D File Offset: 0x00339C0D
		public static IEnumerable<KeyValuePair<Optional<RoundingSpec>, ProgramSetBuilder<roundingSpec>>> ClusterOnInput(this ProgramSetBuilder<roundingSpec> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<RoundingSpec>, ProgramSetBuilder<roundingSpec>>(Cluster.CastValue<RoundingSpec>(kvp.Key), ProgramSetBuilder<roundingSpec>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF8D RID: 61325 RVA: 0x0033BA3F File Offset: 0x00339C3F
		public static IEnumerable<KeyValuePair<Optional<DateTimeRoundingSpec>, ProgramSetBuilder<dtRoundingSpec>>> ClusterOnInput(this ProgramSetBuilder<dtRoundingSpec> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<DateTimeRoundingSpec>, ProgramSetBuilder<dtRoundingSpec>>(Cluster.CastValue<DateTimeRoundingSpec>(kvp.Key), ProgramSetBuilder<dtRoundingSpec>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF8E RID: 61326 RVA: 0x0033BA71 File Offset: 0x00339C71
		public static IEnumerable<KeyValuePair<Optional<uint?>, ProgramSetBuilder<minTrailingZeros>>> ClusterOnInput(this ProgramSetBuilder<minTrailingZeros> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<uint?>, ProgramSetBuilder<minTrailingZeros>>(Cluster.CastValue<uint?>(kvp.Key), ProgramSetBuilder<minTrailingZeros>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF8F RID: 61327 RVA: 0x0033BAA3 File Offset: 0x00339CA3
		public static IEnumerable<KeyValuePair<Optional<uint?>, ProgramSetBuilder<maxTrailingZeros>>> ClusterOnInput(this ProgramSetBuilder<maxTrailingZeros> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<uint?>, ProgramSetBuilder<maxTrailingZeros>>(Cluster.CastValue<uint?>(kvp.Key), ProgramSetBuilder<maxTrailingZeros>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF90 RID: 61328 RVA: 0x0033BAD5 File Offset: 0x00339CD5
		public static IEnumerable<KeyValuePair<Optional<uint?>, ProgramSetBuilder<minTrailingZerosAndWhitespace>>> ClusterOnInput(this ProgramSetBuilder<minTrailingZerosAndWhitespace> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<uint?>, ProgramSetBuilder<minTrailingZerosAndWhitespace>>(Cluster.CastValue<uint?>(kvp.Key), ProgramSetBuilder<minTrailingZerosAndWhitespace>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF91 RID: 61329 RVA: 0x0033BB07 File Offset: 0x00339D07
		public static IEnumerable<KeyValuePair<Optional<uint?>, ProgramSetBuilder<minLeadingZeros>>> ClusterOnInput(this ProgramSetBuilder<minLeadingZeros> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<uint?>, ProgramSetBuilder<minLeadingZeros>>(Cluster.CastValue<uint?>(kvp.Key), ProgramSetBuilder<minLeadingZeros>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF92 RID: 61330 RVA: 0x0033BB39 File Offset: 0x00339D39
		public static IEnumerable<KeyValuePair<Optional<uint?>, ProgramSetBuilder<minLeadingZerosAndWhitespace>>> ClusterOnInput(this ProgramSetBuilder<minLeadingZerosAndWhitespace> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<uint?>, ProgramSetBuilder<minLeadingZerosAndWhitespace>>(Cluster.CastValue<uint?>(kvp.Key), ProgramSetBuilder<minLeadingZerosAndWhitespace>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF93 RID: 61331 RVA: 0x0033BB6B File Offset: 0x00339D6B
		public static IEnumerable<KeyValuePair<Optional<char?>, ProgramSetBuilder<numberFormatSeparatorChar>>> ClusterOnInput(this ProgramSetBuilder<numberFormatSeparatorChar> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<char?>, ProgramSetBuilder<numberFormatSeparatorChar>>(Cluster.CastValue<char?>(kvp.Key), ProgramSetBuilder<numberFormatSeparatorChar>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF94 RID: 61332 RVA: 0x0033BB9D File Offset: 0x00339D9D
		public static IEnumerable<KeyValuePair<Optional<NumberFormatDetails>, ProgramSetBuilder<numberFormatDetails>>> ClusterOnInput(this ProgramSetBuilder<numberFormatDetails> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<NumberFormatDetails>, ProgramSetBuilder<numberFormatDetails>>(Cluster.CastValue<NumberFormatDetails>(kvp.Key), ProgramSetBuilder<numberFormatDetails>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF95 RID: 61333 RVA: 0x0033BBCF File Offset: 0x00339DCF
		public static IEnumerable<KeyValuePair<Optional<NumberFormat>, ProgramSetBuilder<numberFormat>>> ClusterOnInput(this ProgramSetBuilder<numberFormat> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<NumberFormat>, ProgramSetBuilder<numberFormat>>(Cluster.CastValue<NumberFormat>(kvp.Key), ProgramSetBuilder<numberFormat>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF96 RID: 61334 RVA: 0x0033BC01 File Offset: 0x00339E01
		public static IEnumerable<KeyValuePair<Optional<NumberFormat>, ProgramSetBuilder<numberFormatLiteral>>> ClusterOnInput(this ProgramSetBuilder<numberFormatLiteral> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<NumberFormat>, ProgramSetBuilder<numberFormatLiteral>>(Cluster.CastValue<NumberFormat>(kvp.Key), ProgramSetBuilder<numberFormatLiteral>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF97 RID: 61335 RVA: 0x0033BC33 File Offset: 0x00339E33
		public static IEnumerable<KeyValuePair<Optional<DateTimeFormat>, ProgramSetBuilder<outputDtFormat>>> ClusterOnInput(this ProgramSetBuilder<outputDtFormat> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<DateTimeFormat>, ProgramSetBuilder<outputDtFormat>>(Cluster.CastValue<DateTimeFormat>(kvp.Key), ProgramSetBuilder<outputDtFormat>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF98 RID: 61336 RVA: 0x0033BC65 File Offset: 0x00339E65
		public static IEnumerable<KeyValuePair<Optional<DateTimeFormat[]>, ProgramSetBuilder<inputDtFormats>>> ClusterOnInput(this ProgramSetBuilder<inputDtFormats> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<DateTimeFormat[]>, ProgramSetBuilder<inputDtFormats>>(Cluster.CastValue<DateTimeFormat[]>(kvp.Key), ProgramSetBuilder<inputDtFormats>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF99 RID: 61337 RVA: 0x0033BC97 File Offset: 0x00339E97
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyDictionary<Optional<string>, string>>, ProgramSetBuilder<lookupDictionary>>> ClusterOnInput(this ProgramSetBuilder<lookupDictionary> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IReadOnlyDictionary<Optional<string>, string>>, ProgramSetBuilder<lookupDictionary>>(Cluster.CastValue<IReadOnlyDictionary<Optional<string>, string>>(kvp.Key), ProgramSetBuilder<lookupDictionary>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF9A RID: 61338 RVA: 0x0033BCC9 File Offset: 0x00339EC9
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<idx>>> ClusterOnInput(this ProgramSetBuilder<idx> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<idx>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<idx>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF9B RID: 61339 RVA: 0x0033BCFB File Offset: 0x00339EFB
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<columnIdx>>> ClusterOnInput(this ProgramSetBuilder<columnIdx> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<columnIdx>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<columnIdx>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF9C RID: 61340 RVA: 0x0033BD2D File Offset: 0x00339F2D
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<_LetB0>>> ClusterOnInput(this ProgramSetBuilder<_LetB0> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<_LetB0>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF9D RID: 61341 RVA: 0x0033BD5F File Offset: 0x00339F5F
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<_LetB1>>> ClusterOnInput(this ProgramSetBuilder<_LetB1> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<_LetB1>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<_LetB1>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF9E RID: 61342 RVA: 0x0033BD91 File Offset: 0x00339F91
		public static IEnumerable<KeyValuePair<Optional<uint?>, ProgramSetBuilder<_LetB2>>> ClusterOnInput(this ProgramSetBuilder<_LetB2> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<uint?>, ProgramSetBuilder<_LetB2>>(Cluster.CastValue<uint?>(kvp.Key), ProgramSetBuilder<_LetB2>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EF9F RID: 61343 RVA: 0x0033BDC3 File Offset: 0x00339FC3
		public static IEnumerable<KeyValuePair<Optional<Record<uint?, uint?>?>, ProgramSetBuilder<_LetB3>>> ClusterOnInput(this ProgramSetBuilder<_LetB3> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Record<uint?, uint?>?>, ProgramSetBuilder<_LetB3>>(Cluster.CastValue<Record<uint?, uint?>?>(kvp.Key), ProgramSetBuilder<_LetB3>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EFA0 RID: 61344 RVA: 0x0033BDF5 File Offset: 0x00339FF5
		public static IEnumerable<KeyValuePair<Optional<Record<uint?, uint?>?>, ProgramSetBuilder<_LetB4>>> ClusterOnInput(this ProgramSetBuilder<_LetB4> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Record<uint?, uint?>?>, ProgramSetBuilder<_LetB4>>(Cluster.CastValue<Record<uint?, uint?>?>(kvp.Key), ProgramSetBuilder<_LetB4>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EFA1 RID: 61345 RVA: 0x0033BE27 File Offset: 0x0033A027
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<_LetB5>>> ClusterOnInput(this ProgramSetBuilder<_LetB5> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ValueSubstring>, ProgramSetBuilder<_LetB5>>(Cluster.CastValue<ValueSubstring>(kvp.Key), ProgramSetBuilder<_LetB5>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EFA2 RID: 61346 RVA: 0x0033BE59 File Offset: 0x0033A059
		public static IEnumerable<KeyValuePair<Optional<Record<uint?, uint?>?>, ProgramSetBuilder<_LetB6>>> ClusterOnInput(this ProgramSetBuilder<_LetB6> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Record<uint?, uint?>?>, ProgramSetBuilder<_LetB6>>(Cluster.CastValue<Record<uint?, uint?>?>(kvp.Key), ProgramSetBuilder<_LetB6>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EFA3 RID: 61347 RVA: 0x0033BE8B File Offset: 0x0033A08B
		public static IEnumerable<KeyValuePair<Optional<Record<uint?, uint?>?>, ProgramSetBuilder<_LetB7>>> ClusterOnInput(this ProgramSetBuilder<_LetB7> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Record<uint?, uint?>?>, ProgramSetBuilder<_LetB7>>(Cluster.CastValue<Record<uint?, uint?>?>(kvp.Key), ProgramSetBuilder<_LetB7>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600EFA4 RID: 61348 RVA: 0x0033BEBD File Offset: 0x0033A0BD
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<@switch>>> ClusterOnInputTuple(this ProgramSetBuilder<@switch> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<@switch>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<@switch>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFA5 RID: 61349 RVA: 0x0033BEEF File Offset: 0x0033A0EF
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<ite>>> ClusterOnInputTuple(this ProgramSetBuilder<ite> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<ite>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<ite>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFA6 RID: 61350 RVA: 0x0033BF21 File Offset: 0x0033A121
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<pred>>> ClusterOnInputTuple(this ProgramSetBuilder<pred> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<pred>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<pred>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFA7 RID: 61351 RVA: 0x0033BF53 File Offset: 0x0033A153
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<st>>> ClusterOnInputTuple(this ProgramSetBuilder<st> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<st>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<st>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFA8 RID: 61352 RVA: 0x0033BF85 File Offset: 0x0033A185
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<e>>> ClusterOnInputTuple(this ProgramSetBuilder<e> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<e>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<e>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFA9 RID: 61353 RVA: 0x0033BFB7 File Offset: 0x0033A1B7
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<f>>> ClusterOnInputTuple(this ProgramSetBuilder<f> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<f>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<f>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFAA RID: 61354 RVA: 0x0033BFE9 File Offset: 0x0033A1E9
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<letOptions>>> ClusterOnInputTuple(this ProgramSetBuilder<letOptions> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<letOptions>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<letOptions>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFAB RID: 61355 RVA: 0x0033C01B File Offset: 0x0033A21B
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<v>>> ClusterOnInputTuple(this ProgramSetBuilder<v> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<v>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<v>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFAC RID: 61356 RVA: 0x0033C04D File Offset: 0x0033A24D
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<indexInputString>>> ClusterOnInputTuple(this ProgramSetBuilder<indexInputString> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<indexInputString>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<indexInputString>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFAD RID: 61357 RVA: 0x0033C07F File Offset: 0x0033A27F
		public static IEnumerable<KeyValuePair<Optional<object>[], ProgramSetBuilder<lookupInput>>> ClusterOnInputTuple(this ProgramSetBuilder<lookupInput> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<object>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<object>>(Cluster.CastValue<object>));
				}
				return new KeyValuePair<Optional<object>[], ProgramSetBuilder<lookupInput>>(key.Select(func).ToArray<Optional<object>>(), ProgramSetBuilder<lookupInput>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFAE RID: 61358 RVA: 0x0033C0B1 File Offset: 0x0033A2B1
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<conv>>> ClusterOnInputTuple(this ProgramSetBuilder<conv> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<conv>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<conv>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFAF RID: 61359 RVA: 0x0033C0E3 File Offset: 0x0033A2E3
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<rangeString>>> ClusterOnInputTuple(this ProgramSetBuilder<rangeString> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<rangeString>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<rangeString>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFB0 RID: 61360 RVA: 0x0033C115 File Offset: 0x0033A315
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<rangeSubstring>>> ClusterOnInputTuple(this ProgramSetBuilder<rangeSubstring> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<rangeSubstring>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<rangeSubstring>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFB1 RID: 61361 RVA: 0x0033C147 File Offset: 0x0033A347
		public static IEnumerable<KeyValuePair<Optional<decimal?>[], ProgramSetBuilder<rangeNumber>>> ClusterOnInputTuple(this ProgramSetBuilder<rangeNumber> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal?>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<decimal?>>(Cluster.CastValue<decimal?>));
				}
				return new KeyValuePair<Optional<decimal?>[], ProgramSetBuilder<rangeNumber>>(key.Select(func).ToArray<Optional<decimal?>>(), ProgramSetBuilder<rangeNumber>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFB2 RID: 61362 RVA: 0x0033C179 File Offset: 0x0033A379
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<dtRangeString>>> ClusterOnInputTuple(this ProgramSetBuilder<dtRangeString> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<dtRangeString>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<dtRangeString>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFB3 RID: 61363 RVA: 0x0033C1AB File Offset: 0x0033A3AB
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<dtRangeSubstring>>> ClusterOnInputTuple(this ProgramSetBuilder<dtRangeSubstring> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<dtRangeSubstring>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<dtRangeSubstring>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFB4 RID: 61364 RVA: 0x0033C1DD File Offset: 0x0033A3DD
		public static IEnumerable<KeyValuePair<Optional<PartialDateTime>[], ProgramSetBuilder<rangeDateTime>>> ClusterOnInputTuple(this ProgramSetBuilder<rangeDateTime> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<PartialDateTime>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<PartialDateTime>>(Cluster.CastValue<PartialDateTime>));
				}
				return new KeyValuePair<Optional<PartialDateTime>[], ProgramSetBuilder<rangeDateTime>>(key.Select(func).ToArray<Optional<PartialDateTime>>(), ProgramSetBuilder<rangeDateTime>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFB5 RID: 61365 RVA: 0x0033C20F File Offset: 0x0033A40F
		public static IEnumerable<KeyValuePair<Optional<PartialDateTime>[], ProgramSetBuilder<datetime>>> ClusterOnInputTuple(this ProgramSetBuilder<datetime> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<PartialDateTime>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<PartialDateTime>>(Cluster.CastValue<PartialDateTime>));
				}
				return new KeyValuePair<Optional<PartialDateTime>[], ProgramSetBuilder<datetime>>(key.Select(func).ToArray<Optional<PartialDateTime>>(), ProgramSetBuilder<datetime>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFB6 RID: 61366 RVA: 0x0033C241 File Offset: 0x0033A441
		public static IEnumerable<KeyValuePair<Optional<PartialDateTime>[], ProgramSetBuilder<inputDateTime>>> ClusterOnInputTuple(this ProgramSetBuilder<inputDateTime> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<PartialDateTime>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<PartialDateTime>>(Cluster.CastValue<PartialDateTime>));
				}
				return new KeyValuePair<Optional<PartialDateTime>[], ProgramSetBuilder<inputDateTime>>(key.Select(func).ToArray<Optional<PartialDateTime>>(), ProgramSetBuilder<inputDateTime>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFB7 RID: 61367 RVA: 0x0033C273 File Offset: 0x0033A473
		public static IEnumerable<KeyValuePair<Optional<PartialDateTime>[], ProgramSetBuilder<parsedDateTime>>> ClusterOnInputTuple(this ProgramSetBuilder<parsedDateTime> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<PartialDateTime>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<PartialDateTime>>(Cluster.CastValue<PartialDateTime>));
				}
				return new KeyValuePair<Optional<PartialDateTime>[], ProgramSetBuilder<parsedDateTime>>(key.Select(func).ToArray<Optional<PartialDateTime>>(), ProgramSetBuilder<parsedDateTime>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFB8 RID: 61368 RVA: 0x0033C2A5 File Offset: 0x0033A4A5
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<SS>>> ClusterOnInputTuple(this ProgramSetBuilder<SS> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<SS>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<SS>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFB9 RID: 61369 RVA: 0x0033C2D7 File Offset: 0x0033A4D7
		public static IEnumerable<KeyValuePair<Optional<Record<uint?, uint?>?>[], ProgramSetBuilder<PP>>> ClusterOnInputTuple(this ProgramSetBuilder<PP> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Record<uint?, uint?>?>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<Record<uint?, uint?>?>>(Cluster.CastValue<Record<uint?, uint?>?>));
				}
				return new KeyValuePair<Optional<Record<uint?, uint?>?>[], ProgramSetBuilder<PP>>(key.Select(func).ToArray<Optional<Record<uint?, uint?>?>>(), ProgramSetBuilder<PP>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFBA RID: 61370 RVA: 0x0033C309 File Offset: 0x0033A509
		public static IEnumerable<KeyValuePair<Optional<uint?>[], ProgramSetBuilder<pos>>> ClusterOnInputTuple(this ProgramSetBuilder<pos> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<uint?>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<uint?>>(Cluster.CastValue<uint?>));
				}
				return new KeyValuePair<Optional<uint?>[], ProgramSetBuilder<pos>>(key.Select(func).ToArray<Optional<uint?>>(), ProgramSetBuilder<pos>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFBB RID: 61371 RVA: 0x0033C33B File Offset: 0x0033A53B
		public static IEnumerable<KeyValuePair<Optional<Record<RegularExpression, RegularExpression>?>[], ProgramSetBuilder<regexPair>>> ClusterOnInputTuple(this ProgramSetBuilder<regexPair> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Record<RegularExpression, RegularExpression>?>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<Record<RegularExpression, RegularExpression>?>>(Cluster.CastValue<Record<RegularExpression, RegularExpression>?>));
				}
				return new KeyValuePair<Optional<Record<RegularExpression, RegularExpression>?>[], ProgramSetBuilder<regexPair>>(key.Select(func).ToArray<Optional<Record<RegularExpression, RegularExpression>?>>(), ProgramSetBuilder<regexPair>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFBC RID: 61372 RVA: 0x0033C36D File Offset: 0x0033A56D
		public static IEnumerable<KeyValuePair<Optional<decimal?>[], ProgramSetBuilder<number>>> ClusterOnInputTuple(this ProgramSetBuilder<number> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal?>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<decimal?>>(Cluster.CastValue<decimal?>));
				}
				return new KeyValuePair<Optional<decimal?>[], ProgramSetBuilder<number>>(key.Select(func).ToArray<Optional<decimal?>>(), ProgramSetBuilder<number>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFBD RID: 61373 RVA: 0x0033C39F File Offset: 0x0033A59F
		public static IEnumerable<KeyValuePair<Optional<decimal?>[], ProgramSetBuilder<castToNumber>>> ClusterOnInputTuple(this ProgramSetBuilder<castToNumber> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal?>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<decimal?>>(Cluster.CastValue<decimal?>));
				}
				return new KeyValuePair<Optional<decimal?>[], ProgramSetBuilder<castToNumber>>(key.Select(func).ToArray<Optional<decimal?>>(), ProgramSetBuilder<castToNumber>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFBE RID: 61374 RVA: 0x0033C3D1 File Offset: 0x0033A5D1
		public static IEnumerable<KeyValuePair<Optional<decimal?>[], ProgramSetBuilder<inputNumber>>> ClusterOnInputTuple(this ProgramSetBuilder<inputNumber> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal?>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<decimal?>>(Cluster.CastValue<decimal?>));
				}
				return new KeyValuePair<Optional<decimal?>[], ProgramSetBuilder<inputNumber>>(key.Select(func).ToArray<Optional<decimal?>>(), ProgramSetBuilder<inputNumber>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFBF RID: 61375 RVA: 0x0033C403 File Offset: 0x0033A603
		public static IEnumerable<KeyValuePair<Optional<decimal?>[], ProgramSetBuilder<parsedNumber>>> ClusterOnInputTuple(this ProgramSetBuilder<parsedNumber> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal?>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<decimal?>>(Cluster.CastValue<decimal?>));
				}
				return new KeyValuePair<Optional<decimal?>[], ProgramSetBuilder<parsedNumber>>(key.Select(func).ToArray<Optional<decimal?>>(), ProgramSetBuilder<parsedNumber>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFC0 RID: 61376 RVA: 0x0033C435 File Offset: 0x0033A635
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<b>>> ClusterOnInputTuple(this ProgramSetBuilder<b> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<b>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<b>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFC1 RID: 61377 RVA: 0x0033C467 File Offset: 0x0033A667
		public static IEnumerable<KeyValuePair<Optional<LearningCacheSubstring>[], ProgramSetBuilder<y>>> ClusterOnInputTuple(this ProgramSetBuilder<y> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<LearningCacheSubstring>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<LearningCacheSubstring>>(Cluster.CastValue<LearningCacheSubstring>));
				}
				return new KeyValuePair<Optional<LearningCacheSubstring>[], ProgramSetBuilder<y>>(key.Select(func).ToArray<Optional<LearningCacheSubstring>>(), ProgramSetBuilder<y>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFC2 RID: 61378 RVA: 0x0033C499 File Offset: 0x0033A699
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<k>>> ClusterOnInputTuple(this ProgramSetBuilder<k> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<9>__CastValue) == null)
				{
					func = (Cluster.<>O.<9>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<k>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<k>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFC3 RID: 61379 RVA: 0x0033C4CB File Offset: 0x0033A6CB
		public static IEnumerable<KeyValuePair<Optional<CustomExtractor>[], ProgramSetBuilder<externalExtractor>>> ClusterOnInputTuple(this ProgramSetBuilder<externalExtractor> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<CustomExtractor>> func;
				if ((func = Cluster.<>O.<10>__CastValue) == null)
				{
					func = (Cluster.<>O.<10>__CastValue = new Func<object, Optional<CustomExtractor>>(Cluster.CastValue<CustomExtractor>));
				}
				return new KeyValuePair<Optional<CustomExtractor>[], ProgramSetBuilder<externalExtractor>>(key.Select(func).ToArray<Optional<CustomExtractor>>(), ProgramSetBuilder<externalExtractor>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFC4 RID: 61380 RVA: 0x0033C4FD File Offset: 0x0033A6FD
		public static IEnumerable<KeyValuePair<Optional<RegularExpression>[], ProgramSetBuilder<r>>> ClusterOnInputTuple(this ProgramSetBuilder<r> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<RegularExpression>> func;
				if ((func = Cluster.<>O.<11>__CastValue) == null)
				{
					func = (Cluster.<>O.<11>__CastValue = new Func<object, Optional<RegularExpression>>(Cluster.CastValue<RegularExpression>));
				}
				return new KeyValuePair<Optional<RegularExpression>[], ProgramSetBuilder<r>>(key.Select(func).ToArray<Optional<RegularExpression>>(), ProgramSetBuilder<r>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFC5 RID: 61381 RVA: 0x0033C52F File Offset: 0x0033A72F
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<s>>> ClusterOnInputTuple(this ProgramSetBuilder<s> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<12>__CastValue) == null)
				{
					func = (Cluster.<>O.<12>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<s>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<s>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFC6 RID: 61382 RVA: 0x0033C561 File Offset: 0x0033A761
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<name>>> ClusterOnInputTuple(this ProgramSetBuilder<name> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<12>__CastValue) == null)
				{
					func = (Cluster.<>O.<12>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<name>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<name>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFC7 RID: 61383 RVA: 0x0033C593 File Offset: 0x0033A793
		public static IEnumerable<KeyValuePair<Optional<RoundingSpec>[], ProgramSetBuilder<roundingSpec>>> ClusterOnInputTuple(this ProgramSetBuilder<roundingSpec> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<RoundingSpec>> func;
				if ((func = Cluster.<>O.<13>__CastValue) == null)
				{
					func = (Cluster.<>O.<13>__CastValue = new Func<object, Optional<RoundingSpec>>(Cluster.CastValue<RoundingSpec>));
				}
				return new KeyValuePair<Optional<RoundingSpec>[], ProgramSetBuilder<roundingSpec>>(key.Select(func).ToArray<Optional<RoundingSpec>>(), ProgramSetBuilder<roundingSpec>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFC8 RID: 61384 RVA: 0x0033C5C5 File Offset: 0x0033A7C5
		public static IEnumerable<KeyValuePair<Optional<DateTimeRoundingSpec>[], ProgramSetBuilder<dtRoundingSpec>>> ClusterOnInputTuple(this ProgramSetBuilder<dtRoundingSpec> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<DateTimeRoundingSpec>> func;
				if ((func = Cluster.<>O.<14>__CastValue) == null)
				{
					func = (Cluster.<>O.<14>__CastValue = new Func<object, Optional<DateTimeRoundingSpec>>(Cluster.CastValue<DateTimeRoundingSpec>));
				}
				return new KeyValuePair<Optional<DateTimeRoundingSpec>[], ProgramSetBuilder<dtRoundingSpec>>(key.Select(func).ToArray<Optional<DateTimeRoundingSpec>>(), ProgramSetBuilder<dtRoundingSpec>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFC9 RID: 61385 RVA: 0x0033C5F7 File Offset: 0x0033A7F7
		public static IEnumerable<KeyValuePair<Optional<uint?>[], ProgramSetBuilder<minTrailingZeros>>> ClusterOnInputTuple(this ProgramSetBuilder<minTrailingZeros> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<uint?>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<uint?>>(Cluster.CastValue<uint?>));
				}
				return new KeyValuePair<Optional<uint?>[], ProgramSetBuilder<minTrailingZeros>>(key.Select(func).ToArray<Optional<uint?>>(), ProgramSetBuilder<minTrailingZeros>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFCA RID: 61386 RVA: 0x0033C629 File Offset: 0x0033A829
		public static IEnumerable<KeyValuePair<Optional<uint?>[], ProgramSetBuilder<maxTrailingZeros>>> ClusterOnInputTuple(this ProgramSetBuilder<maxTrailingZeros> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<uint?>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<uint?>>(Cluster.CastValue<uint?>));
				}
				return new KeyValuePair<Optional<uint?>[], ProgramSetBuilder<maxTrailingZeros>>(key.Select(func).ToArray<Optional<uint?>>(), ProgramSetBuilder<maxTrailingZeros>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFCB RID: 61387 RVA: 0x0033C65B File Offset: 0x0033A85B
		public static IEnumerable<KeyValuePair<Optional<uint?>[], ProgramSetBuilder<minTrailingZerosAndWhitespace>>> ClusterOnInputTuple(this ProgramSetBuilder<minTrailingZerosAndWhitespace> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<uint?>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<uint?>>(Cluster.CastValue<uint?>));
				}
				return new KeyValuePair<Optional<uint?>[], ProgramSetBuilder<minTrailingZerosAndWhitespace>>(key.Select(func).ToArray<Optional<uint?>>(), ProgramSetBuilder<minTrailingZerosAndWhitespace>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFCC RID: 61388 RVA: 0x0033C68D File Offset: 0x0033A88D
		public static IEnumerable<KeyValuePair<Optional<uint?>[], ProgramSetBuilder<minLeadingZeros>>> ClusterOnInputTuple(this ProgramSetBuilder<minLeadingZeros> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<uint?>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<uint?>>(Cluster.CastValue<uint?>));
				}
				return new KeyValuePair<Optional<uint?>[], ProgramSetBuilder<minLeadingZeros>>(key.Select(func).ToArray<Optional<uint?>>(), ProgramSetBuilder<minLeadingZeros>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFCD RID: 61389 RVA: 0x0033C6BF File Offset: 0x0033A8BF
		public static IEnumerable<KeyValuePair<Optional<uint?>[], ProgramSetBuilder<minLeadingZerosAndWhitespace>>> ClusterOnInputTuple(this ProgramSetBuilder<minLeadingZerosAndWhitespace> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<uint?>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<uint?>>(Cluster.CastValue<uint?>));
				}
				return new KeyValuePair<Optional<uint?>[], ProgramSetBuilder<minLeadingZerosAndWhitespace>>(key.Select(func).ToArray<Optional<uint?>>(), ProgramSetBuilder<minLeadingZerosAndWhitespace>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFCE RID: 61390 RVA: 0x0033C6F1 File Offset: 0x0033A8F1
		public static IEnumerable<KeyValuePair<Optional<char?>[], ProgramSetBuilder<numberFormatSeparatorChar>>> ClusterOnInputTuple(this ProgramSetBuilder<numberFormatSeparatorChar> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<char?>> func;
				if ((func = Cluster.<>O.<15>__CastValue) == null)
				{
					func = (Cluster.<>O.<15>__CastValue = new Func<object, Optional<char?>>(Cluster.CastValue<char?>));
				}
				return new KeyValuePair<Optional<char?>[], ProgramSetBuilder<numberFormatSeparatorChar>>(key.Select(func).ToArray<Optional<char?>>(), ProgramSetBuilder<numberFormatSeparatorChar>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFCF RID: 61391 RVA: 0x0033C723 File Offset: 0x0033A923
		public static IEnumerable<KeyValuePair<Optional<NumberFormatDetails>[], ProgramSetBuilder<numberFormatDetails>>> ClusterOnInputTuple(this ProgramSetBuilder<numberFormatDetails> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<NumberFormatDetails>> func;
				if ((func = Cluster.<>O.<16>__CastValue) == null)
				{
					func = (Cluster.<>O.<16>__CastValue = new Func<object, Optional<NumberFormatDetails>>(Cluster.CastValue<NumberFormatDetails>));
				}
				return new KeyValuePair<Optional<NumberFormatDetails>[], ProgramSetBuilder<numberFormatDetails>>(key.Select(func).ToArray<Optional<NumberFormatDetails>>(), ProgramSetBuilder<numberFormatDetails>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFD0 RID: 61392 RVA: 0x0033C755 File Offset: 0x0033A955
		public static IEnumerable<KeyValuePair<Optional<NumberFormat>[], ProgramSetBuilder<numberFormat>>> ClusterOnInputTuple(this ProgramSetBuilder<numberFormat> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<NumberFormat>> func;
				if ((func = Cluster.<>O.<17>__CastValue) == null)
				{
					func = (Cluster.<>O.<17>__CastValue = new Func<object, Optional<NumberFormat>>(Cluster.CastValue<NumberFormat>));
				}
				return new KeyValuePair<Optional<NumberFormat>[], ProgramSetBuilder<numberFormat>>(key.Select(func).ToArray<Optional<NumberFormat>>(), ProgramSetBuilder<numberFormat>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFD1 RID: 61393 RVA: 0x0033C787 File Offset: 0x0033A987
		public static IEnumerable<KeyValuePair<Optional<NumberFormat>[], ProgramSetBuilder<numberFormatLiteral>>> ClusterOnInputTuple(this ProgramSetBuilder<numberFormatLiteral> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<NumberFormat>> func;
				if ((func = Cluster.<>O.<17>__CastValue) == null)
				{
					func = (Cluster.<>O.<17>__CastValue = new Func<object, Optional<NumberFormat>>(Cluster.CastValue<NumberFormat>));
				}
				return new KeyValuePair<Optional<NumberFormat>[], ProgramSetBuilder<numberFormatLiteral>>(key.Select(func).ToArray<Optional<NumberFormat>>(), ProgramSetBuilder<numberFormatLiteral>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFD2 RID: 61394 RVA: 0x0033C7B9 File Offset: 0x0033A9B9
		public static IEnumerable<KeyValuePair<Optional<DateTimeFormat>[], ProgramSetBuilder<outputDtFormat>>> ClusterOnInputTuple(this ProgramSetBuilder<outputDtFormat> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<DateTimeFormat>> func;
				if ((func = Cluster.<>O.<18>__CastValue) == null)
				{
					func = (Cluster.<>O.<18>__CastValue = new Func<object, Optional<DateTimeFormat>>(Cluster.CastValue<DateTimeFormat>));
				}
				return new KeyValuePair<Optional<DateTimeFormat>[], ProgramSetBuilder<outputDtFormat>>(key.Select(func).ToArray<Optional<DateTimeFormat>>(), ProgramSetBuilder<outputDtFormat>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFD3 RID: 61395 RVA: 0x0033C7EB File Offset: 0x0033A9EB
		public static IEnumerable<KeyValuePair<Optional<DateTimeFormat[]>[], ProgramSetBuilder<inputDtFormats>>> ClusterOnInputTuple(this ProgramSetBuilder<inputDtFormats> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<DateTimeFormat[]>> func;
				if ((func = Cluster.<>O.<19>__CastValue) == null)
				{
					func = (Cluster.<>O.<19>__CastValue = new Func<object, Optional<DateTimeFormat[]>>(Cluster.CastValue<DateTimeFormat[]>));
				}
				return new KeyValuePair<Optional<DateTimeFormat[]>[], ProgramSetBuilder<inputDtFormats>>(key.Select(func).ToArray<Optional<DateTimeFormat[]>>(), ProgramSetBuilder<inputDtFormats>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFD4 RID: 61396 RVA: 0x0033C81D File Offset: 0x0033AA1D
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyDictionary<Optional<string>, string>>[], ProgramSetBuilder<lookupDictionary>>> ClusterOnInputTuple(this ProgramSetBuilder<lookupDictionary> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IReadOnlyDictionary<Optional<string>, string>>> func;
				if ((func = Cluster.<>O.<20>__CastValue) == null)
				{
					func = (Cluster.<>O.<20>__CastValue = new Func<object, Optional<IReadOnlyDictionary<Optional<string>, string>>>(Cluster.CastValue<IReadOnlyDictionary<Optional<string>, string>>));
				}
				return new KeyValuePair<Optional<IReadOnlyDictionary<Optional<string>, string>>[], ProgramSetBuilder<lookupDictionary>>(key.Select(func).ToArray<Optional<IReadOnlyDictionary<Optional<string>, string>>>(), ProgramSetBuilder<lookupDictionary>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFD5 RID: 61397 RVA: 0x0033C84F File Offset: 0x0033AA4F
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<idx>>> ClusterOnInputTuple(this ProgramSetBuilder<idx> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<12>__CastValue) == null)
				{
					func = (Cluster.<>O.<12>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<idx>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<idx>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFD6 RID: 61398 RVA: 0x0033C881 File Offset: 0x0033AA81
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<columnIdx>>> ClusterOnInputTuple(this ProgramSetBuilder<columnIdx> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<9>__CastValue) == null)
				{
					func = (Cluster.<>O.<9>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<columnIdx>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<columnIdx>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFD7 RID: 61399 RVA: 0x0033C8B3 File Offset: 0x0033AAB3
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<_LetB0>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB0> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<_LetB0>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFD8 RID: 61400 RVA: 0x0033C8E5 File Offset: 0x0033AAE5
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<_LetB1>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB1> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<_LetB1>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<_LetB1>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFD9 RID: 61401 RVA: 0x0033C917 File Offset: 0x0033AB17
		public static IEnumerable<KeyValuePair<Optional<uint?>[], ProgramSetBuilder<_LetB2>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB2> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<uint?>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<uint?>>(Cluster.CastValue<uint?>));
				}
				return new KeyValuePair<Optional<uint?>[], ProgramSetBuilder<_LetB2>>(key.Select(func).ToArray<Optional<uint?>>(), ProgramSetBuilder<_LetB2>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFDA RID: 61402 RVA: 0x0033C949 File Offset: 0x0033AB49
		public static IEnumerable<KeyValuePair<Optional<Record<uint?, uint?>?>[], ProgramSetBuilder<_LetB3>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB3> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Record<uint?, uint?>?>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<Record<uint?, uint?>?>>(Cluster.CastValue<Record<uint?, uint?>?>));
				}
				return new KeyValuePair<Optional<Record<uint?, uint?>?>[], ProgramSetBuilder<_LetB3>>(key.Select(func).ToArray<Optional<Record<uint?, uint?>?>>(), ProgramSetBuilder<_LetB3>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFDB RID: 61403 RVA: 0x0033C97B File Offset: 0x0033AB7B
		public static IEnumerable<KeyValuePair<Optional<Record<uint?, uint?>?>[], ProgramSetBuilder<_LetB4>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB4> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Record<uint?, uint?>?>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<Record<uint?, uint?>?>>(Cluster.CastValue<Record<uint?, uint?>?>));
				}
				return new KeyValuePair<Optional<Record<uint?, uint?>?>[], ProgramSetBuilder<_LetB4>>(key.Select(func).ToArray<Optional<Record<uint?, uint?>?>>(), ProgramSetBuilder<_LetB4>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFDC RID: 61404 RVA: 0x0033C9AD File Offset: 0x0033ABAD
		public static IEnumerable<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<_LetB5>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB5> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ValueSubstring>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ValueSubstring>>(Cluster.CastValue<ValueSubstring>));
				}
				return new KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<_LetB5>>(key.Select(func).ToArray<Optional<ValueSubstring>>(), ProgramSetBuilder<_LetB5>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFDD RID: 61405 RVA: 0x0033C9DF File Offset: 0x0033ABDF
		public static IEnumerable<KeyValuePair<Optional<Record<uint?, uint?>?>[], ProgramSetBuilder<_LetB6>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB6> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Record<uint?, uint?>?>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<Record<uint?, uint?>?>>(Cluster.CastValue<Record<uint?, uint?>?>));
				}
				return new KeyValuePair<Optional<Record<uint?, uint?>?>[], ProgramSetBuilder<_LetB6>>(key.Select(func).ToArray<Optional<Record<uint?, uint?>?>>(), ProgramSetBuilder<_LetB6>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600EFDE RID: 61406 RVA: 0x0033CA11 File Offset: 0x0033AC11
		public static IEnumerable<KeyValuePair<Optional<Record<uint?, uint?>?>[], ProgramSetBuilder<_LetB7>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB7> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Record<uint?, uint?>?>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<Record<uint?, uint?>?>>(Cluster.CastValue<Record<uint?, uint?>?>));
				}
				return new KeyValuePair<Optional<Record<uint?, uint?>?>[], ProgramSetBuilder<_LetB7>>(key.Select(func).ToArray<Optional<Record<uint?, uint?>?>>(), ProgramSetBuilder<_LetB7>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x02001BED RID: 7149
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005A52 RID: 23122
			public static Func<object, Optional<ValueSubstring>> <0>__CastValue;

			// Token: 0x04005A53 RID: 23123
			public static Func<object, Optional<bool>> <1>__CastValue;

			// Token: 0x04005A54 RID: 23124
			public static Func<object, Optional<object>> <2>__CastValue;

			// Token: 0x04005A55 RID: 23125
			public static Func<object, Optional<decimal?>> <3>__CastValue;

			// Token: 0x04005A56 RID: 23126
			public static Func<object, Optional<PartialDateTime>> <4>__CastValue;

			// Token: 0x04005A57 RID: 23127
			public static Func<object, Optional<Record<uint?, uint?>?>> <5>__CastValue;

			// Token: 0x04005A58 RID: 23128
			public static Func<object, Optional<uint?>> <6>__CastValue;

			// Token: 0x04005A59 RID: 23129
			public static Func<object, Optional<Record<RegularExpression, RegularExpression>?>> <7>__CastValue;

			// Token: 0x04005A5A RID: 23130
			public static Func<object, Optional<LearningCacheSubstring>> <8>__CastValue;

			// Token: 0x04005A5B RID: 23131
			public static Func<object, Optional<int>> <9>__CastValue;

			// Token: 0x04005A5C RID: 23132
			public static Func<object, Optional<CustomExtractor>> <10>__CastValue;

			// Token: 0x04005A5D RID: 23133
			public static Func<object, Optional<RegularExpression>> <11>__CastValue;

			// Token: 0x04005A5E RID: 23134
			public static Func<object, Optional<string>> <12>__CastValue;

			// Token: 0x04005A5F RID: 23135
			public static Func<object, Optional<RoundingSpec>> <13>__CastValue;

			// Token: 0x04005A60 RID: 23136
			public static Func<object, Optional<DateTimeRoundingSpec>> <14>__CastValue;

			// Token: 0x04005A61 RID: 23137
			public static Func<object, Optional<char?>> <15>__CastValue;

			// Token: 0x04005A62 RID: 23138
			public static Func<object, Optional<NumberFormatDetails>> <16>__CastValue;

			// Token: 0x04005A63 RID: 23139
			public static Func<object, Optional<NumberFormat>> <17>__CastValue;

			// Token: 0x04005A64 RID: 23140
			public static Func<object, Optional<DateTimeFormat>> <18>__CastValue;

			// Token: 0x04005A65 RID: 23141
			public static Func<object, Optional<DateTimeFormat[]>> <19>__CastValue;

			// Token: 0x04005A66 RID: 23142
			public static Func<object, Optional<IReadOnlyDictionary<Optional<string>, string>>> <20>__CastValue;
		}
	}
}
