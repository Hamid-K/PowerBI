using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001F1 RID: 497
	internal sealed class ApplyBinder
	{
		// Token: 0x06001654 RID: 5716 RVA: 0x0003E4EA File Offset: 0x0003C6EA
		public ApplyBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
			: this(bindMethod, state, null, null)
		{
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0003E4F6 File Offset: 0x0003C6F6
		public ApplyBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state, ODataUriParserConfiguration configuration, ODataPathInfo odataPathInfo)
		{
			this.bindMethod = bindMethod;
			this.state = state;
			this.filterBinder = new FilterBinder(bindMethod, state);
			this.configuration = configuration;
			this.odataPathInfo = odataPathInfo;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0003E528 File Offset: 0x0003C728
		public ApplyClause BindApply(IEnumerable<QueryToken> tokens)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<QueryToken>>(tokens, "tokens");
			List<TransformationNode> list = new List<TransformationNode>();
			foreach (QueryToken queryToken in tokens)
			{
				QueryTokenKind kind = queryToken.Kind;
				if (kind != QueryTokenKind.Expand)
				{
					switch (kind)
					{
					case QueryTokenKind.Aggregate:
					{
						AggregateTransformationNode aggregateTransformationNode = this.BindAggregateToken((AggregateToken)queryToken);
						list.Add(aggregateTransformationNode);
						this.aggregateExpressionsCache = aggregateTransformationNode.AggregateExpressions;
						this.state.AggregatedPropertyNames = new HashSet<EndPathToken>(aggregateTransformationNode.AggregateExpressions.Select((AggregateExpressionBase statement) => new EndPathToken(statement.Alias, null)));
						this.state.IsCollapsed = true;
						continue;
					}
					case QueryTokenKind.AggregateGroupBy:
					{
						GroupByTransformationNode groupByTransformationNode = this.BindGroupByToken((GroupByToken)queryToken);
						list.Add(groupByTransformationNode);
						this.state.IsCollapsed = true;
						continue;
					}
					case QueryTokenKind.Compute:
					{
						ComputeTransformationNode computeTransformationNode = this.BindComputeToken((ComputeToken)queryToken);
						list.Add(computeTransformationNode);
						this.state.AggregatedPropertyNames = new HashSet<EndPathToken>(computeTransformationNode.Expressions.Select((ComputeExpression statement) => new EndPathToken(statement.Alias, null)));
						continue;
					}
					}
					FilterClause filterClause = this.filterBinder.BindFilter(queryToken);
					FilterTransformationNode filterTransformationNode = new FilterTransformationNode(filterClause);
					list.Add(filterTransformationNode);
				}
				else
				{
					SelectExpandClause selectExpandClause = SelectExpandSemanticBinder.Bind(this.odataPathInfo, (ExpandToken)queryToken, null, this.configuration, null);
					ExpandTransformationNode expandTransformationNode = new ExpandTransformationNode(selectExpandClause);
					list.Add(expandTransformationNode);
				}
			}
			return new ApplyClause(list);
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0003E6F8 File Offset: 0x0003C8F8
		private AggregateTransformationNode BindAggregateToken(AggregateToken token)
		{
			IEnumerable<AggregateTokenBase> enumerable = ApplyBinder.MergeEntitySetAggregates(token.AggregateExpressions);
			List<AggregateExpressionBase> list = new List<AggregateExpressionBase>();
			foreach (AggregateTokenBase aggregateTokenBase in enumerable)
			{
				list.Add(this.BindAggregateExpressionToken(aggregateTokenBase));
			}
			return new AggregateTransformationNode(list);
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0003E760 File Offset: 0x0003C960
		private static IEnumerable<AggregateTokenBase> MergeEntitySetAggregates(IEnumerable<AggregateTokenBase> tokens)
		{
			List<AggregateTokenBase> list = new List<AggregateTokenBase>();
			Dictionary<string, AggregateTokenBase> dictionary = new Dictionary<string, AggregateTokenBase>();
			foreach (AggregateTokenBase aggregateTokenBase in tokens)
			{
				QueryTokenKind kind = aggregateTokenBase.Kind;
				if (kind != QueryTokenKind.AggregateExpression)
				{
					if (kind == QueryTokenKind.EntitySetAggregateExpression)
					{
						EntitySetAggregateToken entitySetAggregateToken = aggregateTokenBase as EntitySetAggregateToken;
						string text = entitySetAggregateToken.Path();
						AggregateTokenBase aggregateTokenBase2;
						if (dictionary.TryGetValue(text, out aggregateTokenBase2))
						{
							dictionary.Remove(text);
						}
						dictionary.Add(text, EntitySetAggregateToken.Merge(entitySetAggregateToken, aggregateTokenBase2 as EntitySetAggregateToken));
					}
				}
				else
				{
					list.Add(aggregateTokenBase);
				}
			}
			return list.Concat(dictionary.Values).ToList<AggregateTokenBase>();
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0003E818 File Offset: 0x0003CA18
		private AggregateExpressionBase BindAggregateExpressionToken(AggregateTokenBase aggregateToken)
		{
			QueryTokenKind kind = aggregateToken.Kind;
			if (kind == QueryTokenKind.AggregateExpression)
			{
				AggregateExpressionToken aggregateExpressionToken = aggregateToken as AggregateExpressionToken;
				SingleValueNode singleValueNode = this.bindMethod(aggregateExpressionToken.Expression) as SingleValueNode;
				IEdmTypeReference edmTypeReference = this.CreateAggregateExpressionTypeReference(singleValueNode, aggregateExpressionToken.MethodDefinition);
				return new AggregateExpression(singleValueNode, aggregateExpressionToken.MethodDefinition, aggregateExpressionToken.Alias, edmTypeReference);
			}
			if (kind != QueryTokenKind.EntitySetAggregateExpression)
			{
				throw new ODataException(Strings.ApplyBinder_UnsupportedAggregateKind(aggregateToken.Kind));
			}
			EntitySetAggregateToken entitySetAggregateToken = aggregateToken as EntitySetAggregateToken;
			QueryNode queryNode = this.bindMethod(entitySetAggregateToken.EntitySet);
			this.state.InEntitySetAggregation = true;
			IEnumerable<AggregateExpressionBase> enumerable = entitySetAggregateToken.Expressions.Select((AggregateTokenBase x) => this.BindAggregateExpressionToken(x)).ToList<AggregateExpressionBase>();
			this.state.InEntitySetAggregation = false;
			return new EntitySetAggregateExpression((CollectionNavigationNode)queryNode, enumerable);
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0003E8F4 File Offset: 0x0003CAF4
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

		// Token: 0x0600165B RID: 5723 RVA: 0x0003E9D8 File Offset: 0x0003CBD8
		private IEdmTypeReference GetTypeReferenceByPropertyName(string name)
		{
			if (this.aggregateExpressionsCache != null)
			{
				AggregateExpression aggregateExpression = this.aggregateExpressionsCache.OfType<AggregateExpression>().FirstOrDefault((AggregateExpression statement) => statement.AggregateKind == AggregateExpressionKind.PropertyAggregate && statement.Alias.Equals(name));
				if (aggregateExpression != null)
				{
					return aggregateExpression.TypeReference;
				}
			}
			return null;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0003EA24 File Offset: 0x0003CC24
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
			HashSet<EndPathToken> hashSet = new HashSet<EndPathToken>(token.Properties);
			TransformationNode transformationNode = null;
			if (token.Child != null)
			{
				if (token.Child.Kind != QueryTokenKind.Aggregate)
				{
					throw new ODataException(Strings.ApplyBinder_UnsupportedGroupByChild(token.Child.Kind));
				}
				transformationNode = this.BindAggregateToken((AggregateToken)token.Child);
				this.aggregateExpressionsCache = ((AggregateTransformationNode)transformationNode).AggregateExpressions;
				hashSet.UnionWith(this.aggregateExpressionsCache.Select((AggregateExpressionBase statement) => new EndPathToken(statement.Alias, null)));
			}
			this.state.AggregatedPropertyNames = hashSet;
			return new GroupByTransformationNode(list, transformationNode, null);
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0003EBB0 File Offset: 0x0003CDB0
		private static bool IsPropertyNode(SingleValueNode node)
		{
			return node.Kind == QueryNodeKind.SingleValuePropertyAccess || node.Kind == QueryNodeKind.SingleComplexNode || node.Kind == QueryNodeKind.SingleNavigationNode;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0003EBD4 File Offset: 0x0003CDD4
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
					node = ((SingleNavigationNode)node).Source;
				}
			}
			while (node != null && ApplyBinder.IsPropertyNode(node));
			return stack;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0003EC44 File Offset: 0x0003CE44
		private static void RegisterProperty(IList<GroupByPropertyNode> properties, Stack<SingleValueNode> propertyStack)
		{
			SingleValueNode singleValueNode = propertyStack.Pop();
			string propertyName = ApplyBinder.GetNodePropertyName(singleValueNode);
			if (propertyStack.Count != 0)
			{
				GroupByPropertyNode groupByPropertyNode = properties.FirstOrDefault((GroupByPropertyNode p) => p.Name == propertyName);
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

		// Token: 0x06001660 RID: 5728 RVA: 0x0003ECC4 File Offset: 0x0003CEC4
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

		// Token: 0x06001661 RID: 5729 RVA: 0x0003ED28 File Offset: 0x0003CF28
		private ComputeTransformationNode BindComputeToken(ComputeToken token)
		{
			List<ComputeExpression> list = new List<ComputeExpression>();
			foreach (ComputeExpressionToken computeExpressionToken in token.Expressions)
			{
				SingleValueNode singleValueNode = (SingleValueNode)this.bindMethod(computeExpressionToken.Expression);
				list.Add(new ComputeExpression(singleValueNode, computeExpressionToken.Alias, singleValueNode.TypeReference));
			}
			return new ComputeTransformationNode(list);
		}

		// Token: 0x04000A11 RID: 2577
		private MetadataBinder.QueryTokenVisitor bindMethod;

		// Token: 0x04000A12 RID: 2578
		private BindingState state;

		// Token: 0x04000A13 RID: 2579
		private FilterBinder filterBinder;

		// Token: 0x04000A14 RID: 2580
		private ODataUriParserConfiguration configuration;

		// Token: 0x04000A15 RID: 2581
		private ODataPathInfo odataPathInfo;

		// Token: 0x04000A16 RID: 2582
		private IEnumerable<AggregateExpressionBase> aggregateExpressionsCache;
	}
}
