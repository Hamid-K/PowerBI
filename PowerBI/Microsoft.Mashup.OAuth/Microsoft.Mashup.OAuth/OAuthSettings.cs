using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x0200001E RID: 30
	public sealed class OAuthSettings
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x000050B6 File Offset: 0x000032B6
		static OAuthSettings()
		{
			Assembly assembly = typeof(object).Assembly;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000050D4 File Offset: 0x000032D4
		public OAuthSettings(ISecureTokenService[] allowedSecureTokenServices)
			: this(allowedSecureTokenServices, new TrustedResource[0])
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000050E3 File Offset: 0x000032E3
		public OAuthSettings(ISecureTokenService[] allowedSecureTokenServices, TrustedResource[] resourceList)
		{
			if (allowedSecureTokenServices == null)
			{
				throw new ArgumentNullException("allowedSecureTokenServices");
			}
			if (resourceList == null)
			{
				throw new ArgumentNullException("resourceList");
			}
			this.allowedSecureTokenServices = allowedSecureTokenServices;
			this.resourceList = resourceList;
			OAuthSettings.InitializeTls12And13();
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000511A File Offset: 0x0000331A
		public ISecureTokenService[] AllowedSecureTokenServices
		{
			get
			{
				return this.allowedSecureTokenServices;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005122 File Offset: 0x00003322
		public TrustedResource[] ResourceList
		{
			get
			{
				return this.resourceList;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000512C File Offset: 0x0000332C
		public static OAuthSettings Deserialize(string settings)
		{
			OAuthSettings oauthSettings;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(settings)))
			{
				oauthSettings = ((SerializedOAuthSettings)new DataContractJsonSerializer(typeof(SerializedOAuthSettings)).ReadObject(memoryStream)).ToOAuthSettings();
			}
			return oauthSettings;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005188 File Offset: 0x00003388
		public string Serialize()
		{
			SerializedOAuthSettings serializedOAuthSettings = new SerializedOAuthSettings(this.allowedSecureTokenServices, this.resourceList);
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new DataContractJsonSerializer(typeof(SerializedOAuthSettings)).WriteObject(memoryStream, serializedOAuthSettings);
				@string = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			return @string;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000051F4 File Offset: 0x000033F4
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000051F7 File Offset: 0x000033F7
		[Obsolete("Deprecated. Tls12 will be enabled during initialization.")]
		public static bool TryEnableTls12
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000051F9 File Offset: 0x000033F9
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00005200 File Offset: 0x00003400
		public static bool CertificateValidationEnabled
		{
			get
			{
				return OAuthSettings.certificateValidationEnabled;
			}
			set
			{
				OAuthSettings.certificateValidationEnabled = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005208 File Offset: 0x00003408
		// (set) Token: 0x060000DF RID: 223 RVA: 0x0000520F File Offset: 0x0000340F
		public static bool RejectOnUnknownRevocation
		{
			get
			{
				return OAuthSettings.rejectUnknownRevocationCerts;
			}
			set
			{
				OAuthSettings.rejectUnknownRevocationCerts = value;
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005218 File Offset: 0x00003418
		internal static void InitializeTls12And13()
		{
			if (!OAuthSettings.initialized)
			{
				int num = (int)(ServicePointManager.SecurityProtocol & ~SecurityProtocolType.Ssl3);
				if ((num & 768) == 0)
				{
					try
					{
						ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11;
					}
					catch (NotSupportedException)
					{
					}
				}
				if ((num & 3072) == 0)
				{
					try
					{
						ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
					}
					catch (NotSupportedException)
					{
					}
				}
				if ((num & 12288) == 0)
				{
					try
					{
						ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls13;
					}
					catch (NotSupportedException)
					{
					}
				}
				OAuthSettings.initialized = true;
			}
		}

		// Token: 0x040000C3 RID: 195
		internal const int Tls11 = 768;

		// Token: 0x040000C4 RID: 196
		internal const int Tls12 = 3072;

		// Token: 0x040000C5 RID: 197
		internal const int Tls13 = 12288;

		// Token: 0x040000C6 RID: 198
		private static bool certificateValidationEnabled = true;

		// Token: 0x040000C7 RID: 199
		private static bool rejectUnknownRevocationCerts = false;

		// Token: 0x040000C8 RID: 200
		private static bool initialized;

		// Token: 0x040000C9 RID: 201
		private ISecureTokenService[] allowedSecureTokenServices;

		// Token: 0x040000CA RID: 202
		private TrustedResource[] resourceList;
	}
}
