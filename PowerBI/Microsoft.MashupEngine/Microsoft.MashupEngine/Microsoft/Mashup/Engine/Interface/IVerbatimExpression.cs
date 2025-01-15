using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000BF RID: 191
	public interface IVerbatimExpression : IExpression, ISyntaxNode
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000308 RID: 776
		IConstantExpression2 Text { get; }
	}
}
