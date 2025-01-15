using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000260 RID: 608
	[Serializable]
	internal class RowValue : TSqlFragment
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x00163F77 File Offset: 0x00162177
		public IList<ScalarExpression> ColumnValues
		{
			get
			{
				return this._columnValues;
			}
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x00163F7F File Offset: 0x0016217F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x00163F8C File Offset: 0x0016218C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.ColumnValues.Count;
			while (i < count)
			{
				this.ColumnValues[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B50 RID: 6992
		private List<ScalarExpression> _columnValues = new List<ScalarExpression>();
	}
}
