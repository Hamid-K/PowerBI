using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003BB RID: 955
	[Serializable]
	internal class GroupingSetsGroupingSpecification : GroupingSpecification
	{
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06002EAC RID: 11948 RVA: 0x0016C87A File Offset: 0x0016AA7A
		public IList<GroupingSpecification> Sets
		{
			get
			{
				return this._sets;
			}
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x0016C882 File Offset: 0x0016AA82
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x0016C890 File Offset: 0x0016AA90
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Sets.Count;
			while (i < count)
			{
				this.Sets[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DB1 RID: 7601
		private List<GroupingSpecification> _sets = new List<GroupingSpecification>();
	}
}
