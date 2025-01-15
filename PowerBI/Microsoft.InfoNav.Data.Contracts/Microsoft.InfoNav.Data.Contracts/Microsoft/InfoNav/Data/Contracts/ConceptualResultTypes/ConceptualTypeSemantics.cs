using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes
{
	// Token: 0x02000148 RID: 328
	public static class ConceptualTypeSemantics
	{
		// Token: 0x06000867 RID: 2151 RVA: 0x0001184C File Offset: 0x0000FA4C
		private static IReadOnlyList<IReadOnlyList<ConceptualPrimitiveType>> CreatePromotableTypes()
		{
			ConceptualPrimitiveType[] array = (ConceptualPrimitiveType[])Enum.GetValues(typeof(ConceptualPrimitiveType));
			List<List<ConceptualPrimitiveType>> list = new List<List<ConceptualPrimitiveType>>(array.Length);
			foreach (ConceptualPrimitiveType conceptualPrimitiveType in array)
			{
				list.Add(new List<ConceptualPrimitiveType> { conceptualPrimitiveType });
			}
			List<ConceptualPrimitiveType> list2 = list[4];
			list2.Add(ConceptualPrimitiveType.Decimal);
			list2.Add(ConceptualPrimitiveType.Double);
			return list;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000118B0 File Offset: 0x0000FAB0
		private static IReadOnlyDictionary<Type, ConceptualPrimitiveResultType> CreateClrTypeToPrimitiveMap()
		{
			return new Dictionary<Type, ConceptualPrimitiveResultType>
			{
				{
					typeof(bool),
					ConceptualPrimitiveResultType.Boolean
				},
				{
					typeof(string),
					ConceptualPrimitiveResultType.Text
				},
				{
					typeof(byte),
					ConceptualPrimitiveResultType.Integer
				},
				{
					typeof(short),
					ConceptualPrimitiveResultType.Integer
				},
				{
					typeof(int),
					ConceptualPrimitiveResultType.Integer
				},
				{
					typeof(long),
					ConceptualPrimitiveResultType.Integer
				},
				{
					typeof(float),
					ConceptualPrimitiveResultType.Double
				},
				{
					typeof(double),
					ConceptualPrimitiveResultType.Double
				},
				{
					typeof(decimal),
					ConceptualPrimitiveResultType.Decimal
				},
				{
					typeof(DateTime),
					ConceptualPrimitiveResultType.DateTime
				},
				{
					typeof(byte[]),
					ConceptualPrimitiveResultType.Binary
				},
				{
					typeof(TimeSpan),
					ConceptualPrimitiveResultType.Time
				},
				{
					typeof(DateTimeOffset),
					ConceptualPrimitiveResultType.DateTimeZone
				}
			};
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000119D4 File Offset: 0x0000FBD4
		public static ConceptualPrimitiveResultType GetPrimitive(this Type type)
		{
			ConceptualPrimitiveResultType conceptualPrimitiveResultType;
			ConceptualTypeSemantics._clrTypeToPrimitiveType.TryGetValue(type, out conceptualPrimitiveResultType);
			return conceptualPrimitiveResultType;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000119F0 File Offset: 0x0000FBF0
		internal static IReadOnlyList<ConceptualPrimitiveType> GetPromotionTypes(ConceptualPrimitiveType primitiveType)
		{
			return ConceptualTypeSemantics._promotionTypes[(int)primitiveType];
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00011A00 File Offset: 0x0000FC00
		internal static bool IsPromotableTo(this ConceptualResultType fromType, ConceptualResultType toType)
		{
			if (toType.Equals(fromType))
			{
				return true;
			}
			if (fromType.IsNullType() && toType.Kind == ConceptualResultTypeKind.Primitive)
			{
				return true;
			}
			if (fromType.Kind == ConceptualResultTypeKind.Primitive && toType.Kind == ConceptualResultTypeKind.Primitive)
			{
				return ConceptualTypeSemantics.IsPrimitivePromotableTo((ConceptualPrimitiveResultType)fromType, (ConceptualPrimitiveResultType)toType);
			}
			return fromType.Kind == ConceptualResultTypeKind.Collection && toType.Kind == ConceptualResultTypeKind.Collection && ConceptualTypeSemantics.IsPromotableTo((ConceptualCollectionType)fromType, (ConceptualCollectionType)toType);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00011A71 File Offset: 0x0000FC71
		private static bool IsPromotableTo(ConceptualCollectionType fromCollectionType, ConceptualCollectionType toCollectionType)
		{
			return ConceptualTypeSemantics.IsPrimitivePromotableTo(fromCollectionType.PrimitiveType, toCollectionType.PrimitiveType);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00011A84 File Offset: 0x0000FC84
		private static bool IsPrimitivePromotableTo(ConceptualPrimitiveResultType fromType, ConceptualPrimitiveResultType toType)
		{
			return ConceptualTypeSemantics.IsSubTypeOf(fromType.ConceptualDataType, toType.ConceptualDataType);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00011A9C File Offset: 0x0000FC9C
		private static bool IsSubTypeOf(ConceptualPrimitiveType subPrimitiveType, ConceptualPrimitiveType superPrimitiveType)
		{
			return subPrimitiveType == superPrimitiveType || ConceptualTypeSemantics.GetPromotionTypes(subPrimitiveType).Contains(superPrimitiveType);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00011AB0 File Offset: 0x0000FCB0
		internal static bool IsNullType(this ConceptualResultType type)
		{
			return type.Equals(ConceptualPrimitiveResultType.Null);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00011ABD File Offset: 0x0000FCBD
		internal static bool IsOrderComparable(this ConceptualResultType type)
		{
			return type is ConceptualPrimitiveResultType;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00011AC8 File Offset: 0x0000FCC8
		internal static bool IsOrderComparableTo(this ConceptualResultType fromType, ConceptualResultType toType)
		{
			return fromType.IsOrderComparable() && toType.IsOrderComparable() && (fromType.IsPromotableTo(toType) || toType.IsPromotableTo(fromType));
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00011AEE File Offset: 0x0000FCEE
		internal static bool IsEqualComparable(this ConceptualResultType type)
		{
			return type is ConceptualPrimitiveResultType || type is ConceptualTableType || type is ConceptualRowType;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00011B0B File Offset: 0x0000FD0B
		internal static bool IsEqualComparableTo(this ConceptualResultType fromType, ConceptualResultType toType)
		{
			return fromType.IsEqualComparable() && toType.IsEqualComparable() && (fromType.IsPromotableTo(toType) || toType.IsPromotableTo(fromType));
		}

		// Token: 0x040003EA RID: 1002
		private static readonly IReadOnlyList<IReadOnlyList<ConceptualPrimitiveType>> _promotionTypes = ConceptualTypeSemantics.CreatePromotableTypes();

		// Token: 0x040003EB RID: 1003
		private static readonly IReadOnlyDictionary<Type, ConceptualPrimitiveResultType> _clrTypeToPrimitiveType = ConceptualTypeSemantics.CreateClrTypeToPrimitiveMap();
	}
}
