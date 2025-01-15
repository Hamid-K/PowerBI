using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000071 RID: 113
	internal static class TSqlTriggerEventGroupHelper
	{
		// Token: 0x06000290 RID: 656 RVA: 0x0000B395 File Offset: 0x00009595
		public static bool TryParseOption(string input, SqlVersion version, out EventNotificationEventGroup returnValue)
		{
			return TSqlTriggerEventGroupHelper.HelperInstance.TryParseOption(input, TSqlTriggerEventGroupHelper.HelperInstance.MapSqlVersionToSqlVersionFlags(version), out returnValue);
		}

		// Token: 0x040002A1 RID: 673
		private static readonly TriggerEventGroupHelper HelperInstance = TriggerEventGroupHelper.Instance;
	}
}
