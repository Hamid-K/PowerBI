using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000CC RID: 204
	public interface IIfExpression : IExpression, ISyntaxNode
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600031F RID: 799
		IExpression Condition { get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000320 RID: 800
		IExpression TrueCase { get; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000321 RID: 801
		IExpression FalseCase { get; }
	}
}
