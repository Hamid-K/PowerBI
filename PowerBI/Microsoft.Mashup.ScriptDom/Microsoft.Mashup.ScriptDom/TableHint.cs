using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001E2 RID: 482
	[Serializable]
	internal class TableHint : TSqlFragment
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x00160579 File Offset: 0x0015E779
		// (set) Token: 0x0600233F RID: 9023 RVA: 0x00160581 File Offset: 0x0015E781
		public TableHintKind HintKind
		{
			get
			{
				return this._hintKind;
			}
			set
			{
				this._hintKind = value;
			}
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x0016058A File Offset: 0x0015E78A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x00160596 File Offset: 0x0015E796
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A61 RID: 6753
		private TableHintKind _hintKind;
	}
}
