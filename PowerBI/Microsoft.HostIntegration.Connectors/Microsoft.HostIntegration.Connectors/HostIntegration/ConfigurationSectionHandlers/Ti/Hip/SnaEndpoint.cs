using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x0200057D RID: 1405
	public class SnaEndpoint : ConfigurationElement
	{
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x000A30C1 File Offset: 0x000A12C1
		// (set) Token: 0x06002FD4 RID: 12244 RVA: 0x000A30C9 File Offset: 0x000A12C9
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

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000A30D2 File Offset: 0x000A12D2
		// (set) Token: 0x06002FD6 RID: 12246 RVA: 0x000A30E4 File Offset: 0x000A12E4
		[ConfigurationProperty("localLuName", IsRequired = true)]
		[Browsable(false)]
		[StringValidator(MaxLength = 8)]
		public string LocalLuName
		{
			get
			{
				return (string)base["localLuName"];
			}
			set
			{
				base["localLuName"] = value;
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x0009B260 File Offset: 0x00099460
		// (set) Token: 0x06002FD8 RID: 12248 RVA: 0x00017A96 File Offset: 0x00015C96
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

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06002FD9 RID: 12249 RVA: 0x0009B2F1 File Offset: 0x000994F1
		// (set) Token: 0x06002FDA RID: 12250 RVA: 0x0009B303 File Offset: 0x00099503
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

		// Token: 0x04001C3C RID: 7228
		private bool isTypeDefined;
	}
}
