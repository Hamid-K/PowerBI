using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000095 RID: 149
	internal static class FilterRestrictions
	{
		// Token: 0x04000326 RID: 806
		internal static readonly HashSet<DsqFilterType> Where = new HashSet<DsqFilterType>
		{
			DsqFilterType.AnyValue,
			DsqFilterType.AnyValueDefaultValueOverridesAncestors,
			DsqFilterType.Apply,
			DsqFilterType.Context,
			DsqFilterType.DataShape,
			DsqFilterType.DefaultValue,
			DsqFilterType.Exist,
			DsqFilterType.Scope
		};

		// Token: 0x04000327 RID: 807
		internal static readonly HashSet<DsqFilterType> Expressions = new HashSet<DsqFilterType> { DsqFilterType.DataShape };

		// Token: 0x04000328 RID: 808
		internal static readonly HashSet<DsqFilterType> InstanceFilters = new HashSet<DsqFilterType> { DsqFilterType.DataShape };
	}
}
