using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000D7 RID: 215
	public interface IUnaryExpression : IExpression, ISyntaxNode
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000337 RID: 823
		UnaryOperator2 Operator { get; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000338 RID: 824
		IExpression Expression { get; }
	}
}
