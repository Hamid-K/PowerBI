using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x02000007 RID: 7
	internal static class ProviderSpecificProperties
	{
		// Token: 0x0200000B RID: 11
		internal enum MSOLAP : uint
		{
			// Token: 0x04000012 RID: 18
			CUBE = 4158U,
			// Token: 0x04000013 RID: 19
			EFFECTIVE_USER_NAME = 4166U,
			// Token: 0x04000014 RID: 20
			CUSTOMDATA = 4169U,
			// Token: 0x04000015 RID: 21
			ACTIVITYID = 4181U,
			// Token: 0x04000016 RID: 22
			REQUESTID,
			// Token: 0x04000017 RID: 23
			REQUESTMEMORYLIMIT = 4209U,
			// Token: 0x04000018 RID: 24
			CURRENTACTIVITYID,
			// Token: 0x04000019 RID: 25
			MDXCOMPATIBILITY = 4118U,
			// Token: 0x0400001A RID: 26
			SAFETY_OPTIONS = 4133U,
			// Token: 0x0400001B RID: 27
			SOURCE_DSN = 4099U,
			// Token: 0x0400001C RID: 28
			SOURCE_DSN_SUFFIX = 4106U,
			// Token: 0x0400001D RID: 29
			MDX_MISSING_MEMBER_MODE = 4167U,
			// Token: 0x0400001E RID: 30
			SUBQUERIES = 4173U,
			// Token: 0x0400001F RID: 31
			UPDATE_ISOLATION_LEVEL = 4172U,
			// Token: 0x04000020 RID: 32
			CERTIFICATE = 4202U,
			// Token: 0x04000021 RID: 33
			DMTSCONNECTIONDETAILS = 4216U,
			// Token: 0x04000022 RID: 34
			GATEWAY = 4220U,
			// Token: 0x04000023 RID: 35
			REQUESTPRIORITY,
			// Token: 0x04000024 RID: 36
			NESTINGLEVEL = 4228U,
			// Token: 0x04000025 RID: 37
			EXECUTIONMETRICS = 4224U,
			// Token: 0x04000026 RID: 38
			REQUESTLABEL = 4227U,
			// Token: 0x04000027 RID: 39
			APPLICATIONCONTEXT = 4225U,
			// Token: 0x04000028 RID: 40
			FLATTENED2 = 4098U
		}
	}
}
