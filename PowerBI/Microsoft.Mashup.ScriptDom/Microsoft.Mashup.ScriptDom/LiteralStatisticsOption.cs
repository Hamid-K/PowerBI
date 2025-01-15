using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002D2 RID: 722
	[Serializable]
	internal class LiteralStatisticsOption : StatisticsOption
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060028E6 RID: 10470 RVA: 0x0016695E File Offset: 0x00164B5E
		// (set) Token: 0x060028E7 RID: 10471 RVA: 0x00166966 File Offset: 0x00164B66
		public Literal Literal
		{
			get
			{
				return this._literal;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._literal = value;
			}
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x00166976 File Offset: 0x00164B76
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x00166982 File Offset: 0x00164B82
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Literal != null)
			{
				this.Literal.Accept(visitor);
			}
		}

		// Token: 0x04001BFE RID: 7166
		private Literal _literal;
	}
}
