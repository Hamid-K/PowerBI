using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x0200000E RID: 14
	internal static class DataRowExtensionsMethods
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000037EC File Offset: 0x000019EC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static DataRow CloneToTable(this DataRow originalRow, DataTable dataTable)
		{
			DataRow dataRow = dataTable.NewRow();
			foreach (object obj in dataTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (originalRow.Table.Columns.Contains(dataColumn.ColumnName))
				{
					dataRow[dataColumn.ColumnName] = originalRow[dataColumn.ColumnName];
				}
			}
			return dataRow;
		}
	}
}
