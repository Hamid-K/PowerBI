using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200024E RID: 590
	[Serializable]
	internal class SecurityPrincipal : TSqlFragment
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x00163966 File Offset: 0x00161B66
		// (set) Token: 0x060025FC RID: 9724 RVA: 0x0016396E File Offset: 0x00161B6E
		public PrincipalType PrincipalType
		{
			get
			{
				return this._principalType;
			}
			set
			{
				this._principalType = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x00163977 File Offset: 0x00161B77
		// (set) Token: 0x060025FE RID: 9726 RVA: 0x0016397F File Offset: 0x00161B7F
		public Identifier Identifier
		{
			get
			{
				return this._identifier;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._identifier = value;
			}
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x0016398F File Offset: 0x00161B8F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x0016399B File Offset: 0x00161B9B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Identifier != null)
			{
				this.Identifier.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B34 RID: 6964
		private PrincipalType _principalType;

		// Token: 0x04001B35 RID: 6965
		private Identifier _identifier;
	}
}
