using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000D3 RID: 211
	internal static class BindingGenerationUtils
	{
		// Token: 0x060008D7 RID: 2263 RVA: 0x000225D8 File Offset: 0x000207D8
		internal static bool IsDuplicateMember(List<JoinCondition> joinConditions, DataMember member, ExpressionTable expressionTable)
		{
			return joinConditions != null && joinConditions.Any((JoinCondition jc) => BindingGenerationUtils.HaveSameGroupKeys(jc.DataMember, member, expressionTable));
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00022610 File Offset: 0x00020810
		internal static bool HaveSameGroupKeys(DataMember member1, DataMember member2, ExpressionTable expressionTable)
		{
			List<GroupKey> groupKeys = member1.Group.GroupKeys;
			List<GroupKey> groupKeys2 = member2.Group.GroupKeys;
			if (groupKeys.Count != groupKeys2.Count)
			{
				return false;
			}
			for (int i = 0; i < groupKeys.Count; i++)
			{
				ExpressionNode node = expressionTable.GetNode(groupKeys[i].Value);
				ExpressionNode node2 = expressionTable.GetNode(groupKeys2[i].Value);
				if (!node.Equals(node2))
				{
					return false;
				}
			}
			return true;
		}
	}
}
