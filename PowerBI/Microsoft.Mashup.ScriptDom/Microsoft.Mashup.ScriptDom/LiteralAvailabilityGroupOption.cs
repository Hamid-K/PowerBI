using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000497 RID: 1175
	[Serializable]
	internal class LiteralAvailabilityGroupOption : AvailabilityGroupOption
	{
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06003383 RID: 13187 RVA: 0x001713CE File Offset: 0x0016F5CE
		// (set) Token: 0x06003384 RID: 13188 RVA: 0x001713D6 File Offset: 0x0016F5D6
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

		// Token: 0x06003385 RID: 13189 RVA: 0x001713E6 File Offset: 0x0016F5E6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x001713F2 File Offset: 0x0016F5F2
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001EF8 RID: 7928
		private Literal _value;
	}
}
