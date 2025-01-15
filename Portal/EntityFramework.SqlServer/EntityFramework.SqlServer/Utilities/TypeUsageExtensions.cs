using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000022 RID: 34
	internal static class TypeUsageExtensions
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x0000EFCA File Offset: 0x0000D1CA
		internal static byte GetPrecision(this TypeUsage type)
		{
			return type.GetFacetValue("Precision");
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000EFD7 File Offset: 0x0000D1D7
		internal static byte GetScale(this TypeUsage type)
		{
			return type.GetFacetValue("Scale");
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
		internal static int GetMaxLength(this TypeUsage type)
		{
			return type.GetFacetValue("MaxLength");
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000EFF1 File Offset: 0x0000D1F1
		internal static T GetFacetValue<T>(this TypeUsage type, string facetName)
		{
			return (T)((object)type.Facets[facetName].Value);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000F00C File Offset: 0x0000D20C
		internal static bool IsFixedLength(this TypeUsage type)
		{
			Facet facet = type.Facets.SingleOrDefault((Facet f) => f.Name == "FixedLength");
			return facet != null && facet.Value != null && (bool)facet.Value;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000F05C File Offset: 0x0000D25C
		internal static bool TryGetPrecision(this TypeUsage type, out byte precision)
		{
			if (!type.IsPrimitiveType(PrimitiveTypeKind.Decimal))
			{
				precision = 0;
				return false;
			}
			return type.TryGetFacetValue("Precision", out precision);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000F078 File Offset: 0x0000D278
		internal static bool TryGetScale(this TypeUsage type, out byte scale)
		{
			if (!type.IsPrimitiveType(PrimitiveTypeKind.Decimal))
			{
				scale = 0;
				return false;
			}
			return type.TryGetFacetValue("Scale", out scale);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000F094 File Offset: 0x0000D294
		internal static bool TryGetFacetValue<T>(this TypeUsage type, string facetName, out T value)
		{
			value = default(T);
			Facet facet;
			if (type.Facets.TryGetValue(facetName, false, out facet) && facet.Value is T)
			{
				value = (T)((object)facet.Value);
				return true;
			}
			return false;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000F0DA File Offset: 0x0000D2DA
		internal static bool IsPrimitiveType(this TypeUsage type, PrimitiveTypeKind primitiveTypeKind)
		{
			return type.IsPrimitiveType() && ((PrimitiveType)type.EdmType).PrimitiveTypeKind == primitiveTypeKind;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000F0F9 File Offset: 0x0000D2F9
		internal static bool IsPrimitiveType(this TypeUsage type)
		{
			return type != null && type.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000F110 File Offset: 0x0000D310
		internal static bool IsNullable(this TypeUsage type)
		{
			Facet facet = type.Facets.SingleOrDefault((Facet f) => f.Name == "Nullable");
			return facet != null && facet.Value != null && (bool)facet.Value;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000F160 File Offset: 0x0000D360
		internal static PrimitiveTypeKind GetPrimitiveTypeKind(this TypeUsage type)
		{
			return ((PrimitiveType)type.EdmType).PrimitiveTypeKind;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000F172 File Offset: 0x0000D372
		internal static bool TryGetIsUnicode(this TypeUsage type, out bool isUnicode)
		{
			if (!type.IsPrimitiveType(PrimitiveTypeKind.String))
			{
				isUnicode = false;
				return false;
			}
			return type.TryGetFacetValue("Unicode", out isUnicode);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000F18F File Offset: 0x0000D38F
		internal static bool TryGetMaxLength(this TypeUsage type, out int maxLength)
		{
			if (!type.IsPrimitiveType(PrimitiveTypeKind.String) && !type.IsPrimitiveType(PrimitiveTypeKind.Binary))
			{
				maxLength = 0;
				return false;
			}
			return type.TryGetFacetValue("MaxLength", out maxLength);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
		internal static IEnumerable<EdmProperty> GetProperties(this TypeUsage type)
		{
			EdmType edmType = type.EdmType;
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
				return Enumerable.Empty<EdmProperty>();
			}
			return ((RowType)edmType).Properties;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000F20C File Offset: 0x0000D40C
		internal static TypeUsage GetElementTypeUsage(this TypeUsage type)
		{
			EdmType edmType = type.EdmType;
			if (BuiltInTypeKind.CollectionType == edmType.BuiltInTypeKind)
			{
				return ((CollectionType)edmType).TypeUsage;
			}
			if (BuiltInTypeKind.RefType == edmType.BuiltInTypeKind)
			{
				return TypeUsage.CreateDefaultTypeUsage(((RefType)edmType).ElementType);
			}
			return null;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000F254 File Offset: 0x0000D454
		internal static bool MustFacetBeConstant(this TypeUsage type, string facetName)
		{
			return ((PrimitiveType)type.EdmType).FacetDescriptions.Single((FacetDescription f) => f.FacetName == facetName).IsConstant;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000F294 File Offset: 0x0000D494
		internal static bool IsHierarchyIdType(this TypeUsage type)
		{
			return type.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType && ((PrimitiveType)type.EdmType).IsHierarchyIdType();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000F2B7 File Offset: 0x0000D4B7
		internal static bool IsSpatialType(this TypeUsage type)
		{
			return type.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType && ((PrimitiveType)type.EdmType).IsSpatialType();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000F2DA File Offset: 0x0000D4DA
		internal static bool IsSpatialType(this TypeUsage type, out PrimitiveTypeKind spatialType)
		{
			if (type.IsSpatialType())
			{
				spatialType = ((PrimitiveType)type.EdmType).PrimitiveTypeKind;
				return true;
			}
			spatialType = PrimitiveTypeKind.Binary;
			return false;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000F2FC File Offset: 0x0000D4FC
		internal static TypeUsage ForceNonUnicode(this TypeUsage typeUsage)
		{
			TypeUsage typeUsage2 = TypeUsage.CreateStringTypeUsage((PrimitiveType)typeUsage.EdmType, false, false);
			return TypeUsage.Create(typeUsage.EdmType, typeUsage.Facets.Where((Facet f) => f.Name != "Unicode").Union(typeUsage2.Facets.Where((Facet f) => f.Name == "Unicode")));
		}
	}
}
