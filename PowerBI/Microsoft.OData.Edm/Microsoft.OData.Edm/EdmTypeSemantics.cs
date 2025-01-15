using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000D1 RID: 209
	public static class EdmTypeSemantics
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x0000C4AE File Offset: 0x0000A6AE
		public static bool IsCollection(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Collection;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000C4C5 File Offset: 0x0000A6C5
		public static bool IsEntity(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Entity;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000C4DC File Offset: 0x0000A6DC
		public static bool IsPath(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Path;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000C4F4 File Offset: 0x0000A6F4
		public static bool IsEntityReference(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.EntityReference;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000C50B File Offset: 0x0000A70B
		public static bool IsComplex(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Complex;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000C522 File Offset: 0x0000A722
		public static bool IsUntyped(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Untyped;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000C539 File Offset: 0x0000A739
		public static bool IsEnum(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Enum;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000C550 File Offset: 0x0000A750
		public static bool IsTypeDefinition(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.TypeDefinition;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000C568 File Offset: 0x0000A768
		public static bool IsStructured(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			EdmTypeKind edmTypeKind = type.TypeKind();
			return edmTypeKind == EdmTypeKind.Entity || edmTypeKind == EdmTypeKind.Complex;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000C593 File Offset: 0x0000A793
		public static bool IsStructured(this EdmTypeKind typeKind)
		{
			return typeKind == EdmTypeKind.Entity || typeKind == EdmTypeKind.Complex;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		public static bool IsPrimitive(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Primitive;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000C5B7 File Offset: 0x0000A7B7
		public static bool IsBinary(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Binary;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000C5CE File Offset: 0x0000A7CE
		public static bool IsBoolean(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Boolean;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000C5E5 File Offset: 0x0000A7E5
		public static bool IsTemporal(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.IsTemporal();
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000C600 File Offset: 0x0000A800
		public static bool IsTemporal(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind.IsTemporal();
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000C630 File Offset: 0x0000A830
		public static bool IsTemporal(this EdmPrimitiveTypeKind typeKind)
		{
			return typeKind == EdmPrimitiveTypeKind.DateTimeOffset || typeKind == EdmPrimitiveTypeKind.Duration || typeKind == EdmPrimitiveTypeKind.TimeOfDay;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000C643 File Offset: 0x0000A843
		public static bool IsDuration(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Duration;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000C65B File Offset: 0x0000A85B
		public static bool IsDate(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Date;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000C673 File Offset: 0x0000A873
		public static bool IsDateTimeOffset(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.DateTimeOffset;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000C68A File Offset: 0x0000A88A
		public static bool IsDecimal(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.IsDecimal();
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
		public static bool IsDecimal(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.Decimal;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		public static bool IsFloating(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			EdmPrimitiveTypeKind edmPrimitiveTypeKind = type.PrimitiveKind();
			return edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Double || edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Single;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000C700 File Offset: 0x0000A900
		public static bool IsSingle(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Single;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000C718 File Offset: 0x0000A918
		public static bool IsTimeOfDay(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.TimeOfDay;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000C730 File Offset: 0x0000A930
		public static bool IsDouble(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Double;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000C747 File Offset: 0x0000A947
		public static bool IsGuid(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Guid;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000C760 File Offset: 0x0000A960
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

		// Token: 0x060004FE RID: 1278 RVA: 0x0000C79D File Offset: 0x0000A99D
		public static bool IsSByte(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.SByte;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000C7B5 File Offset: 0x0000A9B5
		public static bool IsInt16(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Int16;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000C7CC File Offset: 0x0000A9CC
		public static bool IsInt32(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Int32;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
		public static bool IsInt64(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Int64;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000C7FC File Offset: 0x0000A9FC
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

		// Token: 0x06000503 RID: 1283 RVA: 0x0000C84D File Offset: 0x0000AA4D
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

		// Token: 0x06000504 RID: 1284 RVA: 0x0000C880 File Offset: 0x0000AA80
		public static bool IsByte(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Byte;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000C897 File Offset: 0x0000AA97
		public static bool IsString(this IEdmTypeReference type)
		{
			return type.Definition.IsString();
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
		public static bool IsString(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.String;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000C8D3 File Offset: 0x0000AAD3
		public static bool IsUntyped(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			return type.TypeKind == EdmTypeKind.Untyped;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		public static bool IsBinary(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.Binary;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000C91A File Offset: 0x0000AB1A
		public static bool IsStream(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.IsStream();
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000C934 File Offset: 0x0000AB34
		public static bool IsStream(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.Stream;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000C963 File Offset: 0x0000AB63
		public static bool IsSpatial(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.IsSpatial();
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000C97C File Offset: 0x0000AB7C
		public static bool IsSpatial(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind.IsSpatial();
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000C9AC File Offset: 0x0000ABAC
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

		// Token: 0x0600050E RID: 1294 RVA: 0x0000CA08 File Offset: 0x0000AC08
		public static bool IsGeography(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind.IsGeography();
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000CA38 File Offset: 0x0000AC38
		public static bool IsGeography(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.IsGeography();
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000CA51 File Offset: 0x0000AC51
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

		// Token: 0x06000511 RID: 1297 RVA: 0x0000CA84 File Offset: 0x0000AC84
		public static bool IsGeometry(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind.IsGeometry();
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		public static bool IsGeometry(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.IsGeometry();
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000CACD File Offset: 0x0000ACCD
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

		// Token: 0x06000514 RID: 1300 RVA: 0x0000CB00 File Offset: 0x0000AD00
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
					case EdmPrimitiveTypeKind.PrimitiveType:
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

		// Token: 0x06000515 RID: 1301 RVA: 0x0000CDBC File Offset: 0x0000AFBC
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

		// Token: 0x06000516 RID: 1302 RVA: 0x0000CE38 File Offset: 0x0000B038
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

		// Token: 0x06000517 RID: 1303 RVA: 0x0000CEBC File Offset: 0x0000B0BC
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

		// Token: 0x06000518 RID: 1304 RVA: 0x0000CF2C File Offset: 0x0000B12C
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

		// Token: 0x06000519 RID: 1305 RVA: 0x0000CF9C File Offset: 0x0000B19C
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

		// Token: 0x0600051A RID: 1306 RVA: 0x0000D024 File Offset: 0x0000B224
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

		// Token: 0x0600051B RID: 1307 RVA: 0x0000D0AC File Offset: 0x0000B2AC
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

		// Token: 0x0600051C RID: 1308 RVA: 0x0000D134 File Offset: 0x0000B334
		public static IEdmPathTypeReference AsPath(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmPathTypeReference edmPathTypeReference = type as IEdmPathTypeReference;
			if (edmPathTypeReference != null)
			{
				return edmPathTypeReference;
			}
			IEdmType definition = type.Definition;
			if (definition.TypeKind == EdmTypeKind.Path)
			{
				return new EdmPathTypeReference((IEdmPathType)definition, type.IsNullable);
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Path"));
			}
			return new BadPathTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000D1BC File Offset: 0x0000B3BC
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

		// Token: 0x0600051E RID: 1310 RVA: 0x0000D220 File Offset: 0x0000B420
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

		// Token: 0x0600051F RID: 1311 RVA: 0x0000D284 File Offset: 0x0000B484
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

		// Token: 0x06000520 RID: 1312 RVA: 0x0000D2E8 File Offset: 0x0000B4E8
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

		// Token: 0x06000521 RID: 1313 RVA: 0x0000D34C File Offset: 0x0000B54C
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

		// Token: 0x06000522 RID: 1314 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
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

		// Token: 0x06000523 RID: 1315 RVA: 0x0000D3DF File Offset: 0x0000B5DF
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

		// Token: 0x06000524 RID: 1316 RVA: 0x0000D3FC File Offset: 0x0000B5FC
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

		// Token: 0x06000525 RID: 1317 RVA: 0x0000D447 File Offset: 0x0000B647
		public static bool IsOnSameTypeHierarchyLineWith(this IEdmType thisType, IEdmType otherType)
		{
			return thisType.IsOrInheritsFrom(otherType) || otherType.IsOrInheritsFrom(thisType);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000D45C File Offset: 0x0000B65C
		public static IEdmType AsActualType(this IEdmType type)
		{
			IEdmPrimitiveType edmPrimitiveType = type.UnderlyingType();
			IEdmType edmType = edmPrimitiveType;
			return edmType ?? type;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000D478 File Offset: 0x0000B678
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
			case EdmPrimitiveTypeKind.PrimitiveType:
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

		// Token: 0x06000528 RID: 1320 RVA: 0x0000D558 File Offset: 0x0000B758
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
			IEdmPathType edmPathType = type as IEdmPathType;
			if (edmPathType != null)
			{
				return new EdmPathTypeReference(edmPathType, isNullable);
			}
			throw new InvalidOperationException(Strings.EdmType_UnexpectedEdmType);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000D5CC File Offset: 0x0000B7CC
		internal static IEdmPrimitiveType UnderlyingType(this IEdmType type)
		{
			if (type == null || type.TypeKind != EdmTypeKind.TypeDefinition)
			{
				return null;
			}
			return ((IEdmTypeDefinition)type).UnderlyingType;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000D5E7 File Offset: 0x0000B7E7
		internal static IEdmPrimitiveType UnderlyingType(this IEdmTypeReference type)
		{
			if (type == null)
			{
				return null;
			}
			return type.Definition.UnderlyingType();
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000D5F9 File Offset: 0x0000B7F9
		internal static IEdmTypeReference AsActualTypeReference(this IEdmTypeReference type)
		{
			if (type == null || type.TypeKind() != EdmTypeKind.TypeDefinition)
			{
				return type;
			}
			return type.AsPrimitive();
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000D610 File Offset: 0x0000B810
		internal static bool CanSpecifyMaxLength(this IEdmPrimitiveType type)
		{
			EdmPrimitiveTypeKind primitiveKind = type.PrimitiveKind;
			return primitiveKind == EdmPrimitiveTypeKind.Binary || primitiveKind == EdmPrimitiveTypeKind.String || primitiveKind == EdmPrimitiveTypeKind.Stream;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000D635 File Offset: 0x0000B835
		private static IEnumerable<EdmError> ConversionError(EdmLocation location, string typeName, string typeKindName)
		{
			return new EdmError[]
			{
				new EdmError(location, EdmErrorCode.TypeSemanticsCouldNotConvertTypeReference, Strings.TypeSemantics_CouldNotConvertTypeReference(typeName ?? "UnnamedType", typeKindName))
			};
		}
	}
}
