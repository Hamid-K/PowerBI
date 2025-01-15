using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000163 RID: 355
	internal sealed class IntermediateCorrelations : Dictionary<Identifier, IntermediateColumnReference>
	{
		// Token: 0x06000CED RID: 3309 RVA: 0x00035890 File Offset: 0x00033A90
		internal IntersectionCorrelations Bind(BatchDataSetPlan plan, OutputTableMapping outputTableMapping)
		{
			IntersectionCorrelations intersectionCorrelations = new IntersectionCorrelations();
			foreach (KeyValuePair<Identifier, IntermediateColumnReference> keyValuePair in this)
			{
				if (keyValuePair.Value != null)
				{
					intersectionCorrelations.Add(keyValuePair.Key, keyValuePair.Value.Bind(plan, outputTableMapping));
				}
			}
			return intersectionCorrelations;
		}
	}
}
