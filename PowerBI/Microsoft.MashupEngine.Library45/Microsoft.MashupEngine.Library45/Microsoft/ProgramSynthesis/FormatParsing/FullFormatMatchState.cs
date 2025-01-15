using System;
using System.Collections.Immutable;

namespace Microsoft.ProgramSynthesis.FormatParsing
{
	// Token: 0x02000763 RID: 1891
	public sealed class FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> : FormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> where TPartialParse : IPartialParse<TFullParse, TFormatPart, TSubstring, TPartialParse>, IEquatable<TPartialParse> where TFormatPart : IFormatPart<TPartialParse, TFullParse, TSubstring, TFormatPart> where TSubstring : ISubstring<TSubstring>, IEquatable<TSubstring>
	{
		// Token: 0x0600287F RID: 10367 RVA: 0x00072D0C File Offset: 0x00070F0C
		private FullFormatMatchState(TSubstring unparsedSuffix)
			: base(unparsedSuffix)
		{
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x00072D15 File Offset: 0x00070F15
		private FullFormatMatchState(TSubstring unparsedSuffix, TPartialParse cumulativeParse, ImmutableDictionary<int, TPartialParse> parsedValues)
			: base(unparsedSuffix, cumulativeParse, parsedValues)
		{
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x00072D20 File Offset: 0x00070F20
		public static FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> Create(TSubstring unparsedSuffix)
		{
			return new FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>(unparsedSuffix);
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x00072D28 File Offset: 0x00070F28
		public static FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> Create(TSubstring unparsedSuffix, TPartialParse cumulativeParse, ImmutableDictionary<int, TPartialParse> parsedValues)
		{
			return new FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>(unparsedSuffix, cumulativeParse, parsedValues);
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FullFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring> ToFullState()
		{
			return this;
		}
	}
}
