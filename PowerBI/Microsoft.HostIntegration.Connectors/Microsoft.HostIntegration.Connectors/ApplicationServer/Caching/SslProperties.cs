using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000C1 RID: 193
	[Serializable]
	internal class SslProperties : ConfigurationElement, ISerializable
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x00015607 File Offset: 0x00013807
		internal SslProperties()
		{
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00016B33 File Offset: 0x00014D33
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x00016B45 File Offset: 0x00014D45
		[ConfigurationProperty("sslEndpointMode", IsRequired = false, DefaultValue = DataCacheSslMode.Dual)]
		public DataCacheSslMode SslMode
		{
			get
			{
				return (DataCacheSslMode)base["sslEndpointMode"];
			}
			set
			{
				base["sslEndpointMode"] = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00016B58 File Offset: 0x00014D58
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x00016B6A File Offset: 0x00014D6A
		[ConfigurationProperty("certificateSubject", IsRequired = false)]
		public string SslCertIdentity
		{
			get
			{
				return (string)base["certificateSubject"];
			}
			set
			{
				base["certificateSubject"] = value;
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00016B78 File Offset: 0x00014D78
		protected SslProperties(SerializationInfo info, StreamingContext context)
		{
			this.SslMode = (DataCacheSslMode)info.GetInt32("sslEndpointMode");
			this.SslCertIdentity = info.GetString("certificateSubject");
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00016BA2 File Offset: 0x00014DA2
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info != null)
			{
				info.AddValue("sslEndpointMode", (int)this.SslMode);
				info.AddValue("certificateSubject", this.SslCertIdentity);
			}
		}

		// Token: 0x04000388 RID: 904
		internal const string SSL_MODE = "sslEndpointMode";

		// Token: 0x04000389 RID: 905
		internal const string CERT_IDENTITY = "certificateSubject";
	}
}
