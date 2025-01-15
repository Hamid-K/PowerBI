using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000B6 RID: 182
	internal static class RestartTokenUtils
	{
		// Token: 0x0600043D RID: 1085 RVA: 0x0000793C File Offset: 0x00005B3C
		internal static List<RestartToken> ToRestartTokens(this IReadOnlyList<IReadOnlyList<PrimitiveValue>> parsedTokens)
		{
			if (parsedTokens == null)
			{
				return null;
			}
			List<RestartToken> list = new List<RestartToken>(parsedTokens.Count);
			foreach (IReadOnlyList<PrimitiveValue> readOnlyList in parsedTokens)
			{
				list.Add(readOnlyList.ToRestartToken());
			}
			return list;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000799C File Offset: 0x00005B9C
		internal static RestartToken ToRestartToken(this IReadOnlyList<PrimitiveValue> parsedToken)
		{
			RestartToken restartToken = new RestartToken();
			foreach (PrimitiveValue primitiveValue in parsedToken)
			{
				restartToken.Add(Candidate<ScalarValue>.Valid(new ScalarValue(primitiveValue.GetValueAsObject())));
			}
			return restartToken;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000079FC File Offset: 0x00005BFC
		internal static List<RestartTokenGroupingValues> MapRestartTokenToGroupingValues(IReadOnlyList<RestartToken> restartTokens, IReadOnlyList<RestartTokenGroupingBinding> restartBindings, out RestartTokenGroupingBinding[] restartTokenToBinding)
		{
			List<RestartTokenGroupingValues> list = new List<RestartTokenGroupingValues>(restartBindings.Count);
			restartTokenToBinding = new RestartTokenGroupingBinding[restartTokens.Count];
			int num = 0;
			int num2 = restartTokens.Count - 1;
			for (int i = 0; i < restartBindings.Count; i++)
			{
				RestartTokenGroupingValues restartTokenGroupingValues = RestartTokenUtils.FindRestartTokens(restartTokens, restartBindings[i], restartTokenToBinding, ref num, ref num2);
				list.Add(restartTokenGroupingValues);
			}
			return list;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00007A5C File Offset: 0x00005C5C
		private static RestartTokenGroupingValues FindRestartTokens(IReadOnlyList<RestartToken> restartTokens, RestartTokenGroupingBinding binding, RestartTokenGroupingBinding[] restartTokenToBinding, ref int firstIndexWithoutBinding, ref int lastIndexWithoutBinding)
		{
			if (firstIndexWithoutBinding == restartTokens.Count || firstIndexWithoutBinding > lastIndexWithoutBinding)
			{
				return null;
			}
			Contract.RetailAssert(restartTokenToBinding[firstIndexWithoutBinding] == null, "FirstIndexWithoutBinding should have no binding assigned");
			RestartToken restartToken = restartTokens[firstIndexWithoutBinding];
			restartTokenToBinding[firstIndexWithoutBinding] = binding;
			firstIndexWithoutBinding++;
			if (binding.SubtotalPosition == SubtotalType.Before)
			{
				if (RestartTokenUtils.MatchesSubtotalTokenPattern(restartToken))
				{
					Contract.RetailAssert(firstIndexWithoutBinding < restartTokens.Count, "Subtotal token (before) identified without a corresponding group token");
					Contract.RetailAssert(restartTokenToBinding[firstIndexWithoutBinding] == null, "Group token accompanying a subtotal token was already assigned");
					RestartToken restartToken2 = restartTokens[firstIndexWithoutBinding];
					restartTokenToBinding[firstIndexWithoutBinding] = binding;
					firstIndexWithoutBinding++;
					return new RestartTokenGroupingValues(restartToken2, restartToken);
				}
				return new RestartTokenGroupingValues(restartToken, null);
			}
			else
			{
				if (binding.SubtotalPosition == SubtotalType.After)
				{
					RestartToken restartToken3 = null;
					if (restartTokenToBinding[lastIndexWithoutBinding] == null && lastIndexWithoutBinding >= firstIndexWithoutBinding && RestartTokenUtils.MatchesSubtotalTokenPattern(restartTokens[lastIndexWithoutBinding]))
					{
						restartToken3 = restartTokens[lastIndexWithoutBinding];
						restartTokenToBinding[lastIndexWithoutBinding] = binding;
						lastIndexWithoutBinding--;
					}
					return new RestartTokenGroupingValues(restartToken, restartToken3);
				}
				return new RestartTokenGroupingValues(restartToken, null);
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00007B4C File Offset: 0x00005D4C
		private static bool MatchesSubtotalTokenPattern(RestartToken token)
		{
			return token.Count == 1 && token[0].IsValid && token[0].Value.IsOfType<bool>();
		}
	}
}
