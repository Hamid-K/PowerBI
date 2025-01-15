using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000108 RID: 264
	public interface IEdmIfExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000736 RID: 1846
		IEdmExpression TestExpression { get; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000737 RID: 1847
		IEdmExpression TrueExpression { get; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000738 RID: 1848
		IEdmExpression FalseExpression { get; }
	}
}
