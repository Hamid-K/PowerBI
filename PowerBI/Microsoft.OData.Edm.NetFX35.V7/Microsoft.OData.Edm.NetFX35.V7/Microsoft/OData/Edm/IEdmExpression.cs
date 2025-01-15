using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200009D RID: 157
	public interface IEdmExpression : IEdmElement
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000474 RID: 1140
		EdmExpressionKind ExpressionKind { get; }
	}
}
