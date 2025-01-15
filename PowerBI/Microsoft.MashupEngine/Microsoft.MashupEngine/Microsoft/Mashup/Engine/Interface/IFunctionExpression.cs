using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000C8 RID: 200
	public interface IFunctionExpression : IExpression, ISyntaxNode, IDeclarator
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000318 RID: 792
		IFunctionTypeExpression FunctionType { get; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000319 RID: 793
		IExpression Expression { get; }
	}
}
