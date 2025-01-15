using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001C3 RID: 451
	internal sealed class ApplyBinder
	{
		// Token: 0x060011BB RID: 4539 RVA: 0x000316F7 File Offset: 0x0002F8F7
		public ApplyBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
		{
			this.bindMethod = bindMethod;
			this.state = state;
			this.filterBinder = new FilterBinder(bindMethod, state);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0003171C File Offset: 0x0002F91C
		public ApplyClause BindApply(IEnumerable<QueryToken> tokens)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<QueryToken>>(tokens, "tokens");
			List<TransformationNode> list = new List<TransformationNode>();
			foreach (QueryToken queryToken in tokens)
			{
				QueryTokenKind kind = queryToken.Kind;
				if (kind != QueryTokenKind.Aggregate)
				{
					if (kind != QueryTokenKind.AggregateGroupBy)
					{
						FilterClause filterClause = this.filterBinder.BindFilter(queryToken);
						FilterTransformationNode filterTransformationNode = new FilterTransformationNode(filterClause);
						list.Add(filterTransformationNode);
					}
					else
					{
						GroupByTransformationNode groupByTransformationNode = this.BindGroupByToken((GroupByToken)queryToken);
						list.Add(groupByTransformationNode);
					}
				}
				else
				{
					AggregateTransformationNode aggregateTransformationNode = this.BindAggregateToken((AggregateToken)queryToken);
					list.Add(aggregateTransformationNode);
					this.aggregateExpressionsCache = aggregateTransformationNode.Expressions;
					this.state.AggregatedPropertyNames = Enumerable.ToList<string>(Enumerable.Select<AggregateExpression, string>(aggregateTransformationNode.Expressions, (AggregateExpression statement) => statement.Alias));
				}
			}
			return new ApplyClause(list);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00031828 File Offset: 0x0002FA28
		private AggregateTransformationNode BindAggregateToken(AggregateToken token)
		{
			List<AggregateExpression> list = new List<AggregateExpression>();
			foreach (AggregateExpressionToken aggregateExpressionToken in token.Expressions)
			{
				list.Add(this.BindAggregateExpressionToken(aggregateExpressionToken));
			}
			return new AggregateTransformationNode(list);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00031888 File Offset: 0x0002FA88
		private AggregateExpression BindAggregateExpressionToken(AggregateExpressionToken token)
		{
			SingleValueNode singleValueNode = this.bindMethod(token.Expression) as SingleValueNode;
			if (singleValueNode == null)
			{
				throw new ODataException(Strings.ApplyBinder_AggregateExpressionNotSingleValue(token.Expression));
			}
			IEdmTypeReference edmTypeReference = this.CreateAggregateExpressionTypeReference(singleValueNode, token.MethodDefinition);
			return new AggregateExpression(singleValueNode, token.MethodDefinition, token.Alias, edmTypeReference);
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x000318E4 File Offset: 0x0002FAE4
		private IEdmTypeReference CreateAggregateExpressionTypeReference(SingleValueNode expression, AggregationMethodDefinition method)
		{
			IEdmTypeReference edmTypeReference = expression.TypeReference;
			if (edmTypeReference == null && this.aggregateExpressionsCache != null)
			{
				SingleValueOpenPropertyAccessNode singleValueOpenPropertyAccessNode = expression as SingleValueOpenPropertyAccessNode;
				if (singleValueOpenPropertyAccessNode != null)
				{
					edmTypeReference = this.GetTypeReferenceByPropertyName(singleValueOpenPropertyAccessNode.Name);
				}
			}
			switch (method.MethodKind)
			{
			case AggregationMethod.Sum:
			case AggregationMethod.Min:
			case AggregationMethod.Max:
				return edmTypeReference;
			case AggregationMethod.Average:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind = edmTypeReference.PrimitiveKind();
				if (edmPrimitiveTypeKind != EdmPrimitiveTypeKind.None)
				{
					switch (edmPrimitiveTypeKind)
					{
					case EdmPrimitiveTypeKind.Decimal:
						return EdmCoreModel.Instance.GetPrimitive(EdmPrimitiveTypeKind.Decimal, edmTypeReference.IsNullable);
					case EdmPrimitiveTypeKind.Double:
					case EdmPrimitiveTypeKind.Int32:
					case EdmPrimitiveTypeKind.Int64:
						return EdmCoreModel.Instance.GetPrimitive(EdmPrimitiveTypeKind.Double, edmTypeReference.IsNullable);
					}
					throw new ODataException(Strings.ApplyBinder_AggregateExpressionIncompatibleTypeForMethod(expression, edmPrimitiveTypeKind));
				}
				return edmTypeReference;
			}
			case AggregationMethod.CountDistinct:
			case AggregationMethod.VirtualPropertyCount:
				return EdmCoreModel.Instance.GetPrimitive(EdmPrimitiveTypeKind.Int64, false);
			default:
				return EdmCoreModel.Instance.GetPrimitive(EdmPrimitiveTypeKind.Double, edmTypeReference.IsNullable);
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x000319C8 File Offset: 0x0002FBC8
		private IEdmTypeReference GetTypeReferenceByPropertyName(string name)
		{
			if (this.aggregateExpressionsCache != null)
			{
				AggregateExpression aggregateExpression = Enumerable.FirstOrDefault<AggregateExpression>(this.aggregateExpressionsCache, (AggregateExpression statement) => statement.Alias.Equals(name));
				if (aggregateExpression != null)
				{
					return aggregateExpression.TypeReference;
				}
			}
			return null;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00031A10 File Offset: 0x0002FC10
		private GroupByTransformationNode BindGroupByToken(GroupByToken token)
		{
			List<GroupByPropertyNode> list = new List<GroupByPropertyNode>();
			foreach (EndPathToken endPathToken in token.Properties)
			{
				QueryNode queryNode = this.bindMethod(endPathToken);
				SingleValuePropertyAccessNode singleValuePropertyAccessNode = queryNode as SingleValuePropertyAccessNode;
				SingleComplexNode singleComplexNode = queryNode as SingleComplexNode;
				if (singleValuePropertyAccessNode != null)
				{
					ApplyBinder.RegisterProperty(list, ApplyBinder.ReversePropertyPath(singleValuePropertyAccessNode));
				}
				else if (singleComplexNode != null)
				{
					ApplyBinder.RegisterProperty(list, ApplyBinder.ReversePropertyPath(singleComplexNode));
				}
				else
				{
					SingleValueOpenPropertyAccessNode singleValueOpenPropertyAccessNode = queryNode as SingleValueOpenPropertyAccessNode;
					if (singleValueOpenPropertyAccessNode == null)
					{
						throw new ODataException(Strings.ApplyBinder_GroupByPropertyNotPropertyAccessValue(endPathToken.Identifier));
					}
					IEdmTypeReference typeReferenceByPropertyName = this.GetTypeReferenceByPropertyName(singleValueOpenPropertyAccessNode.Name);
					list.Add(new GroupByPropertyNode(singleValueOpenPropertyAccessNode.Name, singleValueOpenPropertyAccessNode, typeReferenceByPropertyName));
				}
			}
			TransformationNode transformationNode = null;
			if (token.Child != null)
			{
				if (token.Child.Kind != QueryTokenKind.Aggregate)
				{
					throw new ODataException(Strings.ApplyBinder_UnsupportedGroupByChild(token.Child.Kind));
				}
				transformationNode = this.BindAggregateToken((AggregateToken)token.Child);
				this.aggregateExpressionsCache = ((AggregateTransformationNode)transformationNode).Expressions;
				this.state.AggregatedPropertyNames = Enumerable.ToList<string>(Enumerable.Select<AggregateExpression, string>(this.aggregateExpressionsCache, (AggregateExpression statement) => statement.Alias));
			}
			return new GroupByTransformationNode(list, transformationNode, null);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00031B8C File Offset: 0x0002FD8C
		private static bool IsPropertyNode(SingleValueNode node)
		{
			return node.Kind == QueryNodeKind.SingleValuePropertyAccess || node.Kind == QueryNodeKind.SingleComplexNode || node.Kind == QueryNodeKind.SingleNavigationNode;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00031BB0 File Offset: 0x0002FDB0
		private static Stack<SingleValueNode> ReversePropertyPath(SingleValueNode node)
		{
			Stack<SingleValueNode> stack = new Stack<SingleValueNode>();
			do
			{
				stack.Push(node);
				if (node.Kind == QueryNodeKind.SingleValuePropertyAccess)
				{
					node = ((SingleValuePropertyAccessNode)node).Source;
				}
				else if (node.Kind == QueryNodeKind.SingleComplexNode)
				{
					node = ((SingleComplexNode)node).Source;
				}
				else if (node.Kind == QueryNodeKind.SingleNavigationNode)
				{
					node = ((SingleNavigationNode)node).NavigationSource as SingleValueNode;
				}
			}
			while (node != null && ApplyBinder.IsPropertyNode(node));
			return stack;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00031C24 File Offset: 0x0002FE24
		private static void RegisterProperty(IList<GroupByPropertyNode> properties, Stack<SingleValueNode> propertyStack)
		{
			SingleValueNode singleValueNode = propertyStack.Pop();
			string propertyName = ApplyBinder.GetNodePropertyName(singleValueNode);
			if (propertyStack.Count != 0)
			{
				GroupByPropertyNode groupByPropertyNode = Enumerable.FirstOrDefault<GroupByPropertyNode>(properties, (GroupByPropertyNode p) => p.Name == propertyName);
				if (groupByPropertyNode == null)
				{
					groupByPropertyNode = new GroupByPropertyNode(propertyName, null);
					properties.Add(groupByPropertyNode);
				}
				ApplyBinder.RegisterProperty(groupByPropertyNode.ChildTransformations, propertyStack);
				return;
			}
			properties.Add(new GroupByPropertyNode(propertyName, singleValueNode, singleValueNode.TypeReference));
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00031CA4 File Offset: 0x0002FEA4
		private static string GetNodePropertyName(SingleValueNode property)
		{
			if (property.Kind == QueryNodeKind.SingleValuePropertyAccess)
			{
				return ((SingleValuePropertyAccessNode)property).Property.Name;
			}
			if (property.Kind == QueryNodeKind.SingleComplexNode)
			{
				return ((SingleComplexNode)property).Property.Name;
			}
			if (property.Kind == QueryNodeKind.SingleNavigationNode)
			{
				return ((SingleNavigationNode)property).NavigationProperty.Name;
			}
			throw new NotSupportedException();
		}

		// Token: 0x04000906 RID: 2310
		private MetadataBinder.QueryTokenVisitor bindMethod;

		// Token: 0x04000907 RID: 2311
		private BindingState state;

		// Token: 0x04000908 RID: 2312
		private FilterBinder filterBinder;

		// Token: 0x04000909 RID: 2313
		private IEnumerable<AggregateExpression> aggregateExpressionsCache;
	}
}
