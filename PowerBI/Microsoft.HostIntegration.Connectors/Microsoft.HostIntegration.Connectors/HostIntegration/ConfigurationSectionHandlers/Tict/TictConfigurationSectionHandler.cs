using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Tict
{
	// Token: 0x0200051D RID: 1309
	public sealed class TictConfigurationSectionHandler : HisConfigurationSectionHandler
	{
		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002C41 RID: 11329 RVA: 0x00096DEB File Offset: 0x00094FEB
		// (set) Token: 0x06002C42 RID: 11330 RVA: 0x00096DFD File Offset: 0x00094FFD
		[ConfigurationProperty("xmlns", IsRequired = true, DefaultValue = "http://schemas.microsoft.com/his/Tict/2013")]
		public string XmlNs
		{
			get
			{
				return (string)base["xmlns"];
			}
			set
			{
				base["xmlns"] = value;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06002C43 RID: 11331 RVA: 0x000974B7 File Offset: 0x000956B7
		// (set) Token: 0x06002C44 RID: 11332 RVA: 0x000974C9 File Offset: 0x000956C9
		[ConfigurationProperty("applicationServerCachingClientVersion", IsRequired = true)]
		public ApplicationServerCachingClientVersion ApplicationServerCachingClientVersion
		{
			get
			{
				return (ApplicationServerCachingClientVersion)base["applicationServerCachingClientVersion"];
			}
			set
			{
				base["applicationServerCachingClientVersion"] = value;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x000974D7 File Offset: 0x000956D7
		// (set) Token: 0x06002C46 RID: 11334 RVA: 0x000974E9 File Offset: 0x000956E9
		[ConfigurationProperty("applicationServerCachingCoreVersion", IsRequired = true)]
		public ApplicationServerCachingCoreVersion ApplicationServerCachingCoreVersion
		{
			get
			{
				return (ApplicationServerCachingCoreVersion)base["applicationServerCachingCoreVersion"];
			}
			set
			{
				base["applicationServerCachingCoreVersion"] = value;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002C47 RID: 11335 RVA: 0x000974F7 File Offset: 0x000956F7
		// (set) Token: 0x06002C48 RID: 11336 RVA: 0x00097509 File Offset: 0x00095709
		[ConfigurationProperty("windowsFabricCommonVersion", IsRequired = true)]
		public WindowsFabricCommonVersion WindowsFabricCommonVersion
		{
			get
			{
				return (WindowsFabricCommonVersion)base["windowsFabricCommonVersion"];
			}
			set
			{
				base["windowsFabricCommonVersion"] = value;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002C49 RID: 11337 RVA: 0x00097517 File Offset: 0x00095717
		// (set) Token: 0x06002C4A RID: 11338 RVA: 0x00097529 File Offset: 0x00095729
		[ConfigurationProperty("windowsFabricDataCommonVersion", IsRequired = true)]
		public WindowsFabricDataCommonVersion WindowsFabricDataCommonVersion
		{
			get
			{
				return (WindowsFabricDataCommonVersion)base["windowsFabricDataCommonVersion"];
			}
			set
			{
				base["windowsFabricDataCommonVersion"] = value;
			}
		}
	}
}
