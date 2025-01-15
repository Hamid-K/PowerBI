using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav
{
	// Token: 0x02000036 RID: 54
	public interface IConceptualDisplayFolder : IConceptualDisplayItem
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000C5 RID: 197
		IReadOnlyList<IConceptualDisplayItem> DisplayItems { get; }
	}
}
