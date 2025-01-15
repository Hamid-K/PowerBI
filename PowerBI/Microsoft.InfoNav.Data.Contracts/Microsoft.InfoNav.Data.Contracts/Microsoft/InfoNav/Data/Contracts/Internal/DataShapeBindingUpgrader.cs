using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001D9 RID: 473
	internal sealed class DataShapeBindingUpgrader
	{
		// Token: 0x06000CBC RID: 3260 RVA: 0x00018E59 File Offset: 0x00017059
		private DataShapeBindingUpgrader(IErrorContext errorContext, IFederatedConceptualSchema federatedSchema)
		{
			this._errorContext = errorContext;
			this._federatedSchema = federatedSchema;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00018E6F File Offset: 0x0001706F
		internal static bool TryUpgrade(IErrorContext errorContext, DataShapeBinding binding, IFederatedConceptualSchema federatedSchema)
		{
			return binding == null || new DataShapeBindingUpgrader(errorContext, federatedSchema).TryUpgrade(binding);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00018E89 File Offset: 0x00017089
		private bool TryUpgrade(DataShapeBinding binding)
		{
			return this.TryUpgradeHighlights(binding) && (!DataShapeBindingUpgrader.ShouldUpgrade(binding, 1) || this.UpgradeV000ToV001(binding)) && this.TryUpgradeAggregates(binding);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00018EB8 File Offset: 0x000170B8
		private static bool ShouldUpgrade(DataShapeBinding binding, int version)
		{
			if (binding.Version != null)
			{
				int? version2 = binding.Version;
				return (version2.GetValueOrDefault() < version) & (version2 != null);
			}
			return true;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00018EF4 File Offset: 0x000170F4
		private bool TryUpgradeHighlights(DataShapeBinding binding)
		{
			if (!binding.Highlights.IsNullOrEmptyCollection<FilterDefinition>())
			{
				for (int i = 0; i < binding.Highlights.Count; i++)
				{
					if (!QueryDefinitionUpgrader.TryUpgrade(this._errorContext, binding.Highlights[i], this._federatedSchema, null))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00018F50 File Offset: 0x00017150
		private bool TryUpgradeAggregates(DataShapeBinding binding)
		{
			if (binding.Aggregates == null)
			{
				return true;
			}
			for (int i = 0; i < binding.Aggregates.Count; i++)
			{
				DataShapeBindingAggregate dataShapeBindingAggregate = binding.Aggregates[i];
				if (dataShapeBindingAggregate.Kind != DataShapeBindingAggregateKind.None)
				{
					List<DataShapeBindingAggregateContainer> list = ((dataShapeBindingAggregate.Aggregations != null) ? dataShapeBindingAggregate.Aggregations.ToList<DataShapeBindingAggregateContainer>() : new List<DataShapeBindingAggregateContainer>());
					if (dataShapeBindingAggregate.Kind.HasFlag(DataShapeBindingAggregateKind.Max))
					{
						dataShapeBindingAggregate.Kind ^= DataShapeBindingAggregateKind.Max;
						list.Add(DataShapeBindingMaxAggregate.CreateContainer(IncludeAllTypes.Default));
					}
					if (dataShapeBindingAggregate.Kind.HasFlag(DataShapeBindingAggregateKind.Min))
					{
						dataShapeBindingAggregate.Kind ^= DataShapeBindingAggregateKind.Min;
						list.Add(DataShapeBindingMinAggregate.CreateContainer(IncludeAllTypes.Default));
					}
					dataShapeBindingAggregate.Aggregations = list;
				}
			}
			return true;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00019022 File Offset: 0x00017222
		private bool UpgradeV000ToV001(DataShapeBinding binding)
		{
			this.ResolveAutomaticSubtotals(binding);
			binding.Version = new int?(1);
			return true;
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00019038 File Offset: 0x00017238
		private void ResolveAutomaticSubtotals(DataShapeBinding binding)
		{
			bool flag = binding.Secondary != null && !binding.Secondary.Groupings.IsNullOrEmptyCollection<DataShapeBindingAxisGrouping>();
			bool flag2 = binding.Aggregates != null;
			SubtotalType subtotalType = SubtotalType.None;
			if (!flag && !flag2)
			{
				subtotalType = SubtotalType.Before;
			}
			DataShapeBindingUpgrader.ResolveMissingSubtotals(binding.Primary, subtotalType);
			DataShapeBindingUpgrader.ResolveMissingSubtotals(binding.Secondary, SubtotalType.None);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00019094 File Offset: 0x00017294
		internal static void ResolveMissingSubtotals(DataShapeBindingAxis axis, SubtotalType subtotal)
		{
			if (axis == null || axis.Groupings.IsNullOrEmptyCollection<DataShapeBindingAxisGrouping>())
			{
				return;
			}
			IList<DataShapeBindingAxisGrouping> groupings = axis.Groupings;
			for (int i = 0; i < groupings.Count; i++)
			{
				DataShapeBindingAxisGrouping dataShapeBindingAxisGrouping = groupings[i];
				if (dataShapeBindingAxisGrouping.Subtotal == null)
				{
					dataShapeBindingAxisGrouping.Subtotal = new SubtotalType?(subtotal);
				}
			}
		}

		// Token: 0x040006A4 RID: 1700
		private readonly IErrorContext _errorContext;

		// Token: 0x040006A5 RID: 1701
		private readonly IFederatedConceptualSchema _federatedSchema;
	}
}
