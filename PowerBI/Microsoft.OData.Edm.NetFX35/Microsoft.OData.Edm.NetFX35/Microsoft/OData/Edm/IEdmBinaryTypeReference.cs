using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000123 RID: 291
	public interface IEdmBinaryTypeReference : IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060005C7 RID: 1479
		bool IsUnbounded { get; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060005C8 RID: 1480
		int? MaxLength { get; }
	}
}
