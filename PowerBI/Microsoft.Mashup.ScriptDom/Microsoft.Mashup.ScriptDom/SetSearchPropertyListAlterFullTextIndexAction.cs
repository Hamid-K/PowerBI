using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003ED RID: 1005
	[Serializable]
	internal class SetSearchPropertyListAlterFullTextIndexAction : AlterFullTextIndexAction
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06002FC7 RID: 12231 RVA: 0x0016DA16 File Offset: 0x0016BC16
		// (set) Token: 0x06002FC8 RID: 12232 RVA: 0x0016DA1E File Offset: 0x0016BC1E
		public SearchPropertyListFullTextIndexOption SearchPropertyListOption
		{
			get
			{
				return this._searchPropertyListOption;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._searchPropertyListOption = value;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x0016DA2E File Offset: 0x0016BC2E
		// (set) Token: 0x06002FCA RID: 12234 RVA: 0x0016DA36 File Offset: 0x0016BC36
		public bool WithNoPopulation
		{
			get
			{
				return this._withNoPopulation;
			}
			set
			{
				this._withNoPopulation = value;
			}
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x0016DA3F File Offset: 0x0016BC3F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x0016DA4B File Offset: 0x0016BC4B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SearchPropertyListOption != null)
			{
				this.SearchPropertyListOption.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DFA RID: 7674
		private SearchPropertyListFullTextIndexOption _searchPropertyListOption;

		// Token: 0x04001DFB RID: 7675
		private bool _withNoPopulation;
	}
}
