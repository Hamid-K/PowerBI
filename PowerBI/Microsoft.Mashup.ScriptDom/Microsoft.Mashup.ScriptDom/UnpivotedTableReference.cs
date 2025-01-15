using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003AA RID: 938
	[Serializable]
	internal class UnpivotedTableReference : TableReferenceWithAlias
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06002E44 RID: 11844 RVA: 0x0016C1DA File Offset: 0x0016A3DA
		// (set) Token: 0x06002E45 RID: 11845 RVA: 0x0016C1E2 File Offset: 0x0016A3E2
		public TableReference TableReference
		{
			get
			{
				return this._tableReference;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._tableReference = value;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06002E46 RID: 11846 RVA: 0x0016C1F2 File Offset: 0x0016A3F2
		public IList<ColumnReferenceExpression> InColumns
		{
			get
			{
				return this._inColumns;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06002E47 RID: 11847 RVA: 0x0016C1FA File Offset: 0x0016A3FA
		// (set) Token: 0x06002E48 RID: 11848 RVA: 0x0016C202 File Offset: 0x0016A402
		public Identifier PivotColumn
		{
			get
			{
				return this._pivotColumn;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._pivotColumn = value;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x0016C212 File Offset: 0x0016A412
		// (set) Token: 0x06002E4A RID: 11850 RVA: 0x0016C21A File Offset: 0x0016A41A
		public Identifier ValueColumn
		{
			get
			{
				return this._valueColumn;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._valueColumn = value;
			}
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x0016C22A File Offset: 0x0016A42A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x0016C238 File Offset: 0x0016A438
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.TableReference != null)
			{
				this.TableReference.Accept(visitor);
			}
			int i = 0;
			int count = this.InColumns.Count;
			while (i < count)
			{
				this.InColumns[i].Accept(visitor);
				i++;
			}
			if (this.PivotColumn != null)
			{
				this.PivotColumn.Accept(visitor);
			}
			if (this.ValueColumn != null)
			{
				this.ValueColumn.Accept(visitor);
			}
		}

		// Token: 0x04001D93 RID: 7571
		private TableReference _tableReference;

		// Token: 0x04001D94 RID: 7572
		private List<ColumnReferenceExpression> _inColumns = new List<ColumnReferenceExpression>();

		// Token: 0x04001D95 RID: 7573
		private Identifier _pivotColumn;

		// Token: 0x04001D96 RID: 7574
		private Identifier _valueColumn;
	}
}
