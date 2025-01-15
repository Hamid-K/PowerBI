using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000F2 RID: 242
	internal sealed class DataSetPlannerExpressionTranslator : DataShapeVisitor
	{
		// Token: 0x060009B3 RID: 2483 RVA: 0x00025128 File Offset: 0x00023328
		private DataSetPlannerExpressionTranslator(ScopeTree scopeTree, IFederatedConceptualSchema schema, DataShapeAnnotations annotations, TranslationErrorContext errorContext, ExpressionTable inputExpressionTable, ContextGraph contextGraph, ContextWeights contextWeights, IReadOnlyDictionary<IIdentifiable, List<Filter>> translatedFilterTable, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery)
		{
			this.m_scopeTree = scopeTree;
			this.m_schema = schema;
			this.m_annotations = annotations;
			this.m_errorContext = errorContext;
			this.m_inputExpressionTable = inputExpressionTable;
			this.m_outputExpressionTable = this.m_inputExpressionTable.CopyTable();
			this.m_subQueriesByName = new Dictionary<string, DataSetPlan>(StringComparer.Ordinal);
			this.m_subQueryPlans = new List<DataSetPlan>();
			this.m_activeDataShapes = new Stack<DataShape>();
			this.m_contextGraph = contextGraph;
			this.m_contextWeights = contextWeights;
			this.m_translatedFilterTable = translatedFilterTable;
			this.m_transformReferenceMap = transformReferenceMap;
			this.m_applyTransformsInQuery = applyTransformsInQuery;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x000251BF File Offset: 0x000233BF
		private ReadOnlyExpressionTable OutputExpressionTable
		{
			get
			{
				return this.m_outputExpressionTable.AsReadOnly();
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x000251CC File Offset: 0x000233CC
		private List<DataSetPlan> SubQueryPlans
		{
			get
			{
				return this.m_subQueryPlans;
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x000251D4 File Offset: 0x000233D4
		public static DataSetPlannerExpressionTranslationResult Translate(ScopeTree scopeTree, IFederatedConceptualSchema schema, DataShapeAnnotations annotations, TranslationErrorContext errorContext, ExpressionTable inputExpressionTable, ContextGraph contextGraph, ContextWeights contextWeights, DataShape dataShape, IReadOnlyDictionary<IIdentifiable, List<Filter>> translatedFilterTable, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery)
		{
			DataSetPlannerExpressionTranslator dataSetPlannerExpressionTranslator = new DataSetPlannerExpressionTranslator(scopeTree, schema, annotations, errorContext, inputExpressionTable, contextGraph, contextWeights, translatedFilterTable, transformReferenceMap, applyTransformsInQuery);
			dataSetPlannerExpressionTranslator.Visit(dataShape);
			return new DataSetPlannerExpressionTranslationResult(dataSetPlannerExpressionTranslator.OutputExpressionTable, dataSetPlannerExpressionTranslator.SubQueryPlans);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00025210 File Offset: 0x00023410
		protected override void Enter(DataMember dataMember)
		{
			if (!dataMember.IsDynamic)
			{
				return;
			}
			List<SortKey> sortKeys = dataMember.Group.SortKeys;
			if (sortKeys == null)
			{
				return;
			}
			ScopeIdDefinition scopeIdDefinition = dataMember.Group.ScopeIdDefinition;
			for (int i = 0; i < sortKeys.Count; i++)
			{
				SortKey sortKey = sortKeys[i];
				this.TransformSortExpression(sortKey, dataMember, sortKey.Value);
				if (scopeIdDefinition != null)
				{
					this.TransformSortExpression(sortKey, dataMember, scopeIdDefinition.Values[i].Value);
				}
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00025288 File Offset: 0x00023488
		private void TransformSortExpression(SortKey sortKey, IScope containingScope, Expression expression)
		{
			ExpressionNode expressionNode = DataSetPlannerFilterExpressionTreeTranslator.Translate(new ExpressionContext(this.m_errorContext, sortKey.ObjectType, sortKey.Id, "Value"), this.m_outputExpressionTable.GetNode(expression), this.m_scopeTree, this.m_annotations, containingScope, this.m_outputExpressionTable, this.m_transformReferenceMap, this.m_applyTransformsInQuery);
			this.m_outputExpressionTable.SetNode(expression, expressionNode);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000252F0 File Offset: 0x000234F0
		protected override void Visit(Calculation calculation)
		{
			IScope containingScope = this.m_scopeTree.GetContainingScope(calculation);
			ExpressionNode node = this.m_inputExpressionTable.GetNode(calculation.Value);
			ExpressionContext expressionContext = new ExpressionContext(this.m_errorContext, calculation.ObjectType, calculation.Id, "Value");
			ExpressionNode expressionNode = this.TranslateNode(expressionContext, containingScope, node);
			if (expressionNode != null)
			{
				DataShape dataShape = this.m_activeDataShapes.Peek();
				expressionNode = CalculationFilterTransform.TranslateToInlineFilter(expressionNode, this.m_inputExpressionTable, calculation, dataShape);
				string text;
				DataSetPlan filterForCalculation = this.GetFilterForCalculation(containingScope, calculation, expressionNode, out text);
				if (filterForCalculation != null)
				{
					ExpressionNode expressionNode2 = ExprNodes.TableSubQuery(filterForCalculation, calculation, text);
					expressionNode = ExprNodes.Filter(calculation, null, expressionNode, expressionNode2);
				}
				this.m_outputExpressionTable.SetNode(calculation.Value, expressionNode);
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000253A0 File Offset: 0x000235A0
		private DataSetPlan GetFilterForCalculation(IScope scope, Calculation calculation, ExpressionNode node, out string subqueryFilterName)
		{
			subqueryFilterName = null;
			if (!calculation.IsDataShapeLevelProjection(this.m_annotations, scope, node))
			{
				return null;
			}
			DataShape dataShape = scope as DataShape;
			IReadOnlyList<Filter> scopedValueFilters = this.m_annotations.GetDataShapeAnnotation(dataShape).ScopedValueFilters;
			if (scopedValueFilters.IsNullOrEmpty<Filter>())
			{
				return null;
			}
			ExpressionContext expressionContext = new ExpressionContext(this.m_errorContext, calculation.ObjectType, calculation.Id, "Value");
			IScope resolvedScope = scopedValueFilters.Single("Expected only 1 ValueFilter", Array.Empty<string>()).Target.GetResolvedScope(this.m_inputExpressionTable);
			subqueryFilterName = "ValueFilter" + resolvedScope.Id.Value;
			DataSetPlan dataSetPlan;
			if (this.m_subQueriesByName.TryGetValue(subqueryFilterName, out dataSetPlan))
			{
				return dataSetPlan;
			}
			DataSetPlan dataSetPlan2 = SubQueryDataSetPlanner.BuildSubQueryDataSetPlan(this.m_inputExpressionTable, this.m_scopeTree, this.m_schema, this.m_annotations, this.m_errorContext, this.m_contextGraph, this.m_contextWeights, expressionContext, scope, resolvedScope, null, true, resolvedScope, this.m_translatedFilterTable, EvaluationContextBuilderOptions.ContextOnlyChildrenMarked);
			this.m_subQueryPlans.Add(dataSetPlan2);
			this.m_subQueriesByName.Add(subqueryFilterName, dataSetPlan2);
			return dataSetPlan2;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x000254B4 File Offset: 0x000236B4
		protected override void Visit(Filter filter, Identifier dataShapeId)
		{
			DataSetPlannerFilterExpressionTranslator.Translate(new DataSetPlannerFilterExpressionTreeTranslator(this.m_scopeTree, this.m_annotations, this.m_inputExpressionTable, this.m_transformReferenceMap, this.m_applyTransformsInQuery), filter, this.m_errorContext, this.m_inputExpressionTable, this.m_outputExpressionTable, this.m_scopeTree);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00025504 File Offset: 0x00023704
		private void TranslateTargetExpression(Expression target, Identifier parentIdentifier, ObjectType objectType)
		{
			ExpressionNode node = this.m_inputExpressionTable.GetNode(target);
			ResolvedCalculationReferenceExpressionNode resolvedCalculationReferenceExpressionNode = (ResolvedCalculationReferenceExpressionNode)node;
			IScope containingScope = this.m_scopeTree.GetContainingScope(resolvedCalculationReferenceExpressionNode.Calculation);
			ExpressionContext expressionContext = new ExpressionContext(this.m_errorContext, objectType, parentIdentifier, "Value");
			ExpressionNode expressionNode = this.TranslateNode(expressionContext, containingScope, node);
			this.m_outputExpressionTable.SetNode(target, expressionNode);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00025563 File Offset: 0x00023763
		protected override void Enter(DataShape dataShape)
		{
			this.m_activeDataShapes.Push(dataShape);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00025571 File Offset: 0x00023771
		protected override void Exit(DataShape dataShape)
		{
			this.m_activeDataShapes.Pop();
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00025580 File Offset: 0x00023780
		private ExpressionNode TranslateNode(ExpressionContext context, IScope containingScope, ExpressionNode inputNode)
		{
			DataShape dataShape = this.m_activeDataShapes.Peek();
			List<DataSetPlan> list;
			ExpressionNode expressionNode = DataSetPlannerExpressionTreeTranslator.Translate(context, dataShape, this.m_scopeTree, this.m_schema, this.m_annotations, containingScope, inputNode, this.m_inputExpressionTable, this.m_outputExpressionTable, this.m_contextGraph, this.m_contextWeights, this.m_translatedFilterTable, this.m_transformReferenceMap, this.m_applyTransformsInQuery, out list);
			if (list != null)
			{
				this.m_subQueryPlans.AddRange(list);
			}
			return expressionNode;
		}

		// Token: 0x0400049C RID: 1180
		private readonly ScopeTree m_scopeTree;

		// Token: 0x0400049D RID: 1181
		private readonly IFederatedConceptualSchema m_schema;

		// Token: 0x0400049E RID: 1182
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x0400049F RID: 1183
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x040004A0 RID: 1184
		private readonly ExpressionTable m_inputExpressionTable;

		// Token: 0x040004A1 RID: 1185
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x040004A2 RID: 1186
		private readonly List<DataSetPlan> m_subQueryPlans;

		// Token: 0x040004A3 RID: 1187
		private readonly Dictionary<string, DataSetPlan> m_subQueriesByName;

		// Token: 0x040004A4 RID: 1188
		private readonly Stack<DataShape> m_activeDataShapes;

		// Token: 0x040004A5 RID: 1189
		private readonly ContextGraph m_contextGraph;

		// Token: 0x040004A6 RID: 1190
		private readonly ContextWeights m_contextWeights;

		// Token: 0x040004A7 RID: 1191
		private readonly IReadOnlyDictionary<IIdentifiable, List<Filter>> m_translatedFilterTable;

		// Token: 0x040004A8 RID: 1192
		private readonly DataTransformReferenceMap m_transformReferenceMap;

		// Token: 0x040004A9 RID: 1193
		private readonly bool m_applyTransformsInQuery;
	}
}
