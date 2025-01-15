using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002AD RID: 685
	[Serializable]
	internal class IndexStateOption : IndexOption
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06002822 RID: 10274 RVA: 0x00165DDE File Offset: 0x00163FDE
		// (set) Token: 0x06002823 RID: 10275 RVA: 0x00165DE6 File Offset: 0x00163FE6
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

		// Token: 0x06002824 RID: 10276 RVA: 0x00165DEF File Offset: 0x00163FEF
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x00165DFB File Offset: 0x00163FFB
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BCD RID: 7117
		private OptionState _optionState;
	}
}
