using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000299 RID: 665
	[Serializable]
	internal class ExecuteAsClause : TSqlFragment
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060027A4 RID: 10148 RVA: 0x00165474 File Offset: 0x00163674
		// (set) Token: 0x060027A5 RID: 10149 RVA: 0x0016547C File Offset: 0x0016367C
		public ExecuteAsOption ExecuteAsOption
		{
			get
			{
				return this._executeAsOption;
			}
			set
			{
				this._executeAsOption = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060027A6 RID: 10150 RVA: 0x00165485 File Offset: 0x00163685
		// (set) Token: 0x060027A7 RID: 10151 RVA: 0x0016548D File Offset: 0x0016368D
		public Literal Literal
		{
			get
			{
				return this._literal;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._literal = value;
			}
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x0016549D File Offset: 0x0016369D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x001654A9 File Offset: 0x001636A9
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Literal != null)
			{
				this.Literal.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BA7 RID: 7079
		private ExecuteAsOption _executeAsOption;

		// Token: 0x04001BA8 RID: 7080
		private Literal _literal;
	}
}
