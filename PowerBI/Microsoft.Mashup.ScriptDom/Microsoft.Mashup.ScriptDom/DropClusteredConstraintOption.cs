using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200028C RID: 652
	[Serializable]
	internal abstract class DropClusteredConstraintOption : TSqlFragment
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x00164F35 File Offset: 0x00163135
		// (set) Token: 0x06002756 RID: 10070 RVA: 0x00164F3D File Offset: 0x0016313D
		public DropClusteredConstraintOptionKind OptionKind
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

		// Token: 0x06002757 RID: 10071 RVA: 0x00164F46 File Offset: 0x00163146
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B90 RID: 7056
		private DropClusteredConstraintOptionKind _optionKind;
	}
}
