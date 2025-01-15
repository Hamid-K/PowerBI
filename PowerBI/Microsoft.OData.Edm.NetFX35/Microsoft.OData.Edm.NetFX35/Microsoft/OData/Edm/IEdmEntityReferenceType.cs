using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000133 RID: 307
	public interface IEdmEntityReferenceType : IEdmType, IEdmElement
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060005F5 RID: 1525
		IEdmEntityType EntityType { get; }
	}
}
