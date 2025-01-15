using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200043D RID: 1085
	[Serializable]
	internal class DatabaseAuditAction : TSqlFragment
	{
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x0016F77E File Offset: 0x0016D97E
		// (set) Token: 0x060031A2 RID: 12706 RVA: 0x0016F786 File Offset: 0x0016D986
		public DatabaseAuditActionKind ActionKind
		{
			get
			{
				return this._actionKind;
			}
			set
			{
				this._actionKind = value;
			}
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x0016F78F File Offset: 0x0016D98F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x0016F79B File Offset: 0x0016D99B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E7D RID: 7805
		private DatabaseAuditActionKind _actionKind;
	}
}
