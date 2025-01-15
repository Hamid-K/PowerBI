using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001F7 RID: 503
	[Serializable]
	internal class LiteralOptimizerHint : OptimizerHint
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x00160DEB File Offset: 0x0015EFEB
		// (set) Token: 0x060023BB RID: 9147 RVA: 0x00160DF3 File Offset: 0x0015EFF3
		public Literal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x00160E03 File Offset: 0x0015F003
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x00160E0F File Offset: 0x0015F00F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001A85 RID: 6789
		private Literal _value;
	}
}
