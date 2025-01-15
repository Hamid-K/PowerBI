using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000077 RID: 119
	public interface IEdmCollectionType : IEdmType, IEdmElement
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600025F RID: 607
		IEdmTypeReference ElementType { get; }
	}
}
