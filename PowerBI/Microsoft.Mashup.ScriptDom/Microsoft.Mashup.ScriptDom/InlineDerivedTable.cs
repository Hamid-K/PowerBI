using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001E6 RID: 486
	[Serializable]
	internal class InlineDerivedTable : TableReferenceWithAliasAndColumns
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x0016069F File Offset: 0x0015E89F
		public IList<RowValue> RowValues
		{
			get
			{
				return this._rowValues;
			}
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x001606A7 File Offset: 0x0015E8A7
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x001606B4 File Offset: 0x0015E8B4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.RowValues.Count;
			while (i < count)
			{
				this.RowValues[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001A65 RID: 6757
		private List<RowValue> _rowValues = new List<RowValue>();
	}
}
