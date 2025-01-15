using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003B7 RID: 951
	[Serializable]
	internal class CompositeGroupingSpecification : GroupingSpecification
	{
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06002E9D RID: 11933 RVA: 0x0016C727 File Offset: 0x0016A927
		public IList<GroupingSpecification> Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x0016C72F File Offset: 0x0016A92F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x0016C73C File Offset: 0x0016A93C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Items.Count;
			while (i < count)
			{
				this.Items[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DAE RID: 7598
		private List<GroupingSpecification> _items = new List<GroupingSpecification>();
	}
}
