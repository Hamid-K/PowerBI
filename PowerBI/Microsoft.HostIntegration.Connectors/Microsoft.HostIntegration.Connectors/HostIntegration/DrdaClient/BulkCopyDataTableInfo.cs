using System;
using System.Data;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009B1 RID: 2481
	internal class BulkCopyDataTableInfo : IBulkCopySourceInfo
	{
		// Token: 0x06004CBE RID: 19646 RVA: 0x001329FD File Offset: 0x00130BFD
		internal BulkCopyDataTableInfo(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06004CBF RID: 19647 RVA: 0x00132A0C File Offset: 0x00130C0C
		public int FieldCount
		{
			get
			{
				return this._table.Columns.Count;
			}
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x00132A1E File Offset: 0x00130C1E
		public Type GetFieldType(int index)
		{
			return this._table.Columns[index].DataType;
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x00132A36 File Offset: 0x00130C36
		public string GetFieldName(int index)
		{
			return this._table.Columns[index].ColumnName;
		}

		// Token: 0x04003CAF RID: 15535
		private DataTable _table;
	}
}
