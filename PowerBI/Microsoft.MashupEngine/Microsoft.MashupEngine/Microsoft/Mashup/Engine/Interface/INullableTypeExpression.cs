using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000B7 RID: 183
	public interface INullableTypeExpression : IExpression, ISyntaxNode
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000300 RID: 768
		IExpression ItemType { get; }
	}
}
