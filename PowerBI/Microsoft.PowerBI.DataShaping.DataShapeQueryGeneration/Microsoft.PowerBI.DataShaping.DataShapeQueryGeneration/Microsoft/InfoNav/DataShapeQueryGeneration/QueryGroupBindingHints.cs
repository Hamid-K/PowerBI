using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000A5 RID: 165
	internal sealed class QueryGroupBindingHints
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x00017864 File Offset: 0x00015A64
		internal QueryGroupBindingHints(Dictionary<DsqSortKey, ModelSortBindingInfo> modelSorts, HashSet<DsqSortKey> restartIdentities, bool trackNonMeasureSortKeysForReferencing)
		{
			this.ModelSorts = modelSorts;
			this.RestartIdentities = restartIdentities;
			this.TrackNonMeasureSortKeysForReferencing = trackNonMeasureSortKeysForReferencing;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00017881 File Offset: 0x00015A81
		private Dictionary<DsqSortKey, ModelSortBindingInfo> ModelSorts { get; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00017889 File Offset: 0x00015A89
		private HashSet<DsqSortKey> RestartIdentities { get; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00017891 File Offset: 0x00015A91
		internal bool TrackNonMeasureSortKeysForReferencing { get; }

		// Token: 0x06000613 RID: 1555 RVA: 0x00017899 File Offset: 0x00015A99
		internal bool IsRestartIdentity(DsqSortKey sortKey)
		{
			return this.RestartIdentities.Contains(sortKey);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000178A7 File Offset: 0x00015AA7
		internal bool TryGetModelSortBindingInfo(DsqSortKey sortKey, out ModelSortBindingInfo info)
		{
			return this.ModelSorts.TryGetValue(sortKey, out info);
		}
	}
}
