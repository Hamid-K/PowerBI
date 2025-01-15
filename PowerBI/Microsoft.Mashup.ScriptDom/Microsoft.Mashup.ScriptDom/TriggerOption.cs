using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001BA RID: 442
	[Serializable]
	internal class TriggerOption : TSqlFragment
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x0015F53A File Offset: 0x0015D73A
		// (set) Token: 0x06002264 RID: 8804 RVA: 0x0015F542 File Offset: 0x0015D742
		public TriggerOptionKind OptionKind
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

		// Token: 0x06002265 RID: 8805 RVA: 0x0015F54B File Offset: 0x0015D74B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x0015F557 File Offset: 0x0015D757
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A23 RID: 6691
		private TriggerOptionKind _optionKind;
	}
}
