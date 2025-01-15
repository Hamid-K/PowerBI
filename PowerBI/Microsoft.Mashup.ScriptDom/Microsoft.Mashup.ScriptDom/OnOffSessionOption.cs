using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200047E RID: 1150
	[Serializable]
	internal class OnOffSessionOption : SessionOption
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060032FC RID: 13052 RVA: 0x00170B58 File Offset: 0x0016ED58
		// (set) Token: 0x060032FD RID: 13053 RVA: 0x00170B60 File Offset: 0x0016ED60
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

		// Token: 0x060032FE RID: 13054 RVA: 0x00170B69 File Offset: 0x0016ED69
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x00170B75 File Offset: 0x0016ED75
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001ED3 RID: 7891
		private OptionState _optionState;
	}
}
