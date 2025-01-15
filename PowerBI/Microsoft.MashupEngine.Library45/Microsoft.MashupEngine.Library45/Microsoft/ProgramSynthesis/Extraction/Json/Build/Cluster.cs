using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TreeOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build
{
	// Token: 0x02000B55 RID: 2901
	public static class Cluster
	{
		// Token: 0x06004906 RID: 18694 RVA: 0x000AB0AF File Offset: 0x000A92AF
		private static Optional<T> CastValue<T>(object obj)
		{
			if (!(obj is Bottom))
			{
				return ((T)((object)obj)).Some<T>();
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x06004907 RID: 18695 RVA: 0x000E70B7 File Offset: 0x000E52B7
		public static IEnumerable<KeyValuePair<Optional<JPath>, ProgramSetBuilder<path>>> ClusterOnInput(this ProgramSetBuilder<path> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<JPath>, ProgramSetBuilder<path>>(Cluster.CastValue<JPath>(kvp.Key), ProgramSetBuilder<path>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x000E70E9 File Offset: 0x000E52E9
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<id>>> ClusterOnInput(this ProgramSetBuilder<id> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<id>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<id>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06004909 RID: 18697 RVA: 0x000E711B File Offset: 0x000E531B
		public static IEnumerable<KeyValuePair<Optional<ITreeOutput<JsonRegion>>, ProgramSetBuilder<output>>> ClusterOnInput(this ProgramSetBuilder<output> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ITreeOutput<JsonRegion>>, ProgramSetBuilder<output>>(Cluster.CastValue<ITreeOutput<JsonRegion>>(kvp.Key), ProgramSetBuilder<output>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x000E714D File Offset: 0x000E534D
		public static IEnumerable<KeyValuePair<Optional<ITreeOutput<JsonRegion>>, ProgramSetBuilder<@struct>>> ClusterOnInput(this ProgramSetBuilder<@struct> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ITreeOutput<JsonRegion>>, ProgramSetBuilder<@struct>>(Cluster.CastValue<ITreeOutput<JsonRegion>>(kvp.Key), ProgramSetBuilder<@struct>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x000E717F File Offset: 0x000E537F
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<ITreeOutput<JsonRegion>>>, ProgramSetBuilder<structBodyRec>>> ClusterOnInput(this ProgramSetBuilder<structBodyRec> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<ITreeOutput<JsonRegion>>>, ProgramSetBuilder<structBodyRec>>(Cluster.CastValue<IEnumerable<ITreeOutput<JsonRegion>>>(kvp.Key), ProgramSetBuilder<structBodyRec>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x000E71B1 File Offset: 0x000E53B1
		public static IEnumerable<KeyValuePair<Optional<ITreeOutput<JsonRegion>>, ProgramSetBuilder<sequence>>> ClusterOnInput(this ProgramSetBuilder<sequence> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ITreeOutput<JsonRegion>>, ProgramSetBuilder<sequence>>(Cluster.CastValue<ITreeOutput<JsonRegion>>(kvp.Key), ProgramSetBuilder<sequence>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x000E71E3 File Offset: 0x000E53E3
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<ITreeOutput<JsonRegion>>>, ProgramSetBuilder<sequenceBody>>> ClusterOnInput(this ProgramSetBuilder<sequenceBody> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<ITreeOutput<JsonRegion>>>, ProgramSetBuilder<sequenceBody>>(Cluster.CastValue<IEnumerable<ITreeOutput<JsonRegion>>>(kvp.Key), ProgramSetBuilder<sequenceBody>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x000E7215 File Offset: 0x000E5415
		public static IEnumerable<KeyValuePair<Optional<ITreeOutput<JsonRegion>>, ProgramSetBuilder<wrapStruct>>> ClusterOnInput(this ProgramSetBuilder<wrapStruct> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ITreeOutput<JsonRegion>>, ProgramSetBuilder<wrapStruct>>(Cluster.CastValue<ITreeOutput<JsonRegion>>(kvp.Key), ProgramSetBuilder<wrapStruct>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600490F RID: 18703 RVA: 0x000E7247 File Offset: 0x000E5447
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<JsonRegion>>, ProgramSetBuilder<selectSequence>>> ClusterOnInput(this ProgramSetBuilder<selectSequence> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<JsonRegion>>, ProgramSetBuilder<selectSequence>>(Cluster.CastValue<IEnumerable<JsonRegion>>(kvp.Key), ProgramSetBuilder<selectSequence>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x000E7279 File Offset: 0x000E5479
		public static IEnumerable<KeyValuePair<Optional<JsonRegion>, ProgramSetBuilder<selectRegion>>> ClusterOnInput(this ProgramSetBuilder<selectRegion> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<JsonRegion>, ProgramSetBuilder<selectRegion>>(Cluster.CastValue<JsonRegion>(kvp.Key), ProgramSetBuilder<selectRegion>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x000E72AB File Offset: 0x000E54AB
		public static IEnumerable<KeyValuePair<Optional<JPath>[], ProgramSetBuilder<path>>> ClusterOnInputTuple(this ProgramSetBuilder<path> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<JPath>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<JPath>>(Cluster.CastValue<JPath>));
				}
				return new KeyValuePair<Optional<JPath>[], ProgramSetBuilder<path>>(key.Select(func).ToArray<Optional<JPath>>(), ProgramSetBuilder<path>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x000E72DD File Offset: 0x000E54DD
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<id>>> ClusterOnInputTuple(this ProgramSetBuilder<id> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<id>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<id>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x000E730F File Offset: 0x000E550F
		public static IEnumerable<KeyValuePair<Optional<ITreeOutput<JsonRegion>>[], ProgramSetBuilder<output>>> ClusterOnInputTuple(this ProgramSetBuilder<output> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ITreeOutput<JsonRegion>>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<ITreeOutput<JsonRegion>>>(Cluster.CastValue<ITreeOutput<JsonRegion>>));
				}
				return new KeyValuePair<Optional<ITreeOutput<JsonRegion>>[], ProgramSetBuilder<output>>(key.Select(func).ToArray<Optional<ITreeOutput<JsonRegion>>>(), ProgramSetBuilder<output>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x000E7341 File Offset: 0x000E5541
		public static IEnumerable<KeyValuePair<Optional<ITreeOutput<JsonRegion>>[], ProgramSetBuilder<@struct>>> ClusterOnInputTuple(this ProgramSetBuilder<@struct> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ITreeOutput<JsonRegion>>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<ITreeOutput<JsonRegion>>>(Cluster.CastValue<ITreeOutput<JsonRegion>>));
				}
				return new KeyValuePair<Optional<ITreeOutput<JsonRegion>>[], ProgramSetBuilder<@struct>>(key.Select(func).ToArray<Optional<ITreeOutput<JsonRegion>>>(), ProgramSetBuilder<@struct>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x000E7373 File Offset: 0x000E5573
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<ITreeOutput<JsonRegion>>>[], ProgramSetBuilder<structBodyRec>>> ClusterOnInputTuple(this ProgramSetBuilder<structBodyRec> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<ITreeOutput<JsonRegion>>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<ITreeOutput<JsonRegion>>>>(Cluster.CastValue<IEnumerable<ITreeOutput<JsonRegion>>>));
				}
				return new KeyValuePair<Optional<IEnumerable<ITreeOutput<JsonRegion>>>[], ProgramSetBuilder<structBodyRec>>(key.Select(func).ToArray<Optional<IEnumerable<ITreeOutput<JsonRegion>>>>(), ProgramSetBuilder<structBodyRec>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x000E73A5 File Offset: 0x000E55A5
		public static IEnumerable<KeyValuePair<Optional<ITreeOutput<JsonRegion>>[], ProgramSetBuilder<sequence>>> ClusterOnInputTuple(this ProgramSetBuilder<sequence> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ITreeOutput<JsonRegion>>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<ITreeOutput<JsonRegion>>>(Cluster.CastValue<ITreeOutput<JsonRegion>>));
				}
				return new KeyValuePair<Optional<ITreeOutput<JsonRegion>>[], ProgramSetBuilder<sequence>>(key.Select(func).ToArray<Optional<ITreeOutput<JsonRegion>>>(), ProgramSetBuilder<sequence>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x000E73D7 File Offset: 0x000E55D7
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<ITreeOutput<JsonRegion>>>[], ProgramSetBuilder<sequenceBody>>> ClusterOnInputTuple(this ProgramSetBuilder<sequenceBody> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<ITreeOutput<JsonRegion>>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<ITreeOutput<JsonRegion>>>>(Cluster.CastValue<IEnumerable<ITreeOutput<JsonRegion>>>));
				}
				return new KeyValuePair<Optional<IEnumerable<ITreeOutput<JsonRegion>>>[], ProgramSetBuilder<sequenceBody>>(key.Select(func).ToArray<Optional<IEnumerable<ITreeOutput<JsonRegion>>>>(), ProgramSetBuilder<sequenceBody>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x000E7409 File Offset: 0x000E5609
		public static IEnumerable<KeyValuePair<Optional<ITreeOutput<JsonRegion>>[], ProgramSetBuilder<wrapStruct>>> ClusterOnInputTuple(this ProgramSetBuilder<wrapStruct> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ITreeOutput<JsonRegion>>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<ITreeOutput<JsonRegion>>>(Cluster.CastValue<ITreeOutput<JsonRegion>>));
				}
				return new KeyValuePair<Optional<ITreeOutput<JsonRegion>>[], ProgramSetBuilder<wrapStruct>>(key.Select(func).ToArray<Optional<ITreeOutput<JsonRegion>>>(), ProgramSetBuilder<wrapStruct>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x000E743B File Offset: 0x000E563B
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<JsonRegion>>[], ProgramSetBuilder<selectSequence>>> ClusterOnInputTuple(this ProgramSetBuilder<selectSequence> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<JsonRegion>>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<IEnumerable<JsonRegion>>>(Cluster.CastValue<IEnumerable<JsonRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<JsonRegion>>[], ProgramSetBuilder<selectSequence>>(key.Select(func).ToArray<Optional<IEnumerable<JsonRegion>>>(), ProgramSetBuilder<selectSequence>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x000E746D File Offset: 0x000E566D
		public static IEnumerable<KeyValuePair<Optional<JsonRegion>[], ProgramSetBuilder<selectRegion>>> ClusterOnInputTuple(this ProgramSetBuilder<selectRegion> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<JsonRegion>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<JsonRegion>>(Cluster.CastValue<JsonRegion>));
				}
				return new KeyValuePair<Optional<JsonRegion>[], ProgramSetBuilder<selectRegion>>(key.Select(func).ToArray<Optional<JsonRegion>>(), ProgramSetBuilder<selectRegion>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x02000B56 RID: 2902
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04002138 RID: 8504
			public static Func<object, Optional<JPath>> <0>__CastValue;

			// Token: 0x04002139 RID: 8505
			public static Func<object, Optional<string>> <1>__CastValue;

			// Token: 0x0400213A RID: 8506
			public static Func<object, Optional<ITreeOutput<JsonRegion>>> <2>__CastValue;

			// Token: 0x0400213B RID: 8507
			public static Func<object, Optional<IEnumerable<ITreeOutput<JsonRegion>>>> <3>__CastValue;

			// Token: 0x0400213C RID: 8508
			public static Func<object, Optional<IEnumerable<JsonRegion>>> <4>__CastValue;

			// Token: 0x0400213D RID: 8509
			public static Func<object, Optional<JsonRegion>> <5>__CastValue;
		}
	}
}
