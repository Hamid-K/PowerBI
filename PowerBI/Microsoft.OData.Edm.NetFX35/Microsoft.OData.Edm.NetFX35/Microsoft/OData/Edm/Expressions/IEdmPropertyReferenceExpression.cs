using System;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x020000B4 RID: 180
	public interface IEdmPropertyReferenceExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600030B RID: 779
		IEdmExpression Base { get; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600030C RID: 780
		IEdmProperty ReferencedProperty { get; }
	}
}
