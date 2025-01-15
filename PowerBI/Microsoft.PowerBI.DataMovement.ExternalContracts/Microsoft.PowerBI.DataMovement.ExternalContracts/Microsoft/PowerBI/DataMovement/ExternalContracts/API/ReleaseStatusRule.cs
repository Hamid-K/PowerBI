using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000077 RID: 119
	[DataContract]
	public sealed class ReleaseStatusRule
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00004948 File Offset: 0x00002B48
		// (set) Token: 0x0600034B RID: 843 RVA: 0x00004950 File Offset: 0x00002B50
		[DataMember(Name = "version")]
		public string Version { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00004959 File Offset: 0x00002B59
		// (set) Token: 0x0600034D RID: 845 RVA: 0x00004961 File Offset: 0x00002B61
		[DataMember(Name = "status")]
		public GatewayVersionStatus? Status { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000496A File Offset: 0x00002B6A
		// (set) Token: 0x0600034F RID: 847 RVA: 0x00004972 File Offset: 0x00002B72
		[DataMember(Name = "expiryDate")]
		public DateTime? ExpiryDate { get; set; }
	}
}
