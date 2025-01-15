using System;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000747 RID: 1863
	[DataContract]
	[Serializable]
	public class SnaUserDataRemoteEnvironment : SnaBaseRemoteEnvironment
	{
		// Token: 0x06003B32 RID: 15154 RVA: 0x000C6EC1 File Offset: 0x000C50C1
		public SnaUserDataRemoteEnvironment()
		{
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x000C6F40 File Offset: 0x000C5140
		public SnaUserDataRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string localLuName, string remoteLuName, string modeName, bool syncLevel2Supported)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication, localLuName, remoteLuName, modeName, syncLevel2Supported)
		{
		}
	}
}
