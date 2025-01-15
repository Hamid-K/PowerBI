using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200004B RID: 75
	public interface IEdmType : IEdmElement
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000116 RID: 278
		EdmTypeKind TypeKind { get; }
	}
}
