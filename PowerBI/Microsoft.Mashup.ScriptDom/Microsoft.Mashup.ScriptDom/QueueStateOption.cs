using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200029B RID: 667
	[Serializable]
	internal class QueueStateOption : QueueOption
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060027B0 RID: 10160 RVA: 0x001654FC File Offset: 0x001636FC
		// (set) Token: 0x060027B1 RID: 10161 RVA: 0x00165504 File Offset: 0x00163704
		public OptionState OptionState
		{
			get
			{
				return this._optionState;
			}
			set
			{
				this._optionState = value;
			}
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x0016550D File Offset: 0x0016370D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x00165519 File Offset: 0x00163719
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BAA RID: 7082
		private OptionState _optionState;
	}
}
