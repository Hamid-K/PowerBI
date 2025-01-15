using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200001C RID: 28
	public static class EdmTypeSemantics
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00007831 File Offset: 0x00005A31
		public static bool IsCollection(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Collection;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007848 File Offset: 0x00005A48
		public static bool IsEntity(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Entity;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000785F File Offset: 0x00005A5F
		public static bool IsEntityReference(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.EntityReference;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007876 File Offset: 0x00005A76
		public static bool IsComplex(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Complex;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000788D File Offset: 0x00005A8D
		public static bool IsUntyped(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Untyped;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000078A4 File Offset: 0x00005AA4
		public static bool IsEnum(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Enum;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000078BB File Offset: 0x00005ABB
		public static bool IsTypeDefinition(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.TypeDefinition;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000078D4 File Offset: 0x00005AD4
		public static bool IsStructured(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			EdmTypeKind edmTypeKind = type.TypeKind();
			return edmTypeKind == EdmTypeKind.Entity || edmTypeKind == EdmTypeKind.Complex;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000078FF File Offset: 0x00005AFF
		public static bool IsStructured(this EdmTypeKind typeKind)
		{
			return typeKind == EdmTypeKind.Entity || typeKind == EdmTypeKind.Complex;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000790C File Offset: 0x00005B0C
		public static bool IsPrimitive(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Primitive;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007923 File Offset: 0x00005B23
		public static bool IsBinary(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Binary;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000793A File Offset: 0x00005B3A
		public static bool IsBoolean(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Boolean;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00007951 File Offset: 0x00005B51
		public static bool IsTemporal(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.IsTemporal();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000796C File Offset: 0x00005B6C
		public static bool IsTemporal(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind.IsTemporal();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000799C File Offset: 0x00005B9C
		public static bool IsTemporal(this EdmPrimitiveTypeKind typeKind)
		{
			return typeKind == EdmPrimitiveTypeKind.DateTimeOffset || typeKind == EdmPrimitiveTypeKind.Duration || typeKind == EdmPrimitiveTypeKind.TimeOfDay;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000079AF File Offset: 0x00005BAF
		public static bool IsDuration(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Duration;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000079C7 File Offset: 0x00005BC7
		public static bool IsDate(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Date;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000079DF File Offset: 0x00005BDF
		public static bool IsDateTimeOffset(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.DateTimeOffset;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000079F6 File Offset: 0x00005BF6
		public static bool IsDecimal(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.IsDecimal();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007A10 File Offset: 0x00005C10
		public static bool IsDecimal(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.Decimal;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007A40 File Offset: 0x00005C40
		public static bool IsFloating(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			EdmPrimitiveTypeKind edmPrimitiveTypeKind = type.PrimitiveKind();
			return edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Double || edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Single;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007A6C File Offset: 0x00005C6C
		public static bool IsSingle(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Single;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00007A84 File Offset: 0x00005C84
		public static bool IsTimeOfDay(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.TimeOfDay;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007A9C File Offset: 0x00005C9C
		public static bool IsDouble(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Double;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007AB3 File Offset: 0x00005CB3
		public static bool IsGuid(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Guid;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007ACC File Offset: 0x00005CCC
		public static bool IsSignedIntegral(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			switch (type.PrimitiveKind())
			{
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.Int32:
			case EdmPrimitiveTypeKind.Int64:
			case EdmPrimitiveTypeKind.SByte:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00007B09 File Offset: 0x00005D09
		public static bool IsSByte(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.SByte;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00007B21 File Offset: 0x00005D21
		public static bool IsInt16(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Int16;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007B38 File Offset: 0x00005D38
		public static bool IsInt32(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Int32;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007B50 File Offset: 0x00005D50
		public static bool IsInt64(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Int64;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007B68 File Offset: 0x00005D68
		public static bool IsIntegral(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			switch (type.PrimitiveKind())
			{
			case EdmPrimitiveTypeKind.Byte:
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.Int32:
			case EdmPrimitiveTypeKind.Int64:
			case EdmPrimitiveTypeKind.SByte:
				return true;
			}
			return false;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007BB9 File Offset: 0x00005DB9
		public static bool IsIntegral(this EdmPrimitiveTypeKind primitiveTypeKind)
		{
			switch (primitiveTypeKind)
			{
			case EdmPrimitiveTypeKind.Byte:
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.Int32:
			case EdmPrimitiveTypeKind.Int64:
			case EdmPrimitiveTypeKind.SByte:
				return true;
			}
			return false;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007BEC File Offset: 0x00005DEC
		public static bool IsByte(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Byte;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007C03 File Offset: 0x00005E03
		public static bool IsString(this IEdmTypeReference type)
		{
			return type.Definition.IsString();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007C10 File Offset: 0x00005E10
		public static bool IsString(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.String;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007C3F File Offset: 0x00005E3F
		public static bool IsStream(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.IsStream();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007C58 File Offset: 0x00005E58
		public static bool IsStream(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.Stream;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007C87 File Offset: 0x00005E87
		public static bool IsSpatial(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.IsSpatial();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007CA0 File Offset: 0x00005EA0
		public static bool IsSpatial(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind.IsSpatial();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007CD0 File Offset: 0x00005ED0
		public static bool IsSpatial(this EdmPrimitiveTypeKind typeKind)
		{
			switch (typeKind)
			{
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.GeographyPoint:
			case EdmPrimitiveTypeKind.GeographyLineString:
			case EdmPrimitiveTypeKind.GeographyPolygon:
			case EdmPrimitiveTypeKind.GeographyCollection:
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
			case EdmPrimitiveTypeKind.Geometry:
			case EdmPrimitiveTypeKind.GeometryPoint:
			case EdmPrimitiveTypeKind.GeometryLineString:
			case EdmPrimitiveTypeKind.GeometryPolygon:
			case EdmPrimitiveTypeKind.GeometryCollection:
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007D2C File Offset: 0x00005F2C
		public static bool IsGeography(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind.IsGeography();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007D5C File Offset: 0x00005F5C
		public static bool IsGeography(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.IsGeography();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007D75 File Offset: 0x00005F75
		public static bool IsGeography(this EdmPrimitiveTypeKind typeKind)
		{
			switch (typeKind)
			{
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.GeographyPoint:
			case EdmPrimitiveTypeKind.GeographyLineString:
			case EdmPrimitiveTypeKind.GeographyPolygon:
			case EdmPrimitiveTypeKind.GeographyCollection:
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007DA8 File Offset: 0x00005FA8
		public static bool IsGeometry(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind.IsGeometry();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007DD8 File Offset: 0x00005FD8
		public static bool IsGeometry(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.IsGeometry();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007DF1 File Offset: 0x00005FF1
		public static bool IsGeometry(this EdmPrimitiveTypeKind typeKind)
		{
			switch (typeKind)
			{
			case EdmPrimitiveTypeKind.Geometry:
			case EdmPrimitiveTypeKind.GeometryPoint:
			case EdmPrimitiveTypeKind.GeometryLineString:
			case EdmPrimitiveTypeKind.GeometryPolygon:
			case EdmPrimitiveTypeKind.GeometryCollection:
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007E24 File Offset: 0x00006024
		public static IEdmPrimitiveTypeReference AsPrimitive(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = type as IEdmPrimitiveTypeReference;
			if (edmPrimitiveTypeReference != null)
			{
				return edmPrimitiveTypeReference;
			}
			IEdmType definition = type.Definition;
			if (definition.TypeKind == EdmTypeKind.Primitive)
			{
				IEdmPrimitiveType edmPrimitiveType = definition as IEdmPrimitiveType;
				if (edmPrimitiveType != null)
				{
					switch (edmPrimitiveType.PrimitiveKind)
					{
					case EdmPrimitiveTypeKind.Binary:
						return type.AsBinary();
					case EdmPrimitiveTypeKind.Boolean:
					case EdmPrimitiveTypeKind.Byte:
					case EdmPrimitiveTypeKind.Double:
					case EdmPrimitiveTypeKind.Guid:
					case EdmPrimitiveTypeKind.Int16:
					case EdmPrimitiveTypeKind.Int32:
					case EdmPrimitiveTypeKind.Int64:
					case EdmPrimitiveTypeKind.SByte:
					case EdmPrimitiveTypeKind.Single:
					case EdmPrimitiveTypeKind.Stream:
					case EdmPrimitiveTypeKind.Date:
						return new EdmPrimitiveTypeReference(edmPrimitiveType, type.IsNullable);
					case EdmPrimitiveTypeKind.DateTimeOffset:
					case EdmPrimitiveTypeKind.Duration:
					case EdmPrimitiveTypeKind.TimeOfDay:
						return type.AsTemporal();
					case EdmPrimitiveTypeKind.Decimal:
						return type.AsDecimal();
					case EdmPrimitiveTypeKind.String:
						return type.AsString();
					case EdmPrimitiveTypeKind.Geography:
					case EdmPrimitiveTypeKind.GeographyPoint:
					case EdmPrimitiveTypeKind.GeographyLineString:
					case EdmPrimitiveTypeKind.GeographyPolygon:
					case EdmPrimitiveTypeKind.GeographyCollection:
					case EdmPrimitiveTypeKind.GeographyMultiPolygon:
					case EdmPrimitiveTypeKind.GeographyMultiLineString:
					case EdmPrimitiveTypeKind.GeographyMultiPoint:
					case EdmPrimitiveTypeKind.Geometry:
					case EdmPrimitiveTypeKind.GeometryPoint:
					case EdmPrimitiveTypeKind.GeometryLineString:
					case EdmPrimitiveTypeKind.GeometryPolygon:
					case EdmPrimitiveTypeKind.GeometryCollection:
					case EdmPrimitiveTypeKind.GeometryMultiPolygon:
					case EdmPrimitiveTypeKind.GeometryMultiLineString:
					case EdmPrimitiveTypeKind.GeometryMultiPoint:
						return type.AsSpatial();
					}
				}
			}
			else if (definition.TypeKind == EdmTypeKind.TypeDefinition)
			{
				IEdmPrimitiveType edmPrimitiveType2 = definition.UnderlyingType();
				IEdmTypeDefinitionReference edmTypeDefinitionReference = type as IEdmTypeDefinitionReference;
				if (edmTypeDefinitionReference == null)
				{
					return new EdmPrimitiveTypeReference(edmPrimitiveType2, type.IsNullable);
				}
				switch (edmPrimitiveType2.PrimitiveKind)
				{
				case EdmPrimitiveTypeKind.Binary:
					return new EdmBinaryTypeReference(edmPrimitiveType2, edmTypeDefinitionReference.IsNullable, edmTypeDefinitionReference.IsUnbounded, edmTypeDefinitionReference.MaxLength);
				case EdmPrimitiveTypeKind.DateTimeOffset:
				case EdmPrimitiveTypeKind.Duration:
				case EdmPrimitiveTypeKind.TimeOfDay:
					return new EdmTemporalTypeReference(edmPrimitiveType2, edmTypeDefinitionReference.IsNullable, edmTypeDefinitionReference.Precision);
				case EdmPrimitiveTypeKind.Decimal:
					return new EdmDecimalTypeReference(edmPrimitiveType2, edmTypeDefinitionReference.IsNullable, edmTypeDefinitionReference.Precision, edmTypeDefinitionReference.Scale);
				case EdmPrimitiveTypeKind.String:
					return new EdmStringTypeReference(edmPrimitiveType2, edmTypeDefinitionReference.IsNullable, edmTypeDefinitionReference.IsUnbounded, edmTypeDefinitionReference.MaxLength, edmTypeDefinitionReference.IsUnicode);
				case EdmPrimitiveTypeKind.Geography:
				case EdmPrimitiveTypeKind.GeographyPoint:
				case EdmPrimitiveTypeKind.GeographyLineString:
				case EdmPrimitiveTypeKind.GeographyPolygon:
				case EdmPrimitiveTypeKind.GeographyCollection:
				case EdmPrimitiveTypeKind.GeographyMultiPolygon:
				case EdmPrimitiveTypeKind.GeographyMultiLineString:
				case EdmPrimitiveTypeKind.GeographyMultiPoint:
				case EdmPrimitiveTypeKind.Geometry:
				case EdmPrimitiveTypeKind.GeometryPoint:
				case EdmPrimitiveTypeKind.GeometryLineString:
				case EdmPrimitiveTypeKind.GeometryPolygon:
				case EdmPrimitiveTypeKind.GeometryCollection:
				case EdmPrimitiveTypeKind.GeometryMultiPolygon:
				case EdmPrimitiveTypeKind.GeometryMultiLineString:
				case EdmPrimitiveTypeKind.GeometryMultiPoint:
					return new EdmSpatialTypeReference(edmPrimitiveType2, edmTypeDefinitionReference.IsNullable, edmTypeDefinitionReference.SpatialReferenceIdentifier);
				}
				return new EdmPrimitiveTypeReference(edmPrimitiveType2, edmTypeDefinitionReference.IsNullable);
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Primitive"));
			}
			return new BadPrimitiveTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000080DC File Offset: 0x000062DC
		public static IEdmCollectionTypeReference AsCollection(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmCollectionTypeReference edmCollectionTypeReference = type as IEdmCollectionTypeReference;
			if (edmCollectionTypeReference != null)
			{
				return edmCollectionTypeReference;
			}
			IEdmType definition = type.Definition;
			if (definition.TypeKind == EdmTypeKind.Collection)
			{
				return new EdmCollectionTypeReference((IEdmCollectionType)definition);
			}
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), type.FullName(), "Collection"));
			}
			return new EdmCollectionTypeReference(new BadCollectionType(list));
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008158 File Offset: 0x00006358
		public static IEdmStructuredTypeReference AsStructured(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmStructuredTypeReference edmStructuredTypeReference = type as IEdmStructuredTypeReference;
			if (edmStructuredTypeReference != null)
			{
				return edmStructuredTypeReference;
			}
			EdmTypeKind edmTypeKind = type.TypeKind();
			if (edmTypeKind == EdmTypeKind.Entity)
			{
				return type.AsEntity();
			}
			if (edmTypeKind != EdmTypeKind.Complex)
			{
				string text = type.FullName();
				List<EdmError> list = new List<EdmError>(type.TypeErrors());
				if (list.Count == 0)
				{
					list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Structured"));
				}
				return new BadEntityTypeReference(text, type.IsNullable, list);
			}
			return type.AsComplex();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000081DC File Offset: 0x000063DC
		public static IEdmEnumTypeReference AsEnum(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmEnumTypeReference edmEnumTypeReference = type as IEdmEnumTypeReference;
			if (edmEnumTypeReference != null)
			{
				return edmEnumTypeReference;
			}
			IEdmType definition = type.Definition;
			if (definition.TypeKind == EdmTypeKind.Enum)
			{
				return new EdmEnumTypeReference((IEdmEnumType)definition, type.IsNullable);
			}
			string text = type.FullName();
			return new EdmEnumTypeReference(new BadEnumType(text, EdmTypeSemantics.ConversionError(type.Location(), text, "Enum")), type.IsNullable);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000824C File Offset: 0x0000644C
		public static IEdmTypeDefinitionReference AsTypeDefinition(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmTypeDefinitionReference edmTypeDefinitionReference = type as IEdmTypeDefinitionReference;
			if (edmTypeDefinitionReference != null)
			{
				return edmTypeDefinitionReference;
			}
			IEdmType definition = type.Definition;
			if (definition.TypeKind == EdmTypeKind.TypeDefinition)
			{
				return new EdmTypeDefinitionReference((IEdmTypeDefinition)definition, type.IsNullable);
			}
			string text = type.FullName();
			return new EdmTypeDefinitionReference(new BadTypeDefinition(text, EdmTypeSemantics.ConversionError(type.Location(), text, "TypeDefinition")), type.IsNullable);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000082BC File Offset: 0x000064BC
		public static IEdmEntityTypeReference AsEntity(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmEntityTypeReference edmEntityTypeReference = type as IEdmEntityTypeReference;
			if (edmEntityTypeReference != null)
			{
				return edmEntityTypeReference;
			}
			IEdmType definition = type.Definition;
			if (definition.TypeKind == EdmTypeKind.Entity)
			{
				return new EdmEntityTypeReference((IEdmEntityType)definition, type.IsNullable);
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Entity"));
			}
			return new BadEntityTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008344 File Offset: 0x00006544
		public static IEdmEntityReferenceTypeReference AsEntityReference(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmEntityReferenceTypeReference edmEntityReferenceTypeReference = type as IEdmEntityReferenceTypeReference;
			if (edmEntityReferenceTypeReference != null)
			{
				return edmEntityReferenceTypeReference;
			}
			IEdmType definition = type.Definition;
			if (definition.TypeKind == EdmTypeKind.EntityReference)
			{
				return new EdmEntityReferenceTypeReference((IEdmEntityReferenceType)definition, type.IsNullable);
			}
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), type.FullName(), "EntityReference"));
			}
			return new EdmEntityReferenceTypeReference(new BadEntityReferenceType(list), type.IsNullable);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000083CC File Offset: 0x000065CC
		public static IEdmComplexTypeReference AsComplex(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmComplexTypeReference edmComplexTypeReference = type as IEdmComplexTypeReference;
			if (edmComplexTypeReference != null)
			{
				return edmComplexTypeReference;
			}
			IEdmType definition = type.Definition;
			if (definition.TypeKind == EdmTypeKind.Complex)
			{
				return new EdmComplexTypeReference((IEdmComplexType)definition, type.IsNullable);
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Complex"));
			}
			return new BadComplexTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00008454 File Offset: 0x00006654
		public static IEdmSpatialTypeReference AsSpatial(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmSpatialTypeReference edmSpatialTypeReference = type as IEdmSpatialTypeReference;
			if (edmSpatialTypeReference != null)
			{
				return edmSpatialTypeReference;
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Spatial"));
			}
			return new BadSpatialTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000084B8 File Offset: 0x000066B8
		public static IEdmTemporalTypeReference AsTemporal(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmTemporalTypeReference edmTemporalTypeReference = type as IEdmTemporalTypeReference;
			if (edmTemporalTypeReference != null)
			{
				return edmTemporalTypeReference;
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Temporal"));
			}
			return new BadTemporalTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000851C File Offset: 0x0000671C
		public static IEdmDecimalTypeReference AsDecimal(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmDecimalTypeReference edmDecimalTypeReference = type as IEdmDecimalTypeReference;
			if (edmDecimalTypeReference != null)
			{
				return edmDecimalTypeReference;
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Decimal"));
			}
			return new BadDecimalTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008580 File Offset: 0x00006780
		public static IEdmStringTypeReference AsString(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmStringTypeReference edmStringTypeReference = type as IEdmStringTypeReference;
			if (edmStringTypeReference != null)
			{
				return edmStringTypeReference;
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "String"));
			}
			return new BadStringTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000085E4 File Offset: 0x000067E4
		public static IEdmBinaryTypeReference AsBinary(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmBinaryTypeReference edmBinaryTypeReference = type as IEdmBinaryTypeReference;
			if (edmBinaryTypeReference != null)
			{
				return edmBinaryTypeReference;
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Binary"));
			}
			return new BadBinaryTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008648 File Offset: 0x00006848
		public static EdmPrimitiveTypeKind PrimitiveKind(this IEdmTypeReference type)
		{
			if (type == null)
			{
				return EdmPrimitiveTypeKind.None;
			}
			IEdmType definition = type.Definition;
			if (definition.TypeKind != EdmTypeKind.Primitive)
			{
				return EdmPrimitiveTypeKind.None;
			}
			return ((IEdmPrimitiveType)definition).PrimitiveKind;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008677 File Offset: 0x00006877
		public static bool InheritsFrom(this IEdmStructuredType type, IEdmStructuredType potentialBaseType)
		{
			for (;;)
			{
				type = type.BaseType;
				if (type != null && type.IsEquivalentTo(potentialBaseType))
				{
					break;
				}
				if (type == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008694 File Offset: 0x00006894
		public static bool IsOrInheritsFrom(this IEdmType thisType, IEdmType otherType)
		{
			if (thisType == null || otherType == null)
			{
				return false;
			}
			if (thisType.IsEquivalentTo(otherType))
			{
				return true;
			}
			EdmTypeKind typeKind = thisType.TypeKind;
			return typeKind == otherType.TypeKind && (typeKind == EdmTypeKind.Entity || typeKind == EdmTypeKind.Complex) && ((IEdmStructuredType)thisType).InheritsFrom((IEdmStructuredType)otherType);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000086DF File Offset: 0x000068DF
		public static bool IsOnSameTypeHierarchyLineWith(this IEdmType thisType, IEdmType otherType)
		{
			return thisType.IsOrInheritsFrom(otherType) || otherType.IsOrInheritsFrom(thisType);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000086F4 File Offset: 0x000068F4
		public static IEdmType AsActualType(this IEdmType type)
		{
			IEdmPrimitiveType edmPrimitiveType = type.UnderlyingType();
			IEdmType edmType = edmPrimitiveType;
			return edmType ?? type;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008710 File Offset: 0x00006910
		internal static IEdmPrimitiveTypeReference GetPrimitiveTypeReference(this IEdmPrimitiveType type, bool isNullable)
		{
			switch (type.PrimitiveKind)
			{
			case EdmPrimitiveTypeKind.Binary:
				return new EdmBinaryTypeReference(type, isNullable);
			case EdmPrimitiveTypeKind.Boolean:
			case EdmPrimitiveTypeKind.Byte:
			case EdmPrimitiveTypeKind.Double:
			case EdmPrimitiveTypeKind.Guid:
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.Int32:
			case EdmPrimitiveTypeKind.Int64:
			case EdmPrimitiveTypeKind.SByte:
			case EdmPrimitiveTypeKind.Single:
			case EdmPrimitiveTypeKind.Stream:
			case EdmPrimitiveTypeKind.Date:
				return new EdmPrimitiveTypeReference(type, isNullable);
			case EdmPrimitiveTypeKind.DateTimeOffset:
			case EdmPrimitiveTypeKind.Duration:
			case EdmPrimitiveTypeKind.TimeOfDay:
				return new EdmTemporalTypeReference(type, isNullable);
			case EdmPrimitiveTypeKind.Decimal:
				return new EdmDecimalTypeReference(type, isNullable);
			case EdmPrimitiveTypeKind.String:
				return new EdmStringTypeReference(type, isNullable);
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.GeographyPoint:
			case EdmPrimitiveTypeKind.GeographyLineString:
			case EdmPrimitiveTypeKind.GeographyPolygon:
			case EdmPrimitiveTypeKind.GeographyCollection:
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
			case EdmPrimitiveTypeKind.Geometry:
			case EdmPrimitiveTypeKind.GeometryPoint:
			case EdmPrimitiveTypeKind.GeometryLineString:
			case EdmPrimitiveTypeKind.GeometryPolygon:
			case EdmPrimitiveTypeKind.GeometryCollection:
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return new EdmSpatialTypeReference(type, isNullable);
			default:
				throw new InvalidOperationException(Strings.EdmPrimitive_UnexpectedKind);
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000087EC File Offset: 0x000069EC
		internal static IEdmTypeReference GetTypeReference(this IEdmType type, bool isNullable)
		{
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			if (edmPrimitiveType != null)
			{
				return edmPrimitiveType.GetPrimitiveTypeReference(isNullable);
			}
			IEdmComplexType edmComplexType = type as IEdmComplexType;
			if (edmComplexType != null)
			{
				return new EdmComplexTypeReference(edmComplexType, isNullable);
			}
			IEdmEntityType edmEntityType = type as IEdmEntityType;
			if (edmEntityType != null)
			{
				return new EdmEntityTypeReference(edmEntityType, isNullable);
			}
			IEdmEnumType edmEnumType = type as IEdmEnumType;
			if (edmEnumType != null)
			{
				return new EdmEnumTypeReference(edmEnumType, isNullable);
			}
			throw new InvalidOperationException(Strings.EdmType_UnexpectedEdmType);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000884B File Offset: 0x00006A4B
		internal static IEdmPrimitiveType UnderlyingType(this IEdmType type)
		{
			if (type == null || type.TypeKind != EdmTypeKind.TypeDefinition)
			{
				return null;
			}
			return ((IEdmTypeDefinition)type).UnderlyingType;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008866 File Offset: 0x00006A66
		internal static IEdmPrimitiveType UnderlyingType(this IEdmTypeReference type)
		{
			if (type == null)
			{
				return null;
			}
			return type.Definition.UnderlyingType();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008878 File Offset: 0x00006A78
		internal static IEdmTypeReference AsActualTypeReference(this IEdmTypeReference type)
		{
			if (type == null || type.TypeKind() != EdmTypeKind.TypeDefinition)
			{
				return type;
			}
			return type.AsPrimitive();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00008890 File Offset: 0x00006A90
		internal static bool CanSpecifyMaxLength(this IEdmPrimitiveType type)
		{
			EdmPrimitiveTypeKind primitiveKind = type.PrimitiveKind;
			return primitiveKind == EdmPrimitiveTypeKind.Binary || primitiveKind == EdmPrimitiveTypeKind.String || primitiveKind == EdmPrimitiveTypeKind.Stream;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000088B5 File Offset: 0x00006AB5
		private static IEnumerable<EdmError> ConversionError(EdmLocation location, string typeName, string typeKindName)
		{
			return new EdmError[]
			{
				new EdmError(location, EdmErrorCode.TypeSemanticsCouldNotConvertTypeReference, Strings.TypeSemantics_CouldNotConvertTypeReference(typeName ?? "UnnamedType", typeKindName))
			};
		}
	}
}
