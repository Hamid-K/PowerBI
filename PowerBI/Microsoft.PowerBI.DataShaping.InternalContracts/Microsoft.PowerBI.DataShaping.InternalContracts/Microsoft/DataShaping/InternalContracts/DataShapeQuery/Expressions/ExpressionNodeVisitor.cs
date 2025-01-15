using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000D0 RID: 208
	internal abstract class ExpressionNodeVisitor<TResult> : IExpressionNodeVisitor<TResult>
	{
		// Token: 0x060005C4 RID: 1476 RVA: 0x0000B7A2 File Offset: 0x000099A2
		protected ExpressionNodeVisitor(bool trackParents)
		{
			if (trackParents)
			{
				this.m_parentNodes = new Stack<ExpressionNode>();
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000B7B8 File Offset: 0x000099B8
		protected bool PushParentNode(ExpressionNode parentNode)
		{
			if (this.m_parentNodes != null)
			{
				this.m_parentNodes.Push(parentNode);
				return true;
			}
			return false;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000B7D1 File Offset: 0x000099D1
		protected ExpressionNode PopParentNode()
		{
			if (this.m_parentNodes != null)
			{
				return this.m_parentNodes.Pop();
			}
			return null;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000B7E8 File Offset: 0x000099E8
		internal virtual TResult Visit(ExpressionNode node)
		{
			this.PushParentNode(node);
			TResult tresult = this.VisitInternal(node);
			this.PopParentNode();
			return tresult;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000B800 File Offset: 0x00009A00
		protected TResult VisitInternal(ExpressionNode node)
		{
			ExpressionNodeKind kind = node.Kind;
			if (kind <= ExpressionNodeKind.StructureReference)
			{
				switch (kind)
				{
				case ExpressionNodeKind.BinaryOperator:
					return this.Visit((BinaryOperatorExpressionNode)node);
				case ExpressionNodeKind.DataSetFieldReference:
				case ExpressionNodeKind.FilterInlinedCalculation:
				case ExpressionNodeKind.Placeholder:
					break;
				case ExpressionNodeKind.DataTransformTableColumnReference:
					return this.Visit((DataTransformTableColumnReferenceExpressionNode)node);
				case ExpressionNodeKind.DaxText:
					return this.Visit((DaxTextExpressionNode)node);
				case ExpressionNodeKind.EntitySet:
					return this.Visit((EntitySetExpressionNode)node);
				case ExpressionNodeKind.FunctionCall:
					return this.Visit((FunctionCallExpressionNode)node);
				case ExpressionNodeKind.Literal:
					return this.Visit((LiteralExpressionNode)node);
				case ExpressionNodeKind.Property:
					return this.Visit((PropertyExpressionNode)node);
				default:
					if (kind == ExpressionNodeKind.StructureReference)
					{
						return this.Visit((StructureReferenceExpressionNode)node);
					}
					break;
				}
			}
			else
			{
				if (kind == ExpressionNodeKind.VisualCalculation)
				{
					return this.Visit((VisualCalculationExpressionNode)node);
				}
				if (kind == ExpressionNodeKind.QueryParameterReference)
				{
					return this.Visit((QueryParameterReferenceExpressionNode)node);
				}
			}
			Contract.RetailFail("Unsupported expression node kind: " + node.Kind.ToString());
			throw new InvalidOperationException();
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0000B937 File Offset: 0x00009B37
		internal Stack<ExpressionNode> ParentNodes
		{
			get
			{
				return this.m_parentNodes;
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000B93F File Offset: 0x00009B3F
		internal T ReplaceCurrentNode<T>(T replacement) where T : ExpressionNode
		{
			if (this.m_parentNodes == null)
			{
				return replacement;
			}
			if (this.m_parentNodes.Peek() != replacement)
			{
				this.m_parentNodes.Pop();
				this.m_parentNodes.Push(replacement);
			}
			return replacement;
		}

		// Token: 0x060005CB RID: 1483
		public abstract TResult Visit(BinaryOperatorExpressionNode node);

		// Token: 0x060005CC RID: 1484
		public abstract TResult Visit(EntitySetExpressionNode node);

		// Token: 0x060005CD RID: 1485
		public abstract TResult Visit(LiteralExpressionNode node);

		// Token: 0x060005CE RID: 1486
		public abstract TResult Visit(StructureReferenceExpressionNode node);

		// Token: 0x060005CF RID: 1487
		public abstract TResult Visit(PropertyExpressionNode node);

		// Token: 0x060005D0 RID: 1488
		public abstract TResult Visit(FunctionCallExpressionNode node);

		// Token: 0x060005D1 RID: 1489
		public abstract TResult Visit(DataTransformTableColumnReferenceExpressionNode node);

		// Token: 0x060005D2 RID: 1490
		public abstract TResult Visit(DaxTextExpressionNode node);

		// Token: 0x060005D3 RID: 1491
		public abstract TResult Visit(QueryParameterReferenceExpressionNode node);

		// Token: 0x060005D4 RID: 1492
		public abstract TResult Visit(VisualCalculationExpressionNode node);

		// Token: 0x04000264 RID: 612
		private readonly Stack<ExpressionNode> m_parentNodes;
	}
}
