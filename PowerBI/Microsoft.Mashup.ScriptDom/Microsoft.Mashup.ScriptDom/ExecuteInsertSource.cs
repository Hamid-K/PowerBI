using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200025F RID: 607
	[Serializable]
	internal class ExecuteInsertSource : InsertSource
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600265B RID: 9819 RVA: 0x00163F2E File Offset: 0x0016212E
		// (set) Token: 0x0600265C RID: 9820 RVA: 0x00163F36 File Offset: 0x00162136
		public ExecuteSpecification Execute
		{
			get
			{
				return this._execute;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._execute = value;
			}
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x00163F46 File Offset: 0x00162146
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x00163F52 File Offset: 0x00162152
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Execute != null)
			{
				this.Execute.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B4F RID: 6991
		private ExecuteSpecification _execute;
	}
}
