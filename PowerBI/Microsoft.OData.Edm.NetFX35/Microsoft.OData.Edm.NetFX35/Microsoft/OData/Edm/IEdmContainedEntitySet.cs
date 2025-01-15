using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000ED RID: 237
	public interface IEdmContainedEntitySet : IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060004C2 RID: 1218
		IEdmNavigationSource ParentNavigationSource { get; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060004C3 RID: 1219
		IEdmNavigationProperty NavigationProperty { get; }
	}
}
