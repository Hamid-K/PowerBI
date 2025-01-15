using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200032F RID: 815
	[Serializable]
	internal class PartnerDatabaseOption : DatabaseOption
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06002B07 RID: 11015 RVA: 0x001689E2 File Offset: 0x00166BE2
		// (set) Token: 0x06002B08 RID: 11016 RVA: 0x001689EA File Offset: 0x00166BEA
		public Literal PartnerServer
		{
			get
			{
				return this._partnerServer;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._partnerServer = value;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06002B09 RID: 11017 RVA: 0x001689FA File Offset: 0x00166BFA
		// (set) Token: 0x06002B0A RID: 11018 RVA: 0x00168A02 File Offset: 0x00166C02
		public PartnerDatabaseOptionKind PartnerOption
		{
			get
			{
				return this._partnerOption;
			}
			set
			{
				this._partnerOption = value;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06002B0B RID: 11019 RVA: 0x00168A0B File Offset: 0x00166C0B
		// (set) Token: 0x06002B0C RID: 11020 RVA: 0x00168A13 File Offset: 0x00166C13
		public Literal Timeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._timeout = value;
			}
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x00168A23 File Offset: 0x00166C23
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x00168A2F File Offset: 0x00166C2F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.PartnerServer != null)
			{
				this.PartnerServer.Accept(visitor);
			}
			if (this.Timeout != null)
			{
				this.Timeout.Accept(visitor);
			}
		}

		// Token: 0x04001C90 RID: 7312
		private Literal _partnerServer;

		// Token: 0x04001C91 RID: 7313
		private PartnerDatabaseOptionKind _partnerOption;

		// Token: 0x04001C92 RID: 7314
		private Literal _timeout;
	}
}
