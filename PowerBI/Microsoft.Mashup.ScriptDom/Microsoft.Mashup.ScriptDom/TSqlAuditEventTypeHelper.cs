using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000072 RID: 114
	internal static class TSqlAuditEventTypeHelper
	{
		// Token: 0x06000292 RID: 658 RVA: 0x0000B3BA File Offset: 0x000095BA
		public static bool TryParseOption(string input, SqlVersion version, out EventNotificationEventType returnValue)
		{
			return TSqlAuditEventTypeHelper.HelperInstance.TryParseOption(input, TSqlAuditEventTypeHelper.HelperInstance.MapSqlVersionToSqlVersionFlags(version), out returnValue);
		}

		// Token: 0x040002A2 RID: 674
		private static readonly AuditEventTypeHelper HelperInstance = AuditEventTypeHelper.Instance;
	}
}
