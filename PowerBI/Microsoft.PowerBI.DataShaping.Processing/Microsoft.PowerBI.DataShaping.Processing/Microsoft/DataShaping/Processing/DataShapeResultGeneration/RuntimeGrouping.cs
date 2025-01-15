using System;
using Microsoft.DataShaping.Processing.Pipeline;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x0200007F RID: 127
	internal sealed class RuntimeGrouping
	{
		// Token: 0x06000337 RID: 823 RVA: 0x0000A6D9 File Offset: 0x000088D9
		internal RuntimeGrouping(DataMember member)
		{
			this._member = member;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000A6E8 File Offset: 0x000088E8
		internal void Process(DataPipeline data, ExpressionEvaluator evaluator, Action action)
		{
			if (!evaluator.HasActiveRow)
			{
				return;
			}
			Group group = this._member.Group;
			DataLimitGovernor limitGovernor = data.LimitGovernor;
			DataMember activeMember = limitGovernor.ActiveMember;
			limitGovernor.ActiveMember = this._member;
			do
			{
				int num = this.PushGroupKeyConditions(data, evaluator, group);
				action();
				this.PopGroupKeyConditions(data, num);
				data.ClearActiveRow();
			}
			while (data.SetupNextRow(evaluator));
			limitGovernor.ActiveMember = activeMember;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000A754 File Offset: 0x00008954
		private int PushGroupKeyConditions(DataPipeline data, ExpressionEvaluator evaluator, Group group)
		{
			MatchConditionGovernor matchConditions = data.MatchConditions;
			int i;
			for (i = 0; i < group.GroupKeys.Count; i++)
			{
				FieldValueExpressionNode fieldValueExpressionNode = group.GroupKeys[i].Value as FieldValueExpressionNode;
				matchConditions.PushCondition(fieldValueExpressionNode.FieldIndex, evaluator.Evaluate(fieldValueExpressionNode));
			}
			return i;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000A7AC File Offset: 0x000089AC
		private void PopGroupKeyConditions(DataPipeline data, int keyCount)
		{
			MatchConditionGovernor matchConditions = data.MatchConditions;
			for (int i = 0; i < keyCount; i++)
			{
				matchConditions.PopCondition();
			}
		}

		// Token: 0x040001D7 RID: 471
		private readonly DataMember _member;
	}
}
