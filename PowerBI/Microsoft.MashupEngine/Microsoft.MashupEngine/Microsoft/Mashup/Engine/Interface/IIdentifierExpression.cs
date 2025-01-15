using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000CA RID: 202
	public interface IIdentifierExpression : IExpression, ISyntaxNode
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600031B RID: 795
		Identifier Name { get; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600031C RID: 796
		bool IsInclusive { get; }
	}
}
