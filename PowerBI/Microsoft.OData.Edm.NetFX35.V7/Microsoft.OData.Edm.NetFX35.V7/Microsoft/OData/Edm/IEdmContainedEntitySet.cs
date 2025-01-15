using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200008B RID: 139
	public interface IEdmContainedEntitySet : IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600045E RID: 1118
		IEdmNavigationSource ParentNavigationSource { get; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600045F RID: 1119
		IEdmNavigationProperty NavigationProperty { get; }
	}
}
