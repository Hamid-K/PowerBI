using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build
{
	// Token: 0x02001E55 RID: 7765
	public static class Cluster
	{
		// Token: 0x06010552 RID: 66898 RVA: 0x000AB0AF File Offset: 0x000A92AF
		private static Optional<T> CastValue<T>(object obj)
		{
			if (!(obj is Bottom))
			{
				return ((T)((object)obj)).Some<T>();
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x06010553 RID: 66899 RVA: 0x00387737 File Offset: 0x00385937
		public static IEnumerable<KeyValuePair<Optional<Node>, ProgramSetBuilder<guardedRule>>> ClusterOnInput(this ProgramSetBuilder<guardedRule> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Node>, ProgramSetBuilder<guardedRule>>(Cluster.CastValue<Node>(kvp.Key), ProgramSetBuilder<guardedRule>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06010554 RID: 66900 RVA: 0x00387769 File Offset: 0x00385969
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<match>>> ClusterOnInput(this ProgramSetBuilder<match> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<match>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<match>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06010555 RID: 66901 RVA: 0x0038779B File Offset: 0x0038599B
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<pred>>> ClusterOnInput(this ProgramSetBuilder<pred> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<pred>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<pred>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06010556 RID: 66902 RVA: 0x003877CD File Offset: 0x003859CD
		public static IEnumerable<KeyValuePair<Optional<Node>, ProgramSetBuilder<newDsl>>> ClusterOnInput(this ProgramSetBuilder<newDsl> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Node>, ProgramSetBuilder<newDsl>>(Cluster.CastValue<Node>(kvp.Key), ProgramSetBuilder<newDsl>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06010557 RID: 66903 RVA: 0x003877FF File Offset: 0x003859FF
		public static IEnumerable<KeyValuePair<Optional<Node>, ProgramSetBuilder<construction>>> ClusterOnInput(this ProgramSetBuilder<construction> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Node>, ProgramSetBuilder<construction>>(Cluster.CastValue<Node>(kvp.Key), ProgramSetBuilder<construction>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06010558 RID: 66904 RVA: 0x00387831 File Offset: 0x00385A31
		public static IEnumerable<KeyValuePair<Optional<Node>, ProgramSetBuilder<select>>> ClusterOnInput(this ProgramSetBuilder<select> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Node>, ProgramSetBuilder<@select>>(Cluster.CastValue<Node>(kvp.Key), ProgramSetBuilder<@select>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06010559 RID: 66905 RVA: 0x00387863 File Offset: 0x00385A63
		public static IEnumerable<KeyValuePair<Optional<Node[]>, ProgramSetBuilder<tmpFilter>>> ClusterOnInput(this ProgramSetBuilder<tmpFilter> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Node[]>, ProgramSetBuilder<tmpFilter>>(Cluster.CastValue<Node[]>(kvp.Key), ProgramSetBuilder<tmpFilter>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0601055A RID: 66906 RVA: 0x00387895 File Offset: 0x00385A95
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<Node>>, ProgramSetBuilder<sequenceChildren>>> ClusterOnInput(this ProgramSetBuilder<sequenceChildren> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<Node>>, ProgramSetBuilder<sequenceChildren>>(Cluster.CastValue<IEnumerable<Node>>(kvp.Key), ProgramSetBuilder<sequenceChildren>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0601055B RID: 66907 RVA: 0x003878C7 File Offset: 0x00385AC7
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<Node>>, ProgramSetBuilder<convertSequence>>> ClusterOnInput(this ProgramSetBuilder<convertSequence> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<Node>>, ProgramSetBuilder<convertSequence>>(Cluster.CastValue<IEnumerable<Node>>(kvp.Key), ProgramSetBuilder<convertSequence>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0601055C RID: 66908 RVA: 0x003878F9 File Offset: 0x00385AF9
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<Node>>, ProgramSetBuilder<sequenceMap>>> ClusterOnInput(this ProgramSetBuilder<sequenceMap> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<Node>>, ProgramSetBuilder<sequenceMap>>(Cluster.CastValue<IEnumerable<Node>>(kvp.Key), ProgramSetBuilder<sequenceMap>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0601055D RID: 66909 RVA: 0x0038792B File Offset: 0x00385B2B
		public static IEnumerable<KeyValuePair<Optional<Node[]>, ProgramSetBuilder<parentChildren>>> ClusterOnInput(this ProgramSetBuilder<parentChildren> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Node[]>, ProgramSetBuilder<parentChildren>>(Cluster.CastValue<Node[]>(kvp.Key), ProgramSetBuilder<parentChildren>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0601055E RID: 66910 RVA: 0x0038795D File Offset: 0x00385B5D
		public static IEnumerable<KeyValuePair<Optional<Node[]>, ProgramSetBuilder<relChildList>>> ClusterOnInput(this ProgramSetBuilder<relChildList> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Node[]>, ProgramSetBuilder<relChildList>>(Cluster.CastValue<Node[]>(kvp.Key), ProgramSetBuilder<relChildList>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0601055F RID: 66911 RVA: 0x0038798F File Offset: 0x00385B8F
		public static IEnumerable<KeyValuePair<Optional<Node[]>, ProgramSetBuilder<singleRelChildList>>> ClusterOnInput(this ProgramSetBuilder<singleRelChildList> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Node[]>, ProgramSetBuilder<singleRelChildList>>(Cluster.CastValue<Node[]>(kvp.Key), ProgramSetBuilder<singleRelChildList>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06010560 RID: 66912 RVA: 0x003879C1 File Offset: 0x00385BC1
		public static IEnumerable<KeyValuePair<Optional<Node>, ProgramSetBuilder<relChild>>> ClusterOnInput(this ProgramSetBuilder<relChild> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Node>, ProgramSetBuilder<relChild>>(Cluster.CastValue<Node>(kvp.Key), ProgramSetBuilder<relChild>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06010561 RID: 66913 RVA: 0x003879F3 File Offset: 0x00385BF3
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<pos>>> ClusterOnInput(this ProgramSetBuilder<pos> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<pos>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<pos>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06010562 RID: 66914 RVA: 0x00387A25 File Offset: 0x00385C25
		public static IEnumerable<KeyValuePair<Optional<Node[]>, ProgramSetBuilder<children>>> ClusterOnInput(this ProgramSetBuilder<children> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Node[]>, ProgramSetBuilder<children>>(Cluster.CastValue<Node[]>(kvp.Key), ProgramSetBuilder<children>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06010563 RID: 66915 RVA: 0x00387A57 File Offset: 0x00385C57
		public static IEnumerable<KeyValuePair<Optional<Node[]>, ProgramSetBuilder<interval>>> ClusterOnInput(this ProgramSetBuilder<interval> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Node[]>, ProgramSetBuilder<interval>>(Cluster.CastValue<Node[]>(kvp.Key), ProgramSetBuilder<interval>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06010564 RID: 66916 RVA: 0x00387A89 File Offset: 0x00385C89
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<Node>>, ProgramSetBuilder<inorderAllNodes>>> ClusterOnInput(this ProgramSetBuilder<inorderAllNodes> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<Node>>, ProgramSetBuilder<inorderAllNodes>>(Cluster.CastValue<IEnumerable<Node>>(kvp.Key), ProgramSetBuilder<inorderAllNodes>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06010565 RID: 66917 RVA: 0x00387ABB File Offset: 0x00385CBB
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<label>>> ClusterOnInput(this ProgramSetBuilder<label> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<label>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<label>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06010566 RID: 66918 RVA: 0x00387AED File Offset: 0x00385CED
		public static IEnumerable<KeyValuePair<Optional<Dictionary<string, string>>, ProgramSetBuilder<attributes>>> ClusterOnInput(this ProgramSetBuilder<attributes> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Dictionary<string, string>>, ProgramSetBuilder<attributes>>(Cluster.CastValue<Dictionary<string, string>>(kvp.Key), ProgramSetBuilder<attributes>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06010567 RID: 66919 RVA: 0x00387B1F File Offset: 0x00385D1F
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<kind>>> ClusterOnInput(this ProgramSetBuilder<kind> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<kind>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<kind>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06010568 RID: 66920 RVA: 0x00387B51 File Offset: 0x00385D51
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<name>>> ClusterOnInput(this ProgramSetBuilder<name> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<name>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<name>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06010569 RID: 66921 RVA: 0x00387B83 File Offset: 0x00385D83
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<value>>> ClusterOnInput(this ProgramSetBuilder<value> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<value>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<value>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0601056A RID: 66922 RVA: 0x00387BB5 File Offset: 0x00385DB5
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<k>>> ClusterOnInput(this ProgramSetBuilder<k> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<k>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<k>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0601056B RID: 66923 RVA: 0x00387BE7 File Offset: 0x00385DE7
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<p>>> ClusterOnInput(this ProgramSetBuilder<p> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<p>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<p>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0601056C RID: 66924 RVA: 0x00387C19 File Offset: 0x00385E19
		public static IEnumerable<KeyValuePair<Optional<TreePath>, ProgramSetBuilder<path>>> ClusterOnInput(this ProgramSetBuilder<path> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<TreePath>, ProgramSetBuilder<path>>(Cluster.CastValue<TreePath>(kvp.Key), ProgramSetBuilder<path>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0601056D RID: 66925 RVA: 0x00387C4B File Offset: 0x00385E4B
		public static IEnumerable<KeyValuePair<Optional<Node>[], ProgramSetBuilder<guardedRule>>> ClusterOnInputTuple(this ProgramSetBuilder<guardedRule> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Node>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<Node>>(Cluster.CastValue<Node>));
				}
				return new KeyValuePair<Optional<Node>[], ProgramSetBuilder<guardedRule>>(key.Select(func).ToArray<Optional<Node>>(), ProgramSetBuilder<guardedRule>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0601056E RID: 66926 RVA: 0x00387C7D File Offset: 0x00385E7D
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<match>>> ClusterOnInputTuple(this ProgramSetBuilder<match> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<match>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<match>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0601056F RID: 66927 RVA: 0x00387CAF File Offset: 0x00385EAF
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

		// Token: 0x06010570 RID: 66928 RVA: 0x00387CE1 File Offset: 0x00385EE1
		public static IEnumerable<KeyValuePair<Optional<Node>[], ProgramSetBuilder<newDsl>>> ClusterOnInputTuple(this ProgramSetBuilder<newDsl> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Node>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<Node>>(Cluster.CastValue<Node>));
				}
				return new KeyValuePair<Optional<Node>[], ProgramSetBuilder<newDsl>>(key.Select(func).ToArray<Optional<Node>>(), ProgramSetBuilder<newDsl>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06010571 RID: 66929 RVA: 0x00387D13 File Offset: 0x00385F13
		public static IEnumerable<KeyValuePair<Optional<Node>[], ProgramSetBuilder<construction>>> ClusterOnInputTuple(this ProgramSetBuilder<construction> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Node>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<Node>>(Cluster.CastValue<Node>));
				}
				return new KeyValuePair<Optional<Node>[], ProgramSetBuilder<construction>>(key.Select(func).ToArray<Optional<Node>>(), ProgramSetBuilder<construction>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06010572 RID: 66930 RVA: 0x00387D45 File Offset: 0x00385F45
		public static IEnumerable<KeyValuePair<Optional<Node>[], ProgramSetBuilder<select>>> ClusterOnInputTuple(this ProgramSetBuilder<select> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Node>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<Node>>(Cluster.CastValue<Node>));
				}
				return new KeyValuePair<Optional<Node>[], ProgramSetBuilder<select>>(key.Select(func).ToArray<Optional<Node>>(), ProgramSetBuilder<select>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06010573 RID: 66931 RVA: 0x00387D77 File Offset: 0x00385F77
		public static IEnumerable<KeyValuePair<Optional<Node[]>[], ProgramSetBuilder<tmpFilter>>> ClusterOnInputTuple(this ProgramSetBuilder<tmpFilter> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Node[]>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<Node[]>>(Cluster.CastValue<Node[]>));
				}
				return new KeyValuePair<Optional<Node[]>[], ProgramSetBuilder<tmpFilter>>(key.Select(func).ToArray<Optional<Node[]>>(), ProgramSetBuilder<tmpFilter>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06010574 RID: 66932 RVA: 0x00387DA9 File Offset: 0x00385FA9
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<Node>>[], ProgramSetBuilder<sequenceChildren>>> ClusterOnInputTuple(this ProgramSetBuilder<sequenceChildren> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<Node>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<Node>>>(Cluster.CastValue<IEnumerable<Node>>));
				}
				return new KeyValuePair<Optional<IEnumerable<Node>>[], ProgramSetBuilder<sequenceChildren>>(key.Select(func).ToArray<Optional<IEnumerable<Node>>>(), ProgramSetBuilder<sequenceChildren>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06010575 RID: 66933 RVA: 0x00387DDB File Offset: 0x00385FDB
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<Node>>[], ProgramSetBuilder<convertSequence>>> ClusterOnInputTuple(this ProgramSetBuilder<convertSequence> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<Node>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<Node>>>(Cluster.CastValue<IEnumerable<Node>>));
				}
				return new KeyValuePair<Optional<IEnumerable<Node>>[], ProgramSetBuilder<convertSequence>>(key.Select(func).ToArray<Optional<IEnumerable<Node>>>(), ProgramSetBuilder<convertSequence>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06010576 RID: 66934 RVA: 0x00387E0D File Offset: 0x0038600D
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<Node>>[], ProgramSetBuilder<sequenceMap>>> ClusterOnInputTuple(this ProgramSetBuilder<sequenceMap> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<Node>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<Node>>>(Cluster.CastValue<IEnumerable<Node>>));
				}
				return new KeyValuePair<Optional<IEnumerable<Node>>[], ProgramSetBuilder<sequenceMap>>(key.Select(func).ToArray<Optional<IEnumerable<Node>>>(), ProgramSetBuilder<sequenceMap>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06010577 RID: 66935 RVA: 0x00387E3F File Offset: 0x0038603F
		public static IEnumerable<KeyValuePair<Optional<Node[]>[], ProgramSetBuilder<parentChildren>>> ClusterOnInputTuple(this ProgramSetBuilder<parentChildren> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Node[]>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<Node[]>>(Cluster.CastValue<Node[]>));
				}
				return new KeyValuePair<Optional<Node[]>[], ProgramSetBuilder<parentChildren>>(key.Select(func).ToArray<Optional<Node[]>>(), ProgramSetBuilder<parentChildren>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06010578 RID: 66936 RVA: 0x00387E71 File Offset: 0x00386071
		public static IEnumerable<KeyValuePair<Optional<Node[]>[], ProgramSetBuilder<relChildList>>> ClusterOnInputTuple(this ProgramSetBuilder<relChildList> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Node[]>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<Node[]>>(Cluster.CastValue<Node[]>));
				}
				return new KeyValuePair<Optional<Node[]>[], ProgramSetBuilder<relChildList>>(key.Select(func).ToArray<Optional<Node[]>>(), ProgramSetBuilder<relChildList>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06010579 RID: 66937 RVA: 0x00387EA3 File Offset: 0x003860A3
		public static IEnumerable<KeyValuePair<Optional<Node[]>[], ProgramSetBuilder<singleRelChildList>>> ClusterOnInputTuple(this ProgramSetBuilder<singleRelChildList> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Node[]>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<Node[]>>(Cluster.CastValue<Node[]>));
				}
				return new KeyValuePair<Optional<Node[]>[], ProgramSetBuilder<singleRelChildList>>(key.Select(func).ToArray<Optional<Node[]>>(), ProgramSetBuilder<singleRelChildList>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0601057A RID: 66938 RVA: 0x00387ED5 File Offset: 0x003860D5
		public static IEnumerable<KeyValuePair<Optional<Node>[], ProgramSetBuilder<relChild>>> ClusterOnInputTuple(this ProgramSetBuilder<relChild> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Node>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<Node>>(Cluster.CastValue<Node>));
				}
				return new KeyValuePair<Optional<Node>[], ProgramSetBuilder<relChild>>(key.Select(func).ToArray<Optional<Node>>(), ProgramSetBuilder<relChild>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0601057B RID: 66939 RVA: 0x00387F07 File Offset: 0x00386107
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<pos>>> ClusterOnInputTuple(this ProgramSetBuilder<pos> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<pos>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<pos>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0601057C RID: 66940 RVA: 0x00387F39 File Offset: 0x00386139
		public static IEnumerable<KeyValuePair<Optional<Node[]>[], ProgramSetBuilder<children>>> ClusterOnInputTuple(this ProgramSetBuilder<children> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Node[]>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<Node[]>>(Cluster.CastValue<Node[]>));
				}
				return new KeyValuePair<Optional<Node[]>[], ProgramSetBuilder<children>>(key.Select(func).ToArray<Optional<Node[]>>(), ProgramSetBuilder<children>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0601057D RID: 66941 RVA: 0x00387F6B File Offset: 0x0038616B
		public static IEnumerable<KeyValuePair<Optional<Node[]>[], ProgramSetBuilder<interval>>> ClusterOnInputTuple(this ProgramSetBuilder<interval> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Node[]>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<Node[]>>(Cluster.CastValue<Node[]>));
				}
				return new KeyValuePair<Optional<Node[]>[], ProgramSetBuilder<interval>>(key.Select(func).ToArray<Optional<Node[]>>(), ProgramSetBuilder<interval>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0601057E RID: 66942 RVA: 0x00387F9D File Offset: 0x0038619D
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<Node>>[], ProgramSetBuilder<inorderAllNodes>>> ClusterOnInputTuple(this ProgramSetBuilder<inorderAllNodes> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<Node>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<Node>>>(Cluster.CastValue<IEnumerable<Node>>));
				}
				return new KeyValuePair<Optional<IEnumerable<Node>>[], ProgramSetBuilder<inorderAllNodes>>(key.Select(func).ToArray<Optional<IEnumerable<Node>>>(), ProgramSetBuilder<inorderAllNodes>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0601057F RID: 66943 RVA: 0x00387FCF File Offset: 0x003861CF
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<label>>> ClusterOnInputTuple(this ProgramSetBuilder<label> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<label>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<label>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06010580 RID: 66944 RVA: 0x00388001 File Offset: 0x00386201
		public static IEnumerable<KeyValuePair<Optional<Dictionary<string, string>>[], ProgramSetBuilder<attributes>>> ClusterOnInputTuple(this ProgramSetBuilder<attributes> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Dictionary<string, string>>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<Dictionary<string, string>>>(Cluster.CastValue<Dictionary<string, string>>));
				}
				return new KeyValuePair<Optional<Dictionary<string, string>>[], ProgramSetBuilder<attributes>>(key.Select(func).ToArray<Optional<Dictionary<string, string>>>(), ProgramSetBuilder<attributes>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06010581 RID: 66945 RVA: 0x00388033 File Offset: 0x00386233
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<kind>>> ClusterOnInputTuple(this ProgramSetBuilder<kind> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<kind>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<kind>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06010582 RID: 66946 RVA: 0x00388065 File Offset: 0x00386265
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<name>>> ClusterOnInputTuple(this ProgramSetBuilder<name> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<name>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<name>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06010583 RID: 66947 RVA: 0x00388097 File Offset: 0x00386297
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<value>>> ClusterOnInputTuple(this ProgramSetBuilder<value> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<value>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<value>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06010584 RID: 66948 RVA: 0x003880C9 File Offset: 0x003862C9
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

		// Token: 0x06010585 RID: 66949 RVA: 0x003880FB File Offset: 0x003862FB
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<p>>> ClusterOnInputTuple(this ProgramSetBuilder<p> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<p>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<p>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06010586 RID: 66950 RVA: 0x0038812D File Offset: 0x0038632D
		public static IEnumerable<KeyValuePair<Optional<TreePath>[], ProgramSetBuilder<path>>> ClusterOnInputTuple(this ProgramSetBuilder<path> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<TreePath>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<TreePath>>(Cluster.CastValue<TreePath>));
				}
				return new KeyValuePair<Optional<TreePath>[], ProgramSetBuilder<path>>(key.Select(func).ToArray<Optional<TreePath>>(), ProgramSetBuilder<path>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x02001E56 RID: 7766
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400625A RID: 25178
			public static Func<object, Optional<Node>> <0>__CastValue;

			// Token: 0x0400625B RID: 25179
			public static Func<object, Optional<bool>> <1>__CastValue;

			// Token: 0x0400625C RID: 25180
			public static Func<object, Optional<Node[]>> <2>__CastValue;

			// Token: 0x0400625D RID: 25181
			public static Func<object, Optional<IEnumerable<Node>>> <3>__CastValue;

			// Token: 0x0400625E RID: 25182
			public static Func<object, Optional<int>> <4>__CastValue;

			// Token: 0x0400625F RID: 25183
			public static Func<object, Optional<string>> <5>__CastValue;

			// Token: 0x04006260 RID: 25184
			public static Func<object, Optional<Dictionary<string, string>>> <6>__CastValue;

			// Token: 0x04006261 RID: 25185
			public static Func<object, Optional<TreePath>> <7>__CastValue;
		}
	}
}
