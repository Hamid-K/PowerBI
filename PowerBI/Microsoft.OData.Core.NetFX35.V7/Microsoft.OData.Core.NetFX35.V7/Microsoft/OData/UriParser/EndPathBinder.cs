using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000E4 RID: 228
	internal sealed class EndPathBinder : BinderBase
	{
		// Token: 0x06000B6D RID: 2925 RVA: 0x0001C109 File Offset: 0x0001A309
		internal EndPathBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
			: base(bindMethod, state)
		{
			this.functionCallBinder = new FunctionCallBinder(bindMethod, state);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0001C120 File Offset: 0x0001A320
		internal static QueryNode GeneratePropertyAccessQueryNode(SingleResourceNode parentNode, IEdmProperty property, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleResourceNode>(parentNode, "parentNode");
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			if (property.Type.IsNonEntityCollectionType())
			{
				if (property.Type.IsStructuredCollectionType())
				{
					return new CollectionComplexNode(parentNode, property);
				}
				return new CollectionPropertyAccessNode(parentNode, property);
			}
			else if (property.PropertyKind == EdmPropertyKind.Navigation)
			{
				IEdmNavigationProperty edmNavigationProperty = (IEdmNavigationProperty)property;
				if (edmNavigationProperty.TargetMultiplicity() == EdmMultiplicity.Many)
				{
					return new CollectionNavigationNode(parentNode, edmNavigationProperty, state.ParsedSegments);
				}
				return new SingleNavigationNode(parentNode, edmNavigationProperty, state.ParsedSegments);
			}
			else
			{
				if (property.Type.IsComplex())
				{
					return new SingleComplexNode(parentNode, property);
				}
				return new SingleValuePropertyAccessNode(parentNode, property);
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0001C1C0 File Offset: 0x0001A3C0
		internal static SingleValueNode CreateParentFromImplicitRangeVariable(BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			if (state.ImplicitRangeVariable == null)
			{
				throw new ODataException(Strings.MetadataBinder_PropertyAccessWithoutParentParameter);
			}
			return NodeFactory.CreateRangeVariableReferenceNode(state.ImplicitRangeVariable);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0001C1EC File Offset: 0x0001A3EC
		internal SingleValueOpenPropertyAccessNode GeneratePropertyAccessQueryForOpenType(EndPathToken endPathToken, SingleValueNode parentNode)
		{
			if (parentNode.TypeReference == null || parentNode.TypeReference.Definition.IsOpen() || this.IsAggregatedProperty(endPathToken.Identifier))
			{
				return new SingleValueOpenPropertyAccessNode(parentNode, endPathToken.Identifier);
			}
			throw new ODataException(Strings.MetadataBinder_PropertyNotDeclared(parentNode.TypeReference.FullName(), endPathToken.Identifier));
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0001C24C File Offset: 0x0001A44C
		internal QueryNode BindEndPath(EndPathToken endPathToken)
		{
			ExceptionUtils.CheckArgumentNotNull<EndPathToken>(endPathToken, "EndPathToken");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(endPathToken.Identifier, "EndPathToken.Identifier");
			QueryNode queryNode = this.DetermineParentNode(endPathToken);
			SingleValueNode singleValueNode = queryNode as SingleValueNode;
			if (singleValueNode == null)
			{
				QueryNode queryNode2;
				if (this.functionCallBinder.TryBindEndPathAsFunctionCall(endPathToken, queryNode, this.state, out queryNode2))
				{
					return queryNode2;
				}
				CollectionNode collectionNode = queryNode as CollectionNode;
				if (collectionNode != null && endPathToken.Identifier.Equals("$count"))
				{
					return new CountNode(collectionNode);
				}
				throw new ODataException(Strings.MetadataBinder_PropertyAccessSourceNotSingleValue(endPathToken.Identifier));
			}
			else
			{
				IEdmStructuredTypeReference edmStructuredTypeReference = ((singleValueNode.TypeReference == null) ? null : singleValueNode.TypeReference.AsStructuredOrNull());
				IEdmProperty edmProperty = ((edmStructuredTypeReference == null) ? null : base.Resolver.ResolveProperty(edmStructuredTypeReference.StructuredDefinition(), endPathToken.Identifier));
				if (edmProperty != null)
				{
					return EndPathBinder.GeneratePropertyAccessQueryNode(singleValueNode as SingleResourceNode, edmProperty, this.state);
				}
				if (endPathToken.Identifier == "$count")
				{
					return new CountVirtualPropertyNode();
				}
				QueryNode queryNode2;
				if (this.functionCallBinder.TryBindEndPathAsFunctionCall(endPathToken, singleValueNode, this.state, out queryNode2))
				{
					return queryNode2;
				}
				return this.GeneratePropertyAccessQueryForOpenType(endPathToken, singleValueNode);
			}
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0001C360 File Offset: 0x0001A560
		private QueryNode DetermineParentNode(EndPathToken segmentToken)
		{
			ExceptionUtils.CheckArgumentNotNull<EndPathToken>(segmentToken, "segmentToken");
			ExceptionUtils.CheckArgumentNotNull<BindingState>(this.state, "state");
			if (segmentToken.NextToken != null)
			{
				return this.bindMethod(segmentToken.NextToken);
			}
			RangeVariable implicitRangeVariable = this.state.ImplicitRangeVariable;
			return NodeFactory.CreateRangeVariableReferenceNode(implicitRangeVariable);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0001C3B6 File Offset: 0x0001A5B6
		private bool IsAggregatedProperty(string identifier)
		{
			return this.state.AggregatedPropertyNames != null && this.state.AggregatedPropertyNames.Contains(identifier);
		}

		// Token: 0x0400068F RID: 1679
		private readonly FunctionCallBinder functionCallBinder;
	}
}
