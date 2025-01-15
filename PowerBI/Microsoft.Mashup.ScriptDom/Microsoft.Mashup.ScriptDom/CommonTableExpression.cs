using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001CF RID: 463
	[Serializable]
	internal class CommonTableExpression : TSqlFragment
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x0015FE68 File Offset: 0x0015E068
		// (set) Token: 0x060022DE RID: 8926 RVA: 0x0015FE70 File Offset: 0x0015E070
		public Identifier ExpressionName
		{
			get
			{
				return this._expressionName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._expressionName = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x0015FE80 File Offset: 0x0015E080
		public IList<Identifier> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x0015FE88 File Offset: 0x0015E088
		// (set) Token: 0x060022E1 RID: 8929 RVA: 0x0015FE90 File Offset: 0x0015E090
		public QueryExpression QueryExpression
		{
			get
			{
				return this._queryExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._queryExpression = value;
			}
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x0015FEA0 File Offset: 0x0015E0A0
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x0015FEAC File Offset: 0x0015E0AC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ExpressionName != null)
			{
				this.ExpressionName.Accept(visitor);
			}
			int i = 0;
			int count = this.Columns.Count;
			while (i < count)
			{
				this.Columns[i].Accept(visitor);
				i++;
			}
			if (this.QueryExpression != null)
			{
				this.QueryExpression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A46 RID: 6726
		private Identifier _expressionName;

		// Token: 0x04001A47 RID: 6727
		private List<Identifier> _columns = new List<Identifier>();

		// Token: 0x04001A48 RID: 6728
		private QueryExpression _queryExpression;
	}
}
