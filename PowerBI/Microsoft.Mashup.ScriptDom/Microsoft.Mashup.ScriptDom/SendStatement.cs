using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000418 RID: 1048
	[Serializable]
	internal class SendStatement : TSqlStatement
	{
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x0016E991 File Offset: 0x0016CB91
		public IList<ScalarExpression> ConversationHandles
		{
			get
			{
				return this._conversationHandles;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x0016E999 File Offset: 0x0016CB99
		// (set) Token: 0x060030C5 RID: 12485 RVA: 0x0016E9A1 File Offset: 0x0016CBA1
		public IdentifierOrValueExpression MessageTypeName
		{
			get
			{
				return this._messageTypeName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._messageTypeName = value;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060030C6 RID: 12486 RVA: 0x0016E9B1 File Offset: 0x0016CBB1
		// (set) Token: 0x060030C7 RID: 12487 RVA: 0x0016E9B9 File Offset: 0x0016CBB9
		public ScalarExpression MessageBody
		{
			get
			{
				return this._messageBody;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._messageBody = value;
			}
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x0016E9C9 File Offset: 0x0016CBC9
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x0016E9D8 File Offset: 0x0016CBD8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.ConversationHandles.Count;
			while (i < count)
			{
				this.ConversationHandles[i].Accept(visitor);
				i++;
			}
			if (this.MessageTypeName != null)
			{
				this.MessageTypeName.Accept(visitor);
			}
			if (this.MessageBody != null)
			{
				this.MessageBody.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E3E RID: 7742
		private List<ScalarExpression> _conversationHandles = new List<ScalarExpression>();

		// Token: 0x04001E3F RID: 7743
		private IdentifierOrValueExpression _messageTypeName;

		// Token: 0x04001E40 RID: 7744
		private ScalarExpression _messageBody;
	}
}
