using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Binders;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001C6 RID: 454
	internal sealed class EndPathBinder : BinderBase
	{
		// Token: 0x060010F2 RID: 4338 RVA: 0x0003B10B File Offset: 0x0003930B
		internal EndPathBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
			: base(bindMethod, state)
		{
			this.functionCallBinder = new FunctionCallBinder(bindMethod, state);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0003B124 File Offset: 0x00039324
		internal static QueryNode GeneratePropertyAccessQueryNode(SingleValueNode parentNode, IEdmProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(parentNode, "parent");
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			if (property.Type.IsNonEntityCollectionType())
			{
				return new CollectionPropertyAccessNode(parentNode, property);
			}
			if (property.PropertyKind != EdmPropertyKind.Navigation)
			{
				return new SingleValuePropertyAccessNode(parentNode, property);
			}
			IEdmNavigationProperty edmNavigationProperty = (IEdmNavigationProperty)property;
			SingleEntityNode singleEntityNode = (SingleEntityNode)parentNode;
			if (edmNavigationProperty.TargetMultiplicity() == EdmMultiplicity.Many)
			{
				return new CollectionNavigationNode(edmNavigationProperty, singleEntityNode);
			}
			return new SingleNavigationNode(edmNavigationProperty, singleEntityNode);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0003B193 File Offset: 0x00039393
		internal static SingleValueNode CreateParentFromImplicitRangeVariable(BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			if (state.ImplicitRangeVariable == null)
			{
				throw new ODataException(Strings.MetadataBinder_PropertyAccessWithoutParentParameter);
			}
			return NodeFactory.CreateRangeVariableReferenceNode(state.ImplicitRangeVariable);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0003B1C0 File Offset: 0x000393C0
		internal SingleValueOpenPropertyAccessNode GeneratePropertyAccessQueryForOpenType(EndPathToken endPathToken, SingleValueNode parentNode)
		{
			if (parentNode.TypeReference == null || parentNode.TypeReference.Definition.IsOpenType() || this.IsAggregatedProperty(endPathToken.Identifier))
			{
				return new SingleValueOpenPropertyAccessNode(parentNode, endPathToken.Identifier);
			}
			throw new ODataException(Strings.MetadataBinder_PropertyNotDeclared(parentNode.TypeReference.FullName(), endPathToken.Identifier));
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0003B220 File Offset: 0x00039420
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
					return EndPathBinder.GeneratePropertyAccessQueryNode(singleValueNode, edmProperty);
				}
				QueryNode queryNode2;
				if (this.functionCallBinder.TryBindEndPathAsFunctionCall(endPathToken, singleValueNode, this.state, out queryNode2))
				{
					return queryNode2;
				}
				return this.GeneratePropertyAccessQueryForOpenType(endPathToken, singleValueNode);
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0003B310 File Offset: 0x00039510
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

		// Token: 0x060010F8 RID: 4344 RVA: 0x0003B364 File Offset: 0x00039564
		private bool IsAggregatedProperty(string identifier)
		{
			return this.state.AggregatedPropertyNames != null && this.state.AggregatedPropertyNames.Contains(identifier);
		}

		// Token: 0x0400077E RID: 1918
		private readonly FunctionCallBinder functionCallBinder;
	}
}
