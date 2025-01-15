using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200028D RID: 653
	[Serializable]
	internal class DropClusteredConstraintStateOption : DropClusteredConstraintOption
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x00164F57 File Offset: 0x00163157
		// (set) Token: 0x0600275A RID: 10074 RVA: 0x00164F5F File Offset: 0x0016315F
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

		// Token: 0x0600275B RID: 10075 RVA: 0x00164F68 File Offset: 0x00163168
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x00164F74 File Offset: 0x00163174
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B91 RID: 7057
		private OptionState _optionState;
	}
}
