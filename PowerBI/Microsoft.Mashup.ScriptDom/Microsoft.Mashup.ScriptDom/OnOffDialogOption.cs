using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000420 RID: 1056
	[Serializable]
	internal class OnOffDialogOption : DialogOption
	{
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x0016EE78 File Offset: 0x0016D078
		// (set) Token: 0x0600310B RID: 12555 RVA: 0x0016EE80 File Offset: 0x0016D080
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

		// Token: 0x0600310C RID: 12556 RVA: 0x0016EE89 File Offset: 0x0016D089
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x0016EE95 File Offset: 0x0016D095
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E57 RID: 7767
		private OptionState _optionState;
	}
}
