using System;

namespace Microsoft.Identity.Client.TelemetryCore
{
	// Token: 0x020001E1 RID: 481
	internal static class TelemetryConstants
	{
		// Token: 0x04000871 RID: 2161
		public const char HttpTelemetrySchemaVersion = '5';

		// Token: 0x04000872 RID: 2162
		public const char HttpTelemetryPipe = '|';

		// Token: 0x04000873 RID: 2163
		public const string XClientCurrentTelemetry = "x-client-current-telemetry";

		// Token: 0x04000874 RID: 2164
		public const string XClientLastTelemetry = "x-client-last-telemetry";

		// Token: 0x04000875 RID: 2165
		public const string False = "false";

		// Token: 0x04000876 RID: 2166
		public const string True = "true";

		// Token: 0x04000877 RID: 2167
		public const char One = '1';

		// Token: 0x04000878 RID: 2168
		public const char Zero = '0';

		// Token: 0x04000879 RID: 2169
		public const char CommaDelimiter = ',';

		// Token: 0x0400087A RID: 2170
		public const string PlatformFields = "platform_fields";

		// Token: 0x0400087B RID: 2171
		public const string AcquireTokenEventName = "acquire_token";

		// Token: 0x0400087C RID: 2172
		public const string ConfigurationUpdateEventName = "config_update";

		// Token: 0x0400087D RID: 2173
		public const string MsalVersion = "MsalVersion";

		// Token: 0x0400087E RID: 2174
		public const string RemainingLifetime = "RemainingLifetime";

		// Token: 0x0400087F RID: 2175
		public const string TokenType = "TokenType";

		// Token: 0x04000880 RID: 2176
		public const string TokenSource = "TokenSource";

		// Token: 0x04000881 RID: 2177
		public const string CacheInfoTelemetry = "CacheInfoTelemetry";

		// Token: 0x04000882 RID: 2178
		public const string CacheRefreshReason = "CacheRefreshReason";

		// Token: 0x04000883 RID: 2179
		public const string ErrorCode = "ErrorCode";

		// Token: 0x04000884 RID: 2180
		public const string StsErrorCode = "StsErrorCode";

		// Token: 0x04000885 RID: 2181
		public const string ErrorMessage = "ErrorMessage";

		// Token: 0x04000886 RID: 2182
		public const string Duration = "Duration";

		// Token: 0x04000887 RID: 2183
		public const string DurationInUs = "DurationInUs";

		// Token: 0x04000888 RID: 2184
		public const string Succeeded = "Succeeded";

		// Token: 0x04000889 RID: 2185
		public const string DurationInCache = "DurationInCache";

		// Token: 0x0400088A RID: 2186
		public const string DurationInHttp = "DurationInHttp";

		// Token: 0x0400088B RID: 2187
		public const string ActivityId = "ActivityId";

		// Token: 0x0400088C RID: 2188
		public const string Resource = "Resource";

		// Token: 0x0400088D RID: 2189
		public const string RefreshOn = "RefreshOn";

		// Token: 0x0400088E RID: 2190
		public const string CacheLevel = "CacheLevel";

		// Token: 0x0400088F RID: 2191
		public const string AssertionType = "AssertionType";

		// Token: 0x04000890 RID: 2192
		public const string Endpoint = "Endpoint";

		// Token: 0x04000891 RID: 2193
		public const string Scopes = "Scopes";

		// Token: 0x04000892 RID: 2194
		public const string ClientId = "ClientId";

		// Token: 0x04000893 RID: 2195
		public const string Platform = "Platform";

		// Token: 0x04000894 RID: 2196
		public const string ApiId = "ApiId";

		// Token: 0x04000895 RID: 2197
		public const string IsProactiveRefresh = "IsProactiveRefresh";
	}
}
