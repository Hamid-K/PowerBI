using System;
using System.Linq;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FC8 RID: 8136
	public class SchemaTableHelper
	{
		// Token: 0x0600C6D9 RID: 50905 RVA: 0x00279F5C File Offset: 0x0027815C
		public static int MaxRowCount(TableSchema schema)
		{
			int num = 0;
			for (int i = 0; i < schema.ColumnCount; i++)
			{
				num += SchemaTableHelper.EstimateColumnSize(schema.GetColumn(i));
			}
			return Math.Max(Math.Min(262144 / Math.Max(num, 1), 4096), 1);
		}

		// Token: 0x0600C6DA RID: 50906 RVA: 0x00279FA8 File Offset: 0x002781A8
		private static int EstimateColumnSize(SchemaColumn column)
		{
			if (column.ColumnSchema != null)
			{
				return column.ColumnSchema.Sum((SchemaColumn s) => SchemaTableHelper.EstimateColumnSize(s));
			}
			bool flag;
			ColumnType columnType = Column.GetColumnType(column.DataType, out flag);
			int num = -1;
			switch (columnType)
			{
			case ColumnType.String:
				num = 128;
				break;
			case ColumnType.Binary:
				num = 8192;
				break;
			case ColumnType.TypedRecord:
			case ColumnType.UntypedRecord:
				num = 8192;
				break;
			}
			return Column.GetColumnSizeEstimate(columnType, num) + (flag ? 96 : 0);
		}

		// Token: 0x0400657E RID: 25982
		private const int TextLengthEstimate = 128;

		// Token: 0x0400657F RID: 25983
		private const int BinaryLengthEstimate = 8192;

		// Token: 0x04006580 RID: 25984
		private const int RecordLengthEstimate = 8192;

		// Token: 0x04006581 RID: 25985
		private const int MetadataLengthEstimate = 96;

		// Token: 0x04006582 RID: 25986
		private const int MaxBytesPerPage = 262144;

		// Token: 0x04006583 RID: 25987
		private const int MaxRowsPerPage = 4096;
	}
}
