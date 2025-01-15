using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000714 RID: 1812
	[DataContract]
	[Serializable]
	public class SnaBaseRemoteEnvironment : BaseWithSsoRemoteEnvironment
	{
		// Token: 0x0600397B RID: 14715 RVA: 0x000C5B04 File Offset: 0x000C3D04
		public SnaBaseRemoteEnvironment()
		{
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x000C5B0C File Offset: 0x000C3D0C
		protected SnaBaseRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string localLuName, string remoteLuName, string modeName, bool syncLevel2Supported)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication)
		{
			this.LocalLuName = localLuName;
			this.RemoteLuName = remoteLuName;
			this.ModeName = modeName;
			this.SyncLevel2Supported = syncLevel2Supported;
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x0600397D RID: 14717 RVA: 0x000C5B4C File Offset: 0x000C3D4C
		// (set) Token: 0x0600397E RID: 14718 RVA: 0x000C5B54 File Offset: 0x000C3D54
		[DataMember]
		[Description("Name of the local LU")]
		[DisplayName("LocalLuName *")]
		[Category("SNA")]
		public string LocalLuName { get; set; }

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x0600397F RID: 14719 RVA: 0x000C5B5D File Offset: 0x000C3D5D
		// (set) Token: 0x06003980 RID: 14720 RVA: 0x000C5B65 File Offset: 0x000C3D65
		[DataMember]
		[Description("Name of the remote LU")]
		[DisplayName("RemoteLuName *")]
		[Category("SNA")]
		public string RemoteLuName { get; set; }

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06003981 RID: 14721 RVA: 0x000C5B6E File Offset: 0x000C3D6E
		// (set) Token: 0x06003982 RID: 14722 RVA: 0x000C5B76 File Offset: 0x000C3D76
		[DataMember]
		[Description("Name of the mode")]
		[DisplayName("ModeName *")]
		[Category("SNA")]
		public string ModeName { get; set; }

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06003983 RID: 14723 RVA: 0x000C5B7F File Offset: 0x000C3D7F
		// (set) Token: 0x06003984 RID: 14724 RVA: 0x000C5B87 File Offset: 0x000C3D87
		[DataMember]
		[Description("Whether Sync Level2 is supported.")]
		[Browsable(false)]
		public bool SyncLevel2Supported { get; set; }
	}
}
