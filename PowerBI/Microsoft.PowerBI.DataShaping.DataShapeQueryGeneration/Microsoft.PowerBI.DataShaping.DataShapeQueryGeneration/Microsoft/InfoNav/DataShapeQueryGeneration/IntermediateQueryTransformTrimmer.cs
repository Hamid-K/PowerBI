using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000E0 RID: 224
	internal static class IntermediateQueryTransformTrimmer
	{
		// Token: 0x060007C2 RID: 1986 RVA: 0x0001D190 File Offset: 0x0001B390
		internal static bool TryTrim(IntermediateQueryTransformContext intermediateTransformContext, DataShapeGenerationOptions generationOptions, IReadOnlyList<ResolvedQuerySelect> selects, DsqExpressionGenerator expressionGenerator)
		{
			if (generationOptions == DataShapeGenerationOptions.Empty || intermediateTransformContext == IntermediateQueryTransformContext.Empty)
			{
				return true;
			}
			IReadOnlyList<int> selectIndicesToPreserve = generationOptions.SelectIndicesToPreserve;
			bool projectIdentityOnly = generationOptions.ProjectIdentityOnly;
			if (!selectIndicesToPreserve.IsNullOrEmpty<int>() || projectIdentityOnly)
			{
				for (int i = 0; i < selects.Count; i++)
				{
					bool flag = selectIndicesToPreserve != null && !selectIndicesToPreserve.Contains(i);
					IntermediateQueryTransformTableColumn intermediateQueryTransformTableColumn;
					if ((flag || projectIdentityOnly) && expressionGenerator.TryGetAsTransformColumn(selects[i].Expression, out intermediateQueryTransformTableColumn) && (flag || (projectIdentityOnly && intermediateQueryTransformTableColumn.OmitFromOutput)))
					{
						intermediateQueryTransformTableColumn.Table.RemoveColumn(intermediateQueryTransformTableColumn);
					}
				}
			}
			return true;
		}
	}
}
