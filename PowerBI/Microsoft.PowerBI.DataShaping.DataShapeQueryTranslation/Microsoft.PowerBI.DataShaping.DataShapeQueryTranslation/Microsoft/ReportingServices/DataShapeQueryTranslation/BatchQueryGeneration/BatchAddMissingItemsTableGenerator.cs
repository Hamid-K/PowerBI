using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200012E RID: 302
	internal sealed class BatchAddMissingItemsTableGenerator
	{
		// Token: 0x06000B51 RID: 2897 RVA: 0x0002C83C File Offset: 0x0002AA3C
		private BatchAddMissingItemsTableGenerator(GeneratedTable input, BatchQueryGenerationContext context, GeneratedDeclarationCollection declarations, IQueryExpressionGenerator expressionGenerator)
		{
			this.m_input = input;
			this.m_context = context;
			this.m_declarations = declarations;
			this.m_expressionGenerator = expressionGenerator;
			this.m_groupKeysWithShowAll = new HashSet<GroupKey>();
			this.m_builder = new AddMissingItemsTableBuilder(input.QueryTable);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002C888 File Offset: 0x0002AA88
		public static GeneratedTable Generate(PlanOperationAddMissingItems operation, GeneratedTable input, BatchQueryGenerationContext context, GeneratedDeclarationCollection declarations, IQueryExpressionGenerator expressionGenerator)
		{
			return new BatchAddMissingItemsTableGenerator(input, context, declarations, expressionGenerator).Generate(operation);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002C89A File Offset: 0x0002AA9A
		private GeneratedTable Generate(PlanOperationAddMissingItems operation)
		{
			this.AddShowAllColumns(operation);
			this.AddContextTables(operation);
			this.AddGroups(operation);
			return new GeneratedTable(this.m_builder.ToQueryTable(), this.m_input.ColumnMap);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002C8CC File Offset: 0x0002AACC
		private void AddGroups(PlanOperationAddMissingItems operation)
		{
			IAddMissingItemsRollupBuilder addMissingItemsRollupBuilder = null;
			IAddMissingItemsGroupBuilder addMissingItemsGroupBuilder = null;
			foreach (PlanGroupByMember planGroupByMember in operation.Groups)
			{
				if (planGroupByMember.RequiresRollupGroup)
				{
					if (addMissingItemsRollupBuilder == null)
					{
						addMissingItemsRollupBuilder = this.m_builder.AddRollup();
					}
					QueryTableColumn queryTableColumn = this.m_input.ColumnMap[planGroupByMember.SubtotalIndicatorColumnName];
					addMissingItemsGroupBuilder = addMissingItemsRollupBuilder.AddRollupGroup(queryTableColumn.QdmReference());
				}
				else if (addMissingItemsRollupBuilder == null)
				{
					addMissingItemsGroupBuilder = this.m_builder.AddGroup();
				}
				this.AddGroupKeysAndDetails(addMissingItemsGroupBuilder, planGroupByMember);
				this.AddGroupContextTables(addMissingItemsGroupBuilder, planGroupByMember);
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002C974 File Offset: 0x0002AB74
		private void AddGroupContextTables(IAddMissingItemsGroupBuilder groupBuilder, PlanGroupByMember groupByMemberItem)
		{
			if (groupByMemberItem.ContextTables.IsNullOrEmpty<PlanOperation>())
			{
				return;
			}
			foreach (global::System.ValueTuple<QueryTable, bool> valueTuple in BatchQueryGenerationUtils.GenerateContextTables(groupByMemberItem.ContextTables, this.m_declarations, this.m_context, this.m_expressionGenerator, new BatchQueryExpressionReferenceContext()))
			{
				groupBuilder.AddContextTable(valueTuple.Item1.Expression.NonVisual());
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002C9FC File Offset: 0x0002ABFC
		private void AddShowAllColumns(PlanOperationAddMissingItems operation)
		{
			foreach (PlanGroupByMember planGroupByMember in operation.ShowAllMembers)
			{
				foreach (GroupCluster groupCluster in GroupClusterBuilder.Build(planGroupByMember.Member.Group, this.m_context.ExpressionTable))
				{
					foreach (GroupKey groupKey in groupCluster.GroupKeys)
					{
						ExpressionId value = groupKey.Value.ExpressionId.Value;
						QueryTableColumn queryTableColumn = this.m_input.ColumnMap[value];
						if (groupCluster.ShowItemsWithNoData)
						{
							this.m_builder.AddShowAllColumn(queryTableColumn.QdmReference());
							this.m_groupKeysWithShowAll.Add(groupKey);
						}
					}
				}
			}
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0002CB28 File Offset: 0x0002AD28
		private void AddGroupKeysAndDetails(IAddMissingItemsGroupBuilder groupBuilder, PlanGroupByMember groupByMember)
		{
			foreach (GroupKey groupKey in groupByMember.Member.Group.GroupKeys)
			{
				ExpressionId expressionId = groupKey.Value.ExpressionId.Value;
				QueryTableColumn queryTableColumn = this.m_input.ColumnMap[groupKey.Value.ExpressionId.Value];
				groupBuilder.AddGroupKey(queryTableColumn.QdmReference());
				bool flag = this.m_groupKeysWithShowAll.Contains(groupKey);
				expressionId = this.AddGroupDetails(groupBuilder, expressionId, flag);
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002CBE0 File Offset: 0x0002ADE0
		private ExpressionId AddGroupDetails(IAddMissingItemsGroupBuilder groupBuilder, ExpressionId keyExprId, bool isShowAll)
		{
			ReadOnlyCollection<ExpressionId> readOnlyCollection;
			if (this.m_context.GroupDetailMapping.TryGetDetails(keyExprId, out readOnlyCollection))
			{
				foreach (ExpressionId expressionId in readOnlyCollection)
				{
					QueryTableColumn queryTableColumn;
					if (this.m_input.ColumnMap.TryGetColumn(expressionId, out queryTableColumn))
					{
						groupBuilder.AddGroupKey(queryTableColumn.QdmReference());
						if (isShowAll)
						{
							this.m_builder.AddShowAllColumn(queryTableColumn.QdmReference());
						}
					}
				}
			}
			return keyExprId;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002CC6C File Offset: 0x0002AE6C
		private void AddContextTables(PlanOperationAddMissingItems operation)
		{
			foreach (global::System.ValueTuple<QueryTable, bool> valueTuple in BatchQueryGenerationUtils.GenerateContextTables(operation.ContextTables, this.m_declarations, this.m_context, this.m_expressionGenerator, new BatchQueryExpressionReferenceContext()))
			{
				this.m_builder.AddContextTable(valueTuple.Item1);
			}
		}

		// Token: 0x040005B7 RID: 1463
		private readonly GeneratedTable m_input;

		// Token: 0x040005B8 RID: 1464
		private readonly BatchQueryGenerationContext m_context;

		// Token: 0x040005B9 RID: 1465
		private readonly GeneratedDeclarationCollection m_declarations;

		// Token: 0x040005BA RID: 1466
		private readonly IQueryExpressionGenerator m_expressionGenerator;

		// Token: 0x040005BB RID: 1467
		private readonly HashSet<GroupKey> m_groupKeysWithShowAll;

		// Token: 0x040005BC RID: 1468
		private readonly AddMissingItemsTableBuilder m_builder;
	}
}
