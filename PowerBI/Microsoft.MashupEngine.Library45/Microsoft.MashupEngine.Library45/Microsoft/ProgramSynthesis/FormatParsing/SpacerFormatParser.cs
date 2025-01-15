using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.FormatParsing
{
	// Token: 0x0200077B RID: 1915
	public abstract class SpacerFormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> : FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> where TFormatPart : IFormatPart<TPartialParse, TFullParse, TSubstring, TFormatPart> where TPartialParse : IPartialParse<TFullParse, TFormatPart, TSubstring, TPartialParse>, IEquatable<TPartialParse> where TSubstring : ISubstring<TSubstring>, IEquatable<TSubstring>
	{
		// Token: 0x060028EB RID: 10475 RVA: 0x00073EF8 File Offset: 0x000720F8
		protected override IEnumerable<int> GetPossibleStartPositionsImpl(TSubstring region)
		{
			return ((int)region.Start).Yield<int>();
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060028EC RID: 10476 RVA: 0x00073F0C File Offset: 0x0007210C
		public int MaxSpacerLength { get; }

		// Token: 0x060028ED RID: 10477 RVA: 0x00073F14 File Offset: 0x00072114
		protected SpacerFormatParser(Func<TSubstring, TPartialParse> emptyPartialParseFactory, int maxSpacerLength, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate = null, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint> directionalConstraints = null, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint> groupConstraints = null, int minRepetitions = 1, int maxRepetitions = 1, int? matchStoreIndex = null)
			: base(emptyPartialParseFactory, filterPredicate, directionalConstraints, groupConstraints, minRepetitions, maxRepetitions, matchStoreIndex)
		{
			this.MaxSpacerLength = maxSpacerLength;
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000170F6 File Offset: 0x000152F6
		protected override HashSet<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> ComputeDeltasImpl(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> state, ParseConfig config)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060028EF RID: 10479
		public abstract Optional<TPartialParse> CreatePartialParse(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> stateBeforeDelimiter, int delimiterLength);

		// Token: 0x060028F0 RID: 10480
		internal abstract override FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> Clone(int? matchStoreIndex, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint> directionalConstraints, IEnumerable<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint> groupConstraints, int minRepetitions, int maxRepetitions);

		// Token: 0x060028F1 RID: 10481 RVA: 0x00073F2F File Offset: 0x0007212F
		public override string ToString()
		{
			return "Spacer";
		}
	}
}
