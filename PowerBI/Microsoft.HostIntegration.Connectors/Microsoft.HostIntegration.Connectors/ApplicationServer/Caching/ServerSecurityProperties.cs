using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000C0 RID: 192
	[Serializable]
	internal class ServerSecurityProperties : ConfigurationElement, ISerializable
	{
		// Token: 0x060004E0 RID: 1248 RVA: 0x00015607 File Offset: 0x00013807
		internal ServerSecurityProperties()
		{
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001682B File Offset: 0x00014A2B
		internal DataCacheSecurity GetDataCacheSecurity()
		{
			return new DataCacheSecurity(this);
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00016833 File Offset: 0x00014A33
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x00016845 File Offset: 0x00014A45
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00016858 File Offset: 0x00014A58
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x0001686A File Offset: 0x00014A6A
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0001687D File Offset: 0x00014A7D
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x0001688F File Offset: 0x00014A8F
		[ConfigurationCollection(typeof(AuthorizationElement), AddItemName = "allow")]
		[ConfigurationProperty("authorization", IsDefaultCollection = false, IsRequired = false)]
		public AuthorizationElement Authorization
		{
			get
			{
				return (AuthorizationElement)base["authorization"];
			}
			set
			{
				base["authorization"] = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0001689D File Offset: 0x00014A9D
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x000168AF File Offset: 0x00014AAF
		[ConfigurationProperty("serverAcs", IsDefaultCollection = false, IsRequired = false)]
		public ServerAcsSecurityElement AcsSecurity
		{
			get
			{
				return (ServerAcsSecurityElement)base["serverAcs"];
			}
			set
			{
				base["serverAcs"] = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x000168BD File Offset: 0x00014ABD
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x000168CF File Offset: 0x00014ACF
		[ConfigurationProperty("useAcsForClient", DefaultValue = false)]
		public bool UseAcsForClient
		{
			get
			{
				return (bool)base["useAcsForClient"];
			}
			set
			{
				base["useAcsForClient"] = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x000168E2 File Offset: 0x00014AE2
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x000168F4 File Offset: 0x00014AF4
		[ConfigurationProperty("sharedKeyAuth", IsRequired = false)]
		public SharedKeyAuthorization SharedKeyAuth
		{
			get
			{
				return (SharedKeyAuthorization)base["sharedKeyAuth"];
			}
			set
			{
				base["sharedKeyAuth"] = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00016902 File Offset: 0x00014B02
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x00016914 File Offset: 0x00014B14
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

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00016927 File Offset: 0x00014B27
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x00016939 File Offset: 0x00014B39
		[ConfigurationProperty("sslProperties", IsRequired = false)]
		public SslProperties SslProperties
		{
			get
			{
				return (SslProperties)base["sslProperties"];
			}
			set
			{
				base["sslProperties"] = value;
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00016948 File Offset: 0x00014B48
		protected ServerSecurityProperties(SerializationInfo info, StreamingContext context)
		{
			this.DataCacheSecurityMode = (DataCacheSecurityMode)info.GetValue("mode", typeof(DataCacheSecurityMode));
			this.DataCacheProtectionLevel = (DataCacheProtectionLevel)info.GetValue("protectionLevel", typeof(DataCacheProtectionLevel));
			this.Authorization = (AuthorizationElement)info.GetValue("authorization", typeof(AuthorizationElement));
			try
			{
				this.AcsSecurity = (ServerAcsSecurityElement)info.GetValue("serverAcs", typeof(ServerAcsSecurityElement));
				this.UseAcsForClient = (bool)info.GetValue("useAcsForClient", typeof(bool));
				this.SharedKeyAuth = (SharedKeyAuthorization)info.GetValue("sharedKeyAuth", typeof(SharedKeyAuthorization));
			}
			catch (SerializationException)
			{
				this.UseAcsForClient = false;
				this.SharedKeyAuth = new SharedKeyAuthorization();
			}
			try
			{
				this.SslProperties = (SslProperties)info.GetValue("sslProperties", typeof(SslProperties));
				this.SslEnabled = info.GetBoolean("sslEnabled");
			}
			catch (SerializationException)
			{
				this.SslEnabled = false;
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00016A8C File Offset: 0x00014C8C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("mode", this.DataCacheSecurityMode);
			info.AddValue("protectionLevel", this.DataCacheProtectionLevel);
			info.AddValue("authorization", this.Authorization);
			info.AddValue("serverAcs", this.AcsSecurity);
			info.AddValue("useAcsForClient", this.UseAcsForClient);
			info.AddValue("sharedKeyAuth", this.SharedKeyAuth);
			info.AddValue("sslEnabled", this.SslEnabled);
			if (this.SslEnabled)
			{
				info.AddValue("sslProperties", this.SslProperties);
			}
		}

		// Token: 0x0400037F RID: 895
		internal const string MODE = "mode";

		// Token: 0x04000380 RID: 896
		internal const string PROTECTION_LEVEL = "protectionLevel";

		// Token: 0x04000381 RID: 897
		internal const string AUTHORIZATION = "authorization";

		// Token: 0x04000382 RID: 898
		internal const string ALLOW = "allow";

		// Token: 0x04000383 RID: 899
		internal const string SERVER_ACS_PROPERTIES = "serverAcs";

		// Token: 0x04000384 RID: 900
		internal const string USE_ACS_FOR_CLIENT = "useAcsForClient";

		// Token: 0x04000385 RID: 901
		internal const string SHARED_KEY_AUTH = "sharedKeyAuth";

		// Token: 0x04000386 RID: 902
		internal const string SSL_ENABLED = "sslEnabled";

		// Token: 0x04000387 RID: 903
		internal const string SSL_PROPERTIES = "sslProperties";
	}
}
