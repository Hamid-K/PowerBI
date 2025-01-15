using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.FormatParsing
{
	// Token: 0x02000790 RID: 1936
	public interface IPartialParse<out TFullParse, TFormatPart, out TSubstring, TPartialParse> where TFormatPart : IFormatPart<TPartialParse, TFullParse, TSubstring, TFormatPart> where TSubstring : ISubstring<TSubstring>, IEquatable<TSubstring> where TPartialParse : IPartialParse<TFullParse, TFormatPart, TSubstring, TPartialParse>, IEquatable<TPartialParse>
	{
		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002993 RID: 10643
		TSubstring ParsedRegion { get; }

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002994 RID: 10644
		TFullParse CompleteParse { get; }

		// Token: 0x06002995 RID: 10645
		Optional<TPartialParse> Sequence(TPartialParse other);

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002996 RID: 10646
		bool ContainsOnlyEmptyParse { get; }
	}
}
