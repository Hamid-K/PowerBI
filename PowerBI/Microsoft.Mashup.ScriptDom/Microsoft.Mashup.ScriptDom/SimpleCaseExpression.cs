using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000200 RID: 512
	[Serializable]
	internal class SimpleCaseExpression : CaseExpression
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x00161163 File Offset: 0x0015F363
		// (set) Token: 0x060023ED RID: 9197 RVA: 0x0016116B File Offset: 0x0015F36B
		public ScalarExpression InputExpression
		{
			get
			{
				return this._inputExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._inputExpression = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x0016117B File Offset: 0x0015F37B
		public IList<SimpleWhenClause> WhenClauses
		{
			get
			{
				return this._whenClauses;
			}
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x00161183 File Offset: 0x0015F383
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x00161190 File Offset: 0x0015F390
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.InputExpression != null)
			{
				this.InputExpression.Accept(visitor);
			}
			int i = 0;
			int count = this.WhenClauses.Count;
			while (i < count)
			{
				this.WhenClauses[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A93 RID: 6803
		private ScalarExpression _inputExpression;

		// Token: 0x04001A94 RID: 6804
		private List<SimpleWhenClause> _whenClauses = new List<SimpleWhenClause>();
	}
}
