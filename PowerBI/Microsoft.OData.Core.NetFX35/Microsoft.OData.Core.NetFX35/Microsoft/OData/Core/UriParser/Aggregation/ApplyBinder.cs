using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.UriParser.Parsers;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Aggregation
{
	// Token: 0x020002A6 RID: 678
	internal sealed class ApplyBinder
	{
		// Token: 0x06001778 RID: 6008 RVA: 0x0005042F File Offset: 0x0004E62F
		public ApplyBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
		{
			this.bindMethod = bindMethod;
			this.state = state;
			this.filterBinder = new FilterBinder(bindMethod, state);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0005045C File Offset: 0x0004E65C
		public ApplyClause BindApply(IEnumerable<QueryToken> tokens)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<QueryToken>>(tokens, "tokens");
			List<TransformationNode> list = new List<TransformationNode>();
			foreach (QueryToken queryToken in tokens)
			{
				switch (queryToken.Kind)
				{
				case QueryTokenKind.Aggregate:
				{
					AggregateTransformationNode aggregateTransformationNode = this.BindAggregateToken((AggregateToken)queryToken);
					list.Add(aggregateTransformationNode);
					this.aggregateExpressionsCache = aggregateTransformationNode.Expressions;
					this.state.AggregatedPropertyNames = Enumerable.ToList<string>(Enumerable.Select<AggregateExpression, string>(aggregateTransformationNode.Expressions, (AggregateExpression statement) => statement.Alias));
					continue;
				}
				case QueryTokenKind.AggregateGroupBy:
				{
					GroupByTransformationNode groupByTransformationNode = this.BindGroupByToken((GroupByToken)queryToken);
					list.Add(groupByTransformationNode);
					continue;
				}
				}
				FilterClause filterClause = this.filterBinder.BindFilter(queryToken);
				FilterTransformationNode filterTransformationNode = new FilterTransformationNode(filterClause);
				list.Add(filterTransformationNode);
			}
			return new ApplyClause(list);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00050570 File Offset: 0x0004E770
		private AggregateTransformationNode BindAggregateToken(AggregateToken token)
		{
			List<AggregateExpression> list = new List<AggregateExpression>();
			foreach (AggregateExpressionToken aggregateExpressionToken in token.Expressions)
			{
				list.Add(this.BindAggregateExpressionToken(aggregateExpressionToken));
			}
			return new AggregateTransformationNode(list);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x000505D0 File Offset: 0x0004E7D0
		private AggregateExpression BindAggregateExpressionToken(AggregateExpressionToken token)
		{
			SingleValueNode singleValueNode = this.bindMethod(token.Expression) as SingleValueNode;
			if (singleValueNode == null)
			{
				throw new ODataException(Strings.ApplyBinder_AggregateExpressionNotSingleValue(token.Expression));
			}
			IEdmTypeReference edmTypeReference = this.CreateAggregateExpressionTypeReference(singleValueNode, token.Method);
			return new AggregateExpression(singleValueNode, token.Method, token.Alias, edmTypeReference);
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0005062C File Offset: 0x0004E82C
		private IEdmTypeReference CreateAggregateExpressionTypeReference(SingleValueNode expression, AggregationMethod withVerb)
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
			switch (withVerb)
			{
			case AggregationMethod.Sum:
			case AggregationMethod.Min:
			case AggregationMethod.Max:
				return edmTypeReference;
			case AggregationMethod.Average:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind = edmTypeReference.PrimitiveKind();
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
			case AggregationMethod.CountDistinct:
				return EdmCoreModel.Instance.GetPrimitive(EdmPrimitiveTypeKind.Int64, false);
			default:
				throw new ODataException(Strings.ApplyBinder_UnsupportedAggregateMethod(withVerb));
			}
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00050720 File Offset: 0x0004E920
		private IEdmTypeReference GetTypeReferenceByPropertyName(string name)
		{
			return Enumerable.First<AggregateExpression>(this.aggregateExpressionsCache, (AggregateExpression statement) => statement.Alias.Equals(name)).TypeReference;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00050760 File Offset: 0x0004E960
		private GroupByTransformationNode BindGroupByToken(GroupByToken token)
		{
			List<GroupByPropertyNode> list = new List<GroupByPropertyNode>();
			foreach (EndPathToken endPathToken in token.Properties)
			{
				QueryNode queryNode = this.bindMethod(endPathToken);
				SingleValuePropertyAccessNode singleValuePropertyAccessNode = queryNode as SingleValuePropertyAccessNode;
				if (singleValuePropertyAccessNode != null)
				{
					ApplyBinder.RegisterProperty(list, ApplyBinder.ReversePropertyPath(singleValuePropertyAccessNode));
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

		// Token: 0x0600177F RID: 6015 RVA: 0x000508BC File Offset: 0x0004EABC
		private static bool IsPropertyNode(SingleValueNode node)
		{
			return node.Kind == QueryNodeKind.SingleValuePropertyAccess || node.Kind == QueryNodeKind.SingleNavigationNode;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x000508D4 File Offset: 0x0004EAD4
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
				else if (node.Kind == QueryNodeKind.SingleNavigationNode)
				{
					node = ((SingleNavigationNode)node).NavigationSource as SingleValueNode;
				}
			}
			while (node != null && ApplyBinder.IsPropertyNode(node));
			return stack;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0005094C File Offset: 0x0004EB4C
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
			SingleValuePropertyAccessNode singleValuePropertyAccessNode = singleValueNode as SingleValuePropertyAccessNode;
			properties.Add(new GroupByPropertyNode(propertyName, singleValueNode, singleValuePropertyAccessNode.TypeReference));
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x000509DC File Offset: 0x0004EBDC
		private static string GetNodePropertyName(SingleValueNode property)
		{
			if (property.Kind == QueryNodeKind.SingleValuePropertyAccess)
			{
				return ((SingleValuePropertyAccessNode)property).Property.Name;
			}
			if (property.Kind == QueryNodeKind.SingleNavigationNode)
			{
				return ((SingleNavigationNode)property).NavigationProperty.Name;
			}
			throw new NotSupportedException();
		}

		// Token: 0x04000A19 RID: 2585
		private MetadataBinder.QueryTokenVisitor bindMethod;

		// Token: 0x04000A1A RID: 2586
		private BindingState state;

		// Token: 0x04000A1B RID: 2587
		private FilterBinder filterBinder;

		// Token: 0x04000A1C RID: 2588
		private IEnumerable<AggregateExpression> aggregateExpressionsCache;
	}
}
