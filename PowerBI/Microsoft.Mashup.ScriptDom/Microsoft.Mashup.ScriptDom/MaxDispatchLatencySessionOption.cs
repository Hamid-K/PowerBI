using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200047D RID: 1149
	[Serializable]
	internal class MaxDispatchLatencySessionOption : SessionOption
	{
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060032F5 RID: 13045 RVA: 0x00170B19 File Offset: 0x0016ED19
		// (set) Token: 0x060032F6 RID: 13046 RVA: 0x00170B21 File Offset: 0x0016ED21
		public bool IsInfinite
		{
			get
			{
				return this._isInfinite;
			}
			set
			{
				this._isInfinite = value;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060032F7 RID: 13047 RVA: 0x00170B2A File Offset: 0x0016ED2A
		// (set) Token: 0x060032F8 RID: 13048 RVA: 0x00170B32 File Offset: 0x0016ED32
		public Literal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x00170B3B File Offset: 0x0016ED3B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x00170B47 File Offset: 0x0016ED47
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001ED1 RID: 7889
		private bool _isInfinite;

		// Token: 0x04001ED2 RID: 7890
		private Literal _value;
	}
}
