using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002AC RID: 684
	internal static class QueryExpressionExtensions
	{
		// Token: 0x0600157E RID: 5502 RVA: 0x00026D70 File Offset: 0x00024F70
		internal static bool CanReturnStrings(this QueryExpressionContainer expr)
		{
			return expr.Property != null || expr.HierarchyLevel != null || expr.NativeFormat != null || expr.Measure != null || (expr.Aggregation != null && expr.Aggregation.CanReturnStrings());
		}
	}
}
