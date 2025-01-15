using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200039D RID: 925
	[Serializable]
	internal abstract class FullTextCatalogOption : TSqlFragment
	{
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x0016BB1D File Offset: 0x00169D1D
		// (set) Token: 0x06002DF0 RID: 11760 RVA: 0x0016BB25 File Offset: 0x00169D25
		public FullTextCatalogOptionKind OptionKind
		{
			get
			{
				return this._optionKind;
			}
			set
			{
				this._optionKind = value;
			}
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x0016BB2E File Offset: 0x00169D2E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D78 RID: 7544
		private FullTextCatalogOptionKind _optionKind;
	}
}
