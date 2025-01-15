using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A82 RID: 2690
	public class AsyncMessageInformation
	{
		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x0600539B RID: 21403 RVA: 0x001552BC File Offset: 0x001534BC
		// (set) Token: 0x0600539C RID: 21404 RVA: 0x001552C4 File Offset: 0x001534C4
		public int ObjectHandle { get; set; }

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x0600539D RID: 21405 RVA: 0x001552CD File Offset: 0x001534CD
		// (set) Token: 0x0600539E RID: 21406 RVA: 0x001552D5 File Offset: 0x001534D5
		public int SegmentLength { get; set; }

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x0600539F RID: 21407 RVA: 0x001552DE File Offset: 0x001534DE
		// (set) Token: 0x060053A0 RID: 21408 RVA: 0x001552E6 File Offset: 0x001534E6
		public AsyncMessageReasonCode ReasonCode { get; set; }

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x060053A1 RID: 21409 RVA: 0x001552EF File Offset: 0x001534EF
		// (set) Token: 0x060053A2 RID: 21410 RVA: 0x001552F7 File Offset: 0x001534F7
		public int TruncatedLength { get; set; }

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x060053A3 RID: 21411 RVA: 0x00155300 File Offset: 0x00153500
		// (set) Token: 0x060053A4 RID: 21412 RVA: 0x00155308 File Offset: 0x00153508
		public int OriginalMessageLength { get; set; }

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x060053A5 RID: 21413 RVA: 0x00155311 File Offset: 0x00153511
		// (set) Token: 0x060053A6 RID: 21414 RVA: 0x00155319 File Offset: 0x00153519
		public int Length { get; set; }
	}
}
