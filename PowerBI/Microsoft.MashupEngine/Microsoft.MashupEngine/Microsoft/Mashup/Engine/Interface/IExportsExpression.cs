using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000C9 RID: 201
	public interface IExportsExpression : IExpression, ISyntaxNode
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600031A RID: 794
		Identifier Name { get; }
	}
}
