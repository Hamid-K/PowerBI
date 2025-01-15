using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000235 RID: 565
	[Serializable]
	internal class WhileStatement : TSqlStatement
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x00162BFD File Offset: 0x00160DFD
		// (set) Token: 0x0600254F RID: 9551 RVA: 0x00162C05 File Offset: 0x00160E05
		public BooleanExpression Predicate
		{
			get
			{
				return this._predicate;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._predicate = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06002550 RID: 9552 RVA: 0x00162C15 File Offset: 0x00160E15
		// (set) Token: 0x06002551 RID: 9553 RVA: 0x00162C1D File Offset: 0x00160E1D
		public TSqlStatement Statement
		{
			get
			{
				return this._statement;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._statement = value;
			}
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x00162C2D File Offset: 0x00160E2D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x00162C39 File Offset: 0x00160E39
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Predicate != null)
			{
				this.Predicate.Accept(visitor);
			}
			if (this.Statement != null)
			{
				this.Statement.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AFD RID: 6909
		private BooleanExpression _predicate;

		// Token: 0x04001AFE RID: 6910
		private TSqlStatement _statement;
	}
}
