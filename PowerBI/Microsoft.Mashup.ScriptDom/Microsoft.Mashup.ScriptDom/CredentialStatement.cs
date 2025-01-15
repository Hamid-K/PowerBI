using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000375 RID: 885
	[Serializable]
	internal abstract class CredentialStatement : TSqlStatement
	{
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x0016ABCB File Offset: 0x00168DCB
		// (set) Token: 0x06002CF6 RID: 11510 RVA: 0x0016ABD3 File Offset: 0x00168DD3
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

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06002CF7 RID: 11511 RVA: 0x0016ABE3 File Offset: 0x00168DE3
		// (set) Token: 0x06002CF8 RID: 11512 RVA: 0x0016ABEB File Offset: 0x00168DEB
		public Literal Identity
		{
			get
			{
				return this._identity;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._identity = value;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06002CF9 RID: 11513 RVA: 0x0016ABFB File Offset: 0x00168DFB
		// (set) Token: 0x06002CFA RID: 11514 RVA: 0x0016AC03 File Offset: 0x00168E03
		public Literal Secret
		{
			get
			{
				return this._secret;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._secret = value;
			}
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x0016AC14 File Offset: 0x00168E14
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.Identity != null)
			{
				this.Identity.Accept(visitor);
			}
			if (this.Secret != null)
			{
				this.Secret.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D30 RID: 7472
		private Identifier _name;

		// Token: 0x04001D31 RID: 7473
		private Literal _identity;

		// Token: 0x04001D32 RID: 7474
		private Literal _secret;
	}
}
