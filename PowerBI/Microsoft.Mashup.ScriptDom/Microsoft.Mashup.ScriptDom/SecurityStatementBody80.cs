using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200024F RID: 591
	[Serializable]
	internal abstract class SecurityStatementBody80 : TSqlStatement
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x001639C0 File Offset: 0x00161BC0
		// (set) Token: 0x06002603 RID: 9731 RVA: 0x001639C8 File Offset: 0x00161BC8
		public SecurityElement80 SecurityElement80
		{
			get
			{
				return this._securityElement80;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._securityElement80 = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06002604 RID: 9732 RVA: 0x001639D8 File Offset: 0x00161BD8
		// (set) Token: 0x06002605 RID: 9733 RVA: 0x001639E0 File Offset: 0x00161BE0
		public SecurityUserClause80 SecurityUserClause80
		{
			get
			{
				return this._securityUserClause80;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._securityUserClause80 = value;
			}
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x001639F0 File Offset: 0x00161BF0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SecurityElement80 != null)
			{
				this.SecurityElement80.Accept(visitor);
			}
			if (this.SecurityUserClause80 != null)
			{
				this.SecurityUserClause80.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B36 RID: 6966
		private SecurityElement80 _securityElement80;

		// Token: 0x04001B37 RID: 6967
		private SecurityUserClause80 _securityUserClause80;
	}
}
