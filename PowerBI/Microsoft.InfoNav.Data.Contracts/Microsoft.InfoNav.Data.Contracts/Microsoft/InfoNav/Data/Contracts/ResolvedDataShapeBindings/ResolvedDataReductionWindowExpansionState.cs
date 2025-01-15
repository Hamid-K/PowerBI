using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings
{
	// Token: 0x020000AA RID: 170
	public sealed class ResolvedDataReductionWindowExpansionState
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x0000B6E3 File Offset: 0x000098E3
		public ResolvedDataReductionWindowExpansionState(IReadOnlyList<ResolvedQuerySource> from, IReadOnlyList<ResolvedDataShapeBindingAxisExpansionLevel> levels, ResolvedDataReductionWindowExpansionInstance windowInstances)
		{
			this.From = from;
			this.Levels = levels;
			this.WindowInstances = windowInstances;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000B700 File Offset: 0x00009900
		public IReadOnlyList<ResolvedQuerySource> From { get; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000B708 File Offset: 0x00009908
		public IReadOnlyList<ResolvedDataShapeBindingAxisExpansionLevel> Levels { get; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000B710 File Offset: 0x00009910
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x0000B718 File Offset: 0x00009918
		[DataMember(IsRequired = true, Order = 30)]
		public ResolvedDataReductionWindowExpansionInstance WindowInstances { get; set; }
	}
}
