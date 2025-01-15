using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.Model;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000188 RID: 392
	internal class HighlightDeclarationCollector : DataShapeVisitor
	{
		// Token: 0x06000DAD RID: 3501 RVA: 0x000380D4 File Offset: 0x000362D4
		private HighlightDeclarationCollector(WritableExpressionTable expressionTable, IFeatureSwitchProvider featureSwitches, IFederatedConceptualSchema schema, TranslationErrorContext errorContext, PlanDeclarationCollection declarations)
		{
			this.m_expressionTable = expressionTable;
			this.m_featureSwitches = featureSwitches;
			this.m_schema = schema;
			this.m_errorContext = errorContext;
			this.m_declarations = declarations;
			this.m_filterDeclarations = new Dictionary<FilterCondition, PlanOperationDeclarationReference>();
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003810C File Offset: 0x0003630C
		internal static IFilterDeclarationCollection Analyze(WritableExpressionTable expressionTable, IFeatureSwitchProvider featureSwitches, IFederatedConceptualSchema schema, TranslationErrorContext errorContext, PlanDeclarationCollection declarations, DataShape dataShape)
		{
			HighlightDeclarationCollector highlightDeclarationCollector = new HighlightDeclarationCollector(expressionTable, featureSwitches, schema, errorContext, declarations);
			highlightDeclarationCollector.Visit(dataShape);
			if (highlightDeclarationCollector.m_filterDeclarations.Count == 0)
			{
				return EmptyFilterDeclarationCollection.Instance;
			}
			return new FilterDeclarationCollection(highlightDeclarationCollector.m_filterDeclarations);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0003814C File Offset: 0x0003634C
		protected override void Enter(DataShape dataShape)
		{
			if (dataShape.Filters.IsNullOrEmpty<Filter>())
			{
				return;
			}
			IReadOnlyList<Filter> filtersForTargetStructureType = dataShape.Filters.GetFiltersForTargetStructureType(this.m_expressionTable);
			if (filtersForTargetStructureType.IsNullOrEmpty<Filter>())
			{
				return;
			}
			foreach (Filter filter in filtersForTargetStructureType)
			{
				bool flag2;
				bool flag3;
				bool flag = FilterComplexityAnalyzer.IsComplexFilter(filter.Condition, this.m_expressionTable, this.m_schema.SupportsHierarchicalFilterDisjunction(), true, out flag2, out flag3, this.m_schema);
				ResolvedCalculationReferenceExpressionNode resolvedCalculationReferenceExpressionNode = (ResolvedCalculationReferenceExpressionNode)this.m_expressionTable.GetNode(filter.Target);
				if (flag)
				{
					this.m_errorContext.Register(TranslationMessages.ComplexHighlightsNotAllowed(EngineMessageSeverity.Error, filter.Condition.ObjectType, resolvedCalculationReferenceExpressionNode.Calculation.Id, "Highlights", resolvedCalculationReferenceExpressionNode.Calculation.Id));
				}
				else if (flag3)
				{
					if (!this.m_schema.GetDefaultSchemaDaxCapabilitiesAnnotation().SupportsKeepFiltersOverTableVariable())
					{
						if (FilterComplexityAnalyzer.IsComplexFilter(filter.Condition, this.m_expressionTable, this.m_schema.SupportsHierarchicalFilterDisjunction(), false, this.m_schema))
						{
							this.m_errorContext.Register(TranslationMessages.ComplexHighlightsNotAllowed(EngineMessageSeverity.Error, filter.Condition.ObjectType, resolvedCalculationReferenceExpressionNode.Calculation.Id, "Highlights", resolvedCalculationReferenceExpressionNode.Calculation.Id));
						}
					}
					else
					{
						PlanOperationCreateFilterContextTable planOperationCreateFilterContextTable = new PlanOperationCreateFilterContextTable(filter.Condition);
						Calculation calculation = resolvedCalculationReferenceExpressionNode.Calculation;
						PlanOperationDeclarationReference planOperationDeclarationReference;
						if (!this.m_filterDeclarations.TryGetValue(filter.Condition, out planOperationDeclarationReference))
						{
							planOperationDeclarationReference = planOperationCreateFilterContextTable.DeclareIfNotDeclared(PlanNames.FilterTable(calculation.Id, null), this.m_declarations, true, false, null, false);
							this.m_filterDeclarations.Add(filter.Condition, planOperationDeclarationReference);
						}
					}
				}
			}
		}

		// Token: 0x040006AD RID: 1709
		private readonly WritableExpressionTable m_expressionTable;

		// Token: 0x040006AE RID: 1710
		private readonly IFeatureSwitchProvider m_featureSwitches;

		// Token: 0x040006AF RID: 1711
		private readonly IFederatedConceptualSchema m_schema;

		// Token: 0x040006B0 RID: 1712
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x040006B1 RID: 1713
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x040006B2 RID: 1714
		private Dictionary<FilterCondition, PlanOperationDeclarationReference> m_filterDeclarations;
	}
}
