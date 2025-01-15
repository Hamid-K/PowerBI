using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000E5 RID: 229
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class SchemaTableHelper
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x0000E268 File Offset: 0x0000C468
		public static int MaxRowCount(DataTable schemaTable)
		{
			return Math.Max(4096 / Math.Max(schemaTable.Rows.Count, 1), 1);
		}

		// Token: 0x040003F3 RID: 1011
		private const int MaxCellsPerPage = 4096;

		// Token: 0x040003F4 RID: 1012
		public const string ColumnName = "ColumnName";

		// Token: 0x040003F5 RID: 1013
		public const string ColumnGuid = "ColumnGuid";

		// Token: 0x040003F6 RID: 1014
		public const string ColumnPropId = "ColumnPropId";

		// Token: 0x040003F7 RID: 1015
		public const string ColumnOrdinal = "ColumnOrdinal";

		// Token: 0x040003F8 RID: 1016
		public const string DataType = "DataType";

		// Token: 0x040003F9 RID: 1017
		public const string AllowDBNull = "AllowDBNull";

		// Token: 0x040003FA RID: 1018
		public const string IsKey = "IsKey";
	}
}
