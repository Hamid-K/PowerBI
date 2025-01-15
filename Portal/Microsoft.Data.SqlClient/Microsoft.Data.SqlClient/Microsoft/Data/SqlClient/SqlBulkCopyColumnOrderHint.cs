using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000053 RID: 83
	public sealed class SqlBulkCopyColumnOrderHint
	{
		// Token: 0x06000838 RID: 2104 RVA: 0x00012EBC File Offset: 0x000110BC
		public SqlBulkCopyColumnOrderHint(string column, SortOrder sortOrder)
		{
			this.Column = column;
			this.SortOrder = sortOrder;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000839 RID: 2105 RVA: 0x00012ED4 File Offset: 0x000110D4
		// (remove) Token: 0x0600083A RID: 2106 RVA: 0x00012F0C File Offset: 0x0001110C
		internal event EventHandler<string> NameChanging;

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00012F41 File Offset: 0x00011141
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x00012F52 File Offset: 0x00011152
		public string Column
		{
			get
			{
				return this._columnName ?? string.Empty;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw SQL.BulkLoadNullEmptyColumnName("Column");
				}
				if (this._columnName != value)
				{
					this.OnNameChanging(value);
					this._columnName = value;
				}
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00012F83 File Offset: 0x00011183
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x00012F8B File Offset: 0x0001118B
		public SortOrder SortOrder
		{
			get
			{
				return this._sortOrder;
			}
			set
			{
				if (value != SortOrder.Unspecified)
				{
					this._sortOrder = value;
					return;
				}
				throw SQL.BulkLoadUnspecifiedSortOrder();
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00012FA0 File Offset: 0x000111A0
		private void OnNameChanging(string newName)
		{
			EventHandler<string> nameChanging = this.NameChanging;
			if (nameChanging != null)
			{
				nameChanging(this, newName);
			}
		}

		// Token: 0x0400012A RID: 298
		private string _columnName;

		// Token: 0x0400012B RID: 299
		private SortOrder _sortOrder;
	}
}
