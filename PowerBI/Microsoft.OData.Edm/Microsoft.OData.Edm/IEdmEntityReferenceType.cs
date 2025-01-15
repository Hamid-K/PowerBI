using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000087 RID: 135
	public interface IEdmEntityReferenceType : IEdmType, IEdmElement
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000397 RID: 919
		IEdmEntityType EntityType { get; }
	}
}
