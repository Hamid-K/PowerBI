using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000232 RID: 562
	[Serializable]
	internal class IfStatement : TSqlStatement
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x00162AE2 File Offset: 0x00160CE2
		// (set) Token: 0x0600253A RID: 9530 RVA: 0x00162AEA File Offset: 0x00160CEA
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

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x00162AFA File Offset: 0x00160CFA
		// (set) Token: 0x0600253C RID: 9532 RVA: 0x00162B02 File Offset: 0x00160D02
		public TSqlStatement ThenStatement
		{
			get
			{
				return this._thenStatement;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._thenStatement = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x00162B12 File Offset: 0x00160D12
		// (set) Token: 0x0600253E RID: 9534 RVA: 0x00162B1A File Offset: 0x00160D1A
		public TSqlStatement ElseStatement
		{
			get
			{
				return this._elseStatement;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._elseStatement = value;
			}
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x00162B2A File Offset: 0x00160D2A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x00162B38 File Offset: 0x00160D38
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Predicate != null)
			{
				this.Predicate.Accept(visitor);
			}
			if (this.ThenStatement != null)
			{
				this.ThenStatement.Accept(visitor);
			}
			if (this.ElseStatement != null)
			{
				this.ElseStatement.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AF7 RID: 6903
		private BooleanExpression _predicate;

		// Token: 0x04001AF8 RID: 6904
		private TSqlStatement _thenStatement;

		// Token: 0x04001AF9 RID: 6905
		private TSqlStatement _elseStatement;
	}
}
