using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000105 RID: 261
	internal class RoutingInfo
	{
		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x0005E377 File Offset: 0x0005C577
		// (set) Token: 0x0600156A RID: 5482 RVA: 0x0005E37F File Offset: 0x0005C57F
		internal byte Protocol { get; private set; }

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x0005E388 File Offset: 0x0005C588
		// (set) Token: 0x0600156C RID: 5484 RVA: 0x0005E390 File Offset: 0x0005C590
		internal ushort Port { get; private set; }

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x0005E399 File Offset: 0x0005C599
		// (set) Token: 0x0600156E RID: 5486 RVA: 0x0005E3A1 File Offset: 0x0005C5A1
		internal string ServerName { get; private set; }

		// Token: 0x0600156F RID: 5487 RVA: 0x0005E3AA File Offset: 0x0005C5AA
		internal RoutingInfo(byte protocol, ushort port, string servername)
		{
			this.Protocol = protocol;
			this.Port = port;
			this.ServerName = servername;
		}
	}
}
