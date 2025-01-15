using System;

namespace Microsoft.InfoNav
{
	// Token: 0x0200003F RID: 63
	public interface IConceptualPodParameter
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000103 RID: 259
		string Name { get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000104 RID: 260
		IConceptualNavigationProperty NavigationProperty { get; }
	}
}
