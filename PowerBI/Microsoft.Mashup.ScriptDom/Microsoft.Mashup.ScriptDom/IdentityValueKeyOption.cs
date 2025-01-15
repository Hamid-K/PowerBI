using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000398 RID: 920
	[Serializable]
	internal class IdentityValueKeyOption : KeyOption
	{
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06002DD6 RID: 11734 RVA: 0x0016B965 File Offset: 0x00169B65
		// (set) Token: 0x06002DD7 RID: 11735 RVA: 0x0016B96D File Offset: 0x00169B6D
		public Literal IdentityPhrase
		{
			get
			{
				return this._identityPhrase;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._identityPhrase = value;
			}
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x0016B97D File Offset: 0x00169B7D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x0016B989 File Offset: 0x00169B89
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.IdentityPhrase != null)
			{
				this.IdentityPhrase.Accept(visitor);
			}
		}

		// Token: 0x04001D72 RID: 7538
		private Literal _identityPhrase;
	}
}
