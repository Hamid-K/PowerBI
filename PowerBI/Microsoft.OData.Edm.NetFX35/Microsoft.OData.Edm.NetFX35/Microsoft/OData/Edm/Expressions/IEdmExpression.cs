using System;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x02000008 RID: 8
	public interface IEdmExpression : IEdmElement
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001F RID: 31
		EdmExpressionKind ExpressionKind { get; }
	}
}
