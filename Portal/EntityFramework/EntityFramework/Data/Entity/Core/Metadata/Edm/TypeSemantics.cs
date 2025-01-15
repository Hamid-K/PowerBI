using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004FF RID: 1279
	internal static class TypeSemantics
	{
		// Token: 0x06003F20 RID: 16160 RVA: 0x000D212C File Offset: 0x000D032C
		internal static bool IsEqual(TypeUsage type1, TypeUsage type2)
		{
			return TypeSemantics.CompareTypes(type1, type2, false);
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x000D2136 File Offset: 0x000D0336
		internal static bool IsStructurallyEqual(TypeUsage fromType, TypeUsage toType)
		{
			return TypeSemantics.CompareTypes(fromType, toType, true);
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x000D2140 File Offset: 0x000D0340
		internal static bool IsStructurallyEqualOrPromotableTo(TypeUsage fromType, TypeUsage toType)
		{
			return TypeSemantics.IsStructurallyEqual(fromType, toType) || TypeSemantics.IsPromotableTo(fromType, toType);
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x000D2154 File Offset: 0x000D0354
		internal static bool IsStructurallyEqualOrPromotableTo(EdmType fromType, EdmType toType)
		{
			return TypeSemantics.IsStructurallyEqualOrPromotableTo(TypeUsage.Create(fromType), TypeUsage.Create(toType));
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x000D2167 File Offset: 0x000D0367
		internal static bool IsSubTypeOf(TypeUsage subType, TypeUsage superType)
		{
			if (subType.EdmEquals(superType))
			{
				return true;
			}
			if (Helper.IsPrimitiveType(subType.EdmType) && Helper.IsPrimitiveType(superType.EdmType))
			{
				return TypeSemantics.IsPrimitiveTypeSubTypeOf(subType, superType);
			}
			return subType.IsSubtypeOf(superType);
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x000D219D File Offset: 0x000D039D
		internal static bool IsSubTypeOf(EdmType subEdmType, EdmType superEdmType)
		{
			return subEdmType.IsSubtypeOf(superEdmType);
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x000D21A8 File Offset: 0x000D03A8
		internal static bool IsPromotableTo(TypeUsage fromType, TypeUsage toType)
		{
			if (toType.EdmType.EdmEquals(fromType.EdmType))
			{
				return true;
			}
			if (Helper.IsPrimitiveType(fromType.EdmType) && Helper.IsPrimitiveType(toType.EdmType))
			{
				return TypeSemantics.IsPrimitiveTypePromotableTo(fromType, toType);
			}
			if (Helper.IsCollectionType(fromType.EdmType) && Helper.IsCollectionType(toType.EdmType))
			{
				return TypeSemantics.IsPromotableTo(TypeHelpers.GetElementTypeUsage(fromType), TypeHelpers.GetElementTypeUsage(toType));
			}
			if (Helper.IsEntityTypeBase(fromType.EdmType) && Helper.IsEntityTypeBase(toType.EdmType))
			{
				return fromType.EdmType.IsSubtypeOf(toType.EdmType);
			}
			if (Helper.IsRefType(fromType.EdmType) && Helper.IsRefType(toType.EdmType))
			{
				return TypeSemantics.IsPromotableTo(TypeHelpers.GetElementTypeUsage(fromType), TypeHelpers.GetElementTypeUsage(toType));
			}
			return Helper.IsRowType(fromType.EdmType) && Helper.IsRowType(toType.EdmType) && TypeSemantics.IsPromotableTo((RowType)fromType.EdmType, (RowType)toType.EdmType);
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x000D22A8 File Offset: 0x000D04A8
		internal static IEnumerable<TypeUsage> FlattenType(TypeUsage type)
		{
			Func<TypeUsage, bool> func = (TypeUsage t) => !Helper.IsTransientType(t.EdmType);
			Func<TypeUsage, IEnumerable<TypeUsage>> func2 = delegate(TypeUsage t)
			{
				if (Helper.IsCollectionType(t.EdmType) || Helper.IsRefType(t.EdmType))
				{
					return new TypeUsage[] { TypeHelpers.GetElementTypeUsage(t) };
				}
				if (Helper.IsRowType(t.EdmType))
				{
					return ((RowType)t.EdmType).Properties.Select((EdmProperty p) => p.TypeUsage);
				}
				return new TypeUsage[0];
			};
			return Helpers.GetLeafNodes<TypeUsage>(type, func, func2);
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x000D2300 File Offset: 0x000D0500
		internal static bool IsCastAllowed(TypeUsage fromType, TypeUsage toType)
		{
			return (Helper.IsPrimitiveType(fromType.EdmType) && Helper.IsPrimitiveType(toType.EdmType)) || (Helper.IsPrimitiveType(fromType.EdmType) && Helper.IsEnumType(toType.EdmType)) || (Helper.IsEnumType(fromType.EdmType) && Helper.IsPrimitiveType(toType.EdmType)) || (Helper.IsEnumType(fromType.EdmType) && Helper.IsEnumType(toType.EdmType) && fromType.EdmType.Equals(toType.EdmType));
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x000D238C File Offset: 0x000D058C
		internal static bool TryGetCommonType(TypeUsage type1, TypeUsage type2, out TypeUsage commonType)
		{
			commonType = null;
			if (type1.EdmEquals(type2))
			{
				commonType = TypeSemantics.ForgetConstraints(type2);
				return true;
			}
			if (Helper.IsPrimitiveType(type1.EdmType) && Helper.IsPrimitiveType(type2.EdmType))
			{
				return TypeSemantics.TryGetCommonPrimitiveType(type1, type2, out commonType);
			}
			EdmType edmType;
			if (TypeSemantics.TryGetCommonType(type1.EdmType, type2.EdmType, out edmType))
			{
				commonType = TypeSemantics.ForgetConstraints(TypeUsage.Create(edmType));
				return true;
			}
			commonType = null;
			return false;
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x000D23FC File Offset: 0x000D05FC
		internal static TypeUsage GetCommonType(TypeUsage type1, TypeUsage type2)
		{
			TypeUsage typeUsage = null;
			if (TypeSemantics.TryGetCommonType(type1, type2, out typeUsage))
			{
				return typeUsage;
			}
			return null;
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x000D2419 File Offset: 0x000D0619
		internal static bool IsAggregateFunction(EdmFunction function)
		{
			return function.AggregateAttribute;
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x000D2421 File Offset: 0x000D0621
		internal static bool IsValidPolymorphicCast(TypeUsage fromType, TypeUsage toType)
		{
			return TypeSemantics.IsPolymorphicType(fromType) && TypeSemantics.IsPolymorphicType(toType) && (TypeSemantics.IsStructurallyEqual(fromType, toType) || TypeSemantics.IsSubTypeOf(fromType, toType) || TypeSemantics.IsSubTypeOf(toType, fromType));
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x000D2450 File Offset: 0x000D0650
		internal static bool IsValidPolymorphicCast(EdmType fromEdmType, EdmType toEdmType)
		{
			return TypeSemantics.IsValidPolymorphicCast(TypeUsage.Create(fromEdmType), TypeUsage.Create(toEdmType));
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x000D2463 File Offset: 0x000D0663
		internal static bool IsNominalType(TypeUsage type)
		{
			return TypeSemantics.IsEntityType(type) || TypeSemantics.IsComplexType(type);
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x000D2475 File Offset: 0x000D0675
		internal static bool IsCollectionType(TypeUsage type)
		{
			return Helper.IsCollectionType(type.EdmType);
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x000D2482 File Offset: 0x000D0682
		internal static bool IsComplexType(TypeUsage type)
		{
			return BuiltInTypeKind.ComplexType == type.EdmType.BuiltInTypeKind;
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x000D2492 File Offset: 0x000D0692
		internal static bool IsEntityType(TypeUsage type)
		{
			return Helper.IsEntityType(type.EdmType);
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x000D249F File Offset: 0x000D069F
		internal static bool IsRelationshipType(TypeUsage type)
		{
			return BuiltInTypeKind.AssociationType == type.EdmType.BuiltInTypeKind;
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x000D24AF File Offset: 0x000D06AF
		internal static bool IsEnumerationType(TypeUsage type)
		{
			return Helper.IsEnumType(type.EdmType);
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x000D24BC File Offset: 0x000D06BC
		internal static bool IsScalarType(TypeUsage type)
		{
			return TypeSemantics.IsScalarType(type.EdmType);
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x000D24C9 File Offset: 0x000D06C9
		internal static bool IsScalarType(EdmType type)
		{
			return Helper.IsPrimitiveType(type) || Helper.IsEnumType(type);
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x000D24DB File Offset: 0x000D06DB
		internal static bool IsNumericType(TypeUsage type)
		{
			return TypeSemantics.IsIntegerNumericType(type) || TypeSemantics.IsFixedPointNumericType(type) || TypeSemantics.IsFloatPointNumericType(type);
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x000D24F8 File Offset: 0x000D06F8
		internal static bool IsIntegerNumericType(TypeUsage type)
		{
			PrimitiveTypeKind primitiveTypeKind;
			return TypeHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind) && (primitiveTypeKind == PrimitiveTypeKind.Byte || primitiveTypeKind - PrimitiveTypeKind.SByte <= 3);
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x000D2520 File Offset: 0x000D0720
		internal static bool IsFixedPointNumericType(TypeUsage type)
		{
			PrimitiveTypeKind primitiveTypeKind;
			return TypeHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind) && primitiveTypeKind == PrimitiveTypeKind.Decimal;
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x000D2540 File Offset: 0x000D0740
		internal static bool IsFloatPointNumericType(TypeUsage type)
		{
			PrimitiveTypeKind primitiveTypeKind;
			return TypeHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind) && (primitiveTypeKind == PrimitiveTypeKind.Double || primitiveTypeKind == PrimitiveTypeKind.Single);
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x000D2564 File Offset: 0x000D0764
		internal static bool IsUnsignedNumericType(TypeUsage type)
		{
			PrimitiveTypeKind primitiveTypeKind;
			return TypeHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind) && primitiveTypeKind == PrimitiveTypeKind.Byte;
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x000D2584 File Offset: 0x000D0784
		internal static bool IsPolymorphicType(TypeUsage type)
		{
			return TypeSemantics.IsEntityType(type) || TypeSemantics.IsComplexType(type);
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x000D2596 File Offset: 0x000D0796
		internal static bool IsBooleanType(TypeUsage type)
		{
			return TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Boolean);
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x000D259F File Offset: 0x000D079F
		internal static bool IsPrimitiveType(TypeUsage type)
		{
			return Helper.IsPrimitiveType(type.EdmType);
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x000D25AC File Offset: 0x000D07AC
		internal static bool IsPrimitiveType(TypeUsage type, PrimitiveTypeKind primitiveTypeKind)
		{
			PrimitiveTypeKind primitiveTypeKind2;
			return TypeHelpers.TryGetPrimitiveTypeKind(type, out primitiveTypeKind2) && primitiveTypeKind2 == primitiveTypeKind;
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x000D25C9 File Offset: 0x000D07C9
		internal static bool IsRowType(TypeUsage type)
		{
			return Helper.IsRowType(type.EdmType);
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x000D25D6 File Offset: 0x000D07D6
		internal static bool IsReferenceType(TypeUsage type)
		{
			return Helper.IsRefType(type.EdmType);
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x000D25E3 File Offset: 0x000D07E3
		internal static bool IsSpatialType(TypeUsage type)
		{
			return Helper.IsSpatialType(type);
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x000D25EB File Offset: 0x000D07EB
		internal static bool IsStrongSpatialType(TypeUsage type)
		{
			return TypeSemantics.IsPrimitiveType(type) && Helper.IsStrongSpatialTypeKind(((PrimitiveType)type.EdmType).PrimitiveTypeKind);
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x000D260C File Offset: 0x000D080C
		internal static bool IsStructuralType(TypeUsage type)
		{
			return Helper.IsStructuralType(type.EdmType);
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x000D261C File Offset: 0x000D081C
		internal static bool IsPartOfKey(EdmMember edmMember)
		{
			if (Helper.IsRelationshipEndMember(edmMember))
			{
				return ((RelationshipType)edmMember.DeclaringType).KeyMembers.Contains(edmMember);
			}
			return Helper.IsEdmProperty(edmMember) && Helper.IsEntityTypeBase(edmMember.DeclaringType) && ((EntityTypeBase)edmMember.DeclaringType).KeyMembers.Contains(edmMember);
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x000D2678 File Offset: 0x000D0878
		internal static bool IsNullable(TypeUsage type)
		{
			Facet facet;
			return !type.Facets.TryGetValue("Nullable", false, out facet) || (bool)facet.Value;
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x000D26A7 File Offset: 0x000D08A7
		internal static bool IsNullable(EdmMember edmMember)
		{
			return TypeSemantics.IsNullable(edmMember.TypeUsage);
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x000D26B4 File Offset: 0x000D08B4
		internal static bool IsEqualComparable(TypeUsage type)
		{
			return TypeSemantics.IsEqualComparable(type.EdmType);
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x000D26C1 File Offset: 0x000D08C1
		internal static bool IsEqualComparableTo(TypeUsage type1, TypeUsage type2)
		{
			return TypeSemantics.IsEqualComparable(type1) && TypeSemantics.IsEqualComparable(type2) && TypeSemantics.HasCommonType(type1, type2);
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x000D26DC File Offset: 0x000D08DC
		internal static bool IsOrderComparable(TypeUsage type)
		{
			return TypeSemantics.IsOrderComparable(type.EdmType);
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x000D26E9 File Offset: 0x000D08E9
		internal static bool IsOrderComparableTo(TypeUsage type1, TypeUsage type2)
		{
			return TypeSemantics.IsOrderComparable(type1) && TypeSemantics.IsOrderComparable(type2) && TypeSemantics.HasCommonType(type1, type2);
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x000D2704 File Offset: 0x000D0904
		internal static TypeUsage ForgetConstraints(TypeUsage type)
		{
			if (Helper.IsPrimitiveType(type.EdmType))
			{
				return EdmProviderManifest.Instance.ForgetScalarConstraints(type);
			}
			return type;
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x000D2720 File Offset: 0x000D0920
		[Conditional("DEBUG")]
		internal static void AssertTypeInvariant(string message, Func<bool> assertPredicate)
		{
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x000D2722 File Offset: 0x000D0922
		private static bool IsPrimitiveTypeSubTypeOf(TypeUsage fromType, TypeUsage toType)
		{
			return TypeSemantics.IsSubTypeOf((PrimitiveType)fromType.EdmType, (PrimitiveType)toType.EdmType);
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x000D2744 File Offset: 0x000D0944
		private static bool IsSubTypeOf(PrimitiveType subPrimitiveType, PrimitiveType superPrimitiveType)
		{
			if (subPrimitiveType == superPrimitiveType)
			{
				return true;
			}
			if (Helper.AreSameSpatialUnionType(subPrimitiveType, superPrimitiveType))
			{
				return true;
			}
			ReadOnlyCollection<PrimitiveType> promotionTypes = EdmProviderManifest.Instance.GetPromotionTypes(subPrimitiveType);
			return -1 != promotionTypes.IndexOf(superPrimitiveType);
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x000D277C File Offset: 0x000D097C
		private static bool IsPromotableTo(RowType fromRowType, RowType toRowType)
		{
			if (fromRowType.Properties.Count != toRowType.Properties.Count)
			{
				return false;
			}
			for (int i = 0; i < fromRowType.Properties.Count; i++)
			{
				if (!TypeSemantics.IsPromotableTo(fromRowType.Properties[i].TypeUsage, toRowType.Properties[i].TypeUsage))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x000D27E5 File Offset: 0x000D09E5
		private static bool IsPrimitiveTypePromotableTo(TypeUsage fromType, TypeUsage toType)
		{
			return TypeSemantics.IsSubTypeOf((PrimitiveType)fromType.EdmType, (PrimitiveType)toType.EdmType);
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x000D2808 File Offset: 0x000D0A08
		private static bool TryGetCommonType(EdmType edmType1, EdmType edmType2, out EdmType commonEdmType)
		{
			if (edmType2 == edmType1)
			{
				commonEdmType = edmType1;
				return true;
			}
			if (Helper.IsPrimitiveType(edmType1) && Helper.IsPrimitiveType(edmType2))
			{
				return TypeSemantics.TryGetCommonType((PrimitiveType)edmType1, (PrimitiveType)edmType2, out commonEdmType);
			}
			if (Helper.IsCollectionType(edmType1) && Helper.IsCollectionType(edmType2))
			{
				return TypeSemantics.TryGetCommonType((CollectionType)edmType1, (CollectionType)edmType2, out commonEdmType);
			}
			if (Helper.IsEntityTypeBase(edmType1) && Helper.IsEntityTypeBase(edmType2))
			{
				return TypeSemantics.TryGetCommonBaseType(edmType1, edmType2, out commonEdmType);
			}
			if (Helper.IsRefType(edmType1) && Helper.IsRefType(edmType2))
			{
				return TypeSemantics.TryGetCommonType((RefType)edmType1, (RefType)edmType2, out commonEdmType);
			}
			if (Helper.IsRowType(edmType1) && Helper.IsRowType(edmType2))
			{
				return TypeSemantics.TryGetCommonType((RowType)edmType1, (RowType)edmType2, out commonEdmType);
			}
			commonEdmType = null;
			return false;
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x000D28C8 File Offset: 0x000D0AC8
		private static bool TryGetCommonPrimitiveType(TypeUsage type1, TypeUsage type2, out TypeUsage commonType)
		{
			commonType = null;
			if (TypeSemantics.IsPromotableTo(type1, type2))
			{
				commonType = TypeSemantics.ForgetConstraints(type2);
				return true;
			}
			if (TypeSemantics.IsPromotableTo(type2, type1))
			{
				commonType = TypeSemantics.ForgetConstraints(type1);
				return true;
			}
			ReadOnlyCollection<PrimitiveType> primitiveCommonSuperTypes = TypeSemantics.GetPrimitiveCommonSuperTypes((PrimitiveType)type1.EdmType, (PrimitiveType)type2.EdmType);
			if (primitiveCommonSuperTypes.Count == 0)
			{
				return false;
			}
			commonType = TypeUsage.CreateDefaultTypeUsage(primitiveCommonSuperTypes[0]);
			return commonType != null;
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x000D2938 File Offset: 0x000D0B38
		private static bool TryGetCommonType(PrimitiveType primitiveType1, PrimitiveType primitiveType2, out EdmType commonType)
		{
			commonType = null;
			if (TypeSemantics.IsSubTypeOf(primitiveType1, primitiveType2))
			{
				commonType = primitiveType2;
				return true;
			}
			if (TypeSemantics.IsSubTypeOf(primitiveType2, primitiveType1))
			{
				commonType = primitiveType1;
				return true;
			}
			ReadOnlyCollection<PrimitiveType> primitiveCommonSuperTypes = TypeSemantics.GetPrimitiveCommonSuperTypes(primitiveType1, primitiveType2);
			if (primitiveCommonSuperTypes.Count > 0)
			{
				commonType = primitiveCommonSuperTypes[0];
				return true;
			}
			return false;
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x000D2984 File Offset: 0x000D0B84
		private static bool TryGetCommonType(CollectionType collectionType1, CollectionType collectionType2, out EdmType commonType)
		{
			TypeUsage typeUsage = null;
			if (!TypeSemantics.TryGetCommonType(collectionType1.TypeUsage, collectionType2.TypeUsage, out typeUsage))
			{
				commonType = null;
				return false;
			}
			commonType = new CollectionType(typeUsage);
			return true;
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x000D29B6 File Offset: 0x000D0BB6
		private static bool TryGetCommonType(RefType refType1, RefType reftype2, out EdmType commonType)
		{
			if (!TypeSemantics.TryGetCommonType(refType1.ElementType, reftype2.ElementType, out commonType))
			{
				return false;
			}
			commonType = new RefType((EntityType)commonType);
			return true;
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x000D29E0 File Offset: 0x000D0BE0
		private static bool TryGetCommonType(RowType rowType1, RowType rowType2, out EdmType commonRowType)
		{
			if (rowType1.Properties.Count != rowType2.Properties.Count || rowType1.InitializerMetadata != rowType2.InitializerMetadata)
			{
				commonRowType = null;
				return false;
			}
			List<EdmProperty> list = new List<EdmProperty>();
			for (int i = 0; i < rowType1.Properties.Count; i++)
			{
				TypeUsage typeUsage;
				if (!TypeSemantics.TryGetCommonType(rowType1.Properties[i].TypeUsage, rowType2.Properties[i].TypeUsage, out typeUsage))
				{
					commonRowType = null;
					return false;
				}
				list.Add(new EdmProperty(rowType1.Properties[i].Name, typeUsage));
			}
			commonRowType = new RowType(list, rowType1.InitializerMetadata);
			return true;
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x000D2A90 File Offset: 0x000D0C90
		internal static bool TryGetCommonBaseType(EdmType type1, EdmType type2, out EdmType commonBaseType)
		{
			Dictionary<EdmType, byte> dictionary = new Dictionary<EdmType, byte>();
			for (EdmType edmType = type2; edmType != null; edmType = edmType.BaseType)
			{
				dictionary.Add(edmType, 0);
			}
			for (EdmType edmType2 = type1; edmType2 != null; edmType2 = edmType2.BaseType)
			{
				if (dictionary.ContainsKey(edmType2))
				{
					commonBaseType = edmType2;
					return true;
				}
			}
			commonBaseType = null;
			return false;
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x000D2AD9 File Offset: 0x000D0CD9
		private static bool HasCommonType(TypeUsage type1, TypeUsage type2)
		{
			return TypeHelpers.GetCommonTypeUsage(type1, type2) != null;
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x000D2AE8 File Offset: 0x000D0CE8
		private static bool IsEqualComparable(EdmType edmType)
		{
			if (Helper.IsPrimitiveType(edmType) || Helper.IsRefType(edmType) || Helper.IsEntityType(edmType) || Helper.IsEnumType(edmType))
			{
				return true;
			}
			if (Helper.IsRowType(edmType))
			{
				using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator = ((RowType)edmType).Properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!TypeSemantics.IsEqualComparable(enumerator.Current.TypeUsage))
						{
							return false;
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x000D2B78 File Offset: 0x000D0D78
		private static bool IsOrderComparable(EdmType edmType)
		{
			return Helper.IsScalarType(edmType);
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x000D2B80 File Offset: 0x000D0D80
		private static bool CompareTypes(TypeUsage fromType, TypeUsage toType, bool equivalenceOnly)
		{
			if (fromType == toType)
			{
				return true;
			}
			if (fromType.EdmType.BuiltInTypeKind != toType.EdmType.BuiltInTypeKind)
			{
				return false;
			}
			if (fromType.EdmType.BuiltInTypeKind == BuiltInTypeKind.CollectionType)
			{
				return TypeSemantics.CompareTypes(((CollectionType)fromType.EdmType).TypeUsage, ((CollectionType)toType.EdmType).TypeUsage, equivalenceOnly);
			}
			if (fromType.EdmType.BuiltInTypeKind == BuiltInTypeKind.RefType)
			{
				return ((RefType)fromType.EdmType).ElementType.EdmEquals(((RefType)toType.EdmType).ElementType);
			}
			if (fromType.EdmType.BuiltInTypeKind != BuiltInTypeKind.RowType)
			{
				return fromType.EdmType.EdmEquals(toType.EdmType);
			}
			RowType rowType = (RowType)fromType.EdmType;
			RowType rowType2 = (RowType)toType.EdmType;
			if (rowType.Properties.Count != rowType2.Properties.Count)
			{
				return false;
			}
			for (int i = 0; i < rowType.Properties.Count; i++)
			{
				EdmProperty edmProperty = rowType.Properties[i];
				EdmProperty edmProperty2 = rowType2.Properties[i];
				if (!equivalenceOnly && edmProperty.Name != edmProperty2.Name)
				{
					return false;
				}
				if (!TypeSemantics.CompareTypes(edmProperty.TypeUsage, edmProperty2.TypeUsage, equivalenceOnly))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x000D2CD0 File Offset: 0x000D0ED0
		private static void ComputeCommonTypeClosure()
		{
			if (TypeSemantics._commonTypeClosure != null)
			{
				return;
			}
			ReadOnlyCollection<PrimitiveType>[,] array = new ReadOnlyCollection<PrimitiveType>[32, 32];
			for (int i = 0; i < 32; i++)
			{
				array[i, i] = Helper.EmptyPrimitiveTypeReadOnlyCollection;
			}
			ReadOnlyCollection<PrimitiveType> storeTypes = EdmProviderManifest.Instance.GetStoreTypes();
			for (int j = 0; j < 32; j++)
			{
				for (int k = 0; k < j; k++)
				{
					array[j, k] = TypeSemantics.Intersect(EdmProviderManifest.Instance.GetPromotionTypes(storeTypes[j]), EdmProviderManifest.Instance.GetPromotionTypes(storeTypes[k]));
					array[k, j] = array[j, k];
				}
			}
			Interlocked.CompareExchange<ReadOnlyCollection<PrimitiveType>[,]>(ref TypeSemantics._commonTypeClosure, array, null);
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x000D2D84 File Offset: 0x000D0F84
		private static ReadOnlyCollection<PrimitiveType> Intersect(IList<PrimitiveType> types1, IList<PrimitiveType> types2)
		{
			List<PrimitiveType> list = new List<PrimitiveType>();
			for (int i = 0; i < types1.Count; i++)
			{
				if (types2.Contains(types1[i]))
				{
					list.Add(types1[i]);
				}
			}
			if (list.Count == 0)
			{
				return Helper.EmptyPrimitiveTypeReadOnlyCollection;
			}
			return new ReadOnlyCollection<PrimitiveType>(list);
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x000D2DD8 File Offset: 0x000D0FD8
		private static ReadOnlyCollection<PrimitiveType> GetPrimitiveCommonSuperTypes(PrimitiveType primitiveType1, PrimitiveType primitiveType2)
		{
			TypeSemantics.ComputeCommonTypeClosure();
			return TypeSemantics._commonTypeClosure[(int)primitiveType1.PrimitiveTypeKind, (int)primitiveType2.PrimitiveTypeKind];
		}

		// Token: 0x0400158C RID: 5516
		private static ReadOnlyCollection<PrimitiveType>[,] _commonTypeClosure;
	}
}
