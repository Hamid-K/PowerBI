using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200035B RID: 859
	[Serializable]
	internal class LiteralBulkInsertOption : BulkInsertOption
	{
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x00169F53 File Offset: 0x00168153
		// (set) Token: 0x06002C41 RID: 11329 RVA: 0x00169F5B File Offset: 0x0016815B
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

		// Token: 0x06002C42 RID: 11330 RVA: 0x00169F6B File Offset: 0x0016816B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x00169F77 File Offset: 0x00168177
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001CF6 RID: 7414
		private Literal _value;
	}
}
