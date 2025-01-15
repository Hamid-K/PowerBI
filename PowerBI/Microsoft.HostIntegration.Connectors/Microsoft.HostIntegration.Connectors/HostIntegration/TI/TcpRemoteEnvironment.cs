using System;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200074C RID: 1868
	[DataContract]
	[Serializable]
	public class TcpRemoteEnvironment : TcpBaseRemoteEnvironment
	{
		// Token: 0x06003B4A RID: 15178 RVA: 0x000C7295 File Offset: 0x000C5495
		public TcpRemoteEnvironment()
		{
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x000C72A0 File Offset: 0x000C54A0
		public TcpRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, string address, string ports)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, address, ports)
		{
		}
	}
}
