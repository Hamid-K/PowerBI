using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000EE RID: 238
	internal static class MetadataBindingUtils
	{
		// Token: 0x06000BC0 RID: 3008 RVA: 0x0001E26C File Offset: 0x0001C46C
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
				if (!TypePromotionUtils.CanConvertTo(source, source.TypeReference, targetTypeReference))
				{
					throw new ODataException(Strings.MetadataBinder_CannotConvertToType(source.TypeReference.FullName(), targetTypeReference.FullName()));
				}
				ConstantNode constantNode = source as ConstantNode;
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

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0001E414 File Offset: 0x0001C614
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

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0001E45C File Offset: 0x0001C65C
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
