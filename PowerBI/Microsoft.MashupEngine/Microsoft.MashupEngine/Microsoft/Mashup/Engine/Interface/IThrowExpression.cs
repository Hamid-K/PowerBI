using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000D4 RID: 212
	public interface IThrowExpression : IExpression, ISyntaxNode
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600032F RID: 815
		IExpression Expression { get; }
	}
}
