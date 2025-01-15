using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000085 RID: 133
	public interface IEdmBinaryTypeReference : IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600045A RID: 1114
		bool IsUnbounded { get; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600045B RID: 1115
		int? MaxLength { get; }
	}
}
