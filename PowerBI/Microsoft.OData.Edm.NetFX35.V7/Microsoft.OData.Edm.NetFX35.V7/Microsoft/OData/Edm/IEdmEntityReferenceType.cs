using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000092 RID: 146
	public interface IEdmEntityReferenceType : IEdmType, IEdmElement
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600046A RID: 1130
		IEdmEntityType EntityType { get; }
	}
}
