using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000087 RID: 135
	public interface IEdmCollectionType : IEdmType, IEdmElement
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600045D RID: 1117
		IEdmTypeReference ElementType { get; }
	}
}
