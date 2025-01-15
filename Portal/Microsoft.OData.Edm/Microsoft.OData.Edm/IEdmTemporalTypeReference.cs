using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200008D RID: 141
	public interface IEdmTemporalTypeReference : IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600039C RID: 924
		int? Precision { get; }
	}
}
