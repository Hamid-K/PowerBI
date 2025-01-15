using System;

namespace Model
{
	// Token: 0x02000042 RID: 66
	public sealed class SubscriptionHistory
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00002DB1 File Offset: 0x00000FB1
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00002DB9 File Offset: 0x00000FB9
		public int Id { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00002DC2 File Offset: 0x00000FC2
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00002DCA File Offset: 0x00000FCA
		public Guid SubscriptionID { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00002DD3 File Offset: 0x00000FD3
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00002DDB File Offset: 0x00000FDB
		public SubscriptionExecutionType Type { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00002DE4 File Offset: 0x00000FE4
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00002DEC File Offset: 0x00000FEC
		public DateTime StartTime { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00002DF5 File Offset: 0x00000FF5
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00002DFD File Offset: 0x00000FFD
		public DateTime EndTime { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00002E06 File Offset: 0x00001006
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00002E0E File Offset: 0x0000100E
		public SubscriptionStatus Status { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00002E17 File Offset: 0x00001017
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00002E1F File Offset: 0x0000101F
		public string Message { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00002E28 File Offset: 0x00001028
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00002E30 File Offset: 0x00001030
		public string Details { get; set; }
	}
}
