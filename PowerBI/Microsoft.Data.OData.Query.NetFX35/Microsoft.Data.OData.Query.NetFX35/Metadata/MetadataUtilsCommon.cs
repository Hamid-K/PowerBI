using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Metadata
{
	// Token: 0x0200001C RID: 28
	internal static class MetadataUtilsCommon
	{
		// Token: 0x06000069 RID: 105 RVA: 0x0000377A File Offset: 0x0000197A
		internal static bool IsODataPrimitiveTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataPrimitiveTypeKind();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000037A4 File Offset: 0x000019A4
		internal static bool IsODataPrimitiveTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			EdmTypeKind typeKind = type.TypeKind;
			return typeKind == EdmTypeKind.Primitive && !type.IsStream();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000037D2 File Offset: 0x000019D2
		internal static bool IsODataComplexTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataComplexTypeKind();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000037FA File Offset: 0x000019FA
		internal static bool IsODataComplexTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			return type.TypeKind == EdmTypeKind.Complex;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003810 File Offset: 0x00001A10
		internal static bool IsODataEntityTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataEntityTypeKind();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003838 File Offset: 0x00001A38
		internal static bool IsODataEntityTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			return type.TypeKind == EdmTypeKind.Entity;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003850 File Offset: 0x00001A50
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
			case EdmPrimitiveTypeKind.DateTime:
			case EdmPrimitiveTypeKind.DateTimeOffset:
			case EdmPrimitiveTypeKind.Decimal:
			case EdmPrimitiveTypeKind.Double:
			case EdmPrimitiveTypeKind.Guid:
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.Int32:
			case EdmPrimitiveTypeKind.Int64:
			case EdmPrimitiveTypeKind.SByte:
			case EdmPrimitiveTypeKind.Single:
			case EdmPrimitiveTypeKind.Time:
				return true;
			}
			return false;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000038B9 File Offset: 0x00001AB9
		internal static bool IsNonEntityODataCollectionTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsNonEntityODataCollectionTypeKind();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000038E4 File Offset: 0x00001AE4
		internal static bool IsNonEntityODataCollectionTypeKind(this IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			return edmCollectionType != null && (edmCollectionType.ElementType == null || edmCollectionType.ElementType.TypeKind() != EdmTypeKind.Entity);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003914 File Offset: 0x00001B14
		internal static string ODataFullName(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.ODataFullName();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000393C File Offset: 0x00001B3C
		internal static string ODataFullName(this IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			if (edmCollectionType != null)
			{
				string text = edmCollectionType.ElementType.ODataFullName();
				if (text == null)
				{
					return null;
				}
				return "Collection(" + text + ")";
			}
			else
			{
				IEdmSchemaElement edmSchemaElement = type as IEdmSchemaElement;
				if (edmSchemaElement == null)
				{
					return null;
				}
				return edmSchemaElement.FullName();
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003987 File Offset: 0x00001B87
		internal static IEdmPrimitiveTypeReference AsPrimitiveOrNull(this IEdmTypeReference typeReference)
		{
			if (typeReference == null)
			{
				return null;
			}
			if (typeReference.TypeKind() != EdmTypeKind.Primitive)
			{
				return null;
			}
			return typeReference.AsPrimitive();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000399F File Offset: 0x00001B9F
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

		// Token: 0x06000076 RID: 118 RVA: 0x000039B7 File Offset: 0x00001BB7
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

		// Token: 0x06000077 RID: 119 RVA: 0x000039D0 File Offset: 0x00001BD0
		internal static bool CanConvertPrimitiveTypeTo(IEdmPrimitiveType sourcePrimitiveType, IEdmPrimitiveType targetPrimitiveType)
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
				case EdmPrimitiveTypeKind.DateTime:
				case EdmPrimitiveTypeKind.DateTimeOffset:
				case EdmPrimitiveTypeKind.Guid:
				case EdmPrimitiveTypeKind.SByte:
					return false;
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
				EdmPrimitiveTypeKind edmPrimitiveTypeKind = primitiveKind2;
				switch (edmPrimitiveTypeKind)
				{
				case EdmPrimitiveTypeKind.Decimal:
				case EdmPrimitiveTypeKind.Double:
					break;
				default:
					switch (edmPrimitiveTypeKind)
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
				EdmPrimitiveTypeKind edmPrimitiveTypeKind2 = primitiveKind2;
				if (edmPrimitiveTypeKind2 == EdmPrimitiveTypeKind.Double || edmPrimitiveTypeKind2 == EdmPrimitiveTypeKind.Single)
				{
					return true;
				}
				return false;
			}
			}
			if (primitiveKind == primitiveKind2)
			{
				return true;
			}
			return false;
		}
	}
}
