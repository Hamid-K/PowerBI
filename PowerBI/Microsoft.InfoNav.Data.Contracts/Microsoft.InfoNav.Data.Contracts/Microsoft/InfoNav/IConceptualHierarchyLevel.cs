using System;

namespace Microsoft.InfoNav
{
	// Token: 0x0200003A RID: 58
	public interface IConceptualHierarchyLevel : IConceptualDisplayItem, IEquatable<IConceptualHierarchyLevel>
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060000E5 RID: 229
		string EdmName { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060000E6 RID: 230
		IConceptualProperty Source { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000E7 RID: 231
		IConceptualHierarchy Hierarchy { get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060000E8 RID: 232
		string StableName { get; }
	}
}
