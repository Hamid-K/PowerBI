using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000240 RID: 576
	[Serializable]
	internal class WaitForStatement : TSqlStatement
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x00163068 File Offset: 0x00161268
		// (set) Token: 0x0600258F RID: 9615 RVA: 0x00163070 File Offset: 0x00161270
		public WaitForOption WaitForOption
		{
			get
			{
				return this._waitForOption;
			}
			set
			{
				this._waitForOption = value;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x00163079 File Offset: 0x00161279
		// (set) Token: 0x06002591 RID: 9617 RVA: 0x00163081 File Offset: 0x00161281
		public ValueExpression Parameter
		{
			get
			{
				return this._parameter;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._parameter = value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06002592 RID: 9618 RVA: 0x00163091 File Offset: 0x00161291
		// (set) Token: 0x06002593 RID: 9619 RVA: 0x00163099 File Offset: 0x00161299
		public ScalarExpression Timeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._timeout = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06002594 RID: 9620 RVA: 0x001630A9 File Offset: 0x001612A9
		// (set) Token: 0x06002595 RID: 9621 RVA: 0x001630B1 File Offset: 0x001612B1
		public WaitForSupportedStatement Statement
		{
			get
			{
				return this._statement;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._statement = value;
			}
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x001630C1 File Offset: 0x001612C1
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x001630D0 File Offset: 0x001612D0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Parameter != null)
			{
				this.Parameter.Accept(visitor);
			}
			if (this.Timeout != null)
			{
				this.Timeout.Accept(visitor);
			}
			if (this.Statement != null)
			{
				this.Statement.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B0F RID: 6927
		private WaitForOption _waitForOption;

		// Token: 0x04001B10 RID: 6928
		private ValueExpression _parameter;

		// Token: 0x04001B11 RID: 6929
		private ScalarExpression _timeout;

		// Token: 0x04001B12 RID: 6930
		private WaitForSupportedStatement _statement;
	}
}
