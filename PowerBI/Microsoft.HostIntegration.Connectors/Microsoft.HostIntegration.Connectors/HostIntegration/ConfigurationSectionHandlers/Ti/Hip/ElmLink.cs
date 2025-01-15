using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x0200055B RID: 1371
	public class ElmLink : ConfigurationElement
	{
		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06002E50 RID: 11856 RVA: 0x0009B311 File Offset: 0x00099511
		// (set) Token: 0x06002E51 RID: 11857 RVA: 0x0009B319 File Offset: 0x00099519
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

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002E52 RID: 11858 RVA: 0x0009B240 File Offset: 0x00099440
		// (set) Token: 0x06002E53 RID: 11859 RVA: 0x0009B252 File Offset: 0x00099452
		[ConfigurationProperty("ports", IsRequired = true)]
		[Browsable(false)]
		public string Ports
		{
			get
			{
				return (string)base["ports"];
			}
			set
			{
				base["ports"] = value;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002E54 RID: 11860 RVA: 0x0009B260 File Offset: 0x00099460
		// (set) Token: 0x06002E55 RID: 11861 RVA: 0x00017A96 File Offset: 0x00015C96
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

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002E56 RID: 11862 RVA: 0x0009B272 File Offset: 0x00099472
		// (set) Token: 0x06002E57 RID: 11863 RVA: 0x0009B284 File Offset: 0x00099484
		[ConfigurationProperty("requestHeaderFormat", IsRequired = true, DefaultValue = "Microsoft")]
		[Browsable(false)]
		public string RequestHeaderFormat
		{
			get
			{
				return (string)base["requestHeaderFormat"];
			}
			set
			{
				base["requestHeaderFormat"] = value;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x0009B322 File Offset: 0x00099522
		// (set) Token: 0x06002E59 RID: 11865 RVA: 0x0009B342 File Offset: 0x00099542
		[ConfigurationProperty("requestHeaderFormatEnum", IsRequired = false)]
		[Browsable(false)]
		public TcpRequestHeaderFormat RequestHeaderFormatEnum
		{
			get
			{
				if (this.RequestHeaderFormat == "Microsoft")
				{
					return TcpRequestHeaderFormat.Microsoft;
				}
				throw new Exception("ELMLink Request Header Format can only be Microsoft");
			}
			set
			{
				if (value == TcpRequestHeaderFormat.Microsoft)
				{
					this.RequestHeaderFormat = "Microsoft";
					return;
				}
				throw new Exception("ELMLink Request Header Format can only be Microsoft");
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002E5A RID: 11866 RVA: 0x0009B2F1 File Offset: 0x000994F1
		// (set) Token: 0x06002E5B RID: 11867 RVA: 0x0009B303 File Offset: 0x00099503
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

		// Token: 0x04001C12 RID: 7186
		private bool isTypeDefined;
	}
}
