using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200003D RID: 61
	internal static class DataShapeBindingAggregateKindExtensions
	{
		// Token: 0x06000221 RID: 545 RVA: 0x00009EC3 File Offset: 0x000080C3
		internal static bool HasMax(this DataShapeBindingAggregateKind value)
		{
			return DataShapeBindingAggregateKindExtensions.HasFlag(value, DataShapeBindingAggregateKind.Max);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00009ECC File Offset: 0x000080CC
		internal static bool HasMin(this DataShapeBindingAggregateKind value)
		{
			return DataShapeBindingAggregateKindExtensions.HasFlag(value, DataShapeBindingAggregateKind.Min);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009ED5 File Offset: 0x000080D5
		private static bool HasFlag(DataShapeBindingAggregateKind value, DataShapeBindingAggregateKind flag)
		{
			return (value & flag) == flag;
		}
	}
}
