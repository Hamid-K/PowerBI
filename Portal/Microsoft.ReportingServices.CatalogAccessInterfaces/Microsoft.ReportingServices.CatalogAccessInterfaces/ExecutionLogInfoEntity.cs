using System;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x02000008 RID: 8
	public class ExecutionLogInfoEntity
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000247B File Offset: 0x0000067B
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002483 File Offset: 0x00000683
		public string ItemPath { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000248C File Offset: 0x0000068C
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002494 File Offset: 0x00000694
		public Guid ItemId { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000249D File Offset: 0x0000069D
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000024A5 File Offset: 0x000006A5
		public string UserName { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000024AE File Offset: 0x000006AE
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000024B6 File Offset: 0x000006B6
		public DateTimeOffset StartTime { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000024BF File Offset: 0x000006BF
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000024C7 File Offset: 0x000006C7
		public DateTimeOffset EndTime { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000024D0 File Offset: 0x000006D0
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000024D8 File Offset: 0x000006D8
		public string Format { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000024E1 File Offset: 0x000006E1
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000024E9 File Offset: 0x000006E9
		public string Parameters { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000024F2 File Offset: 0x000006F2
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000024FA File Offset: 0x000006FA
		public ExecutionLogInfoEntity.ExecutionLogExecType Source { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002503 File Offset: 0x00000703
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000250B File Offset: 0x0000070B
		public ExecutionLogInfoEntity.ErrorCode Status { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002514 File Offset: 0x00000714
		// (set) Token: 0x06000094 RID: 148 RVA: 0x0000251C File Offset: 0x0000071C
		public long ByteCount { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002525 File Offset: 0x00000725
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000252D File Offset: 0x0000072D
		public long RowCount { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002536 File Offset: 0x00000736
		// (set) Token: 0x06000098 RID: 152 RVA: 0x0000253E File Offset: 0x0000073E
		public int ProcessingTime { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002547 File Offset: 0x00000747
		// (set) Token: 0x0600009A RID: 154 RVA: 0x0000254F File Offset: 0x0000074F
		public int RenderingTime { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002558 File Offset: 0x00000758
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002560 File Offset: 0x00000760
		public int DataRetrievalTime { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002569 File Offset: 0x00000769
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00002571 File Offset: 0x00000771
		public string ExecutionId { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000257A File Offset: 0x0000077A
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00002582 File Offset: 0x00000782
		public ExecutionLogInfoEntity.ReportEventType EventType { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000258B File Offset: 0x0000078B
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00002593 File Offset: 0x00000793
		public string AdditionalInfo { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000259C File Offset: 0x0000079C
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000025A4 File Offset: 0x000007A4
		public ExecutionLogInfoEntity.RequestTypeOf RequestType { get; set; }

		// Token: 0x02000025 RID: 37
		public enum ReportEventType
		{
			// Token: 0x040000C7 RID: 199
			Render = 1,
			// Token: 0x040000C8 RID: 200
			BookmarkNavigation,
			// Token: 0x040000C9 RID: 201
			DocumentMapNavigation,
			// Token: 0x040000CA RID: 202
			DrillThrough,
			// Token: 0x040000CB RID: 203
			FindString,
			// Token: 0x040000CC RID: 204
			GetDocumentMap,
			// Token: 0x040000CD RID: 205
			Toggle,
			// Token: 0x040000CE RID: 206
			Sort,
			// Token: 0x040000CF RID: 207
			Execute,
			// Token: 0x040000D0 RID: 208
			RenderEdit,
			// Token: 0x040000D1 RID: 209
			ExecuteDataShapeQuery,
			// Token: 0x040000D2 RID: 210
			RenderMobileReport,
			// Token: 0x040000D3 RID: 211
			ConceptualSchema,
			// Token: 0x040000D4 RID: 212
			QueryData,
			// Token: 0x040000D5 RID: 213
			ASModelStream,
			// Token: 0x040000D6 RID: 214
			RenderExcelWorkbook,
			// Token: 0x040000D7 RID: 215
			GetExcelWorkbookInfo,
			// Token: 0x040000D8 RID: 216
			SaveToCatalog,
			// Token: 0x040000D9 RID: 217
			DataRefresh
		}

		// Token: 0x02000026 RID: 38
		public enum ExecutionLogExecType
		{
			// Token: 0x040000DB RID: 219
			Live = 1,
			// Token: 0x040000DC RID: 220
			Cache,
			// Token: 0x040000DD RID: 221
			Snapshot,
			// Token: 0x040000DE RID: 222
			History,
			// Token: 0x040000DF RID: 223
			AdHoc,
			// Token: 0x040000E0 RID: 224
			Session,
			// Token: 0x040000E1 RID: 225
			Rdce
		}

		// Token: 0x02000027 RID: 39
		public enum ErrorCode
		{
			// Token: 0x040000E3 RID: 227
			rsSuccess,
			// Token: 0x040000E4 RID: 228
			rsInternalError,
			// Token: 0x040000E5 RID: 229
			rsAccessDenied,
			// Token: 0x040000E6 RID: 230
			rsItemNotFound
		}

		// Token: 0x02000028 RID: 40
		public enum RequestTypeOf
		{
			// Token: 0x040000E8 RID: 232
			Interactive,
			// Token: 0x040000E9 RID: 233
			Subscription,
			// Token: 0x040000EA RID: 234
			RefreshCache
		}
	}
}
