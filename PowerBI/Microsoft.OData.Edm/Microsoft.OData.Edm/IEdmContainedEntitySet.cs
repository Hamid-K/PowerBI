using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200001D RID: 29
	public interface IEdmContainedEntitySet : IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000A1 RID: 161
		IEdmNavigationSource ParentNavigationSource { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000A2 RID: 162
		IEdmNavigationProperty NavigationProperty { get; }
	}
}
