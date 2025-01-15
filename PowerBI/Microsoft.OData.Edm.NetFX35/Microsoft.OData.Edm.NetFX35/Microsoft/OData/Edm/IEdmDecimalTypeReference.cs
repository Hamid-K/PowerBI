using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200012F RID: 303
	public interface IEdmDecimalTypeReference : IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060005E4 RID: 1508
		int? Precision { get; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060005E5 RID: 1509
		int? Scale { get; }
	}
}
