using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003FB RID: 1019
	[Serializable]
	internal class OnOffPrincipalOption : PrincipalOption
	{
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600301E RID: 12318 RVA: 0x0016DF97 File Offset: 0x0016C197
		// (set) Token: 0x0600301F RID: 12319 RVA: 0x0016DF9F File Offset: 0x0016C19F
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

		// Token: 0x06003020 RID: 12320 RVA: 0x0016DFA8 File Offset: 0x0016C1A8
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x0016DFB4 File Offset: 0x0016C1B4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E13 RID: 7699
		private OptionState _optionState;
	}
}
