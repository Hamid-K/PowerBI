using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002DA RID: 730
	[Serializable]
	internal class CursorId : TSqlFragment
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06002926 RID: 10534 RVA: 0x00166E65 File Offset: 0x00165065
		// (set) Token: 0x06002927 RID: 10535 RVA: 0x00166E6D File Offset: 0x0016506D
		public bool IsGlobal
		{
			get
			{
				return this._isGlobal;
			}
			set
			{
				this._isGlobal = value;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06002928 RID: 10536 RVA: 0x00166E76 File Offset: 0x00165076
		// (set) Token: 0x06002929 RID: 10537 RVA: 0x00166E7E File Offset: 0x0016507E
		public IdentifierOrValueExpression Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x00166E8E File Offset: 0x0016508E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x00166E9A File Offset: 0x0016509A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C15 RID: 7189
		private bool _isGlobal;

		// Token: 0x04001C16 RID: 7190
		private IdentifierOrValueExpression _name;
	}
}
