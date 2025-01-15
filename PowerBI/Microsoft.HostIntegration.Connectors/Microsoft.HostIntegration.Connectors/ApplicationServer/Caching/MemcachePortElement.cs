using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200012F RID: 303
	internal sealed class MemcachePortElement : ConfigurationElement
	{
		// Token: 0x060008CF RID: 2255 RVA: 0x0001F2CA File Offset: 0x0001D4CA
		public MemcachePortElement()
			: this(string.Empty, 0)
		{
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001F2D8 File Offset: 0x0001D4D8
		public MemcachePortElement(string cacheName, int memcachePort)
		{
			this.CacheName = cacheName;
			this.MemcacheSocketPort = memcachePort;
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[StringValidator(MaxLength = 255)]
		[ConfigurationProperty("name", IsKey = true, IsRequired = true)]
		public string CacheName
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0001F2EE File Offset: 0x0001D4EE
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x0001F300 File Offset: 0x0001D500
		[ConfigurationProperty("memcachePort", DefaultValue = 0, IsRequired = true)]
		public int MemcacheSocketPort
		{
			get
			{
				return (int)base["memcachePort"];
			}
			set
			{
				base["memcachePort"] = value;
			}
		}
	}
}
