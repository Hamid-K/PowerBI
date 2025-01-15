using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000D2 RID: 210
	public interface IParenthesesExpression : IExpression, ISyntaxNode
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600032C RID: 812
		IExpression Expression { get; }
	}
}
