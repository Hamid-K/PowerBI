using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build
{
	// Token: 0x02000F1B RID: 3867
	public static class Cluster
	{
		// Token: 0x06006AA3 RID: 27299 RVA: 0x000AB0AF File Offset: 0x000A92AF
		private static Optional<T> CastValue<T>(object obj)
		{
			if (!(obj is Bottom))
			{
				return ((T)((object)obj)).Some<T>();
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x06006AA4 RID: 27300 RVA: 0x0015F757 File Offset: 0x0015D957
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<k>>> ClusterOnInput(this ProgramSetBuilder<k> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<k>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<k>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AA5 RID: 27301 RVA: 0x0015F789 File Offset: 0x0015D989
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<str>>> ClusterOnInput(this ProgramSetBuilder<str> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<str>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<str>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AA6 RID: 27302 RVA: 0x0015F7BB File Offset: 0x0015D9BB
		public static IEnumerable<KeyValuePair<Optional<Optional<string>>, ProgramSetBuilder<del>>> ClusterOnInput(this ProgramSetBuilder<del> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Optional<string>>, ProgramSetBuilder<del>>(Cluster.CastValue<Optional<string>>(kvp.Key), ProgramSetBuilder<del>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AA7 RID: 27303 RVA: 0x0015F7ED File Offset: 0x0015D9ED
		public static IEnumerable<KeyValuePair<Optional<Regex>, ProgramSetBuilder<re>>> ClusterOnInput(this ProgramSetBuilder<re> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Regex>, ProgramSetBuilder<re>>(Cluster.CastValue<Regex>(kvp.Key), ProgramSetBuilder<re>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AA8 RID: 27304 RVA: 0x0015F81F File Offset: 0x0015DA1F
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<string>>, ProgramSetBuilder<columnNames>>> ClusterOnInput(this ProgramSetBuilder<columnNames> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IReadOnlyList<string>>, ProgramSetBuilder<columnNames>>(Cluster.CastValue<IReadOnlyList<string>>(kvp.Key), ProgramSetBuilder<columnNames>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AA9 RID: 27305 RVA: 0x0015F851 File Offset: 0x0015DA51
		public static IEnumerable<KeyValuePair<Optional<ITable<StringRegion>>, ProgramSetBuilder<output>>> ClusterOnInput(this ProgramSetBuilder<output> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ITable<StringRegion>>, ProgramSetBuilder<output>>(Cluster.CastValue<ITable<StringRegion>>(kvp.Key), ProgramSetBuilder<output>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AAA RID: 27306 RVA: 0x0015F883 File Offset: 0x0015DA83
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IReadOnlyList<StringRegion>>>, ProgramSetBuilder<table>>> ClusterOnInput(this ProgramSetBuilder<table> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IReadOnlyList<StringRegion>>>, ProgramSetBuilder<table>>(Cluster.CastValue<IEnumerable<IReadOnlyList<StringRegion>>>(kvp.Key), ProgramSetBuilder<table>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AAB RID: 27307 RVA: 0x0015F8B5 File Offset: 0x0015DAB5
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<StringRegion>>, ProgramSetBuilder<colSplit>>> ClusterOnInput(this ProgramSetBuilder<colSplit> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IReadOnlyList<StringRegion>>, ProgramSetBuilder<colSplit>>(Cluster.CastValue<IReadOnlyList<StringRegion>>(kvp.Key), ProgramSetBuilder<colSplit>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AAC RID: 27308 RVA: 0x0015F8E7 File Offset: 0x0015DAE7
		public static IEnumerable<KeyValuePair<Optional<StringRegion>, ProgramSetBuilder<extractTup>>> ClusterOnInput(this ProgramSetBuilder<extractTup> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<StringRegion>, ProgramSetBuilder<extractTup>>(Cluster.CastValue<StringRegion>(kvp.Key), ProgramSetBuilder<extractTup>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AAD RID: 27309 RVA: 0x0015F919 File Offset: 0x0015DB19
		public static IEnumerable<KeyValuePair<Optional<StringRegion>, ProgramSetBuilder<trimExtract>>> ClusterOnInput(this ProgramSetBuilder<trimExtract> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<StringRegion>, ProgramSetBuilder<trimExtract>>(Cluster.CastValue<StringRegion>(kvp.Key), ProgramSetBuilder<trimExtract>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AAE RID: 27310 RVA: 0x0015F94B File Offset: 0x0015DB4B
		public static IEnumerable<KeyValuePair<Optional<StringRegion>, ProgramSetBuilder<extract>>> ClusterOnInput(this ProgramSetBuilder<extract> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<StringRegion>, ProgramSetBuilder<extract>>(Cluster.CastValue<StringRegion>(kvp.Key), ProgramSetBuilder<extract>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AAF RID: 27311 RVA: 0x0015F97D File Offset: 0x0015DB7D
		public static IEnumerable<KeyValuePair<Optional<Record<StringRegion, StringRegion>>, ProgramSetBuilder<split>>> ClusterOnInput(this ProgramSetBuilder<split> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Record<StringRegion, StringRegion>>, ProgramSetBuilder<split>>(Cluster.CastValue<Record<StringRegion, StringRegion>>(kvp.Key), ProgramSetBuilder<split>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AB0 RID: 27312 RVA: 0x0015F9AF File Offset: 0x0015DBAF
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<StringRegion>>, ProgramSetBuilder<records>>> ClusterOnInput(this ProgramSetBuilder<records> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IReadOnlyList<StringRegion>>, ProgramSetBuilder<records>>(Cluster.CastValue<IReadOnlyList<StringRegion>>(kvp.Key), ProgramSetBuilder<records>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AB1 RID: 27313 RVA: 0x0015F9E1 File Offset: 0x0015DBE1
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<StringRegion>>, ProgramSetBuilder<skip>>> ClusterOnInput(this ProgramSetBuilder<skip> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IReadOnlyList<StringRegion>>, ProgramSetBuilder<skip>>(Cluster.CastValue<IReadOnlyList<StringRegion>>(kvp.Key), ProgramSetBuilder<skip>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AB2 RID: 27314 RVA: 0x0015FA13 File Offset: 0x0015DC13
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<StringRegion>>, ProgramSetBuilder<lines>>> ClusterOnInput(this ProgramSetBuilder<lines> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IReadOnlyList<StringRegion>>, ProgramSetBuilder<lines>>(Cluster.CastValue<IReadOnlyList<StringRegion>>(kvp.Key), ProgramSetBuilder<lines>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AB3 RID: 27315 RVA: 0x0015FA45 File Offset: 0x0015DC45
		public static IEnumerable<KeyValuePair<Optional<StringRegion>, ProgramSetBuilder<_LetB0>>> ClusterOnInput(this ProgramSetBuilder<_LetB0> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<StringRegion>, ProgramSetBuilder<_LetB0>>(Cluster.CastValue<StringRegion>(kvp.Key), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AB4 RID: 27316 RVA: 0x0015FA77 File Offset: 0x0015DC77
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<StringRegion>>, ProgramSetBuilder<_LetB1>>> ClusterOnInput(this ProgramSetBuilder<_LetB1> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IReadOnlyList<StringRegion>>, ProgramSetBuilder<_LetB1>>(Cluster.CastValue<IReadOnlyList<StringRegion>>(kvp.Key), ProgramSetBuilder<_LetB1>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AB5 RID: 27317 RVA: 0x0015FAA9 File Offset: 0x0015DCA9
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<StringRegion>>, ProgramSetBuilder<_LetB2>>> ClusterOnInput(this ProgramSetBuilder<_LetB2> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IReadOnlyList<StringRegion>>, ProgramSetBuilder<_LetB2>>(Cluster.CastValue<IReadOnlyList<StringRegion>>(kvp.Key), ProgramSetBuilder<_LetB2>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AB6 RID: 27318 RVA: 0x0015FADB File Offset: 0x0015DCDB
		public static IEnumerable<KeyValuePair<Optional<StringRegion>, ProgramSetBuilder<_LetB3>>> ClusterOnInput(this ProgramSetBuilder<_LetB3> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<StringRegion>, ProgramSetBuilder<_LetB3>>(Cluster.CastValue<StringRegion>(kvp.Key), ProgramSetBuilder<_LetB3>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06006AB7 RID: 27319 RVA: 0x0015FB0D File Offset: 0x0015DD0D
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<k>>> ClusterOnInputTuple(this ProgramSetBuilder<k> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<k>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<k>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006AB8 RID: 27320 RVA: 0x0015FB3F File Offset: 0x0015DD3F
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<str>>> ClusterOnInputTuple(this ProgramSetBuilder<str> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<str>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<str>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006AB9 RID: 27321 RVA: 0x0015FB71 File Offset: 0x0015DD71
		public static IEnumerable<KeyValuePair<Optional<Optional<string>>[], ProgramSetBuilder<del>>> ClusterOnInputTuple(this ProgramSetBuilder<del> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Optional<string>>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<Optional<string>>>(Cluster.CastValue<Optional<string>>));
				}
				return new KeyValuePair<Optional<Optional<string>>[], ProgramSetBuilder<del>>(key.Select(func).ToArray<Optional<Optional<string>>>(), ProgramSetBuilder<del>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006ABA RID: 27322 RVA: 0x0015FBA3 File Offset: 0x0015DDA3
		public static IEnumerable<KeyValuePair<Optional<Regex>[], ProgramSetBuilder<re>>> ClusterOnInputTuple(this ProgramSetBuilder<re> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Regex>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<Regex>>(Cluster.CastValue<Regex>));
				}
				return new KeyValuePair<Optional<Regex>[], ProgramSetBuilder<re>>(key.Select(func).ToArray<Optional<Regex>>(), ProgramSetBuilder<re>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006ABB RID: 27323 RVA: 0x0015FBD5 File Offset: 0x0015DDD5
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<string>>[], ProgramSetBuilder<columnNames>>> ClusterOnInputTuple(this ProgramSetBuilder<columnNames> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IReadOnlyList<string>>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<IReadOnlyList<string>>>(Cluster.CastValue<IReadOnlyList<string>>));
				}
				return new KeyValuePair<Optional<IReadOnlyList<string>>[], ProgramSetBuilder<columnNames>>(key.Select(func).ToArray<Optional<IReadOnlyList<string>>>(), ProgramSetBuilder<columnNames>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006ABC RID: 27324 RVA: 0x0015FC07 File Offset: 0x0015DE07
		public static IEnumerable<KeyValuePair<Optional<ITable<StringRegion>>[], ProgramSetBuilder<output>>> ClusterOnInputTuple(this ProgramSetBuilder<output> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ITable<StringRegion>>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<ITable<StringRegion>>>(Cluster.CastValue<ITable<StringRegion>>));
				}
				return new KeyValuePair<Optional<ITable<StringRegion>>[], ProgramSetBuilder<output>>(key.Select(func).ToArray<Optional<ITable<StringRegion>>>(), ProgramSetBuilder<output>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006ABD RID: 27325 RVA: 0x0015FC39 File Offset: 0x0015DE39
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IReadOnlyList<StringRegion>>>[], ProgramSetBuilder<table>>> ClusterOnInputTuple(this ProgramSetBuilder<table> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IReadOnlyList<StringRegion>>>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<IEnumerable<IReadOnlyList<StringRegion>>>>(Cluster.CastValue<IEnumerable<IReadOnlyList<StringRegion>>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IReadOnlyList<StringRegion>>>[], ProgramSetBuilder<table>>(key.Select(func).ToArray<Optional<IEnumerable<IReadOnlyList<StringRegion>>>>(), ProgramSetBuilder<table>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006ABE RID: 27326 RVA: 0x0015FC6B File Offset: 0x0015DE6B
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<StringRegion>>[], ProgramSetBuilder<colSplit>>> ClusterOnInputTuple(this ProgramSetBuilder<colSplit> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IReadOnlyList<StringRegion>>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<IReadOnlyList<StringRegion>>>(Cluster.CastValue<IReadOnlyList<StringRegion>>));
				}
				return new KeyValuePair<Optional<IReadOnlyList<StringRegion>>[], ProgramSetBuilder<colSplit>>(key.Select(func).ToArray<Optional<IReadOnlyList<StringRegion>>>(), ProgramSetBuilder<colSplit>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006ABF RID: 27327 RVA: 0x0015FC9D File Offset: 0x0015DE9D
		public static IEnumerable<KeyValuePair<Optional<StringRegion>[], ProgramSetBuilder<extractTup>>> ClusterOnInputTuple(this ProgramSetBuilder<extractTup> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<StringRegion>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<StringRegion>>(Cluster.CastValue<StringRegion>));
				}
				return new KeyValuePair<Optional<StringRegion>[], ProgramSetBuilder<extractTup>>(key.Select(func).ToArray<Optional<StringRegion>>(), ProgramSetBuilder<extractTup>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006AC0 RID: 27328 RVA: 0x0015FCCF File Offset: 0x0015DECF
		public static IEnumerable<KeyValuePair<Optional<StringRegion>[], ProgramSetBuilder<trimExtract>>> ClusterOnInputTuple(this ProgramSetBuilder<trimExtract> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<StringRegion>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<StringRegion>>(Cluster.CastValue<StringRegion>));
				}
				return new KeyValuePair<Optional<StringRegion>[], ProgramSetBuilder<trimExtract>>(key.Select(func).ToArray<Optional<StringRegion>>(), ProgramSetBuilder<trimExtract>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006AC1 RID: 27329 RVA: 0x0015FD01 File Offset: 0x0015DF01
		public static IEnumerable<KeyValuePair<Optional<StringRegion>[], ProgramSetBuilder<extract>>> ClusterOnInputTuple(this ProgramSetBuilder<extract> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<StringRegion>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<StringRegion>>(Cluster.CastValue<StringRegion>));
				}
				return new KeyValuePair<Optional<StringRegion>[], ProgramSetBuilder<extract>>(key.Select(func).ToArray<Optional<StringRegion>>(), ProgramSetBuilder<extract>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006AC2 RID: 27330 RVA: 0x0015FD33 File Offset: 0x0015DF33
		public static IEnumerable<KeyValuePair<Optional<Record<StringRegion, StringRegion>>[], ProgramSetBuilder<split>>> ClusterOnInputTuple(this ProgramSetBuilder<split> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Record<StringRegion, StringRegion>>> func;
				if ((func = Cluster.<>O.<9>__CastValue) == null)
				{
					func = (Cluster.<>O.<9>__CastValue = new Func<object, Optional<Record<StringRegion, StringRegion>>>(Cluster.CastValue<Record<StringRegion, StringRegion>>));
				}
				return new KeyValuePair<Optional<Record<StringRegion, StringRegion>>[], ProgramSetBuilder<split>>(key.Select(func).ToArray<Optional<Record<StringRegion, StringRegion>>>(), ProgramSetBuilder<split>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006AC3 RID: 27331 RVA: 0x0015FD65 File Offset: 0x0015DF65
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<StringRegion>>[], ProgramSetBuilder<records>>> ClusterOnInputTuple(this ProgramSetBuilder<records> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IReadOnlyList<StringRegion>>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<IReadOnlyList<StringRegion>>>(Cluster.CastValue<IReadOnlyList<StringRegion>>));
				}
				return new KeyValuePair<Optional<IReadOnlyList<StringRegion>>[], ProgramSetBuilder<records>>(key.Select(func).ToArray<Optional<IReadOnlyList<StringRegion>>>(), ProgramSetBuilder<records>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006AC4 RID: 27332 RVA: 0x0015FD97 File Offset: 0x0015DF97
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<StringRegion>>[], ProgramSetBuilder<skip>>> ClusterOnInputTuple(this ProgramSetBuilder<skip> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IReadOnlyList<StringRegion>>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<IReadOnlyList<StringRegion>>>(Cluster.CastValue<IReadOnlyList<StringRegion>>));
				}
				return new KeyValuePair<Optional<IReadOnlyList<StringRegion>>[], ProgramSetBuilder<skip>>(key.Select(func).ToArray<Optional<IReadOnlyList<StringRegion>>>(), ProgramSetBuilder<skip>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006AC5 RID: 27333 RVA: 0x0015FDC9 File Offset: 0x0015DFC9
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<StringRegion>>[], ProgramSetBuilder<lines>>> ClusterOnInputTuple(this ProgramSetBuilder<lines> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IReadOnlyList<StringRegion>>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<IReadOnlyList<StringRegion>>>(Cluster.CastValue<IReadOnlyList<StringRegion>>));
				}
				return new KeyValuePair<Optional<IReadOnlyList<StringRegion>>[], ProgramSetBuilder<lines>>(key.Select(func).ToArray<Optional<IReadOnlyList<StringRegion>>>(), ProgramSetBuilder<lines>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006AC6 RID: 27334 RVA: 0x0015FDFB File Offset: 0x0015DFFB
		public static IEnumerable<KeyValuePair<Optional<StringRegion>[], ProgramSetBuilder<_LetB0>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB0> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<StringRegion>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<StringRegion>>(Cluster.CastValue<StringRegion>));
				}
				return new KeyValuePair<Optional<StringRegion>[], ProgramSetBuilder<_LetB0>>(key.Select(func).ToArray<Optional<StringRegion>>(), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006AC7 RID: 27335 RVA: 0x0015FE2D File Offset: 0x0015E02D
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<StringRegion>>[], ProgramSetBuilder<_LetB1>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB1> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IReadOnlyList<StringRegion>>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<IReadOnlyList<StringRegion>>>(Cluster.CastValue<IReadOnlyList<StringRegion>>));
				}
				return new KeyValuePair<Optional<IReadOnlyList<StringRegion>>[], ProgramSetBuilder<_LetB1>>(key.Select(func).ToArray<Optional<IReadOnlyList<StringRegion>>>(), ProgramSetBuilder<_LetB1>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006AC8 RID: 27336 RVA: 0x0015FE5F File Offset: 0x0015E05F
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<StringRegion>>[], ProgramSetBuilder<_LetB2>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB2> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IReadOnlyList<StringRegion>>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<IReadOnlyList<StringRegion>>>(Cluster.CastValue<IReadOnlyList<StringRegion>>));
				}
				return new KeyValuePair<Optional<IReadOnlyList<StringRegion>>[], ProgramSetBuilder<_LetB2>>(key.Select(func).ToArray<Optional<IReadOnlyList<StringRegion>>>(), ProgramSetBuilder<_LetB2>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06006AC9 RID: 27337 RVA: 0x0015FE91 File Offset: 0x0015E091
		public static IEnumerable<KeyValuePair<Optional<StringRegion>[], ProgramSetBuilder<_LetB3>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB3> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<StringRegion>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<StringRegion>>(Cluster.CastValue<StringRegion>));
				}
				return new KeyValuePair<Optional<StringRegion>[], ProgramSetBuilder<_LetB3>>(key.Select(func).ToArray<Optional<StringRegion>>(), ProgramSetBuilder<_LetB3>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x02000F1C RID: 3868
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04002ED8 RID: 11992
			public static Func<object, Optional<int>> <0>__CastValue;

			// Token: 0x04002ED9 RID: 11993
			public static Func<object, Optional<string>> <1>__CastValue;

			// Token: 0x04002EDA RID: 11994
			public static Func<object, Optional<Optional<string>>> <2>__CastValue;

			// Token: 0x04002EDB RID: 11995
			public static Func<object, Optional<Regex>> <3>__CastValue;

			// Token: 0x04002EDC RID: 11996
			public static Func<object, Optional<IReadOnlyList<string>>> <4>__CastValue;

			// Token: 0x04002EDD RID: 11997
			public static Func<object, Optional<ITable<StringRegion>>> <5>__CastValue;

			// Token: 0x04002EDE RID: 11998
			public static Func<object, Optional<IEnumerable<IReadOnlyList<StringRegion>>>> <6>__CastValue;

			// Token: 0x04002EDF RID: 11999
			public static Func<object, Optional<IReadOnlyList<StringRegion>>> <7>__CastValue;

			// Token: 0x04002EE0 RID: 12000
			public static Func<object, Optional<StringRegion>> <8>__CastValue;

			// Token: 0x04002EE1 RID: 12001
			public static Func<object, Optional<Record<StringRegion, StringRegion>>> <9>__CastValue;
		}
	}
}
