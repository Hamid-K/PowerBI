using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000413 RID: 1043
	[Serializable]
	internal class EndConversationStatement : TSqlStatement
	{
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x0016E6B3 File Offset: 0x0016C8B3
		// (set) Token: 0x0600309B RID: 12443 RVA: 0x0016E6BB File Offset: 0x0016C8BB
		public ScalarExpression Conversation
		{
			get
			{
				return this._conversation;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._conversation = value;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x0016E6CB File Offset: 0x0016C8CB
		// (set) Token: 0x0600309D RID: 12445 RVA: 0x0016E6D3 File Offset: 0x0016C8D3
		public bool WithCleanup
		{
			get
			{
				return this._withCleanup;
			}
			set
			{
				this._withCleanup = value;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x0016E6DC File Offset: 0x0016C8DC
		// (set) Token: 0x0600309F RID: 12447 RVA: 0x0016E6E4 File Offset: 0x0016C8E4
		public ValueExpression ErrorCode
		{
			get
			{
				return this._errorCode;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._errorCode = value;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x0016E6F4 File Offset: 0x0016C8F4
		// (set) Token: 0x060030A1 RID: 12449 RVA: 0x0016E6FC File Offset: 0x0016C8FC
		public ValueExpression ErrorDescription
		{
			get
			{
				return this._errorDescription;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._errorDescription = value;
			}
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x0016E70C File Offset: 0x0016C90C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x0016E718 File Offset: 0x0016C918
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Conversation != null)
			{
				this.Conversation.Accept(visitor);
			}
			if (this.ErrorCode != null)
			{
				this.ErrorCode.Accept(visitor);
			}
			if (this.ErrorDescription != null)
			{
				this.ErrorDescription.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E30 RID: 7728
		private ScalarExpression _conversation;

		// Token: 0x04001E31 RID: 7729
		private bool _withCleanup;

		// Token: 0x04001E32 RID: 7730
		private ValueExpression _errorCode;

		// Token: 0x04001E33 RID: 7731
		private ValueExpression _errorDescription;
	}
}
