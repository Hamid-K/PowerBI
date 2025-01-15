using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000416 RID: 1046
	[Serializable]
	internal class GetConversationGroupStatement : WaitForSupportedStatement
	{
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x0016E7F6 File Offset: 0x0016C9F6
		// (set) Token: 0x060030AF RID: 12463 RVA: 0x0016E7FE File Offset: 0x0016C9FE
		public VariableReference GroupId
		{
			get
			{
				return this._groupId;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._groupId = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x0016E80E File Offset: 0x0016CA0E
		// (set) Token: 0x060030B1 RID: 12465 RVA: 0x0016E816 File Offset: 0x0016CA16
		public SchemaObjectName Queue
		{
			get
			{
				return this._queue;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._queue = value;
			}
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x0016E826 File Offset: 0x0016CA26
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x0016E832 File Offset: 0x0016CA32
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.GroupId != null)
			{
				this.GroupId.Accept(visitor);
			}
			if (this.Queue != null)
			{
				this.Queue.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E36 RID: 7734
		private VariableReference _groupId;

		// Token: 0x04001E37 RID: 7735
		private SchemaObjectName _queue;
	}
}
