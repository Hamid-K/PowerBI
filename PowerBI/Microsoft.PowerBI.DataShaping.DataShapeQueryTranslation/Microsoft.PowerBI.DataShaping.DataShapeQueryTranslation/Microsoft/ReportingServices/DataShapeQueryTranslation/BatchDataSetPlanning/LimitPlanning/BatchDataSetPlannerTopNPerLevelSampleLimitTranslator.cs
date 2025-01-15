using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.LimitPlanning
{
	// Token: 0x02000239 RID: 569
	internal sealed class BatchDataSetPlannerTopNPerLevelSampleLimitTranslator
	{
		// Token: 0x06001377 RID: 4983 RVA: 0x0004B47D File Offset: 0x0004967D
		private BatchDataSetPlannerTopNPerLevelSampleLimitTranslator(ICommonPlanningContext context, PlanOperationContext input, Limit limit, IReadOnlyList<DataMember> primaryDynamics, BatchRestartIndicator restartIndicator)
		{
			this.m_context = context;
			this.m_input = input;
			this.m_limit = limit;
			this.m_primaryDynamics = primaryDynamics;
			this.m_restartIndicator = restartIndicator;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0004B4AC File Offset: 0x000496AC
		public static PlanOperationContext Translate(ICommonPlanningContext context, PlanOperationContext input, Limit limit, IReadOnlyList<DataMember> primaryDynamics, out BatchRestartIndicator restartIndicator)
		{
			restartIndicator = new BatchRestartIndicator("RestartIndicator", primaryDynamics);
			PlanOperation planOperation = new BatchDataSetPlannerTopNPerLevelSampleLimitTranslator(context, input, limit, primaryDynamics, restartIndicator).Translate();
			return input.ReplaceTable(planOperation, null, null, null);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0004B4E4 File Offset: 0x000496E4
		private PlanOperation Translate()
		{
			TopNPerLevelLimitOperator topNPerLevelLimitOperator = this.m_limit.Operator as TopNPerLevelLimitOperator;
			Contract.RetailAssert(topNPerLevelLimitOperator != null, "Limit operator should have been TopNPerLevelLimitOperator");
			PlanExpression planExpression = this.CreateCountExpression(topNPerLevelLimitOperator.Count.Value);
			List<PlanMemberSortItem> list = this.m_primaryDynamics.ToSortItems(this.m_context.Annotations, false).Cast<PlanMemberSortItem>().ToList<PlanMemberSortItem>();
			List<List<Expression>> list2 = this.ReplaceLevelsExpression(topNPerLevelLimitOperator.Levels, this.m_primaryDynamics);
			return this.m_input.Table.TopNPerLevelSample(planExpression, this.m_restartIndicator.RestartIndicatorName, list, list2, topNPerLevelLimitOperator.WindowExpansionInstance);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0004B57B File Offset: 0x0004977B
		private PlanExpression CreateCountExpression(int count)
		{
			return BatchDataSetPlanningUtils.CreateLimitCountExpression(count, this.m_limit.Id, this.m_context.ErrorContext);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0004B59C File Offset: 0x0004979C
		private List<List<Expression>> ReplaceLevelsExpression(IReadOnlyList<IReadOnlyList<Expression>> levels, IReadOnlyList<DataMember> dataMembers)
		{
			if (levels.IsNullOrEmpty<IReadOnlyList<Expression>>())
			{
				return null;
			}
			List<List<Expression>> list = new List<List<Expression>>(levels.Count);
			for (int i = 0; i < levels.Count; i++)
			{
				Dictionary<ExpressionNode, Expression> dictionary = new Dictionary<ExpressionNode, Expression>(levels[i].Count);
				foreach (SortKey sortKey in dataMembers[i].Group.SortKeys)
				{
					dictionary[this.m_context.OutputExpressionTable.GetNode(sortKey.Value)] = sortKey.Value;
				}
				List<Expression> list2 = new List<Expression>(levels[i].Count);
				foreach (Expression expression in levels[i])
				{
					ExpressionNode node = this.m_context.OutputExpressionTable.GetNode(expression);
					Expression expression2;
					if (!dictionary.TryGetValue(node, out expression2))
					{
						TranslationMessage translationMessage = TranslationMessages.TopNPerLevelLevelNotPresentOnPrimary(EngineMessageSeverity.Error, ObjectType.TopNPerLevelLimitOperator, this.m_limit.Id, "Levels");
						this.m_context.ErrorContext.Register(translationMessage);
						throw new DataSetPlanningException(translationMessage.Message);
					}
					list2.Add(expression2);
				}
				list.Add(list2);
			}
			return list;
		}

		// Token: 0x0400089C RID: 2204
		private readonly ICommonPlanningContext m_context;

		// Token: 0x0400089D RID: 2205
		private readonly PlanOperationContext m_input;

		// Token: 0x0400089E RID: 2206
		private readonly Limit m_limit;

		// Token: 0x0400089F RID: 2207
		private readonly IReadOnlyList<DataMember> m_primaryDynamics;

		// Token: 0x040008A0 RID: 2208
		private readonly BatchRestartIndicator m_restartIndicator;
	}
}
