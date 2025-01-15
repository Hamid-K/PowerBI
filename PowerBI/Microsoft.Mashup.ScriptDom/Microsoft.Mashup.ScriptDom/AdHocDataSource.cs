using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001B4 RID: 436
	[Serializable]
	internal class AdHocDataSource : TSqlFragment
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06002240 RID: 8768 RVA: 0x0015F301 File Offset: 0x0015D501
		// (set) Token: 0x06002241 RID: 8769 RVA: 0x0015F309 File Offset: 0x0015D509
		public StringLiteral ProviderName
		{
			get
			{
				return this._providerName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._providerName = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x0015F319 File Offset: 0x0015D519
		// (set) Token: 0x06002243 RID: 8771 RVA: 0x0015F321 File Offset: 0x0015D521
		public StringLiteral InitString
		{
			get
			{
				return this._initString;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._initString = value;
			}
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x0015F331 File Offset: 0x0015D531
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x0015F33D File Offset: 0x0015D53D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ProviderName != null)
			{
				this.ProviderName.Accept(visitor);
			}
			if (this.InitString != null)
			{
				this.InitString.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A19 RID: 6681
		private StringLiteral _providerName;

		// Token: 0x04001A1A RID: 6682
		private StringLiteral _initString;
	}
}
