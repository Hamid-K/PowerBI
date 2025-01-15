using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200029E RID: 670
	[Serializable]
	internal class QueueExecuteAsOption : QueueOption
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060027BF RID: 10175 RVA: 0x001655BC File Offset: 0x001637BC
		// (set) Token: 0x060027C0 RID: 10176 RVA: 0x001655C4 File Offset: 0x001637C4
		public ExecuteAsClause OptionValue
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

		// Token: 0x060027C1 RID: 10177 RVA: 0x001655D4 File Offset: 0x001637D4
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x001655E0 File Offset: 0x001637E0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.OptionValue != null)
			{
				this.OptionValue.Accept(visitor);
			}
		}

		// Token: 0x04001BAD RID: 7085
		private ExecuteAsClause _optionValue;
	}
}
