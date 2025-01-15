using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200026E RID: 622
	[Serializable]
	internal class ScalarExpressionSequenceOption : SequenceOption
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060026B1 RID: 9905 RVA: 0x00164451 File Offset: 0x00162651
		// (set) Token: 0x060026B2 RID: 9906 RVA: 0x00164459 File Offset: 0x00162659
		public ScalarExpression OptionValue
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

		// Token: 0x060026B3 RID: 9907 RVA: 0x00164469 File Offset: 0x00162669
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x00164475 File Offset: 0x00162675
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.OptionValue != null)
			{
				this.OptionValue.Accept(visitor);
			}
		}

		// Token: 0x04001B65 RID: 7013
		private ScalarExpression _optionValue;
	}
}
