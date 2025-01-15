using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav
{
	// Token: 0x02000018 RID: 24
	public sealed class ConceptualColumnGrouping
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002503 File Offset: 0x00000703
		public ConceptualColumnGrouping(GroupingIdentity identity, IReadOnlyList<IConceptualColumn> identityColumns, IReadOnlyList<IConceptualColumn> queryGroupColumns, IReadOnlyList<IConceptualColumn> groupByColumns, bool queryGroupColumnsCoverEntityKey)
		{
			this._identity = identity;
			this._identityColumns = identityColumns;
			this._queryGroupColumns = queryGroupColumns;
			this._groupByColumns = groupByColumns;
			this._queryGroupColumnsCoverEntityKey = queryGroupColumnsCoverEntityKey;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002530 File Offset: 0x00000730
		public GroupingIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002538 File Offset: 0x00000738
		public IReadOnlyList<IConceptualColumn> IdentityColumns
		{
			get
			{
				return this._identityColumns;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002540 File Offset: 0x00000740
		public bool IsIdentityOnEntityKey
		{
			get
			{
				return this.Identity == GroupingIdentity.EntityKey;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000254B File Offset: 0x0000074B
		public IReadOnlyList<IConceptualColumn> QueryGroupColumns
		{
			get
			{
				return this._queryGroupColumns;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002553 File Offset: 0x00000753
		public IReadOnlyList<IConceptualColumn> GroupByColumns
		{
			get
			{
				return this._groupByColumns;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000255B File Offset: 0x0000075B
		public bool IsQueryGroupOnEntityKey
		{
			get
			{
				return this._queryGroupColumnsCoverEntityKey;
			}
		}

		// Token: 0x0400005E RID: 94
		private readonly GroupingIdentity _identity;

		// Token: 0x0400005F RID: 95
		private readonly IReadOnlyList<IConceptualColumn> _identityColumns;

		// Token: 0x04000060 RID: 96
		private readonly IReadOnlyList<IConceptualColumn> _queryGroupColumns;

		// Token: 0x04000061 RID: 97
		private readonly IReadOnlyList<IConceptualColumn> _groupByColumns;

		// Token: 0x04000062 RID: 98
		private readonly bool _queryGroupColumnsCoverEntityKey;
	}
}
