using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000399 RID: 921
	[Serializable]
	internal class ProviderKeyNameKeyOption : KeyOption
	{
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06002DDB RID: 11739 RVA: 0x0016B9AE File Offset: 0x00169BAE
		// (set) Token: 0x06002DDC RID: 11740 RVA: 0x0016B9B6 File Offset: 0x00169BB6
		public Literal KeyName
		{
			get
			{
				return this._keyName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._keyName = value;
			}
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x0016B9C6 File Offset: 0x00169BC6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x0016B9D2 File Offset: 0x00169BD2
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.KeyName != null)
			{
				this.KeyName.Accept(visitor);
			}
		}

		// Token: 0x04001D73 RID: 7539
		private Literal _keyName;
	}
}
