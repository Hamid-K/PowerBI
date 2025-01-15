using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Entity.Core.Common
{
	// Token: 0x020005EF RID: 1519
	internal static class TypeHelpers
	{
		// Token: 0x06004A3C RID: 19004 RVA: 0x00107210 File Offset: 0x00105410
		[Conditional("DEBUG")]
		internal static void AssertEdmType(TypeUsage typeUsage)
		{
			EdmType edmType = typeUsage.EdmType;
			if (!TypeSemantics.IsCollectionType(typeUsage))
			{
				if (TypeSemantics.IsStructuralType(typeUsage) && !Helper.IsComplexType(typeUsage.EdmType) && !Helper.IsEntityType(typeUsage.EdmType))
				{
					using (IEnumerator enumerator = TypeHelpers.GetDeclaredStructuralMembers(typeUsage).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							EdmMember edmMember = (EdmMember)obj;
						}
						return;
					}
				}
				if (TypeSemantics.IsPrimitiveType(typeUsage))
				{
					PrimitiveType primitiveType = edmType as PrimitiveType;
					if (primitiveType != null && primitiveType.DataSpace != DataSpace.CSpace)
					{
						throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "PrimitiveType must be CSpace '{0}'", new object[] { typeUsage }));
					}
				}
			}
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x001072D0 File Offset: 0x001054D0
		[Conditional("DEBUG")]
		internal static void AssertEdmType(DbCommandTree commandTree)
		{
			DbQueryCommandTree dbQueryCommandTree = commandTree as DbQueryCommandTree;
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x001072E8 File Offset: 0x001054E8
		internal static bool IsValidSortOpKeyType(TypeUsage typeUsage)
		{
			if (TypeSemantics.IsRowType(typeUsage))
			{
				using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator = ((RowType)typeUsage.EdmType).Properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!TypeHelpers.IsValidSortOpKeyType(enumerator.Current.TypeUsage))
						{
							return false;
						}
					}
				}
				return true;
			}
			return TypeSemantics.IsOrderComparable(typeUsage);
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x00107360 File Offset: 0x00105560
		internal static bool IsValidGroupKeyType(TypeUsage typeUsage)
		{
			return TypeHelpers.IsSetComparableOpType(typeUsage);
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x00107368 File Offset: 0x00105568
		internal static bool IsValidDistinctOpType(TypeUsage typeUsage)
		{
			return TypeHelpers.IsSetComparableOpType(typeUsage);
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x00107370 File Offset: 0x00105570
		internal static bool IsSetComparableOpType(TypeUsage typeUsage)
		{
			if (Helper.IsEntityType(typeUsage.EdmType) || Helper.IsPrimitiveType(typeUsage.EdmType) || Helper.IsEnumType(typeUsage.EdmType) || Helper.IsRefType(typeUsage.EdmType))
			{
				return true;
			}
			if (TypeSemantics.IsRowType(typeUsage))
			{
				using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator = ((RowType)typeUsage.EdmType).Properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!TypeHelpers.IsSetComparableOpType(enumerator.Current.TypeUsage))
						{
							return false;
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x00107418 File Offset: 0x00105618
		internal static bool IsValidIsNullOpType(TypeUsage typeUsage)
		{
			return TypeSemantics.IsReferenceType(typeUsage) || TypeSemantics.IsEntityType(typeUsage) || TypeSemantics.IsScalarType(typeUsage) || TypeSemantics.IsRowType(typeUsage);
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x0010743A File Offset: 0x0010563A
		internal static bool IsValidInOpType(TypeUsage typeUsage)
		{
			return TypeSemantics.IsReferenceType(typeUsage) || TypeSemantics.IsEntityType(typeUsage) || TypeSemantics.IsScalarType(typeUsage);
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x00107454 File Offset: 0x00105654
		internal static TypeUsage GetCommonTypeUsage(TypeUsage typeUsage1, TypeUsage typeUsage2)
		{
			return TypeSemantics.GetCommonType(typeUsage1, typeUsage2);
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x00107460 File Offset: 0x00105660
		internal static TypeUsage GetCommonTypeUsage(IEnumerable<TypeUsage> types)
		{
			TypeUsage typeUsage = null;
			foreach (TypeUsage typeUsage2 in types)
			{
				if (typeUsage2 == null)
				{
					return null;
				}
				if (typeUsage == null)
				{
					typeUsage = typeUsage2;
				}
				else
				{
					typeUsage = TypeSemantics.GetCommonType(typeUsage, typeUsage2);
					if (typeUsage == null)
					{
						break;
					}
				}
			}
			return typeUsage;
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x001074C4 File Offset: 0x001056C4
		internal static bool TryGetClosestPromotableType(TypeUsage fromType, out TypeUsage promotableType)
		{
			promotableType = null;
			if (Helper.IsPrimitiveType(fromType.EdmType))
			{
				PrimitiveType primitiveType = (PrimitiveType)fromType.EdmType;
				IList<PrimitiveType> promotionTypes = EdmProviderManifest.Instance.GetPromotionTypes(primitiveType);
				int num = promotionTypes.IndexOf(primitiveType);
				if (-1 != num && num + 1 < promotionTypes.Count)
				{
					promotableType = TypeUsage.Create(promotionTypes[num + 1]);
				}
			}
			return promotableType != null;
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x00107528 File Offset: 0x00105728
		internal static bool TryGetBooleanFacetValue(TypeUsage type, string facetName, out bool boolValue)
		{
			boolValue = false;
			Facet facet;
			if (type.Facets.TryGetValue(facetName, false, out facet) && facet.Value != null)
			{
				boolValue = (bool)facet.Value;
				return true;
			}
			return false;
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x00107564 File Offset: 0x00105764
		internal static bool TryGetByteFacetValue(TypeUsage type, string facetName, out byte byteValue)
		{
			byteValue = 0;
			Facet facet;
			if (type.Facets.TryGetValue(facetName, false, out facet) && facet.Value != null && !Helper.IsUnboundedFacetValue(facet))
			{
				byteValue = (byte)facet.Value;
				return true;
			}
			return false;
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x001075A8 File Offset: 0x001057A8
		internal static bool TryGetIntFacetValue(TypeUsage type, string facetName, out int intValue)
		{
			intValue = 0;
			Facet facet;
			if (type.Facets.TryGetValue(facetName, false, out facet) && facet.Value != null && !Helper.IsUnboundedFacetValue(facet) && !Helper.IsVariableFacetValue(facet))
			{
				intValue = (int)facet.Value;
				return true;
			}
			return false;
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x001075F1 File Offset: 0x001057F1
		internal static bool TryGetIsFixedLength(TypeUsage type, out bool isFixedLength)
		{
			if (!TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.String) && !TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Binary))
			{
				isFixedLength = false;
				return false;
			}
			return TypeHelpers.TryGetBooleanFacetValue(type, "FixedLength", out isFixedLength);
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x00107617 File Offset: 0x00105817
		internal static bool TryGetIsUnicode(TypeUsage type, out bool isUnicode)
		{
			if (!TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.String))
			{
				isUnicode = false;
				return false;
			}
			return TypeHelpers.TryGetBooleanFacetValue(type, "Unicode", out isUnicode);
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x00107634 File Offset: 0x00105834
		internal static bool IsFacetValueConstant(TypeUsage type, string facetName)
		{
			return Helper.GetFacet(((PrimitiveType)type.EdmType).FacetDescriptions, facetName).IsConstant;
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x00107651 File Offset: 0x00105851
		internal static bool TryGetMaxLength(TypeUsage type, out int maxLength)
		{
			if (!TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.String) && !TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Binary))
			{
				maxLength = 0;
				return false;
			}
			return TypeHelpers.TryGetIntFacetValue(type, "MaxLength", out maxLength);
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x00107677 File Offset: 0x00105877
		internal static bool TryGetPrecision(TypeUsage type, out byte precision)
		{
			if (!TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Decimal))
			{
				precision = 0;
				return false;
			}
			return TypeHelpers.TryGetByteFacetValue(type, "Precision", out precision);
		}

		// Token: 0x06004A4F RID: 19023 RVA: 0x00107693 File Offset: 0x00105893
		internal static bool TryGetScale(TypeUsage type, out byte scale)
		{
			if (!TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Decimal))
			{
				scale = 0;
				return false;
			}
			return TypeHelpers.TryGetByteFacetValue(type, "Scale", out scale);
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x001076AF File Offset: 0x001058AF
		internal static bool TryGetPrimitiveTypeKind(TypeUsage type, out PrimitiveTypeKind typeKind)
		{
			if (type != null && type.EdmType != null && type.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType)
			{
				typeKind = ((PrimitiveType)type.EdmType).PrimitiveTypeKind;
				return true;
			}
			typeKind = PrimitiveTypeKind.Binary;
			return false;
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x001076E3 File Offset: 0x001058E3
		internal static CollectionType CreateCollectionType(TypeUsage elementType)
		{
			return new CollectionType(elementType);
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x001076EB File Offset: 0x001058EB
		internal static TypeUsage CreateCollectionTypeUsage(TypeUsage elementType)
		{
			return TypeUsage.Create(new CollectionType(elementType));
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x001076F8 File Offset: 0x001058F8
		internal static RowType CreateRowType(IEnumerable<KeyValuePair<string, TypeUsage>> columns)
		{
			return TypeHelpers.CreateRowType(columns, null);
		}

		// Token: 0x06004A54 RID: 19028 RVA: 0x00107704 File Offset: 0x00105904
		internal static RowType CreateRowType(IEnumerable<KeyValuePair<string, TypeUsage>> columns, InitializerMetadata initializerMetadata)
		{
			List<EdmProperty> list = new List<EdmProperty>();
			foreach (KeyValuePair<string, TypeUsage> keyValuePair in columns)
			{
				list.Add(new EdmProperty(keyValuePair.Key, keyValuePair.Value));
			}
			return new RowType(list, initializerMetadata);
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x0010776C File Offset: 0x0010596C
		internal static TypeUsage CreateRowTypeUsage(IEnumerable<KeyValuePair<string, TypeUsage>> columns)
		{
			return TypeUsage.Create(TypeHelpers.CreateRowType(columns));
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x00107779 File Offset: 0x00105979
		internal static RefType CreateReferenceType(EntityTypeBase entityType)
		{
			return new RefType((EntityType)entityType);
		}

		// Token: 0x06004A57 RID: 19031 RVA: 0x00107786 File Offset: 0x00105986
		internal static TypeUsage CreateReferenceTypeUsage(EntityType entityType)
		{
			return TypeUsage.Create(TypeHelpers.CreateReferenceType(entityType));
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x00107794 File Offset: 0x00105994
		internal static RowType CreateKeyRowType(EntityTypeBase entityType)
		{
			IEnumerable<EdmMember> keyMembers = entityType.KeyMembers;
			if (keyMembers == null)
			{
				throw new ArgumentException(Strings.Cqt_Metadata_EntityTypeNullKeyMembersInvalid, "entityType");
			}
			List<KeyValuePair<string, TypeUsage>> list = new List<KeyValuePair<string, TypeUsage>>();
			foreach (EdmMember edmMember in keyMembers)
			{
				EdmProperty edmProperty = (EdmProperty)edmMember;
				list.Add(new KeyValuePair<string, TypeUsage>(edmProperty.Name, Helper.GetModelTypeUsage(edmProperty)));
			}
			if (list.Count < 1)
			{
				throw new ArgumentException(Strings.Cqt_Metadata_EntityTypeEmptyKeyMembersInvalid, "entityType");
			}
			return TypeHelpers.CreateRowType(list);
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x00107830 File Offset: 0x00105A30
		internal static TypeUsage GetPrimitiveTypeUsageForScalar(TypeUsage scalarType)
		{
			if (!TypeSemantics.IsEnumerationType(scalarType))
			{
				return scalarType;
			}
			return TypeHelpers.CreateEnumUnderlyingTypeUsage(scalarType);
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x00107842 File Offset: 0x00105A42
		internal static TypeUsage CreateEnumUnderlyingTypeUsage(TypeUsage enumTypeUsage)
		{
			return TypeUsage.Create(Helper.GetUnderlyingEdmTypeForEnumType(enumTypeUsage.EdmType), enumTypeUsage.Facets);
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x0010785A File Offset: 0x00105A5A
		internal static TypeUsage CreateSpatialUnionTypeUsage(TypeUsage spatialTypeUsage)
		{
			return TypeUsage.Create(Helper.GetSpatialNormalizedPrimitiveType(spatialTypeUsage.EdmType), spatialTypeUsage.Facets);
		}

		// Token: 0x06004A5C RID: 19036 RVA: 0x00107872 File Offset: 0x00105A72
		internal static IBaseList<EdmMember> GetAllStructuralMembers(TypeUsage type)
		{
			return TypeHelpers.GetAllStructuralMembers(type.EdmType);
		}

		// Token: 0x06004A5D RID: 19037 RVA: 0x00107880 File Offset: 0x00105A80
		internal static IBaseList<EdmMember> GetAllStructuralMembers(EdmType edmType)
		{
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.ComplexType)
			{
				if (builtInTypeKind == BuiltInTypeKind.AssociationType)
				{
					return (IBaseList<EdmMember>)((AssociationType)edmType).AssociationEndMembers;
				}
				if (builtInTypeKind == BuiltInTypeKind.ComplexType)
				{
					return (IBaseList<EdmMember>)((ComplexType)edmType).Properties;
				}
			}
			else
			{
				if (builtInTypeKind == BuiltInTypeKind.EntityType)
				{
					return (IBaseList<EdmMember>)((EntityType)edmType).Properties;
				}
				if (builtInTypeKind == BuiltInTypeKind.RowType)
				{
					return (IBaseList<EdmMember>)((RowType)edmType).Properties;
				}
			}
			return TypeHelpers.EmptyArrayEdmProperty;
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x001078F7 File Offset: 0x00105AF7
		internal static IEnumerable GetDeclaredStructuralMembers(TypeUsage type)
		{
			return TypeHelpers.GetDeclaredStructuralMembers(type.EdmType);
		}

		// Token: 0x06004A5F RID: 19039 RVA: 0x00107904 File Offset: 0x00105B04
		internal static IEnumerable GetDeclaredStructuralMembers(EdmType edmType)
		{
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.ComplexType)
			{
				if (builtInTypeKind == BuiltInTypeKind.AssociationType)
				{
					return ((AssociationType)edmType).GetDeclaredOnlyMembers<AssociationEndMember>();
				}
				if (builtInTypeKind == BuiltInTypeKind.ComplexType)
				{
					return ((ComplexType)edmType).GetDeclaredOnlyMembers<EdmProperty>();
				}
			}
			else
			{
				if (builtInTypeKind == BuiltInTypeKind.EntityType)
				{
					return ((EntityType)edmType).GetDeclaredOnlyMembers<EdmProperty>();
				}
				if (builtInTypeKind == BuiltInTypeKind.RowType)
				{
					return ((RowType)edmType).GetDeclaredOnlyMembers<EdmProperty>();
				}
			}
			return TypeHelpers.EmptyArrayEdmProperty;
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x00107967 File Offset: 0x00105B67
		internal static ReadOnlyMetadataCollection<EdmProperty> GetProperties(TypeUsage typeUsage)
		{
			return TypeHelpers.GetProperties(typeUsage.EdmType);
		}

		// Token: 0x06004A61 RID: 19041 RVA: 0x00107974 File Offset: 0x00105B74
		internal static ReadOnlyMetadataCollection<EdmProperty> GetProperties(EdmType edmType)
		{
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind == BuiltInTypeKind.ComplexType)
			{
				return ((ComplexType)edmType).Properties;
			}
			if (builtInTypeKind == BuiltInTypeKind.EntityType)
			{
				return ((EntityType)edmType).Properties;
			}
			if (builtInTypeKind != BuiltInTypeKind.RowType)
			{
				return TypeHelpers.EmptyArrayEdmProperty;
			}
			return ((RowType)edmType).Properties;
		}

		// Token: 0x06004A62 RID: 19042 RVA: 0x001079C1 File Offset: 0x00105BC1
		internal static TypeUsage GetElementTypeUsage(TypeUsage type)
		{
			if (TypeSemantics.IsCollectionType(type))
			{
				return ((CollectionType)type.EdmType).TypeUsage;
			}
			if (TypeSemantics.IsReferenceType(type))
			{
				return TypeUsage.Create(((RefType)type.EdmType).ElementType);
			}
			return null;
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x001079FC File Offset: 0x00105BFC
		internal static RowType GetTvfReturnType(EdmFunction tvf)
		{
			if (tvf.ReturnParameter != null && TypeSemantics.IsCollectionType(tvf.ReturnParameter.TypeUsage))
			{
				TypeUsage typeUsage = ((CollectionType)tvf.ReturnParameter.TypeUsage.EdmType).TypeUsage;
				if (TypeSemantics.IsRowType(typeUsage))
				{
					return (RowType)typeUsage.EdmType;
				}
			}
			return null;
		}

		// Token: 0x06004A64 RID: 19044 RVA: 0x00107A54 File Offset: 0x00105C54
		internal static bool TryGetCollectionElementType(TypeUsage type, out TypeUsage elementType)
		{
			CollectionType collectionType;
			if (TypeHelpers.TryGetEdmType<CollectionType>(type, out collectionType))
			{
				elementType = collectionType.TypeUsage;
				return elementType != null;
			}
			elementType = null;
			return false;
		}

		// Token: 0x06004A65 RID: 19045 RVA: 0x00107A80 File Offset: 0x00105C80
		internal static bool TryGetRefEntityType(TypeUsage type, out EntityType referencedEntityType)
		{
			RefType refType;
			if (TypeHelpers.TryGetEdmType<RefType>(type, out refType) && Helper.IsEntityType(refType.ElementType))
			{
				referencedEntityType = (EntityType)refType.ElementType;
				return true;
			}
			referencedEntityType = null;
			return false;
		}

		// Token: 0x06004A66 RID: 19046 RVA: 0x00107AB7 File Offset: 0x00105CB7
		internal static TEdmType GetEdmType<TEdmType>(TypeUsage typeUsage) where TEdmType : EdmType
		{
			return (TEdmType)((object)typeUsage.EdmType);
		}

		// Token: 0x06004A67 RID: 19047 RVA: 0x00107AC4 File Offset: 0x00105CC4
		internal static bool TryGetEdmType<TEdmType>(TypeUsage typeUsage, out TEdmType type) where TEdmType : EdmType
		{
			type = typeUsage.EdmType as TEdmType;
			return type != null;
		}

		// Token: 0x06004A68 RID: 19048 RVA: 0x00107AEA File Offset: 0x00105CEA
		internal static TypeUsage GetReadOnlyType(TypeUsage type)
		{
			if (!type.IsReadOnly)
			{
				type.SetReadOnly();
			}
			return type;
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x00107AFC File Offset: 0x00105CFC
		internal static string GetFullName(string qualifier, string name)
		{
			if (!string.IsNullOrEmpty(qualifier))
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[] { qualifier, name });
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { name });
		}

		// Token: 0x06004A6A RID: 19050 RVA: 0x00107B48 File Offset: 0x00105D48
		internal static DbType ConvertClrTypeToDbType(Type clrType)
		{
			switch (Type.GetTypeCode(clrType))
			{
			case TypeCode.Empty:
				throw new ArgumentException(Strings.ADP_InvalidDataType(TypeCode.Empty.ToString()));
			case TypeCode.Object:
				if (clrType == typeof(byte[]))
				{
					return DbType.Binary;
				}
				if (clrType == typeof(char[]))
				{
					return DbType.String;
				}
				if (clrType == typeof(Guid))
				{
					return DbType.Guid;
				}
				if (clrType == typeof(TimeSpan))
				{
					return DbType.Time;
				}
				if (clrType == typeof(DateTimeOffset))
				{
					return DbType.DateTimeOffset;
				}
				return DbType.Object;
			case TypeCode.DBNull:
				return DbType.Object;
			case TypeCode.Boolean:
				return DbType.Boolean;
			case TypeCode.Char:
				return DbType.String;
			case TypeCode.SByte:
				return DbType.SByte;
			case TypeCode.Byte:
				return DbType.Byte;
			case TypeCode.Int16:
				return DbType.Int16;
			case TypeCode.UInt16:
				return DbType.UInt16;
			case TypeCode.Int32:
				return DbType.Int32;
			case TypeCode.UInt32:
				return DbType.UInt32;
			case TypeCode.Int64:
				return DbType.Int64;
			case TypeCode.UInt64:
				return DbType.UInt64;
			case TypeCode.Single:
				return DbType.Single;
			case TypeCode.Double:
				return DbType.Double;
			case TypeCode.Decimal:
				return DbType.Decimal;
			case TypeCode.DateTime:
				return DbType.DateTime;
			case TypeCode.String:
				return DbType.String;
			}
			throw new ArgumentException(Strings.ADP_UnknownDataTypeCode(((int)Type.GetTypeCode(clrType)).ToString(CultureInfo.InvariantCulture), clrType.FullName));
		}

		// Token: 0x06004A6B RID: 19051 RVA: 0x00107C88 File Offset: 0x00105E88
		internal static bool IsIntegerConstant(TypeUsage valueType, object value, long expectedValue)
		{
			if (!TypeSemantics.IsIntegerNumericType(valueType))
			{
				return false;
			}
			if (value == null)
			{
				return false;
			}
			PrimitiveTypeKind primitiveTypeKind = ((PrimitiveType)valueType.EdmType).PrimitiveTypeKind;
			if (primitiveTypeKind == PrimitiveTypeKind.Byte)
			{
				return expectedValue == (long)((ulong)((byte)value));
			}
			switch (primitiveTypeKind)
			{
			case PrimitiveTypeKind.SByte:
				return expectedValue == (long)((sbyte)value);
			case PrimitiveTypeKind.Int16:
				return expectedValue == (long)((short)value);
			case PrimitiveTypeKind.Int32:
				return expectedValue == (long)((int)value);
			case PrimitiveTypeKind.Int64:
				return expectedValue == (long)value;
			default:
				return false;
			}
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x00107D0A File Offset: 0x00105F0A
		internal static TypeUsage GetLiteralTypeUsage(PrimitiveTypeKind primitiveTypeKind)
		{
			return TypeHelpers.GetLiteralTypeUsage(primitiveTypeKind, true);
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x00107D14 File Offset: 0x00105F14
		internal static TypeUsage GetLiteralTypeUsage(PrimitiveTypeKind primitiveTypeKind, bool isUnicode)
		{
			PrimitiveType primitiveType = EdmProviderManifest.Instance.GetPrimitiveType(primitiveTypeKind);
			TypeUsage typeUsage;
			if (primitiveTypeKind == PrimitiveTypeKind.String)
			{
				typeUsage = TypeUsage.Create(primitiveType, new FacetValues
				{
					Unicode = new bool?(isUnicode),
					MaxLength = TypeUsage.DefaultMaxLengthFacetValue,
					FixedLength = new bool?(false),
					Nullable = new bool?(false)
				});
			}
			else
			{
				typeUsage = TypeUsage.Create(primitiveType, new FacetValues
				{
					Nullable = new bool?(false)
				});
			}
			return typeUsage;
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x00107DA1 File Offset: 0x00105FA1
		internal static bool IsCanonicalFunction(EdmFunction function)
		{
			return function.DataSpace == DataSpace.CSpace && function.NamespaceName == "Edm";
		}

		// Token: 0x04001A2E RID: 6702
		internal static readonly ReadOnlyMetadataCollection<EdmMember> EmptyArrayEdmMember = new ReadOnlyMetadataCollection<EdmMember>(new MetadataCollection<EdmMember>().SetReadOnly());

		// Token: 0x04001A2F RID: 6703
		internal static readonly FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember> EmptyArrayEdmProperty = new FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember>(TypeHelpers.EmptyArrayEdmMember, null);
	}
}
