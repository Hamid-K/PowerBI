using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200029D RID: 669
	[Serializable]
	internal class QueueValueOption : QueueOption
	{
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060027BA RID: 10170 RVA: 0x00165573 File Offset: 0x00163773
		// (set) Token: 0x060027BB RID: 10171 RVA: 0x0016557B File Offset: 0x0016377B
		public ValueExpression OptionValue
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

		// Token: 0x060027BC RID: 10172 RVA: 0x0016558B File Offset: 0x0016378B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x00165597 File Offset: 0x00163797
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.OptionValue != null)
			{
				this.OptionValue.Accept(visitor);
			}
		}

		// Token: 0x04001BAC RID: 7084
		private ValueExpression _optionValue;
	}
}
