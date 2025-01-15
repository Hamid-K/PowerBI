using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000C2 RID: 194
	[Serializable]
	internal class SharedKeyAuthorization : ConfigurationElement, ISerializable
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x00015607 File Offset: 0x00013807
		internal SharedKeyAuthorization()
		{
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00015FAF File Offset: 0x000141AF
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x00015FC1 File Offset: 0x000141C1
		[ConfigurationProperty("isEnabled", DefaultValue = false, IsRequired = false)]
		internal bool IsEnabled
		{
			get
			{
				return (bool)base["isEnabled"];
			}
			set
			{
				base["isEnabled"] = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00016BC9 File Offset: 0x00014DC9
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x00016BDB File Offset: 0x00014DDB
		[ConfigurationProperty("authorizationKey", DefaultValue = null, IsRequired = false)]
		internal string AuthorizationKey
		{
			get
			{
				return (string)base["authorizationKey"];
			}
			set
			{
				base["authorizationKey"] = value;
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00016BE9 File Offset: 0x00014DE9
		protected SharedKeyAuthorization(SerializationInfo info, StreamingContext context)
		{
			this.IsEnabled = info.GetBoolean("isEnabled");
			this.AuthorizationKey = info.GetString("authorizationKey");
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00016C13 File Offset: 0x00014E13
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("isEnabled", this.IsEnabled);
			info.AddValue("authorizationKey", this.AuthorizationKey);
		}

		// Token: 0x0400038A RID: 906
		private const string IS_ENABLED = "isEnabled";

		// Token: 0x0400038B RID: 907
		internal const string AUTHORIZATION_KEY = "authorizationKey";
	}
}
