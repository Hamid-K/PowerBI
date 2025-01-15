using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000F4 RID: 244
	internal sealed class DataSetPlannerExpressionTreeTranslator : DataSetPlannerExpressionTreeTranslatorBase
	{
		// Token: 0x060009C3 RID: 2499 RVA: 0x00025618 File Offset: 0x00023818
		private DataSetPlannerExpressionTreeTranslator(ExpressionContext context, DataShape dataShape, ScopeTree scopeTree, IFederatedConceptualSchema schema, DataShapeAnnotations annotations, IScope containingScope, ExpressionTable inputExpressionTable, WritableExpressionTable outputExpressionTable, ContextGraph contextGraph, ContextWeights contextWeights, IReadOnlyDictionary<IIdentifiable, List<Filter>> translatedFilterTable, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery)
			: base(scopeTree, annotations, inputExpressionTable, transformReferenceMap, applyTransformsInQuery, context, containingScope)
		{
			this.m_schema = schema;
			this.m_outputExpressionTable = outputExpressionTable;
			this.m_dataShape = dataShape;
			this.m_contextGraph = contextGraph;
			this.m_contextWeights = contextWeights;
			this.m_translatedFilterTable = translatedFilterTable;
			this.m_subQueryPlans = new List<DataSetPlan>();
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00025671 File Offset: 0x00023871
		private List<DataSetPlan> SubQueryPlans
		{
			get
			{
				return this.m_subQueryPlans;
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0002567C File Offset: 0x0002387C
		public static ExpressionNode Translate(ExpressionContext context, DataShape dataShape, ScopeTree scopeTree, IFederatedConceptualSchema schema, DataShapeAnnotations annotations, IScope containingScope, ExpressionNode node, ExpressionTable inputExpressionTable, WritableExpressionTable outputExpressionTable, ContextGraph contextGraph, ContextWeights contextWeights, IReadOnlyDictionary<IIdentifiable, List<Filter>> translatedFilterTable, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery, out List<DataSetPlan> subQueryPlans)
		{
			DataSetPlannerExpressionTreeTranslator dataSetPlannerExpressionTreeTranslator = new DataSetPlannerExpressionTreeTranslator(context, dataShape, scopeTree, schema, annotations, containingScope, inputExpressionTable, outputExpressionTable, contextGraph, contextWeights, translatedFilterTable, transformReferenceMap, applyTransformsInQuery);
			ExpressionNode expressionNode = dataSetPlannerExpressionTreeTranslator.Visit(node);
			subQueryPlans = dataSetPlannerExpressionTreeTranslator.SubQueryPlans;
			return expressionNode;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x000256B8 File Offset: 0x000238B8
		public override ExpressionNode Visit(FunctionCallExpressionNode node)
		{
			ExpressionNode expressionNode;
			if (this.TranslateToFlatAggregate(node, this.m_containingScope, out expressionNode))
			{
				return expressionNode;
			}
			return base.Visit(node);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x000256E0 File Offset: 0x000238E0
		private bool TranslateToFlatAggregate(FunctionCallExpressionNode node, IScope containingScope, out ExpressionNode result)
		{
			if (node.Descriptor.FunctionCategory == FunctionCategory.Aggregate && node.Arguments.Count == 1)
			{
				ResolvedCalculationReferenceExpressionNode resolvedCalculationReferenceExpressionNode = node.Arguments[0] as ResolvedCalculationReferenceExpressionNode;
				if (resolvedCalculationReferenceExpressionNode != null)
				{
					IScope containingScope2 = this.m_scopeTree.GetContainingScope(resolvedCalculationReferenceExpressionNode.Calculation);
					if (this.m_scopeTree.AreSameScope(containingScope2, containingScope))
					{
						result = AggregateExpressionFlattener.Rewrite(node);
						return true;
					}
				}
			}
			result = null;
			return false;
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002574C File Offset: 0x0002394C
		private bool AllArgumentsOfKind(FunctionCallExpressionNode node, ExpressionNodeKind kind)
		{
			return node.Arguments.All((ExpressionNode argument) => argument.Kind == kind);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00025780 File Offset: 0x00023980
		protected override ExpressionNode TranslateEvaluateExpression(FunctionCallExpressionNode node)
		{
			ExpressionNode expressionNode = node.Arguments[1];
			if (expressionNode.Kind == ExpressionNodeKind.FunctionCall)
			{
				FunctionCallExpressionNode functionCallExpressionNode = (FunctionCallExpressionNode)expressionNode;
				if (functionCallExpressionNode.Descriptor.Name == "Scope")
				{
					return base.TranslateEvaluateWithScopeFunction(node, functionCallExpressionNode);
				}
			}
			return this.TranslateEvaluateWithScopeExpression(node);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x000257D4 File Offset: 0x000239D4
		private ExpressionNode TranslateEvaluateWithScopeExpression(FunctionCallExpressionNode node)
		{
			ExpressionNode expressionNode = node.Arguments[0];
			IScope scope = null;
			IScope scope2 = null;
			ExpressionNodeKind kind = node.Arguments[1].Kind;
			if (kind != ExpressionNodeKind.FunctionCall)
			{
				if (kind == ExpressionNodeKind.ResolvedScopeReference)
				{
					scope = ((ResolvedScopeReferenceExpressionNode)node.Arguments[1]).Scope;
				}
			}
			else
			{
				FunctionCallExpressionNode functionCallExpressionNode = (FunctionCallExpressionNode)node.Arguments[1];
				if (functionCallExpressionNode.Descriptor.Name == "Rollup")
				{
					scope2 = ((ResolvedScopeReferenceExpressionNode)functionCallExpressionNode.Arguments[0]).Scope;
					scope = ((ResolvedScopeReferenceExpressionNode)functionCallExpressionNode.Arguments[1]).Scope;
				}
			}
			ExpressionNode expressionNode2 = base.VisitNodeInScope(expressionNode, scope);
			SubExpressionNode subExpressionNode = this.m_outputExpressionTable.CreateSubExpression(expressionNode2);
			FunctionCallExpressionNode functionCallExpressionNode2 = base.ParentNodes.Skip(1).FirstOrDefault<ExpressionNode>() as FunctionCallExpressionNode;
			bool flag = functionCallExpressionNode2 == null || functionCallExpressionNode2.Descriptor.FunctionCategory != FunctionCategory.Aggregate || !functionCallExpressionNode2.Descriptor.IgnoresNulls;
			DataSetPlan dataSetPlan = this.BuildSubQueryDataSetPlan(scope, this.m_containingScope, scope2, flag, scope, this.m_translatedFilterTable);
			if (dataSetPlan == null)
			{
				return node;
			}
			ExpressionPlanElement expressionPlanElement = new ExpressionPlanElement(subExpressionNode.ExpressionId, this.m_context, true);
			dataSetPlan = dataSetPlan.AddExpression(expressionPlanElement, scope);
			this.m_subQueryPlans.Add(dataSetPlan);
			return new AggregatableSubQueryExpressionNode(subExpressionNode.ExpressionId, dataSetPlan, null);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00025936 File Offset: 0x00023B36
		protected override ExpressionNode TranslateSubtotalExpression(FunctionCallExpressionNode node)
		{
			return new RollupExpressionNode(new ResolvedCalculationReferenceExpressionNode(((ResolvedCalculationReferenceExpressionNode)node.Arguments[0]).Calculation));
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00025958 File Offset: 0x00023B58
		public override ExpressionNode Visit(ResolvedCalculationReferenceExpressionNode node)
		{
			IScope containingScope = this.m_scopeTree.GetContainingScope(node.Calculation);
			if (SubQueryDataSetPlanner.SatisfiesSubQueryGenerationConstraints(this.m_containingScope, containingScope, null, this.m_scopeTree, this.m_context, this.m_inputExpressionTable))
			{
				DataSetPlan dataSetPlan = this.BuildSubQueryDataSetPlan(containingScope, this.m_containingScope, null, true, node.Calculation, this.m_translatedFilterTable);
				if (dataSetPlan != null)
				{
					this.m_subQueryPlans.Add(dataSetPlan);
					return new AggregatableSubQueryExpressionNode(node.Calculation.Value.ExpressionId.Value, dataSetPlan, null);
				}
			}
			return node;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x000259E4 File Offset: 0x00023BE4
		private DataSetPlan BuildSubQueryDataSetPlan(IScope innerScope, IScope outerScope, IScope rollupStartScope, bool filterEmptyGroups, IContextItem contextItem, IReadOnlyDictionary<IIdentifiable, List<Filter>> translatedFilterTable)
		{
			return SubQueryDataSetPlanner.BuildSubQueryDataSetPlan(this.m_inputExpressionTable, this.m_scopeTree, this.m_schema, this.m_annotations, this.m_context.ErrorContext, this.m_contextGraph, this.m_contextWeights, this.m_context, outerScope, innerScope, rollupStartScope, filterEmptyGroups, contextItem, translatedFilterTable, EvaluationContextBuilderOptions.Default);
		}

		// Token: 0x040004AC RID: 1196
		private readonly IFederatedConceptualSchema m_schema;

		// Token: 0x040004AD RID: 1197
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x040004AE RID: 1198
		private readonly DataShape m_dataShape;

		// Token: 0x040004AF RID: 1199
		private readonly ContextGraph m_contextGraph;

		// Token: 0x040004B0 RID: 1200
		private readonly ContextWeights m_contextWeights;

		// Token: 0x040004B1 RID: 1201
		private readonly IReadOnlyDictionary<IIdentifiable, List<Filter>> m_translatedFilterTable;

		// Token: 0x040004B2 RID: 1202
		private List<DataSetPlan> m_subQueryPlans;
	}
}
