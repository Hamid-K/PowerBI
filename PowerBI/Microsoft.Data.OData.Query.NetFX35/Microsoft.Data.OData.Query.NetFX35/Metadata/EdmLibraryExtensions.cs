using System;
using System.Collections.Generic;
using System.IO;
using System.Spatial;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Metadata
{
	// Token: 0x02000021 RID: 33
	internal static class EdmLibraryExtensions
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00003CF4 File Offset: 0x00001EF4
		static EdmLibraryExtensions()
		{
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(bool), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Boolean), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(bool?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Boolean), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(byte), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Byte), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(byte?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Byte), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(DateTime), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.DateTime), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(DateTime?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.DateTime), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(DateTimeOffset), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.DateTimeOffset), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(DateTimeOffset?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.DateTimeOffset), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(decimal), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Decimal), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(decimal?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Decimal), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(double), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Double), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(double?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Double), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(short), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int16), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(short?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int16), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(int), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(int?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(long), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int64), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(long?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int64), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(sbyte), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.SByte), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(sbyte?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.SByte), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(string), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.String), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(float), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Single), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(float?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Single), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(byte[]), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Binary), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(Stream), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Stream), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(Guid), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Guid), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(Guid?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Guid), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(TimeSpan), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Time), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(TimeSpan?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Time), true));
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004150 File Offset: 0x00002350
		internal static bool IsAssignableFrom(this IEdmStructuredType baseType, IEdmStructuredType subtype)
		{
			if (baseType.TypeKind != subtype.TypeKind)
			{
				return false;
			}
			if (!baseType.IsODataEntityTypeKind() && !baseType.IsODataComplexTypeKind())
			{
				return false;
			}
			for (IEdmStructuredType edmStructuredType = subtype; edmStructuredType != null; edmStructuredType = edmStructuredType.BaseType)
			{
				if (edmStructuredType.IsEquivalentTo(baseType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000419C File Offset: 0x0000239C
		internal static Type GetPrimitiveClrType(IEdmPrimitiveType primitiveType, bool isNullable)
		{
			switch (primitiveType.PrimitiveKind)
			{
			case EdmPrimitiveTypeKind.Binary:
				return typeof(byte[]);
			case EdmPrimitiveTypeKind.Boolean:
				if (!isNullable)
				{
					return typeof(bool);
				}
				return typeof(bool?);
			case EdmPrimitiveTypeKind.Byte:
				if (!isNullable)
				{
					return typeof(byte);
				}
				return typeof(byte?);
			case EdmPrimitiveTypeKind.DateTime:
				if (!isNullable)
				{
					return typeof(DateTime);
				}
				return typeof(DateTime?);
			case EdmPrimitiveTypeKind.DateTimeOffset:
				if (!isNullable)
				{
					return typeof(DateTimeOffset);
				}
				return typeof(DateTimeOffset?);
			case EdmPrimitiveTypeKind.Decimal:
				if (!isNullable)
				{
					return typeof(decimal);
				}
				return typeof(decimal?);
			case EdmPrimitiveTypeKind.Double:
				if (!isNullable)
				{
					return typeof(double);
				}
				return typeof(double?);
			case EdmPrimitiveTypeKind.Guid:
				if (!isNullable)
				{
					return typeof(Guid);
				}
				return typeof(Guid?);
			case EdmPrimitiveTypeKind.Int16:
				if (!isNullable)
				{
					return typeof(short);
				}
				return typeof(short?);
			case EdmPrimitiveTypeKind.Int32:
				if (!isNullable)
				{
					return typeof(int);
				}
				return typeof(int?);
			case EdmPrimitiveTypeKind.Int64:
				if (!isNullable)
				{
					return typeof(long);
				}
				return typeof(long?);
			case EdmPrimitiveTypeKind.SByte:
				if (!isNullable)
				{
					return typeof(sbyte);
				}
				return typeof(sbyte?);
			case EdmPrimitiveTypeKind.Single:
				if (!isNullable)
				{
					return typeof(float);
				}
				return typeof(float?);
			case EdmPrimitiveTypeKind.String:
				return typeof(string);
			case EdmPrimitiveTypeKind.Stream:
				return typeof(Stream);
			case EdmPrimitiveTypeKind.Time:
				if (!isNullable)
				{
					return typeof(TimeSpan);
				}
				return typeof(TimeSpan?);
			case EdmPrimitiveTypeKind.Geography:
				return typeof(Geography);
			case EdmPrimitiveTypeKind.GeographyPoint:
				return typeof(GeographyPoint);
			case EdmPrimitiveTypeKind.GeographyLineString:
				return typeof(GeographyLineString);
			case EdmPrimitiveTypeKind.GeographyPolygon:
				return typeof(GeographyPolygon);
			case EdmPrimitiveTypeKind.GeographyCollection:
				return typeof(GeographyCollection);
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
				return typeof(GeographyMultiPolygon);
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
				return typeof(GeographyMultiLineString);
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
				return typeof(GeographyMultiPoint);
			case EdmPrimitiveTypeKind.Geometry:
				return typeof(Geometry);
			case EdmPrimitiveTypeKind.GeometryPoint:
				return typeof(GeometryPoint);
			case EdmPrimitiveTypeKind.GeometryLineString:
				return typeof(GeometryLineString);
			case EdmPrimitiveTypeKind.GeometryPolygon:
				return typeof(GeometryPolygon);
			case EdmPrimitiveTypeKind.GeometryCollection:
				return typeof(GeometryCollection);
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
				return typeof(GeometryMultiPolygon);
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
				return typeof(GeometryMultiLineString);
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return typeof(GeometryMultiPoint);
			default:
				return null;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004454 File Offset: 0x00002654
		internal static Type GetPrimitiveClrType(IEdmPrimitiveTypeReference primitiveTypeReference)
		{
			return EdmLibraryExtensions.GetPrimitiveClrType(primitiveTypeReference.PrimitiveDefinition(), primitiveTypeReference.IsNullable);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004467 File Offset: 0x00002667
		internal static IEdmTypeReference ToTypeReference(this IEdmType type)
		{
			return type.ToTypeReference(false);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004470 File Offset: 0x00002670
		internal static bool IsOpenType(this IEdmType type)
		{
			IEdmStructuredType edmStructuredType = type as IEdmStructuredType;
			return edmStructuredType != null && edmStructuredType.IsOpen;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004490 File Offset: 0x00002690
		internal static bool IsStream(this IEdmType type)
		{
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.Stream;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000044B4 File Offset: 0x000026B4
		internal static IEdmPrimitiveTypeReference GetPrimitiveTypeReference(Type clrType)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference;
			if (EdmLibraryExtensions.PrimitiveTypeReferenceMap.TryGetValue(clrType, ref edmPrimitiveTypeReference))
			{
				return edmPrimitiveTypeReference;
			}
			IEdmPrimitiveType edmPrimitiveType = null;
			if (typeof(GeographyPoint).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyPoint);
			}
			else if (typeof(GeographyLineString).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyLineString);
			}
			else if (typeof(GeographyPolygon).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyPolygon);
			}
			else if (typeof(GeographyMultiPoint).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyMultiPoint);
			}
			else if (typeof(GeographyMultiLineString).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyMultiLineString);
			}
			else if (typeof(GeographyMultiPolygon).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyMultiPolygon);
			}
			else if (typeof(GeographyCollection).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyCollection);
			}
			else if (typeof(Geography).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geography);
			}
			else if (typeof(GeometryPoint).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryPoint);
			}
			else if (typeof(GeometryLineString).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryLineString);
			}
			else if (typeof(GeometryPolygon).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryPolygon);
			}
			else if (typeof(GeometryMultiPoint).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryMultiPoint);
			}
			else if (typeof(GeometryMultiLineString).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryMultiLineString);
			}
			else if (typeof(GeometryMultiPolygon).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryMultiPolygon);
			}
			else if (typeof(GeometryCollection).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryCollection);
			}
			else if (typeof(Geometry).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geometry);
			}
			if (edmPrimitiveType == null)
			{
				return null;
			}
			return EdmLibraryExtensions.ToTypeReference(edmPrimitiveType, true);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004714 File Offset: 0x00002914
		internal static IEdmTypeReference ToTypeReference(this IEdmType type, bool nullable)
		{
			if (type == null)
			{
				return null;
			}
			switch (type.TypeKind)
			{
			case EdmTypeKind.Primitive:
				return EdmLibraryExtensions.ToTypeReference((IEdmPrimitiveType)type, nullable);
			case EdmTypeKind.Entity:
				return new EdmEntityTypeReference((IEdmEntityType)type, nullable);
			case EdmTypeKind.Complex:
				return new EdmComplexTypeReference((IEdmComplexType)type, nullable);
			case EdmTypeKind.Row:
				return new EdmRowTypeReference((IEdmRowType)type, nullable);
			case EdmTypeKind.Collection:
				return new EdmCollectionTypeReference((IEdmCollectionType)type, nullable);
			case EdmTypeKind.EntityReference:
				return new EdmEntityReferenceTypeReference((IEdmEntityReferenceType)type, nullable);
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_ToTypeReference));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000047B0 File Offset: 0x000029B0
		private static EdmPrimitiveTypeReference ToTypeReference(IEdmPrimitiveType primitiveType, bool nullable)
		{
			switch (primitiveType.PrimitiveKind)
			{
			case EdmPrimitiveTypeKind.Binary:
				return new EdmBinaryTypeReference(primitiveType, nullable);
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
				return new EdmPrimitiveTypeReference(primitiveType, nullable);
			case EdmPrimitiveTypeKind.DateTime:
			case EdmPrimitiveTypeKind.DateTimeOffset:
			case EdmPrimitiveTypeKind.Time:
				return new EdmTemporalTypeReference(primitiveType, nullable);
			case EdmPrimitiveTypeKind.Decimal:
				return new EdmDecimalTypeReference(primitiveType, nullable);
			case EdmPrimitiveTypeKind.String:
				return new EdmStringTypeReference(primitiveType, nullable);
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
				return new EdmSpatialTypeReference(primitiveType, nullable);
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_PrimitiveTypeReference));
			}
		}

		// Token: 0x040000FA RID: 250
		private static readonly Dictionary<Type, IEdmPrimitiveTypeReference> PrimitiveTypeReferenceMap = new Dictionary<Type, IEdmPrimitiveTypeReference>(EqualityComparer<Type>.Default);
	}
}
