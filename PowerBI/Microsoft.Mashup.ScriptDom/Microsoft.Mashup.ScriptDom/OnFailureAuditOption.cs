using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200044D RID: 1101
	[Serializable]
	internal class OnFailureAuditOption : AuditOption
	{
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060031E7 RID: 12775 RVA: 0x0016FB25 File Offset: 0x0016DD25
		// (set) Token: 0x060031E8 RID: 12776 RVA: 0x0016FB2D File Offset: 0x0016DD2D
		public AuditFailureActionType OnFailureAction
		{
			get
			{
				return this._onFailureAction;
			}
			set
			{
				this._onFailureAction = value;
			}
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x0016FB36 File Offset: 0x0016DD36
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x0016FB42 File Offset: 0x0016DD42
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E8A RID: 7818
		private AuditFailureActionType _onFailureAction;
	}
}
