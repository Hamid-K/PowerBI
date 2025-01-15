using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Binders;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001C5 RID: 453
	internal sealed class DottedIdentifierBinder : BinderBase
	{
		// Token: 0x060010F0 RID: 4336 RVA: 0x0003AF02 File Offset: 0x00039102
		internal DottedIdentifierBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
			: base(bindMethod, state)
		{
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0003AF0C File Offset: 0x0003910C
		internal QueryNode BindDottedIdentifier(DottedIdentifierToken dottedIdentifierToken)
		{
			ExceptionUtils.CheckArgumentNotNull<DottedIdentifierToken>(dottedIdentifierToken, "castToken");
			ExceptionUtils.CheckArgumentNotNull<BindingState>(this.state, "state");
			QueryNode queryNode = null;
			IEdmType edmType = null;
			if (this.state.ImplicitRangeVariable != null)
			{
				if (dottedIdentifierToken.NextToken == null)
				{
					queryNode = NodeFactory.CreateRangeVariableReferenceNode(this.state.ImplicitRangeVariable);
					edmType = this.state.ImplicitRangeVariable.TypeReference.Definition;
				}
				else
				{
					queryNode = this.bindMethod(dottedIdentifierToken.NextToken);
					edmType = queryNode.GetEdmType();
				}
			}
			SingleEntityNode singleEntityNode = queryNode as SingleEntityNode;
			IEdmSchemaType edmSchemaType = UriEdmHelpers.FindTypeFromModel(this.state.Model, dottedIdentifierToken.Identifier, base.Resolver);
			IEdmStructuredType edmStructuredType = edmSchemaType as IEdmStructuredType;
			if (edmStructuredType == null)
			{
				FunctionCallBinder functionCallBinder = new FunctionCallBinder(this.bindMethod, this.state);
				QueryNode queryNode2;
				if (functionCallBinder.TryBindDottedIdentifierAsFunctionCall(dottedIdentifierToken, queryNode as SingleValueNode, out queryNode2))
				{
					return queryNode2;
				}
				if (!string.IsNullOrEmpty(dottedIdentifierToken.Identifier) && dottedIdentifierToken.Identifier.get_Chars(dottedIdentifierToken.Identifier.Length - 1) == '\'')
				{
					QueryNode queryNode3;
					if (EnumBinder.TryBindDottedIdentifierAsEnum(dottedIdentifierToken, singleEntityNode, this.state, out queryNode3))
					{
						return queryNode3;
					}
					throw new ODataException(Strings.Binder_IsNotValidEnumConstant(dottedIdentifierToken.Identifier));
				}
				else
				{
					IEdmTypeReference edmTypeReference = UriEdmHelpers.FindTypeFromModel(this.state.Model, dottedIdentifierToken.Identifier, base.Resolver).ToTypeReference();
					if (edmTypeReference is IEdmPrimitiveTypeReference || edmTypeReference is IEdmEnumTypeReference)
					{
						return new ConstantNode(dottedIdentifierToken.Identifier, dottedIdentifierToken.Identifier);
					}
					throw new ODataException(Strings.CastBinder_ChildTypeIsNotEntity(dottedIdentifierToken.Identifier));
				}
			}
			else
			{
				UriEdmHelpers.CheckRelatedTo(edmType, edmSchemaType);
				IEdmEntityType edmEntityType = edmStructuredType as IEdmEntityType;
				if (edmEntityType != null)
				{
					EntityCollectionNode entityCollectionNode = queryNode as EntityCollectionNode;
					if (entityCollectionNode != null)
					{
						return new EntityCollectionCastNode(entityCollectionNode, edmEntityType);
					}
					if (queryNode == null)
					{
						return new SingleEntityCastNode(null, edmEntityType);
					}
					return new SingleEntityCastNode(singleEntityNode, edmEntityType);
				}
				else
				{
					IEdmComplexType edmComplexType = edmStructuredType as IEdmComplexType;
					CollectionPropertyAccessNode collectionPropertyAccessNode = queryNode as CollectionPropertyAccessNode;
					if (collectionPropertyAccessNode != null)
					{
						return new CollectionPropertyCastNode(collectionPropertyAccessNode, edmComplexType);
					}
					if (queryNode == null)
					{
						return new SingleValueCastNode(null, edmComplexType);
					}
					SingleValueNode singleValueNode = queryNode as SingleValueNode;
					return new SingleValueCastNode(singleValueNode, edmComplexType);
				}
			}
		}
	}
}
