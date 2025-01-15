using System;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x0200008A RID: 138
	public interface IEdmIfExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000241 RID: 577
		IEdmExpression TestExpression { get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000242 RID: 578
		IEdmExpression TrueExpression { get; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000243 RID: 579
		IEdmExpression FalseExpression { get; }
	}
}
