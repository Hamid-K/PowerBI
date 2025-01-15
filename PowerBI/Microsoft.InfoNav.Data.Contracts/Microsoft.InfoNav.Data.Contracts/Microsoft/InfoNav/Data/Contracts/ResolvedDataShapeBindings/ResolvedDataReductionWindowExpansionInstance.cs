using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings
{
	// Token: 0x020000A8 RID: 168
	public sealed class ResolvedDataReductionWindowExpansionInstance
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x0000B688 File Offset: 0x00009888
		public ResolvedDataReductionWindowExpansionInstance(IReadOnlyList<ResolvedQueryExpression> values, IReadOnlyList<ResolvedDataReductionWindowExpansionInstance> children, IReadOnlyList<ResolvedDataReductionWindowExpansionInstanceValue> windowExpansionInstanceWindowValue)
		{
			this.Values = values;
			this.Children = children;
			this.WindowExpansionInstanceWindowValue = windowExpansionInstanceWindowValue;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000B6A5 File Offset: 0x000098A5
		public IReadOnlyList<ResolvedQueryExpression> Values { get; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000B6AD File Offset: 0x000098AD
		public IReadOnlyList<ResolvedDataReductionWindowExpansionInstance> Children { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000B6B5 File Offset: 0x000098B5
		public IReadOnlyList<ResolvedDataReductionWindowExpansionInstanceValue> WindowExpansionInstanceWindowValue { get; }
	}
}
