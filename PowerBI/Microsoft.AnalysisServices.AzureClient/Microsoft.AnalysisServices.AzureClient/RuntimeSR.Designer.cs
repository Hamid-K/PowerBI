using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.AzureClient
{
	// Token: 0x02000012 RID: 18
	[CompilerGenerated]
	internal class RuntimeSR
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002808 File Offset: 0x00000A08
		protected RuntimeSR()
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002810 File Offset: 0x00000A10
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002817 File Offset: 0x00000A17
		public static CultureInfo Culture
		{
			get
			{
				return RuntimeSR.Keys.Culture;
			}
			set
			{
				RuntimeSR.Keys.Culture = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000281F File Offset: 0x00000A1F
		public static string Exception_DeprecatedAndNotSupportedFeature
		{
			get
			{
				return RuntimeSR.Keys.GetString("Exception_DeprecatedAndNotSupportedFeature");
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000282B File Offset: 0x00000A2B
		public static string AsyncHelper_TimeoutElapsed
		{
			get
			{
				return RuntimeSR.Keys.GetString("AsyncHelper_TimeoutElapsed");
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002837 File Offset: 0x00000A37
		public static string Token_Expired
		{
			get
			{
				return RuntimeSR.Keys.GetString("Token_Expired");
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002843 File Offset: 0x00000A43
		public static string AccessToken_Invalid
		{
			get
			{
				return RuntimeSR.Keys.GetString("AccessToken_Invalid");
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000284F File Offset: 0x00000A4F
		public static string TokenRefreshFailure
		{
			get
			{
				return RuntimeSR.Keys.GetString("TokenRefreshFailure");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000285B File Offset: 0x00000A5B
		public static string NonRefreshableToken_AlreadyPresented
		{
			get
			{
				return RuntimeSR.Keys.GetString("NonRefreshableToken_AlreadyPresented");
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002867 File Offset: 0x00000A67
		public static string NeedRefreshableToken
		{
			get
			{
				return RuntimeSR.Keys.GetString("NeedRefreshableToken");
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002873 File Offset: 0x00000A73
		public static string Exception_ResourceMissingInPool(string key)
		{
			return RuntimeSR.Keys.GetString("Exception_ResourceMissingInPool", key);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002880 File Offset: 0x00000A80
		public static string Exception_InvalidKeyValueSet(string keyValueSet)
		{
			return RuntimeSR.Keys.GetString("Exception_InvalidKeyValueSet", keyValueSet);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000288D File Offset: 0x00000A8D
		public static string Exception_InvalidConnectionStringPropertyNameFormat(string propertyName)
		{
			return RuntimeSR.Keys.GetString("Exception_InvalidConnectionStringPropertyNameFormat", propertyName);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000289A File Offset: 0x00000A9A
		public static string Exception_InvalidRelativePath(string path, string relativePath)
		{
			return RuntimeSR.Keys.GetString("Exception_InvalidRelativePath", path, relativePath);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000028A8 File Offset: 0x00000AA8
		public static string AsPaasHelper_TechnicalDetails_AaPaasInfra(string rootActivityId, string currentUtcDate, string eol)
		{
			return RuntimeSR.Keys.GetString("AsPaasHelper_TechnicalDetails_AaPaasInfra", rootActivityId, currentUtcDate, eol);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000028B7 File Offset: 0x00000AB7
		public static string AsPaasHelper_TechnicalDetails_PbiShared(string rootActivityId, string eol)
		{
			return RuntimeSR.Keys.GetString("AsPaasHelper_TechnicalDetails_PbiShared", rootActivityId, eol);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000028C5 File Offset: 0x00000AC5
		public static string AsPaasHelper_TechnicalDetails_Dataverse(string clientRequestId, string serviceRequestId, string eol)
		{
			return RuntimeSR.Keys.GetString("AsPaasHelper_TechnicalDetails_Dataverse", clientRequestId, serviceRequestId, eol);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000028D4 File Offset: 0x00000AD4
		public static string AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo(string technicalDetails, string workspaceName, string eol)
		{
			return RuntimeSR.Keys.GetString("AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo", technicalDetails, workspaceName, eol);
		}

		// Token: 0x02000046 RID: 70
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x0600020F RID: 527 RVA: 0x0000AB1B File Offset: 0x00008D1B
			private Keys()
			{
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000210 RID: 528 RVA: 0x0000AB23 File Offset: 0x00008D23
			// (set) Token: 0x06000211 RID: 529 RVA: 0x0000AB2A File Offset: 0x00008D2A
			public static CultureInfo Culture
			{
				get
				{
					return RuntimeSR.Keys._culture;
				}
				set
				{
					RuntimeSR.Keys._culture = value;
				}
			}

			// Token: 0x06000212 RID: 530 RVA: 0x0000AB32 File Offset: 0x00008D32
			public static string GetString(string key)
			{
				return RuntimeSR.Keys.resourceManager.GetString(key, RuntimeSR.Keys._culture);
			}

			// Token: 0x06000213 RID: 531 RVA: 0x0000AB44 File Offset: 0x00008D44
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, RuntimeSR.Keys.resourceManager.GetString(key, RuntimeSR.Keys._culture), new object[] { arg0 });
			}

			// Token: 0x06000214 RID: 532 RVA: 0x0000AB6A File Offset: 0x00008D6A
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, RuntimeSR.Keys.resourceManager.GetString(key, RuntimeSR.Keys._culture), new object[] { arg0, arg1 });
			}

			// Token: 0x06000215 RID: 533 RVA: 0x0000AB94 File Offset: 0x00008D94
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, RuntimeSR.Keys.resourceManager.GetString(key, RuntimeSR.Keys._culture), new object[] { arg0, arg1, arg2 });
			}

			// Token: 0x04000153 RID: 339
			private static ResourceManager resourceManager = new ResourceManager(typeof(RuntimeSR).FullName, typeof(RuntimeSR).Module.Assembly);

			// Token: 0x04000154 RID: 340
			private static CultureInfo _culture = null;

			// Token: 0x04000155 RID: 341
			public const string Exception_ResourceMissingInPool = "Exception_ResourceMissingInPool";

			// Token: 0x04000156 RID: 342
			public const string Exception_InvalidKeyValueSet = "Exception_InvalidKeyValueSet";

			// Token: 0x04000157 RID: 343
			public const string Exception_InvalidConnectionStringPropertyNameFormat = "Exception_InvalidConnectionStringPropertyNameFormat";

			// Token: 0x04000158 RID: 344
			public const string Exception_DeprecatedAndNotSupportedFeature = "Exception_DeprecatedAndNotSupportedFeature";

			// Token: 0x04000159 RID: 345
			public const string Exception_InvalidRelativePath = "Exception_InvalidRelativePath";

			// Token: 0x0400015A RID: 346
			public const string AsPaasHelper_TechnicalDetails_AaPaasInfra = "AsPaasHelper_TechnicalDetails_AaPaasInfra";

			// Token: 0x0400015B RID: 347
			public const string AsPaasHelper_TechnicalDetails_PbiShared = "AsPaasHelper_TechnicalDetails_PbiShared";

			// Token: 0x0400015C RID: 348
			public const string AsPaasHelper_TechnicalDetails_Dataverse = "AsPaasHelper_TechnicalDetails_Dataverse";

			// Token: 0x0400015D RID: 349
			public const string AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo = "AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo";

			// Token: 0x0400015E RID: 350
			public const string AsyncHelper_TimeoutElapsed = "AsyncHelper_TimeoutElapsed";

			// Token: 0x0400015F RID: 351
			public const string Token_Expired = "Token_Expired";

			// Token: 0x04000160 RID: 352
			public const string AccessToken_Invalid = "AccessToken_Invalid";

			// Token: 0x04000161 RID: 353
			public const string TokenRefreshFailure = "TokenRefreshFailure";

			// Token: 0x04000162 RID: 354
			public const string NonRefreshableToken_AlreadyPresented = "NonRefreshableToken_AlreadyPresented";

			// Token: 0x04000163 RID: 355
			public const string NeedRefreshableToken = "NeedRefreshableToken";
		}
	}
}
