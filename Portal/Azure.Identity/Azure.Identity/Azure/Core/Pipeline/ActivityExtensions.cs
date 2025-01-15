using System;

namespace Azure.Core.Pipeline
{
	// Token: 0x0200001B RID: 27
	internal static class ActivityExtensions
	{
		// Token: 0x0600007D RID: 125 RVA: 0x0000396B File Offset: 0x00001B6B
		static ActivityExtensions()
		{
			ActivityExtensions.ResetFeatureSwitch();
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003972 File Offset: 0x00001B72
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003979 File Offset: 0x00001B79
		public static bool SupportsActivitySource { get; private set; }

		// Token: 0x06000080 RID: 128 RVA: 0x00003981 File Offset: 0x00001B81
		public static void ResetFeatureSwitch()
		{
			ActivityExtensions.SupportsActivitySource = AppContextSwitchHelper.GetConfigValue("Azure.Experimental.EnableActivitySource", "AZURE_EXPERIMENTAL_ENABLE_ACTIVITY_SOURCE");
		}
	}
}
