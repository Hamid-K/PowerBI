using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings
{
	// Token: 0x020000A9 RID: 169
	public sealed class ResolvedDataReductionWindowExpansionInstanceValue
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x0000B6BD File Offset: 0x000098BD
		public ResolvedDataReductionWindowExpansionInstanceValue(IReadOnlyList<ResolvedQueryExpression> values, WindowKind windowStartKind)
		{
			this.Values = values;
			this.WindowStartKind = windowStartKind;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000B6D3 File Offset: 0x000098D3
		public IReadOnlyList<ResolvedQueryExpression> Values { get; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000B6DB File Offset: 0x000098DB
		public WindowKind WindowStartKind { get; }
	}
}
