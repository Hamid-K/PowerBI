using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000BD RID: 189
	public interface IEdmStringTypeReference : IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004B5 RID: 1205
		bool IsUnbounded { get; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004B6 RID: 1206
		int? MaxLength { get; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004B7 RID: 1207
		bool? IsUnicode { get; }
	}
}
