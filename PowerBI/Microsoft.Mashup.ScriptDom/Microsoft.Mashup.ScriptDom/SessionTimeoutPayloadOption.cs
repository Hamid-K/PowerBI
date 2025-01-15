using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200038D RID: 909
	[Serializable]
	internal class SessionTimeoutPayloadOption : PayloadOption
	{
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06002D93 RID: 11667 RVA: 0x0016B5AF File Offset: 0x001697AF
		// (set) Token: 0x06002D94 RID: 11668 RVA: 0x0016B5B7 File Offset: 0x001697B7
		public bool IsNever
		{
			get
			{
				return this._isNever;
			}
			set
			{
				this._isNever = value;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06002D95 RID: 11669 RVA: 0x0016B5C0 File Offset: 0x001697C0
		// (set) Token: 0x06002D96 RID: 11670 RVA: 0x0016B5C8 File Offset: 0x001697C8
		public Literal Timeout
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

		// Token: 0x06002D97 RID: 11671 RVA: 0x0016B5D8 File Offset: 0x001697D8
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x0016B5E4 File Offset: 0x001697E4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Timeout != null)
			{
				this.Timeout.Accept(visitor);
			}
		}

		// Token: 0x04001D5F RID: 7519
		private bool _isNever;

		// Token: 0x04001D60 RID: 7520
		private Literal _timeout;
	}
}
