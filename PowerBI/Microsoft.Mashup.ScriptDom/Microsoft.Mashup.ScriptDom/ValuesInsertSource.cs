using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200025D RID: 605
	[Serializable]
	internal class ValuesInsertSource : InsertSource
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06002650 RID: 9808 RVA: 0x00163E6E File Offset: 0x0016206E
		// (set) Token: 0x06002651 RID: 9809 RVA: 0x00163E76 File Offset: 0x00162076
		public bool IsDefaultValues
		{
			get
			{
				return this._isDefaultValues;
			}
			set
			{
				this._isDefaultValues = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x00163E7F File Offset: 0x0016207F
		public IList<RowValue> RowValues
		{
			get
			{
				return this._rowValues;
			}
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x00163E87 File Offset: 0x00162087
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x00163E94 File Offset: 0x00162094
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.RowValues.Count;
			while (i < count)
			{
				this.RowValues[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B4C RID: 6988
		private bool _isDefaultValues;

		// Token: 0x04001B4D RID: 6989
		private List<RowValue> _rowValues = new List<RowValue>();
	}
}
