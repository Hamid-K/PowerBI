using System;
using System.Text;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.ApiConfig.Parameters
{
	// Token: 0x020002D6 RID: 726
	internal class AcquireTokenForClientParameters : AbstractAcquireTokenConfidentialClientParameters, IAcquireTokenParameters
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x00057022 File Offset: 0x00055222
		// (set) Token: 0x06001B03 RID: 6915 RVA: 0x0005702A File Offset: 0x0005522A
		public bool ForceRefresh { get; set; }

		// Token: 0x06001B04 RID: 6916 RVA: 0x00057034 File Offset: 0x00055234
		public void LogParameters(ILoggerAdapter logger)
		{
			if (logger.IsLoggingEnabled(LogLevel.Info))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("=== AcquireTokenForClientParameters ===");
				stringBuilder.AppendLine("SendX5C: " + base.SendX5C.ToString());
				stringBuilder.AppendLine("ForceRefresh: " + this.ForceRefresh.ToString());
				logger.Info(stringBuilder.ToString());
			}
		}
	}
}
