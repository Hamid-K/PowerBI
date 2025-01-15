using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200003E RID: 62
	public static class ProvisioningConstants
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000EB16 File Offset: 0x0000CD16
		public static int DefaultDatabaseExpirationIntervalInMinutes
		{
			get
			{
				return 40;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000EB1A File Offset: 0x0000CD1A
		public static string LegacyWowVirtualServer
		{
			get
			{
				return "wowvirtualserver";
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000EB21 File Offset: 0x0000CD21
		public static string WowSubscription
		{
			get
			{
				return "wowsubscription";
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000EB28 File Offset: 0x0000CD28
		public static string WowAuthorityId
		{
			get
			{
				return "wowauthorityid";
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000EB2F File Offset: 0x0000CD2F
		public static DateTime WowDefaultDate
		{
			get
			{
				return new DateTime(2014, 6, 1);
			}
		}

		// Token: 0x040000E5 RID: 229
		public static readonly string SERVICE_GROUP_NAME_NO_SERVICE = "00000000-0000-0000-0000-000000000000";

		// Token: 0x040000E6 RID: 230
		public const string PBI_PLACEHOLDER_DATABASE_IDENTIFIER = "pbi_placeholder_db_9710bd19-7bc8-481a-b343-b2451f104323";
	}
}
