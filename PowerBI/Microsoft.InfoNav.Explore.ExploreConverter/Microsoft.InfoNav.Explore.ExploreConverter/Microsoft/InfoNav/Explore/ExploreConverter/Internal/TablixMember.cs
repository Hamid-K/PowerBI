using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000A6 RID: 166
	internal sealed class TablixMember : Member
	{
		// Token: 0x06000339 RID: 825 RVA: 0x0000D2BE File Offset: 0x0000B4BE
		internal TablixMember(Group group, List<SortExpression> sortExpressions, TablixHeader tablixHeader, List<TablixMember> childMembers, bool isSubtotal)
			: base(group, sortExpressions)
		{
			this._tablixHeader = tablixHeader;
			this._isSubtotal = isSubtotal;
			this._childMembers = childMembers;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000D2DF File Offset: 0x0000B4DF
		public TablixHeader TablixHeader
		{
			get
			{
				return this._tablixHeader;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000D2E7 File Offset: 0x0000B4E7
		public List<TablixMember> Members
		{
			get
			{
				return this._childMembers;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000D2EF File Offset: 0x0000B4EF
		public bool IsSubtotal
		{
			get
			{
				return this._isSubtotal;
			}
		}

		// Token: 0x04000224 RID: 548
		private readonly TablixHeader _tablixHeader;

		// Token: 0x04000225 RID: 549
		private readonly bool _isSubtotal;

		// Token: 0x04000226 RID: 550
		private readonly List<TablixMember> _childMembers;
	}
}
