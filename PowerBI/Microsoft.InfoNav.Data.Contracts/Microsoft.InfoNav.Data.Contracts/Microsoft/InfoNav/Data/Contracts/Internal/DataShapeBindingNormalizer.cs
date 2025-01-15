using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001BD RID: 445
	internal sealed class DataShapeBindingNormalizer
	{
		// Token: 0x06000BC9 RID: 3017 RVA: 0x000171F5 File Offset: 0x000153F5
		private DataShapeBindingNormalizer(IErrorContext errorContext)
		{
			this._errorContext = errorContext;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00017204 File Offset: 0x00015404
		internal static bool TryNormalize(IErrorContext errorContext, DataShapeBinding binding, string rootQueryName)
		{
			return binding == null || new DataShapeBindingNormalizer(errorContext).TryNormalize(binding, rootQueryName);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00017220 File Offset: 0x00015420
		private bool TryNormalize(DataShapeBinding binding, string rootQueryName)
		{
			DataShapeBindingNormalizer.NormalizeMissingSubtotals(binding);
			DataShapeBindingNormalizer.NormalizeExpansionState(binding);
			DataShapeBindingNormalizer.NormalizeQueryReferenceSourceNames<DataShapeBindingSuppressedJoinPredicate>(binding.SuppressedJoinPredicatesByName, (DataShapeBindingSuppressedJoinPredicate sjp) => sjp.QueryReference, rootQueryName);
			DataShapeBindingNormalizer.NormalizeQueryReferenceSourceNames<DataShapeBindingHiddenProjections>(binding.HiddenProjections, (DataShapeBindingHiddenProjections hiddenProjection) => hiddenProjection.QueryReference, rootQueryName);
			return true;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00017290 File Offset: 0x00015490
		private static void NormalizeQueryReferenceSourceNames<T>(IList<T> list, Func<T, DataShapeBindingQueryReference> getQueryReference, string rootQueryName)
		{
			if (list == null)
			{
				return;
			}
			foreach (T t in list)
			{
				DataShapeBindingQueryReference dataShapeBindingQueryReference = getQueryReference(t);
				if (dataShapeBindingQueryReference.SourceName == null)
				{
					dataShapeBindingQueryReference.SourceName = rootQueryName;
				}
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x000172EC File Offset: 0x000154EC
		private static void NormalizeMissingSubtotals(DataShapeBinding binding)
		{
			DataShapeBindingUpgrader.ResolveMissingSubtotals(binding.Primary, SubtotalType.None);
			DataShapeBindingUpgrader.ResolveMissingSubtotals(binding.Secondary, SubtotalType.None);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00017306 File Offset: 0x00015506
		private static void NormalizeExpansionState(DataShapeBinding binding)
		{
			DataShapeBindingNormalizer.NormalizeExpansionState(binding.Primary);
			DataShapeBindingNormalizer.NormalizeExpansionState(binding.Secondary);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00017320 File Offset: 0x00015520
		private static void NormalizeExpansionState(DataShapeBindingAxis axis)
		{
			if (axis == null)
			{
				return;
			}
			if (axis.Expansion == null || axis.Expansion.Instances == null || (axis.Expansion.Instances.Children.IsNullOrEmptyCollection<DataShapeBindingAxisExpansionInstance>() && axis.Expansion.Instances.Values.IsNullOrEmptyCollection<QueryExpressionContainer>()) || axis.Groupings.IsNullOrEmptyCollection<DataShapeBindingAxisGrouping>())
			{
				axis.Expansion = null;
				return;
			}
			List<List<FilterDefinition>> list = ExpansionStateConverter.ConvertToInstanceFilters(axis.Expansion, axis.Groupings.Count);
			for (int i = 0; i < list.Count; i++)
			{
				axis.Groupings[i + 1].InstanceFilters = list[i];
			}
			axis.Expansion = null;
		}

		// Token: 0x04000652 RID: 1618
		private readonly IErrorContext _errorContext;
	}
}
