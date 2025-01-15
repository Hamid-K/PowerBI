using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000076 RID: 118
	[DataContract]
	public sealed class ReleaseLabelRule
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000341 RID: 833 RVA: 0x000048FC File Offset: 0x00002AFC
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00004904 File Offset: 0x00002B04
		[DataMember(Name = "version")]
		public string Version { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000490D File Offset: 0x00002B0D
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00004915 File Offset: 0x00002B15
		[DataMember(Name = "label")]
		public string Label { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000491E File Offset: 0x00002B1E
		// (set) Token: 0x06000346 RID: 838 RVA: 0x00004926 File Offset: 0x00002B26
		[DataMember(Name = "updateNotificationDate")]
		public DateTime UpdateNotificationDate { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000492F File Offset: 0x00002B2F
		// (set) Token: 0x06000348 RID: 840 RVA: 0x00004937 File Offset: 0x00002B37
		[DataMember(Name = "expiryDate")]
		public DateTime ExpiryDate { get; set; }
	}
}
