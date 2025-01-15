using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x0200055A RID: 1370
	public class ElmUserData : ConfigurationElement
	{
		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002E43 RID: 11843 RVA: 0x0009B22F File Offset: 0x0009942F
		// (set) Token: 0x06002E44 RID: 11844 RVA: 0x0009B237 File Offset: 0x00099437
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

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002E45 RID: 11845 RVA: 0x0009B240 File Offset: 0x00099440
		// (set) Token: 0x06002E46 RID: 11846 RVA: 0x0009B252 File Offset: 0x00099452
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

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002E47 RID: 11847 RVA: 0x0009B260 File Offset: 0x00099460
		// (set) Token: 0x06002E48 RID: 11848 RVA: 0x00017A96 File Offset: 0x00015C96
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

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x0009B272 File Offset: 0x00099472
		// (set) Token: 0x06002E4A RID: 11850 RVA: 0x0009B284 File Offset: 0x00099484
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

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002E4B RID: 11851 RVA: 0x0009B292 File Offset: 0x00099492
		// (set) Token: 0x06002E4C RID: 11852 RVA: 0x0009B2C6 File Offset: 0x000994C6
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
				if (this.RequestHeaderFormat == "IbmSuppliedExitRoutine")
				{
					return TcpRequestHeaderFormat.IbmSuppliedExitRoutine;
				}
				throw new Exception("Invalid ELMUserData RequestHeaderFormat");
			}
			set
			{
				if (value == TcpRequestHeaderFormat.Microsoft)
				{
					this.RequestHeaderFormat = "Microsoft";
					return;
				}
				if (value == TcpRequestHeaderFormat.IbmSuppliedExitRoutine)
				{
					this.RequestHeaderFormat = "IbmSuppliedExitRoutine";
					return;
				}
				throw new Exception("Invalid ELMUserData RequestHeaderFormat");
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06002E4D RID: 11853 RVA: 0x0009B2F1 File Offset: 0x000994F1
		// (set) Token: 0x06002E4E RID: 11854 RVA: 0x0009B303 File Offset: 0x00099503
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

		// Token: 0x04001C11 RID: 7185
		private bool isTypeDefined;
	}
}
