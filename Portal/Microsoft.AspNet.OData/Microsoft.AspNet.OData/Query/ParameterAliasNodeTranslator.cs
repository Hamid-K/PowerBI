using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000BD RID: 189
	public class ParameterAliasNodeTranslator : QueryNodeVisitor<QueryNode>
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x000161B1 File Offset: 0x000143B1
		public ParameterAliasNodeTranslator(IDictionary<string, SingleValueNode> parameterAliasNodes)
		{
			if (parameterAliasNodes == null)
			{
				throw Error.ArgumentNull("parameterAliasNodes");
			}
			this._parameterAliasNode = parameterAliasNodes;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000161D0 File Offset: 0x000143D0
		public override QueryNode Visit(AllNode nodeIn)
		{
			AllNode allNode = new AllNode(nodeIn.RangeVariables, nodeIn.CurrentRangeVariable);
			if (nodeIn.Source != null)
			{
				allNode.Source = (CollectionNode)nodeIn.Source.Accept<QueryNode>(this);
			}
			if (nodeIn.Body != null)
			{
				allNode.Body = (SingleValueNode)nodeIn.Body.Accept<QueryNode>(this);
			}
			return allNode;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00016230 File Offset: 0x00014430
		public override QueryNode Visit(AnyNode nodeIn)
		{
			AnyNode anyNode = new AnyNode(nodeIn.RangeVariables, nodeIn.CurrentRangeVariable);
			if (nodeIn.Source != null)
			{
				anyNode.Source = (CollectionNode)nodeIn.Source.Accept<QueryNode>(this);
			}
			if (nodeIn.Body != null)
			{
				anyNode.Body = (SingleValueNode)nodeIn.Body.Accept<QueryNode>(this);
			}
			return anyNode;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001628E File Offset: 0x0001448E
		public override QueryNode Visit(BinaryOperatorNode nodeIn)
		{
			return new BinaryOperatorNode(nodeIn.OperatorKind, (SingleValueNode)nodeIn.Left.Accept<QueryNode>(this), (SingleValueNode)nodeIn.Right.Accept<QueryNode>(this));
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000162BD File Offset: 0x000144BD
		public override QueryNode Visit(InNode nodeIn)
		{
			return new InNode((SingleValueNode)nodeIn.Left.Accept<QueryNode>(this), (CollectionNode)nodeIn.Right.Accept<QueryNode>(this));
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000162E8 File Offset: 0x000144E8
		public override QueryNode Visit(CollectionFunctionCallNode nodeIn)
		{
			return new CollectionFunctionCallNode(nodeIn.Name, nodeIn.Functions, nodeIn.Parameters.Select((QueryNode p) => p.Accept<QueryNode>(this)), nodeIn.CollectionType, (nodeIn.Source == null) ? null : nodeIn.Source.Accept<QueryNode>(this));
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001633C File Offset: 0x0001453C
		public override QueryNode Visit(CollectionNavigationNode nodeIn)
		{
			if (nodeIn.Source != null)
			{
				return new CollectionNavigationNode((SingleResourceNode)nodeIn.Source.Accept<QueryNode>(this), nodeIn.NavigationProperty, nodeIn.BindingPath ?? new EdmPathExpression(nodeIn.NavigationProperty.Name));
			}
			return nodeIn;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00016389 File Offset: 0x00014589
		public override QueryNode Visit(CollectionOpenPropertyAccessNode nodeIn)
		{
			return new CollectionOpenPropertyAccessNode((SingleValueNode)nodeIn.Source.Accept<QueryNode>(this), nodeIn.Name);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000163A7 File Offset: 0x000145A7
		public override QueryNode Visit(CollectionComplexNode nodeIn)
		{
			return new CollectionComplexNode((SingleResourceNode)nodeIn.Source.Accept<QueryNode>(this), nodeIn.Property);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000163C5 File Offset: 0x000145C5
		public override QueryNode Visit(CollectionPropertyAccessNode nodeIn)
		{
			return new CollectionPropertyAccessNode((SingleValueNode)nodeIn.Source.Accept<QueryNode>(this), nodeIn.Property);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override QueryNode Visit(ConstantNode nodeIn)
		{
			return nodeIn;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override QueryNode Visit(CollectionConstantNode nodeIn)
		{
			return nodeIn;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000163E3 File Offset: 0x000145E3
		public override QueryNode Visit(ConvertNode nodeIn)
		{
			return new ConvertNode((SingleValueNode)nodeIn.Source.Accept<QueryNode>(this), nodeIn.TypeReference);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00016401 File Offset: 0x00014601
		public override QueryNode Visit(CollectionResourceCastNode nodeIn)
		{
			return new CollectionResourceCastNode((CollectionResourceNode)nodeIn.Source.Accept<QueryNode>(this), (IEdmStructuredType)nodeIn.ItemType.Definition);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001642C File Offset: 0x0001462C
		public override QueryNode Visit(CollectionResourceFunctionCallNode nodeIn)
		{
			return new CollectionResourceFunctionCallNode(nodeIn.Name, nodeIn.Functions, nodeIn.Parameters.Select((QueryNode p) => p.Accept<QueryNode>(this)), nodeIn.CollectionType, (IEdmEntitySetBase)nodeIn.NavigationSource, (nodeIn.Source == null) ? null : nodeIn.Source.Accept<QueryNode>(this));
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override QueryNode Visit(ResourceRangeVariableReferenceNode nodeIn)
		{
			return nodeIn;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00016489 File Offset: 0x00014689
		public override QueryNode Visit(NamedFunctionParameterNode nodeIn)
		{
			return new NamedFunctionParameterNode(nodeIn.Name, (nodeIn.Value == null) ? null : nodeIn.Value.Accept<QueryNode>(this));
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override QueryNode Visit(NonResourceRangeVariableReferenceNode nodeIn)
		{
			return nodeIn;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000164B0 File Offset: 0x000146B0
		public override QueryNode Visit(ParameterAliasNode nodeIn)
		{
			SingleValueNode singleValueNode = ODataPathSegmentTranslator.TranslateParameterAlias(nodeIn, this._parameterAliasNode);
			if (singleValueNode == null)
			{
				return new ConstantNode(null);
			}
			return singleValueNode.Accept<QueryNode>(this);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override QueryNode Visit(SearchTermNode nodeIn)
		{
			return nodeIn;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000164DB File Offset: 0x000146DB
		public override QueryNode Visit(SingleResourceCastNode nodeIn)
		{
			if (nodeIn.Source != null)
			{
				return new SingleResourceCastNode((SingleResourceNode)nodeIn.Source.Accept<QueryNode>(this), (IEdmStructuredType)nodeIn.TypeReference.Definition);
			}
			return nodeIn;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00016510 File Offset: 0x00014710
		public override QueryNode Visit(SingleResourceFunctionCallNode nodeIn)
		{
			return new SingleResourceFunctionCallNode(nodeIn.Name, nodeIn.Functions, nodeIn.Parameters.Select((QueryNode p) => p.Accept<QueryNode>(this)), nodeIn.StructuredTypeReference, nodeIn.NavigationSource, (nodeIn.Source == null) ? null : nodeIn.Source.Accept<QueryNode>(this));
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00016568 File Offset: 0x00014768
		public override QueryNode Visit(SingleNavigationNode nodeIn)
		{
			if (nodeIn.Source != null)
			{
				return new SingleNavigationNode((SingleResourceNode)nodeIn.Source.Accept<QueryNode>(this), nodeIn.NavigationProperty, nodeIn.BindingPath ?? new EdmPathExpression(nodeIn.NavigationProperty.Name));
			}
			return nodeIn;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x000165B8 File Offset: 0x000147B8
		public override QueryNode Visit(SingleValueFunctionCallNode nodeIn)
		{
			return new SingleValueFunctionCallNode(nodeIn.Name, nodeIn.Functions, nodeIn.Parameters.Select((QueryNode p) => p.Accept<QueryNode>(this)), nodeIn.TypeReference, (nodeIn.Source == null) ? null : nodeIn.Source.Accept<QueryNode>(this));
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001660A File Offset: 0x0001480A
		public override QueryNode Visit(SingleValueOpenPropertyAccessNode nodeIn)
		{
			return new SingleValueOpenPropertyAccessNode((SingleValueNode)nodeIn.Source.Accept<QueryNode>(this), nodeIn.Name);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00016628 File Offset: 0x00014828
		public override QueryNode Visit(SingleValuePropertyAccessNode nodeIn)
		{
			return new SingleValuePropertyAccessNode((SingleValueNode)nodeIn.Source.Accept<QueryNode>(this), nodeIn.Property);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00016646 File Offset: 0x00014846
		public override QueryNode Visit(SingleComplexNode nodeIn)
		{
			return new SingleComplexNode((SingleResourceNode)nodeIn.Source.Accept<QueryNode>(this), nodeIn.Property);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00016664 File Offset: 0x00014864
		public override QueryNode Visit(UnaryOperatorNode nodeIn)
		{
			return new UnaryOperatorNode(nodeIn.OperatorKind, (SingleValueNode)nodeIn.Operand.Accept<QueryNode>(this));
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00016682 File Offset: 0x00014882
		public override QueryNode Visit(CountNode nodeIn)
		{
			return new CountNode((CollectionNode)nodeIn.Source.Accept<QueryNode>(this));
		}

		// Token: 0x0400018D RID: 397
		private IDictionary<string, SingleValueNode> _parameterAliasNode;
	}
}
