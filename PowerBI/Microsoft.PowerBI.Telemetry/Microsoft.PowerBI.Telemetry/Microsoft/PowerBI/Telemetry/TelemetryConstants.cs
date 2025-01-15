using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000024 RID: 36
	public static class TelemetryConstants
	{
		// Token: 0x04000065 RID: 101
		public const string NullGuid = "00000000-0000-0000-0000-000000000000";

		// Token: 0x04000066 RID: 102
		public const string Application = "WinDesktop";

		// Token: 0x04000067 RID: 103
		public const string Cluster = "InProc";

		// Token: 0x04000068 RID: 104
		public const string AnalysisServicesActivityPrefix = "PBI.AS.";

		// Token: 0x04000069 RID: 105
		public const string UtcFormatString = "O";

		// Token: 0x0400006A RID: 106
		public const string ClientDateFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffZ";

		// Token: 0x0400006B RID: 107
		public const string ClientEndWithErrorEventName = "EndWithError";

		// Token: 0x0400006C RID: 108
		public const string ClientEndEventName = "End";

		// Token: 0x0400006D RID: 109
		public const string ClientPropertyName = "client";

		// Token: 0x0400006E RID: 110
		public const string BuildPropertyName = "build";

		// Token: 0x0400006F RID: 111
		public const string EventAuthenticatedUserId = "authenticatedUserId";

		// Token: 0x04000070 RID: 112
		public const string EventId = "id";

		// Token: 0x04000071 RID: 113
		public const string EventCategory = "category";

		// Token: 0x04000072 RID: 114
		public const string EventActivityType = "activityType";

		// Token: 0x04000073 RID: 115
		public const string EventParentId = "parentId";

		// Token: 0x04000074 RID: 116
		public const string EventClientId = "clientId";

		// Token: 0x04000075 RID: 117
		public const string EventRootId = "rootId";

		// Token: 0x04000076 RID: 118
		public const string EventEndTime = "end";

		// Token: 0x04000077 RID: 119
		public const string EventDuration = "duration";

		// Token: 0x04000078 RID: 120
		public const string EventIsInternalUser = "isInternal";

		// Token: 0x04000079 RID: 121
		public const string EventIsReturningUser = "isReturningUser";

		// Token: 0x0400007A RID: 122
		public const string EventSessionId = "sessionId";

		// Token: 0x0400007B RID: 123
		public const string EventDeviceId = "deviceId";

		// Token: 0x0400007C RID: 124
		public const string EventUserId = "userId";

		// Token: 0x0400007D RID: 125
		public const string EventErrorState = "isError";

		// Token: 0x0400007E RID: 126
		public const string EventError = "Error";

		// Token: 0x0400007F RID: 127
		public const string EventMessage = "message";

		// Token: 0x04000080 RID: 128
		public const string EventType = "type";

		// Token: 0x04000081 RID: 129
		public const string UiCultureName = "uiCulture";

		// Token: 0x04000082 RID: 130
		public const string ClientCultureName = "clientCulture";

		// Token: 0x04000083 RID: 131
		public const string SessionIndex = "c";

		// Token: 0x04000084 RID: 132
		public const string TelemetryConfigurationMutexName = "Global\\PBITelemetryConfigMutex";

		// Token: 0x04000085 RID: 133
		public const string AppDirectoryName = "Power BI Desktop";

		// Token: 0x04000086 RID: 134
		public const string AppDirectoryNameSSRS = "Power BI Desktop SSRS";

		// Token: 0x04000087 RID: 135
		public const string UserIdFileName = "conf_uid";

		// Token: 0x04000088 RID: 136
		public const string UnknownEventProperty = "Unknown";

		// Token: 0x04000089 RID: 137
		public const string EventTarget = "EventTarget";

		// Token: 0x0400008A RID: 138
		public const string RequestId = "RequestId";
	}
}
