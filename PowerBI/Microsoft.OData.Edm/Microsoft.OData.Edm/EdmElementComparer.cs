using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000006 RID: 6
	public static class EdmElementComparer
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000212C File Offset: 0x0000032C
		public static bool IsEquivalentTo(this IEdmType thisType, IEdmType otherType)
		{
			if (thisType == otherType)
			{
				return true;
			}
			if (thisType == null || otherType == null)
			{
				return false;
			}
			thisType = thisType.AsActualType();
			otherType = otherType.AsActualType();
			if (thisType.TypeKind != otherType.TypeKind)
			{
				return false;
			}
			switch (thisType.TypeKind)
			{
			case EdmTypeKind.None:
				return otherType.TypeKind == EdmTypeKind.None;
			case EdmTypeKind.Primitive:
				return ((IEdmPrimitiveType)thisType).IsEquivalentTo((IEdmPrimitiveType)otherType);
			case EdmTypeKind.Entity:
			case EdmTypeKind.Complex:
				return ((IEdmSchemaType)thisType).IsEquivalentTo((IEdmSchemaType)otherType);
			case EdmTypeKind.Collection:
				return ((IEdmCollectionType)thisType).IsEquivalentTo((IEdmCollectionType)otherType);
			case EdmTypeKind.EntityReference:
				return ((IEdmEntityReferenceType)thisType).IsEquivalentTo((IEdmEntityReferenceType)otherType);
			case EdmTypeKind.Enum:
				return ((IEdmEnumType)thisType).IsEquivalentTo((IEdmEnumType)otherType);
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_TypeKind(thisType.TypeKind));
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000220C File Offset: 0x0000040C
		public static bool IsEquivalentTo(this IEdmTypeReference thisType, IEdmTypeReference otherType)
		{
			if (thisType == otherType)
			{
				return true;
			}
			if (thisType == null || otherType == null)
			{
				return false;
			}
			thisType = thisType.AsActualTypeReference();
			otherType = otherType.AsActualTypeReference();
			EdmTypeKind edmTypeKind = thisType.TypeKind();
			if (edmTypeKind != otherType.TypeKind())
			{
				return false;
			}
			if (edmTypeKind == EdmTypeKind.Primitive)
			{
				return ((IEdmPrimitiveTypeReference)thisType).IsEquivalentTo((IEdmPrimitiveTypeReference)otherType);
			}
			return thisType.IsNullable == otherType.IsNullable && thisType.Definition.IsEquivalentTo(otherType.Definition);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002280 File Offset: 0x00000480
		private static bool IsEquivalentTo(this IEdmPrimitiveType thisType, IEdmPrimitiveType otherType)
		{
			return thisType.PrimitiveKind == otherType.PrimitiveKind && thisType.FullName() == otherType.FullName();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022A3 File Offset: 0x000004A3
		private static bool IsEquivalentTo(this IEdmEnumType thisType, IEdmEnumType otherType)
		{
			return thisType.FullName() == otherType.FullName() && thisType.UnderlyingType.IsEquivalentTo(otherType.UnderlyingType) && thisType.IsFlags == otherType.IsFlags;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022DB File Offset: 0x000004DB
		private static bool IsEquivalentTo(this IEdmSchemaType thisType, IEdmSchemaType otherType)
		{
			return thisType == otherType;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022E1 File Offset: 0x000004E1
		private static bool IsEquivalentTo(this IEdmCollectionType thisType, IEdmCollectionType otherType)
		{
			return thisType.ElementType.IsEquivalentTo(otherType.ElementType);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022F4 File Offset: 0x000004F4
		private static bool IsEquivalentTo(this IEdmEntityReferenceType thisType, IEdmEntityReferenceType otherType)
		{
			return thisType.EntityType.IsEquivalentTo(otherType.EntityType);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002308 File Offset: 0x00000508
		private static bool IsEquivalentTo(this IEdmPrimitiveTypeReference thisType, IEdmPrimitiveTypeReference otherType)
		{
			EdmPrimitiveTypeKind edmPrimitiveTypeKind = thisType.PrimitiveKind();
			if (edmPrimitiveTypeKind != otherType.PrimitiveKind())
			{
				return false;
			}
			switch (edmPrimitiveTypeKind)
			{
			case EdmPrimitiveTypeKind.Binary:
				return (thisType as IEdmBinaryTypeReference).IsEquivalentTo(otherType as IEdmBinaryTypeReference);
			case EdmPrimitiveTypeKind.Boolean:
			case EdmPrimitiveTypeKind.Byte:
				goto IL_0082;
			case EdmPrimitiveTypeKind.DateTimeOffset:
				break;
			case EdmPrimitiveTypeKind.Decimal:
				return (thisType as IEdmDecimalTypeReference).IsEquivalentTo(otherType as IEdmDecimalTypeReference);
			default:
				if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.String)
				{
					return (thisType as IEdmStringTypeReference).IsEquivalentTo(otherType as IEdmStringTypeReference);
				}
				if (edmPrimitiveTypeKind != EdmPrimitiveTypeKind.Duration)
				{
					goto IL_0082;
				}
				break;
			}
			return (thisType as IEdmTemporalTypeReference).IsEquivalentTo(otherType as IEdmTemporalTypeReference);
			IL_0082:
			if (edmPrimitiveTypeKind.IsSpatial())
			{
				return (thisType as IEdmSpatialTypeReference).IsEquivalentTo(otherType as IEdmSpatialTypeReference);
			}
			return thisType.IsNullable == otherType.IsNullable && thisType.Definition.IsEquivalentTo(otherType.Definition);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023D4 File Offset: 0x000005D4
		private static bool IsEquivalentTo(this IEdmBinaryTypeReference thisType, IEdmBinaryTypeReference otherType)
		{
			return thisType != null && otherType != null && thisType.IsNullable == otherType.IsNullable && thisType.IsUnbounded == otherType.IsUnbounded && thisType.MaxLength == otherType.MaxLength;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002438 File Offset: 0x00000638
		private static bool IsEquivalentTo(this IEdmDecimalTypeReference thisType, IEdmDecimalTypeReference otherType)
		{
			return thisType != null && otherType != null && thisType.IsNullable == otherType.IsNullable && thisType.Precision == otherType.Precision && thisType.Scale == otherType.Scale;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024C0 File Offset: 0x000006C0
		private static bool IsEquivalentTo(this IEdmTemporalTypeReference thisType, IEdmTemporalTypeReference otherType)
		{
			return thisType != null && otherType != null && thisType.TypeKind() == otherType.TypeKind() && thisType.IsNullable == otherType.IsNullable && thisType.Precision == otherType.Precision;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002524 File Offset: 0x00000724
		private static bool IsEquivalentTo(this IEdmStringTypeReference thisType, IEdmStringTypeReference otherType)
		{
			return thisType != null && otherType != null && thisType.IsNullable == otherType.IsNullable && thisType.IsUnbounded == otherType.IsUnbounded && thisType.MaxLength == otherType.MaxLength && thisType.IsUnicode == otherType.IsUnicode;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025C0 File Offset: 0x000007C0
		private static bool IsEquivalentTo(this IEdmSpatialTypeReference thisType, IEdmSpatialTypeReference otherType)
		{
			return thisType != null && otherType != null && thisType.IsNullable == otherType.IsNullable && thisType.SpatialReferenceIdentifier == otherType.SpatialReferenceIdentifier;
		}
	}
}
