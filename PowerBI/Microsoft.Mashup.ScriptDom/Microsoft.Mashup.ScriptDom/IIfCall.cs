using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000204 RID: 516
	[Serializable]
	internal class IIfCall : PrimaryExpression
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x00161339 File Offset: 0x0015F539
		// (set) Token: 0x06002402 RID: 9218 RVA: 0x00161341 File Offset: 0x0015F541
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

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06002403 RID: 9219 RVA: 0x00161351 File Offset: 0x0015F551
		// (set) Token: 0x06002404 RID: 9220 RVA: 0x00161359 File Offset: 0x0015F559
		public ScalarExpression ThenExpression
		{
			get
			{
				return this._thenExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._thenExpression = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06002405 RID: 9221 RVA: 0x00161369 File Offset: 0x0015F569
		// (set) Token: 0x06002406 RID: 9222 RVA: 0x00161371 File Offset: 0x0015F571
		public ScalarExpression ElseExpression
		{
			get
			{
				return this._elseExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._elseExpression = value;
			}
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x00161381 File Offset: 0x0015F581
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x00161390 File Offset: 0x0015F590
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Predicate != null)
			{
				this.Predicate.Accept(visitor);
			}
			if (this.ThenExpression != null)
			{
				this.ThenExpression.Accept(visitor);
			}
			if (this.ElseExpression != null)
			{
				this.ElseExpression.Accept(visitor);
			}
		}

		// Token: 0x04001A99 RID: 6809
		private BooleanExpression _predicate;

		// Token: 0x04001A9A RID: 6810
		private ScalarExpression _thenExpression;

		// Token: 0x04001A9B RID: 6811
		private ScalarExpression _elseExpression;
	}
}
