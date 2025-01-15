using System;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020001D8 RID: 472
	internal static class MetadataUtilsCommon
	{
		// Token: 0x06001285 RID: 4741 RVA: 0x00035044 File Offset: 0x00033244
		internal static bool IsODataPrimitiveTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataPrimitiveTypeKind();
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00035070 File Offset: 0x00033270
		internal static bool IsODataPrimitiveTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			EdmTypeKind typeKind = type.TypeKind;
			return typeKind == EdmTypeKind.Primitive && !type.IsStream();
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0003509F File Offset: 0x0003329F
		internal static bool IsODataComplexTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataComplexTypeKind();
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000350C9 File Offset: 0x000332C9
		internal static bool IsODataComplexTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			return type.TypeKind == EdmTypeKind.Complex;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x000350E0 File Offset: 0x000332E0
		internal static bool IsODataEnumTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataEnumTypeKind();
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0003510C File Offset: 0x0003330C
		internal static bool IsODataEnumTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			EdmTypeKind typeKind = type.TypeKind;
			return typeKind == EdmTypeKind.Enum;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00035130 File Offset: 0x00033330
		internal static bool IsODataTypeDefinitionTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataTypeDefinitionTypeKind();
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0003515A File Offset: 0x0003335A
		internal static bool IsODataTypeDefinitionTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			return type.TypeKind == EdmTypeKind.TypeDefinition;
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00035171 File Offset: 0x00033371
		internal static bool IsODataEntityTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataEntityTypeKind();
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0003519B File Offset: 0x0003339B
		internal static bool IsODataEntityTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			return type.TypeKind == EdmTypeKind.Entity;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x000351B4 File Offset: 0x000333B4
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

		// Token: 0x06001290 RID: 4752 RVA: 0x00035219 File Offset: 0x00033419
		internal static bool IsNonEntityCollectionType(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsNonEntityCollectionType();
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00035243 File Offset: 0x00033443
		internal static bool IsEntityCollectionType(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsEntityCollectionType();
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0003526D File Offset: 0x0003346D
		internal static bool IsStructuredCollectionType(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsStructuredCollectionType();
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00035298 File Offset: 0x00033498
		internal static bool IsNonEntityCollectionType(this IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			return edmCollectionType != null && (edmCollectionType.ElementType == null || edmCollectionType.ElementType.TypeKind() != EdmTypeKind.Entity);
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x000352C8 File Offset: 0x000334C8
		internal static bool IsEntityCollectionType(this IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			return edmCollectionType != null && (edmCollectionType.ElementType == null || edmCollectionType.ElementType.TypeKind() == EdmTypeKind.Entity);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000352F8 File Offset: 0x000334F8
		internal static bool IsStructuredCollectionType(this IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			return edmCollectionType != null && (edmCollectionType.ElementType == null || edmCollectionType.ElementType.TypeKind() == EdmTypeKind.Entity || edmCollectionType.ElementType.TypeKind() == EdmTypeKind.Complex);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00035338 File Offset: 0x00033538
		internal static bool IsEntityOrEntityCollectionType(this IEdmType edmType)
		{
			IEdmEntityType edmEntityType;
			return edmType.IsEntityOrEntityCollectionType(out edmEntityType);
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0003534D File Offset: 0x0003354D
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

		// Token: 0x06001298 RID: 4760 RVA: 0x0003538C File Offset: 0x0003358C
		internal static bool IsStructuredOrStructuredCollectionType(this IEdmType edmType)
		{
			IEdmStructuredType edmStructuredType;
			return edmType.IsStructuredOrStructuredCollectionType(out edmStructuredType);
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x000353A4 File Offset: 0x000335A4
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

		// Token: 0x0600129A RID: 4762 RVA: 0x000353F2 File Offset: 0x000335F2
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

		// Token: 0x0600129B RID: 4763 RVA: 0x00035413 File Offset: 0x00033613
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

		// Token: 0x0600129C RID: 4764 RVA: 0x0003542B File Offset: 0x0003362B
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

		// Token: 0x0600129D RID: 4765 RVA: 0x00035444 File Offset: 0x00033644
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

		// Token: 0x0600129E RID: 4766 RVA: 0x000355BC File Offset: 0x000337BC
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
