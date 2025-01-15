using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.FormatParsing
{
	// Token: 0x02000768 RID: 1896
	public abstract class FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> where TFormatPart : IFormatPart<TPartialParse, TFullParse, TSubstring, TFormatPart> where TPartialParse : IPartialParse<TFullParse, TFormatPart, TSubstring, TPartialParse>, IEquatable<TPartialParse> where TSubstring : ISubstring<TSubstring>, IEquatable<TSubstring>
	{
		// Token: 0x06002891 RID: 10385
		internal abstract FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> Clone(int? matchStoreIndex, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint> directionalConstraints, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint> groupConstraints, int minRepetitions, int maxRepetitions);

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002892 RID: 10386 RVA: 0x00072F65 File Offset: 0x00071165
		// (set) Token: 0x06002893 RID: 10387 RVA: 0x00072F6D File Offset: 0x0007116D
		internal int? MatchStoreIndex { get; private set; }

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x00072F76 File Offset: 0x00071176
		// (set) Token: 0x06002895 RID: 10389 RVA: 0x00072F7E File Offset: 0x0007117E
		internal Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> FilterPredicate { get; private set; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x00072F87 File Offset: 0x00071187
		public Func<TSubstring, TPartialParse> EmptyPartialParseFactory { get; }

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002897 RID: 10391 RVA: 0x00072F8F File Offset: 0x0007118F
		internal IReadOnlyList<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint> DirectionalConstraints { get; }

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x00072F97 File Offset: 0x00071197
		internal IReadOnlyList<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint> GroupConstraints { get; }

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002899 RID: 10393 RVA: 0x00072F9F File Offset: 0x0007119F
		public int MinRepetitions { get; }

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600289A RID: 10394 RVA: 0x00072FA7 File Offset: 0x000711A7
		public int MaxRepetitions { get; }

		// Token: 0x0600289B RID: 10395 RVA: 0x00072FB0 File Offset: 0x000711B0
		protected FormatParser(Func<TSubstring, TPartialParse> emptyPartialParseFactory, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate = null, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint> directionalConstraints = null, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint> groupConstraints = null, int minRepetitions = 1, int maxRepetitions = 1, int? matchStoreIndex = null)
		{
			this.EmptyPartialParseFactory = emptyPartialParseFactory;
			this.FilterPredicate = filterPredicate;
			this.DirectionalConstraints = ((directionalConstraints != null) ? directionalConstraints.ToList<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint>() : null) ?? new List<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint>();
			this.GroupConstraints = ((groupConstraints != null) ? groupConstraints.ToList<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint>() : null) ?? new List<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint>();
			this.MinRepetitions = minRepetitions;
			this.MaxRepetitions = maxRepetitions;
			this.MatchStoreIndex = matchStoreIndex;
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x00073024 File Offset: 0x00071224
		public IEnumerable<TPartialParse> Parse(TSubstring region, ParseConfig config = null)
		{
			config = config ?? ParseConfig.Default;
			FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> fullFormatMatchState = FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>.CreateFullState(region);
			return from s in this.ComputeDeltas(fullFormatMatchState, config)
				select s.CumulativeParse into p
				where p.HasValue
				select p.Value;
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000730B8 File Offset: 0x000712B8
		private HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> _ComputeDeltas(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> state, ParseConfig config)
		{
			HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> hashSet = new HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> { state.Sequence(this.EmptyPartialParseFactory) };
			HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> hashSet2 = new HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>();
			Func<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>, IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>> <>9__1;
			for (int i = 1; i <= this.MinRepetitions; i++)
			{
				IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> enumerable = hashSet;
				Func<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>, IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> d) => this.ComputeDeltasImpl(d, config));
				}
				hashSet = enumerable.SelectMany(func).ConvertToHashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>();
				if (!hashSet.Any<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>())
				{
					return hashSet2;
				}
			}
			if (config.RepetitionMode == RepetitionMode.Lazy || config.RepetitionMode == RepetitionMode.Exhaustive || this.MinRepetitions == this.MaxRepetitions)
			{
				hashSet = hashSet.Select((DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> delta) => delta.Squash()).ConvertToHashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>();
				hashSet2.AddRange(hashSet);
			}
			if (config.RepetitionMode == RepetitionMode.Lazy)
			{
				return hashSet2;
			}
			for (int j = this.MinRepetitions + 1; j <= this.MaxRepetitions; j++)
			{
				HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> hashSet3 = new HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>();
				foreach (DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> deltaFormatMatchState in hashSet)
				{
					List<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> list = this.ComputeDeltasImpl(deltaFormatMatchState, config).ToList<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>();
					if (!list.Any<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>())
					{
						hashSet2.Add(deltaFormatMatchState);
					}
					hashSet3.AddRange(list);
				}
				if (!hashSet3.Any<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>())
				{
					break;
				}
				if (config.RepetitionMode == RepetitionMode.Exhaustive || j == this.MaxRepetitions)
				{
					hashSet2.AddRange(hashSet3);
				}
				hashSet = hashSet3;
			}
			return hashSet2;
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x0007326C File Offset: 0x0007146C
		private static List<Optional<TPartialParse>> GetParsedValuesForIds(IEnumerable<int> ids, FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> state)
		{
			return ids.Select(delegate(int dep)
			{
				TPartialParse tpartialParse;
				if (!state.ParsedValues.TryGetValue(dep, out tpartialParse))
				{
					return Optional<TPartialParse>.Nothing;
				}
				return tpartialParse.Some<TPartialParse>();
			}).ToList<Optional<TPartialParse>>();
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x000732A0 File Offset: 0x000714A0
		internal IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> FilterDeltas(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> stateBeforeDeltas, IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> deltas)
		{
			if (this.FilterPredicate != null)
			{
				deltas = deltas.Where((DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> d) => this.FilterPredicate(d));
			}
			IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint> directionalConstraints = this.DirectionalConstraints;
			using (IEnumerator<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint> enumerator = (directionalConstraints ?? Enumerable.Empty<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint>()).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint constraint2 = enumerator.Current;
					List<Optional<TPartialParse>> valuesForDependencies = FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GetParsedValuesForIds(constraint2.IndicesOfDependencies, stateBeforeDeltas);
					if (valuesForDependencies.Any((Optional<TPartialParse> v) => v.HasValue))
					{
						deltas = deltas.Where((DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> delta) => constraint2.DependencyChecker(delta.PartialParse, valuesForDependencies));
					}
				}
			}
			IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint> groupConstraints = this.GroupConstraints;
			using (IEnumerator<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint> enumerator2 = (groupConstraints ?? Enumerable.Empty<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint>()).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint constraint = enumerator2.Current;
					int myIndex = constraint.IndicesOfConstrainedSets.IndexOf(this.MatchStoreIndex.Value).Value;
					List<Optional<TPartialParse>> valuesForConstrainedSets = FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GetParsedValuesForIds(constraint.IndicesOfConstrainedSets, stateBeforeDeltas);
					if (valuesForConstrainedSets.Any((Optional<TPartialParse> v) => v.HasValue))
					{
						deltas = deltas.Where((DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> delta) => constraint.DependencyChecker(myIndex, delta.PartialParse, valuesForConstrainedSets));
					}
				}
			}
			if (this.MatchStoreIndex == null)
			{
				return deltas;
			}
			return deltas.Select((DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> delta) => delta.WithResolution(this.MatchStoreIndex.Value, delta.PartialParse));
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x00073474 File Offset: 0x00071674
		internal DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> FilterDelta(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> stateBeforeDelta, DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> delta)
		{
			if (this.FilterPredicate != null && !this.FilterPredicate(delta))
			{
				return null;
			}
			return this.FilterDeltas(stateBeforeDelta, delta.Yield<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>()).FirstOrDefault<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>();
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000734A0 File Offset: 0x000716A0
		public virtual IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> ComputeDeltas(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> state, ParseConfig config)
		{
			FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> fullFormatMatchState = state.ToFullState();
			IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> enumerable = this._ComputeDeltas(fullFormatMatchState, config);
			return this.FilterDeltas(state, enumerable);
		}

		// Token: 0x060028A2 RID: 10402
		protected abstract HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> ComputeDeltasImpl(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> state, ParseConfig config);

		// Token: 0x060028A3 RID: 10403 RVA: 0x000734C5 File Offset: 0x000716C5
		public IEnumerable<int> GetPossibleStartPositions(TSubstring region)
		{
			return this.GetPossibleStartPositionsImpl(region);
		}

		// Token: 0x060028A4 RID: 10404
		protected abstract IEnumerable<int> GetPossibleStartPositionsImpl(TSubstring region);

		// Token: 0x060028A5 RID: 10405
		public abstract override string ToString();

		// Token: 0x02000769 RID: 1897
		public struct DirectionalConstraint
		{
			// Token: 0x17000715 RID: 1813
			// (get) Token: 0x060028A8 RID: 10408 RVA: 0x00073503 File Offset: 0x00071703
			public readonly IReadOnlyList<int> IndicesOfDependencies { get; }

			// Token: 0x17000716 RID: 1814
			// (get) Token: 0x060028A9 RID: 10409 RVA: 0x0007350B File Offset: 0x0007170B
			public readonly Func<TPartialParse, IReadOnlyList<Optional<TPartialParse>>, bool> DependencyChecker { get; }

			// Token: 0x060028AA RID: 10410 RVA: 0x00073513 File Offset: 0x00071713
			public DirectionalConstraint(IEnumerable<int> indicesOfDependencies, Func<TPartialParse, IReadOnlyList<Optional<TPartialParse>>, bool> dependencyChecker)
			{
				this.IndicesOfDependencies = indicesOfDependencies.ToList<int>();
				this.DependencyChecker = dependencyChecker;
			}
		}

		// Token: 0x0200076A RID: 1898
		public struct GroupConstraint
		{
			// Token: 0x17000717 RID: 1815
			// (get) Token: 0x060028AB RID: 10411 RVA: 0x00073528 File Offset: 0x00071728
			public readonly IReadOnlyList<int> IndicesOfConstrainedSets { get; }

			// Token: 0x17000718 RID: 1816
			// (get) Token: 0x060028AC RID: 10412 RVA: 0x00073530 File Offset: 0x00071730
			public readonly Func<int, TPartialParse, IReadOnlyList<Optional<TPartialParse>>, bool> DependencyChecker { get; }

			// Token: 0x060028AD RID: 10413 RVA: 0x00073538 File Offset: 0x00071738
			public GroupConstraint(IEnumerable<int> indicesOfConstrainedSets, Func<int, TPartialParse, IReadOnlyList<Optional<TPartialParse>>, bool> dependencyChecker)
			{
				this.IndicesOfConstrainedSets = indicesOfConstrainedSets.ToList<int>();
				this.DependencyChecker = dependencyChecker;
			}
		}

		// Token: 0x0200076B RID: 1899
		internal class AtomicFormatParser : FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>
		{
			// Token: 0x17000719 RID: 1817
			// (get) Token: 0x060028AE RID: 10414 RVA: 0x0007354D File Offset: 0x0007174D
			public TFormatPart FormatPart { get; }

			// Token: 0x060028AF RID: 10415 RVA: 0x00073555 File Offset: 0x00071755
			public AtomicFormatParser(Func<TSubstring, TPartialParse> emptyPartialParseFactory, TFormatPart formatPart, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate = null, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint> directionalConstraints = null, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint> groupConstraints = null, int minRepetitions = 1, int maxRepetitions = 1, int? matchStoreIndex = null)
				: base(emptyPartialParseFactory, filterPredicate, directionalConstraints, groupConstraints, minRepetitions, maxRepetitions, matchStoreIndex)
			{
				this.FormatPart = formatPart;
			}

			// Token: 0x060028B0 RID: 10416 RVA: 0x00073570 File Offset: 0x00071770
			internal override FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> Clone(int? matchStoreIndex, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint> directionalConstraints, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint> groupConstraints, int minRepetitions, int maxRepetitions)
			{
				return new FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.AtomicFormatParser(base.EmptyPartialParseFactory, this.FormatPart, filterPredicate, directionalConstraints, groupConstraints, minRepetitions, maxRepetitions, null);
			}

			// Token: 0x060028B1 RID: 10417 RVA: 0x000735A0 File Offset: 0x000717A0
			protected override HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> ComputeDeltasImpl(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> state, ParseConfig config)
			{
				FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.AtomicFormatParser.<>c__DisplayClass5_0 CS$<>8__locals1 = new FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.AtomicFormatParser.<>c__DisplayClass5_0();
				CS$<>8__locals1.state = state;
				FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.AtomicFormatParser.<>c__DisplayClass5_0 CS$<>8__locals2 = CS$<>8__locals1;
				TSubstring unparsedSuffix = CS$<>8__locals1.state.UnparsedSuffix;
				CS$<>8__locals2.endOfSuffix = unparsedSuffix.End;
				TFormatPart formatPart = this.FormatPart;
				return (from delta in formatPart.Parse(CS$<>8__locals1.state.UnparsedSuffix).Select(delegate(TPartialParse p)
					{
						FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> state2 = CS$<>8__locals1.state;
						TPartialParse tpartialParse = p;
						ImmutableDictionary<int, TPartialParse> parsedValues = CS$<>8__locals1.state.ParsedValues;
						TSubstring unparsedSuffix2 = CS$<>8__locals1.state.UnparsedSuffix;
						TSubstring parsedRegion = p.ParsedRegion;
						return state2.Sequence(tpartialParse, parsedValues, unparsedSuffix2.AbsoluteSlice(parsedRegion.End, CS$<>8__locals1.endOfSuffix));
					})
					where delta != null
					select delta).ConvertToHashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>();
			}

			// Token: 0x060028B2 RID: 10418 RVA: 0x00073634 File Offset: 0x00071834
			protected override IEnumerable<int> GetPossibleStartPositionsImpl(TSubstring region)
			{
				TFormatPart formatPart = this.FormatPart;
				return formatPart.GetAllPossibleMatchPositions(region);
			}

			// Token: 0x060028B3 RID: 10419 RVA: 0x00073658 File Offset: 0x00071858
			public override string ToString()
			{
				if (base.MinRepetitions == 1 && base.MaxRepetitions == 1)
				{
					TFormatPart formatPart = this.FormatPart;
					return formatPart.ToString();
				}
				if (base.MinRepetitions == 0 && base.MaxRepetitions == 1)
				{
					return FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[] { this.FormatPart }));
				}
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}{{{1}, {2}}}", new object[] { this.FormatPart, base.MinRepetitions, base.MaxRepetitions }));
			}
		}

		// Token: 0x0200076E RID: 1902
		internal class UnionFormatParser : FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>
		{
			// Token: 0x1700071A RID: 1818
			// (get) Token: 0x060028B9 RID: 10425 RVA: 0x0007376A File Offset: 0x0007196A
			public IReadOnlyList<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>> UnionedSets { get; }

			// Token: 0x060028BA RID: 10426 RVA: 0x00073772 File Offset: 0x00071972
			public UnionFormatParser(Func<TSubstring, TPartialParse> emptyPartialParseFactory, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>> setsToUnion, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate = null, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint> directionalConstraints = null, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint> groupConstraints = null, int minRepetitions = 1, int maxRepetitions = 1, int? matchStoreIndex = null)
				: base(emptyPartialParseFactory, filterPredicate, directionalConstraints, groupConstraints, minRepetitions, maxRepetitions, matchStoreIndex)
			{
				this.UnionedSets = setsToUnion.ToList<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>>();
			}

			// Token: 0x060028BB RID: 10427 RVA: 0x00073792 File Offset: 0x00071992
			internal override FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> Clone(int? matchStoreIndex, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint> directionalConstraints, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint> groupConstraints, int minRepetitions, int maxRepetitions)
			{
				return new FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.UnionFormatParser(base.EmptyPartialParseFactory, this.UnionedSets.Select((FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> s) => s.Clone(s.MatchStoreIndex, s.FilterPredicate, s.DirectionalConstraints, s.GroupConstraints, s.MinRepetitions, s.MaxRepetitions)), filterPredicate, directionalConstraints, groupConstraints, minRepetitions, maxRepetitions, matchStoreIndex);
			}

			// Token: 0x060028BC RID: 10428 RVA: 0x000737D4 File Offset: 0x000719D4
			protected override HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> ComputeDeltasImpl(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> state, ParseConfig config)
			{
				List<IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>> list = this.UnionedSets.Select((FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> s) => s.ComputeDeltas(state, config)).ToList<IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>>();
				switch (config.UnionMatchingMode)
				{
				case UnionMatchingMode.First:
				{
					IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> enumerable = list.FirstOrDefault((IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> d) => d.Any<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>());
					return ((enumerable != null) ? enumerable.ConvertToHashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>() : null) ?? new HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>();
				}
				case UnionMatchingMode.LongestMatch:
				{
					HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> hashSet = list.SelectMany((IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> d) => d).ConvertToHashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>();
					if (!hashSet.Any<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>())
					{
						return hashSet;
					}
					HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> hashSet2 = new HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>();
					hashSet2.Add(hashSet.ArgMax(delegate(DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> d)
					{
						TPartialParse partialParse = d.PartialParse;
						TSubstring parsedRegion = partialParse.ParsedRegion;
						return parsedRegion.Length;
					}));
					return hashSet2;
				}
				case UnionMatchingMode.FirstLongestMatch:
				{
					IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> enumerable2 = list.FirstOrDefault((IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> d) => d.Any<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>());
					HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> hashSet3 = ((enumerable2 != null) ? enumerable2.ConvertToHashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>() : null) ?? new HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>();
					if (!hashSet3.Any<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>())
					{
						return hashSet3;
					}
					HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> hashSet4 = new HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>();
					hashSet4.Add(hashSet3.ArgMax(delegate(DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> d)
					{
						TPartialParse partialParse2 = d.PartialParse;
						TSubstring parsedRegion2 = partialParse2.ParsedRegion;
						return parsedRegion2.Length;
					}));
					return hashSet4;
				}
				case UnionMatchingMode.Exhaustive:
					return list.SelectMany((IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> d) => d).ConvertToHashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>();
				default:
					throw new NotImplementedException();
				}
			}

			// Token: 0x060028BD RID: 10429 RVA: 0x00073988 File Offset: 0x00071B88
			protected override IEnumerable<int> GetPossibleStartPositionsImpl(TSubstring region)
			{
				return from v in this.UnionedSets.SelectMany((FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> set) => set.GetPossibleStartPositions(region)).Distinct<int>()
					orderby v
					select v;
			}

			// Token: 0x060028BE RID: 10430 RVA: 0x000739E4 File Offset: 0x00071BE4
			public override string ToString()
			{
				string text = "Union({0})";
				object[] array = new object[1];
				array[0] = string.Join(", ", this.UnionedSets.Select((FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> s) => s.ToString()));
				string text2 = FormattableString.Invariant(FormattableStringFactory.Create(text, array));
				if (base.MinRepetitions == 1 && base.MaxRepetitions == 1)
				{
					return text2;
				}
				if (base.MinRepetitions == 0 && base.MaxRepetitions == 1)
				{
					return FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[] { text2 }));
				}
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}{{{1}, {2}}}", new object[] { text2, base.MinRepetitions, base.MaxRepetitions }));
			}
		}

		// Token: 0x02000772 RID: 1906
		internal class SequenceFormatParser : FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>
		{
			// Token: 0x1700071B RID: 1819
			// (get) Token: 0x060028CE RID: 10446 RVA: 0x00073B75 File Offset: 0x00071D75
			public IReadOnlyList<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>> SequencedSets { get; }

			// Token: 0x060028CF RID: 10447 RVA: 0x00073B80 File Offset: 0x00071D80
			public SequenceFormatParser(Func<TSubstring, TPartialParse> emptyPartialParseFactory, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>> setsToSequence, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate = null, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint> directionalConstraints = null, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint> groupConstraints = null, int minRepetitions = 1, int maxRepetitions = 1, int? matchStoreIndex = null)
				: base(emptyPartialParseFactory, filterPredicate, directionalConstraints, groupConstraints, minRepetitions, maxRepetitions, matchStoreIndex)
			{
				List<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>> list = setsToSequence.ToList<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>>();
				List<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>> list2 = new List<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>>();
				int i = 0;
				while (i < list.Count)
				{
					SpacerFormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> spacerFormatParser = list[i] as SpacerFormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>;
					if (spacerFormatParser != null)
					{
						list2.Add(new SpacerAndSuccessor<TFormatPart, TPartialParse, TFullParse, TSubstring>(spacerFormatParser, list[i + 1]));
						i += 2;
					}
					else
					{
						list2.Add(list[i]);
						i++;
					}
				}
				this.SequencedSets = list2;
			}

			// Token: 0x060028D0 RID: 10448 RVA: 0x00073BFC File Offset: 0x00071DFC
			internal override FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> Clone(int? matchStoreIndex, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint> directionalConstraints, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint> groupConstraints, int minRepetitions, int maxRepetitions)
			{
				return new FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.SequenceFormatParser(base.EmptyPartialParseFactory, this.SequencedSets.Select((FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> s) => s.Clone(s.MatchStoreIndex, s.FilterPredicate, s.DirectionalConstraints, s.GroupConstraints, s.MinRepetitions, s.MaxRepetitions)), filterPredicate, directionalConstraints, groupConstraints, minRepetitions, maxRepetitions, matchStoreIndex);
			}

			// Token: 0x060028D1 RID: 10449 RVA: 0x00073C3C File Offset: 0x00071E3C
			protected override HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> ComputeDeltasImpl(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> state, ParseConfig config)
			{
				HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> hashSet = null;
				using (IEnumerator<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>> enumerator = this.SequencedSets.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> currentSet = enumerator.Current;
						hashSet = ((hashSet == null) ? currentSet.ComputeDeltas(state, config).ConvertToHashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>() : hashSet.SelectMany((DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> s) => currentSet.ComputeDeltas(s, config)).ConvertToHashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>());
						if (hashSet.IsEmpty<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>())
						{
							break;
						}
					}
				}
				return hashSet;
			}

			// Token: 0x060028D2 RID: 10450 RVA: 0x00073CE8 File Offset: 0x00071EE8
			protected override IEnumerable<int> GetPossibleStartPositionsImpl(TSubstring region)
			{
				IEnumerable<int> enumerable = Enumerable.Empty<int>();
				foreach (FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> formatParser in this.SequencedSets)
				{
					enumerable = enumerable.Concat(formatParser.GetPossibleStartPositions(region));
					if (formatParser.MinRepetitions > 0)
					{
						break;
					}
				}
				return enumerable;
			}

			// Token: 0x060028D3 RID: 10451 RVA: 0x00073D50 File Offset: 0x00071F50
			public override string ToString()
			{
				string text = "Sequence({0})";
				object[] array = new object[1];
				array[0] = string.Join(", ", this.SequencedSets.Select((FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> s) => s.ToString()));
				string text2 = FormattableString.Invariant(FormattableStringFactory.Create(text, array));
				if (base.MinRepetitions == 1 && base.MaxRepetitions == 1)
				{
					return text2;
				}
				if (base.MinRepetitions == 0 && base.MaxRepetitions == 1)
				{
					return FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[] { text2 }));
				}
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}{{{1}, {2}}}", new object[] { text2, base.MinRepetitions, base.MaxRepetitions }));
			}
		}
	}
}
