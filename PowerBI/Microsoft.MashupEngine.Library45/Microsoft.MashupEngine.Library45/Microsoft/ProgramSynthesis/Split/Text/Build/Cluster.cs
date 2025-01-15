using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Split.Text.Build
{
	// Token: 0x02001335 RID: 4917
	public static class Cluster
	{
		// Token: 0x060096D5 RID: 38613 RVA: 0x000AB0AF File Offset: 0x000A92AF
		private static Optional<T> CastValue<T>(object obj)
		{
			if (!(obj is Bottom))
			{
				return ((T)((object)obj)).Some<T>();
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x060096D6 RID: 38614 RVA: 0x00203777 File Offset: 0x00201977
		public static IEnumerable<KeyValuePair<Optional<SplitCell[]>, ProgramSetBuilder<regionSplit>>> ClusterOnInput(this ProgramSetBuilder<regionSplit> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SplitCell[]>, ProgramSetBuilder<regionSplit>>(Cluster.CastValue<SplitCell[]>(kvp.Key), ProgramSetBuilder<regionSplit>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096D7 RID: 38615 RVA: 0x002037A9 File Offset: 0x002019A9
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<splitMatches>>> ClusterOnInput(this ProgramSetBuilder<splitMatches> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<splitMatches>>(Cluster.CastValue<MatchRecord>(kvp.Key), ProgramSetBuilder<splitMatches>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096D8 RID: 38616 RVA: 0x002037DB File Offset: 0x002019DB
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<multipleMatches>>> ClusterOnInput(this ProgramSetBuilder<multipleMatches> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<multipleMatches>>(Cluster.CastValue<MatchRecord>(kvp.Key), ProgramSetBuilder<multipleMatches>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096D9 RID: 38617 RVA: 0x0020380D File Offset: 0x00201A0D
		public static IEnumerable<KeyValuePair<Optional<List<MatchRecord>>, ProgramSetBuilder<delimiterList>>> ClusterOnInput(this ProgramSetBuilder<delimiterList> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<List<MatchRecord>>, ProgramSetBuilder<delimiterList>>(Cluster.CastValue<List<MatchRecord>>(kvp.Key), ProgramSetBuilder<delimiterList>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096DA RID: 38618 RVA: 0x0020383F File Offset: 0x00201A3F
		public static IEnumerable<KeyValuePair<Optional<Record<int, int, int, int>[]>, ProgramSetBuilder<extractionPoints>>> ClusterOnInput(this ProgramSetBuilder<extractionPoints> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Record<int, int, int, int>[]>, ProgramSetBuilder<extractionPoints>>(Cluster.CastValue<Record<int, int, int, int>[]>(kvp.Key), ProgramSetBuilder<extractionPoints>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096DB RID: 38619 RVA: 0x00203871 File Offset: 0x00201A71
		public static IEnumerable<KeyValuePair<Optional<Record<int, int, int, int>?>, ProgramSetBuilder<cndExtPoint>>> ClusterOnInput(this ProgramSetBuilder<cndExtPoint> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Record<int, int, int, int>?>, ProgramSetBuilder<cndExtPoint>>(Cluster.CastValue<Record<int, int, int, int>?>(kvp.Key), ProgramSetBuilder<cndExtPoint>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096DC RID: 38620 RVA: 0x002038A3 File Offset: 0x00201AA3
		public static IEnumerable<KeyValuePair<Optional<Record<int, int, int, int>?>, ProgramSetBuilder<extPoint>>> ClusterOnInput(this ProgramSetBuilder<extPoint> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Record<int, int, int, int>?>, ProgramSetBuilder<extPoint>>(Cluster.CastValue<Record<int, int, int, int>?>(kvp.Key), ProgramSetBuilder<extPoint>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096DD RID: 38621 RVA: 0x002038D5 File Offset: 0x00201AD5
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<pred>>> ClusterOnInput(this ProgramSetBuilder<pred> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<pred>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<pred>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096DE RID: 38622 RVA: 0x00203907 File Offset: 0x00201B07
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<pattern>>> ClusterOnInput(this ProgramSetBuilder<pattern> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<pattern>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<pattern>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096DF RID: 38623 RVA: 0x00203939 File Offset: 0x00201B39
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<d>>> ClusterOnInput(this ProgramSetBuilder<d> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<d>>(Cluster.CastValue<MatchRecord>(kvp.Key), ProgramSetBuilder<d>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096E0 RID: 38624 RVA: 0x0020396B File Offset: 0x00201B6B
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<c>>> ClusterOnInput(this ProgramSetBuilder<c> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<c>>(Cluster.CastValue<MatchRecord>(kvp.Key), ProgramSetBuilder<c>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096E1 RID: 38625 RVA: 0x0020399D File Offset: 0x00201B9D
		public static IEnumerable<KeyValuePair<Optional<QuotingConfiguration>, ProgramSetBuilder<quotingConf>>> ClusterOnInput(this ProgramSetBuilder<quotingConf> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<QuotingConfiguration>, ProgramSetBuilder<quotingConf>>(Cluster.CastValue<QuotingConfiguration>(kvp.Key), ProgramSetBuilder<quotingConf>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096E2 RID: 38626 RVA: 0x002039CF File Offset: 0x00201BCF
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<constantDelimiterMatches>>> ClusterOnInput(this ProgramSetBuilder<constantDelimiterMatches> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<constantDelimiterMatches>>(Cluster.CastValue<MatchRecord>(kvp.Key), ProgramSetBuilder<constantDelimiterMatches>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096E3 RID: 38627 RVA: 0x00203A01 File Offset: 0x00201C01
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<r>>> ClusterOnInput(this ProgramSetBuilder<r> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<r>>(Cluster.CastValue<MatchRecord>(kvp.Key), ProgramSetBuilder<r>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096E4 RID: 38628 RVA: 0x00203A33 File Offset: 0x00201C33
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<regexMatch>>> ClusterOnInput(this ProgramSetBuilder<regexMatch> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<regexMatch>>(Cluster.CastValue<MatchRecord>(kvp.Key), ProgramSetBuilder<regexMatch>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096E5 RID: 38629 RVA: 0x00203A65 File Offset: 0x00201C65
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<fieldMatch>>> ClusterOnInput(this ProgramSetBuilder<fieldMatch> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<fieldMatch>>(Cluster.CastValue<MatchRecord>(kvp.Key), ProgramSetBuilder<fieldMatch>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096E6 RID: 38630 RVA: 0x00203A97 File Offset: 0x00201C97
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<fixedWidthMatches>>> ClusterOnInput(this ProgramSetBuilder<fixedWidthMatches> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<MatchRecord>, ProgramSetBuilder<fixedWidthMatches>>(Cluster.CastValue<MatchRecord>(kvp.Key), ProgramSetBuilder<fixedWidthMatches>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096E7 RID: 38631 RVA: 0x00203AC9 File Offset: 0x00201CC9
		public static IEnumerable<KeyValuePair<Optional<object>, ProgramSetBuilder<gen_Concat>>> ClusterOnInput(this ProgramSetBuilder<gen_Concat> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<object>, ProgramSetBuilder<gen_Concat>>(Cluster.CastValue<object>(kvp.Key), ProgramSetBuilder<gen_Concat>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096E8 RID: 38632 RVA: 0x00203AFB File Offset: 0x00201CFB
		public static IEnumerable<KeyValuePair<Optional<object>, ProgramSetBuilder<gen_LookAround>>> ClusterOnInput(this ProgramSetBuilder<gen_LookAround> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<object>, ProgramSetBuilder<gen_LookAround>>(Cluster.CastValue<object>(kvp.Key), ProgramSetBuilder<gen_LookAround>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096E9 RID: 38633 RVA: 0x00203B2D File Offset: 0x00201D2D
		public static IEnumerable<KeyValuePair<Optional<object>, ProgramSetBuilder<gen_LookAroundField>>> ClusterOnInput(this ProgramSetBuilder<gen_LookAroundField> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<object>, ProgramSetBuilder<gen_LookAroundField>>(Cluster.CastValue<object>(kvp.Key), ProgramSetBuilder<gen_LookAroundField>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096EA RID: 38634 RVA: 0x00203B5F File Offset: 0x00201D5F
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<delimiterStart>>> ClusterOnInput(this ProgramSetBuilder<delimiterStart> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<delimiterStart>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<delimiterStart>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096EB RID: 38635 RVA: 0x00203B91 File Offset: 0x00201D91
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<delimiterEnd>>> ClusterOnInput(this ProgramSetBuilder<delimiterEnd> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<delimiterEnd>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<delimiterEnd>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096EC RID: 38636 RVA: 0x00203BC3 File Offset: 0x00201DC3
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<includeDelimiters>>> ClusterOnInput(this ProgramSetBuilder<includeDelimiters> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<includeDelimiters>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<includeDelimiters>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096ED RID: 38637 RVA: 0x00203BF5 File Offset: 0x00201DF5
		public static IEnumerable<KeyValuePair<Optional<FillStrategy>, ProgramSetBuilder<fillStrategy>>> ClusterOnInput(this ProgramSetBuilder<fillStrategy> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<FillStrategy>, ProgramSetBuilder<fillStrategy>>(Cluster.CastValue<FillStrategy>(kvp.Key), ProgramSetBuilder<fillStrategy>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096EE RID: 38638 RVA: 0x00203C27 File Offset: 0x00201E27
		public static IEnumerable<KeyValuePair<Optional<int[]>, ProgramSetBuilder<ignoreIndexes>>> ClusterOnInput(this ProgramSetBuilder<ignoreIndexes> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int[]>, ProgramSetBuilder<ignoreIndexes>>(Cluster.CastValue<int[]>(kvp.Key), ProgramSetBuilder<ignoreIndexes>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096EF RID: 38639 RVA: 0x00203C59 File Offset: 0x00201E59
		public static IEnumerable<KeyValuePair<Optional<int[]>, ProgramSetBuilder<fieldStartPositions>>> ClusterOnInput(this ProgramSetBuilder<fieldStartPositions> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int[]>, ProgramSetBuilder<fieldStartPositions>>(Cluster.CastValue<int[]>(kvp.Key), ProgramSetBuilder<fieldStartPositions>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096F0 RID: 38640 RVA: 0x00203C8B File Offset: 0x00201E8B
		public static IEnumerable<KeyValuePair<Optional<Record<int, int>[]>, ProgramSetBuilder<delimiterPositions>>> ClusterOnInput(this ProgramSetBuilder<delimiterPositions> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Record<int, int>[]>, ProgramSetBuilder<delimiterPositions>>(Cluster.CastValue<Record<int, int>[]>(kvp.Key), ProgramSetBuilder<delimiterPositions>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096F1 RID: 38641 RVA: 0x00203CBD File Offset: 0x00201EBD
		public static IEnumerable<KeyValuePair<Optional<RegularExpression>, ProgramSetBuilder<fregex>>> ClusterOnInput(this ProgramSetBuilder<fregex> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<RegularExpression>, ProgramSetBuilder<fregex>>(Cluster.CastValue<RegularExpression>(kvp.Key), ProgramSetBuilder<fregex>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096F2 RID: 38642 RVA: 0x00203CEF File Offset: 0x00201EEF
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<s>>> ClusterOnInput(this ProgramSetBuilder<s> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<s>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<s>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096F3 RID: 38643 RVA: 0x00203D21 File Offset: 0x00201F21
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<a>>> ClusterOnInput(this ProgramSetBuilder<a> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<a>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<a>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096F4 RID: 38644 RVA: 0x00203D53 File Offset: 0x00201F53
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<numSplits>>> ClusterOnInput(this ProgramSetBuilder<numSplits> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<numSplits>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<numSplits>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096F5 RID: 38645 RVA: 0x00203D85 File Offset: 0x00201F85
		public static IEnumerable<KeyValuePair<Optional<RegularExpression>, ProgramSetBuilder<regex>>> ClusterOnInput(this ProgramSetBuilder<regex> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<RegularExpression>, ProgramSetBuilder<regex>>(Cluster.CastValue<RegularExpression>(kvp.Key), ProgramSetBuilder<regex>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096F6 RID: 38646 RVA: 0x00203DB7 File Offset: 0x00201FB7
		public static IEnumerable<KeyValuePair<Optional<object>, ProgramSetBuilder<obj>>> ClusterOnInput(this ProgramSetBuilder<obj> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<object>, ProgramSetBuilder<obj>>(Cluster.CastValue<object>(kvp.Key), ProgramSetBuilder<obj>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096F7 RID: 38647 RVA: 0x00203DE9 File Offset: 0x00201FE9
		public static IEnumerable<KeyValuePair<Optional<Record<RegularExpression, RegularExpression, RegularExpression>>, ProgramSetBuilder<delimiter>>> ClusterOnInput(this ProgramSetBuilder<delimiter> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Record<RegularExpression, RegularExpression, RegularExpression>>, ProgramSetBuilder<delimiter>>(Cluster.CastValue<Record<RegularExpression, RegularExpression, RegularExpression>>(kvp.Key), ProgramSetBuilder<delimiter>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096F8 RID: 38648 RVA: 0x00203E1B File Offset: 0x0020201B
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<output>>> ClusterOnInput(this ProgramSetBuilder<output> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<output>>(Cluster.CastValue<IEnumerable<StringRegion>>(kvp.Key), ProgramSetBuilder<output>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096F9 RID: 38649 RVA: 0x00203E4D File Offset: 0x0020204D
		public static IEnumerable<KeyValuePair<Optional<StringRegion>, ProgramSetBuilder<item1>>> ClusterOnInput(this ProgramSetBuilder<item1> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<StringRegion>, ProgramSetBuilder<item1>>(Cluster.CastValue<StringRegion>(kvp.Key), ProgramSetBuilder<item1>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096FA RID: 38650 RVA: 0x00203E7F File Offset: 0x0020207F
		public static IEnumerable<KeyValuePair<Optional<StringRegion>, ProgramSetBuilder<_LetB0>>> ClusterOnInput(this ProgramSetBuilder<_LetB0> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<StringRegion>, ProgramSetBuilder<_LetB0>>(Cluster.CastValue<StringRegion>(kvp.Key), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096FB RID: 38651 RVA: 0x00203EB1 File Offset: 0x002020B1
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<_LetB1>>> ClusterOnInput(this ProgramSetBuilder<_LetB1> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<_LetB1>>(Cluster.CastValue<IEnumerable<StringRegion>>(kvp.Key), ProgramSetBuilder<_LetB1>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096FC RID: 38652 RVA: 0x00203EE3 File Offset: 0x002020E3
		public static IEnumerable<KeyValuePair<Optional<Record<StringRegion, StringRegion>?>, ProgramSetBuilder<_LetB2>>> ClusterOnInput(this ProgramSetBuilder<_LetB2> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Record<StringRegion, StringRegion>?>, ProgramSetBuilder<_LetB2>>(Cluster.CastValue<Record<StringRegion, StringRegion>?>(kvp.Key), ProgramSetBuilder<_LetB2>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096FD RID: 38653 RVA: 0x00203F15 File Offset: 0x00202115
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<_LetB3>>> ClusterOnInput(this ProgramSetBuilder<_LetB3> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<_LetB3>>(Cluster.CastValue<IEnumerable<StringRegion>>(kvp.Key), ProgramSetBuilder<_LetB3>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x060096FE RID: 38654 RVA: 0x00203F47 File Offset: 0x00202147
		public static IEnumerable<KeyValuePair<Optional<SplitCell[]>[], ProgramSetBuilder<regionSplit>>> ClusterOnInputTuple(this ProgramSetBuilder<regionSplit> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SplitCell[]>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SplitCell[]>>(Cluster.CastValue<SplitCell[]>));
				}
				return new KeyValuePair<Optional<SplitCell[]>[], ProgramSetBuilder<regionSplit>>(key.Select(func).ToArray<Optional<SplitCell[]>>(), ProgramSetBuilder<regionSplit>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x060096FF RID: 38655 RVA: 0x00203F79 File Offset: 0x00202179
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<splitMatches>>> ClusterOnInputTuple(this ProgramSetBuilder<splitMatches> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<MatchRecord>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<MatchRecord>>(Cluster.CastValue<MatchRecord>));
				}
				return new KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<splitMatches>>(key.Select(func).ToArray<Optional<MatchRecord>>(), ProgramSetBuilder<splitMatches>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009700 RID: 38656 RVA: 0x00203FAB File Offset: 0x002021AB
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<multipleMatches>>> ClusterOnInputTuple(this ProgramSetBuilder<multipleMatches> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<MatchRecord>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<MatchRecord>>(Cluster.CastValue<MatchRecord>));
				}
				return new KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<multipleMatches>>(key.Select(func).ToArray<Optional<MatchRecord>>(), ProgramSetBuilder<multipleMatches>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009701 RID: 38657 RVA: 0x00203FDD File Offset: 0x002021DD
		public static IEnumerable<KeyValuePair<Optional<List<MatchRecord>>[], ProgramSetBuilder<delimiterList>>> ClusterOnInputTuple(this ProgramSetBuilder<delimiterList> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<List<MatchRecord>>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<List<MatchRecord>>>(Cluster.CastValue<List<MatchRecord>>));
				}
				return new KeyValuePair<Optional<List<MatchRecord>>[], ProgramSetBuilder<delimiterList>>(key.Select(func).ToArray<Optional<List<MatchRecord>>>(), ProgramSetBuilder<delimiterList>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009702 RID: 38658 RVA: 0x0020400F File Offset: 0x0020220F
		public static IEnumerable<KeyValuePair<Optional<Record<int, int, int, int>[]>[], ProgramSetBuilder<extractionPoints>>> ClusterOnInputTuple(this ProgramSetBuilder<extractionPoints> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Record<int, int, int, int>[]>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<Record<int, int, int, int>[]>>(Cluster.CastValue<Record<int, int, int, int>[]>));
				}
				return new KeyValuePair<Optional<Record<int, int, int, int>[]>[], ProgramSetBuilder<extractionPoints>>(key.Select(func).ToArray<Optional<Record<int, int, int, int>[]>>(), ProgramSetBuilder<extractionPoints>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009703 RID: 38659 RVA: 0x00204041 File Offset: 0x00202241
		public static IEnumerable<KeyValuePair<Optional<Record<int, int, int, int>?>[], ProgramSetBuilder<cndExtPoint>>> ClusterOnInputTuple(this ProgramSetBuilder<cndExtPoint> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Record<int, int, int, int>?>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<Record<int, int, int, int>?>>(Cluster.CastValue<Record<int, int, int, int>?>));
				}
				return new KeyValuePair<Optional<Record<int, int, int, int>?>[], ProgramSetBuilder<cndExtPoint>>(key.Select(func).ToArray<Optional<Record<int, int, int, int>?>>(), ProgramSetBuilder<cndExtPoint>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009704 RID: 38660 RVA: 0x00204073 File Offset: 0x00202273
		public static IEnumerable<KeyValuePair<Optional<Record<int, int, int, int>?>[], ProgramSetBuilder<extPoint>>> ClusterOnInputTuple(this ProgramSetBuilder<extPoint> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Record<int, int, int, int>?>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<Record<int, int, int, int>?>>(Cluster.CastValue<Record<int, int, int, int>?>));
				}
				return new KeyValuePair<Optional<Record<int, int, int, int>?>[], ProgramSetBuilder<extPoint>>(key.Select(func).ToArray<Optional<Record<int, int, int, int>?>>(), ProgramSetBuilder<extPoint>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009705 RID: 38661 RVA: 0x002040A5 File Offset: 0x002022A5
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<pred>>> ClusterOnInputTuple(this ProgramSetBuilder<pred> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<pred>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<pred>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009706 RID: 38662 RVA: 0x002040D7 File Offset: 0x002022D7
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<pattern>>> ClusterOnInputTuple(this ProgramSetBuilder<pattern> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<pattern>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<pattern>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009707 RID: 38663 RVA: 0x00204109 File Offset: 0x00202309
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<d>>> ClusterOnInputTuple(this ProgramSetBuilder<d> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<MatchRecord>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<MatchRecord>>(Cluster.CastValue<MatchRecord>));
				}
				return new KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<d>>(key.Select(func).ToArray<Optional<MatchRecord>>(), ProgramSetBuilder<d>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009708 RID: 38664 RVA: 0x0020413B File Offset: 0x0020233B
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<c>>> ClusterOnInputTuple(this ProgramSetBuilder<c> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<MatchRecord>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<MatchRecord>>(Cluster.CastValue<MatchRecord>));
				}
				return new KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<c>>(key.Select(func).ToArray<Optional<MatchRecord>>(), ProgramSetBuilder<c>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009709 RID: 38665 RVA: 0x0020416D File Offset: 0x0020236D
		public static IEnumerable<KeyValuePair<Optional<QuotingConfiguration>[], ProgramSetBuilder<quotingConf>>> ClusterOnInputTuple(this ProgramSetBuilder<quotingConf> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<QuotingConfiguration>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<QuotingConfiguration>>(Cluster.CastValue<QuotingConfiguration>));
				}
				return new KeyValuePair<Optional<QuotingConfiguration>[], ProgramSetBuilder<quotingConf>>(key.Select(func).ToArray<Optional<QuotingConfiguration>>(), ProgramSetBuilder<quotingConf>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600970A RID: 38666 RVA: 0x0020419F File Offset: 0x0020239F
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<constantDelimiterMatches>>> ClusterOnInputTuple(this ProgramSetBuilder<constantDelimiterMatches> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<MatchRecord>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<MatchRecord>>(Cluster.CastValue<MatchRecord>));
				}
				return new KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<constantDelimiterMatches>>(key.Select(func).ToArray<Optional<MatchRecord>>(), ProgramSetBuilder<constantDelimiterMatches>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600970B RID: 38667 RVA: 0x002041D1 File Offset: 0x002023D1
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<r>>> ClusterOnInputTuple(this ProgramSetBuilder<r> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<MatchRecord>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<MatchRecord>>(Cluster.CastValue<MatchRecord>));
				}
				return new KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<r>>(key.Select(func).ToArray<Optional<MatchRecord>>(), ProgramSetBuilder<r>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600970C RID: 38668 RVA: 0x00204203 File Offset: 0x00202403
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<regexMatch>>> ClusterOnInputTuple(this ProgramSetBuilder<regexMatch> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<MatchRecord>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<MatchRecord>>(Cluster.CastValue<MatchRecord>));
				}
				return new KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<regexMatch>>(key.Select(func).ToArray<Optional<MatchRecord>>(), ProgramSetBuilder<regexMatch>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600970D RID: 38669 RVA: 0x00204235 File Offset: 0x00202435
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<fieldMatch>>> ClusterOnInputTuple(this ProgramSetBuilder<fieldMatch> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<MatchRecord>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<MatchRecord>>(Cluster.CastValue<MatchRecord>));
				}
				return new KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<fieldMatch>>(key.Select(func).ToArray<Optional<MatchRecord>>(), ProgramSetBuilder<fieldMatch>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600970E RID: 38670 RVA: 0x00204267 File Offset: 0x00202467
		public static IEnumerable<KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<fixedWidthMatches>>> ClusterOnInputTuple(this ProgramSetBuilder<fixedWidthMatches> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<MatchRecord>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<MatchRecord>>(Cluster.CastValue<MatchRecord>));
				}
				return new KeyValuePair<Optional<MatchRecord>[], ProgramSetBuilder<fixedWidthMatches>>(key.Select(func).ToArray<Optional<MatchRecord>>(), ProgramSetBuilder<fixedWidthMatches>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600970F RID: 38671 RVA: 0x00204299 File Offset: 0x00202499
		public static IEnumerable<KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_Concat>>> ClusterOnInputTuple(this ProgramSetBuilder<gen_Concat> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<object>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<object>>(Cluster.CastValue<object>));
				}
				return new KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_Concat>>(key.Select(func).ToArray<Optional<object>>(), ProgramSetBuilder<gen_Concat>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009710 RID: 38672 RVA: 0x002042CB File Offset: 0x002024CB
		public static IEnumerable<KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_LookAround>>> ClusterOnInputTuple(this ProgramSetBuilder<gen_LookAround> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<object>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<object>>(Cluster.CastValue<object>));
				}
				return new KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_LookAround>>(key.Select(func).ToArray<Optional<object>>(), ProgramSetBuilder<gen_LookAround>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009711 RID: 38673 RVA: 0x002042FD File Offset: 0x002024FD
		public static IEnumerable<KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_LookAroundField>>> ClusterOnInputTuple(this ProgramSetBuilder<gen_LookAroundField> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<object>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<object>>(Cluster.CastValue<object>));
				}
				return new KeyValuePair<Optional<object>[], ProgramSetBuilder<gen_LookAroundField>>(key.Select(func).ToArray<Optional<object>>(), ProgramSetBuilder<gen_LookAroundField>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009712 RID: 38674 RVA: 0x0020432F File Offset: 0x0020252F
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<delimiterStart>>> ClusterOnInputTuple(this ProgramSetBuilder<delimiterStart> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<delimiterStart>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<delimiterStart>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009713 RID: 38675 RVA: 0x00204361 File Offset: 0x00202561
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<delimiterEnd>>> ClusterOnInputTuple(this ProgramSetBuilder<delimiterEnd> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<delimiterEnd>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<delimiterEnd>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009714 RID: 38676 RVA: 0x00204393 File Offset: 0x00202593
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<includeDelimiters>>> ClusterOnInputTuple(this ProgramSetBuilder<includeDelimiters> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<includeDelimiters>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<includeDelimiters>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009715 RID: 38677 RVA: 0x002043C5 File Offset: 0x002025C5
		public static IEnumerable<KeyValuePair<Optional<FillStrategy>[], ProgramSetBuilder<fillStrategy>>> ClusterOnInputTuple(this ProgramSetBuilder<fillStrategy> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<FillStrategy>> func;
				if ((func = Cluster.<>O.<9>__CastValue) == null)
				{
					func = (Cluster.<>O.<9>__CastValue = new Func<object, Optional<FillStrategy>>(Cluster.CastValue<FillStrategy>));
				}
				return new KeyValuePair<Optional<FillStrategy>[], ProgramSetBuilder<fillStrategy>>(key.Select(func).ToArray<Optional<FillStrategy>>(), ProgramSetBuilder<fillStrategy>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009716 RID: 38678 RVA: 0x002043F7 File Offset: 0x002025F7
		public static IEnumerable<KeyValuePair<Optional<int[]>[], ProgramSetBuilder<ignoreIndexes>>> ClusterOnInputTuple(this ProgramSetBuilder<ignoreIndexes> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int[]>> func;
				if ((func = Cluster.<>O.<10>__CastValue) == null)
				{
					func = (Cluster.<>O.<10>__CastValue = new Func<object, Optional<int[]>>(Cluster.CastValue<int[]>));
				}
				return new KeyValuePair<Optional<int[]>[], ProgramSetBuilder<ignoreIndexes>>(key.Select(func).ToArray<Optional<int[]>>(), ProgramSetBuilder<ignoreIndexes>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009717 RID: 38679 RVA: 0x00204429 File Offset: 0x00202629
		public static IEnumerable<KeyValuePair<Optional<int[]>[], ProgramSetBuilder<fieldStartPositions>>> ClusterOnInputTuple(this ProgramSetBuilder<fieldStartPositions> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int[]>> func;
				if ((func = Cluster.<>O.<10>__CastValue) == null)
				{
					func = (Cluster.<>O.<10>__CastValue = new Func<object, Optional<int[]>>(Cluster.CastValue<int[]>));
				}
				return new KeyValuePair<Optional<int[]>[], ProgramSetBuilder<fieldStartPositions>>(key.Select(func).ToArray<Optional<int[]>>(), ProgramSetBuilder<fieldStartPositions>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009718 RID: 38680 RVA: 0x0020445B File Offset: 0x0020265B
		public static IEnumerable<KeyValuePair<Optional<Record<int, int>[]>[], ProgramSetBuilder<delimiterPositions>>> ClusterOnInputTuple(this ProgramSetBuilder<delimiterPositions> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Record<int, int>[]>> func;
				if ((func = Cluster.<>O.<11>__CastValue) == null)
				{
					func = (Cluster.<>O.<11>__CastValue = new Func<object, Optional<Record<int, int>[]>>(Cluster.CastValue<Record<int, int>[]>));
				}
				return new KeyValuePair<Optional<Record<int, int>[]>[], ProgramSetBuilder<delimiterPositions>>(key.Select(func).ToArray<Optional<Record<int, int>[]>>(), ProgramSetBuilder<delimiterPositions>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009719 RID: 38681 RVA: 0x0020448D File Offset: 0x0020268D
		public static IEnumerable<KeyValuePair<Optional<RegularExpression>[], ProgramSetBuilder<fregex>>> ClusterOnInputTuple(this ProgramSetBuilder<fregex> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<RegularExpression>> func;
				if ((func = Cluster.<>O.<12>__CastValue) == null)
				{
					func = (Cluster.<>O.<12>__CastValue = new Func<object, Optional<RegularExpression>>(Cluster.CastValue<RegularExpression>));
				}
				return new KeyValuePair<Optional<RegularExpression>[], ProgramSetBuilder<fregex>>(key.Select(func).ToArray<Optional<RegularExpression>>(), ProgramSetBuilder<fregex>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600971A RID: 38682 RVA: 0x002044BF File Offset: 0x002026BF
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<s>>> ClusterOnInputTuple(this ProgramSetBuilder<s> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<s>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<s>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600971B RID: 38683 RVA: 0x002044F1 File Offset: 0x002026F1
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<a>>> ClusterOnInputTuple(this ProgramSetBuilder<a> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<a>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<a>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600971C RID: 38684 RVA: 0x00204523 File Offset: 0x00202723
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<numSplits>>> ClusterOnInputTuple(this ProgramSetBuilder<numSplits> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<13>__CastValue) == null)
				{
					func = (Cluster.<>O.<13>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<numSplits>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<numSplits>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600971D RID: 38685 RVA: 0x00204555 File Offset: 0x00202755
		public static IEnumerable<KeyValuePair<Optional<RegularExpression>[], ProgramSetBuilder<regex>>> ClusterOnInputTuple(this ProgramSetBuilder<regex> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<RegularExpression>> func;
				if ((func = Cluster.<>O.<12>__CastValue) == null)
				{
					func = (Cluster.<>O.<12>__CastValue = new Func<object, Optional<RegularExpression>>(Cluster.CastValue<RegularExpression>));
				}
				return new KeyValuePair<Optional<RegularExpression>[], ProgramSetBuilder<regex>>(key.Select(func).ToArray<Optional<RegularExpression>>(), ProgramSetBuilder<regex>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600971E RID: 38686 RVA: 0x00204587 File Offset: 0x00202787
		public static IEnumerable<KeyValuePair<Optional<object>[], ProgramSetBuilder<obj>>> ClusterOnInputTuple(this ProgramSetBuilder<obj> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<object>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<object>>(Cluster.CastValue<object>));
				}
				return new KeyValuePair<Optional<object>[], ProgramSetBuilder<obj>>(key.Select(func).ToArray<Optional<object>>(), ProgramSetBuilder<obj>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600971F RID: 38687 RVA: 0x002045B9 File Offset: 0x002027B9
		public static IEnumerable<KeyValuePair<Optional<Record<RegularExpression, RegularExpression, RegularExpression>>[], ProgramSetBuilder<delimiter>>> ClusterOnInputTuple(this ProgramSetBuilder<delimiter> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Record<RegularExpression, RegularExpression, RegularExpression>>> func;
				if ((func = Cluster.<>O.<14>__CastValue) == null)
				{
					func = (Cluster.<>O.<14>__CastValue = new Func<object, Optional<Record<RegularExpression, RegularExpression, RegularExpression>>>(Cluster.CastValue<Record<RegularExpression, RegularExpression, RegularExpression>>));
				}
				return new KeyValuePair<Optional<Record<RegularExpression, RegularExpression, RegularExpression>>[], ProgramSetBuilder<delimiter>>(key.Select(func).ToArray<Optional<Record<RegularExpression, RegularExpression, RegularExpression>>>(), ProgramSetBuilder<delimiter>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009720 RID: 38688 RVA: 0x002045EB File Offset: 0x002027EB
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<output>>> ClusterOnInputTuple(this ProgramSetBuilder<output> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<StringRegion>>> func;
				if ((func = Cluster.<>O.<15>__CastValue) == null)
				{
					func = (Cluster.<>O.<15>__CastValue = new Func<object, Optional<IEnumerable<StringRegion>>>(Cluster.CastValue<IEnumerable<StringRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<output>>(key.Select(func).ToArray<Optional<IEnumerable<StringRegion>>>(), ProgramSetBuilder<output>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009721 RID: 38689 RVA: 0x0020461D File Offset: 0x0020281D
		public static IEnumerable<KeyValuePair<Optional<StringRegion>[], ProgramSetBuilder<item1>>> ClusterOnInputTuple(this ProgramSetBuilder<item1> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<StringRegion>> func;
				if ((func = Cluster.<>O.<16>__CastValue) == null)
				{
					func = (Cluster.<>O.<16>__CastValue = new Func<object, Optional<StringRegion>>(Cluster.CastValue<StringRegion>));
				}
				return new KeyValuePair<Optional<StringRegion>[], ProgramSetBuilder<item1>>(key.Select(func).ToArray<Optional<StringRegion>>(), ProgramSetBuilder<item1>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009722 RID: 38690 RVA: 0x0020464F File Offset: 0x0020284F
		public static IEnumerable<KeyValuePair<Optional<StringRegion>[], ProgramSetBuilder<_LetB0>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB0> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<StringRegion>> func;
				if ((func = Cluster.<>O.<16>__CastValue) == null)
				{
					func = (Cluster.<>O.<16>__CastValue = new Func<object, Optional<StringRegion>>(Cluster.CastValue<StringRegion>));
				}
				return new KeyValuePair<Optional<StringRegion>[], ProgramSetBuilder<_LetB0>>(key.Select(func).ToArray<Optional<StringRegion>>(), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009723 RID: 38691 RVA: 0x00204681 File Offset: 0x00202881
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<_LetB1>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB1> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<StringRegion>>> func;
				if ((func = Cluster.<>O.<15>__CastValue) == null)
				{
					func = (Cluster.<>O.<15>__CastValue = new Func<object, Optional<IEnumerable<StringRegion>>>(Cluster.CastValue<IEnumerable<StringRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<_LetB1>>(key.Select(func).ToArray<Optional<IEnumerable<StringRegion>>>(), ProgramSetBuilder<_LetB1>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009724 RID: 38692 RVA: 0x002046B3 File Offset: 0x002028B3
		public static IEnumerable<KeyValuePair<Optional<Record<StringRegion, StringRegion>?>[], ProgramSetBuilder<_LetB2>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB2> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Record<StringRegion, StringRegion>?>> func;
				if ((func = Cluster.<>O.<17>__CastValue) == null)
				{
					func = (Cluster.<>O.<17>__CastValue = new Func<object, Optional<Record<StringRegion, StringRegion>?>>(Cluster.CastValue<Record<StringRegion, StringRegion>?>));
				}
				return new KeyValuePair<Optional<Record<StringRegion, StringRegion>?>[], ProgramSetBuilder<_LetB2>>(key.Select(func).ToArray<Optional<Record<StringRegion, StringRegion>?>>(), ProgramSetBuilder<_LetB2>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06009725 RID: 38693 RVA: 0x002046E5 File Offset: 0x002028E5
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<_LetB3>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB3> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<StringRegion>>> func;
				if ((func = Cluster.<>O.<15>__CastValue) == null)
				{
					func = (Cluster.<>O.<15>__CastValue = new Func<object, Optional<IEnumerable<StringRegion>>>(Cluster.CastValue<IEnumerable<StringRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<_LetB3>>(key.Select(func).ToArray<Optional<IEnumerable<StringRegion>>>(), ProgramSetBuilder<_LetB3>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x02001336 RID: 4918
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04003D4C RID: 15692
			public static Func<object, Optional<SplitCell[]>> <0>__CastValue;

			// Token: 0x04003D4D RID: 15693
			public static Func<object, Optional<MatchRecord>> <1>__CastValue;

			// Token: 0x04003D4E RID: 15694
			public static Func<object, Optional<List<MatchRecord>>> <2>__CastValue;

			// Token: 0x04003D4F RID: 15695
			public static Func<object, Optional<Record<int, int, int, int>[]>> <3>__CastValue;

			// Token: 0x04003D50 RID: 15696
			public static Func<object, Optional<Record<int, int, int, int>?>> <4>__CastValue;

			// Token: 0x04003D51 RID: 15697
			public static Func<object, Optional<bool>> <5>__CastValue;

			// Token: 0x04003D52 RID: 15698
			public static Func<object, Optional<string>> <6>__CastValue;

			// Token: 0x04003D53 RID: 15699
			public static Func<object, Optional<QuotingConfiguration>> <7>__CastValue;

			// Token: 0x04003D54 RID: 15700
			public static Func<object, Optional<object>> <8>__CastValue;

			// Token: 0x04003D55 RID: 15701
			public static Func<object, Optional<FillStrategy>> <9>__CastValue;

			// Token: 0x04003D56 RID: 15702
			public static Func<object, Optional<int[]>> <10>__CastValue;

			// Token: 0x04003D57 RID: 15703
			public static Func<object, Optional<Record<int, int>[]>> <11>__CastValue;

			// Token: 0x04003D58 RID: 15704
			public static Func<object, Optional<RegularExpression>> <12>__CastValue;

			// Token: 0x04003D59 RID: 15705
			public static Func<object, Optional<int>> <13>__CastValue;

			// Token: 0x04003D5A RID: 15706
			public static Func<object, Optional<Record<RegularExpression, RegularExpression, RegularExpression>>> <14>__CastValue;

			// Token: 0x04003D5B RID: 15707
			public static Func<object, Optional<IEnumerable<StringRegion>>> <15>__CastValue;

			// Token: 0x04003D5C RID: 15708
			public static Func<object, Optional<StringRegion>> <16>__CastValue;

			// Token: 0x04003D5D RID: 15709
			public static Func<object, Optional<Record<StringRegion, StringRegion>?>> <17>__CastValue;
		}
	}
}
