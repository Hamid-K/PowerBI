using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000082 RID: 130
	public interface IEdmDecimalTypeReference : IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600038D RID: 909
		int? Precision { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600038E RID: 910
		int? Scale { get; }
	}
}
