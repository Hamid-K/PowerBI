using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000CD RID: 205
	public interface IElementAccessExpression : IExpression, ISyntaxNode
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000322 RID: 802
		IExpression Collection { get; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000323 RID: 803
		IExpression Key { get; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000324 RID: 804
		bool IsOptional { get; }
	}
}
