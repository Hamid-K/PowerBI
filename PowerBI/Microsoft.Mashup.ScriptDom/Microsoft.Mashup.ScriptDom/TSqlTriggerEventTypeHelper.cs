using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000070 RID: 112
	internal static class TSqlTriggerEventTypeHelper
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0000B370 File Offset: 0x00009570
		public static bool TryParseOption(string input, SqlVersion version, out EventNotificationEventType returnValue)
		{
			return TSqlTriggerEventTypeHelper.HelperInstance.TryParseOption(input, TSqlTriggerEventTypeHelper.HelperInstance.MapSqlVersionToSqlVersionFlags(version), out returnValue);
		}

		// Token: 0x040002A0 RID: 672
		private static readonly TriggerEventTypeHelper HelperInstance = TriggerEventTypeHelper.Instance;
	}
}
