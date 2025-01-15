using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000489 RID: 1161
	[Serializable]
	internal class CellsPerObjectSpatialIndexOption : SpatialIndexOption
	{
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x00170F98 File Offset: 0x0016F198
		// (set) Token: 0x0600333E RID: 13118 RVA: 0x00170FA0 File Offset: 0x0016F1A0
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

		// Token: 0x0600333F RID: 13119 RVA: 0x00170FB0 File Offset: 0x0016F1B0
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x00170FBC File Offset: 0x0016F1BC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EE6 RID: 7910
		private Literal _value;
	}
}
