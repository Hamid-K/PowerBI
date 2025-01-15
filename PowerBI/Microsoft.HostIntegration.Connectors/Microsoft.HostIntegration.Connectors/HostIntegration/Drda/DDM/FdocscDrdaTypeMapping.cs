using System;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008DC RID: 2268
	public class FdocscDrdaTypeMapping
	{
		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x060047CF RID: 18383 RVA: 0x00104CB4 File Offset: 0x00102EB4
		// (set) Token: 0x060047D0 RID: 18384 RVA: 0x00104CBC File Offset: 0x00102EBC
		public byte ReferencType { get; set; }

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x060047D1 RID: 18385 RVA: 0x00104CC5 File Offset: 0x00102EC5
		// (set) Token: 0x060047D2 RID: 18386 RVA: 0x00104CCD File Offset: 0x00102ECD
		public byte FromDrdaType { get; set; }

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x060047D3 RID: 18387 RVA: 0x00104CD6 File Offset: 0x00102ED6
		// (set) Token: 0x060047D4 RID: 18388 RVA: 0x00104CDE File Offset: 0x00102EDE
		public byte TripletLID { get; set; }

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x060047D5 RID: 18389 RVA: 0x00104CE7 File Offset: 0x00102EE7
		// (set) Token: 0x060047D6 RID: 18390 RVA: 0x00104CEF File Offset: 0x00102EEF
		public byte ToDrdaType { get; set; }

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x060047D7 RID: 18391 RVA: 0x00104CF8 File Offset: 0x00102EF8
		// (set) Token: 0x060047D8 RID: 18392 RVA: 0x00104D00 File Offset: 0x00102F00
		public short CCSID { get; set; }

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x060047D9 RID: 18393 RVA: 0x00104D09 File Offset: 0x00102F09
		// (set) Token: 0x060047DA RID: 18394 RVA: 0x00104D11 File Offset: 0x00102F11
		public byte CharSize { get; set; }

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x060047DB RID: 18395 RVA: 0x00104D1A File Offset: 0x00102F1A
		// (set) Token: 0x060047DC RID: 18396 RVA: 0x00104D22 File Offset: 0x00102F22
		public byte Mode { get; set; }

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x060047DD RID: 18397 RVA: 0x00104D2B File Offset: 0x00102F2B
		// (set) Token: 0x060047DE RID: 18398 RVA: 0x00104D33 File Offset: 0x00102F33
		public short Precision { get; set; }
	}
}
