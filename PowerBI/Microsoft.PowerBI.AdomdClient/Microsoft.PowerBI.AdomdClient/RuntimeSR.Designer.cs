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
		// Token: 0x06000D66 RID: 3430 RVA: 0x000307DD File Offset: 0x0002E9DD
		protected RuntimeSR()
		{
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x000307E5 File Offset: 0x0002E9E5
		// (set) Token: 0x06000D68 RID: 3432 RVA: 0x000307EC File Offset: 0x0002E9EC
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

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x000307F4 File Offset: 0x0002E9F4
		public static string Exception_DeprecatedAndNotSupportedFeature
		{
			get
			{
				return RuntimeSR.Keys.GetString("Exception_DeprecatedAndNotSupportedFeature");
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00030800 File Offset: 0x0002EA00
		public static string AsyncHelper_TimeoutElapsed
		{
			get
			{
				return RuntimeSR.Keys.GetString("AsyncHelper_TimeoutElapsed");
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0003080C File Offset: 0x0002EA0C
		public static string Token_Expired
		{
			get
			{
				return RuntimeSR.Keys.GetString("Token_Expired");
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x00030818 File Offset: 0x0002EA18
		public static string AccessToken_Invalid
		{
			get
			{
				return RuntimeSR.Keys.GetString("AccessToken_Invalid");
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x00030824 File Offset: 0x0002EA24
		public static string TokenRefreshFailure
		{
			get
			{
				return RuntimeSR.Keys.GetString("TokenRefreshFailure");
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00030830 File Offset: 0x0002EA30
		public static string NonRefreshableToken_AlreadyPresented
		{
			get
			{
				return RuntimeSR.Keys.GetString("NonRefreshableToken_AlreadyPresented");
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0003083C File Offset: 0x0002EA3C
		public static string NeedRefreshableToken
		{
			get
			{
				return RuntimeSR.Keys.GetString("NeedRefreshableToken");
			}
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00030848 File Offset: 0x0002EA48
		public static string Exception_ResourceMissingInPool(string key)
		{
			return RuntimeSR.Keys.GetString("Exception_ResourceMissingInPool", key);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00030855 File Offset: 0x0002EA55
		public static string Exception_InvalidKeyValueSet(string keyValueSet)
		{
			return RuntimeSR.Keys.GetString("Exception_InvalidKeyValueSet", keyValueSet);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00030862 File Offset: 0x0002EA62
		public static string Exception_InvalidConnectionStringPropertyNameFormat(string propertyName)
		{
			return RuntimeSR.Keys.GetString("Exception_InvalidConnectionStringPropertyNameFormat", propertyName);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0003086F File Offset: 0x0002EA6F
		public static string Exception_InvalidRelativePath(string path, string relativePath)
		{
			return RuntimeSR.Keys.GetString("Exception_InvalidRelativePath", path, relativePath);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0003087D File Offset: 0x0002EA7D
		public static string AsPaasHelper_TechnicalDetails_AaPaasInfra(string rootActivityId, string currentUtcDate, string eol)
		{
			return RuntimeSR.Keys.GetString("AsPaasHelper_TechnicalDetails_AaPaasInfra", rootActivityId, currentUtcDate, eol);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0003088C File Offset: 0x0002EA8C
		public static string AsPaasHelper_TechnicalDetails_PbiShared(string rootActivityId, string eol)
		{
			return RuntimeSR.Keys.GetString("AsPaasHelper_TechnicalDetails_PbiShared", rootActivityId, eol);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0003089A File Offset: 0x0002EA9A
		public static string AsPaasHelper_TechnicalDetails_Dataverse(string clientRequestId, string serviceRequestId, string eol)
		{
			return RuntimeSR.Keys.GetString("AsPaasHelper_TechnicalDetails_Dataverse", clientRequestId, serviceRequestId, eol);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x000308A9 File Offset: 0x0002EAA9
		public static string AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo(string technicalDetails, string workspaceName, string eol)
		{
			return RuntimeSR.Keys.GetString("AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo", technicalDetails, workspaceName, eol);
		}

		// Token: 0x020001CB RID: 459
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x060013B4 RID: 5044 RVA: 0x00044C7F File Offset: 0x00042E7F
			private Keys()
			{
			}

			// Token: 0x170006E3 RID: 1763
			// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00044C87 File Offset: 0x00042E87
			// (set) Token: 0x060013B6 RID: 5046 RVA: 0x00044C8E File Offset: 0x00042E8E
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

			// Token: 0x060013B7 RID: 5047 RVA: 0x00044C96 File Offset: 0x00042E96
			public static string GetString(string key)
			{
				return RuntimeSR.Keys.resourceManager.GetString(key, RuntimeSR.Keys._culture);
			}

			// Token: 0x060013B8 RID: 5048 RVA: 0x00044CA8 File Offset: 0x00042EA8
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, RuntimeSR.Keys.resourceManager.GetString(key, RuntimeSR.Keys._culture), arg0);
			}

			// Token: 0x060013B9 RID: 5049 RVA: 0x00044CC5 File Offset: 0x00042EC5
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, RuntimeSR.Keys.resourceManager.GetString(key, RuntimeSR.Keys._culture), arg0, arg1);
			}

			// Token: 0x060013BA RID: 5050 RVA: 0x00044CE3 File Offset: 0x00042EE3
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, RuntimeSR.Keys.resourceManager.GetString(key, RuntimeSR.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x04000CF1 RID: 3313
			private static ResourceManager resourceManager = new ResourceManager(typeof(RuntimeSR).FullName, typeof(RuntimeSR).Module.Assembly);

			// Token: 0x04000CF2 RID: 3314
			private static CultureInfo _culture = null;

			// Token: 0x04000CF3 RID: 3315
			public const string Exception_ResourceMissingInPool = "Exception_ResourceMissingInPool";

			// Token: 0x04000CF4 RID: 3316
			public const string Exception_InvalidKeyValueSet = "Exception_InvalidKeyValueSet";

			// Token: 0x04000CF5 RID: 3317
			public const string Exception_InvalidConnectionStringPropertyNameFormat = "Exception_InvalidConnectionStringPropertyNameFormat";

			// Token: 0x04000CF6 RID: 3318
			public const string Exception_DeprecatedAndNotSupportedFeature = "Exception_DeprecatedAndNotSupportedFeature";

			// Token: 0x04000CF7 RID: 3319
			public const string Exception_InvalidRelativePath = "Exception_InvalidRelativePath";

			// Token: 0x04000CF8 RID: 3320
			public const string AsPaasHelper_TechnicalDetails_AaPaasInfra = "AsPaasHelper_TechnicalDetails_AaPaasInfra";

			// Token: 0x04000CF9 RID: 3321
			public const string AsPaasHelper_TechnicalDetails_PbiShared = "AsPaasHelper_TechnicalDetails_PbiShared";

			// Token: 0x04000CFA RID: 3322
			public const string AsPaasHelper_TechnicalDetails_Dataverse = "AsPaasHelper_TechnicalDetails_Dataverse";

			// Token: 0x04000CFB RID: 3323
			public const string AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo = "AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo";

			// Token: 0x04000CFC RID: 3324
			public const string AsyncHelper_TimeoutElapsed = "AsyncHelper_TimeoutElapsed";

			// Token: 0x04000CFD RID: 3325
			public const string Token_Expired = "Token_Expired";

			// Token: 0x04000CFE RID: 3326
			public const string AccessToken_Invalid = "AccessToken_Invalid";

			// Token: 0x04000CFF RID: 3327
			public const string TokenRefreshFailure = "TokenRefreshFailure";

			// Token: 0x04000D00 RID: 3328
			public const string NonRefreshableToken_AlreadyPresented = "NonRefreshableToken_AlreadyPresented";

			// Token: 0x04000D01 RID: 3329
			public const string NeedRefreshableToken = "NeedRefreshableToken";
		}
	}
}
