using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.FormatParsing
{
	// Token: 0x02000764 RID: 1892
	public sealed class DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> : FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> where TPartialParse : IPartialParse<TFullParse, TFormatPart, TSubstring, TPartialParse>, IEquatable<TPartialParse> where TFormatPart : IFormatPart<TPartialParse, TFullParse, TSubstring, TFormatPart> where TSubstring : ISubstring<TSubstring>, IEquatable<TSubstring>
	{
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002884 RID: 10372 RVA: 0x00072D32 File Offset: 0x00070F32
		public FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> Origin { get; }

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002885 RID: 10373 RVA: 0x00072D3A File Offset: 0x00070F3A
		public TPartialParse PartialParse { get; }

		// Token: 0x06002886 RID: 10374 RVA: 0x00072D42 File Offset: 0x00070F42
		private DeltaFormatMatchState(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> origin, TPartialParse partialParse, TPartialParse cumulativeParse, ImmutableDictionary<int, TPartialParse> parsedValues, TSubstring unparsedSuffix)
			: base(unparsedSuffix, cumulativeParse, parsedValues)
		{
			this.Origin = origin;
			this.PartialParse = partialParse;
			this._fullStateLazy = new Lazy<FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>(new Func<FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>(this.ComputeFullState));
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x00072D74 File Offset: 0x00070F74
		private FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> ComputeFullState()
		{
			return FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>.CreateFullState(base.UnparsedSuffix, base.CumulativeParse.Value, base.ParsedValues);
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x00072DA0 File Offset: 0x00070FA0
		public override FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> ToFullState()
		{
			return this._fullStateLazy.Value;
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x00072DB0 File Offset: 0x00070FB0
		public static DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> Create(FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> origin, TPartialParse partialParse, ImmutableDictionary<int, TPartialParse> parsedValues, TSubstring unparsedSuffix)
		{
			Optional<TPartialParse> optional;
			if (!origin.CumulativeParse.HasValue)
			{
				optional = partialParse.Some<TPartialParse>();
			}
			else
			{
				TPartialParse value = origin.CumulativeParse.Value;
				optional = value.Sequence(partialParse);
			}
			Optional<TPartialParse> optional2 = optional;
			if (!optional2.HasValue)
			{
				return null;
			}
			return new DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>(origin, partialParse, optional2.Value, parsedValues, unparsedSuffix);
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x00072E10 File Offset: 0x00071010
		public DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> Squash()
		{
			if (this.Origin is FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>)
			{
				return this;
			}
			FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> formatMatchState = this;
			List<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> list = new List<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>();
			for (;;)
			{
				DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> deltaFormatMatchState = formatMatchState as DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>;
				if (deltaFormatMatchState == null)
				{
					break;
				}
				list.Add(deltaFormatMatchState);
				formatMatchState = deltaFormatMatchState.Origin;
			}
			list.Reverse();
			FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> formatMatchState2 = formatMatchState;
			TPartialParse tpartialParse = list.First<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>>().PartialParse;
			foreach (DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> deltaFormatMatchState2 in list.Skip(1))
			{
				ref TPartialParse ptr = ref tpartialParse;
				if (default(TPartialParse) == null)
				{
					TPartialParse tpartialParse2 = tpartialParse;
					ptr = ref tpartialParse2;
				}
				Optional<TPartialParse> optional = ptr.Sequence(deltaFormatMatchState2.PartialParse);
				if (!optional.HasValue)
				{
					return null;
				}
				tpartialParse = optional.Value;
			}
			return DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>.Create(formatMatchState2, tpartialParse, base.ParsedValues, base.UnparsedSuffix);
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x00072F04 File Offset: 0x00071104
		public DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> WithResolution(int globalId, TPartialParse value)
		{
			return DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>.Create(this.Origin, this.PartialParse, base.ParsedValues.Add(globalId, value), base.UnparsedSuffix);
		}

		// Token: 0x040013B2 RID: 5042
		private readonly Lazy<FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> _fullStateLazy;
	}
}
