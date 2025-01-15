using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000400 RID: 1024
	[Serializable]
	internal class AsymmetricKeyCreateLoginSource : CreateLoginSource
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06003038 RID: 12344 RVA: 0x0016E132 File Offset: 0x0016C332
		// (set) Token: 0x06003039 RID: 12345 RVA: 0x0016E13A File Offset: 0x0016C33A
		public Identifier Key
		{
			get
			{
				return this._key;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._key = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600303A RID: 12346 RVA: 0x0016E14A File Offset: 0x0016C34A
		// (set) Token: 0x0600303B RID: 12347 RVA: 0x0016E152 File Offset: 0x0016C352
		public Identifier Credential
		{
			get
			{
				return this._credential;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._credential = value;
			}
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x0016E162 File Offset: 0x0016C362
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x0016E16E File Offset: 0x0016C36E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Key != null)
			{
				this.Key.Accept(visitor);
			}
			if (this.Credential != null)
			{
				this.Credential.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E19 RID: 7705
		private Identifier _key;

		// Token: 0x04001E1A RID: 7706
		private Identifier _credential;
	}
}
