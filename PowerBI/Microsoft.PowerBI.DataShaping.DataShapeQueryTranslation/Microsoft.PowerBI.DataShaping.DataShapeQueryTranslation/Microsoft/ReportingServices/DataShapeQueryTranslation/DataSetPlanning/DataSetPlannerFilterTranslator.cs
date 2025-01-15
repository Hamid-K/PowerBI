using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000F7 RID: 247
	internal sealed class DataSetPlannerFilterTranslator : DataSetPlanElementVisitor
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x00025A87 File Offset: 0x00023C87
		private DataSetPlannerFilterTranslator(DataShapeAnnotations annotations, ScopeTree scopeTree, TranslationErrorContext errorContext, ReadOnlyExpressionTable expressionTable, IFederatedConceptualSchema schema)
		{
			this.m_annotations = annotations;
			this.m_scopeTree = scopeTree;
			this.m_errorContext = errorContext;
			this.m_expressionTable = expressionTable;
			this.m_schema = schema;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00025AB4 File Offset: 0x00023CB4
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x00025AC7 File Offset: 0x00023CC7
		private ScopePlanElement CurrentScopePlanElement
		{
			get
			{
				return this.m_translatedScopes[this.m_currentScopeIndex];
			}
			set
			{
				this.m_translatedScopes[this.m_currentScopeIndex] = value;
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00025ADC File Offset: 0x00023CDC
		public static DataSetPlannerFilterTranslatorResult Translate(IList<DataSetPlan> dataSetPlans, DataShapeAnnotations annotations, ScopeTree scopeTree, TranslationErrorContext errorContext, ReadOnlyExpressionTable expressionTable, IFederatedConceptualSchema schema)
		{
			DataSetPlannerFilterTranslator dataSetPlannerFilterTranslator = new DataSetPlannerFilterTranslator(annotations, scopeTree, errorContext, expressionTable, schema);
			int count = dataSetPlans.Count;
			List<DataSetPlan> list = new List<DataSetPlan>(count);
			for (int i = 0; i < count; i++)
			{
				DataSetPlan dataSetPlan = dataSetPlans[i];
				DataSetPlan dataSetPlan2 = dataSetPlannerFilterTranslator.TranslateFilters(dataSetPlan);
				list.Add(dataSetPlan2);
			}
			return new DataSetPlannerFilterTranslatorResult(list);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00025B34 File Offset: 0x00023D34
		private DataSetPlan TranslateFilters(DataSetPlan plan)
		{
			if (!this.RequiresSubqueryJoinPredicates(plan))
			{
				return plan;
			}
			this.m_dataSetPlan = plan;
			this.m_translatedScopes = new List<ScopePlanElement>(plan.Scopes);
			this.m_currentScopeIndex = 0;
			ReadOnlyCollection<ScopePlanElement> scopes = this.m_dataSetPlan.Scopes;
			for (int i = 0; i < scopes.Count; i++)
			{
				ScopePlanElement scopePlanElement = scopes[i];
				scopePlanElement.Accept(this);
				foreach (NestedPlanElement nestedPlanElement in scopePlanElement.NestedElements)
				{
					nestedPlanElement.Accept(this);
				}
				this.m_currentScopeIndex++;
			}
			return new DataSetPlan(this.m_dataSetPlan.Name, this.m_dataSetPlan.PlanIndex, this.m_translatedScopes, this.m_dataSetPlan.FilterEmptyGroups, this.m_dataSetPlan.SuppressSortingAndLimits, this.m_dataSetPlan.ExtensionSchema, this.m_dataSetPlan.DataSourceVariables, this.m_dataSetPlan.ModelParameters, this.m_dataSetPlan.QueryParameters);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00025C44 File Offset: 0x00023E44
		internal override void Visit(DataShapePlanElement planElement)
		{
			List<FilterCondition> list;
			bool flag;
			if (!FilterComplexityAnalyzer.TryGetFilterConditionsForJoinPredicateSubQuery(planElement.FilterCondition, this.m_schema, this.m_expressionTable, false, false, out list, out flag))
			{
				this.CurrentScopePlanElement = planElement;
				return;
			}
			if (flag)
			{
				this.m_errorContext.Register(TranslationMessages.ComplexSlicerNotAllowed(EngineMessageSeverity.Error, planElement.DataShape.Id));
				this.CurrentScopePlanElement = planElement;
				return;
			}
			if (list.Count > 100)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidFilterConditionExceedsMaxNumberOfDisjunctionsForSubqueryRewrite(EngineMessageSeverity.Error, ObjectType.Filter, planElement.FilterCondition.Id, "Condition", 100));
				this.CurrentScopePlanElement = planElement;
				return;
			}
			int count = list.Count;
			List<DataSetPlan> list2 = new List<DataSetPlan>(count);
			for (int i = 0; i < count; i++)
			{
				DataSetPlan dataSetPlan = JoinPredicateSubQueryDataSetPlanner.DeterminePlan(this.m_dataSetPlan, this.m_scopeTree, list[i], i);
				list2.Add(dataSetPlan);
			}
			DataSetPlannerFilterTranslatorResult dataSetPlannerFilterTranslatorResult = DataSetPlannerFilterTranslator.Translate(list2, this.m_annotations, this.m_scopeTree, this.m_errorContext, this.m_expressionTable, this.m_schema);
			this.CurrentScopePlanElement = new DataShapePlanElement(planElement.DataShape, planElement.NestedElements, planElement.IsProjected, null, dataSetPlannerFilterTranslatorResult.DataSetPlans, null, planElement.ValueFilter, planElement.AnyValueFilters, planElement.DefaultValueFilter);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00025D7C File Offset: 0x00023F7C
		internal bool RequiresSubqueryJoinPredicates(DataSetPlan plan)
		{
			bool flag = false;
			Calculation calculation = null;
			foreach (ScopePlanElement scopePlanElement in plan.Scopes)
			{
				if (!flag)
				{
					flag = FilterComplexityAnalyzer.IsComplexFilter(scopePlanElement.FilterCondition, this.m_expressionTable, false, false, this.m_schema);
					if (flag && calculation != null)
					{
						this.RegisterComplexSlicerWithMeasureError(calculation);
						return false;
					}
				}
				foreach (NestedPlanElement nestedPlanElement in scopePlanElement.NestedElements)
				{
					CalculationPlanElement calculationPlanElement = nestedPlanElement as CalculationPlanElement;
					if (calculationPlanElement != null && calculation == null && this.m_annotations.IsMeasure(calculationPlanElement.Calculation))
					{
						calculation = calculationPlanElement.Calculation;
						if (flag)
						{
							this.RegisterComplexSlicerWithMeasureError(calculation);
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00025E78 File Offset: 0x00024078
		private bool RegisterComplexSlicerWithMeasureError(Calculation measureCalc)
		{
			DataShape containingDataShapeOrSelf = this.m_scopeTree.GetContainingDataShapeOrSelf(this.m_scopeTree.GetContainingScope(measureCalc));
			this.m_errorContext.Register(TranslationMessages.ComplexSlicerNotAllowedWithMeasures(EngineMessageSeverity.Error, containingDataShapeOrSelf.Id));
			return false;
		}

		// Token: 0x040004B3 RID: 1203
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x040004B4 RID: 1204
		private readonly ScopeTree m_scopeTree;

		// Token: 0x040004B5 RID: 1205
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x040004B6 RID: 1206
		private readonly ReadOnlyExpressionTable m_expressionTable;

		// Token: 0x040004B7 RID: 1207
		private readonly IFederatedConceptualSchema m_schema;

		// Token: 0x040004B8 RID: 1208
		private List<ScopePlanElement> m_translatedScopes;

		// Token: 0x040004B9 RID: 1209
		private DataSetPlan m_dataSetPlan;

		// Token: 0x040004BA RID: 1210
		private int m_currentScopeIndex;
	}
}
