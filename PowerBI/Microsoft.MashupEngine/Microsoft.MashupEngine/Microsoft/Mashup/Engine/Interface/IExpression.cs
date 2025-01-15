using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000B1 RID: 177
	public interface IExpression : ISyntaxNode
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002F7 RID: 759
		ExpressionKind Kind { get; }
	}
}
