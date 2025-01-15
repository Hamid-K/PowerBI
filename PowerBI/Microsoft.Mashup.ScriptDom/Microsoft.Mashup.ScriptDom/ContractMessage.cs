using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000374 RID: 884
	[Serializable]
	internal class ContractMessage : TSqlFragment
	{
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06002CEE RID: 11502 RVA: 0x0016AB71 File Offset: 0x00168D71
		// (set) Token: 0x06002CEF RID: 11503 RVA: 0x0016AB79 File Offset: 0x00168D79
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

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x0016AB89 File Offset: 0x00168D89
		// (set) Token: 0x06002CF1 RID: 11505 RVA: 0x0016AB91 File Offset: 0x00168D91
		public MessageSender SentBy
		{
			get
			{
				return this._sentBy;
			}
			set
			{
				this._sentBy = value;
			}
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x0016AB9A File Offset: 0x00168D9A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x0016ABA6 File Offset: 0x00168DA6
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D2E RID: 7470
		private Identifier _name;

		// Token: 0x04001D2F RID: 7471
		private MessageSender _sentBy;
	}
}
