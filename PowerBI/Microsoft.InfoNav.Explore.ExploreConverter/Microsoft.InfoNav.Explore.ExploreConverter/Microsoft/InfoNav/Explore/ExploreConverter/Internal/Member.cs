using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000070 RID: 112
	internal class Member
	{
		// Token: 0x0600023C RID: 572 RVA: 0x0000BA2B File Offset: 0x00009C2B
		internal Member(Group group, List<SortExpression> sortExpressions)
		{
			this._group = group;
			this._sortExpressions = sortExpressions;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000BA41 File Offset: 0x00009C41
		internal Group Group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000BA49 File Offset: 0x00009C49
		internal List<SortExpression> SortExpressions
		{
			get
			{
				return this._sortExpressions;
			}
		}

		// Token: 0x04000183 RID: 387
		private readonly Group _group;

		// Token: 0x04000184 RID: 388
		private readonly List<SortExpression> _sortExpressions;
	}
}
