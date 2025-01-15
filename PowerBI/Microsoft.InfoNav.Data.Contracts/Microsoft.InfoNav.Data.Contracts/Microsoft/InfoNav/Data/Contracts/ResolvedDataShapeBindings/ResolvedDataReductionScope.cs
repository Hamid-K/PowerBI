using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings
{
	// Token: 0x020000A5 RID: 165
	public sealed class ResolvedDataReductionScope
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x0000B625 File Offset: 0x00009825
		public ResolvedDataReductionScope(IReadOnlyList<int> primary, IReadOnlyList<int> secondary)
		{
			this.Primary = primary;
			this.Secondary = secondary;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000B63B File Offset: 0x0000983B
		public IReadOnlyList<int> Primary { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000B643 File Offset: 0x00009843
		public IReadOnlyList<int> Secondary { get; }
	}
}
