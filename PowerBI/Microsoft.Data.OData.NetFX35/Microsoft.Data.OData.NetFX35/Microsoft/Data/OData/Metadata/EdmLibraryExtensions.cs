using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Spatial;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000202 RID: 514
	internal static class EdmLibraryExtensions
	{
		// Token: 0x06000EBD RID: 3773 RVA: 0x00035420 File Offset: 0x00033620
		static EdmLibraryExtensions()
		{
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(bool), EdmLibraryExtensions.BooleanTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(byte), EdmLibraryExtensions.ByteTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(DateTime), EdmLibraryExtensions.DateTimeTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(decimal), EdmLibraryExtensions.DecimalTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(double), EdmLibraryExtensions.DoubleTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(short), EdmLibraryExtensions.Int16TypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(int), EdmLibraryExtensions.Int32TypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(long), EdmLibraryExtensions.Int64TypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(sbyte), EdmLibraryExtensions.SByteTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(string), EdmLibraryExtensions.StringTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(float), EdmLibraryExtensions.SingleTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(DateTimeOffset), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.DateTimeOffset), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(Guid), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Guid), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(TimeSpan), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Time), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(byte[]), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Binary), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(Stream), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Stream), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(bool?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Boolean), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(byte?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Byte), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(DateTime?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.DateTime), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(DateTimeOffset?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.DateTimeOffset), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(decimal?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Decimal), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(double?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Double), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(short?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int16), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(int?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(long?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int64), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(sbyte?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.SByte), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(float?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Single), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(Guid?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Guid), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(TimeSpan?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Time), true));
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x000358E9 File Offset: 0x00033AE9
		internal static bool IsUserModel(this IEdmModel model)
		{
			return !(model is EdmCoreModel);
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x000358F8 File Offset: 0x00033AF8
		internal static bool IsPrimitiveType(Type clrType)
		{
			switch (PlatformHelper.GetTypeCode(clrType))
			{
			case 3:
			case 5:
			case 6:
			case 7:
			case 9:
			case 11:
			case 13:
			case 14:
			case 15:
			case 16:
			case 18:
				return true;
			}
			return EdmLibraryExtensions.PrimitiveTypeReferenceMap.ContainsKey(clrType) || typeof(ISpatial).IsAssignableFrom(clrType);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00035978 File Offset: 0x00033B78
		internal static IEnumerable<IEdmEntityType> EntityTypes(this IEdmModel model)
		{
			IEnumerable<IEdmSchemaElement> schemaElements = model.SchemaElements;
			if (schemaElements != null)
			{
				return Enumerable.OfType<IEdmEntityType>(schemaElements);
			}
			return null;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00035998 File Offset: 0x00033B98
		internal static IEdmCollectionTypeReference ToCollectionTypeReference(this IEdmPrimitiveTypeReference itemTypeReference)
		{
			IEdmCollectionType edmCollectionType = new EdmCollectionType(itemTypeReference);
			return (IEdmCollectionTypeReference)edmCollectionType.ToTypeReference();
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x000359B8 File Offset: 0x00033BB8
		internal static IEdmCollectionTypeReference ToCollectionTypeReference(this IEdmComplexTypeReference itemTypeReference)
		{
			IEdmCollectionType edmCollectionType = new EdmCollectionType(itemTypeReference);
			return (IEdmCollectionTypeReference)edmCollectionType.ToTypeReference();
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x000359D7 File Offset: 0x00033BD7
		internal static bool IsAssignableFrom(this IEdmTypeReference baseType, IEdmTypeReference subtype)
		{
			return baseType.Definition.IsAssignableFrom(subtype.Definition);
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x000359EC File Offset: 0x00033BEC
		internal static bool IsAssignableFrom(this IEdmType baseType, IEdmType subtype)
		{
			EdmTypeKind typeKind = baseType.TypeKind;
			EdmTypeKind typeKind2 = subtype.TypeKind;
			if (typeKind != typeKind2)
			{
				return false;
			}
			switch (typeKind)
			{
			case EdmTypeKind.Primitive:
				return ((IEdmPrimitiveType)baseType).IsAssignableFrom((IEdmPrimitiveType)subtype);
			case EdmTypeKind.Entity:
			case EdmTypeKind.Complex:
				return ((IEdmStructuredType)baseType).IsAssignableFrom((IEdmStructuredType)subtype);
			case EdmTypeKind.Collection:
				return ((IEdmCollectionType)baseType).ElementType.Definition.IsAssignableFrom(((IEdmCollectionType)subtype).ElementType.Definition);
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_IsAssignableFrom_Type));
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00035A88 File Offset: 0x00033C88
		internal static IEdmStructuredType GetCommonBaseType(this IEdmStructuredType firstType, IEdmStructuredType secondType)
		{
			if (firstType.IsEquivalentTo(secondType))
			{
				return firstType;
			}
			for (IEdmStructuredType edmStructuredType = firstType; edmStructuredType != null; edmStructuredType = edmStructuredType.BaseType)
			{
				if (edmStructuredType.IsAssignableFrom(secondType))
				{
					return edmStructuredType;
				}
			}
			for (IEdmStructuredType edmStructuredType = secondType; edmStructuredType != null; edmStructuredType = edmStructuredType.BaseType)
			{
				if (edmStructuredType.IsAssignableFrom(firstType))
				{
					return edmStructuredType;
				}
			}
			return null;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00035AD4 File Offset: 0x00033CD4
		internal static IEdmPrimitiveType GetCommonBaseType(this IEdmPrimitiveType firstType, IEdmPrimitiveType secondType)
		{
			if (firstType.IsEquivalentTo(secondType))
			{
				return firstType;
			}
			for (IEdmPrimitiveType edmPrimitiveType = firstType; edmPrimitiveType != null; edmPrimitiveType = edmPrimitiveType.BaseType())
			{
				if (edmPrimitiveType.IsAssignableFrom(secondType))
				{
					return edmPrimitiveType;
				}
			}
			for (IEdmPrimitiveType edmPrimitiveType = secondType; edmPrimitiveType != null; edmPrimitiveType = edmPrimitiveType.BaseType())
			{
				if (edmPrimitiveType.IsAssignableFrom(firstType))
				{
					return edmPrimitiveType;
				}
			}
			return null;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00035B20 File Offset: 0x00033D20
		internal static IEdmPrimitiveType BaseType(this IEdmPrimitiveType type)
		{
			switch (type.PrimitiveKind)
			{
			case EdmPrimitiveTypeKind.None:
			case EdmPrimitiveTypeKind.Binary:
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
			case EdmPrimitiveTypeKind.String:
			case EdmPrimitiveTypeKind.Stream:
			case EdmPrimitiveTypeKind.Time:
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.Geometry:
				return null;
			case EdmPrimitiveTypeKind.GeographyPoint:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geography);
			case EdmPrimitiveTypeKind.GeographyLineString:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geography);
			case EdmPrimitiveTypeKind.GeographyPolygon:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geography);
			case EdmPrimitiveTypeKind.GeographyCollection:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geography);
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyCollection);
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyCollection);
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyCollection);
			case EdmPrimitiveTypeKind.GeometryPoint:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geometry);
			case EdmPrimitiveTypeKind.GeometryLineString:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geometry);
			case EdmPrimitiveTypeKind.GeometryPolygon:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geometry);
			case EdmPrimitiveTypeKind.GeometryCollection:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geometry);
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryCollection);
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryCollection);
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryCollection);
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_BaseType));
			}
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00035C8B File Offset: 0x00033E8B
		internal static IEdmComplexTypeReference AsComplexOrNull(this IEdmTypeReference typeReference)
		{
			if (typeReference == null)
			{
				return null;
			}
			if (typeReference.TypeKind() != EdmTypeKind.Complex)
			{
				return null;
			}
			return typeReference.AsComplex();
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00035CA4 File Offset: 0x00033EA4
		internal static IEdmCollectionTypeReference AsCollectionOrNull(this IEdmTypeReference typeReference)
		{
			if (typeReference == null)
			{
				return null;
			}
			if (typeReference.TypeKind() != EdmTypeKind.Collection)
			{
				return null;
			}
			IEdmCollectionTypeReference edmCollectionTypeReference = typeReference.AsCollection();
			if (!edmCollectionTypeReference.IsNonEntityCollectionType())
			{
				return null;
			}
			return edmCollectionTypeReference;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00035CD3 File Offset: 0x00033ED3
		internal static IEdmSchemaType ResolvePrimitiveTypeName(string typeName)
		{
			return EdmCoreModel.Instance.FindDeclaredType(typeName);
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00035CE0 File Offset: 0x00033EE0
		internal static IEdmTypeReference GetCollectionItemType(this IEdmTypeReference typeReference)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = typeReference.AsCollectionOrNull();
			if (edmCollectionTypeReference != null)
			{
				return edmCollectionTypeReference.ElementType();
			}
			return null;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00035D00 File Offset: 0x00033F00
		internal static IEdmCollectionType GetCollectionType(IEdmType itemType)
		{
			IEdmTypeReference edmTypeReference = itemType.ToTypeReference();
			return EdmLibraryExtensions.GetCollectionType(edmTypeReference);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00035D1A File Offset: 0x00033F1A
		internal static IEdmCollectionType GetCollectionType(IEdmTypeReference itemTypeReference)
		{
			if (!itemTypeReference.IsODataPrimitiveTypeKind() && !itemTypeReference.IsODataComplexTypeKind())
			{
				throw new ODataException(Strings.EdmLibraryExtensions_CollectionItemCanBeOnlyPrimitiveOrComplex);
			}
			return new EdmCollectionType(itemTypeReference);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00035D40 File Offset: 0x00033F40
		internal static bool IsGeographyType(this IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			if (edmPrimitiveTypeReference == null)
			{
				return false;
			}
			switch (edmPrimitiveTypeReference.PrimitiveKind())
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

		// Token: 0x06000ECF RID: 3791 RVA: 0x00035D90 File Offset: 0x00033F90
		internal static bool IsGeometryType(this IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			if (edmPrimitiveTypeReference == null)
			{
				return false;
			}
			switch (edmPrimitiveTypeReference.PrimitiveKind())
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

		// Token: 0x06000ED0 RID: 3792 RVA: 0x00035DDE File Offset: 0x00033FDE
		internal static string GetCollectionItemTypeName(string typeName)
		{
			return EdmLibraryExtensions.GetCollectionItemTypeName(typeName, false);
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00035DE8 File Offset: 0x00033FE8
		internal static string FunctionImportGroupName(this IEnumerable<IEdmFunctionImport> functionImportGroup)
		{
			return Enumerable.First<IEdmFunctionImport>(functionImportGroup).Name;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00035E04 File Offset: 0x00034004
		internal static string FunctionImportGroupFullName(this IEnumerable<IEdmFunctionImport> functionImportGroup)
		{
			return Enumerable.First<IEdmFunctionImport>(functionImportGroup).FullName();
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00035E1E File Offset: 0x0003401E
		internal static string NameWithParameters(this IEdmFunctionImport functionImport)
		{
			return functionImport.Name + functionImport.ParameterTypesToString();
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00035E31 File Offset: 0x00034031
		internal static string FullNameWithParameters(this IEdmFunctionImport functionImport)
		{
			return functionImport.FullName() + functionImport.ParameterTypesToString();
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00035E44 File Offset: 0x00034044
		internal static bool OperationsBoundToEntityTypeMustBeContainerQualified(this IEdmEntityType entityType)
		{
			return entityType.IsOpen;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00035E4C File Offset: 0x0003404C
		internal static string ODataFullName(this IEdmTypeReference typeReference)
		{
			return typeReference.Definition.ODataFullName();
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00035E5C File Offset: 0x0003405C
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
				return EdmLibraryExtensions.GetCollectionTypeName(text);
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

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00035EA0 File Offset: 0x000340A0
		internal static IEdmTypeReference Clone(this IEdmTypeReference typeReference, bool nullable)
		{
			if (typeReference == null)
			{
				return null;
			}
			switch (typeReference.TypeKind())
			{
			case EdmTypeKind.Primitive:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind = typeReference.PrimitiveKind();
				IEdmPrimitiveType edmPrimitiveType = (IEdmPrimitiveType)typeReference.Definition;
				switch (edmPrimitiveTypeKind)
				{
				case EdmPrimitiveTypeKind.Binary:
				{
					IEdmBinaryTypeReference edmBinaryTypeReference = (IEdmBinaryTypeReference)typeReference;
					return new EdmBinaryTypeReference(edmPrimitiveType, nullable, edmBinaryTypeReference.IsUnbounded, edmBinaryTypeReference.MaxLength, edmBinaryTypeReference.IsFixedLength);
				}
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
					return new EdmPrimitiveTypeReference(edmPrimitiveType, nullable);
				case EdmPrimitiveTypeKind.DateTime:
				case EdmPrimitiveTypeKind.DateTimeOffset:
				case EdmPrimitiveTypeKind.Time:
				{
					IEdmTemporalTypeReference edmTemporalTypeReference = (IEdmTemporalTypeReference)typeReference;
					return new EdmTemporalTypeReference(edmPrimitiveType, nullable, edmTemporalTypeReference.Precision);
				}
				case EdmPrimitiveTypeKind.Decimal:
				{
					IEdmDecimalTypeReference edmDecimalTypeReference = (IEdmDecimalTypeReference)typeReference;
					return new EdmDecimalTypeReference(edmPrimitiveType, nullable, edmDecimalTypeReference.Precision, edmDecimalTypeReference.Scale);
				}
				case EdmPrimitiveTypeKind.String:
				{
					IEdmStringTypeReference edmStringTypeReference = (IEdmStringTypeReference)typeReference;
					return new EdmStringTypeReference(edmPrimitiveType, nullable, edmStringTypeReference.IsUnbounded, edmStringTypeReference.MaxLength, edmStringTypeReference.IsFixedLength, edmStringTypeReference.IsUnicode, edmStringTypeReference.Collation);
				}
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
				{
					IEdmSpatialTypeReference edmSpatialTypeReference = (IEdmSpatialTypeReference)typeReference;
					return new EdmSpatialTypeReference(edmPrimitiveType, nullable, edmSpatialTypeReference.SpatialReferenceIdentifier);
				}
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_Clone_PrimitiveTypeKind));
				}
				break;
			}
			case EdmTypeKind.Entity:
				return new EdmEntityTypeReference((IEdmEntityType)typeReference.Definition, nullable);
			case EdmTypeKind.Complex:
				return new EdmComplexTypeReference((IEdmComplexType)typeReference.Definition, nullable);
			case EdmTypeKind.Row:
				return new EdmRowTypeReference((IEdmRowType)typeReference.Definition, nullable);
			case EdmTypeKind.Collection:
				return new EdmCollectionTypeReference((IEdmCollectionType)typeReference.Definition, nullable);
			case EdmTypeKind.EntityReference:
				return new EdmEntityReferenceTypeReference((IEdmEntityReferenceType)typeReference.Definition, nullable);
			case EdmTypeKind.Enum:
				return new EdmEnumTypeReference((IEdmEnumType)typeReference.Definition, nullable);
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_Clone_TypeKind));
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x000360C4 File Offset: 0x000342C4
		internal static EdmMultiplicity TargetMultiplicityTemporary(this IEdmNavigationProperty property)
		{
			IEdmTypeReference type = property.Type;
			if (type.IsCollection())
			{
				return EdmMultiplicity.Many;
			}
			if (!type.IsNullable)
			{
				return EdmMultiplicity.One;
			}
			return EdmMultiplicity.ZeroOrOne;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x000360F0 File Offset: 0x000342F0
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

		// Token: 0x06000EDB RID: 3803 RVA: 0x0003613C File Offset: 0x0003433C
		internal static bool IsSpatialType(this IEdmPrimitiveType primitiveType)
		{
			switch (primitiveType.PrimitiveKind)
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
			}
			return false;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x000361E0 File Offset: 0x000343E0
		internal static bool IsAssignableFrom(this IEdmPrimitiveType baseType, IEdmPrimitiveType subtype)
		{
			if (baseType.IsEquivalentTo(subtype))
			{
				return true;
			}
			if (!baseType.IsSpatialType() || !subtype.IsSpatialType())
			{
				return false;
			}
			EdmPrimitiveTypeKind primitiveKind = baseType.PrimitiveKind;
			EdmPrimitiveTypeKind primitiveKind2 = subtype.PrimitiveKind;
			switch (primitiveKind)
			{
			case EdmPrimitiveTypeKind.Geography:
				return primitiveKind2 == EdmPrimitiveTypeKind.Geography || primitiveKind2 == EdmPrimitiveTypeKind.GeographyCollection || primitiveKind2 == EdmPrimitiveTypeKind.GeographyLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPolygon || primitiveKind2 == EdmPrimitiveTypeKind.GeographyPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeographyPolygon;
			case EdmPrimitiveTypeKind.GeographyPoint:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyPoint;
			case EdmPrimitiveTypeKind.GeographyLineString:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyLineString;
			case EdmPrimitiveTypeKind.GeographyPolygon:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyPolygon;
			case EdmPrimitiveTypeKind.GeographyCollection:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyCollection || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPolygon;
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPolygon;
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiLineString;
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPoint;
			case EdmPrimitiveTypeKind.Geometry:
				return primitiveKind2 == EdmPrimitiveTypeKind.Geometry || primitiveKind2 == EdmPrimitiveTypeKind.GeometryCollection || primitiveKind2 == EdmPrimitiveTypeKind.GeometryLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPolygon || primitiveKind2 == EdmPrimitiveTypeKind.GeometryPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeometryPolygon;
			case EdmPrimitiveTypeKind.GeometryPoint:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryPoint;
			case EdmPrimitiveTypeKind.GeometryLineString:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryLineString;
			case EdmPrimitiveTypeKind.GeometryPolygon:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryPolygon;
			case EdmPrimitiveTypeKind.GeometryCollection:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryCollection || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPolygon;
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPolygon;
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiLineString;
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPoint;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_IsAssignableFrom_Primitive));
			}
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00036344 File Offset: 0x00034544
		internal static Type GetPrimitiveClrType(IEdmPrimitiveTypeReference primitiveTypeReference)
		{
			return EdmLibraryExtensions.GetPrimitiveClrType(primitiveTypeReference.PrimitiveDefinition(), primitiveTypeReference.IsNullable);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00036357 File Offset: 0x00034557
		internal static IEdmTypeReference ToTypeReference(this IEdmType type)
		{
			return type.ToTypeReference(false);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00036360 File Offset: 0x00034560
		internal static bool IsOpenType(this IEdmType type)
		{
			IEdmStructuredType edmStructuredType = type as IEdmStructuredType;
			return edmStructuredType != null && edmStructuredType.IsOpen;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00036380 File Offset: 0x00034580
		internal static bool IsStream(this IEdmType type)
		{
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.Stream;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x000363CC File Offset: 0x000345CC
		internal static bool ContainsProperty(this IEdmType type, IEdmProperty property)
		{
			IEdmComplexType edmComplexType = type as IEdmComplexType;
			if (edmComplexType != null)
			{
				return Enumerable.Any<IEdmProperty>(edmComplexType.Properties(), (IEdmProperty p) => p == property);
			}
			IEdmEntityType edmEntityType = type as IEdmEntityType;
			if (edmEntityType == null)
			{
				return false;
			}
			if (!Enumerable.Any<IEdmProperty>(edmEntityType.Properties(), (IEdmProperty p) => p == property))
			{
				return Enumerable.Any<IEdmNavigationProperty>(edmEntityType.NavigationProperties(), (IEdmNavigationProperty p) => p == property);
			}
			return true;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00036464 File Offset: 0x00034664
		internal static bool ContainsProperty(this IEdmTypeReference typeReference, IEdmProperty property)
		{
			IEdmStructuredTypeReference edmStructuredTypeReference = typeReference.AsStructuredOrNull();
			return edmStructuredTypeReference != null && edmStructuredTypeReference.Definition.ContainsProperty(property);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00036489 File Offset: 0x00034689
		internal static string FullName(this IEdmEntityContainerElement containerElement)
		{
			return containerElement.Container.Name + "." + containerElement.Name;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x000364A8 File Offset: 0x000346A8
		internal static IEdmPrimitiveTypeReference GetPrimitiveTypeReference(Type clrType)
		{
			switch (PlatformHelper.GetTypeCode(clrType))
			{
			case 3:
				return EdmLibraryExtensions.BooleanTypeReference;
			case 5:
				return EdmLibraryExtensions.SByteTypeReference;
			case 6:
				return EdmLibraryExtensions.ByteTypeReference;
			case 7:
				return EdmLibraryExtensions.Int16TypeReference;
			case 9:
				return EdmLibraryExtensions.Int32TypeReference;
			case 11:
				return EdmLibraryExtensions.Int64TypeReference;
			case 13:
				return EdmLibraryExtensions.SingleTypeReference;
			case 14:
				return EdmLibraryExtensions.DoubleTypeReference;
			case 15:
				return EdmLibraryExtensions.DecimalTypeReference;
			case 16:
				return EdmLibraryExtensions.DateTimeTypeReference;
			case 18:
				return EdmLibraryExtensions.StringTypeReference;
			}
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

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003679C File Offset: 0x0003499C
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

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00036838 File Offset: 0x00034A38
		internal static string GetCollectionTypeName(string itemTypeName)
		{
			return string.Format(CultureInfo.InvariantCulture, "Collection({0})", new object[] { itemTypeName });
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003688A File Offset: 0x00034A8A
		internal static IEdmEntitySet ResolveEntitySet(this IEdmModel model, string containerQualifiedEntitySetName)
		{
			return Enumerable.SingleOrDefault<IEdmEntitySet>(Enumerable.Cast<IEdmEntitySet>(EdmLibraryExtensions.ResolveContainerQualifiedElementName(model, containerQualifiedEntitySetName, delegate(IEdmEntityContainer container, string entitySetName)
			{
				IEdmEntitySet edmEntitySet = container.FindEntitySet(entitySetName);
				if (edmEntitySet != null)
				{
					return new IEdmEntitySet[] { edmEntitySet };
				}
				return Enumerable.Empty<IEdmEntityContainerElement>();
			})));
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x000368BA File Offset: 0x00034ABA
		internal static IEnumerable<IEdmFunctionImport> ResolveFunctionImports(this IEdmModel model, string containerQualifiedFunctionImportName)
		{
			return model.ResolveFunctionImports(containerQualifiedFunctionImportName, true);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x000368E0 File Offset: 0x00034AE0
		internal static IEnumerable<IEdmFunctionImport> ResolveFunctionImports(this IEdmModel model, string containerQualifiedFunctionImportName, bool allowParameterTypeNames)
		{
			return Enumerable.Cast<IEdmFunctionImport>(EdmLibraryExtensions.ResolveContainerQualifiedElementName(model, containerQualifiedFunctionImportName, (IEdmEntityContainer container, string functionImportName) => Enumerable.Cast<IEdmEntityContainerElement>(container.ResolveFunctionImports(functionImportName, allowParameterTypeNames))));
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00036912 File Offset: 0x00034B12
		internal static IEnumerable<IEdmFunctionImport> ResolveFunctionImports(this IEdmEntityContainer container, string functionImportName)
		{
			return container.ResolveFunctionImports(functionImportName, true);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00036938 File Offset: 0x00034B38
		internal static IEnumerable<IEdmFunctionImport> ResolveFunctionImports(this IEdmEntityContainer container, string functionImportName, bool allowParameterTypeNames)
		{
			if (string.IsNullOrEmpty(functionImportName))
			{
				return Enumerable.Empty<IEdmFunctionImport>();
			}
			int num = functionImportName.IndexOf('(');
			string text;
			if (num > 0)
			{
				if (!allowParameterTypeNames)
				{
					return Enumerable.Empty<IEdmFunctionImport>();
				}
				text = functionImportName.Substring(0, num);
			}
			else
			{
				text = functionImportName;
			}
			IEnumerable<IEdmFunctionImport> enumerable = container.FindFunctionImports(text);
			if (num > 0)
			{
				return Enumerable.Where<IEdmFunctionImport>(enumerable, (IEdmFunctionImport f) => f.NameWithParameters().Equals(functionImportName, 4));
			}
			return enumerable;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00036A08 File Offset: 0x00034C08
		internal static IEnumerable<IEdmFunctionImport> FindFunctionImportsByBindingParameterTypeHierarchy(this IEdmModel model, IEdmEntityType bindingType, string functionImportName)
		{
			return Enumerable.Where<IEdmFunctionImport>(model.ResolveFunctionImports(functionImportName, false), (IEdmFunctionImport f) => f.IsBindable && Enumerable.Any<IEdmFunctionParameter>(f.Parameters) && Enumerable.First<IEdmFunctionParameter>(f.Parameters).Type.Definition.IsOrInheritsFrom(bindingType));
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00036AA0 File Offset: 0x00034CA0
		internal static IEnumerable<IEdmFunctionImport> FindFunctionImportsBySpecificBindingParameterType(this IEdmModel model, IEdmType bindingType, string functionImportName)
		{
			if (bindingType != null)
			{
				return Enumerable.Where<IEdmFunctionImport>(model.ResolveFunctionImports(functionImportName, false), (IEdmFunctionImport f) => f.IsBindable && Enumerable.Any<IEdmFunctionParameter>(f.Parameters) && Enumerable.First<IEdmFunctionParameter>(f.Parameters).Type.Definition.ODataFullName() == bindingType.ODataFullName());
			}
			return Enumerable.Where<IEdmFunctionImport>(model.ResolveFunctionImports(functionImportName, false), (IEdmFunctionImport f) => !f.IsBindable);
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00036B10 File Offset: 0x00034D10
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

		// Token: 0x06000EEF RID: 3823 RVA: 0x00036DC8 File Offset: 0x00034FC8
		private static string GetCollectionItemTypeName(string typeName, bool isNested)
		{
			int length = "Collection".Length;
			if (typeName == null || !typeName.StartsWith("Collection(", 4) || typeName.get_Chars(typeName.Length - 1) != ')' || typeName.Length == length + 2)
			{
				return null;
			}
			if (isNested)
			{
				throw new ODataException(Strings.ValidationUtils_NestedCollectionsAreNotSupported);
			}
			string text = typeName.Substring(length + 1, typeName.Length - (length + 2));
			EdmLibraryExtensions.GetCollectionItemTypeName(text, true);
			return text;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00036E48 File Offset: 0x00035048
		private static string ParameterTypesToString(this IEdmFunctionImport functionImport)
		{
			return '(' + string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<IEdmFunctionParameter, string>(functionImport.Parameters, (IEdmFunctionParameter p) => p.Type.ODataFullName()))) + ')';
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00036EA0 File Offset: 0x000350A0
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

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00036F80 File Offset: 0x00035180
		private static bool TryGetSingleOrDefaultEntityContainer(this IEdmModel model, out IEdmEntityContainer foundContainer)
		{
			IEdmEntityContainer[] array = Enumerable.ToArray<IEdmEntityContainer>(model.EntityContainers());
			foundContainer = null;
			if (array.Length > 1)
			{
				IEdmEntityContainer edmEntityContainer = Enumerable.SingleOrDefault<IEdmEntityContainer>(array, new Func<IEdmEntityContainer, bool>(model.IsDefaultEntityContainer));
				if (edmEntityContainer != null)
				{
					foundContainer = edmEntityContainer;
				}
			}
			else if (array.Length == 1)
			{
				foundContainer = array[0];
			}
			return foundContainer != null;
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00036FD0 File Offset: 0x000351D0
		private static IEnumerable<IEdmEntityContainerElement> ResolveContainerQualifiedElementName(IEdmModel model, string containerQualifiedElementName, Func<IEdmEntityContainer, string, IEnumerable<IEdmEntityContainerElement>> resolver)
		{
			if (string.IsNullOrEmpty(containerQualifiedElementName))
			{
				return Enumerable.Empty<IEdmEntityContainerElement>();
			}
			IEdmEntityContainer edmEntityContainer = null;
			string text;
			if (containerQualifiedElementName.IndexOf('.') < 0)
			{
				if (!model.TryGetSingleOrDefaultEntityContainer(out edmEntityContainer))
				{
					return Enumerable.Empty<IEdmEntityContainerElement>();
				}
				text = containerQualifiedElementName;
			}
			else
			{
				int num = containerQualifiedElementName.IndexOf('(');
				string text2 = ((num < 0) ? containerQualifiedElementName : containerQualifiedElementName.Substring(0, num));
				int num2 = text2.LastIndexOf('.');
				if (num2 < 0)
				{
					return Enumerable.Empty<IEdmEntityContainerElement>();
				}
				string text3 = containerQualifiedElementName.Substring(0, num2);
				text = containerQualifiedElementName.Substring(num2 + 1);
				if (string.IsNullOrEmpty(text3) || string.IsNullOrEmpty(text))
				{
					return Enumerable.Empty<IEdmEntityContainerElement>();
				}
				edmEntityContainer = model.FindEntityContainer(text3);
				if (edmEntityContainer == null)
				{
					return Enumerable.Empty<IEdmEntityContainerElement>();
				}
			}
			return resolver.Invoke(edmEntityContainer, text);
		}

		// Token: 0x04000588 RID: 1416
		private const string CollectionTypeQualifier = "Collection";

		// Token: 0x04000589 RID: 1417
		private const string CollectionTypeFormat = "Collection({0})";

		// Token: 0x0400058A RID: 1418
		private static readonly Dictionary<Type, IEdmPrimitiveTypeReference> PrimitiveTypeReferenceMap = new Dictionary<Type, IEdmPrimitiveTypeReference>(EqualityComparer<Type>.Default);

		// Token: 0x0400058B RID: 1419
		private static readonly EdmPrimitiveTypeReference BooleanTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Boolean), false);

		// Token: 0x0400058C RID: 1420
		private static readonly EdmPrimitiveTypeReference ByteTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Byte), false);

		// Token: 0x0400058D RID: 1421
		private static readonly EdmPrimitiveTypeReference DateTimeTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.DateTime), false);

		// Token: 0x0400058E RID: 1422
		private static readonly EdmPrimitiveTypeReference DecimalTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Decimal), false);

		// Token: 0x0400058F RID: 1423
		private static readonly EdmPrimitiveTypeReference DoubleTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Double), false);

		// Token: 0x04000590 RID: 1424
		private static readonly EdmPrimitiveTypeReference Int16TypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int16), false);

		// Token: 0x04000591 RID: 1425
		private static readonly EdmPrimitiveTypeReference Int32TypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32), false);

		// Token: 0x04000592 RID: 1426
		private static readonly EdmPrimitiveTypeReference Int64TypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int64), false);

		// Token: 0x04000593 RID: 1427
		private static readonly EdmPrimitiveTypeReference SByteTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.SByte), false);

		// Token: 0x04000594 RID: 1428
		private static readonly EdmPrimitiveTypeReference StringTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.String), true);

		// Token: 0x04000595 RID: 1429
		private static readonly EdmPrimitiveTypeReference SingleTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Single), false);
	}
}
