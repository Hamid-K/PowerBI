using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000DB RID: 219
	public interface IDocument : ISyntaxNode
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000342 RID: 834
		IDocumentHost Host { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000343 RID: 835
		ITokens Tokens { get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000344 RID: 836
		DocumentKind Kind { get; }
	}
}
