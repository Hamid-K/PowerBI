using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200008D RID: 141
	internal sealed class ChartCategoryHierarchy
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x0000CC98 File Offset: 0x0000AE98
		internal ChartCategoryHierarchy(List<ChartMember> chartMembers, bool enableDrillDown)
		{
			this._chartMembers = chartMembers;
			this._enableDrilldown = enableDrillDown;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000CCAE File Offset: 0x0000AEAE
		internal List<ChartMember> ChartMembers
		{
			get
			{
				return this._chartMembers;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000CCB6 File Offset: 0x0000AEB6
		internal bool EnableDrilldown
		{
			get
			{
				return this._enableDrilldown;
			}
		}

		// Token: 0x040001DF RID: 479
		private readonly List<ChartMember> _chartMembers;

		// Token: 0x040001E0 RID: 480
		private readonly bool _enableDrilldown;
	}
}
