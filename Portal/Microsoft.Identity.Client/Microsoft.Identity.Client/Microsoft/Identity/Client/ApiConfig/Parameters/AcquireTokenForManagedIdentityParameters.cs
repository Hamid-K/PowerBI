using System;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.ApiConfig.Parameters
{
	// Token: 0x020002D7 RID: 727
	internal class AcquireTokenForManagedIdentityParameters : IAcquireTokenParameters
	{
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x000570B4 File Offset: 0x000552B4
		// (set) Token: 0x06001B07 RID: 6919 RVA: 0x000570BC File Offset: 0x000552BC
		public bool ForceRefresh { get; set; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x000570C5 File Offset: 0x000552C5
		// (set) Token: 0x06001B09 RID: 6921 RVA: 0x000570CD File Offset: 0x000552CD
		public string Resource { get; set; }

		// Token: 0x06001B0A RID: 6922 RVA: 0x000570D6 File Offset: 0x000552D6
		public void LogParameters(ILoggerAdapter logger)
		{
			if (logger.IsLoggingEnabled(LogLevel.Info))
			{
				logger.Info(string.Format("=== AcquireTokenForManagedIdentityParameters ===\r\nForceRefresh: {0}\r\nResource: {1}", this.ForceRefresh, this.Resource));
			}
		}
	}
}
