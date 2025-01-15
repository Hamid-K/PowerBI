using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200008C RID: 140
	public interface IEdmDecimalTypeReference : IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000460 RID: 1120
		int? Precision { get; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000461 RID: 1121
		int? Scale { get; }
	}
}
