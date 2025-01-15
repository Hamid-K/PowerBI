using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000B2 RID: 178
	public interface ITypeExpression : IExpression, ISyntaxNode
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002F8 RID: 760
		IExpression Expression { get; }
	}
}
