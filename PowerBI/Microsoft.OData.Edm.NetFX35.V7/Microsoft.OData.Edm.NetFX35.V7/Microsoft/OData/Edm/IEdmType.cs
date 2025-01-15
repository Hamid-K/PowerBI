using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000C3 RID: 195
	public interface IEdmType : IEdmElement
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004BF RID: 1215
		EdmTypeKind TypeKind { get; }
	}
}
