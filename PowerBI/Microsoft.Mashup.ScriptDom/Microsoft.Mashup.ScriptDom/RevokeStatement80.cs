using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000252 RID: 594
	[Serializable]
	internal class RevokeStatement80 : SecurityStatementBody80
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x00163AB1 File Offset: 0x00161CB1
		// (set) Token: 0x06002615 RID: 9749 RVA: 0x00163AB9 File Offset: 0x00161CB9
		public bool GrantOptionFor
		{
			get
			{
				return this._grantOptionFor;
			}
			set
			{
				this._grantOptionFor = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06002616 RID: 9750 RVA: 0x00163AC2 File Offset: 0x00161CC2
		// (set) Token: 0x06002617 RID: 9751 RVA: 0x00163ACA File Offset: 0x00161CCA
		public bool CascadeOption
		{
			get
			{
				return this._cascadeOption;
			}
			set
			{
				this._cascadeOption = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06002618 RID: 9752 RVA: 0x00163AD3 File Offset: 0x00161CD3
		// (set) Token: 0x06002619 RID: 9753 RVA: 0x00163ADB File Offset: 0x00161CDB
		public Identifier AsClause
		{
			get
			{
				return this._asClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._asClause = value;
			}
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x00163AEB File Offset: 0x00161CEB
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x00163AF7 File Offset: 0x00161CF7
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.AsClause != null)
			{
				this.AsClause.Accept(visitor);
			}
		}

		// Token: 0x04001B3B RID: 6971
		private bool _grantOptionFor;

		// Token: 0x04001B3C RID: 6972
		private bool _cascadeOption;

		// Token: 0x04001B3D RID: 6973
		private Identifier _asClause;
	}
}
