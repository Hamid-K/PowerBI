using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002FF RID: 767
	[Serializable]
	internal class CheckpointStatement : TSqlStatement
	{
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060029E6 RID: 10726 RVA: 0x001679FA File Offset: 0x00165BFA
		// (set) Token: 0x060029E7 RID: 10727 RVA: 0x00167A02 File Offset: 0x00165C02
		public Literal Duration
		{
			get
			{
				return this._duration;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._duration = value;
			}
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x00167A12 File Offset: 0x00165C12
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x00167A1E File Offset: 0x00165C1E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Duration != null)
			{
				this.Duration.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C42 RID: 7234
		private Literal _duration;
	}
}
