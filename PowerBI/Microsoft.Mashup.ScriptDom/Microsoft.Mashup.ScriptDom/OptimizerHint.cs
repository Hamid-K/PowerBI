using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001F6 RID: 502
	[Serializable]
	internal class OptimizerHint : TSqlFragment
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x00160DBD File Offset: 0x0015EFBD
		// (set) Token: 0x060023B6 RID: 9142 RVA: 0x00160DC5 File Offset: 0x0015EFC5
		public OptimizerHintKind HintKind
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

		// Token: 0x060023B7 RID: 9143 RVA: 0x00160DCE File Offset: 0x0015EFCE
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x00160DDA File Offset: 0x0015EFDA
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A84 RID: 6788
		private OptimizerHintKind _hintKind;
	}
}
