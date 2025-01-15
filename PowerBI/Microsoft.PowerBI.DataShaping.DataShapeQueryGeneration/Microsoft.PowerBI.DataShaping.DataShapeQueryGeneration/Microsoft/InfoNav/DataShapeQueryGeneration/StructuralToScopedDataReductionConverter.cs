using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200003C RID: 60
	internal static class StructuralToScopedDataReductionConverter
	{
		// Token: 0x06000220 RID: 544 RVA: 0x00009DC4 File Offset: 0x00007FC4
		public static void ConvertToScoped(IntermediateDataReduction dataReduction, int primaryGroupCount, int secondaryGroupCount)
		{
			List<IntermediateScopedReductionAlgorithm> scoped = dataReduction.Scoped;
			if (dataReduction.Primary != null)
			{
				IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm = new IntermediateScopedReductionAlgorithm
				{
					Algorithm = dataReduction.Primary,
					Scope = new IntermediateReductionScope
					{
						Primary = new List<int>(Enumerable.Range(0, primaryGroupCount))
					}
				};
				Util.AddToLazyList<IntermediateScopedReductionAlgorithm>(ref scoped, intermediateScopedReductionAlgorithm);
				dataReduction.Primary = null;
			}
			if (dataReduction.Secondary != null)
			{
				IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm2 = new IntermediateScopedReductionAlgorithm
				{
					Algorithm = dataReduction.Secondary,
					Scope = new IntermediateReductionScope
					{
						Secondary = new List<int>(Enumerable.Range(0, secondaryGroupCount))
					}
				};
				Util.AddToLazyList<IntermediateScopedReductionAlgorithm>(ref scoped, intermediateScopedReductionAlgorithm2);
				dataReduction.Secondary = null;
			}
			if (dataReduction.Intersection != null)
			{
				IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm3 = new IntermediateScopedReductionAlgorithm
				{
					Algorithm = dataReduction.Intersection,
					Scope = new IntermediateReductionScope
					{
						Primary = new List<int>(Enumerable.Range(0, primaryGroupCount)),
						Secondary = new List<int>(Enumerable.Range(0, secondaryGroupCount))
					}
				};
				Util.AddToLazyList<IntermediateScopedReductionAlgorithm>(ref scoped, intermediateScopedReductionAlgorithm3);
				dataReduction.Intersection = null;
			}
			dataReduction.Scoped = scoped;
		}
	}
}
