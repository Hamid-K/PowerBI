using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000193 RID: 403
	internal sealed class IntermediateMemberMatchConditions : Dictionary<DataMember, IntermediateMatchCondition>
	{
		// Token: 0x06000DCE RID: 3534 RVA: 0x00038710 File Offset: 0x00036910
		internal BatchMemberMatchConditions Bind(BatchDataSetPlan plan, OutputTableMapping outputTableMapping)
		{
			BatchMemberMatchConditions batchMemberMatchConditions = new BatchMemberMatchConditions(base.Count);
			foreach (KeyValuePair<DataMember, IntermediateMatchCondition> keyValuePair in this)
			{
				batchMemberMatchConditions.Add(keyValuePair.Key, keyValuePair.Value.Bind(plan, outputTableMapping));
			}
			return batchMemberMatchConditions;
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00038780 File Offset: 0x00036980
		internal static IntermediateMemberMatchConditions Merge(IntermediateMemberMatchConditions first, IntermediateMemberMatchConditions second)
		{
			if (first == null && second == null)
			{
				return null;
			}
			IntermediateMemberMatchConditions intermediateMemberMatchConditions = new IntermediateMemberMatchConditions();
			intermediateMemberMatchConditions.AddMatchConditions(first);
			intermediateMemberMatchConditions.AddMatchConditions(second);
			return intermediateMemberMatchConditions;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x000387A0 File Offset: 0x000369A0
		private void AddMatchConditions(IntermediateMemberMatchConditions conditions)
		{
			if (conditions != null)
			{
				foreach (KeyValuePair<DataMember, IntermediateMatchCondition> keyValuePair in conditions)
				{
					base.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}
	}
}
