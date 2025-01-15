using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000F5 RID: 245
	[CompilerGenerated]
	internal class RuntimeSR
	{
		// Token: 0x06000D73 RID: 3443 RVA: 0x00030B0D File Offset: 0x0002ED0D
		protected RuntimeSR()
		{
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00030B15 File Offset: 0x0002ED15
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x00030B1C File Offset: 0x0002ED1C
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

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00030B24 File Offset: 0x0002ED24
		public static string Exception_DeprecatedAndNotSupportedFeature
		{
			get
			{
				return RuntimeSR.Keys.GetString("Exception_DeprecatedAndNotSupportedFeature");
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x00030B30 File Offset: 0x0002ED30
		public static string AsyncHelper_TimeoutElapsed
		{
			get
			{
				return RuntimeSR.Keys.GetString("AsyncHelper_TimeoutElapsed");
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x00030B3C File Offset: 0x0002ED3C
		public static string Token_Expired
		{
			get
			{
				return RuntimeSR.Keys.GetString("Token_Expired");
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00030B48 File Offset: 0x0002ED48
		public static string AccessToken_Invalid
		{
			get
			{
				return RuntimeSR.Keys.GetString("AccessToken_Invalid");
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00030B54 File Offset: 0x0002ED54
		public static string TokenRefreshFailure
		{
			get
			{
				return RuntimeSR.Keys.GetString("TokenRefreshFailure");
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00030B60 File Offset: 0x0002ED60
		public static string NonRefreshableToken_AlreadyPresented
		{
			get
			{
				return RuntimeSR.Keys.GetString("NonRefreshableToken_AlreadyPresented");
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00030B6C File Offset: 0x0002ED6C
		public static string NeedRefreshableToken
		{
			get
			{
				return RuntimeSR.Keys.GetString("NeedRefreshableToken");
			}
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00030B78 File Offset: 0x0002ED78
		public static string Exception_ResourceMissingInPool(string key)
		{
			return RuntimeSR.Keys.GetString("Exception_ResourceMissingInPool", key);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00030B85 File Offset: 0x0002ED85
		public static string Exception_InvalidKeyValueSet(string keyValueSet)
		{
			return RuntimeSR.Keys.GetString("Exception_InvalidKeyValueSet", keyValueSet);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00030B92 File Offset: 0x0002ED92
		public static string Exception_InvalidConnectionStringPropertyNameFormat(string propertyName)
		{
			return RuntimeSR.Keys.GetString("Exception_InvalidConnectionStringPropertyNameFormat", propertyName);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00030B9F File Offset: 0x0002ED9F
		public static string Exception_InvalidRelativePath(string path, string relativePath)
		{
			return RuntimeSR.Keys.GetString("Exception_InvalidRelativePath", path, relativePath);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00030BAD File Offset: 0x0002EDAD
		public static string AsPaasHelper_TechnicalDetails_AaPaasInfra(string rootActivityId, string currentUtcDate, string eol)
		{
			return RuntimeSR.Keys.GetString("AsPaasHelper_TechnicalDetails_AaPaasInfra", rootActivityId, currentUtcDate, eol);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00030BBC File Offset: 0x0002EDBC
		public static string AsPaasHelper_TechnicalDetails_PbiShared(string rootActivityId, string eol)
		{
			return RuntimeSR.Keys.GetString("AsPaasHelper_TechnicalDetails_PbiShared", rootActivityId, eol);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00030BCA File Offset: 0x0002EDCA
		public static string AsPaasHelper_TechnicalDetails_Dataverse(string clientRequestId, string serviceRequestId, string eol)
		{
			return RuntimeSR.Keys.GetString("AsPaasHelper_TechnicalDetails_Dataverse", clientRequestId, serviceRequestId, eol);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00030BD9 File Offset: 0x0002EDD9
		public static string AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo(string technicalDetails, string workspaceName, string eol)
		{
			return RuntimeSR.Keys.GetString("AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo", technicalDetails, workspaceName, eol);
		}

		// Token: 0x020001CB RID: 459
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x060013C1 RID: 5057 RVA: 0x000451BB File Offset: 0x000433BB
			private Keys()
			{
			}

			// Token: 0x170006E9 RID: 1769
			// (get) Token: 0x060013C2 RID: 5058 RVA: 0x000451C3 File Offset: 0x000433C3
			// (set) Token: 0x060013C3 RID: 5059 RVA: 0x000451CA File Offset: 0x000433CA
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

			// Token: 0x060013C4 RID: 5060 RVA: 0x000451D2 File Offset: 0x000433D2
			public static string GetString(string key)
			{
				return RuntimeSR.Keys.resourceManager.GetString(key, RuntimeSR.Keys._culture);
			}

			// Token: 0x060013C5 RID: 5061 RVA: 0x000451E4 File Offset: 0x000433E4
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, RuntimeSR.Keys.resourceManager.GetString(key, RuntimeSR.Keys._culture), arg0);
			}

			// Token: 0x060013C6 RID: 5062 RVA: 0x00045201 File Offset: 0x00043401
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, RuntimeSR.Keys.resourceManager.GetString(key, RuntimeSR.Keys._culture), arg0, arg1);
			}

			// Token: 0x060013C7 RID: 5063 RVA: 0x0004521F File Offset: 0x0004341F
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, RuntimeSR.Keys.resourceManager.GetString(key, RuntimeSR.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x04000D02 RID: 3330
			private static ResourceManager resourceManager = new ResourceManager(typeof(RuntimeSR).FullName, typeof(RuntimeSR).Module.Assembly);

			// Token: 0x04000D03 RID: 3331
			private static CultureInfo _culture = null;

			// Token: 0x04000D04 RID: 3332
			public const string Exception_ResourceMissingInPool = "Exception_ResourceMissingInPool";

			// Token: 0x04000D05 RID: 3333
			public const string Exception_InvalidKeyValueSet = "Exception_InvalidKeyValueSet";

			// Token: 0x04000D06 RID: 3334
			public const string Exception_InvalidConnectionStringPropertyNameFormat = "Exception_InvalidConnectionStringPropertyNameFormat";

			// Token: 0x04000D07 RID: 3335
			public const string Exception_DeprecatedAndNotSupportedFeature = "Exception_DeprecatedAndNotSupportedFeature";

			// Token: 0x04000D08 RID: 3336
			public const string Exception_InvalidRelativePath = "Exception_InvalidRelativePath";

			// Token: 0x04000D09 RID: 3337
			public const string AsPaasHelper_TechnicalDetails_AaPaasInfra = "AsPaasHelper_TechnicalDetails_AaPaasInfra";

			// Token: 0x04000D0A RID: 3338
			public const string AsPaasHelper_TechnicalDetails_PbiShared = "AsPaasHelper_TechnicalDetails_PbiShared";

			// Token: 0x04000D0B RID: 3339
			public const string AsPaasHelper_TechnicalDetails_Dataverse = "AsPaasHelper_TechnicalDetails_Dataverse";

			// Token: 0x04000D0C RID: 3340
			public const string AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo = "AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo";

			// Token: 0x04000D0D RID: 3341
			public const string AsyncHelper_TimeoutElapsed = "AsyncHelper_TimeoutElapsed";

			// Token: 0x04000D0E RID: 3342
			public const string Token_Expired = "Token_Expired";

			// Token: 0x04000D0F RID: 3343
			public const string AccessToken_Invalid = "AccessToken_Invalid";

			// Token: 0x04000D10 RID: 3344
			public const string TokenRefreshFailure = "TokenRefreshFailure";

			// Token: 0x04000D11 RID: 3345
			public const string NonRefreshableToken_AlreadyPresented = "NonRefreshableToken_AlreadyPresented";

			// Token: 0x04000D12 RID: 3346
			public const string NeedRefreshableToken = "NeedRefreshableToken";
		}
	}
}
