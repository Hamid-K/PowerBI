using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000E9 RID: 233
	internal sealed class InnerPathTokenBinder : BinderBase
	{
		// Token: 0x06000B9B RID: 2971 RVA: 0x0001BF18 File Offset: 0x0001A118
		internal InnerPathTokenBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
			: base(bindMethod, state)
		{
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0001D5C8 File Offset: 0x0001B7C8
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

		// Token: 0x06000B9D RID: 2973 RVA: 0x0001D5F8 File Offset: 0x0001B7F8
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

		// Token: 0x06000B9E RID: 2974 RVA: 0x0001D630 File Offset: 0x0001B830
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

		// Token: 0x06000B9F RID: 2975 RVA: 0x0001D6BC File Offset: 0x0001B8BC
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
				throw new ODataException(Strings.MetadataBinder_PropertyAccessSourceNotSingleValue(segmentToken.Identifier));
			}
			else
			{
				IEdmProperty edmProperty = InnerPathTokenBinder.BindProperty(singleValueNode.TypeReference, segmentToken.Identifier, base.Resolver);
				if (edmProperty == null)
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
					IEdmStructuralProperty edmStructuralProperty = edmProperty as IEdmStructuralProperty;
					if (edmProperty.Type.IsComplex())
					{
						this.state.ParsedSegments.Add(new PropertySegment(edmStructuralProperty));
						return new SingleComplexNode(singleValueNode as SingleResourceNode, edmProperty);
					}
					if (edmProperty.Type.IsPrimitive())
					{
						return new SingleValuePropertyAccessNode(singleValueNode, edmProperty);
					}
					if (edmProperty.Type.IsNonEntityCollectionType())
					{
						if (edmProperty.Type.IsStructuredCollectionType())
						{
							this.state.ParsedSegments.Add(new PropertySegment(edmStructuralProperty));
							return new CollectionComplexNode(singleValueNode as SingleResourceNode, edmProperty);
						}
						return new CollectionPropertyAccessNode(singleValueNode, edmProperty);
					}
					else
					{
						IEdmNavigationProperty edmNavigationProperty = edmProperty as IEdmNavigationProperty;
						if (edmNavigationProperty == null)
						{
							throw new ODataException(Strings.MetadataBinder_IllegalSegmentType(edmProperty.Name));
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

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0001D878 File Offset: 0x0001BA78
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
