using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000BC RID: 188
	public interface IEdmSpatialTypeReference : IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004B4 RID: 1204
		int? SpatialReferenceIdentifier { get; }
	}
}
