using System;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200012C RID: 300
	internal static class MetadataBindingUtils
	{
		// Token: 0x0600101D RID: 4125 RVA: 0x00029C50 File Offset: 0x00027E50
		internal static SingleValueNode ConvertToTypeIfNeeded(SingleValueNode source, IEdmTypeReference targetTypeReference)
		{
			if (targetTypeReference == null)
			{
				return source;
			}
			if (source.TypeReference == null)
			{
				return new ConvertNode(source, targetTypeReference);
			}
			if (source.TypeReference.IsEquivalentTo(targetTypeReference))
			{
				if (source.TypeReference.IsTypeDefinition())
				{
					return new ConvertNode(source, targetTypeReference);
				}
				return source;
			}
			else
			{
				if (targetTypeReference.IsStructured() || targetTypeReference.IsStructuredCollectionType())
				{
					return new ConvertNode(source, targetTypeReference);
				}
				ConstantNode constantNode = source as ConstantNode;
				if (constantNode != null && constantNode.Value != null && source.TypeReference.IsString() && targetTypeReference.IsEnum())
				{
					string memberName = constantNode.Value.ToString();
					IEdmEnumType edmEnumType = targetTypeReference.Definition as IEdmEnumType;
					if (edmEnumType.Members.Any((IEdmEnumMember m) => string.Compare(m.Name, memberName, StringComparison.Ordinal) == 0))
					{
						string text = ODataUriUtils.ConvertToUriLiteral(constantNode.Value, ODataVersion.V4);
						return new ConstantNode(new ODataEnumValue(constantNode.Value.ToString(), targetTypeReference.Definition.ToString()), text, targetTypeReference);
					}
					throw new ODataException(Strings.Binder_IsNotValidEnumConstant(memberName));
				}
				else
				{
					if (!TypePromotionUtils.CanConvertTo(source, source.TypeReference, targetTypeReference))
					{
						throw new ODataException(Strings.MetadataBinder_CannotConvertToType(source.TypeReference.FullName(), targetTypeReference.FullName()));
					}
					if (source.TypeReference.IsEnum() && constantNode != null)
					{
						return new ConstantNode(constantNode.Value, ODataUriUtils.ConvertToUriLiteral(constantNode.Value, ODataVersion.V4), targetTypeReference);
					}
					object obj;
					if (!MetadataUtilsCommon.TryGetConstantNodePrimitiveLDMF(source, out obj) || obj == null)
					{
						return new ConvertNode(source, targetTypeReference);
					}
					object obj2 = ODataUriConversionUtils.CoerceNumericType(obj, targetTypeReference.AsPrimitive().Definition as IEdmPrimitiveType);
					if (string.IsNullOrEmpty(constantNode.LiteralText))
					{
						return new ConstantNode(obj2);
					}
					ConstantNode constantNode2 = new ConstantNode(obj2, constantNode.LiteralText);
					IEdmDecimalTypeReference edmDecimalTypeReference = constantNode2.TypeReference as IEdmDecimalTypeReference;
					if (edmDecimalTypeReference == null)
					{
						return constantNode2;
					}
					IEdmDecimalTypeReference edmDecimalTypeReference2 = (IEdmDecimalTypeReference)targetTypeReference;
					if (!(edmDecimalTypeReference.Precision == edmDecimalTypeReference2.Precision) || !(edmDecimalTypeReference.Scale == edmDecimalTypeReference2.Scale))
					{
						return new ConvertNode(constantNode2, targetTypeReference);
					}
					return constantNode2;
				}
			}
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00029EA8 File Offset: 0x000280A8
		internal static IEdmType GetEdmType(this QueryNode segment)
		{
			SingleValueNode singleValueNode = segment as SingleValueNode;
			if (singleValueNode != null)
			{
				IEdmTypeReference typeReference = singleValueNode.TypeReference;
				if (typeReference == null)
				{
					return null;
				}
				return typeReference.Definition;
			}
			else
			{
				CollectionNode collectionNode = segment as CollectionNode;
				if (collectionNode == null)
				{
					return null;
				}
				IEdmTypeReference itemType = collectionNode.ItemType;
				if (itemType == null)
				{
					return null;
				}
				return itemType.Definition;
			}
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00029EF0 File Offset: 0x000280F0
		internal static IEdmTypeReference GetEdmTypeReference(this QueryNode segment)
		{
			SingleValueNode singleValueNode = segment as SingleValueNode;
			if (singleValueNode != null)
			{
				return singleValueNode.TypeReference;
			}
			CollectionNode collectionNode = segment as CollectionNode;
			if (collectionNode != null)
			{
				return collectionNode.ItemType;
			}
			return null;
		}
	}
}
