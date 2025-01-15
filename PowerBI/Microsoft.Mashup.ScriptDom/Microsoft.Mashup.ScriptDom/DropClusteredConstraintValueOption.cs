using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200028E RID: 654
	[Serializable]
	internal class DropClusteredConstraintValueOption : DropClusteredConstraintOption
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x00164F85 File Offset: 0x00163185
		// (set) Token: 0x0600275F RID: 10079 RVA: 0x00164F8D File Offset: 0x0016318D
		public Literal OptionValue
		{
			get
			{
				return this._optionValue;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._optionValue = value;
			}
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x00164F9D File Offset: 0x0016319D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x00164FA9 File Offset: 0x001631A9
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.OptionValue != null)
			{
				this.OptionValue.Accept(visitor);
			}
		}

		// Token: 0x04001B92 RID: 7058
		private Literal _optionValue;
	}
}
