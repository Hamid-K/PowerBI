using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000052 RID: 82
	[DataContract]
	public sealed class GatewayReleaseVersion
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00004253 File Offset: 0x00002453
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000425B File Offset: 0x0000245B
		[DataMember(Name = "minimumSupportedVersion")]
		public string MinimumSupportedVersion { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00004264 File Offset: 0x00002464
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000426C File Offset: 0x0000246C
		[DataMember(Name = "latestVersion")]
		public string LatestVersion { get; set; }
	}
}
