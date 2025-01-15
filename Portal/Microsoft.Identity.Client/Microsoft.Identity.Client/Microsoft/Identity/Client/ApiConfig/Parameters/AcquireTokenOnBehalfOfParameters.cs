using System;
using System.Text;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.ApiConfig.Parameters
{
	// Token: 0x020002D9 RID: 729
	internal class AcquireTokenOnBehalfOfParameters : AbstractAcquireTokenConfidentialClientParameters, IAcquireTokenParameters
	{
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0005726E File Offset: 0x0005546E
		// (set) Token: 0x06001B1E RID: 6942 RVA: 0x00057276 File Offset: 0x00055476
		public UserAssertion UserAssertion { get; set; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x0005727F File Offset: 0x0005547F
		// (set) Token: 0x06001B20 RID: 6944 RVA: 0x00057287 File Offset: 0x00055487
		public string LongRunningOboCacheKey { get; set; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001B21 RID: 6945 RVA: 0x00057290 File Offset: 0x00055490
		// (set) Token: 0x06001B22 RID: 6946 RVA: 0x00057298 File Offset: 0x00055498
		public bool SearchInCacheForLongRunningObo { get; set; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x000572A1 File Offset: 0x000554A1
		// (set) Token: 0x06001B24 RID: 6948 RVA: 0x000572A9 File Offset: 0x000554A9
		public bool ForceRefresh { get; set; }

		// Token: 0x06001B25 RID: 6949 RVA: 0x000572B4 File Offset: 0x000554B4
		public void LogParameters(ILoggerAdapter logger)
		{
			if (logger.IsLoggingEnabled(LogLevel.Info))
			{
				StringBuilder stringBuilder = new StringBuilder(string.Format("=== OnBehalfOfParameters ===\r\nSendX5C: {0}\r\nForceRefresh: {1}\r\nUserAssertion set: {2}\r\nSearchInCacheForLongRunningObo: {3}\r\nLongRunningOboCacheKey set: {4}", new object[]
				{
					base.SendX5C,
					this.ForceRefresh,
					this.UserAssertion != null,
					this.SearchInCacheForLongRunningObo,
					!string.IsNullOrWhiteSpace(this.LongRunningOboCacheKey)
				}));
				if (this.UserAssertion != null && !string.IsNullOrWhiteSpace(this.LongRunningOboCacheKey))
				{
					stringBuilder.AppendLine("InitiateLongRunningProcessInWebApi called: True");
				}
				else if (this.UserAssertion == null && !string.IsNullOrWhiteSpace(this.LongRunningOboCacheKey))
				{
					stringBuilder.AppendLine("AcquireTokenInLongRunningProcess called: True");
				}
				logger.Info(stringBuilder.ToString());
			}
		}
	}
}
