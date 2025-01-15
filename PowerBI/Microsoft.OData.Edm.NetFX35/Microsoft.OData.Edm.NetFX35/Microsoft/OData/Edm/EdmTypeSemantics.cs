using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000219 RID: 537
	public static class EdmTypeSemantics
	{
		// Token: 0x06000CA2 RID: 3234 RVA: 0x000237DD File Offset: 0x000219DD
		public static bool IsCollection(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Collection;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000237F4 File Offset: 0x000219F4
		public static bool IsEntity(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Entity;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0002380B File Offset: 0x00021A0B
		public static bool IsEntityReference(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.EntityReference;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00023822 File Offset: 0x00021A22
		public static bool IsComplex(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Complex;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00023839 File Offset: 0x00021A39
		public static bool IsEnum(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Enum;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00023850 File Offset: 0x00021A50
		public static bool IsTypeDefinition(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.TypeDefinition;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00023868 File Offset: 0x00021A68
		public static bool IsStructured(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			switch (type.TypeKind())
			{
			case EdmTypeKind.Entity:
			case EdmTypeKind.Complex:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x000238A0 File Offset: 0x00021AA0
		public static bool IsStructured(this EdmTypeKind typeKind)
		{
			switch (typeKind)
			{
			case EdmTypeKind.Entity:
			case EdmTypeKind.Complex:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x000238C4 File Offset: 0x00021AC4
		public static bool IsPrimitive(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Primitive;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x000238DB File Offset: 0x00021ADB
		public static bool IsBinary(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Binary;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000238F2 File Offset: 0x00021AF2
		public static bool IsBoolean(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Boolean;
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00023909 File Offset: 0x00021B09
		public static bool IsTemporal(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind().IsTemporal();
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00023924 File Offset: 0x00021B24
		public static bool IsTemporal(this EdmPrimitiveTypeKind typeKind)
		{
			return typeKind == EdmPrimitiveTypeKind.DateTimeOffset || typeKind == EdmPrimitiveTypeKind.Duration || typeKind == EdmPrimitiveTypeKind.TimeOfDay;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00023944 File Offset: 0x00021B44
		public static bool IsDuration(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Duration;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002395C File Offset: 0x00021B5C
		public static bool IsDate(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Date;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00023974 File Offset: 0x00021B74
		public static bool IsDateTimeOffset(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.DateTimeOffset;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0002398B File Offset: 0x00021B8B
		public static bool IsDecimal(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Decimal;
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x000239A4 File Offset: 0x00021BA4
		public static bool IsFloating(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			EdmPrimitiveTypeKind edmPrimitiveTypeKind = type.PrimitiveKind();
			return edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Double || edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Single;
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x000239D0 File Offset: 0x00021BD0
		public static bool IsSingle(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Single;
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x000239E8 File Offset: 0x00021BE8
		public static bool IsTimeOfDay(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.TimeOfDay;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00023A00 File Offset: 0x00021C00
		public static bool IsDouble(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Double;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00023A17 File Offset: 0x00021C17
		public static bool IsGuid(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Guid;
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00023A30 File Offset: 0x00021C30
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

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00023A6D File Offset: 0x00021C6D
		public static bool IsSByte(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.SByte;
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00023A85 File Offset: 0x00021C85
		public static bool IsInt16(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Int16;
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00023A9C File Offset: 0x00021C9C
		public static bool IsInt32(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Int32;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00023AB4 File Offset: 0x00021CB4
		public static bool IsInt64(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Int64;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00023ACC File Offset: 0x00021CCC
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

		// Token: 0x06000CBE RID: 3262 RVA: 0x00023B20 File Offset: 0x00021D20
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

		// Token: 0x06000CBF RID: 3263 RVA: 0x00023B60 File Offset: 0x00021D60
		public static bool IsByte(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Byte;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00023B77 File Offset: 0x00021D77
		public static bool IsString(this IEdmTypeReference type)
		{
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.String;
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00023B83 File Offset: 0x00021D83
		public static bool IsStream(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Stream;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00023B9B File Offset: 0x00021D9B
		public static bool IsSpatial(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.IsSpatial();
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00023BB4 File Offset: 0x00021DB4
		public static bool IsSpatial(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind.IsSpatial();
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00023BE4 File Offset: 0x00021DE4
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

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00023C44 File Offset: 0x00021E44
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
				return new EdmPrimitiveTypeReference(definition.UnderlyingType(), type.IsNullable);
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Primitive"));
			}
			return new BadPrimitiveTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00023DB0 File Offset: 0x00021FB0
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

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00023E2C File Offset: 0x0002202C
		public static IEdmStructuredTypeReference AsStructured(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmStructuredTypeReference edmStructuredTypeReference = type as IEdmStructuredTypeReference;
			if (edmStructuredTypeReference != null)
			{
				return edmStructuredTypeReference;
			}
			switch (type.TypeKind())
			{
			case EdmTypeKind.Entity:
				return type.AsEntity();
			case EdmTypeKind.Complex:
				return type.AsComplex();
			default:
			{
				string text = type.FullName();
				List<EdmError> list = new List<EdmError>(type.TypeErrors());
				if (list.Count == 0)
				{
					list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Structured"));
				}
				return new BadEntityTypeReference(text, type.IsNullable, list);
			}
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00023EB8 File Offset: 0x000220B8
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

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00023F28 File Offset: 0x00022128
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

		// Token: 0x06000CCA RID: 3274 RVA: 0x00023F98 File Offset: 0x00022198
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

		// Token: 0x06000CCB RID: 3275 RVA: 0x00024020 File Offset: 0x00022220
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

		// Token: 0x06000CCC RID: 3276 RVA: 0x000240A8 File Offset: 0x000222A8
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

		// Token: 0x06000CCD RID: 3277 RVA: 0x00024130 File Offset: 0x00022330
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

		// Token: 0x06000CCE RID: 3278 RVA: 0x00024194 File Offset: 0x00022394
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

		// Token: 0x06000CCF RID: 3279 RVA: 0x000241F8 File Offset: 0x000223F8
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

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0002425C File Offset: 0x0002245C
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

		// Token: 0x06000CD1 RID: 3281 RVA: 0x000242C0 File Offset: 0x000224C0
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

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00024324 File Offset: 0x00022524
		public static EdmPrimitiveTypeKind PrimitiveKind(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmType definition = type.Definition;
			if (definition.TypeKind != EdmTypeKind.Primitive)
			{
				return EdmPrimitiveTypeKind.None;
			}
			return ((IEdmPrimitiveType)definition).PrimitiveKind;
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002435A File Offset: 0x0002255A
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

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00024378 File Offset: 0x00022578
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

		// Token: 0x06000CD5 RID: 3285 RVA: 0x000243C4 File Offset: 0x000225C4
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

		// Token: 0x06000CD6 RID: 3286 RVA: 0x000244A0 File Offset: 0x000226A0
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

		// Token: 0x06000CD7 RID: 3287 RVA: 0x000244FF File Offset: 0x000226FF
		internal static IEdmPrimitiveType UnderlyingType(this IEdmType type)
		{
			if (type == null || type.TypeKind != EdmTypeKind.TypeDefinition)
			{
				return null;
			}
			return ((IEdmTypeDefinition)type).UnderlyingType;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002451C File Offset: 0x0002271C
		internal static IEdmType AsActualType(this IEdmType type)
		{
			IEdmPrimitiveType edmPrimitiveType = type.UnderlyingType();
			return edmPrimitiveType ?? type;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00024536 File Offset: 0x00022736
		internal static IEdmTypeReference AsActualTypeReference(this IEdmTypeReference type)
		{
			if (type == null || type.TypeKind() != EdmTypeKind.TypeDefinition)
			{
				return type;
			}
			return type.AsPrimitive();
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0002454C File Offset: 0x0002274C
		private static IEnumerable<EdmError> ConversionError(EdmLocation location, string typeName, string typeKindName)
		{
			return new EdmError[]
			{
				new EdmError(location, EdmErrorCode.TypeSemanticsCouldNotConvertTypeReference, Strings.TypeSemantics_CouldNotConvertTypeReference(typeName ?? "UnnamedType", typeKindName))
			};
		}
	}
}
