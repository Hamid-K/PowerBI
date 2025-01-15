using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000381 RID: 897
	[Serializable]
	internal class LiteralEndpointProtocolOption : EndpointProtocolOption
	{
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06002D40 RID: 11584 RVA: 0x0016B142 File Offset: 0x00169342
		// (set) Token: 0x06002D41 RID: 11585 RVA: 0x0016B14A File Offset: 0x0016934A
		public Literal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x0016B15A File Offset: 0x0016935A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x0016B166 File Offset: 0x00169366
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001D47 RID: 7495
		private Literal _value;
	}
}
