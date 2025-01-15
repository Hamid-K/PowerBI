using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000396 RID: 918
	[Serializable]
	internal class KeySourceKeyOption : KeyOption
	{
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x0016B8EE File Offset: 0x00169AEE
		// (set) Token: 0x06002DCD RID: 11725 RVA: 0x0016B8F6 File Offset: 0x00169AF6
		public Literal PassPhrase
		{
			get
			{
				return this._passPhrase;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._passPhrase = value;
			}
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x0016B906 File Offset: 0x00169B06
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x0016B912 File Offset: 0x00169B12
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.PassPhrase != null)
			{
				this.PassPhrase.Accept(visitor);
			}
		}

		// Token: 0x04001D70 RID: 7536
		private Literal _passPhrase;
	}
}
