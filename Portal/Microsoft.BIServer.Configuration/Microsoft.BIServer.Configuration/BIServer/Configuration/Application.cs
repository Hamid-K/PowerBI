using System;
using Microsoft.BIServer.Configuration.Exceptions;
using Microsoft.BIServer.Configuration.Http;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x0200002A RID: 42
	public sealed class Application : IEquatable<Application>
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00006448 File Offset: 0x00004648
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00006450 File Offset: 0x00004650
		public string Name { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00006459 File Offset: 0x00004659
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00006461 File Offset: 0x00004661
		public string VirtualDirectory { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000646A File Offset: 0x0000466A
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00006472 File Offset: 0x00004672
		public URL[] URLs { get; set; }

		// Token: 0x06000176 RID: 374 RVA: 0x0000647C File Offset: 0x0000467C
		public Application(string name, string virtualDirectory, params URL[] urls)
		{
			if (virtualDirectory == null)
			{
				throw new ConfigException.InvalidUrlReservation("VirtualDirectory is null.");
			}
			if (virtualDirectory.StartsWith("/"))
			{
				this.VirtualDirectory = virtualDirectory;
			}
			else
			{
				this.VirtualDirectory = "/" + virtualDirectory;
			}
			this.Name = name;
			this.URLs = urls;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00002083 File Offset: 0x00000283
		private Application()
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000064D2 File Offset: 0x000046D2
		public void Register(URL url)
		{
			UrlReservationManager.ReserveAndDeleteIfExists(url.UrlString + this.VirtualDirectory, url.AccountSecurityDescriptor);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000064F0 File Offset: 0x000046F0
		public void Unregister(URL url)
		{
			UrlReservationManager.DeleteIfExists(url.UrlString + this.VirtualDirectory, url.AccountSecurityDescriptor);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000650E File Offset: 0x0000470E
		public bool Equals(Application other)
		{
			return other != null && string.Equals(this.Name, other.Name, StringComparison.OrdinalIgnoreCase);
		}
	}
}
