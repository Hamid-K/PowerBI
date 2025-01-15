using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004CA RID: 1226
	[Obsolete("ConceptualModel and StoreModel are now available as properties directly on DbModel.")]
	public interface IEdmModelAdapter
	{
		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06003CC3 RID: 15555
		[Obsolete("ConceptualModel is now available as a property directly on DbModel.")]
		EdmModel ConceptualModel { get; }

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06003CC4 RID: 15556
		[Obsolete("StoreModel is now available as a property directly on DbModel.")]
		EdmModel StoreModel { get; }
	}
}
