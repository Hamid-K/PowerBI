using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000581 RID: 1409
	public class SnaLink : ConfigurationElement
	{
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06003005 RID: 12293 RVA: 0x000A3389 File Offset: 0x000A1589
		// (set) Token: 0x06003006 RID: 12294 RVA: 0x000A3391 File Offset: 0x000A1591
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

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06003007 RID: 12295 RVA: 0x000A30D2 File Offset: 0x000A12D2
		// (set) Token: 0x06003008 RID: 12296 RVA: 0x000A30E4 File Offset: 0x000A12E4
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

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06003009 RID: 12297 RVA: 0x0009B260 File Offset: 0x00099460
		// (set) Token: 0x0600300A RID: 12298 RVA: 0x00017A96 File Offset: 0x00015C96
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

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x0600300B RID: 12299 RVA: 0x0009B2F1 File Offset: 0x000994F1
		// (set) Token: 0x0600300C RID: 12300 RVA: 0x0009B303 File Offset: 0x00099503
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

		// Token: 0x04001C3E RID: 7230
		private bool isTypeDefined;
	}
}
