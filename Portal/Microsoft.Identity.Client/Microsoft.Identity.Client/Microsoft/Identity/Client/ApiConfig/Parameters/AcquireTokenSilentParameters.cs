using System;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.ApiConfig.Parameters
{
	// Token: 0x020002DA RID: 730
	internal class AcquireTokenSilentParameters : IAcquireTokenParameters
	{
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x0005738C File Offset: 0x0005558C
		// (set) Token: 0x06001B28 RID: 6952 RVA: 0x00057394 File Offset: 0x00055594
		public bool ForceRefresh { get; set; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0005739D File Offset: 0x0005559D
		// (set) Token: 0x06001B2A RID: 6954 RVA: 0x000573A5 File Offset: 0x000555A5
		public string LoginHint { get; set; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x000573AE File Offset: 0x000555AE
		// (set) Token: 0x06001B2C RID: 6956 RVA: 0x000573B6 File Offset: 0x000555B6
		public IAccount Account { get; set; }

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x000573BF File Offset: 0x000555BF
		// (set) Token: 0x06001B2E RID: 6958 RVA: 0x000573C7 File Offset: 0x000555C7
		public bool? SendX5C { get; set; }

		// Token: 0x06001B2F RID: 6959 RVA: 0x000573D0 File Offset: 0x000555D0
		public void LogParameters(ILoggerAdapter logger)
		{
			if (logger.IsLoggingEnabled(LogLevel.Info))
			{
				logger.Info("=== AcquireTokenSilent Parameters ===");
				logger.Info("LoginHint provided: " + (!string.IsNullOrEmpty(this.LoginHint)).ToString());
				logger.InfoPii("Account provided: " + ((this.Account != null) ? this.Account.ToString() : "false"), "Account provided: " + (this.Account != null).ToString());
				logger.Info("ForceRefresh: " + this.ForceRefresh.ToString());
			}
		}
	}
}
