using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001C5 RID: 453
	public sealed class DetailTableBuilder
	{
		// Token: 0x06000FF1 RID: 4081 RVA: 0x00040E14 File Offset: 0x0003F014
		private DetailTableBuilder(IDetailTableBuilderContext plannerContext, DataShapeContext dsContext, ReadOnlyCollection<PlanOperation> filters, WritableExpressionTable writableExpressionTable)
		{
			this.m_plannerContext = plannerContext;
			this.m_dsContext = dsContext;
			this.m_filters = filters;
			this.m_outputExpressionTable = writableExpressionTable;
			this.m_detailIdentityEntity = this.GetDetailIdentity();
			this.m_tableScanProjections = new List<ExpressionId>();
			IConceptualSchema defaultSchema = this.m_plannerContext.Schema.GetDefaultSchema();
			IReadOnlyList<IConceptualEntity> relatedToOneEntities = QueryAlgorithms.GetRelatedToOneEntities(this.m_detailIdentityEntity, defaultSchema);
			this.m_detailTableExpressionRewriter = new DetailTableExpressionRewriter(this.m_detailIdentityEntity, relatedToOneEntities, this.m_plannerContext.Schema);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00040E96 File Offset: 0x0003F096
		internal static PlanOperation CreateDetailTable(IDetailTableBuilderContext plannerContext, DataShapeContext dsContext, ReadOnlyCollection<PlanOperation> filters, WritableExpressionTable outputExpressionTable)
		{
			return new DetailTableBuilder(plannerContext, dsContext, filters, outputExpressionTable).ToTable();
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00040EA8 File Offset: 0x0003F0A8
		private IConceptualEntity GetDetailIdentity()
		{
			IConceptualEntity conceptualEntity;
			this.m_dsContext.DetailIdentities.Single<DetailGroupIdentity>().TryGetDetailGroupIdentityEntity(this.m_plannerContext.OutputExpressionTable, out conceptualEntity);
			return conceptualEntity;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00040EDC File Offset: 0x0003F0DC
		private PlanOperation ToTable()
		{
			IList<PlanProjectItem> projectionsForDetailQuery = this.GetProjectionsForDetailQuery();
			PlanOperation planOperation = PlanOperationBuilder.TableScan(this.m_detailIdentityEntity, this.m_tableScanProjections);
			DataShapeAnnotations annotations = this.m_plannerContext.Annotations;
			IEnumerable<PlanSortItem> enumerable = this.m_dsContext.PrimaryDynamics.ToSortItems(annotations, true);
			PlanOperation planOperation2 = this.ApplySegmentationByRowTransform(planOperation, enumerable, annotations);
			planOperation2 = this.ApplyProjections(planOperation2, projectionsForDetailQuery);
			planOperation2 = this.ApplySlicers(planOperation2);
			return planOperation2.SortBy(enumerable);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00040F4C File Offset: 0x0003F14C
		private PlanOperation ApplyProjections(PlanOperation inputTable, IList<PlanProjectItem> projectItems)
		{
			List<PlanProjectItem> list = new List<PlanProjectItem>();
			list.Add(PlanOperationBuilder.ToAllColumnsProjectItem());
			list.AddRange(projectItems);
			return inputTable.Project(list, false);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00040F79 File Offset: 0x0003F179
		private PlanOperation ApplySlicers(PlanOperation input)
		{
			if (this.m_filters.Count == 0)
			{
				return input;
			}
			return input.CalculateTableInFilterContext(this.m_filters);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00040F98 File Offset: 0x0003F198
		private PlanOperation ApplySegmentationByRowTransform(PlanOperation inputTable, IEnumerable<PlanSortItem> sorts, DataShapeAnnotations annotations)
		{
			PlanExpression limitOrWindowCount = this.GetLimitOrWindowCount();
			if (limitOrWindowCount == null)
			{
				return inputTable;
			}
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Limit primaryHierarchyLimit = this.m_dsContext.PrimaryHierarchyLimit;
			if (primaryHierarchyLimit != null)
			{
				Microsoft.DataShaping.InternalContracts.DataShapeQuery.TopLimitOperator topLimitOperator = primaryHierarchyLimit.Operator as Microsoft.DataShaping.InternalContracts.DataShapeQuery.TopLimitOperator;
				return this.CreatePlanOperationForTopLimit(inputTable, sorts, limitOrWindowCount, (topLimitOperator != null) ? topLimitOperator.Skip : null);
			}
			return this.CreatePlanOperationForTopLimit(inputTable, sorts, limitOrWindowCount, new long?(0L));
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00040FFC File Offset: 0x0003F1FC
		private PlanOperation CreatePlanOperationForTopLimit(PlanOperation inputTable, IEnumerable<PlanSortItem> sorts, PlanExpression rowCountExpr, long? skipCount)
		{
			if (skipCount != null)
			{
				PlanExpression planExpression = DetailTableBuilder.CreatePlanExpressionForSkipCount(skipCount.Value, this.m_dsContext.Id, this.m_plannerContext.ErrorContext);
				return inputTable.TopNSkip(rowCountExpr, planExpression, sorts);
			}
			return inputTable.TopN(rowCountExpr, sorts, false);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00041048 File Offset: 0x0003F248
		private static PlanExpression CreatePlanExpressionForSkipCount(long skipCount, Identifier dataShapeId, TranslationErrorContext errorContext)
		{
			ExpressionNode expressionNode = ExprNodes.Literal(skipCount);
			ExpressionContext expressionContext = new ExpressionContext(errorContext, ObjectType.DataShape, dataShapeId, "Skip");
			return new PlanExpression(expressionNode, expressionContext);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00041078 File Offset: 0x0003F278
		private PlanExpression GetLimitOrWindowCount()
		{
			int? num = BatchDataSetPlanningSegmentationUtils.DetermineEffectiveSegmentSizeWithoutTotals(this.m_dsContext.DataShape.RequestedPrimaryLeafCount);
			if (num != null)
			{
				return BatchDataSetPlanningUtils.CreateSegmentSizeExpression(num.Value, this.m_dsContext.Id, this.m_plannerContext.ErrorContext);
			}
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Limit primaryHierarchyLimit = this.m_dsContext.PrimaryHierarchyLimit;
			if (primaryHierarchyLimit == null)
			{
				return null;
			}
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.TopLimitOperator topLimitOperator = primaryHierarchyLimit.Operator as Microsoft.DataShaping.InternalContracts.DataShapeQuery.TopLimitOperator;
			if (topLimitOperator == null)
			{
				this.m_plannerContext.ErrorContext.Register(TranslationMessages.InvalidLimitOperator(EngineMessageSeverity.Error, ObjectType.Limit, primaryHierarchyLimit.Id.Value, "Operator"));
				return null;
			}
			return BatchDataSetPlanningUtils.CreateLimitCountExpression(topLimitOperator.GetPaddedCount(), primaryHierarchyLimit.Id, this.m_plannerContext.ErrorContext);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00041134 File Offset: 0x0003F334
		private IList<PlanProjectItem> GetProjectionsForDetailQuery()
		{
			List<PlanProjectItem> list = new List<PlanProjectItem>();
			foreach (DataMember dataMember in this.m_dsContext.PrimaryDynamics)
			{
				foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey groupKey in dataMember.Group.GroupKeys)
				{
					this.CreateAndAddProjection(groupKey.Value, groupKey, list, dataMember.Id.Value);
				}
				if (dataMember.Group.SortKeys != null)
				{
					foreach (SortKey sortKey in dataMember.Group.SortKeys)
					{
						this.CreateAndAddProjection(sortKey.Value, sortKey, list, dataMember.Id.Value);
					}
				}
				if (dataMember.Group.ScopeIdDefinition != null)
				{
					foreach (ScopeValueDefinition scopeValueDefinition in dataMember.Group.ScopeIdDefinition.Values)
					{
						this.CreateAndAddProjection(scopeValueDefinition.Value, scopeValueDefinition, list, dataMember.Id.Value);
					}
				}
				if (dataMember.Calculations != null)
				{
					foreach (Calculation calculation in dataMember.Calculations)
					{
						this.CreateAndAddProjection(calculation.Value, calculation, list, dataMember.Id.Value);
					}
				}
			}
			return list;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00041374 File Offset: 0x0003F574
		private void CreateAndAddProjection(Expression expression, IIdentifiable item, IList<PlanProjectItem> projections, Identifier ownerId)
		{
			ExpressionId value = expression.ExpressionId.Value;
			ExpressionNode node = this.m_plannerContext.OutputExpressionTable.GetNode(value);
			ExpressionNode expressionNode = this.m_detailTableExpressionRewriter.Rewrite(node);
			this.m_outputExpressionTable.SetNode(value, expressionNode);
			PlanProjectItem planProjectItem;
			if (this.TryCreateProjectItem(expressionNode, expression, item, ownerId, this.m_tableScanProjections, out planProjectItem))
			{
				projections.Add(planProjectItem);
			}
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x000413DC File Offset: 0x0003F5DC
		private bool TryCreateProjectItem(ExpressionNode expressionNode, Expression expression, IIdentifiable item, Identifier ownerId, IList<ExpressionId> tableScanProjections, out PlanProjectItem projectItem)
		{
			projectItem = null;
			ExpressionId value = expression.ExpressionId.Value;
			if (expressionNode.Kind == ExpressionNodeKind.ResolvedProperty)
			{
				ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = (ResolvedPropertyExpressionNode)expressionNode;
				tableScanProjections.Add(value);
				return false;
			}
			if (expressionNode.Kind == ExpressionNodeKind.RelatedResolvedProperty)
			{
				string edmName = ((RelatedResolvedPropertyExpressionNode)expressionNode).Property.EdmName;
				ExpressionContext expressionContext = new ExpressionContext(this.m_plannerContext.ErrorContext, item.ObjectType, ownerId, edmName);
				projectItem = new PlanNewColumnProjectItem(value, edmName, expressionContext, ColumnReuseKind.ByExpression);
				return true;
			}
			if (item is Calculation)
			{
				Calculation calculation = (Calculation)item;
				projectItem = calculation.ToNewColumnProjectItem();
				return true;
			}
			string value2 = ownerId.Value;
			ExpressionContext expressionContext2 = new ExpressionContext(this.m_plannerContext.ErrorContext, item.ObjectType, ownerId, ownerId.Value);
			projectItem = new PlanNewColumnProjectItem(value, value2, expressionContext2, ColumnReuseKind.ByExpression);
			return true;
		}

		// Token: 0x04000775 RID: 1909
		private readonly IDetailTableBuilderContext m_plannerContext;

		// Token: 0x04000776 RID: 1910
		private readonly DataShapeContext m_dsContext;

		// Token: 0x04000777 RID: 1911
		private readonly ReadOnlyCollection<PlanOperation> m_filters;

		// Token: 0x04000778 RID: 1912
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x04000779 RID: 1913
		private readonly IConceptualEntity m_detailIdentityEntity;

		// Token: 0x0400077A RID: 1914
		private readonly DetailTableExpressionRewriter m_detailTableExpressionRewriter;

		// Token: 0x0400077B RID: 1915
		private readonly List<ExpressionId> m_tableScanProjections;
	}
}
