using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AFB RID: 2811
	public class SegmentHeaderInformation
	{
		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x06005911 RID: 22801 RVA: 0x0017003B File Offset: 0x0016E23B
		// (set) Token: 0x06005912 RID: 22802 RVA: 0x00170043 File Offset: 0x0016E243
		public SegmentHeaderType SegmentHeaderType { get; set; }

		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x06005913 RID: 22803 RVA: 0x0017004C File Offset: 0x0016E24C
		// (set) Token: 0x06005914 RID: 22804 RVA: 0x00170054 File Offset: 0x0016E254
		public int LengthOfSegmentHeader { get; set; }

		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x06005915 RID: 22805 RVA: 0x0017005D File Offset: 0x0016E25D
		// (set) Token: 0x06005916 RID: 22806 RVA: 0x00170065 File Offset: 0x0016E265
		public int ConversationId { get; set; }

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x06005917 RID: 22807 RVA: 0x0017006E File Offset: 0x0016E26E
		// (set) Token: 0x06005918 RID: 22808 RVA: 0x00170076 File Offset: 0x0016E276
		public int RequestId { get; set; }

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x06005919 RID: 22809 RVA: 0x0017007F File Offset: 0x0016E27F
		// (set) Token: 0x0600591A RID: 22810 RVA: 0x00170087 File Offset: 0x0016E287
		public bool LittleEndian { get; set; }

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x0600591B RID: 22811 RVA: 0x00170090 File Offset: 0x0016E290
		// (set) Token: 0x0600591C RID: 22812 RVA: 0x00170098 File Offset: 0x0016E298
		public SegmentType SegmentType { get; set; }

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x0600591D RID: 22813 RVA: 0x001700A1 File Offset: 0x0016E2A1
		// (set) Token: 0x0600591E RID: 22814 RVA: 0x001700A9 File Offset: 0x0016E2A9
		public ControlFlag1 ControlFlag1 { get; set; }

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x0600591F RID: 22815 RVA: 0x001700B2 File Offset: 0x0016E2B2
		// (set) Token: 0x06005920 RID: 22816 RVA: 0x001700BA File Offset: 0x0016E2BA
		public ControlFlag2 ControlFlag2 { get; set; }

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x06005921 RID: 22817 RVA: 0x001700C3 File Offset: 0x0016E2C3
		// (set) Token: 0x06005922 RID: 22818 RVA: 0x001700CB File Offset: 0x0016E2CB
		public int NumericEncoding { get; set; }

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x06005923 RID: 22819 RVA: 0x001700D4 File Offset: 0x0016E2D4
		// (set) Token: 0x06005924 RID: 22820 RVA: 0x001700DC File Offset: 0x0016E2DC
		public short Ccsid { get; set; }

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x06005925 RID: 22821 RVA: 0x001700E5 File Offset: 0x0016E2E5
		// (set) Token: 0x06005926 RID: 22822 RVA: 0x001700ED File Offset: 0x0016E2ED
		public int LengthOfRestOfSegment { get; set; }

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x06005927 RID: 22823 RVA: 0x001700F6 File Offset: 0x0016E2F6
		// (set) Token: 0x06005928 RID: 22824 RVA: 0x001700FE File Offset: 0x0016E2FE
		public int TotalLengthOfSegment { get; set; }
	}
}
