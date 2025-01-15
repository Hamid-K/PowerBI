using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000565 RID: 1381
	public class Http : ConfigurationElement
	{
		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06002EC7 RID: 11975 RVA: 0x000A15B8 File Offset: 0x0009F7B8
		// (set) Token: 0x06002EC8 RID: 11976 RVA: 0x000A15C0 File Offset: 0x0009F7C0
		public bool IsTypeDefined
		{
			get
			{
				return this.isTypeDefined;
			}
			set
			{
				this.isTypeDefined = value;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06002EC9 RID: 11977 RVA: 0x0009B260 File Offset: 0x00099460
		// (set) Token: 0x06002ECA RID: 11978 RVA: 0x00017A96 File Offset: 0x00015C96
		[ConfigurationProperty("hosts", IsRequired = true)]
		[Browsable(false)]
		public string Hosts
		{
			get
			{
				return (string)base["hosts"];
			}
			set
			{
				base["hosts"] = value;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x000A15C9 File Offset: 0x0009F7C9
		// (set) Token: 0x06002ECC RID: 11980 RVA: 0x000A15DB File Offset: 0x0009F7DB
		[ConfigurationProperty("endpoints")]
		[Browsable(false)]
		public HttpEndpointCollection Endpoints
		{
			get
			{
				return (HttpEndpointCollection)base["endpoints"];
			}
			set
			{
				base["endpoints"] = value;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x0009B2F1 File Offset: 0x000994F1
		// (set) Token: 0x06002ECE RID: 11982 RVA: 0x0009B303 File Offset: 0x00099503
		[ConfigurationProperty("resolutionEntries")]
		[Browsable(false)]
		public ResolutionEntryCollection ResolutionEntries
		{
			get
			{
				return (ResolutionEntryCollection)base["resolutionEntries"];
			}
			set
			{
				base["resolutionEntries"] = value;
			}
		}

		// Token: 0x04001C1E RID: 7198
		private bool isTypeDefined;
	}
}
