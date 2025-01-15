using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002CC RID: 716
	internal class RemoteEndpoint
	{
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x00050788 File Offset: 0x0004E988
		// (set) Token: 0x06001A90 RID: 6800 RVA: 0x00050790 File Offset: 0x0004E990
		public string HostAddress { get; private set; }

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x00050799 File Offset: 0x0004E999
		// (set) Token: 0x06001A92 RID: 6802 RVA: 0x000507A1 File Offset: 0x0004E9A1
		public int Port { get; private set; }

		// Token: 0x06001A93 RID: 6803 RVA: 0x000507AA File Offset: 0x0004E9AA
		public RemoteEndpoint(string address, int port)
		{
			this.HostAddress = address;
			this.Port = port;
		}
	}
}
