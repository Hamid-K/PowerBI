using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.FormatParsing
{
	// Token: 0x0200077C RID: 1916
	internal sealed class SpacerAndSuccessor<TFormatPart, TPartialParse, TFullParse, TSubstring> : FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> where TFormatPart : IFormatPart<TPartialParse, TFullParse, TSubstring, TFormatPart> where TPartialParse : IPartialParse<TFullParse, TFormatPart, TSubstring, TPartialParse>, IEquatable<TPartialParse> where TSubstring : ISubstring<TSubstring>, IEquatable<TSubstring>
	{
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x00073F36 File Offset: 0x00072136
		public SpacerFormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> Spacer { get; }

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060028F3 RID: 10483 RVA: 0x00073F3E File Offset: 0x0007213E
		public FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> Successor { get; }

		// Token: 0x060028F4 RID: 10484 RVA: 0x00073F48 File Offset: 0x00072148
		public SpacerAndSuccessor(SpacerFormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> spacer, FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> successor)
			: base(spacer.EmptyPartialParseFactory, null, null, null, 1, 1, null)
		{
			this.Spacer = spacer;
			this.Successor = successor;
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x00073F80 File Offset: 0x00072180
		internal override FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> Clone(int? matchStoreIndex, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint> directionalConstraints, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint> groupConstraints, int minRepetitions, int maxRepetitions)
		{
			return new SpacerAndSuccessor<TFormatPart, TPartialParse, TFullParse, TSubstring>((SpacerFormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>)this.Spacer.Clone(this.Spacer.MatchStoreIndex, this.Spacer.FilterPredicate, this.Spacer.DirectionalConstraints, this.Spacer.GroupConstraints, this.Spacer.MinRepetitions, this.Spacer.MaxRepetitions), this.Successor.Clone(this.Successor.MatchStoreIndex, this.Successor.FilterPredicate, this.Successor.DirectionalConstraints, this.Successor.GroupConstraints, this.Successor.MinRepetitions, this.Successor.MaxRepetitions));
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x00074031 File Offset: 0x00072231
		public override IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> ComputeDeltas(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> state, ParseConfig config)
		{
			return this.ComputeAllDeltas(state, config);
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x0007403B File Offset: 0x0007223B
		private IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> ComputeNonEmptyDeltas(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> state, ParseConfig config)
		{
			TSubstring suffix = state.UnparsedSuffix;
			uint endOfSuffix = suffix.End;
			int startOfSuffix = (int)suffix.Start;
			foreach (int num in this.Successor.GetPossibleStartPositions(suffix))
			{
				if (num - startOfSuffix > this.Spacer.MaxSpacerLength)
				{
					yield break;
				}
				Optional<TPartialParse> optional = this.Spacer.CreatePartialParse(state, num - (int)suffix.Start);
				if (optional.HasValue)
				{
					TPartialParse value = optional.Value;
					ImmutableDictionary<int, TPartialParse> parsedValues = state.ParsedValues;
					ref TSubstring ptr = ref suffix;
					if (default(TSubstring) == null)
					{
						TSubstring tsubstring = suffix;
						ptr = ref tsubstring;
					}
					TPartialParse value2 = optional.Value;
					TSubstring parsedRegion = value2.ParsedRegion;
					DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> deltaFormatMatchState = state.Sequence(value, parsedValues, ptr.AbsoluteSlice(parsedRegion.End, endOfSuffix));
					if (deltaFormatMatchState != null)
					{
						deltaFormatMatchState = this.Spacer.FilterDelta(state, deltaFormatMatchState);
						if (deltaFormatMatchState != null)
						{
							IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> enumerable = this.Successor.ComputeDeltas(deltaFormatMatchState, config).Where(delegate(DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> d)
							{
								TPartialParse partialParse = d.PartialParse;
								return !partialParse.ContainsOnlyEmptyParse;
							});
							foreach (DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> deltaFormatMatchState2 in enumerable)
							{
								yield return deltaFormatMatchState2;
							}
							IEnumerator<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> enumerator2 = null;
						}
					}
				}
			}
			IEnumerator<int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x0007405C File Offset: 0x0007225C
		private IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> ComputeAllDeltas(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> state, ParseConfig config)
		{
			IEnumerable<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> enumerable = this.ComputeNonEmptyDeltas(state, config);
			if (this.Successor.MinRepetitions != 0)
			{
				return enumerable;
			}
			TPartialParse tpartialParse = base.EmptyPartialParseFactory(state.UnparsedSuffix);
			TPartialParse tpartialParse2 = base.EmptyPartialParseFactory(state.UnparsedSuffix);
			DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> deltaFormatMatchState = state.Sequence(tpartialParse, state.ParsedValues, state.UnparsedSuffix);
			deltaFormatMatchState = this.Spacer.FilterDelta(state, deltaFormatMatchState);
			if (deltaFormatMatchState == null)
			{
				return enumerable;
			}
			deltaFormatMatchState = deltaFormatMatchState.Sequence(tpartialParse2, deltaFormatMatchState.ParsedValues, state.UnparsedSuffix);
			deltaFormatMatchState = base.FilterDelta(state, deltaFormatMatchState);
			if (deltaFormatMatchState == null)
			{
				return enumerable;
			}
			return enumerable.AppendItem(deltaFormatMatchState);
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000170F6 File Offset: 0x000152F6
		protected override HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> ComputeDeltasImpl(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> state, ParseConfig config)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x000740F5 File Offset: 0x000722F5
		protected override IEnumerable<int> GetPossibleStartPositionsImpl(TSubstring region)
		{
			return this.Spacer.GetPossibleStartPositions(region);
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x00074103 File Offset: 0x00072303
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}, {1}", new object[]
			{
				this.Spacer.ToString(),
				this.Successor.ToString()
			}));
		}
	}
}
