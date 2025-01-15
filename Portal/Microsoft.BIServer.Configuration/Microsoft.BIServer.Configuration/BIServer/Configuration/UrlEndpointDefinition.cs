using System;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000026 RID: 38
	public class UrlEndpointDefinition
	{
		// Token: 0x0600013E RID: 318 RVA: 0x0000571E File Offset: 0x0000391E
		public UrlEndpointDefinition(AccountCredentials accountCredentials, string name, string virtualDirectory, string urlString, UrlEndpointUsage urlEndpointUsage)
		{
			this.AccountCredentials = accountCredentials;
			this.Name = name;
			this.VirtualDirectory = virtualDirectory;
			this.UrlString = urlString;
			this.UrlEndpointUsage = urlEndpointUsage;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000574B File Offset: 0x0000394B
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00005753 File Offset: 0x00003953
		public UrlEndpointUsage UrlEndpointUsage { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000575C File Offset: 0x0000395C
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00005764 File Offset: 0x00003964
		public AccountCredentials AccountCredentials { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000576D File Offset: 0x0000396D
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00005775 File Offset: 0x00003975
		public string Name { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000577E File Offset: 0x0000397E
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00005786 File Offset: 0x00003986
		public string VirtualDirectory { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000578F File Offset: 0x0000398F
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00005797 File Offset: 0x00003997
		public string UrlString { get; set; }
	}
}
