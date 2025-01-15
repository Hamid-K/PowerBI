using System;

namespace System.Text.Json
{
	// Token: 0x02000037 RID: 55
	internal static class AppContextSwitchHelper
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000640A File Offset: 0x0000460A
		public static bool IsSourceGenReflectionFallbackEnabled { get; }

		// Token: 0x0600025E RID: 606 RVA: 0x00006414 File Offset: 0x00004614
		// Note: this type is marked as 'beforefieldinit'.
		static AppContextSwitchHelper()
		{
			bool flag;
			AppContextSwitchHelper.IsSourceGenReflectionFallbackEnabled = AppContext.TryGetSwitch("System.Text.Json.Serialization.EnableSourceGenReflectionFallback", out flag) && flag;
		}
	}
}
