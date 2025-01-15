using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018AA RID: 6314
	internal static class ITokensExtensions
	{
		// Token: 0x0600A0C2 RID: 41154 RVA: 0x0000A6A5 File Offset: 0x000088A5
		public static TokenReference GetToken(this ITokens tokens, int index)
		{
			return (TokenReference)index;
		}

		// Token: 0x0600A0C3 RID: 41155 RVA: 0x00214B8D File Offset: 0x00212D8D
		public static StringSegment GetText(this ITokens tokens, TokenReference token)
		{
			return tokens.GetText(token, token);
		}

		// Token: 0x0600A0C4 RID: 41156 RVA: 0x00214B98 File Offset: 0x00212D98
		public static StringSegment GetText(this ITokens tokens, TokenReference start, TokenReference end)
		{
			int offset = tokens.GetOffset(start);
			int num = tokens.GetOffset(end) + tokens.GetLength(end);
			return tokens.GetSegmentedText().GetSubstringSegment(offset, num - offset);
		}
	}
}
