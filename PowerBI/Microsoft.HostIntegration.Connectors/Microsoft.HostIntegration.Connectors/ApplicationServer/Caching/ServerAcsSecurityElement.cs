using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000C3 RID: 195
	[Serializable]
	internal class ServerAcsSecurityElement : ConfigurationElement, ISerializable
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x00015607 File Offset: 0x00013807
		internal ServerAcsSecurityElement()
		{
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00016C37 File Offset: 0x00014E37
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x00016C49 File Offset: 0x00014E49
		[ConfigurationProperty("issuerKey")]
		public string SigningKey
		{
			get
			{
				return (string)base["issuerKey"];
			}
			set
			{
				base["issuerKey"] = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00016C57 File Offset: 0x00014E57
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x00016C69 File Offset: 0x00014E69
		[ConfigurationProperty("additionalIssuerKey", IsRequired = false)]
		public string AdditionalSigningKey
		{
			get
			{
				return (string)base["additionalIssuerKey"];
			}
			set
			{
				base["additionalIssuerKey"] = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00016C77 File Offset: 0x00014E77
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x00016C89 File Offset: 0x00014E89
		[ConfigurationProperty("acshostname")]
		public string AcsHostName
		{
			get
			{
				return (string)base["acshostname"];
			}
			set
			{
				base["acshostname"] = value;
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00016C98 File Offset: 0x00014E98
		protected ServerAcsSecurityElement(SerializationInfo info, StreamingContext context)
		{
			this.AcsHostName = (string)info.GetValue("acshostname", typeof(string));
			this.SigningKey = (string)info.GetValue("issuerKey", typeof(string));
			try
			{
				this.AdditionalSigningKey = info.GetString("additionalIssuerKey");
			}
			catch (SerializationException)
			{
				this.AdditionalSigningKey = string.Empty;
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00016D1C File Offset: 0x00014F1C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("acshostname", this.AcsHostName);
			info.AddValue("issuerKey", this.SigningKey);
			info.AddValue("additionalIssuerKey", this.AdditionalSigningKey);
		}

		// Token: 0x0400038C RID: 908
		internal const string SIGNING_KEY = "issuerKey";

		// Token: 0x0400038D RID: 909
		internal const string ADDITIONAL_SIGNING_KEY = "additionalIssuerKey";

		// Token: 0x0400038E RID: 910
		internal const string ACS_HOST_NAME = "acshostname";
	}
}
