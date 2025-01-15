using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build
{
	// Token: 0x020011DB RID: 4571
	public static class Cluster
	{
		// Token: 0x06008915 RID: 35093 RVA: 0x000AB0AF File Offset: 0x000A92AF
		private static Optional<T> CastValue<T>(object obj)
		{
			if (!(obj is Bottom))
			{
				return ((T)((object)obj)).Some<T>();
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x06008916 RID: 35094 RVA: 0x001CE427 File Offset: 0x001CC627
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<result>>> ClusterOnInput(this ProgramSetBuilder<result> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<result>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<result>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008917 RID: 35095 RVA: 0x001CE459 File Offset: 0x001CC659
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<disjunctive_match>>> ClusterOnInput(this ProgramSetBuilder<disjunctive_match> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<disjunctive_match>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<disjunctive_match>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008918 RID: 35096 RVA: 0x001CE48B File Offset: 0x001CC68B
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<match>>> ClusterOnInput(this ProgramSetBuilder<match> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<match>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<match>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008919 RID: 35097 RVA: 0x001CE4BD File Offset: 0x001CC6BD
		public static IEnumerable<KeyValuePair<Optional<IToken>, ProgramSetBuilder<token>>> ClusterOnInput(this ProgramSetBuilder<token> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IToken>, ProgramSetBuilder<token>>(Cluster.CastValue<IToken>(kvp.Key), ProgramSetBuilder<token>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600891A RID: 35098 RVA: 0x001CE4EF File Offset: 0x001CC6EF
		public static IEnumerable<KeyValuePair<Optional<ImmutableList<bool>>, ProgramSetBuilder<multi_result>>> ClusterOnInput(this ProgramSetBuilder<multi_result> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ImmutableList<bool>>, ProgramSetBuilder<multi_result>>(Cluster.CastValue<ImmutableList<bool>>(kvp.Key), ProgramSetBuilder<multi_result>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600891B RID: 35099 RVA: 0x001CE521 File Offset: 0x001CC721
		public static IEnumerable<KeyValuePair<Optional<ImmutableList<bool>>, ProgramSetBuilder<multi_result_matches>>> ClusterOnInput(this ProgramSetBuilder<multi_result_matches> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ImmutableList<bool>>, ProgramSetBuilder<multi_result_matches>>(Cluster.CastValue<ImmutableList<bool>>(kvp.Key), ProgramSetBuilder<multi_result_matches>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600891C RID: 35100 RVA: 0x001CE553 File Offset: 0x001CC753
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<SuffixRegion>>, ProgramSetBuilder<inputSRegions>>> ClusterOnInput(this ProgramSetBuilder<inputSRegions> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<SuffixRegion>>, ProgramSetBuilder<inputSRegions>>(Cluster.CastValue<IEnumerable<SuffixRegion>>(kvp.Key), ProgramSetBuilder<inputSRegions>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600891D RID: 35101 RVA: 0x001CE585 File Offset: 0x001CC785
		public static IEnumerable<KeyValuePair<Optional<MatchingLabel>, ProgramSetBuilder<labelled_disjunction>>> ClusterOnInput(this ProgramSetBuilder<labelled_disjunction> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<MatchingLabel>, ProgramSetBuilder<labelled_disjunction>>(Cluster.CastValue<MatchingLabel>(kvp.Key), ProgramSetBuilder<labelled_disjunction>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600891E RID: 35102 RVA: 0x001CE5B7 File Offset: 0x001CC7B7
		public static IEnumerable<KeyValuePair<Optional<ImmutableList<MatchingLabel>>, ProgramSetBuilder<labelled_multi_result>>> ClusterOnInput(this ProgramSetBuilder<labelled_multi_result> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ImmutableList<MatchingLabel>>, ProgramSetBuilder<labelled_multi_result>>(Cluster.CastValue<ImmutableList<MatchingLabel>>(kvp.Key), ProgramSetBuilder<labelled_multi_result>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600891F RID: 35103 RVA: 0x001CE5E9 File Offset: 0x001CC7E9
		public static IEnumerable<KeyValuePair<Optional<MatchingLabel>, ProgramSetBuilder<label>>> ClusterOnInput(this ProgramSetBuilder<label> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<MatchingLabel>, ProgramSetBuilder<label>>(Cluster.CastValue<MatchingLabel>(kvp.Key), ProgramSetBuilder<label>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008920 RID: 35104 RVA: 0x001CE61B File Offset: 0x001CC81B
		public static IEnumerable<KeyValuePair<Optional<ImmutableList<MatchingLabel>>, ProgramSetBuilder<nil_label>>> ClusterOnInput(this ProgramSetBuilder<nil_label> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ImmutableList<MatchingLabel>>, ProgramSetBuilder<nil_label>>(Cluster.CastValue<ImmutableList<MatchingLabel>>(kvp.Key), ProgramSetBuilder<nil_label>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008921 RID: 35105 RVA: 0x001CE64D File Offset: 0x001CC84D
		public static IEnumerable<KeyValuePair<Optional<SuffixRegion>, ProgramSetBuilder<_LetB0>>> ClusterOnInput(this ProgramSetBuilder<_LetB0> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SuffixRegion>, ProgramSetBuilder<_LetB0>>(Cluster.CastValue<SuffixRegion>(kvp.Key), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008922 RID: 35106 RVA: 0x001CE67F File Offset: 0x001CC87F
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<SuffixRegion>>, ProgramSetBuilder<_LetB1>>> ClusterOnInput(this ProgramSetBuilder<_LetB1> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<SuffixRegion>>, ProgramSetBuilder<_LetB1>>(Cluster.CastValue<IEnumerable<SuffixRegion>>(kvp.Key), ProgramSetBuilder<_LetB1>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008923 RID: 35107 RVA: 0x001CE6B1 File Offset: 0x001CC8B1
		public static IEnumerable<KeyValuePair<Optional<ImmutableList<bool>>, ProgramSetBuilder<_LetB2>>> ClusterOnInput(this ProgramSetBuilder<_LetB2> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ImmutableList<bool>>, ProgramSetBuilder<_LetB2>>(Cluster.CastValue<ImmutableList<bool>>(kvp.Key), ProgramSetBuilder<_LetB2>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008924 RID: 35108 RVA: 0x001CE6E3 File Offset: 0x001CC8E3
		public static IEnumerable<KeyValuePair<Optional<SuffixRegion>, ProgramSetBuilder<_LetB3>>> ClusterOnInput(this ProgramSetBuilder<_LetB3> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SuffixRegion>, ProgramSetBuilder<_LetB3>>(Cluster.CastValue<SuffixRegion>(kvp.Key), ProgramSetBuilder<_LetB3>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008925 RID: 35109 RVA: 0x001CE715 File Offset: 0x001CC915
		public static IEnumerable<KeyValuePair<Optional<ImmutableList<bool>>, ProgramSetBuilder<_LetB4>>> ClusterOnInput(this ProgramSetBuilder<_LetB4> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ImmutableList<bool>>, ProgramSetBuilder<_LetB4>>(Cluster.CastValue<ImmutableList<bool>>(kvp.Key), ProgramSetBuilder<_LetB4>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008926 RID: 35110 RVA: 0x001CE747 File Offset: 0x001CC947
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<result>>> ClusterOnInputTuple(this ProgramSetBuilder<result> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<result>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<result>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008927 RID: 35111 RVA: 0x001CE779 File Offset: 0x001CC979
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<disjunctive_match>>> ClusterOnInputTuple(this ProgramSetBuilder<disjunctive_match> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<disjunctive_match>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<disjunctive_match>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008928 RID: 35112 RVA: 0x001CE7AB File Offset: 0x001CC9AB
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<match>>> ClusterOnInputTuple(this ProgramSetBuilder<match> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<match>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<match>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008929 RID: 35113 RVA: 0x001CE7DD File Offset: 0x001CC9DD
		public static IEnumerable<KeyValuePair<Optional<IToken>[], ProgramSetBuilder<token>>> ClusterOnInputTuple(this ProgramSetBuilder<token> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IToken>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<IToken>>(Cluster.CastValue<IToken>));
				}
				return new KeyValuePair<Optional<IToken>[], ProgramSetBuilder<token>>(key.Select(func).ToArray<Optional<IToken>>(), ProgramSetBuilder<token>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600892A RID: 35114 RVA: 0x001CE80F File Offset: 0x001CCA0F
		public static IEnumerable<KeyValuePair<Optional<ImmutableList<bool>>[], ProgramSetBuilder<multi_result>>> ClusterOnInputTuple(this ProgramSetBuilder<multi_result> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ImmutableList<bool>>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<ImmutableList<bool>>>(Cluster.CastValue<ImmutableList<bool>>));
				}
				return new KeyValuePair<Optional<ImmutableList<bool>>[], ProgramSetBuilder<multi_result>>(key.Select(func).ToArray<Optional<ImmutableList<bool>>>(), ProgramSetBuilder<multi_result>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600892B RID: 35115 RVA: 0x001CE841 File Offset: 0x001CCA41
		public static IEnumerable<KeyValuePair<Optional<ImmutableList<bool>>[], ProgramSetBuilder<multi_result_matches>>> ClusterOnInputTuple(this ProgramSetBuilder<multi_result_matches> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ImmutableList<bool>>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<ImmutableList<bool>>>(Cluster.CastValue<ImmutableList<bool>>));
				}
				return new KeyValuePair<Optional<ImmutableList<bool>>[], ProgramSetBuilder<multi_result_matches>>(key.Select(func).ToArray<Optional<ImmutableList<bool>>>(), ProgramSetBuilder<multi_result_matches>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600892C RID: 35116 RVA: 0x001CE873 File Offset: 0x001CCA73
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<SuffixRegion>>[], ProgramSetBuilder<inputSRegions>>> ClusterOnInputTuple(this ProgramSetBuilder<inputSRegions> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<SuffixRegion>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<SuffixRegion>>>(Cluster.CastValue<IEnumerable<SuffixRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<SuffixRegion>>[], ProgramSetBuilder<inputSRegions>>(key.Select(func).ToArray<Optional<IEnumerable<SuffixRegion>>>(), ProgramSetBuilder<inputSRegions>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600892D RID: 35117 RVA: 0x001CE8A5 File Offset: 0x001CCAA5
		public static IEnumerable<KeyValuePair<Optional<MatchingLabel>[], ProgramSetBuilder<labelled_disjunction>>> ClusterOnInputTuple(this ProgramSetBuilder<labelled_disjunction> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<MatchingLabel>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<MatchingLabel>>(Cluster.CastValue<MatchingLabel>));
				}
				return new KeyValuePair<Optional<MatchingLabel>[], ProgramSetBuilder<labelled_disjunction>>(key.Select(func).ToArray<Optional<MatchingLabel>>(), ProgramSetBuilder<labelled_disjunction>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600892E RID: 35118 RVA: 0x001CE8D7 File Offset: 0x001CCAD7
		public static IEnumerable<KeyValuePair<Optional<ImmutableList<MatchingLabel>>[], ProgramSetBuilder<labelled_multi_result>>> ClusterOnInputTuple(this ProgramSetBuilder<labelled_multi_result> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ImmutableList<MatchingLabel>>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<ImmutableList<MatchingLabel>>>(Cluster.CastValue<ImmutableList<MatchingLabel>>));
				}
				return new KeyValuePair<Optional<ImmutableList<MatchingLabel>>[], ProgramSetBuilder<labelled_multi_result>>(key.Select(func).ToArray<Optional<ImmutableList<MatchingLabel>>>(), ProgramSetBuilder<labelled_multi_result>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600892F RID: 35119 RVA: 0x001CE909 File Offset: 0x001CCB09
		public static IEnumerable<KeyValuePair<Optional<MatchingLabel>[], ProgramSetBuilder<label>>> ClusterOnInputTuple(this ProgramSetBuilder<label> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<MatchingLabel>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<MatchingLabel>>(Cluster.CastValue<MatchingLabel>));
				}
				return new KeyValuePair<Optional<MatchingLabel>[], ProgramSetBuilder<label>>(key.Select(func).ToArray<Optional<MatchingLabel>>(), ProgramSetBuilder<label>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008930 RID: 35120 RVA: 0x001CE93B File Offset: 0x001CCB3B
		public static IEnumerable<KeyValuePair<Optional<ImmutableList<MatchingLabel>>[], ProgramSetBuilder<nil_label>>> ClusterOnInputTuple(this ProgramSetBuilder<nil_label> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ImmutableList<MatchingLabel>>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<ImmutableList<MatchingLabel>>>(Cluster.CastValue<ImmutableList<MatchingLabel>>));
				}
				return new KeyValuePair<Optional<ImmutableList<MatchingLabel>>[], ProgramSetBuilder<nil_label>>(key.Select(func).ToArray<Optional<ImmutableList<MatchingLabel>>>(), ProgramSetBuilder<nil_label>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008931 RID: 35121 RVA: 0x001CE96D File Offset: 0x001CCB6D
		public static IEnumerable<KeyValuePair<Optional<SuffixRegion>[], ProgramSetBuilder<_LetB0>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB0> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SuffixRegion>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<SuffixRegion>>(Cluster.CastValue<SuffixRegion>));
				}
				return new KeyValuePair<Optional<SuffixRegion>[], ProgramSetBuilder<_LetB0>>(key.Select(func).ToArray<Optional<SuffixRegion>>(), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008932 RID: 35122 RVA: 0x001CE99F File Offset: 0x001CCB9F
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<SuffixRegion>>[], ProgramSetBuilder<_LetB1>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB1> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<SuffixRegion>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IEnumerable<SuffixRegion>>>(Cluster.CastValue<IEnumerable<SuffixRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<SuffixRegion>>[], ProgramSetBuilder<_LetB1>>(key.Select(func).ToArray<Optional<IEnumerable<SuffixRegion>>>(), ProgramSetBuilder<_LetB1>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008933 RID: 35123 RVA: 0x001CE9D1 File Offset: 0x001CCBD1
		public static IEnumerable<KeyValuePair<Optional<ImmutableList<bool>>[], ProgramSetBuilder<_LetB2>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB2> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ImmutableList<bool>>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<ImmutableList<bool>>>(Cluster.CastValue<ImmutableList<bool>>));
				}
				return new KeyValuePair<Optional<ImmutableList<bool>>[], ProgramSetBuilder<_LetB2>>(key.Select(func).ToArray<Optional<ImmutableList<bool>>>(), ProgramSetBuilder<_LetB2>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008934 RID: 35124 RVA: 0x001CEA03 File Offset: 0x001CCC03
		public static IEnumerable<KeyValuePair<Optional<SuffixRegion>[], ProgramSetBuilder<_LetB3>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB3> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SuffixRegion>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<SuffixRegion>>(Cluster.CastValue<SuffixRegion>));
				}
				return new KeyValuePair<Optional<SuffixRegion>[], ProgramSetBuilder<_LetB3>>(key.Select(func).ToArray<Optional<SuffixRegion>>(), ProgramSetBuilder<_LetB3>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008935 RID: 35125 RVA: 0x001CEA35 File Offset: 0x001CCC35
		public static IEnumerable<KeyValuePair<Optional<ImmutableList<bool>>[], ProgramSetBuilder<_LetB4>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB4> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ImmutableList<bool>>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<ImmutableList<bool>>>(Cluster.CastValue<ImmutableList<bool>>));
				}
				return new KeyValuePair<Optional<ImmutableList<bool>>[], ProgramSetBuilder<_LetB4>>(key.Select(func).ToArray<Optional<ImmutableList<bool>>>(), ProgramSetBuilder<_LetB4>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x020011DC RID: 4572
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400386A RID: 14442
			public static Func<object, Optional<bool>> <0>__CastValue;

			// Token: 0x0400386B RID: 14443
			public static Func<object, Optional<IToken>> <1>__CastValue;

			// Token: 0x0400386C RID: 14444
			public static Func<object, Optional<ImmutableList<bool>>> <2>__CastValue;

			// Token: 0x0400386D RID: 14445
			public static Func<object, Optional<IEnumerable<SuffixRegion>>> <3>__CastValue;

			// Token: 0x0400386E RID: 14446
			public static Func<object, Optional<MatchingLabel>> <4>__CastValue;

			// Token: 0x0400386F RID: 14447
			public static Func<object, Optional<ImmutableList<MatchingLabel>>> <5>__CastValue;

			// Token: 0x04003870 RID: 14448
			public static Func<object, Optional<SuffixRegion>> <6>__CastValue;
		}
	}
}
