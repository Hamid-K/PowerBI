using System;

namespace Azure.Core.Pipeline
{
	// Token: 0x020000A7 RID: 167
	internal static class ActivityExtensions
	{
		// Token: 0x0600053B RID: 1339 RVA: 0x0001005B File Offset: 0x0000E25B
		static ActivityExtensions()
		{
			ActivityExtensions.ResetFeatureSwitch();
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00010062 File Offset: 0x0000E262
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x00010069 File Offset: 0x0000E269
		public static bool SupportsActivitySource { get; private set; }

		// Token: 0x0600053E RID: 1342 RVA: 0x00010071 File Offset: 0x0000E271
		public static void ResetFeatureSwitch()
		{
			ActivityExtensions.SupportsActivitySource = AppContextSwitchHelper.GetConfigValue("Azure.Experimental.EnableActivitySource", "AZURE_EXPERIMENTAL_ENABLE_ACTIVITY_SOURCE");
		}
	}
}
