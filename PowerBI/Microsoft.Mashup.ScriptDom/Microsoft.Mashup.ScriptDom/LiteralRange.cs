using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000264 RID: 612
	[Serializable]
	internal class LiteralRange : TSqlFragment
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06002675 RID: 9845 RVA: 0x001640E4 File Offset: 0x001622E4
		// (set) Token: 0x06002676 RID: 9846 RVA: 0x001640EC File Offset: 0x001622EC
		public Literal From
		{
			get
			{
				return this._from;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._from = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x001640FC File Offset: 0x001622FC
		// (set) Token: 0x06002678 RID: 9848 RVA: 0x00164104 File Offset: 0x00162304
		public Literal To
		{
			get
			{
				return this._to;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._to = value;
			}
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x00164114 File Offset: 0x00162314
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x00164120 File Offset: 0x00162320
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.From != null)
			{
				this.From.Accept(visitor);
			}
			if (this.To != null)
			{
				this.To.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B55 RID: 6997
		private Literal _from;

		// Token: 0x04001B56 RID: 6998
		private Literal _to;
	}
}
