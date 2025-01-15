using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Compound.Split.Semantics;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build
{
	// Token: 0x02000934 RID: 2356
	public static class Cluster
	{
		// Token: 0x06003614 RID: 13844 RVA: 0x000AB0AF File Offset: 0x000A92AF
		private static Optional<T> CastValue<T>(object obj)
		{
			if (!(obj is Bottom))
			{
				return ((T)((object)obj)).Some<T>();
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x000AB0CA File Offset: 0x000A92CA
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<hasHeader>>> ClusterOnInput(this ProgramSetBuilder<hasHeader> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<hasHeader>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<hasHeader>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x000AB0FC File Offset: 0x000A92FC
		public static IEnumerable<KeyValuePair<Optional<int[]>, ProgramSetBuilder<columnList>>> ClusterOnInput(this ProgramSetBuilder<columnList> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int[]>, ProgramSetBuilder<columnList>>(Cluster.CastValue<int[]>(kvp.Key), ProgramSetBuilder<columnList>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x000AB12E File Offset: 0x000A932E
		public static IEnumerable<KeyValuePair<Optional<ITable<StringRegion>>, ProgramSetBuilder<topSplit>>> ClusterOnInput(this ProgramSetBuilder<topSplit> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ITable<StringRegion>>, ProgramSetBuilder<topSplit>>(Cluster.CastValue<ITable<StringRegion>>(kvp.Key), ProgramSetBuilder<topSplit>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x000AB160 File Offset: 0x000A9360
		public static IEnumerable<KeyValuePair<Optional<ITable<StringRegion>>, ProgramSetBuilder<splitRecordsSelect>>> ClusterOnInput(this ProgramSetBuilder<splitRecordsSelect> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ITable<StringRegion>>, ProgramSetBuilder<splitRecordsSelect>>(Cluster.CastValue<ITable<StringRegion>>(kvp.Key), ProgramSetBuilder<splitRecordsSelect>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x000AB192 File Offset: 0x000A9392
		public static IEnumerable<KeyValuePair<Optional<ITable<StringRegion>>, ProgramSetBuilder<splitRecords>>> ClusterOnInput(this ProgramSetBuilder<splitRecords> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ITable<StringRegion>>, ProgramSetBuilder<splitRecords>>(Cluster.CastValue<ITable<StringRegion>>(kvp.Key), ProgramSetBuilder<splitRecords>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x000AB1C4 File Offset: 0x000A93C4
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<key>>> ClusterOnInput(this ProgramSetBuilder<key> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<key>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<key>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000AB1F6 File Offset: 0x000A93F6
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<sep>>> ClusterOnInput(this ProgramSetBuilder<sep> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<sep>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<sep>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x000AB228 File Offset: 0x000A9428
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<newLineSep>>> ClusterOnInput(this ProgramSetBuilder<newLineSep> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<newLineSep>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<newLineSep>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x000AB25A File Offset: 0x000A945A
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<fwPos>>> ClusterOnInput(this ProgramSetBuilder<fwPos> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<fwPos>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<fwPos>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x000AB28C File Offset: 0x000A948C
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<List<MultiRecordMatch?>>>, ProgramSetBuilder<multiRecordSplit>>> ClusterOnInput(this ProgramSetBuilder<multiRecordSplit> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<List<MultiRecordMatch?>>>, ProgramSetBuilder<multiRecordSplit>>(Cluster.CastValue<IEnumerable<List<MultiRecordMatch?>>>(kvp.Key), ProgramSetBuilder<multiRecordSplit>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x000AB2BE File Offset: 0x000A94BE
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<List<MultiRecordMatch?>>>, ProgramSetBuilder<mapColumnSelectors>>> ClusterOnInput(this ProgramSetBuilder<mapColumnSelectors> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<List<MultiRecordMatch?>>>, ProgramSetBuilder<mapColumnSelectors>>(Cluster.CastValue<IEnumerable<List<MultiRecordMatch?>>>(kvp.Key), ProgramSetBuilder<mapColumnSelectors>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x000AB2F0 File Offset: 0x000A94F0
		public static IEnumerable<KeyValuePair<Optional<List<MultiRecordMatch?>>, ProgramSetBuilder<columnSelectorList>>> ClusterOnInput(this ProgramSetBuilder<columnSelectorList> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<List<MultiRecordMatch?>>, ProgramSetBuilder<columnSelectorList>>(Cluster.CastValue<List<MultiRecordMatch?>>(kvp.Key), ProgramSetBuilder<columnSelectorList>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x000AB322 File Offset: 0x000A9522
		public static IEnumerable<KeyValuePair<Optional<Optional<MultiRecordMatch>>, ProgramSetBuilder<columnSelector>>> ClusterOnInput(this ProgramSetBuilder<columnSelector> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Optional<MultiRecordMatch>>, ProgramSetBuilder<columnSelector>>(Cluster.CastValue<Optional<MultiRecordMatch>>(kvp.Key), ProgramSetBuilder<columnSelector>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x000AB354 File Offset: 0x000A9554
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion[]>>, ProgramSetBuilder<primarySelector>>> ClusterOnInput(this ProgramSetBuilder<primarySelector> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<StringRegion[]>>, ProgramSetBuilder<primarySelector>>(Cluster.CastValue<IEnumerable<StringRegion[]>>(kvp.Key), ProgramSetBuilder<primarySelector>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x000AB386 File Offset: 0x000A9586
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<SplitCell[]>>, ProgramSetBuilder<delimiterSplit>>> ClusterOnInput(this ProgramSetBuilder<delimiterSplit> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<SplitCell[]>>, ProgramSetBuilder<delimiterSplit>>(Cluster.CastValue<IEnumerable<SplitCell[]>>(kvp.Key), ProgramSetBuilder<delimiterSplit>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x000AB3B8 File Offset: 0x000A95B8
		public static IEnumerable<KeyValuePair<Optional<SplitCell[]>, ProgramSetBuilder<splitTextProg>>> ClusterOnInput(this ProgramSetBuilder<splitTextProg> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SplitCell[]>, ProgramSetBuilder<splitTextProg>>(Cluster.CastValue<SplitCell[]>(kvp.Key), ProgramSetBuilder<splitTextProg>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x000AB3EA File Offset: 0x000A95EA
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<splitFile>>> ClusterOnInput(this ProgramSetBuilder<splitFile> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<splitFile>>(Cluster.CastValue<IEnumerable<StringRegion>>(kvp.Key), ProgramSetBuilder<splitFile>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x000AB41C File Offset: 0x000A961C
		public static IEnumerable<KeyValuePair<Optional<RegularExpression>, ProgramSetBuilder<r>>> ClusterOnInput(this ProgramSetBuilder<r> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<RegularExpression>, ProgramSetBuilder<r>>(Cluster.CastValue<RegularExpression>(kvp.Key), ProgramSetBuilder<r>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x000AB44E File Offset: 0x000A964E
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<k>>> ClusterOnInput(this ProgramSetBuilder<k> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<k>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<k>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x000AB480 File Offset: 0x000A9680
		public static IEnumerable<KeyValuePair<Optional<QuotingConfiguration>, ProgramSetBuilder<quotingConfig>>> ClusterOnInput(this ProgramSetBuilder<quotingConfig> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<QuotingConfiguration>, ProgramSetBuilder<quotingConfig>>(Cluster.CastValue<QuotingConfiguration>(kvp.Key), ProgramSetBuilder<quotingConfig>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000AB4B2 File Offset: 0x000A96B2
		public static IEnumerable<KeyValuePair<Optional<Optional<string>>, ProgramSetBuilder<delimiter>>> ClusterOnInput(this ProgramSetBuilder<delimiter> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Optional<string>>, ProgramSetBuilder<delimiter>>(Cluster.CastValue<Optional<string>>(kvp.Key), ProgramSetBuilder<delimiter>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x000AB4E4 File Offset: 0x000A96E4
		public static IEnumerable<KeyValuePair<Optional<Optional<int>>, ProgramSetBuilder<headerIndex>>> ClusterOnInput(this ProgramSetBuilder<headerIndex> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Optional<int>>, ProgramSetBuilder<headerIndex>>(Cluster.CastValue<Optional<int>>(kvp.Key), ProgramSetBuilder<headerIndex>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000AB516 File Offset: 0x000A9716
		public static IEnumerable<KeyValuePair<Optional<Optional<string>>, ProgramSetBuilder<commentStr>>> ClusterOnInput(this ProgramSetBuilder<commentStr> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Optional<string>>, ProgramSetBuilder<commentStr>>(Cluster.CastValue<Optional<string>>(kvp.Key), ProgramSetBuilder<commentStr>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000AB548 File Offset: 0x000A9748
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<skipEmpty>>> ClusterOnInput(this ProgramSetBuilder<skipEmpty> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<skipEmpty>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<skipEmpty>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000AB57A File Offset: 0x000A977A
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<hasCommentHeader>>> ClusterOnInput(this ProgramSetBuilder<hasCommentHeader> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<hasCommentHeader>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<hasCommentHeader>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000AB5AC File Offset: 0x000A97AC
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IEnumerable<StringRegion>>>, ProgramSetBuilder<splitLines>>> ClusterOnInput(this ProgramSetBuilder<splitLines> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IEnumerable<StringRegion>>>, ProgramSetBuilder<splitLines>>(Cluster.CastValue<IEnumerable<IEnumerable<StringRegion>>>(kvp.Key), ProgramSetBuilder<splitLines>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000AB5DE File Offset: 0x000A97DE
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<dataLines>>> ClusterOnInput(this ProgramSetBuilder<dataLines> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<dataLines>>(Cluster.CastValue<IEnumerable<StringRegion>>(kvp.Key), ProgramSetBuilder<dataLines>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x000AB610 File Offset: 0x000A9810
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<skippedRecords>>> ClusterOnInput(this ProgramSetBuilder<skippedRecords> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<skippedRecords>>(Cluster.CastValue<IEnumerable<StringRegion>>(kvp.Key), ProgramSetBuilder<skippedRecords>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x000AB642 File Offset: 0x000A9842
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<skippedFooter>>> ClusterOnInput(this ProgramSetBuilder<skippedFooter> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<skippedFooter>>(Cluster.CastValue<IEnumerable<StringRegion>>(kvp.Key), ProgramSetBuilder<skippedFooter>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x000AB674 File Offset: 0x000A9874
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<allRecords>>> ClusterOnInput(this ProgramSetBuilder<allRecords> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<allRecords>>(Cluster.CastValue<IEnumerable<StringRegion>>(kvp.Key), ProgramSetBuilder<allRecords>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000AB6A6 File Offset: 0x000A98A6
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<basicLinePredicate>>> ClusterOnInput(this ProgramSetBuilder<basicLinePredicate> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<basicLinePredicate>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<basicLinePredicate>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x000AB6D8 File Offset: 0x000A98D8
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IEnumerable<StringRegion>>>, ProgramSetBuilder<splitSequence>>> ClusterOnInput(this ProgramSetBuilder<splitSequence> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IEnumerable<StringRegion>>>, ProgramSetBuilder<splitSequence>>(Cluster.CastValue<IEnumerable<IEnumerable<StringRegion>>>(kvp.Key), ProgramSetBuilder<splitSequence>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000AB70A File Offset: 0x000A990A
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<_LetB0>>> ClusterOnInput(this ProgramSetBuilder<_LetB0> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<_LetB0>>(Cluster.CastValue<IEnumerable<StringRegion>>(kvp.Key), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x000AB73C File Offset: 0x000A993C
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<_LetB1>>> ClusterOnInput(this ProgramSetBuilder<_LetB1> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<_LetB1>>(Cluster.CastValue<IEnumerable<StringRegion>>(kvp.Key), ProgramSetBuilder<_LetB1>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x000AB76E File Offset: 0x000A996E
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<hasHeader>>> ClusterOnInputTuple(this ProgramSetBuilder<hasHeader> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<hasHeader>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<hasHeader>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x000AB7A0 File Offset: 0x000A99A0
		public static IEnumerable<KeyValuePair<Optional<int[]>[], ProgramSetBuilder<columnList>>> ClusterOnInputTuple(this ProgramSetBuilder<columnList> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int[]>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<int[]>>(Cluster.CastValue<int[]>));
				}
				return new KeyValuePair<Optional<int[]>[], ProgramSetBuilder<columnList>>(key.Select(func).ToArray<Optional<int[]>>(), ProgramSetBuilder<columnList>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x000AB7D2 File Offset: 0x000A99D2
		public static IEnumerable<KeyValuePair<Optional<ITable<StringRegion>>[], ProgramSetBuilder<topSplit>>> ClusterOnInputTuple(this ProgramSetBuilder<topSplit> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ITable<StringRegion>>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<ITable<StringRegion>>>(Cluster.CastValue<ITable<StringRegion>>));
				}
				return new KeyValuePair<Optional<ITable<StringRegion>>[], ProgramSetBuilder<topSplit>>(key.Select(func).ToArray<Optional<ITable<StringRegion>>>(), ProgramSetBuilder<topSplit>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x000AB804 File Offset: 0x000A9A04
		public static IEnumerable<KeyValuePair<Optional<ITable<StringRegion>>[], ProgramSetBuilder<splitRecordsSelect>>> ClusterOnInputTuple(this ProgramSetBuilder<splitRecordsSelect> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ITable<StringRegion>>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<ITable<StringRegion>>>(Cluster.CastValue<ITable<StringRegion>>));
				}
				return new KeyValuePair<Optional<ITable<StringRegion>>[], ProgramSetBuilder<splitRecordsSelect>>(key.Select(func).ToArray<Optional<ITable<StringRegion>>>(), ProgramSetBuilder<splitRecordsSelect>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x000AB836 File Offset: 0x000A9A36
		public static IEnumerable<KeyValuePair<Optional<ITable<StringRegion>>[], ProgramSetBuilder<splitRecords>>> ClusterOnInputTuple(this ProgramSetBuilder<splitRecords> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ITable<StringRegion>>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<ITable<StringRegion>>>(Cluster.CastValue<ITable<StringRegion>>));
				}
				return new KeyValuePair<Optional<ITable<StringRegion>>[], ProgramSetBuilder<splitRecords>>(key.Select(func).ToArray<Optional<ITable<StringRegion>>>(), ProgramSetBuilder<splitRecords>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x000AB868 File Offset: 0x000A9A68
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<key>>> ClusterOnInputTuple(this ProgramSetBuilder<key> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<key>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<key>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x000AB89A File Offset: 0x000A9A9A
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<sep>>> ClusterOnInputTuple(this ProgramSetBuilder<sep> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<sep>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<sep>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x000AB8CC File Offset: 0x000A9ACC
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<newLineSep>>> ClusterOnInputTuple(this ProgramSetBuilder<newLineSep> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<newLineSep>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<newLineSep>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x000AB8FE File Offset: 0x000A9AFE
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<fwPos>>> ClusterOnInputTuple(this ProgramSetBuilder<fwPos> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<fwPos>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<fwPos>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x000AB930 File Offset: 0x000A9B30
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<List<MultiRecordMatch?>>>[], ProgramSetBuilder<multiRecordSplit>>> ClusterOnInputTuple(this ProgramSetBuilder<multiRecordSplit> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<List<MultiRecordMatch?>>>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<IEnumerable<List<MultiRecordMatch?>>>>(Cluster.CastValue<IEnumerable<List<MultiRecordMatch?>>>));
				}
				return new KeyValuePair<Optional<IEnumerable<List<MultiRecordMatch?>>>[], ProgramSetBuilder<multiRecordSplit>>(key.Select(func).ToArray<Optional<IEnumerable<List<MultiRecordMatch?>>>>(), ProgramSetBuilder<multiRecordSplit>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x000AB962 File Offset: 0x000A9B62
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<List<MultiRecordMatch?>>>[], ProgramSetBuilder<mapColumnSelectors>>> ClusterOnInputTuple(this ProgramSetBuilder<mapColumnSelectors> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<List<MultiRecordMatch?>>>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<IEnumerable<List<MultiRecordMatch?>>>>(Cluster.CastValue<IEnumerable<List<MultiRecordMatch?>>>));
				}
				return new KeyValuePair<Optional<IEnumerable<List<MultiRecordMatch?>>>[], ProgramSetBuilder<mapColumnSelectors>>(key.Select(func).ToArray<Optional<IEnumerable<List<MultiRecordMatch?>>>>(), ProgramSetBuilder<mapColumnSelectors>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000AB994 File Offset: 0x000A9B94
		public static IEnumerable<KeyValuePair<Optional<List<MultiRecordMatch?>>[], ProgramSetBuilder<columnSelectorList>>> ClusterOnInputTuple(this ProgramSetBuilder<columnSelectorList> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<List<MultiRecordMatch?>>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<List<MultiRecordMatch?>>>(Cluster.CastValue<List<MultiRecordMatch?>>));
				}
				return new KeyValuePair<Optional<List<MultiRecordMatch?>>[], ProgramSetBuilder<columnSelectorList>>(key.Select(func).ToArray<Optional<List<MultiRecordMatch?>>>(), ProgramSetBuilder<columnSelectorList>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x000AB9C6 File Offset: 0x000A9BC6
		public static IEnumerable<KeyValuePair<Optional<Optional<MultiRecordMatch>>[], ProgramSetBuilder<columnSelector>>> ClusterOnInputTuple(this ProgramSetBuilder<columnSelector> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Optional<MultiRecordMatch>>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<Optional<MultiRecordMatch>>>(Cluster.CastValue<Optional<MultiRecordMatch>>));
				}
				return new KeyValuePair<Optional<Optional<MultiRecordMatch>>[], ProgramSetBuilder<columnSelector>>(key.Select(func).ToArray<Optional<Optional<MultiRecordMatch>>>(), ProgramSetBuilder<columnSelector>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000AB9F8 File Offset: 0x000A9BF8
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion[]>>[], ProgramSetBuilder<primarySelector>>> ClusterOnInputTuple(this ProgramSetBuilder<primarySelector> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<StringRegion[]>>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<IEnumerable<StringRegion[]>>>(Cluster.CastValue<IEnumerable<StringRegion[]>>));
				}
				return new KeyValuePair<Optional<IEnumerable<StringRegion[]>>[], ProgramSetBuilder<primarySelector>>(key.Select(func).ToArray<Optional<IEnumerable<StringRegion[]>>>(), ProgramSetBuilder<primarySelector>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x000ABA2A File Offset: 0x000A9C2A
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<SplitCell[]>>[], ProgramSetBuilder<delimiterSplit>>> ClusterOnInputTuple(this ProgramSetBuilder<delimiterSplit> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<SplitCell[]>>> func;
				if ((func = Cluster.<>O.<9>__CastValue) == null)
				{
					func = (Cluster.<>O.<9>__CastValue = new Func<object, Optional<IEnumerable<SplitCell[]>>>(Cluster.CastValue<IEnumerable<SplitCell[]>>));
				}
				return new KeyValuePair<Optional<IEnumerable<SplitCell[]>>[], ProgramSetBuilder<delimiterSplit>>(key.Select(func).ToArray<Optional<IEnumerable<SplitCell[]>>>(), ProgramSetBuilder<delimiterSplit>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x000ABA5C File Offset: 0x000A9C5C
		public static IEnumerable<KeyValuePair<Optional<SplitCell[]>[], ProgramSetBuilder<splitTextProg>>> ClusterOnInputTuple(this ProgramSetBuilder<splitTextProg> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SplitCell[]>> func;
				if ((func = Cluster.<>O.<10>__CastValue) == null)
				{
					func = (Cluster.<>O.<10>__CastValue = new Func<object, Optional<SplitCell[]>>(Cluster.CastValue<SplitCell[]>));
				}
				return new KeyValuePair<Optional<SplitCell[]>[], ProgramSetBuilder<splitTextProg>>(key.Select(func).ToArray<Optional<SplitCell[]>>(), ProgramSetBuilder<splitTextProg>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000ABA8E File Offset: 0x000A9C8E
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<splitFile>>> ClusterOnInputTuple(this ProgramSetBuilder<splitFile> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<StringRegion>>> func;
				if ((func = Cluster.<>O.<11>__CastValue) == null)
				{
					func = (Cluster.<>O.<11>__CastValue = new Func<object, Optional<IEnumerable<StringRegion>>>(Cluster.CastValue<IEnumerable<StringRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<splitFile>>(key.Select(func).ToArray<Optional<IEnumerable<StringRegion>>>(), ProgramSetBuilder<splitFile>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x000ABAC0 File Offset: 0x000A9CC0
		public static IEnumerable<KeyValuePair<Optional<RegularExpression>[], ProgramSetBuilder<r>>> ClusterOnInputTuple(this ProgramSetBuilder<r> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<RegularExpression>> func;
				if ((func = Cluster.<>O.<12>__CastValue) == null)
				{
					func = (Cluster.<>O.<12>__CastValue = new Func<object, Optional<RegularExpression>>(Cluster.CastValue<RegularExpression>));
				}
				return new KeyValuePair<Optional<RegularExpression>[], ProgramSetBuilder<r>>(key.Select(func).ToArray<Optional<RegularExpression>>(), ProgramSetBuilder<r>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x000ABAF2 File Offset: 0x000A9CF2
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<k>>> ClusterOnInputTuple(this ProgramSetBuilder<k> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<k>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<k>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x000ABB24 File Offset: 0x000A9D24
		public static IEnumerable<KeyValuePair<Optional<QuotingConfiguration>[], ProgramSetBuilder<quotingConfig>>> ClusterOnInputTuple(this ProgramSetBuilder<quotingConfig> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<QuotingConfiguration>> func;
				if ((func = Cluster.<>O.<13>__CastValue) == null)
				{
					func = (Cluster.<>O.<13>__CastValue = new Func<object, Optional<QuotingConfiguration>>(Cluster.CastValue<QuotingConfiguration>));
				}
				return new KeyValuePair<Optional<QuotingConfiguration>[], ProgramSetBuilder<quotingConfig>>(key.Select(func).ToArray<Optional<QuotingConfiguration>>(), ProgramSetBuilder<quotingConfig>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x000ABB56 File Offset: 0x000A9D56
		public static IEnumerable<KeyValuePair<Optional<Optional<string>>[], ProgramSetBuilder<delimiter>>> ClusterOnInputTuple(this ProgramSetBuilder<delimiter> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Optional<string>>> func;
				if ((func = Cluster.<>O.<14>__CastValue) == null)
				{
					func = (Cluster.<>O.<14>__CastValue = new Func<object, Optional<Optional<string>>>(Cluster.CastValue<Optional<string>>));
				}
				return new KeyValuePair<Optional<Optional<string>>[], ProgramSetBuilder<delimiter>>(key.Select(func).ToArray<Optional<Optional<string>>>(), ProgramSetBuilder<delimiter>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000ABB88 File Offset: 0x000A9D88
		public static IEnumerable<KeyValuePair<Optional<Optional<int>>[], ProgramSetBuilder<headerIndex>>> ClusterOnInputTuple(this ProgramSetBuilder<headerIndex> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Optional<int>>> func;
				if ((func = Cluster.<>O.<15>__CastValue) == null)
				{
					func = (Cluster.<>O.<15>__CastValue = new Func<object, Optional<Optional<int>>>(Cluster.CastValue<Optional<int>>));
				}
				return new KeyValuePair<Optional<Optional<int>>[], ProgramSetBuilder<headerIndex>>(key.Select(func).ToArray<Optional<Optional<int>>>(), ProgramSetBuilder<headerIndex>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x000ABBBA File Offset: 0x000A9DBA
		public static IEnumerable<KeyValuePair<Optional<Optional<string>>[], ProgramSetBuilder<commentStr>>> ClusterOnInputTuple(this ProgramSetBuilder<commentStr> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Optional<string>>> func;
				if ((func = Cluster.<>O.<14>__CastValue) == null)
				{
					func = (Cluster.<>O.<14>__CastValue = new Func<object, Optional<Optional<string>>>(Cluster.CastValue<Optional<string>>));
				}
				return new KeyValuePair<Optional<Optional<string>>[], ProgramSetBuilder<commentStr>>(key.Select(func).ToArray<Optional<Optional<string>>>(), ProgramSetBuilder<commentStr>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000ABBEC File Offset: 0x000A9DEC
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<skipEmpty>>> ClusterOnInputTuple(this ProgramSetBuilder<skipEmpty> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<skipEmpty>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<skipEmpty>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000ABC1E File Offset: 0x000A9E1E
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<hasCommentHeader>>> ClusterOnInputTuple(this ProgramSetBuilder<hasCommentHeader> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<hasCommentHeader>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<hasCommentHeader>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x000ABC50 File Offset: 0x000A9E50
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IEnumerable<StringRegion>>>[], ProgramSetBuilder<splitLines>>> ClusterOnInputTuple(this ProgramSetBuilder<splitLines> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IEnumerable<StringRegion>>>> func;
				if ((func = Cluster.<>O.<16>__CastValue) == null)
				{
					func = (Cluster.<>O.<16>__CastValue = new Func<object, Optional<IEnumerable<IEnumerable<StringRegion>>>>(Cluster.CastValue<IEnumerable<IEnumerable<StringRegion>>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IEnumerable<StringRegion>>>[], ProgramSetBuilder<splitLines>>(key.Select(func).ToArray<Optional<IEnumerable<IEnumerable<StringRegion>>>>(), ProgramSetBuilder<splitLines>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x000ABC82 File Offset: 0x000A9E82
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<dataLines>>> ClusterOnInputTuple(this ProgramSetBuilder<dataLines> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<StringRegion>>> func;
				if ((func = Cluster.<>O.<11>__CastValue) == null)
				{
					func = (Cluster.<>O.<11>__CastValue = new Func<object, Optional<IEnumerable<StringRegion>>>(Cluster.CastValue<IEnumerable<StringRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<dataLines>>(key.Select(func).ToArray<Optional<IEnumerable<StringRegion>>>(), ProgramSetBuilder<dataLines>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x000ABCB4 File Offset: 0x000A9EB4
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<skippedRecords>>> ClusterOnInputTuple(this ProgramSetBuilder<skippedRecords> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<StringRegion>>> func;
				if ((func = Cluster.<>O.<11>__CastValue) == null)
				{
					func = (Cluster.<>O.<11>__CastValue = new Func<object, Optional<IEnumerable<StringRegion>>>(Cluster.CastValue<IEnumerable<StringRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<skippedRecords>>(key.Select(func).ToArray<Optional<IEnumerable<StringRegion>>>(), ProgramSetBuilder<skippedRecords>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x000ABCE6 File Offset: 0x000A9EE6
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<skippedFooter>>> ClusterOnInputTuple(this ProgramSetBuilder<skippedFooter> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<StringRegion>>> func;
				if ((func = Cluster.<>O.<11>__CastValue) == null)
				{
					func = (Cluster.<>O.<11>__CastValue = new Func<object, Optional<IEnumerable<StringRegion>>>(Cluster.CastValue<IEnumerable<StringRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<skippedFooter>>(key.Select(func).ToArray<Optional<IEnumerable<StringRegion>>>(), ProgramSetBuilder<skippedFooter>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x000ABD18 File Offset: 0x000A9F18
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<allRecords>>> ClusterOnInputTuple(this ProgramSetBuilder<allRecords> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<StringRegion>>> func;
				if ((func = Cluster.<>O.<11>__CastValue) == null)
				{
					func = (Cluster.<>O.<11>__CastValue = new Func<object, Optional<IEnumerable<StringRegion>>>(Cluster.CastValue<IEnumerable<StringRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<allRecords>>(key.Select(func).ToArray<Optional<IEnumerable<StringRegion>>>(), ProgramSetBuilder<allRecords>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x000ABD4A File Offset: 0x000A9F4A
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<basicLinePredicate>>> ClusterOnInputTuple(this ProgramSetBuilder<basicLinePredicate> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<basicLinePredicate>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<basicLinePredicate>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x000ABD7C File Offset: 0x000A9F7C
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IEnumerable<StringRegion>>>[], ProgramSetBuilder<splitSequence>>> ClusterOnInputTuple(this ProgramSetBuilder<splitSequence> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IEnumerable<StringRegion>>>> func;
				if ((func = Cluster.<>O.<16>__CastValue) == null)
				{
					func = (Cluster.<>O.<16>__CastValue = new Func<object, Optional<IEnumerable<IEnumerable<StringRegion>>>>(Cluster.CastValue<IEnumerable<IEnumerable<StringRegion>>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IEnumerable<StringRegion>>>[], ProgramSetBuilder<splitSequence>>(key.Select(func).ToArray<Optional<IEnumerable<IEnumerable<StringRegion>>>>(), ProgramSetBuilder<splitSequence>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000ABDAE File Offset: 0x000A9FAE
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<_LetB0>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB0> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<StringRegion>>> func;
				if ((func = Cluster.<>O.<11>__CastValue) == null)
				{
					func = (Cluster.<>O.<11>__CastValue = new Func<object, Optional<IEnumerable<StringRegion>>>(Cluster.CastValue<IEnumerable<StringRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<_LetB0>>(key.Select(func).ToArray<Optional<IEnumerable<StringRegion>>>(), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x000ABDE0 File Offset: 0x000A9FE0
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<_LetB1>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB1> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<StringRegion>>> func;
				if ((func = Cluster.<>O.<11>__CastValue) == null)
				{
					func = (Cluster.<>O.<11>__CastValue = new Func<object, Optional<IEnumerable<StringRegion>>>(Cluster.CastValue<IEnumerable<StringRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<_LetB1>>(key.Select(func).ToArray<Optional<IEnumerable<StringRegion>>>(), ProgramSetBuilder<_LetB1>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x02000935 RID: 2357
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001A01 RID: 6657
			public static Func<object, Optional<bool>> <0>__CastValue;

			// Token: 0x04001A02 RID: 6658
			public static Func<object, Optional<int[]>> <1>__CastValue;

			// Token: 0x04001A03 RID: 6659
			public static Func<object, Optional<ITable<StringRegion>>> <2>__CastValue;

			// Token: 0x04001A04 RID: 6660
			public static Func<object, Optional<string>> <3>__CastValue;

			// Token: 0x04001A05 RID: 6661
			public static Func<object, Optional<int>> <4>__CastValue;

			// Token: 0x04001A06 RID: 6662
			public static Func<object, Optional<IEnumerable<List<MultiRecordMatch?>>>> <5>__CastValue;

			// Token: 0x04001A07 RID: 6663
			public static Func<object, Optional<List<MultiRecordMatch?>>> <6>__CastValue;

			// Token: 0x04001A08 RID: 6664
			public static Func<object, Optional<Optional<MultiRecordMatch>>> <7>__CastValue;

			// Token: 0x04001A09 RID: 6665
			public static Func<object, Optional<IEnumerable<StringRegion[]>>> <8>__CastValue;

			// Token: 0x04001A0A RID: 6666
			public static Func<object, Optional<IEnumerable<SplitCell[]>>> <9>__CastValue;

			// Token: 0x04001A0B RID: 6667
			public static Func<object, Optional<SplitCell[]>> <10>__CastValue;

			// Token: 0x04001A0C RID: 6668
			public static Func<object, Optional<IEnumerable<StringRegion>>> <11>__CastValue;

			// Token: 0x04001A0D RID: 6669
			public static Func<object, Optional<RegularExpression>> <12>__CastValue;

			// Token: 0x04001A0E RID: 6670
			public static Func<object, Optional<QuotingConfiguration>> <13>__CastValue;

			// Token: 0x04001A0F RID: 6671
			public static Func<object, Optional<Optional<string>>> <14>__CastValue;

			// Token: 0x04001A10 RID: 6672
			public static Func<object, Optional<Optional<int>>> <15>__CastValue;

			// Token: 0x04001A11 RID: 6673
			public static Func<object, Optional<IEnumerable<IEnumerable<StringRegion>>>> <16>__CastValue;
		}
	}
}
