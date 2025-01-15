using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000D8 RID: 216
	[CompilerGenerated]
	internal class RuntimeSR
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x0002AFFE File Offset: 0x000291FE
		protected RuntimeSR()
		{
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0002B006 File Offset: 0x00029206
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x0002B00D File Offset: 0x0002920D
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

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0002B015 File Offset: 0x00029215
		public static string Exception_DeprecatedAndNotSupportedFeature
		{
			get
			{
				return RuntimeSR.Keys.GetString("Exception_DeprecatedAndNotSupportedFeature");
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0002B021 File Offset: 0x00029221
		public static string AsyncHelper_TimeoutElapsed
		{
			get
			{
				return RuntimeSR.Keys.GetString("AsyncHelper_TimeoutElapsed");
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0002B02D File Offset: 0x0002922D
		public static string Token_Expired
		{
			get
			{
				return RuntimeSR.Keys.GetString("Token_Expired");
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0002B039 File Offset: 0x00029239
		public static string AccessToken_Invalid
		{
			get
			{
				return RuntimeSR.Keys.GetString("AccessToken_Invalid");
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0002B045 File Offset: 0x00029245
		public static string TokenRefreshFailure
		{
			get
			{
				return RuntimeSR.Keys.GetString("TokenRefreshFailure");
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0002B051 File Offset: 0x00029251
		public static string NonRefreshableToken_AlreadyPresented
		{
			get
			{
				return RuntimeSR.Keys.GetString("NonRefreshableToken_AlreadyPresented");
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0002B05D File Offset: 0x0002925D
		public static string NeedRefreshableToken
		{
			get
			{
				return RuntimeSR.Keys.GetString("NeedRefreshableToken");
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002B069 File Offset: 0x00029269
		public static string Exception_ResourceMissingInPool(string key)
		{
			return RuntimeSR.Keys.GetString("Exception_ResourceMissingInPool", key);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002B076 File Offset: 0x00029276
		public static string Exception_InvalidKeyValueSet(string keyValueSet)
		{
			return RuntimeSR.Keys.GetString("Exception_InvalidKeyValueSet", keyValueSet);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002B083 File Offset: 0x00029283
		public static string Exception_InvalidConnectionStringPropertyNameFormat(string propertyName)
		{
			return RuntimeSR.Keys.GetString("Exception_InvalidConnectionStringPropertyNameFormat", propertyName);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002B090 File Offset: 0x00029290
		public static string Exception_InvalidRelativePath(string path, string relativePath)
		{
			return RuntimeSR.Keys.GetString("Exception_InvalidRelativePath", path, relativePath);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002B09E File Offset: 0x0002929E
		public static string AsPaasHelper_TechnicalDetails_AaPaasInfra(string rootActivityId, string currentUtcDate, string eol)
		{
			return RuntimeSR.Keys.GetString("AsPaasHelper_TechnicalDetails_AaPaasInfra", rootActivityId, currentUtcDate, eol);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002B0AD File Offset: 0x000292AD
		public static string AsPaasHelper_TechnicalDetails_PbiShared(string rootActivityId, string eol)
		{
			return RuntimeSR.Keys.GetString("AsPaasHelper_TechnicalDetails_PbiShared", rootActivityId, eol);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002B0BB File Offset: 0x000292BB
		public static string AsPaasHelper_TechnicalDetails_Dataverse(string clientRequestId, string serviceRequestId, string eol)
		{
			return RuntimeSR.Keys.GetString("AsPaasHelper_TechnicalDetails_Dataverse", clientRequestId, serviceRequestId, eol);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0002B0CA File Offset: 0x000292CA
		public static string AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo(string technicalDetails, string workspaceName, string eol)
		{
			return RuntimeSR.Keys.GetString("AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo", technicalDetails, workspaceName, eol);
		}

		// Token: 0x0200019D RID: 413
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x0600130E RID: 4878 RVA: 0x000431A7 File Offset: 0x000413A7
			private Keys()
			{
			}

			// Token: 0x1700062D RID: 1581
			// (get) Token: 0x0600130F RID: 4879 RVA: 0x000431AF File Offset: 0x000413AF
			// (set) Token: 0x06001310 RID: 4880 RVA: 0x000431B6 File Offset: 0x000413B6
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

			// Token: 0x06001311 RID: 4881 RVA: 0x000431BE File Offset: 0x000413BE
			public static string GetString(string key)
			{
				return RuntimeSR.Keys.resourceManager.GetString(key, RuntimeSR.Keys._culture);
			}

			// Token: 0x06001312 RID: 4882 RVA: 0x000431D0 File Offset: 0x000413D0
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, RuntimeSR.Keys.resourceManager.GetString(key, RuntimeSR.Keys._culture), arg0);
			}

			// Token: 0x06001313 RID: 4883 RVA: 0x000431ED File Offset: 0x000413ED
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, RuntimeSR.Keys.resourceManager.GetString(key, RuntimeSR.Keys._culture), arg0, arg1);
			}

			// Token: 0x06001314 RID: 4884 RVA: 0x0004320B File Offset: 0x0004140B
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, RuntimeSR.Keys.resourceManager.GetString(key, RuntimeSR.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x04000C46 RID: 3142
			private static ResourceManager resourceManager = new ResourceManager(typeof(RuntimeSR).FullName, typeof(RuntimeSR).Module.Assembly);

			// Token: 0x04000C47 RID: 3143
			private static CultureInfo _culture = null;

			// Token: 0x04000C48 RID: 3144
			public const string Exception_ResourceMissingInPool = "Exception_ResourceMissingInPool";

			// Token: 0x04000C49 RID: 3145
			public const string Exception_InvalidKeyValueSet = "Exception_InvalidKeyValueSet";

			// Token: 0x04000C4A RID: 3146
			public const string Exception_InvalidConnectionStringPropertyNameFormat = "Exception_InvalidConnectionStringPropertyNameFormat";

			// Token: 0x04000C4B RID: 3147
			public const string Exception_DeprecatedAndNotSupportedFeature = "Exception_DeprecatedAndNotSupportedFeature";

			// Token: 0x04000C4C RID: 3148
			public const string Exception_InvalidRelativePath = "Exception_InvalidRelativePath";

			// Token: 0x04000C4D RID: 3149
			public const string AsPaasHelper_TechnicalDetails_AaPaasInfra = "AsPaasHelper_TechnicalDetails_AaPaasInfra";

			// Token: 0x04000C4E RID: 3150
			public const string AsPaasHelper_TechnicalDetails_PbiShared = "AsPaasHelper_TechnicalDetails_PbiShared";

			// Token: 0x04000C4F RID: 3151
			public const string AsPaasHelper_TechnicalDetails_Dataverse = "AsPaasHelper_TechnicalDetails_Dataverse";

			// Token: 0x04000C50 RID: 3152
			public const string AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo = "AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo";

			// Token: 0x04000C51 RID: 3153
			public const string AsyncHelper_TimeoutElapsed = "AsyncHelper_TimeoutElapsed";

			// Token: 0x04000C52 RID: 3154
			public const string Token_Expired = "Token_Expired";

			// Token: 0x04000C53 RID: 3155
			public const string AccessToken_Invalid = "AccessToken_Invalid";

			// Token: 0x04000C54 RID: 3156
			public const string TokenRefreshFailure = "TokenRefreshFailure";

			// Token: 0x04000C55 RID: 3157
			public const string NonRefreshableToken_AlreadyPresented = "NonRefreshableToken_AlreadyPresented";

			// Token: 0x04000C56 RID: 3158
			public const string NeedRefreshableToken = "NeedRefreshableToken";
		}
	}
}
