using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200008B RID: 139
	public interface IEdmStringTypeReference : IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000398 RID: 920
		bool IsUnbounded { get; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000399 RID: 921
		int? MaxLength { get; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600039A RID: 922
		bool? IsUnicode { get; }
	}
}
