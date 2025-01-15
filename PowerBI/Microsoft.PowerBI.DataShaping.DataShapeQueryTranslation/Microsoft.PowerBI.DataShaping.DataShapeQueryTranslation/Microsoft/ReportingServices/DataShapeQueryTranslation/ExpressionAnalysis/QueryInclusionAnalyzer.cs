using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis
{
	// Token: 0x020000B5 RID: 181
	internal sealed class QueryInclusionAnalyzer
	{
		// Token: 0x060007DD RID: 2013 RVA: 0x0001E485 File Offset: 0x0001C685
		private QueryInclusionAnalyzer(ExpressionTable expressionTable)
		{
			this.m_allNodesProcessingSpecific = true;
			this.m_expressionTable = expressionTable;
			this.m_visitedNodes = new HashSet<ExpressionNode>();
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001E4A6 File Offset: 0x0001C6A6
		public static bool CanBeHandledByProcessing(ExpressionNode node, ExpressionTable expressionTable)
		{
			QueryInclusionAnalyzer queryInclusionAnalyzer = new QueryInclusionAnalyzer(expressionTable);
			queryInclusionAnalyzer.Visit(node);
			return queryInclusionAnalyzer.m_allNodesProcessingSpecific;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001E4BC File Offset: 0x0001C6BC
		internal void Visit(ExpressionNode node)
		{
			this.m_visitedNodes.Add(node);
			ExpressionNodeKind kind = node.Kind;
			if (kind <= ExpressionNodeKind.Literal)
			{
				if (kind == ExpressionNodeKind.FunctionCall)
				{
					this.Visit((FunctionCallExpressionNode)node);
					return;
				}
				if (kind == ExpressionNodeKind.Literal)
				{
					return;
				}
			}
			else
			{
				if (kind == ExpressionNodeKind.ResolvedCalculationReference)
				{
					this.Visit((ResolvedCalculationReferenceExpressionNode)node);
					return;
				}
				if (kind == ExpressionNodeKind.ResolvedLimitReference)
				{
					return;
				}
			}
			this.m_allNodesProcessingSpecific = false;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001E51C File Offset: 0x0001C71C
		internal void Visit(FunctionCallExpressionNode node)
		{
			this.m_allNodesProcessingSpecific &= node.Descriptor.CanBeHandledByProcessing;
			foreach (ExpressionNode expressionNode in node.Arguments)
			{
				this.Visit(expressionNode);
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001E584 File Offset: 0x0001C784
		internal void Visit(ResolvedCalculationReferenceExpressionNode node)
		{
			ExpressionNode node2 = this.m_expressionTable.GetNode(node.Calculation.Value.ExpressionId.Value);
			if (this.m_visitedNodes.Contains(node2))
			{
				this.m_allNodesProcessingSpecific = false;
				return;
			}
			this.Visit(node2);
		}

		// Token: 0x040003E3 RID: 995
		private bool m_allNodesProcessingSpecific;

		// Token: 0x040003E4 RID: 996
		private ExpressionTable m_expressionTable;

		// Token: 0x040003E5 RID: 997
		private HashSet<ExpressionNode> m_visitedNodes;
	}
}
