using System;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x0200000E RID: 14
	public class InstallationInfo
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002A6C File Offset: 0x00000C6C
		public InstallationInfo(Guid installationId, string machineName, string instanceName, byte[] publicKey)
		{
			this._installationId = installationId;
			this._machineName = machineName;
			this._instanceName = instanceName;
			this._publicKey = publicKey;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002A91 File Offset: 0x00000C91
		public Guid InstallationId
		{
			get
			{
				return this._installationId;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002A99 File Offset: 0x00000C99
		public string MachineName
		{
			get
			{
				return this._machineName;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002AA1 File Offset: 0x00000CA1
		public string InstanceName
		{
			get
			{
				return this._instanceName;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002AA9 File Offset: 0x00000CA9
		public byte[] PublicKey
		{
			get
			{
				return this._publicKey;
			}
		}

		// Token: 0x0400004A RID: 74
		private readonly Guid _installationId;

		// Token: 0x0400004B RID: 75
		private readonly string _machineName;

		// Token: 0x0400004C RID: 76
		private readonly string _instanceName;

		// Token: 0x0400004D RID: 77
		private readonly byte[] _publicKey;
	}
}
