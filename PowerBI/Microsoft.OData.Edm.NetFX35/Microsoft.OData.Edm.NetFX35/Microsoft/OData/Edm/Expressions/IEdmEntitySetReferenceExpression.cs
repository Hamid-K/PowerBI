using System;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x02000065 RID: 101
	public interface IEdmEntitySetReferenceExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600018D RID: 397
		IEdmEntitySet ReferencedEntitySet { get; }
	}
}
