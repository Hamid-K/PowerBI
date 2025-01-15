using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000073 RID: 115
	internal static class TSqlAuditEventGroupHelper
	{
		// Token: 0x06000294 RID: 660 RVA: 0x0000B3DF File Offset: 0x000095DF
		public static bool TryParseOption(string input, SqlVersion version, out EventNotificationEventGroup returnValue)
		{
			return TSqlAuditEventGroupHelper.HelperInstance.TryParseOption(input, TSqlAuditEventGroupHelper.HelperInstance.MapSqlVersionToSqlVersionFlags(version), out returnValue);
		}

		// Token: 0x040002A3 RID: 675
		private static readonly AuditEventGroupHelper HelperInstance = AuditEventGroupHelper.Instance;
	}
}
