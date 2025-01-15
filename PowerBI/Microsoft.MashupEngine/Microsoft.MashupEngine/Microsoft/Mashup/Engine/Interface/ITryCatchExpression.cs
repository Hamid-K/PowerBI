using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000D5 RID: 213
	public interface ITryCatchExpression : IExpression, ISyntaxNode
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000330 RID: 816
		IExpression Try { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000331 RID: 817
		TryCatchExceptionCase ExceptionCase { get; }
	}
}
