using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000079 RID: 121
	public interface IEdmSpatialTypeReference : IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000260 RID: 608
		int? SpatialReferenceIdentifier { get; }
	}
}
