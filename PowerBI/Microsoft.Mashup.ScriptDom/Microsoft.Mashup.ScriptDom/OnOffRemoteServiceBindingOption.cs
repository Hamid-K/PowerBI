using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000367 RID: 871
	[Serializable]
	internal class OnOffRemoteServiceBindingOption : RemoteServiceBindingOption
	{
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x0016A587 File Offset: 0x00168787
		// (set) Token: 0x06002C9D RID: 11421 RVA: 0x0016A58F File Offset: 0x0016878F
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

		// Token: 0x06002C9E RID: 11422 RVA: 0x0016A598 File Offset: 0x00168798
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x0016A5A4 File Offset: 0x001687A4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D16 RID: 7446
		private OptionState _optionState;
	}
}
