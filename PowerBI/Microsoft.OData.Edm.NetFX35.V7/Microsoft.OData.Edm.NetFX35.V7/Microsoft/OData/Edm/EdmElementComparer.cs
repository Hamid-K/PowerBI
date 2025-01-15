using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000007 RID: 7
	public static class EdmElementComparer
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002164 File Offset: 0x00000364
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
			case EdmTypeKind.Enum:
				return ((IEdmSchemaType)thisType).IsEquivalentTo((IEdmSchemaType)otherType);
			case EdmTypeKind.Collection:
				return ((IEdmCollectionType)thisType).IsEquivalentTo((IEdmCollectionType)otherType);
			case EdmTypeKind.EntityReference:
				return ((IEdmEntityReferenceType)thisType).IsEquivalentTo((IEdmEntityReferenceType)otherType);
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_TypeKind(thisType.TypeKind));
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002234 File Offset: 0x00000434
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

		// Token: 0x06000012 RID: 18 RVA: 0x000022A8 File Offset: 0x000004A8
		private static bool IsEquivalentTo(this IEdmPrimitiveType thisType, IEdmPrimitiveType otherType)
		{
			return thisType.PrimitiveKind == otherType.PrimitiveKind && thisType.FullName() == otherType.FullName();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022CB File Offset: 0x000004CB
		private static bool IsEquivalentTo(this IEdmSchemaType thisType, IEdmSchemaType otherType)
		{
			return thisType == otherType;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022D1 File Offset: 0x000004D1
		private static bool IsEquivalentTo(this IEdmCollectionType thisType, IEdmCollectionType otherType)
		{
			return thisType.ElementType.IsEquivalentTo(otherType.ElementType);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022E4 File Offset: 0x000004E4
		private static bool IsEquivalentTo(this IEdmEntityReferenceType thisType, IEdmEntityReferenceType otherType)
		{
			return thisType.EntityType.IsEquivalentTo(otherType.EntityType);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022F8 File Offset: 0x000004F8
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

		// Token: 0x06000017 RID: 23 RVA: 0x000023C4 File Offset: 0x000005C4
		private static bool IsEquivalentTo(this IEdmBinaryTypeReference thisType, IEdmBinaryTypeReference otherType)
		{
			return thisType != null && otherType != null && thisType.IsNullable == otherType.IsNullable && thisType.IsUnbounded == otherType.IsUnbounded && thisType.MaxLength == otherType.MaxLength;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002428 File Offset: 0x00000628
		private static bool IsEquivalentTo(this IEdmDecimalTypeReference thisType, IEdmDecimalTypeReference otherType)
		{
			return thisType != null && otherType != null && thisType.IsNullable == otherType.IsNullable && thisType.Precision == otherType.Precision && thisType.Scale == otherType.Scale;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024B0 File Offset: 0x000006B0
		private static bool IsEquivalentTo(this IEdmTemporalTypeReference thisType, IEdmTemporalTypeReference otherType)
		{
			return thisType != null && otherType != null && thisType.TypeKind() == otherType.TypeKind() && thisType.IsNullable == otherType.IsNullable && thisType.Precision == otherType.Precision;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002514 File Offset: 0x00000714
		private static bool IsEquivalentTo(this IEdmStringTypeReference thisType, IEdmStringTypeReference otherType)
		{
			return thisType != null && otherType != null && thisType.IsNullable == otherType.IsNullable && thisType.IsUnbounded == otherType.IsUnbounded && thisType.MaxLength == otherType.MaxLength && thisType.IsUnicode == otherType.IsUnicode;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025B0 File Offset: 0x000007B0
		private static bool IsEquivalentTo(this IEdmSpatialTypeReference thisType, IEdmSpatialTypeReference otherType)
		{
			return thisType != null && otherType != null && thisType.IsNullable == otherType.IsNullable && thisType.SpatialReferenceIdentifier == otherType.SpatialReferenceIdentifier;
		}
	}
}
