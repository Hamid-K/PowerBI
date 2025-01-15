using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x0200007C RID: 124
	public interface IUniqueKeyAnnotation
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060002D8 RID: 728
		IReadOnlyList<IReadOnlyList<IConceptualColumn>> UniqueKeys { get; }
	}
}
