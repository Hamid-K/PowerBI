using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200035C RID: 860
	[Serializable]
	internal class OrderBulkInsertOption : BulkInsertOption
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x00169F9C File Offset: 0x0016819C
		public IList<ColumnWithSortOrder> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06002C46 RID: 11334 RVA: 0x00169FA4 File Offset: 0x001681A4
		// (set) Token: 0x06002C47 RID: 11335 RVA: 0x00169FAC File Offset: 0x001681AC
		public bool IsUnique
		{
			get
			{
				return this._isUnique;
			}
			set
			{
				this._isUnique = value;
			}
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x00169FB5 File Offset: 0x001681B5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x00169FC4 File Offset: 0x001681C4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.Columns.Count;
			while (i < count)
			{
				this.Columns[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001CF7 RID: 7415
		private List<ColumnWithSortOrder> _columns = new List<ColumnWithSortOrder>();

		// Token: 0x04001CF8 RID: 7416
		private bool _isUnique;
	}
}
