using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000EA RID: 234
	public static class EdmElementComparer
	{
		// Token: 0x060004B6 RID: 1206 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
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

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000C774 File Offset: 0x0000A974
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

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		private static bool IsEquivalentTo(this IEdmPrimitiveType thisType, IEdmPrimitiveType otherType)
		{
			return thisType.PrimitiveKind == otherType.PrimitiveKind && thisType.FullName() == otherType.FullName();
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000C80B File Offset: 0x0000AA0B
		private static bool IsEquivalentTo(this IEdmSchemaType thisType, IEdmSchemaType otherType)
		{
			return object.ReferenceEquals(thisType, otherType);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000C814 File Offset: 0x0000AA14
		private static bool IsEquivalentTo(this IEdmCollectionType thisType, IEdmCollectionType otherType)
		{
			return thisType.ElementType.IsEquivalentTo(otherType.ElementType);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000C827 File Offset: 0x0000AA27
		private static bool IsEquivalentTo(this IEdmEntityReferenceType thisType, IEdmEntityReferenceType otherType)
		{
			return thisType.EntityType.IsEquivalentTo(otherType.EntityType);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000C83C File Offset: 0x0000AA3C
		private static bool IsEquivalentTo(this IEdmPrimitiveTypeReference thisType, IEdmPrimitiveTypeReference otherType)
		{
			EdmPrimitiveTypeKind edmPrimitiveTypeKind = thisType.PrimitiveKind();
			if (edmPrimitiveTypeKind != otherType.PrimitiveKind())
			{
				return false;
			}
			EdmPrimitiveTypeKind edmPrimitiveTypeKind2 = edmPrimitiveTypeKind;
			switch (edmPrimitiveTypeKind2)
			{
			case EdmPrimitiveTypeKind.Binary:
				return (thisType as IEdmBinaryTypeReference).IsEquivalentTo(otherType as IEdmBinaryTypeReference);
			case EdmPrimitiveTypeKind.Boolean:
			case EdmPrimitiveTypeKind.Byte:
				goto IL_008F;
			case EdmPrimitiveTypeKind.DateTimeOffset:
				break;
			case EdmPrimitiveTypeKind.Decimal:
				return (thisType as IEdmDecimalTypeReference).IsEquivalentTo(otherType as IEdmDecimalTypeReference);
			default:
				switch (edmPrimitiveTypeKind2)
				{
				case EdmPrimitiveTypeKind.String:
					return (thisType as IEdmStringTypeReference).IsEquivalentTo(otherType as IEdmStringTypeReference);
				case EdmPrimitiveTypeKind.Stream:
					goto IL_008F;
				case EdmPrimitiveTypeKind.Duration:
					break;
				default:
					goto IL_008F;
				}
				break;
			}
			return (thisType as IEdmTemporalTypeReference).IsEquivalentTo(otherType as IEdmTemporalTypeReference);
			IL_008F:
			if (edmPrimitiveTypeKind.IsSpatial())
			{
				return (thisType as IEdmSpatialTypeReference).IsEquivalentTo(otherType as IEdmSpatialTypeReference);
			}
			return thisType.IsNullable == otherType.IsNullable && thisType.Definition.IsEquivalentTo(otherType.Definition);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000C914 File Offset: 0x0000AB14
		private static bool IsEquivalentTo(this IEdmBinaryTypeReference thisType, IEdmBinaryTypeReference otherType)
		{
			return thisType != null && otherType != null && thisType.IsNullable == otherType.IsNullable && thisType.IsUnbounded == otherType.IsUnbounded && thisType.MaxLength == otherType.MaxLength;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000C978 File Offset: 0x0000AB78
		private static bool IsEquivalentTo(this IEdmDecimalTypeReference thisType, IEdmDecimalTypeReference otherType)
		{
			return thisType != null && otherType != null && thisType.IsNullable == otherType.IsNullable && thisType.Precision == otherType.Precision && thisType.Scale == otherType.Scale;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000CA00 File Offset: 0x0000AC00
		private static bool IsEquivalentTo(this IEdmTemporalTypeReference thisType, IEdmTemporalTypeReference otherType)
		{
			return thisType != null && otherType != null && thisType.TypeKind() == otherType.TypeKind() && thisType.IsNullable == otherType.IsNullable && thisType.Precision == otherType.Precision;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000CA64 File Offset: 0x0000AC64
		private static bool IsEquivalentTo(this IEdmStringTypeReference thisType, IEdmStringTypeReference otherType)
		{
			return thisType != null && otherType != null && thisType.IsNullable == otherType.IsNullable && thisType.IsUnbounded == otherType.IsUnbounded && thisType.MaxLength == otherType.MaxLength && thisType.IsUnicode == otherType.IsUnicode;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000CB00 File Offset: 0x0000AD00
		private static bool IsEquivalentTo(this IEdmSpatialTypeReference thisType, IEdmSpatialTypeReference otherType)
		{
			return thisType != null && otherType != null && thisType.IsNullable == otherType.IsNullable && thisType.SpatialReferenceIdentifier == otherType.SpatialReferenceIdentifier;
		}
	}
}
