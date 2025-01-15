using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000D9 RID: 217
	public interface ITokens : IEquatable<ITokens>
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000339 RID: 825
		string Text { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600033A RID: 826
		int Count { get; }

		// Token: 0x0600033B RID: 827
		TokenType GetType(TokenReference token);

		// Token: 0x0600033C RID: 828
		int GetOffset(TokenReference token);

		// Token: 0x0600033D RID: 829
		int GetOffset(TextPosition position);

		// Token: 0x0600033E RID: 830
		int GetLength(TokenReference token);

		// Token: 0x0600033F RID: 831
		TextPosition GetPosition(int offset);

		// Token: 0x06000340 RID: 832
		TextRange GetRange(TokenReference token);
	}
}
