using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000064 RID: 100
	[DataContract]
	public class OAuthBrowserNavigationInfo
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00004540 File Offset: 0x00002740
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x00004548 File Offset: 0x00002748
		[DataMember]
		public Uri CallbackUri { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00004551 File Offset: 0x00002751
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00004559 File Offset: 0x00002759
		[DataMember]
		public Uri LogonUri { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00004562 File Offset: 0x00002762
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000456A File Offset: 0x0000276A
		[DataMember]
		public string SerializedContext { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00004573 File Offset: 0x00002773
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000457B File Offset: 0x0000277B
		[DataMember]
		public string State { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00004584 File Offset: 0x00002784
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000458C File Offset: 0x0000278C
		[DataMember]
		public int WindowsHeight { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00004595 File Offset: 0x00002795
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000459D File Offset: 0x0000279D
		[DataMember]
		public int WindowsWidth { get; set; }
	}
}
