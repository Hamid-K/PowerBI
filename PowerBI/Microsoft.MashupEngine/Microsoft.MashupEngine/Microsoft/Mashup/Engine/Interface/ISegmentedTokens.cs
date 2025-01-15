using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000DA RID: 218
	public interface ISegmentedTokens : ITokens, IEquatable<ITokens>
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000341 RID: 833
		SegmentedString SegmentedText { get; }
	}
}
