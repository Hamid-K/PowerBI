using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000176 RID: 374
	internal sealed class IntermediateMemberDiscardConditions : Dictionary<DataMember, IntermediateDiscardCondition>
	{
		// Token: 0x06000D69 RID: 3433 RVA: 0x00037410 File Offset: 0x00035610
		internal IntermediateMemberDiscardConditions()
		{
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00037418 File Offset: 0x00035618
		internal BatchMemberDiscardConditions Bind(BatchDataSetPlan plan, OutputTableMapping outputTableMapping)
		{
			BatchMemberDiscardConditions batchMemberDiscardConditions = new BatchMemberDiscardConditions(base.Count);
			foreach (KeyValuePair<DataMember, IntermediateDiscardCondition> keyValuePair in this)
			{
				batchMemberDiscardConditions.Add(keyValuePair.Key, keyValuePair.Value.Bind(plan, outputTableMapping));
			}
			return batchMemberDiscardConditions;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00037488 File Offset: 0x00035688
		internal void FixupOutputTable(IEnumerable<DataMember> members, string outputTableName)
		{
			if (this.IsNullOrEmpty<KeyValuePair<DataMember, IntermediateDiscardCondition>>())
			{
				return;
			}
			foreach (DataMember dataMember in members)
			{
				IntermediateDiscardCondition intermediateDiscardCondition;
				if (base.TryGetValue(dataMember, out intermediateDiscardCondition))
				{
					intermediateDiscardCondition.SetOutputTableName(outputTableName);
				}
			}
		}
	}
}
