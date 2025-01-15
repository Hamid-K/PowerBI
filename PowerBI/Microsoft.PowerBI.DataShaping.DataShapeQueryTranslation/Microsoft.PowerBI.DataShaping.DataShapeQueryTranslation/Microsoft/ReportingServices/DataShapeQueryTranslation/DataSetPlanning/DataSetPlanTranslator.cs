using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000FD RID: 253
	internal sealed class DataSetPlanTranslator
	{
		// Token: 0x060009F7 RID: 2551 RVA: 0x000262D9 File Offset: 0x000244D9
		private DataSetPlanTranslator(ExpressionTable inputExpressionTable, ScopeTree scopeTree, IFederatedConceptualSchema schema, DataShapeAnnotations annotations, TranslationErrorContext errorContext, IReadOnlyDictionary<IIdentifiable, List<Filter>> translatedFilterTable)
		{
			this.m_inputExpressionTable = inputExpressionTable;
			this.m_scopeTree = scopeTree;
			this.m_schema = schema;
			this.m_annotations = annotations;
			this.m_errorContext = errorContext;
			this.m_translatedFilterTable = translatedFilterTable;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00026310 File Offset: 0x00024510
		public static ReadOnlyCollection<DataSetPlan> Translate(ExpressionTable inputExpressionTable, ScopeTree scopeTree, IFederatedConceptualSchema schema, DataShapeAnnotations annotations, TranslationErrorContext errorContext, List<DataSetPlanInfo> planInfos, OutputPlanMapping outputPlanMapping, bool filterEmptyGroups, Func<string> makePlanName, IReadOnlyDictionary<IIdentifiable, List<Filter>> translatedFilterTable, ExtensionSchema extensionSchema, string dataSourceVariables, IReadOnlyList<ModelParameter> modelParameters, IReadOnlyList<QueryParameterDeclaration> queryParameters)
		{
			return new DataSetPlanTranslator(inputExpressionTable, scopeTree, schema, annotations, errorContext, translatedFilterTable).TranslateToPlans(planInfos, outputPlanMapping, filterEmptyGroups, makePlanName, extensionSchema, dataSourceVariables, modelParameters, queryParameters).ToReadOnlyCollection<DataSetPlan>();
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00026344 File Offset: 0x00024544
		private List<DataSetPlan> TranslateToPlans(List<DataSetPlanInfo> planInfos, OutputPlanMapping outputPlanMapping, bool filterEmptyGroups, Func<string> makePlanName, ExtensionSchema extensionSchema, string dataSourceVariables, IReadOnlyList<ModelParameter> modelParameters, IReadOnlyList<QueryParameterDeclaration> queryParameters)
		{
			List<DataSetPlan> list = new List<DataSetPlan>(planInfos.Count);
			using (List<DataSetPlanInfo>.Enumerator enumerator = planInfos.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DataSetPlanInfo planInfo = enumerator.Current;
					DataSetPlan dataSetPlan = this.TranslateToPlan(planInfo, list.Count, filterEmptyGroups, (outputPlanMapping != null) ? (() => this.CreatePlanName(planInfo, outputPlanMapping)) : makePlanName, extensionSchema, dataSourceVariables, modelParameters, queryParameters);
					list.Add(dataSetPlan);
				}
			}
			return list;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00026400 File Offset: 0x00024600
		public static DataSetPlan Translate(ExpressionTable inputExpressionTable, ScopeTree scopeTree, IFederatedConceptualSchema schema, DataShapeAnnotations annotations, TranslationErrorContext errorContext, DataSetPlanInfo planInfo, int planIndex, bool filterEmptyGroups, Func<string> makePlanName, IReadOnlyDictionary<IIdentifiable, List<Filter>> translatedFilterTable, ExtensionSchema extensionSchema, string dataSourceVariables, IReadOnlyList<ModelParameter> modelParameters, IReadOnlyList<QueryParameterDeclaration> queryParameters)
		{
			return new DataSetPlanTranslator(inputExpressionTable, scopeTree, schema, annotations, errorContext, translatedFilterTable).TranslateToPlan(planInfo, planIndex, filterEmptyGroups, makePlanName, extensionSchema, dataSourceVariables, modelParameters, queryParameters);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00026430 File Offset: 0x00024630
		private DataSetPlan TranslateToPlan(DataSetPlanInfo planInfo, int planIndex, bool filterEmptyGroups, Func<string> makePlanName, ExtensionSchema extensionSchema, string dataSourceVariables, IReadOnlyList<ModelParameter> modelParameters, IReadOnlyList<QueryParameterDeclaration> queryParameters)
		{
			List<ScopePlanElement> list = new List<ScopePlanElement>();
			foreach (ContextElement contextElement in planInfo.Elements)
			{
				IScope scope = contextElement.Content as IScope;
				if (scope != null)
				{
					List<NestedPlanElement> nestedPlanElementsForScope = this.GetNestedPlanElementsForScope(planInfo, scope);
					ScopePlanElement scopePlanElement = this.CreateScopePlanElement(contextElement, nestedPlanElementsForScope);
					list.Add(scopePlanElement);
				}
			}
			return new DataSetPlan(makePlanName(), planIndex, list, filterEmptyGroups, false, extensionSchema, dataSourceVariables, modelParameters, queryParameters);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000264C8 File Offset: 0x000246C8
		private List<NestedPlanElement> GetNestedPlanElementsForScope(DataSetPlanInfo planInfo, IScope scope)
		{
			ReadOnlyCollection<Calculation> items = this.m_scopeTree.GetItems<Calculation>(scope.Id);
			List<NestedPlanElement> list = new List<NestedPlanElement>();
			foreach (ContextElement contextElement in planInfo.Elements)
			{
				Calculation calculation = contextElement.Content as Calculation;
				if (calculation != null && items.Contains(calculation))
				{
					bool flag = contextElement.ElementState.ShouldIncludeInQueryOutput();
					if (flag || !calculation.SuppressJoinPredicate.GetValueOrDefault<bool>())
					{
						CalculationPlanElement calculationPlanElement = new CalculationPlanElement(calculation, this.m_annotations.NeededForQueryCalculationContext(calculation), !flag);
						list.Add(calculationPlanElement);
					}
				}
			}
			return list;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0002658C File Offset: 0x0002478C
		private ScopePlanElement CreateScopePlanElement(ContextElement scopeElement, List<NestedPlanElement> nestedPlanElements)
		{
			IScope scope = (IScope)scopeElement.Content;
			this.m_annotations.GetContainingDataShape(scope);
			bool flag = scopeElement.ElementState.ShouldIncludeInQueryOutput();
			Filter filter = this.m_annotations.GetFilter(scope, this.m_translatedFilterTable);
			switch (scopeElement.Content.ObjectType)
			{
			case ObjectType.DataIntersection:
			{
				FilterCondition filterCondition = null;
				Limit limit = null;
				if (this.m_scopeTree.GetCanonicalScope(scope.Id) == scope)
				{
					filterCondition = ((filter != null) ? filter.Condition : null);
					limit = scopeElement.Limit;
				}
				return new DataIntersectionPlanElement((DataIntersection)scope, nestedPlanElements, flag, filterCondition, limit);
			}
			case ObjectType.DataMember:
			{
				RollupRequirement rollupRequirement = this.ComputeRollupRequirement(scopeElement);
				bool flag2 = scopeElement.ElementState == ContextState.SynchronizationTarget;
				Limit limit2 = scopeElement.Limit;
				return new DataMemberPlanElement((DataMember)scope, nestedPlanElements, flag, rollupRequirement, (filter != null) ? filter.Condition : null, limit2, scopeElement.RequiresReversedSortDirection, flag2);
			}
			case ObjectType.DataShape:
			{
				scopeElement.GetRollupSortDirection(this.m_errorContext, this.m_annotations);
				DataShape dataShape = (DataShape)scope;
				return new DataShapePlanElement(dataShape, nestedPlanElements, flag, (filter != null) ? filter.Condition : null, null, this.GetApplyFilterPlans(dataShape), this.GetDataShapeValueFilter(dataShape), this.m_annotations.GetAnyValueFilters(dataShape), this.m_annotations.GetDefaultValueFilter(dataShape));
			}
			}
			Microsoft.DataShaping.Contract.RetailFail("Unexpected ObjectType: {0}", scopeElement.Content.ObjectType);
			throw new InvalidOperationException("Unexpected ObjectType");
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00026704 File Offset: 0x00024904
		private FilterCondition GetDataShapeValueFilter(DataShape dataShape)
		{
			foreach (Filter filter in this.m_annotations.GetDataShapeAnnotation(dataShape).DataShapeValueFilters)
			{
				ResolvedScopeReferenceExpressionNode resolvedScopeReferenceExpressionNode = (ResolvedScopeReferenceExpressionNode)this.m_inputExpressionTable.GetNode(filter.Target);
				if (this.m_scopeTree.AreSameScope(resolvedScopeReferenceExpressionNode.Scope, dataShape))
				{
					return filter.Condition;
				}
			}
			return null;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0002678C File Offset: 0x0002498C
		private IReadOnlyList<DataSetPlan> GetApplyFilterPlans(DataShape dataShape)
		{
			IReadOnlyList<ApplyFilterCondition> applyFilters = this.m_annotations.GetDataShapeAnnotation(dataShape).ApplyFilters;
			if (applyFilters.IsNullOrEmpty<ApplyFilterCondition>())
			{
				return Microsoft.DataShaping.Util.EmptyReadOnlyCollection<DataSetPlan>();
			}
			List<DataSetPlan> list = new List<DataSetPlan>(applyFilters.Count);
			foreach (ApplyFilterCondition applyFilterCondition in applyFilters)
			{
				DataSetPlanningResult dataSetPlanningResult = DataSetPlanner.DeterminePlans(applyFilterCondition.GetApplyFilterDataShape(this.m_inputExpressionTable), this.m_inputExpressionTable, this.m_annotations, this.m_scopeTree, this.m_schema, this.m_errorContext);
				Microsoft.DataShaping.Contract.RetailAssert(dataSetPlanningResult.SubQueryPlans.IsNullOrEmpty<DataSetPlan>(), "Apply filters cannot generate subqueries");
				Microsoft.DataShaping.Contract.RetailAssert(dataSetPlanningResult.DataSetPlans.Count == 1, "Apply filters should translate into a single DataSetPlan");
				list.Add(dataSetPlanningResult.DataSetPlans.Single<DataSetPlan>());
			}
			return list;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00026868 File Offset: 0x00024A68
		private RollupRequirement ComputeRollupRequirement(ContextElement scopeElement)
		{
			bool flag = scopeElement.ElementState == ContextState.OutputRollup;
			SortDirection rollupSortDirection = scopeElement.GetRollupSortDirection(this.m_errorContext, this.m_annotations);
			return new RollupRequirement(flag, rollupSortDirection);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00026898 File Offset: 0x00024A98
		private string CreatePlanName(DataSetPlanInfo plan, OutputPlanMapping outputPlanMapping)
		{
			foreach (ContextElement contextElement in plan.Elements)
			{
				int num;
				if (outputPlanMapping.TryGetValue(contextElement.Content, out num) && num == plan.PlanIndex)
				{
					return this.CreatePlanName(contextElement.Content);
				}
			}
			Microsoft.DataShaping.Contract.RetailFail("Plan did not contain an output item");
			throw new InvalidOperationException("Plan did not contain an output item");
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00026924 File Offset: 0x00024B24
		private string CreatePlanName(IContextItem item)
		{
			if (item is IScope)
			{
				return item.Id.Value;
			}
			Calculation calculation = item as Calculation;
			Microsoft.DataShaping.Contract.RetailAssert(calculation != null, "Plans can only be anchored at Scopes or Calculations.");
			string text = "Aggregates";
			this.m_inputExpressionTable.GetNode(calculation.Value);
			if (this.m_annotations.IsSubtotal(calculation))
			{
				text = "Totals";
			}
			return this.m_scopeTree.GetContainingScope(calculation).Id.Value + text;
		}

		// Token: 0x040004C7 RID: 1223
		private const string AggregatesPlanNameSuffix = "Aggregates";

		// Token: 0x040004C8 RID: 1224
		private const string TotalsPlanNameSuffix = "Totals";

		// Token: 0x040004C9 RID: 1225
		private readonly ExpressionTable m_inputExpressionTable;

		// Token: 0x040004CA RID: 1226
		private readonly ScopeTree m_scopeTree;

		// Token: 0x040004CB RID: 1227
		private readonly IFederatedConceptualSchema m_schema;

		// Token: 0x040004CC RID: 1228
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x040004CD RID: 1229
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x040004CE RID: 1230
		private readonly IReadOnlyDictionary<IIdentifiable, List<Filter>> m_translatedFilterTable;
	}
}
