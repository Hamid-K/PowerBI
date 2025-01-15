using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree
{
	// Token: 0x02000254 RID: 596
	public interface ISubSequence<out TSequenceable, TSequence, TSubSequence> : IEnumerable<TSequenceable>, IEnumerable where TSequenceable : IEquatable<TSequenceable> where TSequence : class, IEnumerable<TSequenceable> where TSubSequence : class, ISubSequence<TSequenceable, TSequence, TSubSequence>
	{
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000CB8 RID: 3256
		TSequence FullSequence { get; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000CB9 RID: 3257
		uint Start { get; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000CBA RID: 3258
		uint End { get; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000CBB RID: 3259
		uint Length { get; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000CBC RID: 3260
		uint FullLength { get; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000CBD RID: 3261
		TSequence Value { get; }

		// Token: 0x17000302 RID: 770
		TSequenceable this[int position] { get; }

		// Token: 0x06000CBF RID: 3263
		TSubSequence AbsoluteSlice(uint start, uint end);

		// Token: 0x06000CC0 RID: 3264
		bool StartsWith(TSequence prefix);

		// Token: 0x06000CC1 RID: 3265
		bool StartsWith(TSubSequence prefix);

		// Token: 0x06000CC2 RID: 3266
		uint FindFirstMismatchingIndex(TSequence prefix);

		// Token: 0x06000CC3 RID: 3267
		uint FindFirstMismatchingIndex(TSubSequence prefix);

		// Token: 0x06000CC4 RID: 3268
		TSubSequence Concat(TSubSequence other);

		// Token: 0x06000CC5 RID: 3269
		TSubSequence Concat(TSequence other);
	}
}
