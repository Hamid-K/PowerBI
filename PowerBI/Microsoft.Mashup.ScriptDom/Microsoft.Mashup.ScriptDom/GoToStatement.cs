using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000231 RID: 561
	[Serializable]
	internal class GoToStatement : TSqlStatement
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06002534 RID: 9524 RVA: 0x00162A99 File Offset: 0x00160C99
		// (set) Token: 0x06002535 RID: 9525 RVA: 0x00162AA1 File Offset: 0x00160CA1
		public Identifier LabelName
		{
			get
			{
				return this._labelName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._labelName = value;
			}
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x00162AB1 File Offset: 0x00160CB1
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x00162ABD File Offset: 0x00160CBD
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.LabelName != null)
			{
				this.LabelName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AF6 RID: 6902
		private Identifier _labelName;
	}
}
