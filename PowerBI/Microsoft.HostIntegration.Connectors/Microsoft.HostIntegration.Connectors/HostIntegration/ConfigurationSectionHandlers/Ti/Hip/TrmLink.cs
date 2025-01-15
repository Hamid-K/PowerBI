using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000586 RID: 1414
	public class TrmLink : ConfigurationElement
	{
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x000A35D5 File Offset: 0x000A17D5
		// (set) Token: 0x06003041 RID: 12353 RVA: 0x000A35DD File Offset: 0x000A17DD
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

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x0009B240 File Offset: 0x00099440
		// (set) Token: 0x06003043 RID: 12355 RVA: 0x0009B252 File Offset: 0x00099452
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

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x0009B260 File Offset: 0x00099460
		// (set) Token: 0x06003045 RID: 12357 RVA: 0x00017A96 File Offset: 0x00015C96
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

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06003046 RID: 12358 RVA: 0x0009B272 File Offset: 0x00099472
		// (set) Token: 0x06003047 RID: 12359 RVA: 0x0009B284 File Offset: 0x00099484
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

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06003048 RID: 12360 RVA: 0x000A35E6 File Offset: 0x000A17E6
		// (set) Token: 0x06003049 RID: 12361 RVA: 0x000A3606 File Offset: 0x000A1806
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
				throw new Exception("TRMLink Request Header Format can only be Microsoft");
			}
			set
			{
				if (value == TcpRequestHeaderFormat.Microsoft)
				{
					this.RequestHeaderFormat = "Microsoft";
					return;
				}
				throw new Exception("TRMLink Request Header Format can only be Microsoft");
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x0600304A RID: 12362 RVA: 0x0009B2F1 File Offset: 0x000994F1
		// (set) Token: 0x0600304B RID: 12363 RVA: 0x0009B303 File Offset: 0x00099503
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

		// Token: 0x04001C41 RID: 7233
		private bool isTypeDefined;
	}
}
