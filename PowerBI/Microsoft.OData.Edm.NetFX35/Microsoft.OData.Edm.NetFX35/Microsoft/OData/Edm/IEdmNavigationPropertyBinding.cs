using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000F0 RID: 240
	public interface IEdmNavigationPropertyBinding
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060004C9 RID: 1225
		IEdmNavigationProperty NavigationProperty { get; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060004CA RID: 1226
		IEdmNavigationSource Target { get; }
	}
}
