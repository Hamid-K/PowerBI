using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003AC RID: 940
	[Serializable]
	internal class UnqualifiedJoin : JoinTableReference
	{
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06002E54 RID: 11860 RVA: 0x0016C32E File Offset: 0x0016A52E
		// (set) Token: 0x06002E55 RID: 11861 RVA: 0x0016C336 File Offset: 0x0016A536
		public UnqualifiedJoinType UnqualifiedJoinType
		{
			get
			{
				return this._unqualifiedJoinType;
			}
			set
			{
				this._unqualifiedJoinType = value;
			}
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x0016C33F File Offset: 0x0016A53F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x0016C34B File Offset: 0x0016A54B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D99 RID: 7577
		private UnqualifiedJoinType _unqualifiedJoinType;
	}
}
