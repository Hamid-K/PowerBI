using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003BC RID: 956
	[Serializable]
	internal class OutputClause : TSqlFragment
	{
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x0016C8E1 File Offset: 0x0016AAE1
		public IList<SelectElement> SelectColumns
		{
			get
			{
				return this._selectColumns;
			}
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x0016C8E9 File Offset: 0x0016AAE9
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x0016C8F8 File Offset: 0x0016AAF8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.SelectColumns.Count;
			while (i < count)
			{
				this.SelectColumns[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DB2 RID: 7602
		private List<SelectElement> _selectColumns = new List<SelectElement>();
	}
}
