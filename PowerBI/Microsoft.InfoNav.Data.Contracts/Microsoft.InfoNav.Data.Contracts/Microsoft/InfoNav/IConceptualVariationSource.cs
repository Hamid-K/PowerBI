using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000043 RID: 67
	public interface IConceptualVariationSource : IEquatable<IConceptualVariationSource>
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000121 RID: 289
		string Name { get; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000122 RID: 290
		bool IsDefault { get; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000123 RID: 291
		IConceptualNavigationProperty NavigationProperty { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000124 RID: 292
		IConceptualHierarchy DefaultHierarchy { get; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000125 RID: 293
		IConceptualProperty DefaultProperty { get; }
	}
}
