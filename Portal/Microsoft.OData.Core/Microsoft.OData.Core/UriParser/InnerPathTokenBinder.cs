using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000127 RID: 295
	internal sealed class InnerPathTokenBinder : BinderBase
	{
		// Token: 0x06000FF5 RID: 4085 RVA: 0x000275F3 File Offset: 0x000257F3
		internal InnerPathTokenBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
			: base(bindMethod, state)
		{
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00028E5C File Offset: 0x0002705C
		internal static SingleResourceNode EnsureParentIsResourceForNavProp(SingleValueNode parent)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(parent, "parent");
			SingleResourceNode singleResourceNode = parent as SingleResourceNode;
			if (singleResourceNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_NavigationPropertyNotFollowingSingleEntityType);
			}
			return singleResourceNode;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00028E8C File Offset: 0x0002708C
		internal static IEdmProperty BindProperty(IEdmTypeReference parentReference, string propertyName, ODataUriResolver resolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriResolver>(resolver, "resolver");
			IEdmStructuredTypeReference edmStructuredTypeReference = ((parentReference == null) ? null : parentReference.AsStructuredOrNull());
			if (edmStructuredTypeReference != null)
			{
				return resolver.ResolveProperty(edmStructuredTypeReference.StructuredDefinition(), propertyName);
			}
			return null;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00028EC4 File Offset: 0x000270C4
		internal static QueryNode GetNavigationNode(IEdmNavigationProperty property, SingleResourceNode parent, IEnumerable<NamedValue> namedValues, BindingState state, KeyBinder keyBinder, out IEdmNavigationSource navigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationProperty>(property, "property");
			ExceptionUtils.CheckArgumentNotNull<SingleResourceNode>(parent, "parent");
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			ExceptionUtils.CheckArgumentNotNull<KeyBinder>(keyBinder, "keyBinder");
			if (property.TargetMultiplicity() != EdmMultiplicity.Many)
			{
				SingleNavigationNode singleNavigationNode = new SingleNavigationNode(parent, property, state.ParsedSegments);
				navigationSource = singleNavigationNode.NavigationSource;
				return singleNavigationNode;
			}
			CollectionNavigationNode collectionNavigationNode = new CollectionNavigationNode(parent, property, state.ParsedSegments);
			navigationSource = collectionNavigationNode.NavigationSource;
			if (namedValues != null)
			{
				return keyBinder.BindKeyValues(collectionNavigationNode, namedValues, state.Model);
			}
			return collectionNavigationNode;
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00028F50 File Offset: 0x00027150
		internal QueryNode BindInnerPathSegment(InnerPathToken segmentToken)
		{
			FunctionCallBinder functionCallBinder = new FunctionCallBinder(this.bindMethod, this.state);
			QueryNode queryNode = this.DetermineParentNode(segmentToken, this.state);
			SingleValueNode singleValueNode = queryNode as SingleValueNode;
			if (singleValueNode == null)
			{
				QueryNode queryNode2;
				if (functionCallBinder.TryBindInnerPathAsFunctionCall(segmentToken, queryNode, out queryNode2))
				{
					return queryNode2;
				}
				CollectionNavigationNode collectionNavigationNode = queryNode as CollectionNavigationNode;
				if (collectionNavigationNode != null)
				{
					IEdmEntityTypeReference entityItemType = collectionNavigationNode.EntityItemType;
					IEdmProperty edmProperty = base.Resolver.ResolveProperty(entityItemType.StructuredDefinition(), segmentToken.Identifier);
					if (edmProperty != null && edmProperty.PropertyKind == EdmPropertyKind.Structural)
					{
						return new AggregatedCollectionPropertyNode(collectionNavigationNode, edmProperty);
					}
				}
				throw new ODataException(Strings.MetadataBinder_PropertyAccessSourceNotSingleValue(segmentToken.Identifier));
			}
			else
			{
				IEdmProperty edmProperty2 = InnerPathTokenBinder.BindProperty(singleValueNode.TypeReference, segmentToken.Identifier, base.Resolver);
				if (edmProperty2 == null)
				{
					QueryNode queryNode3;
					if (functionCallBinder.TryBindInnerPathAsFunctionCall(segmentToken, queryNode, out queryNode3))
					{
						return queryNode3;
					}
					if (singleValueNode.TypeReference != null && !singleValueNode.TypeReference.Definition.IsOpen())
					{
						throw new ODataException(Strings.MetadataBinder_PropertyNotDeclared(queryNode.GetEdmTypeReference().FullName(), segmentToken.Identifier));
					}
					return new SingleValueOpenPropertyAccessNode(singleValueNode, segmentToken.Identifier);
				}
				else
				{
					IEdmStructuralProperty edmStructuralProperty = edmProperty2 as IEdmStructuralProperty;
					if (edmProperty2.Type.IsComplex())
					{
						this.state.ParsedSegments.Add(new PropertySegment(edmStructuralProperty));
						return new SingleComplexNode(singleValueNode as SingleResourceNode, edmProperty2);
					}
					if (edmProperty2.Type.IsPrimitive())
					{
						return new SingleValuePropertyAccessNode(singleValueNode, edmProperty2);
					}
					if (edmProperty2.Type.IsNonEntityCollectionType())
					{
						if (edmProperty2.Type.IsStructuredCollectionType())
						{
							this.state.ParsedSegments.Add(new PropertySegment(edmStructuralProperty));
							return new CollectionComplexNode(singleValueNode as SingleResourceNode, edmProperty2);
						}
						return new CollectionPropertyAccessNode(singleValueNode, edmProperty2);
					}
					else
					{
						IEdmNavigationProperty edmNavigationProperty = edmProperty2 as IEdmNavigationProperty;
						if (edmNavigationProperty == null)
						{
							throw new ODataException(Strings.MetadataBinder_IllegalSegmentType(edmProperty2.Name));
						}
						SingleResourceNode singleResourceNode = InnerPathTokenBinder.EnsureParentIsResourceForNavProp(singleValueNode);
						IEdmNavigationSource edmNavigationSource;
						QueryNode navigationNode = InnerPathTokenBinder.GetNavigationNode(edmNavigationProperty, singleResourceNode, segmentToken.NamedValues, this.state, new KeyBinder(this.bindMethod), out edmNavigationSource);
						this.state.ParsedSegments.Add(new NavigationPropertySegment(edmNavigationProperty, edmNavigationSource));
						return navigationNode;
					}
				}
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00029154 File Offset: 0x00027354
		private QueryNode DetermineParentNode(InnerPathToken segmentToken, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<InnerPathToken>(segmentToken, "segmentToken");
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			if (segmentToken.NextToken != null)
			{
				return this.bindMethod(segmentToken.NextToken);
			}
			RangeVariable implicitRangeVariable = state.ImplicitRangeVariable;
			return NodeFactory.CreateRangeVariableReferenceNode(implicitRangeVariable);
		}
	}
}
