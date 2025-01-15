using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000054 RID: 84
	public sealed class SqlBulkCopyColumnOrderHintCollection : CollectionBase
	{
		// Token: 0x1700067A RID: 1658
		public SqlBulkCopyColumnOrderHint this[int index]
		{
			get
			{
				return (SqlBulkCopyColumnOrderHint)base.List[index];
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00012FD4 File Offset: 0x000111D4
		public SqlBulkCopyColumnOrderHint Add(SqlBulkCopyColumnOrderHint columnOrderHint)
		{
			if (columnOrderHint == null)
			{
				throw new ArgumentNullException("columnOrderHint");
			}
			if (string.IsNullOrEmpty(columnOrderHint.Column) || columnOrderHint.SortOrder == SortOrder.Unspecified)
			{
				throw SQL.BulkLoadInvalidOrderHint();
			}
			this.RegisterColumnName(columnOrderHint, columnOrderHint.Column);
			base.InnerList.Add(columnOrderHint);
			return columnOrderHint;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00013026 File Offset: 0x00011226
		public SqlBulkCopyColumnOrderHint Add(string column, SortOrder sortOrder)
		{
			return this.Add(new SqlBulkCopyColumnOrderHint(column, sortOrder));
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00013038 File Offset: 0x00011238
		protected override void OnClear()
		{
			foreach (object obj in base.InnerList)
			{
				SqlBulkCopyColumnOrderHint sqlBulkCopyColumnOrderHint = (SqlBulkCopyColumnOrderHint)obj;
				this.UnregisterColumnName(sqlBulkCopyColumnOrderHint, sqlBulkCopyColumnOrderHint.Column);
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00012D91 File Offset: 0x00010F91
		public bool Contains(SqlBulkCopyColumnOrderHint value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00012D9F File Offset: 0x00010F9F
		public void CopyTo(SqlBulkCopyColumnOrderHint[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00012DDC File Offset: 0x00010FDC
		public int IndexOf(SqlBulkCopyColumnOrderHint value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00013098 File Offset: 0x00011298
		public void Insert(int index, SqlBulkCopyColumnOrderHint columnOrderHint)
		{
			if (index < 0 || index > base.InnerList.Count)
			{
				base.InnerList.Insert(index, columnOrderHint);
			}
			if (columnOrderHint == null)
			{
				throw new ArgumentNullException("columnOrderHint");
			}
			this.RegisterColumnName(columnOrderHint, columnOrderHint.Column);
			base.InnerList.Insert(index, columnOrderHint);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000130EC File Offset: 0x000112EC
		public void Remove(SqlBulkCopyColumnOrderHint columnOrderHint)
		{
			if (columnOrderHint == null)
			{
				throw new ArgumentNullException("columnOrderHint");
			}
			base.List.Remove(columnOrderHint);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00013108 File Offset: 0x00011308
		protected override void OnRemove(int index, object value)
		{
			SqlBulkCopyColumnOrderHint sqlBulkCopyColumnOrderHint = value as SqlBulkCopyColumnOrderHint;
			if (sqlBulkCopyColumnOrderHint != null)
			{
				this.UnregisterColumnName(sqlBulkCopyColumnOrderHint, sqlBulkCopyColumnOrderHint.Column);
				return;
			}
			throw new ArgumentNullException("orderHint");
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00013138 File Offset: 0x00011338
		private void ColumnNameChanging(object sender, string newName)
		{
			SqlBulkCopyColumnOrderHint sqlBulkCopyColumnOrderHint = sender as SqlBulkCopyColumnOrderHint;
			if (sqlBulkCopyColumnOrderHint != null)
			{
				if (this._columnNames.Contains(newName))
				{
					throw SQL.BulkLoadOrderHintDuplicateColumn(newName);
				}
				this.UnregisterColumnName(sqlBulkCopyColumnOrderHint, sqlBulkCopyColumnOrderHint.Column);
				this.RegisterColumnName(sqlBulkCopyColumnOrderHint, newName);
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00013179 File Offset: 0x00011379
		private void RegisterColumnName(SqlBulkCopyColumnOrderHint orderHint, string columnName)
		{
			if (this._columnNames.Contains(columnName))
			{
				throw SQL.BulkLoadOrderHintDuplicateColumn(orderHint.Column);
			}
			this._columnNames.Add(columnName);
			orderHint.NameChanging += this.ColumnNameChanging;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000131B4 File Offset: 0x000113B4
		private void UnregisterColumnName(SqlBulkCopyColumnOrderHint orderHint, string columnName)
		{
			if (this.Contains(orderHint))
			{
				this._columnNames.Remove(columnName);
				orderHint.NameChanging -= this.ColumnNameChanging;
			}
		}

		// Token: 0x0400012D RID: 301
		private readonly HashSet<string> _columnNames = new HashSet<string>();
	}
}
