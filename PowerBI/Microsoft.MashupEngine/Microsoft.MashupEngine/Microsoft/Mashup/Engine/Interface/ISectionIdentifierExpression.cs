using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000CB RID: 203
	public interface ISectionIdentifierExpression : IExpression, ISyntaxNode
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600031D RID: 797
		Identifier Section { get; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600031E RID: 798
		Identifier Name { get; }
	}
}
