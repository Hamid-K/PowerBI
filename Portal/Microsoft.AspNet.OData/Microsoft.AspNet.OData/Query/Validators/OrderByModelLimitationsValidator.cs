using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query.Validators
{
	// Token: 0x020000D9 RID: 217
	internal class OrderByModelLimitationsValidator : QueryNodeVisitor<SingleValueNode>
	{
		// Token: 0x06000745 RID: 1861 RVA: 0x00018AEE File Offset: 0x00016CEE
		public OrderByModelLimitationsValidator(ODataQueryContext context, bool enableOrderBy)
		{
			this._model = context.Model;
			this._enableOrderBy = enableOrderBy;
			if (context.Path != null)
			{
				this._property = context.TargetProperty;
				this._structuredType = context.TargetStructuredType;
			}
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00018B29 File Offset: 0x00016D29
		public bool TryValidate(IEdmProperty property, IEdmStructuredType structuredType, OrderByClause orderByClause, bool explicitPropertiesDefined)
		{
			this._property = property;
			this._structuredType = structuredType;
			return this.TryValidate(orderByClause, explicitPropertiesDefined);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00018B44 File Offset: 0x00016D44
		public bool TryValidate(OrderByClause orderByClause, bool explicitPropertiesDefined)
		{
			SingleValueNode singleValueNode = orderByClause.Expression.Accept<SingleValueNode>(this);
			if (singleValueNode != null && !explicitPropertiesDefined)
			{
				throw new ODataException(Error.Format(SRResources.NotSortablePropertyUsedInOrderBy, new object[] { OrderByModelLimitationsValidator.GetPropertyName(singleValueNode) }));
			}
			return singleValueNode == null;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00018B88 File Offset: 0x00016D88
		public override SingleValueNode Visit(SingleValuePropertyAccessNode nodeIn)
		{
			if (nodeIn.Source != null)
			{
				if (nodeIn.Source.Kind == QueryNodeKind.SingleNavigationNode)
				{
					SingleNavigationNode singleNavigationNode = nodeIn.Source as SingleNavigationNode;
					if (EdmLibHelpers.IsNotSortable(nodeIn.Property, singleNavigationNode.NavigationProperty, singleNavigationNode.NavigationProperty.ToEntityType(), this._model, this._enableOrderBy))
					{
						return nodeIn;
					}
				}
				else if (nodeIn.Source.Kind == QueryNodeKind.SingleComplexNode)
				{
					SingleComplexNode singleComplexNode = nodeIn.Source as SingleComplexNode;
					if (EdmLibHelpers.IsNotSortable(nodeIn.Property, singleComplexNode.Property, nodeIn.Property.DeclaringType, this._model, this._enableOrderBy))
					{
						return nodeIn;
					}
				}
				else if (EdmLibHelpers.IsNotSortable(nodeIn.Property, this._property, this._structuredType, this._model, this._enableOrderBy))
				{
					return nodeIn;
				}
			}
			if (nodeIn.Source != null)
			{
				return nodeIn.Source.Accept<SingleValueNode>(this);
			}
			return null;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00018C6B File Offset: 0x00016E6B
		public override SingleValueNode Visit(SingleComplexNode nodeIn)
		{
			if (EdmLibHelpers.IsNotSortable(nodeIn.Property, this._property, this._structuredType, this._model, this._enableOrderBy))
			{
				return nodeIn;
			}
			if (nodeIn.Source != null)
			{
				return nodeIn.Source.Accept<SingleValueNode>(this);
			}
			return null;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00018CAA File Offset: 0x00016EAA
		public override SingleValueNode Visit(SingleNavigationNode nodeIn)
		{
			if (EdmLibHelpers.IsNotSortable(nodeIn.NavigationProperty, this._property, this._structuredType, this._model, this._enableOrderBy))
			{
				return nodeIn;
			}
			if (nodeIn.Source != null)
			{
				return nodeIn.Source.Accept<SingleValueNode>(this);
			}
			return null;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000F7E1 File Offset: 0x0000D9E1
		public override SingleValueNode Visit(ResourceRangeVariableReferenceNode nodeIn)
		{
			return null;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000F7E1 File Offset: 0x0000D9E1
		public override SingleValueNode Visit(NonResourceRangeVariableReferenceNode nodeIn)
		{
			return null;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00018CEC File Offset: 0x00016EEC
		private static string GetPropertyName(SingleValueNode node)
		{
			if (node.Kind == QueryNodeKind.SingleNavigationNode)
			{
				return ((SingleNavigationNode)node).NavigationProperty.Name;
			}
			if (node.Kind == QueryNodeKind.SingleValuePropertyAccess)
			{
				return ((SingleValuePropertyAccessNode)node).Property.Name;
			}
			if (node.Kind == QueryNodeKind.SingleComplexNode)
			{
				return ((SingleComplexNode)node).Property.Name;
			}
			return null;
		}

		// Token: 0x0400022B RID: 555
		private readonly IEdmModel _model;

		// Token: 0x0400022C RID: 556
		private readonly bool _enableOrderBy;

		// Token: 0x0400022D RID: 557
		private IEdmProperty _property;

		// Token: 0x0400022E RID: 558
		private IEdmStructuredType _structuredType;
	}
}
