using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200033D RID: 829
	[Serializable]
	internal class IdentityOptions : TSqlFragment
	{
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x00169025 File Offset: 0x00167225
		// (set) Token: 0x06002B6F RID: 11119 RVA: 0x0016902D File Offset: 0x0016722D
		public ScalarExpression IdentitySeed
		{
			get
			{
				return this._identitySeed;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._identitySeed = value;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06002B70 RID: 11120 RVA: 0x0016903D File Offset: 0x0016723D
		// (set) Token: 0x06002B71 RID: 11121 RVA: 0x00169045 File Offset: 0x00167245
		public ScalarExpression IdentityIncrement
		{
			get
			{
				return this._identityIncrement;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._identityIncrement = value;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06002B72 RID: 11122 RVA: 0x00169055 File Offset: 0x00167255
		// (set) Token: 0x06002B73 RID: 11123 RVA: 0x0016905D File Offset: 0x0016725D
		public bool IsIdentityNotForReplication
		{
			get
			{
				return this._isIdentityNotForReplication;
			}
			set
			{
				this._isIdentityNotForReplication = value;
			}
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x00169066 File Offset: 0x00167266
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x00169072 File Offset: 0x00167272
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.IdentitySeed != null)
			{
				this.IdentitySeed.Accept(visitor);
			}
			if (this.IdentityIncrement != null)
			{
				this.IdentityIncrement.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CB0 RID: 7344
		private ScalarExpression _identitySeed;

		// Token: 0x04001CB1 RID: 7345
		private ScalarExpression _identityIncrement;

		// Token: 0x04001CB2 RID: 7346
		private bool _isIdentityNotForReplication;
	}
}
