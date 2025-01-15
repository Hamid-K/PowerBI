using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000414 RID: 1044
	[Serializable]
	internal class MoveConversationStatement : TSqlStatement
	{
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060030A5 RID: 12453 RVA: 0x0016E770 File Offset: 0x0016C970
		// (set) Token: 0x060030A6 RID: 12454 RVA: 0x0016E778 File Offset: 0x0016C978
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

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x0016E788 File Offset: 0x0016C988
		// (set) Token: 0x060030A8 RID: 12456 RVA: 0x0016E790 File Offset: 0x0016C990
		public ScalarExpression Group
		{
			get
			{
				return this._group;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._group = value;
			}
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x0016E7A0 File Offset: 0x0016C9A0
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x0016E7AC File Offset: 0x0016C9AC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Conversation != null)
			{
				this.Conversation.Accept(visitor);
			}
			if (this.Group != null)
			{
				this.Group.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E34 RID: 7732
		private ScalarExpression _conversation;

		// Token: 0x04001E35 RID: 7733
		private ScalarExpression _group;
	}
}
