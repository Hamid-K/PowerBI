using System;
using Microsoft.DataShaping.InternalContracts.Model;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200005F RID: 95
	internal class DsqFilterConditionGenerationOptions
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x00010A93 File Offset: 0x0000EC93
		private DsqFilterConditionGenerationOptions(bool flattenNegatedTupleInFilters, bool suppressSlicersAndApplyFilters, bool suppressExistsFilters)
		{
			this.FlattenNegatedTupleInFilters = flattenNegatedTupleInFilters;
			this.SuppressSlicersAndApplyFilters = suppressSlicersAndApplyFilters;
			this.SuppressExistsFilters = suppressExistsFilters;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00010AB0 File Offset: 0x0000ECB0
		internal bool FlattenNegatedTupleInFilters { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00010AB8 File Offset: 0x0000ECB8
		internal bool SuppressSlicersAndApplyFilters { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00010AC0 File Offset: 0x0000ECC0
		internal bool SuppressExistsFilters { get; }

		// Token: 0x06000464 RID: 1124 RVA: 0x00010AC8 File Offset: 0x0000ECC8
		internal static DsqFilterConditionGenerationOptions Create(ConceptualCapabilities conceptualCapabilities)
		{
			return DsqFilterConditionGenerationOptions.Create(conceptualCapabilities, false, false);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00010AD2 File Offset: 0x0000ECD2
		internal static DsqFilterConditionGenerationOptions Create(ConceptualCapabilities conceptualCapabilities, bool suppressSlicersAndApplyFilters, bool suppressExistsFilters)
		{
			return new DsqFilterConditionGenerationOptions(!conceptualCapabilities.SupportsHierarchicalFilterDisjunction(), suppressSlicersAndApplyFilters, suppressExistsFilters);
		}

		// Token: 0x0400025E RID: 606
		internal static readonly DsqFilterConditionGenerationOptions ForInstanceFilters = new DsqFilterConditionGenerationOptions(false, true, true);
	}
}
