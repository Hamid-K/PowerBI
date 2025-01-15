using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000201 RID: 513
	[Serializable]
	internal class SearchedCaseExpression : CaseExpression
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x001611F5 File Offset: 0x0015F3F5
		public IList<SearchedWhenClause> WhenClauses
		{
			get
			{
				return this._whenClauses;
			}
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x001611FD File Offset: 0x0015F3FD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x0016120C File Offset: 0x0015F40C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.WhenClauses.Count;
			while (i < count)
			{
				this.WhenClauses[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A95 RID: 6805
		private List<SearchedWhenClause> _whenClauses = new List<SearchedWhenClause>();
	}
}
