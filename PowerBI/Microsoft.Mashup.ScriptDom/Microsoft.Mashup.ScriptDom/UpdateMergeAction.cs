using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000435 RID: 1077
	[Serializable]
	internal class UpdateMergeAction : MergeAction
	{
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06003176 RID: 12662 RVA: 0x0016F424 File Offset: 0x0016D624
		public IList<SetClause> SetClauses
		{
			get
			{
				return this._setClauses;
			}
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x0016F42C File Offset: 0x0016D62C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x0016F438 File Offset: 0x0016D638
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.SetClauses.Count;
			while (i < count)
			{
				this.SetClauses[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E70 RID: 7792
		private List<SetClause> _setClauses = new List<SetClause>();
	}
}
