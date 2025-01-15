using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200047C RID: 1148
	[Serializable]
	internal class LiteralSessionOption : SessionOption
	{
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060032EE RID: 13038 RVA: 0x00170ABF File Offset: 0x0016ECBF
		// (set) Token: 0x060032EF RID: 13039 RVA: 0x00170AC7 File Offset: 0x0016ECC7
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

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060032F0 RID: 13040 RVA: 0x00170AD7 File Offset: 0x0016ECD7
		// (set) Token: 0x060032F1 RID: 13041 RVA: 0x00170ADF File Offset: 0x0016ECDF
		public MemoryUnit Unit
		{
			get
			{
				return this._unit;
			}
			set
			{
				this._unit = value;
			}
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x00170AE8 File Offset: 0x0016ECE8
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x00170AF4 File Offset: 0x0016ECF4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001ECF RID: 7887
		private Literal _value;

		// Token: 0x04001ED0 RID: 7888
		private MemoryUnit _unit;
	}
}
