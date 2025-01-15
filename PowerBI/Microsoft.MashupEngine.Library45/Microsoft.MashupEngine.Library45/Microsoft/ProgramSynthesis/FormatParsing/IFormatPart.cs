using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.FormatParsing
{
	// Token: 0x0200078F RID: 1935
	public interface IFormatPart<out TPartialParse, TFullParse, in TSubstring, TFormatPart> where TPartialParse : IPartialParse<TFullParse, TFormatPart, TSubstring, TPartialParse>, IEquatable<TPartialParse> where TSubstring : ISubstring<TSubstring>, IEquatable<TSubstring> where TFormatPart : IFormatPart<TPartialParse, TFullParse, TSubstring, TFormatPart>
	{
		// Token: 0x06002990 RID: 10640
		Optional<int> GetNextPossibleMatchPosition(TSubstring unparsedSuffix);

		// Token: 0x06002991 RID: 10641
		IEnumerable<int> GetAllPossibleMatchPositions(TSubstring unparsedSuffix);

		// Token: 0x06002992 RID: 10642
		IEnumerable<TPartialParse> Parse(TSubstring unparsedSuffix);
	}
}
