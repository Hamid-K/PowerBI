using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000BC RID: 188
	public interface IEdmSpatialTypeReference : IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600032F RID: 815
		int? SpatialReferenceIdentifier { get; }
	}
}
