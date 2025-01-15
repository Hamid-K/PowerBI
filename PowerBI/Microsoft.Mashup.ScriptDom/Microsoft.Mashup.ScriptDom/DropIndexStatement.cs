using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002E9 RID: 745
	[Serializable]
	internal class DropIndexStatement : TSqlStatement
	{
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06002974 RID: 10612 RVA: 0x00167331 File Offset: 0x00165531
		public IList<DropIndexClauseBase> DropIndexClauses
		{
			get
			{
				return this._dropIndexClauses;
			}
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x00167339 File Offset: 0x00165539
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x00167348 File Offset: 0x00165548
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.DropIndexClauses.Count;
			while (i < count)
			{
				this.DropIndexClauses[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C28 RID: 7208
		private List<DropIndexClauseBase> _dropIndexClauses = new List<DropIndexClauseBase>();
	}
}
