using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001FE RID: 510
	[Serializable]
	internal class SearchedWhenClause : WhenClause
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060023E3 RID: 9187 RVA: 0x001610DD File Offset: 0x0015F2DD
		// (set) Token: 0x060023E4 RID: 9188 RVA: 0x001610E5 File Offset: 0x0015F2E5
		public BooleanExpression WhenExpression
		{
			get
			{
				return this._whenExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._whenExpression = value;
			}
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x001610F5 File Offset: 0x0015F2F5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x00161101 File Offset: 0x0015F301
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.WhenExpression != null)
			{
				this.WhenExpression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A91 RID: 6801
		private BooleanExpression _whenExpression;
	}
}
