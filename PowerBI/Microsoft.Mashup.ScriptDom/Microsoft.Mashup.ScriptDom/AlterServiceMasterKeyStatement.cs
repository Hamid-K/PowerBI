using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200041B RID: 1051
	[Serializable]
	internal class AlterServiceMasterKeyStatement : TSqlStatement
	{
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060030E1 RID: 12513 RVA: 0x0016EBC0 File Offset: 0x0016CDC0
		// (set) Token: 0x060030E2 RID: 12514 RVA: 0x0016EBC8 File Offset: 0x0016CDC8
		public Literal Account
		{
			get
			{
				return this._account;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._account = value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060030E3 RID: 12515 RVA: 0x0016EBD8 File Offset: 0x0016CDD8
		// (set) Token: 0x060030E4 RID: 12516 RVA: 0x0016EBE0 File Offset: 0x0016CDE0
		public Literal Password
		{
			get
			{
				return this._password;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._password = value;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060030E5 RID: 12517 RVA: 0x0016EBF0 File Offset: 0x0016CDF0
		// (set) Token: 0x060030E6 RID: 12518 RVA: 0x0016EBF8 File Offset: 0x0016CDF8
		public AlterServiceMasterKeyOption Kind
		{
			get
			{
				return this._kind;
			}
			set
			{
				this._kind = value;
			}
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x0016EC01 File Offset: 0x0016CE01
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x0016EC0D File Offset: 0x0016CE0D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Account != null)
			{
				this.Account.Accept(visitor);
			}
			if (this.Password != null)
			{
				this.Password.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E49 RID: 7753
		private Literal _account;

		// Token: 0x04001E4A RID: 7754
		private Literal _password;

		// Token: 0x04001E4B RID: 7755
		private AlterServiceMasterKeyOption _kind;
	}
}
