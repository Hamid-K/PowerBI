using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000D1 RID: 209
	internal class ClientSecurityProperties : ConfigurationElement
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x00015607 File Offset: 0x00013807
		internal ClientSecurityProperties()
		{
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00016833 File Offset: 0x00014A33
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x00016845 File Offset: 0x00014A45
		[ConfigurationProperty("mode", DefaultValue = DataCacheSecurityMode.Transport)]
		public DataCacheSecurityMode DataCacheSecurityMode
		{
			get
			{
				return (DataCacheSecurityMode)base["mode"];
			}
			set
			{
				base["mode"] = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00016858 File Offset: 0x00014A58
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x0001686A File Offset: 0x00014A6A
		[ConfigurationProperty("protectionLevel", DefaultValue = DataCacheProtectionLevel.EncryptAndSign)]
		public DataCacheProtectionLevel DataCacheProtectionLevel
		{
			get
			{
				return (DataCacheProtectionLevel)base["protectionLevel"];
			}
			set
			{
				base["protectionLevel"] = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00016902 File Offset: 0x00014B02
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x00016914 File Offset: 0x00014B14
		[ConfigurationProperty("sslEnabled", IsRequired = false, DefaultValue = false)]
		public bool SslEnabled
		{
			get
			{
				return (bool)base["sslEnabled"];
			}
			set
			{
				base["sslEnabled"] = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00017D25 File Offset: 0x00015F25
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x00017D37 File Offset: 0x00015F37
		[ConfigurationProperty("sslSubjectIdentity", IsRequired = false, DefaultValue = null)]
		public string SslSubjectIdentity
		{
			get
			{
				return (string)base["sslSubjectIdentity"];
			}
			set
			{
				base["sslSubjectIdentity"] = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00017D45 File Offset: 0x00015F45
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00017D57 File Offset: 0x00015F57
		[ConfigurationProperty("messageSecurity")]
		public MessageSecurityProperties MessageSecurity
		{
			get
			{
				return (MessageSecurityProperties)base["messageSecurity"];
			}
			set
			{
				base["messageSecurity"] = value;
			}
		}

		// Token: 0x040003C6 RID: 966
		internal const string MODE = "mode";

		// Token: 0x040003C7 RID: 967
		internal const string PROTECTION_LEVEL = "protectionLevel";

		// Token: 0x040003C8 RID: 968
		internal const string AUTH_INFORMATION = "messageSecurity";

		// Token: 0x040003C9 RID: 969
		internal const string SSL_ENABLED = "sslEnabled";

		// Token: 0x040003CA RID: 970
		internal const string SSL_SUBJECT_IDENTITY = "sslSubjectIdentity";
	}
}
