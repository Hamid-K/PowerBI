using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000746 RID: 1862
	[DataContract]
	[Serializable]
	public class SnaLinkRemoteEnvironment : SnaBaseRemoteEnvironment
	{
		// Token: 0x06003B2A RID: 15146 RVA: 0x000C6EC1 File Offset: 0x000C50C1
		public SnaLinkRemoteEnvironment()
		{
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x000C6ECC File Offset: 0x000C50CC
		public SnaLinkRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string localLuName, string remoteLuName, string modeName, bool syncLevel2Supported, bool overrideSourceTP, string mirrorTranId, bool allowExplicitSyncPoint)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication, localLuName, remoteLuName, modeName, syncLevel2Supported)
		{
			this.MirrorTransactionId = mirrorTranId;
			this.AllowExplicitSyncPoint = allowExplicitSyncPoint;
			this.OverrideSnaSourceTp = overrideSourceTP;
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06003B2C RID: 15148 RVA: 0x000C6F0C File Offset: 0x000C510C
		// (set) Token: 0x06003B2D RID: 15149 RVA: 0x000C6F14 File Offset: 0x000C5114
		[DataMember]
		[Category("CICS")]
		public string MirrorTransactionId { get; set; }

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06003B2E RID: 15150 RVA: 0x000C6F1D File Offset: 0x000C511D
		// (set) Token: 0x06003B2F RID: 15151 RVA: 0x000C6F25 File Offset: 0x000C5125
		[DataMember]
		[DefaultValue(true)]
		[Description("Allow explicit SyncPoint commands in non-transactional calls")]
		[Category("SNA")]
		public bool AllowExplicitSyncPoint { get; set; }

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06003B30 RID: 15152 RVA: 0x000C6F2E File Offset: 0x000C512E
		// (set) Token: 0x06003B31 RID: 15153 RVA: 0x000C6F36 File Offset: 0x000C5136
		[DataMember]
		[Category("Security")]
		public bool OverrideSnaSourceTp { get; set; }
	}
}
