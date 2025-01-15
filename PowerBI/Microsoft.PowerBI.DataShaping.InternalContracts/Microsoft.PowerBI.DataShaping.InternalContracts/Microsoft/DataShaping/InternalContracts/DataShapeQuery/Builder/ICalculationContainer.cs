using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x02000104 RID: 260
	internal interface ICalculationContainer<TResult>
	{
		// Token: 0x0600070A RID: 1802
		TResult WithCalculation(string identifier, Expression value, bool suppressJoinPredicate = false, bool? respectInstanceFilters = null, string nativeReferenceName = null, bool isContextOnly = false);
	}
}
