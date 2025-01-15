using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000042 RID: 66
	internal sealed class DataShapeGenerationOptions
	{
		// Token: 0x0600024A RID: 586 RVA: 0x0000A547 File Offset: 0x00008747
		internal DataShapeGenerationOptions(IReadOnlyList<int> selectIndicesToPreserve, bool projectIdentityOnly, bool omitOrderBy, bool suppressAutomaticGroupSorts, bool suppressModelGrouping, bool trackGroupKeysAndSortKeysForReferencing, AllowedExpressionContentFlags allowedSelectExpressionContent)
		{
			this.SelectIndicesToPreserve = selectIndicesToPreserve;
			this.ProjectIdentityOnly = projectIdentityOnly;
			this.OmitOrderBy = omitOrderBy;
			this.SuppressAutomaticGroupSorts = suppressAutomaticGroupSorts;
			this.SuppressModelGrouping = suppressModelGrouping;
			this.TrackGroupKeysAndSortKeysForReferencing = trackGroupKeysAndSortKeysForReferencing;
			this.AllowedSelectExpressionContent = allowedSelectExpressionContent;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000A584 File Offset: 0x00008784
		public IReadOnlyList<int> SelectIndicesToPreserve { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000A58C File Offset: 0x0000878C
		public bool ProjectIdentityOnly { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000A594 File Offset: 0x00008794
		public bool OmitOrderBy { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000A59C File Offset: 0x0000879C
		public bool SuppressAutomaticGroupSorts { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A5A4 File Offset: 0x000087A4
		public bool SuppressModelGrouping { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000A5AC File Offset: 0x000087AC
		public bool TrackGroupKeysAndSortKeysForReferencing { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000A5B4 File Offset: 0x000087B4
		public AllowedExpressionContentFlags AllowedSelectExpressionContent { get; }

		// Token: 0x0400011F RID: 287
		internal static readonly DataShapeGenerationOptions Empty = new DataShapeGenerationOptions(null, false, false, false, false, false, AllowedExpressionContent.TopLevelQuerySelect);
	}
}
