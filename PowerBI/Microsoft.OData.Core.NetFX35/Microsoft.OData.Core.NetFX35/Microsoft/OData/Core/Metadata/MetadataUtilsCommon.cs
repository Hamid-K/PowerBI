using System;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.Metadata
{
	// Token: 0x02000131 RID: 305
	internal static class MetadataUtilsCommon
	{
		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002C4B4 File Offset: 0x0002A6B4
		internal static bool IsODataPrimitiveTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataPrimitiveTypeKind();
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002C4DC File Offset: 0x0002A6DC
		internal static bool IsODataPrimitiveTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			EdmTypeKind typeKind = type.TypeKind;
			return typeKind == EdmTypeKind.Primitive && !type.IsStream();
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002C50A File Offset: 0x0002A70A
		internal static bool IsODataComplexTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataComplexTypeKind();
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002C532 File Offset: 0x0002A732
		internal static bool IsODataComplexTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			return type.TypeKind == EdmTypeKind.Complex;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002C548 File Offset: 0x0002A748
		internal static bool IsODataEnumTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataEnumTypeKind();
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002C570 File Offset: 0x0002A770
		internal static bool IsODataEnumTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			EdmTypeKind typeKind = type.TypeKind;
			return typeKind == EdmTypeKind.Enum;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002C593 File Offset: 0x0002A793
		internal static bool IsODataTypeDefinitionTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataTypeDefinitionTypeKind();
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002C5BB File Offset: 0x0002A7BB
		internal static bool IsODataTypeDefinitionTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			return type.TypeKind == EdmTypeKind.TypeDefinition;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002C5D1 File Offset: 0x0002A7D1
		internal static bool IsODataEntityTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataEntityTypeKind();
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002C5F9 File Offset: 0x0002A7F9
		internal static bool IsODataEntityTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			return type.TypeKind == EdmTypeKind.Entity;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002C610 File Offset: 0x0002A810
		internal static bool IsODataValueType(this IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			if (edmPrimitiveTypeReference == null)
			{
				return false;
			}
			switch (edmPrimitiveTypeReference.PrimitiveKind())
			{
			case EdmPrimitiveTypeKind.Boolean:
			case EdmPrimitiveTypeKind.Byte:
			case EdmPrimitiveTypeKind.DateTimeOffset:
			case EdmPrimitiveTypeKind.Decimal:
			case EdmPrimitiveTypeKind.Double:
			case EdmPrimitiveTypeKind.Guid:
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.Int32:
			case EdmPrimitiveTypeKind.Int64:
			case EdmPrimitiveTypeKind.SByte:
			case EdmPrimitiveTypeKind.Single:
			case EdmPrimitiveTypeKind.Duration:
				return true;
			}
			return false;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002C675 File Offset: 0x0002A875
		internal static bool IsNonEntityCollectionType(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsNonEntityCollectionType();
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002C69D File Offset: 0x0002A89D
		internal static bool IsEntityCollectionType(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsEntityCollectionType();
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002C6C8 File Offset: 0x0002A8C8
		internal static bool IsNonEntityCollectionType(this IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			return edmCollectionType != null && (edmCollectionType.ElementType == null || edmCollectionType.ElementType.TypeKind() != EdmTypeKind.Entity);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002C6F8 File Offset: 0x0002A8F8
		internal static bool IsEntityCollectionType(this IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			return edmCollectionType != null && (edmCollectionType.ElementType == null || edmCollectionType.ElementType.TypeKind() == EdmTypeKind.Entity);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002C728 File Offset: 0x0002A928
		internal static IEdmPrimitiveTypeReference AsPrimitiveOrNull(this IEdmTypeReference typeReference)
		{
			if (typeReference == null)
			{
				return null;
			}
			if (typeReference.TypeKind() != EdmTypeKind.Primitive && typeReference.TypeKind() != EdmTypeKind.TypeDefinition)
			{
				return null;
			}
			return typeReference.AsPrimitive();
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002C749 File Offset: 0x0002A949
		internal static IEdmEntityTypeReference AsEntityOrNull(this IEdmTypeReference typeReference)
		{
			if (typeReference == null)
			{
				return null;
			}
			if (typeReference.TypeKind() != EdmTypeKind.Entity)
			{
				return null;
			}
			return typeReference.AsEntity();
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002C761 File Offset: 0x0002A961
		internal static IEdmStructuredTypeReference AsStructuredOrNull(this IEdmTypeReference typeReference)
		{
			if (typeReference == null)
			{
				return null;
			}
			if (!typeReference.IsStructured())
			{
				return null;
			}
			return typeReference.AsStructured();
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002C778 File Offset: 0x0002A978
		internal static bool CanConvertPrimitiveTypeTo(SingleValueNode sourceNodeOrNull, IEdmPrimitiveType sourcePrimitiveType, IEdmPrimitiveType targetPrimitiveType)
		{
			EdmPrimitiveTypeKind primitiveKind = sourcePrimitiveType.PrimitiveKind;
			EdmPrimitiveTypeKind primitiveKind2 = targetPrimitiveType.PrimitiveKind;
			switch (primitiveKind)
			{
			case EdmPrimitiveTypeKind.Byte:
				switch (primitiveKind2)
				{
				case EdmPrimitiveTypeKind.Byte:
				case EdmPrimitiveTypeKind.Decimal:
				case EdmPrimitiveTypeKind.Double:
				case EdmPrimitiveTypeKind.Int16:
				case EdmPrimitiveTypeKind.Int32:
				case EdmPrimitiveTypeKind.Int64:
				case EdmPrimitiveTypeKind.Single:
					return true;
				case EdmPrimitiveTypeKind.DateTimeOffset:
				case EdmPrimitiveTypeKind.Guid:
				case EdmPrimitiveTypeKind.SByte:
					return false;
				default:
					return false;
				}
				break;
			case EdmPrimitiveTypeKind.DateTimeOffset:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind = primitiveKind2;
				if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.DateTimeOffset)
				{
					return true;
				}
				if (edmPrimitiveTypeKind != EdmPrimitiveTypeKind.Date)
				{
					return false;
				}
				object obj;
				return MetadataUtilsCommon.TryGetConstantNodePrimitiveDate(sourceNodeOrNull, out obj);
			}
			case EdmPrimitiveTypeKind.Double:
				switch (primitiveKind2)
				{
				case EdmPrimitiveTypeKind.Decimal:
				{
					object obj2;
					return MetadataUtilsCommon.TryGetConstantNodePrimitiveLDMF(sourceNodeOrNull, out obj2);
				}
				case EdmPrimitiveTypeKind.Double:
					return true;
				default:
					return false;
				}
				break;
			case EdmPrimitiveTypeKind.Int16:
				switch (primitiveKind2)
				{
				case EdmPrimitiveTypeKind.Decimal:
				case EdmPrimitiveTypeKind.Double:
				case EdmPrimitiveTypeKind.Int16:
				case EdmPrimitiveTypeKind.Int32:
				case EdmPrimitiveTypeKind.Int64:
				case EdmPrimitiveTypeKind.Single:
					return true;
				case EdmPrimitiveTypeKind.Guid:
				case EdmPrimitiveTypeKind.SByte:
					return false;
				default:
					return false;
				}
				break;
			case EdmPrimitiveTypeKind.Int32:
				switch (primitiveKind2)
				{
				case EdmPrimitiveTypeKind.Decimal:
				case EdmPrimitiveTypeKind.Double:
				case EdmPrimitiveTypeKind.Int32:
				case EdmPrimitiveTypeKind.Int64:
				case EdmPrimitiveTypeKind.Single:
					return true;
				case EdmPrimitiveTypeKind.Guid:
				case EdmPrimitiveTypeKind.Int16:
				case EdmPrimitiveTypeKind.SByte:
					return false;
				default:
					return false;
				}
				break;
			case EdmPrimitiveTypeKind.Int64:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind2 = primitiveKind2;
				switch (edmPrimitiveTypeKind2)
				{
				case EdmPrimitiveTypeKind.Decimal:
				case EdmPrimitiveTypeKind.Double:
					break;
				default:
					switch (edmPrimitiveTypeKind2)
					{
					case EdmPrimitiveTypeKind.Int64:
					case EdmPrimitiveTypeKind.Single:
						break;
					case EdmPrimitiveTypeKind.SByte:
						return false;
					default:
						return false;
					}
					break;
				}
				return true;
			}
			case EdmPrimitiveTypeKind.SByte:
				switch (primitiveKind2)
				{
				case EdmPrimitiveTypeKind.Decimal:
				case EdmPrimitiveTypeKind.Double:
				case EdmPrimitiveTypeKind.Int16:
				case EdmPrimitiveTypeKind.Int32:
				case EdmPrimitiveTypeKind.Int64:
				case EdmPrimitiveTypeKind.SByte:
				case EdmPrimitiveTypeKind.Single:
					return true;
				case EdmPrimitiveTypeKind.Guid:
					return false;
				default:
					return false;
				}
				break;
			case EdmPrimitiveTypeKind.Single:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind3 = primitiveKind2;
				switch (edmPrimitiveTypeKind3)
				{
				case EdmPrimitiveTypeKind.Decimal:
				{
					object obj3;
					return MetadataUtilsCommon.TryGetConstantNodePrimitiveLDMF(sourceNodeOrNull, out obj3);
				}
				case EdmPrimitiveTypeKind.Double:
					break;
				default:
					if (edmPrimitiveTypeKind3 != EdmPrimitiveTypeKind.Single)
					{
						return false;
					}
					break;
				}
				return true;
			}
			}
			return primitiveKind == primitiveKind2 || targetPrimitiveType.IsAssignableFrom(sourcePrimitiveType);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002C940 File Offset: 0x0002AB40
		internal static bool TryGetConstantNodePrimitiveDate(SingleValueNode sourceNodeOrNull, out object primitiveValue)
		{
			primitiveValue = null;
			ConstantNode constantNode = sourceNodeOrNull as ConstantNode;
			if (constantNode != null)
			{
				IEdmPrimitiveType edmPrimitiveType = constantNode.TypeReference.AsPrimitiveOrNull().Definition as IEdmPrimitiveType;
				if (edmPrimitiveType != null)
				{
					EdmPrimitiveTypeKind primitiveKind = edmPrimitiveType.PrimitiveKind;
					if (primitiveKind != EdmPrimitiveTypeKind.DateTimeOffset)
					{
						return false;
					}
					Date date;
					if (UriUtils.TryUriStringToDate(constantNode.LiteralText, out date))
					{
						primitiveValue = constantNode.LiteralText;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002C99C File Offset: 0x0002AB9C
		internal static bool TryGetConstantNodePrimitiveLDMF(SingleValueNode sourceNodeOrNull, out object primitiveValue)
		{
			primitiveValue = null;
			ConstantNode constantNode = sourceNodeOrNull as ConstantNode;
			if (constantNode != null)
			{
				IEdmPrimitiveType edmPrimitiveType = constantNode.TypeReference.AsPrimitiveOrNull().Definition as IEdmPrimitiveType;
				if (edmPrimitiveType != null)
				{
					switch (edmPrimitiveType.PrimitiveKind)
					{
					case EdmPrimitiveTypeKind.Decimal:
					case EdmPrimitiveTypeKind.Double:
					case EdmPrimitiveTypeKind.Int32:
					case EdmPrimitiveTypeKind.Int64:
					case EdmPrimitiveTypeKind.Single:
						primitiveValue = constantNode.Value;
						return true;
					}
					return false;
				}
			}
			return false;
		}
	}
}
