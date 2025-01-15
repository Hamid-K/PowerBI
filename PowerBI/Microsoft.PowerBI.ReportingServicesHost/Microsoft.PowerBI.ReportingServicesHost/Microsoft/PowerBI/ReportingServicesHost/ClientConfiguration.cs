using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000031 RID: 49
	public class ClientConfiguration
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x000040DD File Offset: 0x000022DD
		public ClientConfiguration()
		{
			this.ClientCapabilities = new Dictionary<string, string>();
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000040F0 File Offset: 0x000022F0
		// (set) Token: 0x060000FB RID: 251 RVA: 0x000040F8 File Offset: 0x000022F8
		public int Width { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004101 File Offset: 0x00002301
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00004109 File Offset: 0x00002309
		public int Height { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004112 File Offset: 0x00002312
		// (set) Token: 0x060000FF RID: 255 RVA: 0x0000411A File Offset: 0x0000231A
		public string UserAgent { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004123 File Offset: 0x00002323
		// (set) Token: 0x06000101 RID: 257 RVA: 0x0000412B File Offset: 0x0000232B
		public Dictionary<string, string> ClientCapabilities { get; private set; }
	}
}
