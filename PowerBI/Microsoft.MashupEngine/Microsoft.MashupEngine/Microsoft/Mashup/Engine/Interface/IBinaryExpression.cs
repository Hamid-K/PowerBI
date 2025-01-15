using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000C0 RID: 192
	public interface IBinaryExpression : IExpression, ISyntaxNode
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000309 RID: 777
		BinaryOperator2 Operator { get; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600030A RID: 778
		IExpression Left { get; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600030B RID: 779
		IExpression Right { get; }
	}
}
