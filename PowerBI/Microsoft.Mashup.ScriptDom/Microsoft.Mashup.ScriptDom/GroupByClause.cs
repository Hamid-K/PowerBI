using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003B4 RID: 948
	[Serializable]
	internal class GroupByClause : TSqlFragment
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06002E8E RID: 11918 RVA: 0x0016C645 File Offset: 0x0016A845
		// (set) Token: 0x06002E8F RID: 11919 RVA: 0x0016C64D File Offset: 0x0016A84D
		public GroupByOption GroupByOption
		{
			get
			{
				return this._groupByOption;
			}
			set
			{
				this._groupByOption = value;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06002E90 RID: 11920 RVA: 0x0016C656 File Offset: 0x0016A856
		// (set) Token: 0x06002E91 RID: 11921 RVA: 0x0016C65E File Offset: 0x0016A85E
		public bool All
		{
			get
			{
				return this._all;
			}
			set
			{
				this._all = value;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06002E92 RID: 11922 RVA: 0x0016C667 File Offset: 0x0016A867
		public IList<GroupingSpecification> GroupingSpecifications
		{
			get
			{
				return this._groupingSpecifications;
			}
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x0016C66F File Offset: 0x0016A86F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x0016C67C File Offset: 0x0016A87C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.GroupingSpecifications.Count;
			while (i < count)
			{
				this.GroupingSpecifications[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DAA RID: 7594
		private GroupByOption _groupByOption;

		// Token: 0x04001DAB RID: 7595
		private bool _all;

		// Token: 0x04001DAC RID: 7596
		private List<GroupingSpecification> _groupingSpecifications = new List<GroupingSpecification>();
	}
}
