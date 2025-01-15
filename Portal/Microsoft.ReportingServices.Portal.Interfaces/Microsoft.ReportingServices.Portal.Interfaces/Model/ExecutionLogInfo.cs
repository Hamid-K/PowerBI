using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Model
{
	// Token: 0x02000036 RID: 54
	public sealed class ExecutionLogInfo
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00002B09 File Offset: 0x00000D09
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00002B11 File Offset: 0x00000D11
		[Key]
		public long LogEntryId { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00002B1A File Offset: 0x00000D1A
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00002B22 File Offset: 0x00000D22
		public string CatalogItemId { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00002B2B File Offset: 0x00000D2B
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00002B33 File Offset: 0x00000D33
		public string ItemPath { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00002B3C File Offset: 0x00000D3C
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00002B44 File Offset: 0x00000D44
		public DateTime StartTime { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00002B4D File Offset: 0x00000D4D
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00002B55 File Offset: 0x00000D55
		public DateTime EndTime { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00002B5E File Offset: 0x00000D5E
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00002B66 File Offset: 0x00000D66
		public string Format { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00002B6F File Offset: 0x00000D6F
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00002B77 File Offset: 0x00000D77
		public string Parameters { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00002B80 File Offset: 0x00000D80
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00002B88 File Offset: 0x00000D88
		[JsonConverter(typeof(StringEnumConverter))]
		public ExecutionLogExecType Source { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00002B91 File Offset: 0x00000D91
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00002B99 File Offset: 0x00000D99
		[JsonConverter(typeof(StringEnumConverter))]
		public ExecutionLogLevel ExecutionLogLevel { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00002BA2 File Offset: 0x00000DA2
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00002BAA File Offset: 0x00000DAA
		public long Status { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00002BB3 File Offset: 0x00000DB3
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00002BBB File Offset: 0x00000DBB
		public long ByteCount { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00002BC4 File Offset: 0x00000DC4
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00002BCC File Offset: 0x00000DCC
		public long RowCount { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00002BD5 File Offset: 0x00000DD5
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00002BDD File Offset: 0x00000DDD
		public int ProcessingTime { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00002BE6 File Offset: 0x00000DE6
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00002BEE File Offset: 0x00000DEE
		public int RenderingTime { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00002BF7 File Offset: 0x00000DF7
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00002BFF File Offset: 0x00000DFF
		public int DataRetrievalTime { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00002C08 File Offset: 0x00000E08
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00002C10 File Offset: 0x00000E10
		public string ExecutionId { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00002C19 File Offset: 0x00000E19
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00002C21 File Offset: 0x00000E21
		[JsonConverter(typeof(StringEnumConverter))]
		public ExecutionLogEventType EventType { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00002C2A File Offset: 0x00000E2A
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00002C32 File Offset: 0x00000E32
		public string AdditionalInfo { get; set; }
	}
}
