using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav
{
	// Token: 0x0200003E RID: 62
	public interface IConceptualPod : IConceptualEntity, IConceptualDisplayItem, IEquatable<IConceptualEntity>
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000101 RID: 257
		bool CortanaEnabled { get; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000102 RID: 258
		IReadOnlyList<IConceptualPodParameter> Parameters { get; }
	}
}
