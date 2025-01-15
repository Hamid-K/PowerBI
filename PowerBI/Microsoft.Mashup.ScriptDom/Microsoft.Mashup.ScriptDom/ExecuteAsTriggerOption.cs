using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001BB RID: 443
	[Serializable]
	internal class ExecuteAsTriggerOption : TriggerOption
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x0015F568 File Offset: 0x0015D768
		// (set) Token: 0x06002269 RID: 8809 RVA: 0x0015F570 File Offset: 0x0015D770
		public ExecuteAsClause ExecuteAsClause
		{
			get
			{
				return this._executeAsClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._executeAsClause = value;
			}
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x0015F580 File Offset: 0x0015D780
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x0015F58C File Offset: 0x0015D78C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.ExecuteAsClause != null)
			{
				this.ExecuteAsClause.Accept(visitor);
			}
		}

		// Token: 0x04001A24 RID: 6692
		private ExecuteAsClause _executeAsClause;
	}
}
