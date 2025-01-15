using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Normalization
{
	// Token: 0x0200009B RID: 155
	internal sealed class RestartTokenToStartPositionTranslator
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x0001B51C File Offset: 0x0001971C
		internal RestartTokenToStartPositionTranslator(TranslationErrorContext errorContext, List<RestartTokenGroupingBinding> restartBindings, List<RestartTokenGroupingValues> restartGroupingValues)
		{
			this.m_errorContext = errorContext;
			this.m_restartBindings = restartBindings;
			this.m_restartGroupingValues = restartGroupingValues;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001B53C File Offset: 0x0001973C
		internal static RestartTokenToStartPositionTranslator Create(TranslationErrorContext errorContext, List<RestartToken> restartTokens, List<RestartTokenGroupingBinding> restartBindings)
		{
			RestartTokenGroupingBinding[] array;
			List<RestartTokenGroupingValues> list = RestartTokenUtils.MapRestartTokenToGroupingValues(restartTokens, restartBindings, out array);
			return new RestartTokenToStartPositionTranslator(errorContext, restartBindings, list);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001B55C File Offset: 0x0001975C
		internal static RestartTokenToStartPositionTranslator Create(TranslationErrorContext errorContext, IReadOnlyList<IReadOnlyList<RestartToken>> restartTokens, IReadOnlyList<IReadOnlyList<RestartTokenGroupingBinding>> restartBindings)
		{
			List<RestartTokenGroupingBinding> list = new List<RestartTokenGroupingBinding>();
			List<RestartTokenGroupingValues> list2 = new List<RestartTokenGroupingValues>();
			for (int i = 0; i < restartTokens.Count; i++)
			{
				RestartTokenGroupingBinding[] array;
				List<RestartTokenGroupingValues> list3 = RestartTokenUtils.MapRestartTokenToGroupingValues(restartTokens[i], restartBindings[i], out array);
				list2.AddRange(list3);
				list.AddRange(restartBindings[i]);
			}
			return new RestartTokenToStartPositionTranslator(errorContext, list, list2);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001B5B8 File Offset: 0x000197B8
		public ScopeId GetNextStartPosition(Identifier id)
		{
			RestartTokenGroupingValues restartGroupingValues = this.GetRestartGroupingValues(id, false);
			if (restartGroupingValues == null)
			{
				return null;
			}
			RestartToken groupToken = restartGroupingValues.GroupToken;
			List<ScopeValue> list = new List<ScopeValue>(groupToken.Count);
			foreach (Candidate<ScalarValue> candidate in groupToken)
			{
				list.Add(new ScopeValue
				{
					Value = candidate
				});
			}
			return new ScopeId
			{
				Values = list
			};
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001B63C File Offset: 0x0001983C
		public Candidate<bool> GetNextSubtotalStartPosition(Identifier id)
		{
			RestartTokenGroupingValues restartGroupingValues = this.GetRestartGroupingValues(id, true);
			if (restartGroupingValues == null)
			{
				return null;
			}
			RestartToken subtotalToken = restartGroupingValues.SubtotalToken;
			if (subtotalToken == null)
			{
				return Candidate<bool>.Valid(false);
			}
			return Candidate<bool>.Valid((bool)subtotalToken[0].Value.Value);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001B688 File Offset: 0x00019888
		private RestartTokenGroupingValues GetRestartGroupingValues(Identifier id, bool isSubtotal)
		{
			for (int i = 0; i < this.m_restartBindings.Count; i++)
			{
				RestartTokenGroupingBinding restartTokenGroupingBinding = this.m_restartBindings[i];
				if (isSubtotal)
				{
					if (restartTokenGroupingBinding.SubtotalMemberId == id.Value)
					{
						return this.m_restartGroupingValues[i];
					}
				}
				else if (restartTokenGroupingBinding.MemberId == id.Value)
				{
					return this.m_restartGroupingValues[i];
				}
			}
			return null;
		}

		// Token: 0x04000379 RID: 889
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x0400037A RID: 890
		private readonly List<RestartTokenGroupingBinding> m_restartBindings;

		// Token: 0x0400037B RID: 891
		private readonly List<RestartTokenGroupingValues> m_restartGroupingValues;
	}
}
