using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000302 RID: 770
	[Serializable]
	internal class SetUserStatement : TSqlStatement
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060029F5 RID: 10741 RVA: 0x00167A9F File Offset: 0x00165C9F
		// (set) Token: 0x060029F6 RID: 10742 RVA: 0x00167AA7 File Offset: 0x00165CA7
		public ValueExpression UserName
		{
			get
			{
				return this._userName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._userName = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060029F7 RID: 10743 RVA: 0x00167AB7 File Offset: 0x00165CB7
		// (set) Token: 0x060029F8 RID: 10744 RVA: 0x00167ABF File Offset: 0x00165CBF
		public bool WithNoReset
		{
			get
			{
				return this._withNoReset;
			}
			set
			{
				this._withNoReset = value;
			}
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x00167AC8 File Offset: 0x00165CC8
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x00167AD4 File Offset: 0x00165CD4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.UserName != null)
			{
				this.UserName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C45 RID: 7237
		private ValueExpression _userName;

		// Token: 0x04001C46 RID: 7238
		private bool _withNoReset;
	}
}
