using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001D1 RID: 465
	internal static class MetadataBindingUtils
	{
		// Token: 0x0600114C RID: 4428 RVA: 0x0003D1C8 File Offset: 0x0003B3C8
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
				if (MetadataUtilsCommon.TryGetConstantNodePrimitiveDate(source, out obj) && obj != null)
				{
					object obj2 = ODataUriConversionUtils.CoerceTemporalType(obj, targetTypeReference.AsPrimitive().Definition as IEdmPrimitiveType);
					if (obj2 != null)
					{
						if (string.IsNullOrEmpty(constantNode.LiteralText))
						{
							return new ConstantNode(obj2);
						}
						return new ConstantNode(obj2, constantNode.LiteralText, targetTypeReference);
					}
				}
				if (!MetadataUtilsCommon.TryGetConstantNodePrimitiveLDMF(source, out obj) || obj == null)
				{
					return new ConvertNode(source, targetTypeReference);
				}
				object obj3 = ODataUriConversionUtils.CoerceNumericType(obj, targetTypeReference.AsPrimitive().Definition as IEdmPrimitiveType);
				if (string.IsNullOrEmpty(constantNode.LiteralText))
				{
					return new ConstantNode(obj3);
				}
				return new ConstantNode(obj3, constantNode.LiteralText);
			}
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0003D304 File Offset: 0x0003B504
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

		// Token: 0x0600114E RID: 4430 RVA: 0x0003D34C File Offset: 0x0003B54C
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
