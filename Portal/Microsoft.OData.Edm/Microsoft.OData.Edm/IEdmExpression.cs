using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200002D RID: 45
	public interface IEdmExpression : IEdmElement
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000BD RID: 189
		EdmExpressionKind ExpressionKind { get; }
	}
}
