using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020004A4 RID: 1188
	[Serializable]
	internal class WindowDelimiter : TSqlFragment
	{
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x001719FC File Offset: 0x0016FBFC
		// (set) Token: 0x060033E1 RID: 13281 RVA: 0x00171A04 File Offset: 0x0016FC04
		public WindowDelimiterType WindowDelimiterType
		{
			get
			{
				return this._windowDelimiterType;
			}
			set
			{
				this._windowDelimiterType = value;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060033E2 RID: 13282 RVA: 0x00171A0D File Offset: 0x0016FC0D
		// (set) Token: 0x060033E3 RID: 13283 RVA: 0x00171A15 File Offset: 0x0016FC15
		public ScalarExpression OffsetValue
		{
			get
			{
				return this._offsetValue;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._offsetValue = value;
			}
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x00171A25 File Offset: 0x0016FC25
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x00171A31 File Offset: 0x0016FC31
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.OffsetValue != null)
			{
				this.OffsetValue.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001F15 RID: 7957
		private WindowDelimiterType _windowDelimiterType;

		// Token: 0x04001F16 RID: 7958
		private ScalarExpression _offsetValue;
	}
}
