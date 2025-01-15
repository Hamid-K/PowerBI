using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning
{
	// Token: 0x02000129 RID: 297
	internal class DataSetPlannerFilterExpressionTreeTranslatorBase : DataSetPlannerExpressionTreeTranslatorBase
	{
		// Token: 0x06000B2B RID: 2859 RVA: 0x0002BC4D File Offset: 0x00029E4D
		protected DataSetPlannerFilterExpressionTreeTranslatorBase(ScopeTree scopeTree, DataShapeAnnotations annotations, ExpressionTable expressionTable, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery)
			: base(scopeTree, annotations, expressionTable, transformReferenceMap, applyTransformsInQuery)
		{
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002BC5C File Offset: 0x00029E5C
		public ExpressionNode Translate(ExpressionNode node, ExpressionContext context, IScope containingScope)
		{
			this.m_context = context;
			this.m_containingScope = containingScope;
			return this.Visit(node);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002BC74 File Offset: 0x00029E74
		protected override ExpressionNode TranslateEvaluateExpression(FunctionCallExpressionNode node)
		{
			FunctionCallExpressionNode functionCallExpressionNode = node.Arguments[1] as FunctionCallExpressionNode;
			if (functionCallExpressionNode != null)
			{
				if (functionCallExpressionNode.Descriptor.Name == "Scope")
				{
					return base.TranslateEvaluateWithScopeFunction(node, functionCallExpressionNode);
				}
				if (functionCallExpressionNode.Descriptor.Name == "Rollup")
				{
					IScope scope = ((ResolvedScopeReferenceExpressionNode)functionCallExpressionNode.Arguments[0]).Scope;
					IScope scope2 = ((ResolvedScopeReferenceExpressionNode)functionCallExpressionNode.Arguments[1]).Scope;
					ExpressionNode expressionNode = node.Arguments[0];
					ExpressionNode expressionNode2 = base.VisitNodeInScope(expressionNode, scope);
					if (expressionNode != expressionNode2)
					{
						List<ExpressionNode> list = new List<ExpressionNode>(2);
						list.Add(expressionNode2);
						list.Add(functionCallExpressionNode);
						return new FunctionCallExpressionNode(node.Descriptor, node.UsageKind, list);
					}
					return node;
				}
			}
			return base.TranslateEvaluateExpression(node);
		}
	}
}
