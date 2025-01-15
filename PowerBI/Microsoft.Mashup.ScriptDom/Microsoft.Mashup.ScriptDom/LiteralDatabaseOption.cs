using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000332 RID: 818
	[Serializable]
	internal class LiteralDatabaseOption : DatabaseOption
	{
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06002B1C RID: 11036 RVA: 0x00168AF0 File Offset: 0x00166CF0
		// (set) Token: 0x06002B1D RID: 11037 RVA: 0x00168AF8 File Offset: 0x00166CF8
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

		// Token: 0x06002B1E RID: 11038 RVA: 0x00168B08 File Offset: 0x00166D08
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x00168B14 File Offset: 0x00166D14
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001C96 RID: 7318
		private Literal _value;
	}
}
