using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002FA RID: 762
	[Serializable]
	internal class ThrowStatement : TSqlStatement
	{
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060029C5 RID: 10693 RVA: 0x00167805 File Offset: 0x00165A05
		// (set) Token: 0x060029C6 RID: 10694 RVA: 0x0016780D File Offset: 0x00165A0D
		public ValueExpression ErrorNumber
		{
			get
			{
				return this._errorNumber;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._errorNumber = value;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060029C7 RID: 10695 RVA: 0x0016781D File Offset: 0x00165A1D
		// (set) Token: 0x060029C8 RID: 10696 RVA: 0x00167825 File Offset: 0x00165A25
		public ValueExpression Message
		{
			get
			{
				return this._message;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._message = value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060029C9 RID: 10697 RVA: 0x00167835 File Offset: 0x00165A35
		// (set) Token: 0x060029CA RID: 10698 RVA: 0x0016783D File Offset: 0x00165A3D
		public ValueExpression State
		{
			get
			{
				return this._state;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._state = value;
			}
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x0016784D File Offset: 0x00165A4D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x0016785C File Offset: 0x00165A5C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ErrorNumber != null)
			{
				this.ErrorNumber.Accept(visitor);
			}
			if (this.Message != null)
			{
				this.Message.Accept(visitor);
			}
			if (this.State != null)
			{
				this.State.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C39 RID: 7225
		private ValueExpression _errorNumber;

		// Token: 0x04001C3A RID: 7226
		private ValueExpression _message;

		// Token: 0x04001C3B RID: 7227
		private ValueExpression _state;
	}
}
