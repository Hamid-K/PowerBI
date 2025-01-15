using System;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.OData.Metadata
{
	// Token: 0x02000100 RID: 256
	internal static class MetadataUtilsCommon
	{
		// Token: 0x06000EF1 RID: 3825 RVA: 0x0002519C File Offset: 0x0002339C
		internal static bool IsODataPrimitiveTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataPrimitiveTypeKind();
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x000251C8 File Offset: 0x000233C8
		internal static bool IsODataPrimitiveTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			EdmTypeKind typeKind = type.TypeKind;
			return typeKind == EdmTypeKind.Primitive && !type.IsStream();
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x000251F7 File Offset: 0x000233F7
		internal static bool IsODataComplexTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataComplexTypeKind();
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00025221 File Offset: 0x00023421
		internal static bool IsODataComplexTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			return type.TypeKind == EdmTypeKind.Complex;
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00025238 File Offset: 0x00023438
		internal static bool IsODataEnumTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataEnumTypeKind();
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00025264 File Offset: 0x00023464
		internal static bool IsODataEnumTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			EdmTypeKind typeKind = type.TypeKind;
			return typeKind == EdmTypeKind.Enum;
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00025288 File Offset: 0x00023488
		internal static bool IsODataTypeDefinitionTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataTypeDefinitionTypeKind();
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x000252B2 File Offset: 0x000234B2
		internal static bool IsODataTypeDefinitionTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			return type.TypeKind == EdmTypeKind.TypeDefinition;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000252C9 File Offset: 0x000234C9
		internal static bool IsODataEntityTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataEntityTypeKind();
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x000252F3 File Offset: 0x000234F3
		internal static bool IsODataEntityTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			return type.TypeKind == EdmTypeKind.Entity;
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0002530C File Offset: 0x0002350C
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

		// Token: 0x06000EFC RID: 3836 RVA: 0x00025371 File Offset: 0x00023571
		internal static bool IsNonEntityCollectionType(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsNonEntityCollectionType();
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0002539B File Offset: 0x0002359B
		internal static bool IsEntityCollectionType(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsEntityCollectionType();
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x000253C5 File Offset: 0x000235C5
		internal static bool IsStructuredCollectionType(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsStructuredCollectionType();
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x000253F0 File Offset: 0x000235F0
		internal static bool IsNonEntityCollectionType(this IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			return edmCollectionType != null && (edmCollectionType.ElementType == null || edmCollectionType.ElementType.TypeKind() != EdmTypeKind.Entity);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00025420 File Offset: 0x00023620
		internal static bool IsEntityCollectionType(this IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			return edmCollectionType != null && (edmCollectionType.ElementType == null || edmCollectionType.ElementType.TypeKind() == EdmTypeKind.Entity);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00025450 File Offset: 0x00023650
		internal static bool IsStructuredCollectionType(this IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			return edmCollectionType != null && (edmCollectionType.ElementType == null || edmCollectionType.ElementType.TypeKind() == EdmTypeKind.Entity || edmCollectionType.ElementType.TypeKind() == EdmTypeKind.Complex);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00025490 File Offset: 0x00023690
		internal static bool IsEntityOrEntityCollectionType(this IEdmType edmType)
		{
			IEdmEntityType edmEntityType;
			return edmType.IsEntityOrEntityCollectionType(out edmEntityType);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x000254A5 File Offset: 0x000236A5
		internal static bool IsEntityOrEntityCollectionType(this IEdmType edmType, out IEdmEntityType entityType)
		{
			if (edmType.TypeKind == EdmTypeKind.Entity)
			{
				entityType = (IEdmEntityType)edmType;
				return true;
			}
			if (edmType.TypeKind != EdmTypeKind.Collection)
			{
				entityType = null;
				return false;
			}
			entityType = ((IEdmCollectionType)edmType).ElementType.Definition as IEdmEntityType;
			return entityType != null;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x000254E4 File Offset: 0x000236E4
		internal static bool IsStructuredOrStructuredCollectionType(this IEdmType edmType)
		{
			IEdmStructuredType edmStructuredType;
			return edmType.IsStructuredOrStructuredCollectionType(out edmStructuredType);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x000254FC File Offset: 0x000236FC
		internal static bool IsStructuredOrStructuredCollectionType(this IEdmType edmType, out IEdmStructuredType structuredType)
		{
			if (edmType.TypeKind.IsStructured())
			{
				structuredType = (IEdmStructuredType)edmType;
				return true;
			}
			if (edmType.TypeKind != EdmTypeKind.Collection)
			{
				structuredType = null;
				return false;
			}
			structuredType = ((IEdmCollectionType)edmType).ElementType.Definition as IEdmStructuredType;
			return structuredType != null;
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0002554A File Offset: 0x0002374A
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

		// Token: 0x06000F07 RID: 3847 RVA: 0x0002556B File Offset: 0x0002376B
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

		// Token: 0x06000F08 RID: 3848 RVA: 0x00025583 File Offset: 0x00023783
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

		// Token: 0x06000F09 RID: 3849 RVA: 0x0002559C File Offset: 0x0002379C
		internal static bool CanConvertPrimitiveTypeTo(SingleValueNode sourceNodeOrNull, IEdmPrimitiveType sourcePrimitiveType, IEdmPrimitiveType targetPrimitiveType)
		{
			EdmPrimitiveTypeKind primitiveKind = sourcePrimitiveType.PrimitiveKind;
			EdmPrimitiveTypeKind primitiveKind2 = targetPrimitiveType.PrimitiveKind;
			if (primitiveKind2 == EdmPrimitiveTypeKind.PrimitiveType)
			{
				return true;
			}
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
			case EdmPrimitiveTypeKind.Decimal:
			case EdmPrimitiveTypeKind.Guid:
				break;
			case EdmPrimitiveTypeKind.Double:
				if (primitiveKind2 == EdmPrimitiveTypeKind.Decimal)
				{
					object obj;
					return MetadataUtilsCommon.TryGetConstantNodePrimitiveLDMF(sourceNodeOrNull, out obj);
				}
				if (primitiveKind2 == EdmPrimitiveTypeKind.Double)
				{
					return true;
				}
				return false;
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
				if (primitiveKind2 <= EdmPrimitiveTypeKind.Double)
				{
					if (primitiveKind2 != EdmPrimitiveTypeKind.Decimal && primitiveKind2 != EdmPrimitiveTypeKind.Double)
					{
						return false;
					}
				}
				else if (primitiveKind2 != EdmPrimitiveTypeKind.Int64 && primitiveKind2 != EdmPrimitiveTypeKind.Single)
				{
					return false;
				}
				return true;
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
				if (primitiveKind2 == EdmPrimitiveTypeKind.Decimal)
				{
					object obj2;
					return MetadataUtilsCommon.TryGetConstantNodePrimitiveLDMF(sourceNodeOrNull, out obj2);
				}
				if (primitiveKind2 == EdmPrimitiveTypeKind.Double || primitiveKind2 == EdmPrimitiveTypeKind.Single)
				{
					return true;
				}
				return false;
			default:
				if (primitiveKind == EdmPrimitiveTypeKind.Date)
				{
					if (primitiveKind2 == EdmPrimitiveTypeKind.DateTimeOffset || primitiveKind2 == EdmPrimitiveTypeKind.Date)
					{
						return true;
					}
					return false;
				}
				break;
			}
			return primitiveKind == primitiveKind2 || targetPrimitiveType.IsAssignableFrom(sourcePrimitiveType);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0002571C File Offset: 0x0002391C
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
