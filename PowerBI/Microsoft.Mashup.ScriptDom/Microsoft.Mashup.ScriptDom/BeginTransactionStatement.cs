using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000225 RID: 549
	[Serializable]
	internal class BeginTransactionStatement : TransactionStatement
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x0016266B File Offset: 0x0016086B
		// (set) Token: 0x060024F8 RID: 9464 RVA: 0x00162673 File Offset: 0x00160873
		public bool Distributed
		{
			get
			{
				return this._distributed;
			}
			set
			{
				this._distributed = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x0016267C File Offset: 0x0016087C
		// (set) Token: 0x060024FA RID: 9466 RVA: 0x00162684 File Offset: 0x00160884
		public bool MarkDefined
		{
			get
			{
				return this._markDefined;
			}
			set
			{
				this._markDefined = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x0016268D File Offset: 0x0016088D
		// (set) Token: 0x060024FC RID: 9468 RVA: 0x00162695 File Offset: 0x00160895
		public ValueExpression MarkDescription
		{
			get
			{
				return this._markDescription;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._markDescription = value;
			}
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x001626A5 File Offset: 0x001608A5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x001626B1 File Offset: 0x001608B1
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.MarkDescription != null)
			{
				this.MarkDescription.Accept(visitor);
			}
		}

		// Token: 0x04001AE9 RID: 6889
		private bool _distributed;

		// Token: 0x04001AEA RID: 6890
		private bool _markDefined;

		// Token: 0x04001AEB RID: 6891
		private ValueExpression _markDescription;
	}
}
