using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000146 RID: 326
	public interface IEdmStringTypeReference : IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000648 RID: 1608
		bool IsUnbounded { get; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000649 RID: 1609
		int? MaxLength { get; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600064A RID: 1610
		bool? IsUnicode { get; }
	}
}
