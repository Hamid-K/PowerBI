using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002E0 RID: 736
	[Serializable]
	internal class CloseSymmetricKeyStatement : TSqlStatement
	{
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x00167031 File Offset: 0x00165231
		// (set) Token: 0x06002948 RID: 10568 RVA: 0x00167039 File Offset: 0x00165239
		public Identifier Name
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

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06002949 RID: 10569 RVA: 0x00167049 File Offset: 0x00165249
		// (set) Token: 0x0600294A RID: 10570 RVA: 0x00167051 File Offset: 0x00165251
		public bool All
		{
			get
			{
				return this._all;
			}
			set
			{
				this._all = value;
			}
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x0016705A File Offset: 0x0016525A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x00167066 File Offset: 0x00165266
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C1D RID: 7197
		private Identifier _name;

		// Token: 0x04001C1E RID: 7198
		private bool _all;
	}
}
