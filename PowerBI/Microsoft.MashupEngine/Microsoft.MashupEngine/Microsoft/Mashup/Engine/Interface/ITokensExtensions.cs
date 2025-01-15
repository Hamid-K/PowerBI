using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000E6 RID: 230
	public static class ITokensExtensions
	{
		// Token: 0x0600035B RID: 859 RVA: 0x000049AC File Offset: 0x00002BAC
		public static SegmentedString GetSegmentedText(this ITokens tokens)
		{
			ISegmentedTokens segmentedTokens = tokens as ISegmentedTokens;
			if (segmentedTokens != null)
			{
				return segmentedTokens.SegmentedText;
			}
			return SegmentedString.New(tokens.Text);
		}
	}
}
