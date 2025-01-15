using System;
using System.Configuration;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000D3 RID: 211
	internal class DataCacheNamedClient : DataCacheClientSection
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string ClientName
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00017DCF File Offset: 0x00015FCF
		public DataCacheNamedClient()
		{
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00017DD7 File Offset: 0x00015FD7
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x00017DDF File Offset: 0x00015FDF
		public override ClientTraceSettings TraceSettings
		{
			get
			{
				return base.TraceSettings;
			}
			set
			{
				if (value != null)
				{
					throw new NotSupportedException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "InvalidNamedClientConfigProperty"));
				}
				base.TraceSettings = value;
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00017E00 File Offset: 0x00016000
		public DataCacheNamedClient(DataCacheClientSection client)
		{
			base.LocalCache = client.LocalCache;
			base.Notification = client.Notification;
			base.RequestTimeout = client.RequestTimeout;
			base.MaxConnectionsToServer = client.MaxConnectionsToServer;
			base.ChannelOpenTimeout = client.ChannelOpenTimeout;
			base.Hosts = client.Hosts;
			base.TransportProperties = client.TransportProperties;
			base.SecurityProperties = client.SecurityProperties;
			base.DataCacheServiceAccountType = client.DataCacheServiceAccountType;
			this.ClientName = "default";
			base.IsCompressionEnabled = client.IsCompressionEnabled;
			base.SerializationProperties = client.SerializationProperties;
			base.AutoDiscover = client.AutoDiscover;
			base.UseLegacyProtocol = client.UseLegacyProtocol;
			base.CacheReadyRetryPolicy = client.CacheReadyRetryPolicy;
		}

		// Token: 0x040003CD RID: 973
		internal const string CLIENTNAME = "name";
	}
}
