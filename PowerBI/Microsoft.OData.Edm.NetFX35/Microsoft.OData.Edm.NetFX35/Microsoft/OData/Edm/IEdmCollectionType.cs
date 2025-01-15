using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200009C RID: 156
	public interface IEdmCollectionType : IEdmType, IEdmElement
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060002AF RID: 687
		IEdmTypeReference ElementType { get; }
	}
}
