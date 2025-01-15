using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000121 RID: 289
	internal sealed class DottedIdentifierBinder : BinderBase
	{
		// Token: 0x06000FC4 RID: 4036 RVA: 0x000275F3 File Offset: 0x000257F3
		internal DottedIdentifierBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
			: base(bindMethod, state)
		{
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00027600 File Offset: 0x00025800
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
			SingleResourceNode singleResourceNode = queryNode as SingleResourceNode;
			IEdmSchemaType edmSchemaType = UriEdmHelpers.FindTypeFromModel(this.state.Model, dottedIdentifierToken.Identifier, base.Resolver);
			IEdmStructuredType edmStructuredType = edmSchemaType as IEdmStructuredType;
			if (edmStructuredType == null)
			{
				SingleValueNode singleValueNode = queryNode as SingleValueNode;
				FunctionCallBinder functionCallBinder = new FunctionCallBinder(this.bindMethod, this.state);
				QueryNode queryNode2;
				if (functionCallBinder.TryBindDottedIdentifierAsFunctionCall(dottedIdentifierToken, singleValueNode, out queryNode2))
				{
					return queryNode2;
				}
				if (!string.IsNullOrEmpty(dottedIdentifierToken.Identifier) && dottedIdentifierToken.Identifier[dottedIdentifierToken.Identifier.Length - 1] == '\'')
				{
					QueryNode queryNode3;
					if (EnumBinder.TryBindDottedIdentifierAsEnum(dottedIdentifierToken, singleResourceNode, this.state, base.Resolver, out queryNode3))
					{
						return queryNode3;
					}
					throw new ODataException(Strings.Binder_IsNotValidEnumConstant(dottedIdentifierToken.Identifier));
				}
				else
				{
					IEdmTypeReference edmTypeReference = UriEdmHelpers.FindTypeFromModel(this.state.Model, dottedIdentifierToken.Identifier, base.Resolver).ToTypeReference();
					if (!(edmTypeReference is IEdmPrimitiveTypeReference) && !(edmTypeReference is IEdmEnumTypeReference))
					{
						throw new ODataException(Strings.CastBinder_ChildTypeIsNotEntity(dottedIdentifierToken.Identifier));
					}
					IEdmPrimitiveType edmPrimitiveType = edmSchemaType as IEdmPrimitiveType;
					if (edmPrimitiveType != null && dottedIdentifierToken.NextToken != null)
					{
						return new SingleValueCastNode(singleValueNode, edmPrimitiveType);
					}
					return new ConstantNode(dottedIdentifierToken.Identifier, dottedIdentifierToken.Identifier);
				}
			}
			else
			{
				UriEdmHelpers.CheckRelatedTo(edmType, edmSchemaType);
				this.state.ParsedSegments.Add(new TypeSegment(edmSchemaType, edmType, null));
				CollectionResourceNode collectionResourceNode = queryNode as CollectionResourceNode;
				if (collectionResourceNode != null)
				{
					return new CollectionResourceCastNode(collectionResourceNode, edmStructuredType);
				}
				return new SingleResourceCastNode(singleResourceNode, edmStructuredType);
			}
		}
	}
}
