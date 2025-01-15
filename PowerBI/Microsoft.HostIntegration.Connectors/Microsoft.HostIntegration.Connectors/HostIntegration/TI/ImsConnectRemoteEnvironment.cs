using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200073D RID: 1853
	[DataContract]
	[Serializable]
	public class ImsConnectRemoteEnvironment : TcpWithCommonNameRemoteEnvironment
	{
		// Token: 0x06003A10 RID: 14864 RVA: 0x000C664B File Offset: 0x000C484B
		public ImsConnectRemoteEnvironment()
		{
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000C6660 File Offset: 0x000C4860
		public ImsConnectRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string address, string ports, bool useSsl, bool serverVerificationRequired, string certificateCommonName, string itocExitName, string imsSystemId, string mfsModName, bool useHWS01)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication, address, ports, useSsl, serverVerificationRequired, certificateCommonName)
		{
			this.ItocExitName = itocExitName;
			this.ImsSystemId = imsSystemId;
			this.MfsModName = mfsModName;
			this.UseHws01SecurityExit = useHWS01;
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06003A12 RID: 14866 RVA: 0x000C66B5 File Offset: 0x000C48B5
		// (set) Token: 0x06003A13 RID: 14867 RVA: 0x000C66BD File Offset: 0x000C48BD
		[DataMember]
		[Description("Name of the Itoc exit")]
		[DisplayName("ItocExitName *")]
		[Category("IMS")]
		public string ItocExitName
		{
			get
			{
				return this.itocExitName;
			}
			set
			{
				if (string.IsNullOrEmpty(value) || value.Length > 8)
				{
					throw new ArgumentNullException("ItocExitName");
				}
				this.itocExitName = value;
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06003A14 RID: 14868 RVA: 0x000C66E2 File Offset: 0x000C48E2
		// (set) Token: 0x06003A15 RID: 14869 RVA: 0x000C66EA File Offset: 0x000C48EA
		[DataMember]
		[Description("IMS system Id")]
		[DisplayName("IMS System Id *")]
		[Category("IMS")]
		public string ImsSystemId
		{
			get
			{
				return this.imsSystemId;
			}
			set
			{
				if (string.IsNullOrEmpty(value) || value.Length > 8)
				{
					throw new ArgumentNullException("ImsSystemId");
				}
				this.imsSystemId = value;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06003A16 RID: 14870 RVA: 0x000C670F File Offset: 0x000C490F
		// (set) Token: 0x06003A17 RID: 14871 RVA: 0x000C6717 File Offset: 0x000C4917
		[DataMember]
		[Description("Name of the IMS format mode")]
		[Category("IMS")]
		public string MfsModName { get; set; }

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06003A18 RID: 14872 RVA: 0x000C6720 File Offset: 0x000C4920
		// (set) Token: 0x06003A19 RID: 14873 RVA: 0x000C6728 File Offset: 0x000C4928
		[DataMember]
		[Category("Security")]
		public bool UseHws01SecurityExit { get; set; }

		// Token: 0x04002320 RID: 8992
		private string itocExitName = "*IRMREQ*";

		// Token: 0x04002321 RID: 8993
		private string imsSystemId;
	}
}
