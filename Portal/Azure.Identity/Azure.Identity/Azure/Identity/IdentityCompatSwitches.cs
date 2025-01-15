using System;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x02000060 RID: 96
	internal class IdentityCompatSwitches
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000A9EF File Offset: 0x00008BEF
		public static bool DisableInteractiveBrowserThreadpoolExecution
		{
			get
			{
				return AppContextSwitchHelper.GetConfigValue("Azure.Identity.DisableInteractiveBrowserThreadpoolExecution", "AZURE_IDENTITY_DISABLE_INTERACTIVEBROWSERTHREADPOOLEXECUTION");
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000AA00 File Offset: 0x00008C00
		public static bool DisableCP1
		{
			get
			{
				return AppContextSwitchHelper.GetConfigValue("Azure.Identity.DisableCP1", "AZURE_IDENTITY_DISABLE_CP1");
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000AA11 File Offset: 0x00008C11
		public static bool DisableTenantDiscovery
		{
			get
			{
				return AppContextSwitchHelper.GetConfigValue("Azure.Identity.DisableMultiTenantAuth", "AZURE_IDENTITY_DISABLE_MULTITENANTAUTH");
			}
		}

		// Token: 0x0400020B RID: 523
		internal const string DisableInteractiveThreadpoolExecutionSwitchName = "Azure.Identity.DisableInteractiveBrowserThreadpoolExecution";

		// Token: 0x0400020C RID: 524
		internal const string DisableInteractiveThreadpoolExecutionEnvVar = "AZURE_IDENTITY_DISABLE_INTERACTIVEBROWSERTHREADPOOLEXECUTION";

		// Token: 0x0400020D RID: 525
		internal const string DisableCP1ExecutionSwitchName = "Azure.Identity.DisableCP1";

		// Token: 0x0400020E RID: 526
		internal const string DisableCP1ExecutionEnvVar = "AZURE_IDENTITY_DISABLE_CP1";

		// Token: 0x0400020F RID: 527
		internal const string DisableMultiTenantAuthSwitchName = "Azure.Identity.DisableMultiTenantAuth";

		// Token: 0x04000210 RID: 528
		internal const string DisableMultiTenantAuthEnvVar = "AZURE_IDENTITY_DISABLE_MULTITENANTAUTH";
	}
}
