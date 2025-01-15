using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003BD RID: 957
	[Serializable]
	internal class OutputIntoClause : TSqlFragment
	{
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06002EB4 RID: 11956 RVA: 0x0016C949 File Offset: 0x0016AB49
		public IList<SelectElement> SelectColumns
		{
			get
			{
				return this._selectColumns;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06002EB5 RID: 11957 RVA: 0x0016C951 File Offset: 0x0016AB51
		// (set) Token: 0x06002EB6 RID: 11958 RVA: 0x0016C959 File Offset: 0x0016AB59
		public TableReference IntoTable
		{
			get
			{
				return this._intoTable;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._intoTable = value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06002EB7 RID: 11959 RVA: 0x0016C969 File Offset: 0x0016AB69
		public IList<ColumnReferenceExpression> IntoTableColumns
		{
			get
			{
				return this._intoTableColumns;
			}
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x0016C971 File Offset: 0x0016AB71
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x0016C980 File Offset: 0x0016AB80
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.SelectColumns.Count;
			while (i < count)
			{
				this.SelectColumns[i].Accept(visitor);
				i++;
			}
			if (this.IntoTable != null)
			{
				this.IntoTable.Accept(visitor);
			}
			int j = 0;
			int count2 = this.IntoTableColumns.Count;
			while (j < count2)
			{
				this.IntoTableColumns[j].Accept(visitor);
				j++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DB3 RID: 7603
		private List<SelectElement> _selectColumns = new List<SelectElement>();

		// Token: 0x04001DB4 RID: 7604
		private TableReference _intoTable;

		// Token: 0x04001DB5 RID: 7605
		private List<ColumnReferenceExpression> _intoTableColumns = new List<ColumnReferenceExpression>();
	}
}
