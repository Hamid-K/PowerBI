using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000C1 RID: 193
	public interface IEdmTemporalTypeReference : IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004BE RID: 1214
		int? Precision { get; }
	}
}
