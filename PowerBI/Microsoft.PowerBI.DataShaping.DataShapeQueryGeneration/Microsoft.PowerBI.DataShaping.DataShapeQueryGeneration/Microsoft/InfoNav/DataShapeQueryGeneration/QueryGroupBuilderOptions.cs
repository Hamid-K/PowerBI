using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000A8 RID: 168
	internal sealed class QueryGroupBuilderOptions
	{
		// Token: 0x06000640 RID: 1600 RVA: 0x000187F4 File Offset: 0x000169F4
		internal QueryGroupBuilderOptions(bool projectIdentityOnly, bool omitOrderBy, bool suppressAutomaticGroupSorts, bool suppressModelGrouping, bool generateRestartIdentities, bool trackGroupKeysAndSortKeysForReferencing)
		{
			this.ProjectIdentityOnly = projectIdentityOnly;
			this.OmitModelOrderBy = omitOrderBy;
			this.SuppressAutomaticGroupSorts = suppressAutomaticGroupSorts;
			this.SuppressModelGrouping = suppressModelGrouping;
			this.GenerateRestartIdentities = generateRestartIdentities;
			this.TrackGroupKeysAndSortKeysForReferencing = trackGroupKeysAndSortKeysForReferencing;
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x00018829 File Offset: 0x00016A29
		internal bool ProjectIdentityOnly { get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00018831 File Offset: 0x00016A31
		internal bool OmitModelOrderBy { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00018839 File Offset: 0x00016A39
		internal bool SuppressAutomaticGroupSorts { get; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00018841 File Offset: 0x00016A41
		internal bool SuppressModelGrouping { get; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x00018849 File Offset: 0x00016A49
		internal bool GenerateRestartIdentities { get; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00018851 File Offset: 0x00016A51
		internal bool TrackGroupKeysAndSortKeysForReferencing { get; }

		// Token: 0x04000356 RID: 854
		internal static readonly QueryGroupBuilderOptions AllDisabledOptions = new QueryGroupBuilderOptions(false, false, false, false, false, false);
	}
}
