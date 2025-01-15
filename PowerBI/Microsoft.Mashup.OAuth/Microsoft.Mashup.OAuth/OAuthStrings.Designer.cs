using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x0200002B RID: 43
	internal class OAuthStrings
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00006AF0 File Offset: 0x00004CF0
		public static ResourceManager ResourceManager
		{
			get
			{
				return OAuthStrings.ResourceLoader.Resources;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00006AF7 File Offset: 0x00004CF7
		public static string Error_AccessDenied
		{
			get
			{
				return OAuthStrings.ResourceLoader.GetString("Error_AccessDenied");
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006B03 File Offset: 0x00004D03
		public static string HttpsRequired(object p0)
		{
			return OAuthStrings.ResourceLoader.GetString("HttpsRequired", new object[] { p0 });
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00006B19 File Offset: 0x00004D19
		public static string InvalidOAuthResponse
		{
			get
			{
				return OAuthStrings.ResourceLoader.GetString("InvalidOAuthResponse");
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006B25 File Offset: 0x00004D25
		public static string InvalidOAuthState
		{
			get
			{
				return OAuthStrings.ResourceLoader.GetString("InvalidOAuthState");
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006B31 File Offset: 0x00004D31
		public static string InvalidSalesforceUri(object p0)
		{
			return OAuthStrings.ResourceLoader.GetString("InvalidSalesforceUri", new object[] { p0 });
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006B47 File Offset: 0x00004D47
		public static string InvalidToken
		{
			get
			{
				return OAuthStrings.ResourceLoader.GetString("InvalidToken");
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006B53 File Offset: 0x00004D53
		public static string MS_Online_ID_Not_Supported
		{
			get
			{
				return OAuthStrings.ResourceLoader.GetString("MS_Online_ID_Not_Supported");
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00006B5F File Offset: 0x00004D5F
		public static string FinishLoginMissingFormBasedCookies
		{
			get
			{
				return OAuthStrings.ResourceLoader.GetString("FinishLoginMissingFormBasedCookies");
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006B6B File Offset: 0x00004D6B
		public static string NotTrustedSts
		{
			get
			{
				return OAuthStrings.ResourceLoader.GetString("NotTrustedSts");
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006B77 File Offset: 0x00004D77
		public static string OAuthHeader_NoAuthorizationUri(object p0)
		{
			return OAuthStrings.ResourceLoader.GetString("OAuthHeader_NoAuthorizationUri", new object[] { p0 });
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006B8D File Offset: 0x00004D8D
		public static string RedirectError(object p0, object p1)
		{
			return OAuthStrings.ResourceLoader.GetString("RedirectError", new object[] { p0, p1 });
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00006BA7 File Offset: 0x00004DA7
		public static string RefreshFailed
		{
			get
			{
				return OAuthStrings.ResourceLoader.GetString("RefreshFailed");
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00006BB3 File Offset: 0x00004DB3
		public static string TlsAlreadyInitialized
		{
			get
			{
				return OAuthStrings.ResourceLoader.GetString("TlsAlreadyInitialized");
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006BBF File Offset: 0x00004DBF
		public static string CouldNotLoadCertificate(object p0)
		{
			return OAuthStrings.ResourceLoader.GetString("CouldNotLoadCertificate", new object[] { p0 });
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006BD5 File Offset: 0x00004DD5
		public static string NoPrivateKey(object p0)
		{
			return OAuthStrings.ResourceLoader.GetString("NoPrivateKey", new object[] { p0 });
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006BEB File Offset: 0x00004DEB
		public static string UnsupportedSecretType(object p0)
		{
			return OAuthStrings.ResourceLoader.GetString("UnsupportedSecretType", new object[] { p0 });
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006C01 File Offset: 0x00004E01
		public static string CertificateVerificationFailed(object p0)
		{
			return OAuthStrings.ResourceLoader.GetString("CertificateVerificationFailed", new object[] { p0 });
		}

		// Token: 0x02000033 RID: 51
		private class ResourceLoader
		{
			// Token: 0x06000187 RID: 391 RVA: 0x00007419 File Offset: 0x00005619
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.Mashup.OAuth.OAuthStrings", base.GetType().Assembly);
			}

			// Token: 0x06000188 RID: 392 RVA: 0x0000743C File Offset: 0x0000563C
			private static OAuthStrings.ResourceLoader GetLoader()
			{
				if (OAuthStrings.ResourceLoader.instance == null)
				{
					OAuthStrings.ResourceLoader resourceLoader = new OAuthStrings.ResourceLoader();
					Interlocked.CompareExchange<OAuthStrings.ResourceLoader>(ref OAuthStrings.ResourceLoader.instance, resourceLoader, null);
				}
				return OAuthStrings.ResourceLoader.instance;
			}

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x06000189 RID: 393 RVA: 0x00007468 File Offset: 0x00005668
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x0600018A RID: 394 RVA: 0x0000746B File Offset: 0x0000566B
			public static ResourceManager Resources
			{
				get
				{
					return OAuthStrings.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x0600018B RID: 395 RVA: 0x00007478 File Offset: 0x00005678
			public static string GetString(string name, params object[] args)
			{
				OAuthStrings.ResourceLoader loader = OAuthStrings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				string @string = loader.resources.GetString(name, OAuthStrings.ResourceLoader.Culture);
				if (args != null && args.Length != 0)
				{
					return string.Format(CultureInfo.CurrentCulture, @string, args);
				}
				return @string;
			}

			// Token: 0x0600018C RID: 396 RVA: 0x000074B8 File Offset: 0x000056B8
			public static string GetString(string name)
			{
				OAuthStrings.ResourceLoader loader = OAuthStrings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, OAuthStrings.ResourceLoader.Culture);
			}

			// Token: 0x0600018D RID: 397 RVA: 0x000074E4 File Offset: 0x000056E4
			public static object GetObject(string name)
			{
				OAuthStrings.ResourceLoader loader = OAuthStrings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, OAuthStrings.ResourceLoader.Culture);
			}

			// Token: 0x0600018E RID: 398 RVA: 0x00007510 File Offset: 0x00005710
			public static T GetObject<T>(string name) where T : class
			{
				OAuthStrings.ResourceLoader loader = OAuthStrings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, OAuthStrings.ResourceLoader.Culture));
			}

			// Token: 0x04000114 RID: 276
			private static OAuthStrings.ResourceLoader instance;

			// Token: 0x04000115 RID: 277
			private ResourceManager resources;
		}
	}
}
