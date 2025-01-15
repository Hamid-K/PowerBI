using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.ConceptualResultTypes
{
	// Token: 0x02000252 RID: 594
	internal static class FunctionOverloadResolver
	{
		// Token: 0x060019FE RID: 6654 RVA: 0x00047A9C File Offset: 0x00045C9C
		internal static EdmFunction ResolveFunctionOverloads(IEnumerable<EdmFunction> candidateFunctions, IReadOnlyList<ConceptualResultType> argTypes, out bool isAmbiguous)
		{
			EdmFunction edmFunction = null;
			isAmbiguous = false;
			List<int[]> list = new List<int[]>();
			int[] bestCandidateRank = null;
			int num = int.MinValue;
			foreach (EdmFunction edmFunction2 in candidateFunctions)
			{
				int num2;
				int[] array;
				if (FunctionOverloadResolver.TryRankFunctionParameters(argTypes, edmFunction2.Parameters, out num2, out array))
				{
					if (num2 == num)
					{
						isAmbiguous = true;
					}
					else if (num2 > num)
					{
						isAmbiguous = false;
						num = num2;
						edmFunction = edmFunction2;
						bestCandidateRank = array;
					}
					list.Add(array);
				}
			}
			if (edmFunction != null && !isAmbiguous && argTypes.Count > 1 && list.Count > 1)
			{
				isAmbiguous = list.Any(delegate(int[] rank)
				{
					if (bestCandidateRank != rank)
					{
						for (int i = 0; i < rank.Length; i++)
						{
							if (bestCandidateRank[i] < rank[i])
							{
								return true;
							}
						}
					}
					return false;
				});
			}
			return edmFunction;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00047B6C File Offset: 0x00045D6C
		private static bool TryRankFunctionParameters(IReadOnlyList<ConceptualResultType> argumentList, IReadOnlyList<FunctionParameter> overloadParamList, out int totalRank, out int[] parameterRanks)
		{
			totalRank = 0;
			parameterRanks = null;
			if (argumentList.Count != overloadParamList.Count)
			{
				return false;
			}
			parameterRanks = new int[argumentList.Count];
			for (int i = 0; i < parameterRanks.Length; i++)
			{
				ConceptualResultType conceptualResultType = argumentList[i];
				ConceptualResultType type = overloadParamList[i].Type;
				if (!conceptualResultType.IsPromotableTo(type))
				{
					return false;
				}
				int promotionRank = FunctionOverloadResolver.GetPromotionRank(conceptualResultType, type);
				totalRank += promotionRank;
				parameterRanks[i] = promotionRank;
			}
			return true;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00047BE0 File Offset: 0x00045DE0
		private static int GetPromotionRank(ConceptualResultType fromType, ConceptualResultType toType)
		{
			if (fromType.Equals(toType) || fromType.IsNullType())
			{
				return 0;
			}
			ConceptualCollectionType conceptualCollectionType = fromType as ConceptualCollectionType;
			ConceptualCollectionType conceptualCollectionType2 = toType as ConceptualCollectionType;
			if (conceptualCollectionType != null && conceptualCollectionType2 != null)
			{
				return FunctionOverloadResolver.GetPromotionRank(conceptualCollectionType.PrimitiveType, conceptualCollectionType2.PrimitiveType);
			}
			ConceptualPrimitiveResultType conceptualPrimitiveResultType = fromType as ConceptualPrimitiveResultType;
			ConceptualPrimitiveResultType conceptualPrimitiveResultType2 = toType as ConceptualPrimitiveResultType;
			if (conceptualPrimitiveResultType != null && conceptualPrimitiveResultType2 != null)
			{
				IReadOnlyList<ConceptualPrimitiveType> promotionTypes = ConceptualTypeSemantics.GetPromotionTypes(conceptualPrimitiveResultType.ConceptualDataType);
				int num = -1;
				for (int i = 0; i < promotionTypes.Count; i++)
				{
					if (promotionTypes[i] == conceptualPrimitiveResultType2.ConceptualDataType)
					{
						num = i;
						break;
					}
				}
				Microsoft.DataShaping.Contract.RetailAssert(num >= 0, "FailedToGeneratePromotionRank for Scalars from={0}, to={1}}", fromType, toType);
				return -num;
			}
			Microsoft.DataShaping.Contract.RetailFail("FailedToGeneratePromotionRank for from={0}, to={1}", fromType, toType);
			return 0;
		}
	}
}
