using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200041C RID: 1052
	[Serializable]
	internal class BeginConversationTimerStatement : TSqlStatement
	{
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060030EA RID: 12522 RVA: 0x0016EC46 File Offset: 0x0016CE46
		// (set) Token: 0x060030EB RID: 12523 RVA: 0x0016EC4E File Offset: 0x0016CE4E
		public ScalarExpression Handle
		{
			get
			{
				return this._handle;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._handle = value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060030EC RID: 12524 RVA: 0x0016EC5E File Offset: 0x0016CE5E
		// (set) Token: 0x060030ED RID: 12525 RVA: 0x0016EC66 File Offset: 0x0016CE66
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

		// Token: 0x060030EE RID: 12526 RVA: 0x0016EC76 File Offset: 0x0016CE76
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x0016EC82 File Offset: 0x0016CE82
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Handle != null)
			{
				this.Handle.Accept(visitor);
			}
			if (this.Timeout != null)
			{
				this.Timeout.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E4C RID: 7756
		private ScalarExpression _handle;

		// Token: 0x04001E4D RID: 7757
		private ScalarExpression _timeout;
	}
}
