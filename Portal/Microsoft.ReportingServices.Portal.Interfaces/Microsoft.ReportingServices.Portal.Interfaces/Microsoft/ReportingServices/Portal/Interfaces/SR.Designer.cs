using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.Portal.Interfaces
{
	// Token: 0x02000082 RID: 130
	[CompilerGenerated]
	internal class SR
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x0000212F File Offset: 0x0000032F
		protected SR()
		{
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00004978 File Offset: 0x00002B78
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x0000497F File Offset: 0x00002B7F
		public static CultureInfo Culture
		{
			get
			{
				return SR.Keys.Culture;
			}
			set
			{
				SR.Keys.Culture = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00004987 File Offset: 0x00002B87
		public static string ERROR_ContentInvalid
		{
			get
			{
				return SR.Keys.GetString("ERROR_ContentInvalid");
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00004993 File Offset: 0x00002B93
		public static string ERROR_MissingParameter
		{
			get
			{
				return SR.Keys.GetString("ERROR_MissingParameter");
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000499F File Offset: 0x00002B9F
		public static string ERROR_BadParameter
		{
			get
			{
				return SR.Keys.GetString("ERROR_BadParameter");
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x000049AB File Offset: 0x00002BAB
		public static string ERROR_PathStart
		{
			get
			{
				return SR.Keys.GetString("ERROR_PathStart");
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x000049B7 File Offset: 0x00002BB7
		public static string ERROR_MissingServerCredentials
		{
			get
			{
				return SR.Keys.GetString("ERROR_MissingServerCredentials");
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x000049C3 File Offset: 0x00002BC3
		public static string PARAM_DataSourceType
		{
			get
			{
				return SR.Keys.GetString("PARAM_DataSourceType");
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x000049CF File Offset: 0x00002BCF
		public static string PARAM_ConnectionType
		{
			get
			{
				return SR.Keys.GetString("PARAM_ConnectionType");
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x000049DB File Offset: 0x00002BDB
		public static string PARAM_UserId
		{
			get
			{
				return SR.Keys.GetString("PARAM_UserId");
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x000049E7 File Offset: 0x00002BE7
		public static string PARAM_ServerUserId
		{
			get
			{
				return SR.Keys.GetString("PARAM_ServerUserId");
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x000049F3 File Offset: 0x00002BF3
		public static string PARAM_ServerPassword
		{
			get
			{
				return SR.Keys.GetString("PARAM_ServerPassword");
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x000049FF File Offset: 0x00002BFF
		public static string PARAM_Path
		{
			get
			{
				return SR.Keys.GetString("PARAM_Path");
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00004A0B File Offset: 0x00002C0B
		public static string PARAM_UserCredentials
		{
			get
			{
				return SR.Keys.GetString("PARAM_UserCredentials");
			}
		}

		// Token: 0x020000D0 RID: 208
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x060005D3 RID: 1491 RVA: 0x0000212F File Offset: 0x0000032F
			private Keys()
			{
			}

			// Token: 0x1700023B RID: 571
			// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00004C8E File Offset: 0x00002E8E
			// (set) Token: 0x060005D5 RID: 1493 RVA: 0x00004C95 File Offset: 0x00002E95
			public static CultureInfo Culture
			{
				get
				{
					return SR.Keys._culture;
				}
				set
				{
					SR.Keys._culture = value;
				}
			}

			// Token: 0x060005D6 RID: 1494 RVA: 0x00004C9D File Offset: 0x00002E9D
			public static string GetString(string key)
			{
				return SR.Keys.resourceManager.GetString(key, SR.Keys._culture);
			}

			// Token: 0x04000346 RID: 838
			private static ResourceManager resourceManager = new ResourceManager(typeof(SR).FullName, typeof(SR).Module.Assembly);

			// Token: 0x04000347 RID: 839
			private static CultureInfo _culture = null;

			// Token: 0x04000348 RID: 840
			public const string ERROR_ContentInvalid = "ERROR_ContentInvalid";

			// Token: 0x04000349 RID: 841
			public const string ERROR_MissingParameter = "ERROR_MissingParameter";

			// Token: 0x0400034A RID: 842
			public const string ERROR_BadParameter = "ERROR_BadParameter";

			// Token: 0x0400034B RID: 843
			public const string ERROR_PathStart = "ERROR_PathStart";

			// Token: 0x0400034C RID: 844
			public const string ERROR_MissingServerCredentials = "ERROR_MissingServerCredentials";

			// Token: 0x0400034D RID: 845
			public const string PARAM_DataSourceType = "PARAM_DataSourceType";

			// Token: 0x0400034E RID: 846
			public const string PARAM_ConnectionType = "PARAM_ConnectionType";

			// Token: 0x0400034F RID: 847
			public const string PARAM_UserId = "PARAM_UserId";

			// Token: 0x04000350 RID: 848
			public const string PARAM_ServerUserId = "PARAM_ServerUserId";

			// Token: 0x04000351 RID: 849
			public const string PARAM_ServerPassword = "PARAM_ServerPassword";

			// Token: 0x04000352 RID: 850
			public const string PARAM_Path = "PARAM_Path";

			// Token: 0x04000353 RID: 851
			public const string PARAM_UserCredentials = "PARAM_UserCredentials";
		}
	}
}
