using System;
using Microsoft.DataShaping.Processing.DataShapeResultGeneration;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x02000096 RID: 150
	internal sealed class DataPipelineUtils
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x0000CC74 File Offset: 0x0000AE74
		internal static int PushMatchConditionsForRelationships(DataBinding dataBinding, ExpressionEvaluatorBase evaluator, MatchConditionGovernor conditionGovernor)
		{
			int num = 0;
			if (dataBinding != null && dataBinding.Relationships != null)
			{
				Relationship relationship = dataBinding.Relationships[0];
				for (int i = 0; i < relationship.JoinConditions.Count; i++)
				{
					JoinCondition joinCondition = relationship.JoinConditions[i];
					FieldValueExpressionNode fieldValueExpressionNode = joinCondition.SecondaryKey as FieldValueExpressionNode;
					object obj = evaluator.Evaluate(joinCondition.PrimaryKey);
					conditionGovernor.PushCondition(fieldValueExpressionNode.FieldIndex, obj);
					num++;
				}
			}
			return num;
		}
	}
}
