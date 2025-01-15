using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002FE RID: 766
	[Serializable]
	internal class KillStatsJobStatement : TSqlStatement
	{
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060029E1 RID: 10721 RVA: 0x001679B1 File Offset: 0x00165BB1
		// (set) Token: 0x060029E2 RID: 10722 RVA: 0x001679B9 File Offset: 0x00165BB9
		public ScalarExpression JobId
		{
			get
			{
				return this._jobId;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._jobId = value;
			}
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x001679C9 File Offset: 0x00165BC9
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x001679D5 File Offset: 0x00165BD5
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.JobId != null)
			{
				this.JobId.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C41 RID: 7233
		private ScalarExpression _jobId;
	}
}
