using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Conditionals.Build
{
	// Token: 0x02000A31 RID: 2609
	public static class Cluster
	{
		// Token: 0x06003FCA RID: 16330 RVA: 0x000AB0AF File Offset: 0x000A92AF
		private static Optional<T> CastValue<T>(object obj)
		{
			if (!(obj is Bottom))
			{
				return ((T)((object)obj)).Some<T>();
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x000C99B7 File Offset: 0x000C7BB7
		public static IEnumerable<KeyValuePair<Optional<RegularExpression>, ProgramSetBuilder<r>>> ClusterOnInput(this ProgramSetBuilder<r> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<RegularExpression>, ProgramSetBuilder<r>>(Cluster.CastValue<RegularExpression>(kvp.Key), ProgramSetBuilder<r>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x000C99E9 File Offset: 0x000C7BE9
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<k>>> ClusterOnInput(this ProgramSetBuilder<k> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<k>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<k>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x000C9A1B File Offset: 0x000C7C1B
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<str>>> ClusterOnInput(this ProgramSetBuilder<str> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<str>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<str>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x000C9A4D File Offset: 0x000C7C4D
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<output>>> ClusterOnInput(this ProgramSetBuilder<output> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<output>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<output>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x000C9A7F File Offset: 0x000C7C7F
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<disjunct>>> ClusterOnInput(this ProgramSetBuilder<disjunct> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<disjunct>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<disjunct>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x000C9AB1 File Offset: 0x000C7CB1
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<conjunct>>> ClusterOnInput(this ProgramSetBuilder<conjunct> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<conjunct>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<conjunct>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x000C9AE3 File Offset: 0x000C7CE3
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<baseConjunct>>> ClusterOnInput(this ProgramSetBuilder<baseConjunct> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<baseConjunct>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<baseConjunct>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x000C9B15 File Offset: 0x000C7D15
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<pred>>> ClusterOnInput(this ProgramSetBuilder<pred> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<pred>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<pred>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x000C9B47 File Offset: 0x000C7D47
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<match>>> ClusterOnInput(this ProgramSetBuilder<match> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<match>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<match>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x000C9B79 File Offset: 0x000C7D79
		public static IEnumerable<KeyValuePair<Optional<RegularExpression>[], ProgramSetBuilder<r>>> ClusterOnInputTuple(this ProgramSetBuilder<r> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<RegularExpression>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<RegularExpression>>(Cluster.CastValue<RegularExpression>));
				}
				return new KeyValuePair<Optional<RegularExpression>[], ProgramSetBuilder<r>>(key.Select(func).ToArray<Optional<RegularExpression>>(), ProgramSetBuilder<r>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x000C9BAB File Offset: 0x000C7DAB
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<k>>> ClusterOnInputTuple(this ProgramSetBuilder<k> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<k>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<k>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x000C9BDD File Offset: 0x000C7DDD
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<str>>> ClusterOnInputTuple(this ProgramSetBuilder<str> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<str>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<str>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x000C9C0F File Offset: 0x000C7E0F
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<output>>> ClusterOnInputTuple(this ProgramSetBuilder<output> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<output>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<output>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x000C9C41 File Offset: 0x000C7E41
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<disjunct>>> ClusterOnInputTuple(this ProgramSetBuilder<disjunct> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<disjunct>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<disjunct>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x000C9C73 File Offset: 0x000C7E73
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<conjunct>>> ClusterOnInputTuple(this ProgramSetBuilder<conjunct> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<conjunct>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<conjunct>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x000C9CA5 File Offset: 0x000C7EA5
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<baseConjunct>>> ClusterOnInputTuple(this ProgramSetBuilder<baseConjunct> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<baseConjunct>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<baseConjunct>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x000C9CD7 File Offset: 0x000C7ED7
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<pred>>> ClusterOnInputTuple(this ProgramSetBuilder<pred> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<pred>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<pred>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x000C9D09 File Offset: 0x000C7F09
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<match>>> ClusterOnInputTuple(this ProgramSetBuilder<match> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<match>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<match>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x02000A32 RID: 2610
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001D58 RID: 7512
			public static Func<object, Optional<RegularExpression>> <0>__CastValue;

			// Token: 0x04001D59 RID: 7513
			public static Func<object, Optional<int>> <1>__CastValue;

			// Token: 0x04001D5A RID: 7514
			public static Func<object, Optional<string>> <2>__CastValue;

			// Token: 0x04001D5B RID: 7515
			public static Func<object, Optional<bool>> <3>__CastValue;
		}
	}
}
