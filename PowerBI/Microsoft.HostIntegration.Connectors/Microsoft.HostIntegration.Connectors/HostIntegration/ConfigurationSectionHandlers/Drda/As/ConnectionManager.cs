using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000534 RID: 1332
	public class ConnectionManager : ConfigurationElement
	{
		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06002CFD RID: 11517 RVA: 0x0002038A File Offset: 0x0001E58A
		// (set) Token: 0x06002CFE RID: 11518 RVA: 0x0002039C File Offset: 0x0001E59C
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("type", IsRequired = true)]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06002CFF RID: 11519 RVA: 0x00097339 File Offset: 0x00095539
		// (set) Token: 0x06002D00 RID: 11520 RVA: 0x0009734B File Offset: 0x0009554B
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("port", IsRequired = false, DefaultValue = 446)]
		public int Port
		{
			get
			{
				return (int)base["port"];
			}
			set
			{
				base["port"] = value;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002D01 RID: 11521 RVA: 0x0009735E File Offset: 0x0009555E
		// (set) Token: 0x06002D02 RID: 11522 RVA: 0x00097370 File Offset: 0x00095570
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("useSsl", IsRequired = false, DefaultValue = false)]
		public bool UseSsl
		{
			get
			{
				return (bool)base["useSsl"];
			}
			set
			{
				base["useSsl"] = value;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002D03 RID: 11523 RVA: 0x00097FEB File Offset: 0x000961EB
		// (set) Token: 0x06002D04 RID: 11524 RVA: 0x00097FFD File Offset: 0x000961FD
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("sslCertificatePath", IsRequired = false)]
		public string SslCertificatePath
		{
			get
			{
				return (string)base["sslCertificatePath"];
			}
			set
			{
				base["sslCertificatePath"] = value;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06002D05 RID: 11525 RVA: 0x0009800B File Offset: 0x0009620B
		// (set) Token: 0x06002D06 RID: 11526 RVA: 0x0009801D File Offset: 0x0009621D
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("sslCertificateSerialNumber", IsRequired = false)]
		public string SslCertificateSerialNumber
		{
			get
			{
				return (string)base["sslCertificateSerialNumber"];
			}
			set
			{
				base["sslCertificateSerialNumber"] = value;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06002D07 RID: 11527 RVA: 0x0009802B File Offset: 0x0009622B
		// (set) Token: 0x06002D08 RID: 11528 RVA: 0x0009803D File Offset: 0x0009623D
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("sslClientCertificateRequired", IsRequired = false)]
		public bool SslClientCertificateRequired
		{
			get
			{
				return (bool)base["sslClientCertificateRequired"];
			}
			set
			{
				base["sslClientCertificateRequired"] = value;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002D09 RID: 11529 RVA: 0x00098050 File Offset: 0x00096250
		// (set) Token: 0x06002D0A RID: 11530 RVA: 0x00098062 File Offset: 0x00096262
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("certificateDnsName", IsRequired = false)]
		public string CertificateDnsName
		{
			get
			{
				return (string)base["certificateDnsName"];
			}
			set
			{
				base["certificateDnsName"] = value;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002D0B RID: 11531 RVA: 0x00098070 File Offset: 0x00096270
		// (set) Token: 0x06002D0C RID: 11532 RVA: 0x00098082 File Offset: 0x00096282
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("certificateFriendlyName", IsRequired = false)]
		public string CertificateFriendlyName
		{
			get
			{
				return (string)base["certificateFriendlyName"];
			}
			set
			{
				base["certificateFriendlyName"] = value;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x00098090 File Offset: 0x00096290
		// (set) Token: 0x06002D0E RID: 11534 RVA: 0x000980A2 File Offset: 0x000962A2
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("partnerServers", IsRequired = false)]
		public string PartnerServers
		{
			get
			{
				return (string)base["partnerServers"];
			}
			set
			{
				base["partnerServers"] = value;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06002D0F RID: 11535 RVA: 0x000980B0 File Offset: 0x000962B0
		// (set) Token: 0x06002D10 RID: 11536 RVA: 0x000980C2 File Offset: 0x000962C2
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("isPrimary", IsRequired = false, DefaultValue = true)]
		public bool IsPrimary
		{
			get
			{
				return (bool)base["isPrimary"];
			}
			set
			{
				base["isPrimary"] = value;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06002D11 RID: 11537 RVA: 0x000980D5 File Offset: 0x000962D5
		// (set) Token: 0x06002D12 RID: 11538 RVA: 0x000980E7 File Offset: 0x000962E7
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("pingInterval", IsRequired = false, DefaultValue = 10000)]
		public int PingInterval
		{
			get
			{
				return (int)base["pingInterval"];
			}
			set
			{
				base["pingInterval"] = value;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002D13 RID: 11539 RVA: 0x000980FA File Offset: 0x000962FA
		// (set) Token: 0x06002D14 RID: 11540 RVA: 0x0009810C File Offset: 0x0009630C
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("performanceCountersOn", IsRequired = false, DefaultValue = false)]
		public bool PerformanceCountersOn
		{
			get
			{
				return (bool)base["performanceCountersOn"];
			}
			set
			{
				base["performanceCountersOn"] = value;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002D15 RID: 11541 RVA: 0x0009811F File Offset: 0x0009631F
		// (set) Token: 0x06002D16 RID: 11542 RVA: 0x00098131 File Offset: 0x00096331
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("clientIpAddressesAllowed", IsRequired = false)]
		public string ClientIpAddressesAllowed
		{
			get
			{
				return (string)base["clientIpAddressesAllowed"];
			}
			set
			{
				base["clientIpAddressesAllowed"] = value;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002D17 RID: 11543 RVA: 0x0009813F File Offset: 0x0009633F
		// (set) Token: 0x06002D18 RID: 11544 RVA: 0x00098151 File Offset: 0x00096351
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("resyncIpAddress", IsRequired = false)]
		public string ResyncIpAddress
		{
			get
			{
				return (string)base["resyncIpAddress"];
			}
			set
			{
				base["resyncIpAddress"] = value;
			}
		}
	}
}
