using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001CA RID: 458
	[Serializable]
	internal class ExecuteAsFunctionOption : FunctionOption
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060022C8 RID: 8904 RVA: 0x0015FD16 File Offset: 0x0015DF16
		// (set) Token: 0x060022C9 RID: 8905 RVA: 0x0015FD1E File Offset: 0x0015DF1E
		public ExecuteAsClause ExecuteAs
		{
			get
			{
				return this._executeAs;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._executeAs = value;
			}
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x0015FD2E File Offset: 0x0015DF2E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x0015FD3A File Offset: 0x0015DF3A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.ExecuteAs != null)
			{
				this.ExecuteAs.Accept(visitor);
			}
		}

		// Token: 0x04001A42 RID: 6722
		private ExecuteAsClause _executeAs;
	}
}
