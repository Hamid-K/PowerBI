using System;
using System.Collections.Generic;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Binders;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001CB RID: 459
	internal sealed class InnerPathTokenBinder : BinderBase
	{
		// Token: 0x06001123 RID: 4387 RVA: 0x0003C5AE File Offset: 0x0003A7AE
		internal InnerPathTokenBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
			: base(bindMethod, state)
		{
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0003C5B8 File Offset: 0x0003A7B8
		internal static SingleEntityNode EnsureParentIsEntityForNavProp(SingleValueNode parent)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(parent, "parent");
			SingleEntityNode singleEntityNode = parent as SingleEntityNode;
			if (singleEntityNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_NavigationPropertyNotFollowingSingleEntityType);
			}
			return singleEntityNode;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0003C5E8 File Offset: 0x0003A7E8
		internal static IEdmProperty BindProperty(IEdmTypeReference parentReference, string propertyName, ODataUriResolver resolver = null)
		{
			if (resolver == null)
			{
				resolver = ODataUriResolver.Default;
			}
			IEdmStructuredTypeReference edmStructuredTypeReference = ((parentReference == null) ? null : parentReference.AsStructuredOrNull());
			if (edmStructuredTypeReference != null)
			{
				return resolver.ResolveProperty(edmStructuredTypeReference.StructuredDefinition(), propertyName);
			}
			return null;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0003C620 File Offset: 0x0003A820
		internal static QueryNode GetNavigationNode(IEdmNavigationProperty property, SingleEntityNode parent, IEnumerable<NamedValue> namedValues, BindingState state, KeyBinder keyBinder)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationProperty>(property, "property");
			ExceptionUtils.CheckArgumentNotNull<SingleEntityNode>(parent, "parent");
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			ExceptionUtils.CheckArgumentNotNull<KeyBinder>(keyBinder, "keyBinder");
			if (property.TargetMultiplicity() != EdmMultiplicity.Many)
			{
				return new SingleNavigationNode(property, parent);
			}
			CollectionNavigationNode collectionNavigationNode = new CollectionNavigationNode(property, parent);
			if (namedValues != null)
			{
				return keyBinder.BindKeyValues(collectionNavigationNode, namedValues, state.Model);
			}
			return collectionNavigationNode;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0003C688 File Offset: 0x0003A888
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
					if (singleValueNode.TypeReference != null && !singleValueNode.TypeReference.Definition.IsOpenType())
					{
						throw new ODataException(Strings.MetadataBinder_PropertyNotDeclared(queryNode.GetEdmTypeReference().FullName(), segmentToken.Identifier));
					}
					return new SingleValueOpenPropertyAccessNode(singleValueNode, segmentToken.Identifier);
				}
				else
				{
					if (edmProperty.Type.IsODataComplexTypeKind())
					{
						return new SingleValuePropertyAccessNode(singleValueNode, edmProperty);
					}
					if (edmProperty.Type.IsNonEntityCollectionType())
					{
						return new CollectionPropertyAccessNode(singleValueNode, edmProperty);
					}
					IEdmNavigationProperty edmNavigationProperty = edmProperty as IEdmNavigationProperty;
					if (edmNavigationProperty == null)
					{
						throw new ODataException(Strings.MetadataBinder_IllegalSegmentType(edmProperty.Name));
					}
					SingleEntityNode singleEntityNode = InnerPathTokenBinder.EnsureParentIsEntityForNavProp(singleValueNode);
					return InnerPathTokenBinder.GetNavigationNode(edmNavigationProperty, singleEntityNode, segmentToken.NamedValues, this.state, new KeyBinder(this.bindMethod));
				}
			}
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0003C7C4 File Offset: 0x0003A9C4
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
