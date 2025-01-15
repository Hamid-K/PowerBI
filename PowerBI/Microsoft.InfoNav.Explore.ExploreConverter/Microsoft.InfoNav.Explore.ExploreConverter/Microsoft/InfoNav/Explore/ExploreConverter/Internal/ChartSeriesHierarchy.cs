using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000092 RID: 146
	internal sealed class ChartSeriesHierarchy
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000CD98 File Offset: 0x0000AF98
		internal ChartSeriesHierarchy(List<ChartMember> chartMembers)
		{
			this._chartMembers = chartMembers;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000CDA7 File Offset: 0x0000AFA7
		public List<ChartMember> ChartMembers
		{
			get
			{
				return this._chartMembers;
			}
		}

		// Token: 0x040001ED RID: 493
		private readonly List<ChartMember> _chartMembers;
	}
}
