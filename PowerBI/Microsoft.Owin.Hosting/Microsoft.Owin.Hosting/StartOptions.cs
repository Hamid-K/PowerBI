using System;
using System.Collections.Generic;

namespace Microsoft.Owin.Hosting
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class StartOptions
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002C70 File Offset: 0x00000E70
		public StartOptions()
		{
			this.Urls = new List<string>();
			this.Settings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002C93 File Offset: 0x00000E93
		public StartOptions(string url)
			: this()
		{
			this.Urls.Add(url);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002CA7 File Offset: 0x00000EA7
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002CAF File Offset: 0x00000EAF
		public IList<string> Urls { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002CB8 File Offset: 0x00000EB8
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public int? Port { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002CC9 File Offset: 0x00000EC9
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002CD1 File Offset: 0x00000ED1
		public string AppStartup { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002CDA File Offset: 0x00000EDA
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002CE2 File Offset: 0x00000EE2
		public string ServerFactory { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002CEB File Offset: 0x00000EEB
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002CF3 File Offset: 0x00000EF3
		public IDictionary<string, string> Settings { get; private set; }
	}
}
