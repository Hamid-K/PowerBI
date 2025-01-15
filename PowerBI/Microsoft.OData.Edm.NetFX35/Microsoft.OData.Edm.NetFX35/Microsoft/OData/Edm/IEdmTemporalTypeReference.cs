using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000149 RID: 329
	public interface IEdmTemporalTypeReference : IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000653 RID: 1619
		int? Precision { get; }
	}
}
