using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x020001A5 RID: 421
	internal sealed class SortByMeasureTranslator
	{
		// Token: 0x06000ED4 RID: 3796 RVA: 0x0003BEEE File Offset: 0x0003A0EE
		private SortByMeasureTranslator(DataShapeContext dsContext, SortByMeasureTranslator.Context context)
		{
			this.m_dsContext = dsContext;
			this.m_context = context;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0003BF04 File Offset: 0x0003A104
		internal static BatchSortByMeasureExpressionMappings CreateSortByMeasureExpressionMappings(DataShape dataShape, ScopeTree scopeTree, DataShapeAnnotations annotations, WritableExpressionTable expressionTable, TranslationErrorContext errorContext, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery)
		{
			BatchSortByMeasureExpressionMappings batchSortByMeasureExpressionMappings = new BatchSortByMeasureExpressionMappings();
			SortByMeasureTranslator.AddSortByMeasureMappings(dataShape, scopeTree, annotations, expressionTable, errorContext, transformReferenceMap, applyTransformsInQuery, batchSortByMeasureExpressionMappings);
			return batchSortByMeasureExpressionMappings;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003BF28 File Offset: 0x0003A128
		private static void AddSortByMeasureMappings(DataShape dataShape, ScopeTree scopeTree, DataShapeAnnotations annotations, WritableExpressionTable expressionTable, TranslationErrorContext errorContext, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery, BatchSortByMeasureExpressionMappings mappings)
		{
			SortByMeasureTranslator.AddSortByMeasureExpressionMappingsForDataShape(dataShape, scopeTree, annotations, expressionTable, errorContext, mappings, transformReferenceMap, applyTransformsInQuery);
			if (!dataShape.DataShapes.IsNullOrEmpty<DataShape>())
			{
				foreach (DataShape dataShape2 in dataShape.DataShapes)
				{
					SortByMeasureTranslator.AddSortByMeasureMappings(dataShape2, scopeTree, annotations, expressionTable, errorContext, transformReferenceMap, applyTransformsInQuery, mappings);
				}
			}
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003BFA4 File Offset: 0x0003A1A4
		private static void AddSortByMeasureExpressionMappingsForDataShape(DataShape dataShape, ScopeTree scopeTree, DataShapeAnnotations annotations, WritableExpressionTable expressionTable, TranslationErrorContext errorContext, BatchSortByMeasureExpressionMappings mappings, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery)
		{
			IScope innermostScopeInDataShape = scopeTree.GetInnermostScopeInDataShape(dataShape);
			foreach (DataMember dataMember in scopeTree.GetAllParentScopes(innermostScopeInDataShape).OfType<DataMember>())
			{
				if (annotations.DataMemberAnnotations.HasSortByMeasureKeys(dataMember))
				{
					SortByMeasureInfoCollection sortByMeasureInfos = annotations.DataMemberAnnotations.GetSortByMeasureInfos(dataMember);
					if (sortByMeasureInfos != null && !sortByMeasureInfos.IsAtMeasureScope)
					{
						foreach (KeyValuePair<SortKey, SortByMeasureInfo> keyValuePair in sortByMeasureInfos)
						{
							SortKey key = keyValuePair.Key;
							ExpressionNode sortByMeasureNode = SortByMeasureTranslator.GetSortByMeasureNode(key, dataMember, expressionTable, keyValuePair.Value.SortKeyPlanIdentity, errorContext, scopeTree, annotations, transformReferenceMap, applyTransformsInQuery);
							ExpressionId expressionId = expressionTable.Add(sortByMeasureNode);
							mappings.Add(key, expressionId, keyValuePair.Value.SuggestedName);
						}
					}
				}
			}
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003C0A8 File Offset: 0x0003A2A8
		private static ExpressionNode GetSortByMeasureNode(SortKey sortKey, IScope containingScope, ExpressionTable expressionTable, ExpressionId expressionId, TranslationErrorContext errorContext, ScopeTree scopeTree, DataShapeAnnotations annotations, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery)
		{
			ExpressionNode expressionNode = expressionTable.GetNode(expressionId);
			expressionNode = BatchDataSetPlannerFilterExpressionTreeTranslator.Translate(new ExpressionContext(errorContext, sortKey.ObjectType, sortKey.Id, "Value"), expressionNode, scopeTree, annotations, containingScope, expressionTable, transformReferenceMap, applyTransformsInQuery);
			FunctionCallExpressionNode functionCallExpressionNode = expressionNode as FunctionCallExpressionNode;
			if (functionCallExpressionNode != null && functionCallExpressionNode.Descriptor.Name == "Evaluate")
			{
				expressionNode = functionCallExpressionNode.Arguments[0];
			}
			return expressionNode;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003C115 File Offset: 0x0003A315
		internal static PlanOperation ApplySortByMeasureTransforms(DataShapeContext dsContext, SortByMeasureTranslator.Context context, PlanOperationContext primaryTable, bool primary)
		{
			return new SortByMeasureTranslator(dsContext, context).ApplySortByMeasureTransforms(primaryTable, primary);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0003C128 File Offset: 0x0003A328
		private PlanOperation ApplySortByMeasureTransforms(PlanOperationContext primaryTable, bool primary)
		{
			if (this.m_context.SortByMeasureExpressionMappings.Count == 0)
			{
				return primaryTable.Table;
			}
			IReadOnlyList<DataMember> readOnlyList = (primary ? this.m_dsContext.PrimaryDynamicsExcludingContextOnly : this.m_dsContext.SecondaryDynamicsExcludingContextOnly);
			List<PlanOperation> list = this.CreateSortByMeasureTables(readOnlyList);
			if (list == null || list.Count == 0)
			{
				return primaryTable.Table;
			}
			list.Insert(0, primaryTable.Table);
			PlanOperation planOperation = this.JoinSortByMeasureTables(list);
			string text = (primary ? PlanNames.PrimaryWithSortColumns(this.m_dsContext.DataShape.Id) : PlanNames.SecondaryWithSortColumns(this.m_dsContext.DataShape.Id));
			planOperation = planOperation.DeclareIfNotDeclared(text, this.m_context.Declarations, false, false, null, false);
			PlanOperation planOperation2 = this.RemoveUneededTotals(planOperation, primaryTable.GetAllGroups(), primaryTable.Calculations, primary);
			text = (primary ? PlanNames.PrimaryWithSortColumnsOutputTotals(this.m_dsContext.DataShape.Id) : PlanNames.SecondaryWithSortColumnsOutputTotals(this.m_dsContext.DataShape.Id));
			return planOperation2.DeclareIfNotDeclared(text, this.m_context.Declarations, false, false, null, false);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0003C23C File Offset: 0x0003A43C
		private List<PlanOperation> CreateSortByMeasureTables(IReadOnlyList<DataMember> dynamicMembers)
		{
			List<PlanOperation> list = null;
			for (int i = 0; i < dynamicMembers.Count; i++)
			{
				PlanOperation planOperation = this.CreateSortByMeasureTable(dynamicMembers, i);
				if (planOperation != null)
				{
					if (list == null)
					{
						list = new List<PlanOperation>();
					}
					planOperation = planOperation.DeclareIfNotDeclared(PlanNames.SortByMeasureTable(this.m_dsContext.DataShape.Id, dynamicMembers[i].Id), this.m_context.Declarations, false, false, null, false);
					list.Add(planOperation);
				}
			}
			return list;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0003C2B0 File Offset: 0x0003A4B0
		private PlanOperation CreateSortByMeasureTable(IReadOnlyList<DataMember> members, int memberIndex)
		{
			BatchSortByMeasureSourceAnnotation batchSortByMeasureSourceAnnotation;
			if (!this.m_context.Annotations.TryGetBatchSortByMeasureSourceAnnotation(members[memberIndex], out batchSortByMeasureSourceAnnotation))
			{
				return null;
			}
			FilterCondition filterCondition = this.CreateSortByMeasureTableFilterCondition(batchSortByMeasureSourceAnnotation, members, memberIndex);
			PlanOperation planOperation = this.m_context.Table.FilterBy(filterCondition);
			return this.CreateSortByMeasureTableProjection(planOperation, members, memberIndex);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0003C300 File Offset: 0x0003A500
		private FilterCondition CreateSortByMeasureTableFilterCondition(BatchSortByMeasureSourceAnnotation sortByMeasureAnnotation, IReadOnlyList<DataMember> members, int memberIndex)
		{
			List<FilterCondition> list = new List<FilterCondition>();
			if (sortByMeasureAnnotation.SameHierarchyAnnotation != null)
			{
				list.AddRange(this.CreateSubtotalIndicatorColumnConditions(members, memberIndex));
			}
			if (sortByMeasureAnnotation.OtherHierarchyAnnotation != null)
			{
				BinaryFilterCondition binaryFilterCondition = FilterUtils.CreateBooleanColumnFilterCondition(this.m_context.OutputExpressionTable, sortByMeasureAnnotation.OtherHierarchyAnnotation.SubtotalIndicatorColumnName, true);
				list.Add(binaryFilterCondition);
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return new CompoundFilterCondition
			{
				Operator = CompoundFilterOperator.All,
				Conditions = list
			};
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0003C380 File Offset: 0x0003A580
		private IEnumerable<FilterCondition> CreateSubtotalIndicatorColumnConditions(IReadOnlyList<DataMember> members, int memberIndex)
		{
			List<FilterCondition> list = new List<FilterCondition>();
			for (int i = 0; i < members.Count; i++)
			{
				bool flag = i > memberIndex;
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				if (this.m_context.Annotations.TryGetBatchSubtotalAnnotation(members[i], out batchSubtotalAnnotation) && (!batchSubtotalAnnotation.Usage.IsIncludeInOutput() || flag))
				{
					BinaryFilterCondition binaryFilterCondition = FilterUtils.CreateBooleanColumnFilterCondition(this.m_context.OutputExpressionTable, batchSubtotalAnnotation.SubtotalIndicatorColumnName, flag);
					list.Add(binaryFilterCondition);
				}
			}
			return list;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0003C3F8 File Offset: 0x0003A5F8
		private PlanOperation CreateSortByMeasureTableProjection(PlanOperation input, IReadOnlyList<DataMember> members, int memberIndex)
		{
			List<PlanProjectItem> list = new List<PlanProjectItem>();
			for (int i = 0; i <= memberIndex; i++)
			{
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				list.AddRange(members[i].ToGroupProjectItems(this.m_context.Annotations, SubtotalUsage.SortByMeasure, false, out batchSubtotalAnnotation));
			}
			DataMember dataMember = members[memberIndex];
			SortByMeasureInfoCollection sortByMeasureInfos = this.m_context.Annotations.DataMemberAnnotations.GetSortByMeasureInfos(dataMember);
			if (!sortByMeasureInfos.IsAtMeasureScope)
			{
				list.AddRange(this.CreateSortKeyProjectItems(sortByMeasureInfos));
			}
			return input.Project(list, false);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0003C47C File Offset: 0x0003A67C
		private IEnumerable<PlanProjectItem> CreateSortKeyProjectItems(SortByMeasureInfoCollection sortByMeasureInfos)
		{
			List<PlanProjectItem> list = new List<PlanProjectItem>();
			foreach (KeyValuePair<SortKey, SortByMeasureInfo> keyValuePair in sortByMeasureInfos)
			{
				BatchSortByMeasureExpressionMappings.SortByMeasureExpressionInfo sortByMeasureExpressionInfo = this.m_context.SortByMeasureExpressionMappings[keyValuePair.Key];
				ExpressionContext expressionContext = new ExpressionContext(this.m_context.ErrorContext, ObjectType.SortKey, keyValuePair.Key.Id, sortByMeasureExpressionInfo.SuggestedName);
				PlanTransformExistingColumnProjectItem planTransformExistingColumnProjectItem = sortByMeasureExpressionInfo.NewColumnExpression.ToNewColumnFromExistingProjectItem(expressionContext, keyValuePair.Value.GetEffectivePlanIdentities());
				list.Add(planTransformExistingColumnProjectItem);
			}
			return list;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003C528 File Offset: 0x0003A728
		private PlanOperation JoinSortByMeasureTables(List<PlanOperation> tables)
		{
			PlanOperation planOperation = new PlanOperationLeftOuterJoin(tables[0], tables[1]);
			for (int i = 2; i < tables.Count; i++)
			{
				planOperation = new PlanOperationLeftOuterJoin(planOperation, tables[i]);
			}
			return planOperation;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0003C56C File Offset: 0x0003A76C
		private PlanOperation RemoveUneededTotals(PlanOperation table, IReadOnlyList<DataMember> members, IReadOnlyList<Calculation> calculationsToPreserve, bool primary)
		{
			PlanOperation planOperation = CoreTableTotalsTransforms.FilterSortByMeasureTotalsFromTable(new CoreTableTotalsTransforms.Context(this.m_context.Annotations, this.m_context.OutputExpressionTable, this.m_context.SortByMeasureExpressionMappings, this.m_context.ScopeTree), table, members, primary);
			if (planOperation == table)
			{
				return table;
			}
			List<PlanProjectItem> list = new List<PlanProjectItem>();
			foreach (DataMember dataMember in members)
			{
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				list.AddRange(dataMember.ToGroupProjectItems(this.m_context.Annotations, SubtotalUsage.Output, false, out batchSubtotalAnnotation));
				SortByMeasureInfoCollection sortByMeasureInfos = this.m_context.Annotations.DataMemberAnnotations.GetSortByMeasureInfos(dataMember);
				if (sortByMeasureInfos != null && !dataMember.Group.SuppressSortByMeasureRollup)
				{
					foreach (KeyValuePair<SortKey, SortByMeasureInfo> keyValuePair in sortByMeasureInfos)
					{
						PlanPreserveColumnsProjectItem planPreserveColumnsProjectItem = new PlanPreserveColumnsProjectItem(keyValuePair.Value.GetEffectivePlanIdentities());
						list.Add(planPreserveColumnsProjectItem);
					}
				}
			}
			foreach (Calculation calculation in calculationsToPreserve)
			{
				list.Add(calculation.ToPreserveColumnProjectItem());
			}
			return planOperation.Project(list, false);
		}

		// Token: 0x04000700 RID: 1792
		private readonly DataShapeContext m_dsContext;

		// Token: 0x04000701 RID: 1793
		private readonly SortByMeasureTranslator.Context m_context;

		// Token: 0x020002FE RID: 766
		internal sealed class Context
		{
			// Token: 0x06001700 RID: 5888 RVA: 0x00052495 File Offset: 0x00050695
			internal Context(DataShapeAnnotations annotations, WritableExpressionTable outputExpressionTable, TranslationErrorContext errorContext, BatchSortByMeasureExpressionMappings sortByMeasureExpressionMappings, PlanDeclarationCollection declarations, ScopeTree scopeTree, PlanOperation table)
			{
				this.Annotations = annotations;
				this.OutputExpressionTable = outputExpressionTable;
				this.ErrorContext = errorContext;
				this.SortByMeasureExpressionMappings = sortByMeasureExpressionMappings;
				this.Declarations = declarations;
				this.ScopeTree = scopeTree;
				this.Table = table;
			}

			// Token: 0x17000409 RID: 1033
			// (get) Token: 0x06001701 RID: 5889 RVA: 0x000524D2 File Offset: 0x000506D2
			internal DataShapeAnnotations Annotations { get; }

			// Token: 0x1700040A RID: 1034
			// (get) Token: 0x06001702 RID: 5890 RVA: 0x000524DA File Offset: 0x000506DA
			internal WritableExpressionTable OutputExpressionTable { get; }

			// Token: 0x1700040B RID: 1035
			// (get) Token: 0x06001703 RID: 5891 RVA: 0x000524E2 File Offset: 0x000506E2
			internal TranslationErrorContext ErrorContext { get; }

			// Token: 0x1700040C RID: 1036
			// (get) Token: 0x06001704 RID: 5892 RVA: 0x000524EA File Offset: 0x000506EA
			internal BatchSortByMeasureExpressionMappings SortByMeasureExpressionMappings { get; }

			// Token: 0x1700040D RID: 1037
			// (get) Token: 0x06001705 RID: 5893 RVA: 0x000524F2 File Offset: 0x000506F2
			internal PlanDeclarationCollection Declarations { get; }

			// Token: 0x1700040E RID: 1038
			// (get) Token: 0x06001706 RID: 5894 RVA: 0x000524FA File Offset: 0x000506FA
			internal ScopeTree ScopeTree { get; }

			// Token: 0x1700040F RID: 1039
			// (get) Token: 0x06001707 RID: 5895 RVA: 0x00052502 File Offset: 0x00050702
			internal PlanOperation Table { get; }
		}
	}
}
