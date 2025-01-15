using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001FD RID: 509
	[Serializable]
	internal class SimpleWhenClause : WhenClause
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060023DE RID: 9182 RVA: 0x00161094 File Offset: 0x0015F294
		// (set) Token: 0x060023DF RID: 9183 RVA: 0x0016109C File Offset: 0x0015F29C
		public ScalarExpression WhenExpression
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

		// Token: 0x060023E0 RID: 9184 RVA: 0x001610AC File Offset: 0x0015F2AC
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x001610B8 File Offset: 0x0015F2B8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.WhenExpression != null)
			{
				this.WhenExpression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A90 RID: 6800
		private ScalarExpression _whenExpression;
	}
}
