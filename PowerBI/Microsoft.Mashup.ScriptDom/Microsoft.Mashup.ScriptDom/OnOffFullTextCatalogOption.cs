using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200039E RID: 926
	[Serializable]
	internal class OnOffFullTextCatalogOption : FullTextCatalogOption
	{
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x0016BB3F File Offset: 0x00169D3F
		// (set) Token: 0x06002DF4 RID: 11764 RVA: 0x0016BB47 File Offset: 0x00169D47
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

		// Token: 0x06002DF5 RID: 11765 RVA: 0x0016BB50 File Offset: 0x00169D50
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x0016BB5C File Offset: 0x00169D5C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D79 RID: 7545
		private OptionState _optionState;
	}
}
