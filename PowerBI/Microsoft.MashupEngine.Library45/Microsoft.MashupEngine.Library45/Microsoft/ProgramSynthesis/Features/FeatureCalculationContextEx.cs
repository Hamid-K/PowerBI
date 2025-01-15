using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007CD RID: 1997
	internal static class FeatureCalculationContextEx
	{
		// Token: 0x06002A8B RID: 10891 RVA: 0x0007771C File Offset: 0x0007591C
		public static IEqualityComparer<FeatureCalculationContext> GetFccEqualityComparer(this IFeature feature)
		{
			if (!feature.UseComputedInputsForFccEquality)
			{
				return EqualityComparer<FeatureCalculationContext>.Default;
			}
			return FeatureCalculationContext.EqualityComparerOnComputedInputs;
		}
	}
}
