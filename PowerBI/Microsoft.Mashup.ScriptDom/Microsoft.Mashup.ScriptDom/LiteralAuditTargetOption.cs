using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000452 RID: 1106
	[Serializable]
	internal class LiteralAuditTargetOption : AuditTargetOption
	{
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06003205 RID: 12805 RVA: 0x0016FC68 File Offset: 0x0016DE68
		// (set) Token: 0x06003206 RID: 12806 RVA: 0x0016FC70 File Offset: 0x0016DE70
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

		// Token: 0x06003207 RID: 12807 RVA: 0x0016FC80 File Offset: 0x0016DE80
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x0016FC8C File Offset: 0x0016DE8C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001E92 RID: 7826
		private Literal _value;
	}
}
