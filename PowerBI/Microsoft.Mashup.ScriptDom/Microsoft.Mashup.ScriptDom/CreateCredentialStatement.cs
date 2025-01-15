using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000376 RID: 886
	[Serializable]
	internal class CreateCredentialStatement : CredentialStatement
	{
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06002CFD RID: 11517 RVA: 0x0016AC6C File Offset: 0x00168E6C
		// (set) Token: 0x06002CFE RID: 11518 RVA: 0x0016AC74 File Offset: 0x00168E74
		public Identifier CryptographicProviderName
		{
			get
			{
				return this._cryptographicProviderName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._cryptographicProviderName = value;
			}
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x0016AC84 File Offset: 0x00168E84
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x0016AC90 File Offset: 0x00168E90
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.CryptographicProviderName != null)
			{
				this.CryptographicProviderName.Accept(visitor);
			}
		}

		// Token: 0x04001D33 RID: 7475
		private Identifier _cryptographicProviderName;
	}
}
