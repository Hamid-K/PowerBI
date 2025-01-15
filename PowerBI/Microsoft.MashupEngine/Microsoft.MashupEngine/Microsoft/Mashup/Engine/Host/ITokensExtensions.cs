using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Host
{
	// Token: 0x0200021B RID: 539
	public static class ITokensExtensions
	{
		// Token: 0x06000AE0 RID: 2784 RVA: 0x00018F74 File Offset: 0x00017174
		public static TextPosition GetPosition(this ITokens tokens, TokenReference token)
		{
			return tokens.GetRange(token).Start;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00018F90 File Offset: 0x00017190
		public static TokenReference GetTokenAtOffset(this ITokens tokens, int offset)
		{
			TokenReference token = tokens.GetToken(0);
			while (tokens.GetType(token) != TokenType.Eof && offset >= tokens.GetLength(token))
			{
				offset -= tokens.GetLength(token++);
			}
			return token;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0000A6A5 File Offset: 0x000088A5
		public static int GetIndex(this ITokens tokens, TokenReference token)
		{
			return (int)token;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0000A6A5 File Offset: 0x000088A5
		public static TokenReference GetToken(this ITokens tokens, int index)
		{
			return (TokenReference)index;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00018FCB File Offset: 0x000171CB
		public static StringSegment GetText(this ITokens tokens, TokenReference token)
		{
			return tokens.GetText(token, token);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00018FD8 File Offset: 0x000171D8
		public static StringSegment GetText(this ITokens tokens, TokenReference start, TokenReference end)
		{
			int offset = tokens.GetOffset(start);
			int num = tokens.GetOffset(end) + tokens.GetLength(end);
			return tokens.GetSegmentedText().GetSubstringSegment(offset, num - offset);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001900C File Offset: 0x0001720C
		public static TextRange GetTextRange(this ITokens tokens, TokenRange range)
		{
			return new TextRange(tokens.GetPosition(range.Start), tokens.GetPosition(range.End));
		}
	}
}
