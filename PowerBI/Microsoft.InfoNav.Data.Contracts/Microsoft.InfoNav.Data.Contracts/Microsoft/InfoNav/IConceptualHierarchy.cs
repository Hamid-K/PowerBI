using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav
{
	// Token: 0x02000039 RID: 57
	public interface IConceptualHierarchy : IConceptualDisplayItem, IEquatable<IConceptualHierarchy>
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000DF RID: 223
		string EdmName { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000E0 RID: 224
		bool IsHidden { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000E1 RID: 225
		IReadOnlyList<IConceptualHierarchyLevel> Levels { get; }

		// Token: 0x060000E2 RID: 226
		bool TryGetLevel(string name, out IConceptualHierarchyLevel conceptualHierarchyLevel);

		// Token: 0x060000E3 RID: 227
		bool TryGetLevelByEdmName(string edmName, out IConceptualHierarchyLevel conceptualHierarchyLevel);

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000E4 RID: 228
		string StableName { get; }
	}
}
