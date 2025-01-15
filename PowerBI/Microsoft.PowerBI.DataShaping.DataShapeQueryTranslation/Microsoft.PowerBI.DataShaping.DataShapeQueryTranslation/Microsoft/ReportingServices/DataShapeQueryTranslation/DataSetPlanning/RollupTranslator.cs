using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x0200010A RID: 266
	internal sealed class RollupTranslator : DataSetPlanElementVisitor
	{
		// Token: 0x06000A4E RID: 2638 RVA: 0x00027DF8 File Offset: 0x00025FF8
		private RollupTranslator(DataShape dataShape, DataShapeAnnotations annotations, ExpressionTable inputExpressionTable, ScopeTree scopeTree, TranslationErrorContext errorContext)
		{
			this.m_dataShape = dataShape;
			this.m_annotations = annotations;
			this.m_scopeTree = scopeTree;
			this.m_errorContext = errorContext;
			this.m_outputExpressionTable = inputExpressionTable.CopyTable();
			this.m_innermostScopeRollupReplacements = new Dictionary<Calculation, Calculation>();
			this.m_requiredCalculationsInInnermostScope = new HashSet<Calculation>();
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00027E4C File Offset: 0x0002604C
		public static RollupTranslatorResult Translate(DataShape dataShape, DataShapeAnnotations annotations, IList<DataSetPlan> dataSetPlans, ScopeTree scopeTree, TranslationErrorContext errorContext, ReadOnlyExpressionTable expressionTable)
		{
			RollupTranslator rollupTranslator = new RollupTranslator(dataShape, annotations, expressionTable, scopeTree, errorContext);
			int count = dataSetPlans.Count;
			List<DataSetPlan> list = new List<DataSetPlan>(count);
			for (int i = 0; i < count; i++)
			{
				DataSetPlan dataSetPlan = dataSetPlans[i];
				DataSetPlan dataSetPlan2 = rollupTranslator.TranslateRollup(dataSetPlan);
				list.Add(dataSetPlan2);
			}
			return new RollupTranslatorResult(list, rollupTranslator.m_outputExpressionTable.AsReadOnly());
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00027EAC File Offset: 0x000260AC
		private DataSetPlan TranslateRollup(DataSetPlan plan)
		{
			this.m_translatedScopes = new List<ScopePlanElement>(plan.Scopes);
			this.m_innermostScopeRollupReplacements.Clear();
			this.m_requiredCalculationsInInnermostScope.Clear();
			List<ScopeElementWithDistances> list = RollupTranslatorScopeSort.SortScopes(plan.Scopes, this.m_annotations, this.m_scopeTree);
			this.m_innermostProjectedScope = (from s in list
				where s.ScopeElement.IsProjected
				select s.ScopeElement.Scope).FirstOrDefault<IScope>();
			for (int i = 0; i < list.Count; i++)
			{
				this.m_currentScopeElementWithDistances = list[i];
				ScopePlanElement scopeElement = this.m_currentScopeElementWithDistances.ScopeElement;
				scopeElement.Accept(this);
				foreach (NestedPlanElement nestedPlanElement in scopeElement.NestedElements)
				{
					nestedPlanElement.Accept(this);
				}
			}
			IEnumerable<ScopeElementWithDistances> innermostScopeElements = RollupTranslator.GetInnermostScopeElements(list);
			this.EnsureRequiredCalculationsAreIncluded(innermostScopeElements);
			return new DataSetPlan(plan.Name, plan.PlanIndex, this.m_translatedScopes, plan.FilterEmptyGroups, plan.SuppressSortingAndLimits, plan.ExtensionSchema, plan.DataSourceVariables, plan.ModelParameters, plan.QueryParameters);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00028004 File Offset: 0x00026204
		private static IEnumerable<ScopeElementWithDistances> GetInnermostScopeElements(List<ScopeElementWithDistances> sortedScopesWithDistances)
		{
			int primaryDistance = sortedScopesWithDistances[0].PrimaryDistance;
			int secondaryDistance = sortedScopesWithDistances[0].SecondaryDistance;
			return sortedScopesWithDistances.TakeWhile((ScopeElementWithDistances e) => e.PrimaryDistance == primaryDistance && e.SecondaryDistance == secondaryDistance);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00028050 File Offset: 0x00026250
		private void EnsureRequiredCalculationsAreIncluded(IEnumerable<ScopeElementWithDistances> innermostScopeElements)
		{
			if (this.m_requiredCalculationsInInnermostScope.Count == 0)
			{
				return;
			}
			int num = 0;
			foreach (ScopeElementWithDistances scopeElementWithDistances in innermostScopeElements)
			{
				ScopePlanElement scopePlanElement = scopeElementWithDistances.ScopeElement.OmitNestedElements();
				ReadOnlyCollection<NestedPlanElement> nestedElements = scopeElementWithDistances.ScopeElement.NestedElements;
				for (int i = 0; i < nestedElements.Count; i++)
				{
					NestedPlanElement nestedPlanElement = nestedElements[i];
					CalculationPlanElement calculationPlanElement = nestedPlanElement as CalculationPlanElement;
					if (calculationPlanElement != null && this.m_requiredCalculationsInInnermostScope.Contains(calculationPlanElement.Calculation))
					{
						nestedPlanElement = calculationPlanElement.OmitMeasureJoinPredicateOnly();
						num++;
					}
					scopePlanElement = scopePlanElement.AddNestedPlanElement(nestedPlanElement);
				}
				this.m_translatedScopes[scopeElementWithDistances.OriginalIndexInPlan] = scopePlanElement;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0002812C File Offset: 0x0002632C
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x00028144 File Offset: 0x00026344
		private ScopePlanElement CurrentScopePlanElement
		{
			get
			{
				return this.m_translatedScopes[this.m_currentScopeElementWithDistances.OriginalIndexInPlan];
			}
			set
			{
				this.m_translatedScopes[this.m_currentScopeElementWithDistances.OriginalIndexInPlan] = value;
			}
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002815D File Offset: 0x0002635D
		protected override void DefaultVisit(ScopePlanElement planElement)
		{
			this.CurrentScopePlanElement = planElement.OmitNestedElements();
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002816C File Offset: 0x0002636C
		internal override void Visit(CalculationPlanElement planElement)
		{
			Calculation calculation = planElement.Calculation;
			ExpressionNode node = this.m_outputExpressionTable.GetNode(planElement.Calculation.Value);
			if (node.Kind == ExpressionNodeKind.Rollup)
			{
				Calculation calculation2 = ((RollupExpressionNode)node).Argument.Calculation;
				ExpressionNode expressionNode;
				if (this.m_scopeTree.AreSameScope(this.m_currentScopeElementWithDistances.ScopeElement.Scope, this.m_innermostProjectedScope))
				{
					expressionNode = this.m_outputExpressionTable.GetNode(calculation2.Value);
					if (!this.m_innermostScopeRollupReplacements.ContainsKey(calculation2))
					{
						this.m_innermostScopeRollupReplacements.Add(calculation2, calculation);
					}
				}
				else
				{
					Calculation calculation3;
					if (!this.m_innermostScopeRollupReplacements.TryGetValue(calculation2, out calculation3))
					{
						calculation3 = calculation2;
						this.m_requiredCalculationsInInnermostScope.Add(calculation2);
					}
					expressionNode = new ResolvedRollupExpressionNode(new ResolvedCalculationReferenceExpressionNode(calculation3));
				}
				this.m_outputExpressionTable.SetNode(calculation.Value, expressionNode);
			}
			this.CurrentScopePlanElement = this.CurrentScopePlanElement.AddNestedPlanElement(planElement);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00028258 File Offset: 0x00026458
		protected override void DefaultVisit(NestedPlanElement planElement)
		{
			this.CurrentScopePlanElement = this.CurrentScopePlanElement.AddNestedPlanElement(planElement);
		}

		// Token: 0x04000506 RID: 1286
		private readonly DataShape m_dataShape;

		// Token: 0x04000507 RID: 1287
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x04000508 RID: 1288
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000509 RID: 1289
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x0400050A RID: 1290
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x0400050B RID: 1291
		private readonly Dictionary<Calculation, Calculation> m_innermostScopeRollupReplacements;

		// Token: 0x0400050C RID: 1292
		private readonly HashSet<Calculation> m_requiredCalculationsInInnermostScope;

		// Token: 0x0400050D RID: 1293
		private List<ScopePlanElement> m_translatedScopes;

		// Token: 0x0400050E RID: 1294
		private IScope m_innermostProjectedScope;

		// Token: 0x0400050F RID: 1295
		private ScopeElementWithDistances m_currentScopeElementWithDistances;
	}
}
