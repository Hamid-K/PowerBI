using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003D1 RID: 977
	[Serializable]
	internal class OffsetClause : TSqlFragment
	{
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06002F3F RID: 12095 RVA: 0x0016D327 File Offset: 0x0016B527
		// (set) Token: 0x06002F40 RID: 12096 RVA: 0x0016D32F File Offset: 0x0016B52F
		public ScalarExpression OffsetExpression
		{
			get
			{
				return this._offsetExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._offsetExpression = value;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06002F41 RID: 12097 RVA: 0x0016D33F File Offset: 0x0016B53F
		// (set) Token: 0x06002F42 RID: 12098 RVA: 0x0016D347 File Offset: 0x0016B547
		public ScalarExpression FetchExpression
		{
			get
			{
				return this._fetchExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._fetchExpression = value;
			}
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x0016D357 File Offset: 0x0016B557
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x0016D363 File Offset: 0x0016B563
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.OffsetExpression != null)
			{
				this.OffsetExpression.Accept(visitor);
			}
			if (this.FetchExpression != null)
			{
				this.FetchExpression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DDF RID: 7647
		private ScalarExpression _offsetExpression;

		// Token: 0x04001DE0 RID: 7648
		private ScalarExpression _fetchExpression;
	}
}
