using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003A9 RID: 937
	[Serializable]
	internal class PivotedTableReference : TableReferenceWithAlias
	{
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06002E39 RID: 11833 RVA: 0x0016C0B4 File Offset: 0x0016A2B4
		// (set) Token: 0x06002E3A RID: 11834 RVA: 0x0016C0BC File Offset: 0x0016A2BC
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

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x0016C0CC File Offset: 0x0016A2CC
		public IList<Identifier> InColumns
		{
			get
			{
				return this._inColumns;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06002E3C RID: 11836 RVA: 0x0016C0D4 File Offset: 0x0016A2D4
		// (set) Token: 0x06002E3D RID: 11837 RVA: 0x0016C0DC File Offset: 0x0016A2DC
		public ColumnReferenceExpression PivotColumn
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

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06002E3E RID: 11838 RVA: 0x0016C0EC File Offset: 0x0016A2EC
		public IList<ColumnReferenceExpression> ValueColumns
		{
			get
			{
				return this._valueColumns;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06002E3F RID: 11839 RVA: 0x0016C0F4 File Offset: 0x0016A2F4
		// (set) Token: 0x06002E40 RID: 11840 RVA: 0x0016C0FC File Offset: 0x0016A2FC
		public MultiPartIdentifier AggregateFunctionIdentifier
		{
			get
			{
				return this._aggregateFunctionIdentifier;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._aggregateFunctionIdentifier = value;
			}
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x0016C10C File Offset: 0x0016A30C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x0016C118 File Offset: 0x0016A318
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
			int j = 0;
			int count2 = this.ValueColumns.Count;
			while (j < count2)
			{
				this.ValueColumns[j].Accept(visitor);
				j++;
			}
			if (this.AggregateFunctionIdentifier != null)
			{
				this.AggregateFunctionIdentifier.Accept(visitor);
			}
		}

		// Token: 0x04001D8E RID: 7566
		private TableReference _tableReference;

		// Token: 0x04001D8F RID: 7567
		private List<Identifier> _inColumns = new List<Identifier>();

		// Token: 0x04001D90 RID: 7568
		private ColumnReferenceExpression _pivotColumn;

		// Token: 0x04001D91 RID: 7569
		private List<ColumnReferenceExpression> _valueColumns = new List<ColumnReferenceExpression>();

		// Token: 0x04001D92 RID: 7570
		private MultiPartIdentifier _aggregateFunctionIdentifier;
	}
}
