using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureTables
{
	// Token: 0x02000EB5 RID: 3765
	public static class AzureTablesConstants
	{
		// Token: 0x04003693 RID: 13971
		public const string TablesRequestSuffix = "Tables";

		// Token: 0x04003694 RID: 13972
		public const string TablesFeedName = "TableName";

		// Token: 0x04003695 RID: 13973
		public const string NextPartitionKey = "x-ms-continuation-NextPartitionKey";

		// Token: 0x04003696 RID: 13974
		public const string NextRowKey = "x-ms-continuation-NextRowKey";

		// Token: 0x04003697 RID: 13975
		public const string NextTableKey = "x-ms-continuation-NextTableName";

		// Token: 0x04003698 RID: 13976
		public const string NextPartitionKeyInUri = "NextPartitionKey";

		// Token: 0x04003699 RID: 13977
		public const string NextRowKeyInUri = "NextRowKey";

		// Token: 0x0400369A RID: 13978
		public const string NextTableKeyInUri = "NextTableName";

		// Token: 0x0400369B RID: 13979
		public const int BiggestPageSize = 1000;

		// Token: 0x0400369C RID: 13980
		public const string PartitionKeyColumnName = "PartitionKey";

		// Token: 0x0400369D RID: 13981
		public const string RowKeyColumnName = "RowKey";

		// Token: 0x0400369E RID: 13982
		public const string TimestampColumnName = "Timestamp";

		// Token: 0x0400369F RID: 13983
		public const string ContentColumnName = "Content";

		// Token: 0x040036A0 RID: 13984
		public const string TempContentColumnName = "TempContent";

		// Token: 0x040036A1 RID: 13985
		public static readonly RecordValue ColumnList = RecordValue.New(Keys.New("PartitionKey", "RowKey", "Timestamp", "Content"), new Value[]
		{
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.DateTime,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Record,
				LogicalValue.False
			})
		});

		// Token: 0x040036A2 RID: 13986
		public static readonly string[] PredefinedColumns = new string[] { "PartitionKey", "RowKey", "Timestamp" };

		// Token: 0x040036A3 RID: 13987
		public const int DefaultColumnCounts = 4;

		// Token: 0x040036A4 RID: 13988
		public const int IndexOfContentColumn = 3;
	}
}
