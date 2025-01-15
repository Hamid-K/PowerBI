using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000582 RID: 1410
	public class SnaUserData : ConfigurationElement
	{
		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x0600300E RID: 12302 RVA: 0x000A339A File Offset: 0x000A159A
		// (set) Token: 0x0600300F RID: 12303 RVA: 0x000A33A2 File Offset: 0x000A15A2
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

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06003010 RID: 12304 RVA: 0x000A30D2 File Offset: 0x000A12D2
		// (set) Token: 0x06003011 RID: 12305 RVA: 0x000A30E4 File Offset: 0x000A12E4
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

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06003012 RID: 12306 RVA: 0x0009B260 File Offset: 0x00099460
		// (set) Token: 0x06003013 RID: 12307 RVA: 0x00017A96 File Offset: 0x00015C96
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

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06003014 RID: 12308 RVA: 0x0009B2F1 File Offset: 0x000994F1
		// (set) Token: 0x06003015 RID: 12309 RVA: 0x0009B303 File Offset: 0x00099503
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

		// Token: 0x04001C3F RID: 7231
		private bool isTypeDefined;
	}
}
