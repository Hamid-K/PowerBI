using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001F5 RID: 501
	[Serializable]
	internal class UpdateForClause : ForClause
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x00160D57 File Offset: 0x0015EF57
		public IList<ColumnReferenceExpression> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x00160D5F File Offset: 0x0015EF5F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x00160D6C File Offset: 0x0015EF6C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Columns.Count;
			while (i < count)
			{
				this.Columns[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A83 RID: 6787
		private List<ColumnReferenceExpression> _columns = new List<ColumnReferenceExpression>();
	}
}
