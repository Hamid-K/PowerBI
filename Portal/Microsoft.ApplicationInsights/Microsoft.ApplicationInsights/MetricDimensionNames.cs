using System;
using Microsoft.ApplicationInsights.Metrics;

namespace Microsoft.ApplicationInsights
{
	// Token: 0x02000021 RID: 33
	public static class MetricDimensionNames
	{
		// Token: 0x020000E9 RID: 233
		public static class TelemetryContext
		{
			// Token: 0x06000884 RID: 2180 RVA: 0x0001B75B File Offset: 0x0001995B
			internal static string Property(string propertyName)
			{
				Util.ValidateNotNullOrWhitespace(propertyName, "propertyName");
				return "TelemetryContext.Property_" + propertyName + "_";
			}

			// Token: 0x06000885 RID: 2181 RVA: 0x0001B778 File Offset: 0x00019978
			internal static bool IsProperty(string dimensionName, out string propertyName)
			{
				propertyName = null;
				if (string.IsNullOrWhiteSpace(dimensionName))
				{
					return false;
				}
				if (!dimensionName.StartsWith("TelemetryContext.Property_", StringComparison.Ordinal))
				{
					return false;
				}
				if (!dimensionName.EndsWith("_", StringComparison.Ordinal))
				{
					return false;
				}
				propertyName = dimensionName.Substring("TelemetryContext.Property_".Length);
				propertyName = propertyName.Substring(0, propertyName.Length - "_".Length);
				return true;
			}

			// Token: 0x04000343 RID: 835
			public const string InstrumentationKey = "TelemetryContext.InstrumentationKey";

			// Token: 0x04000344 RID: 836
			private const string TelemetryContextPrefix = "TelemetryContext.";

			// Token: 0x04000345 RID: 837
			private const string PropertyPrefix = "TelemetryContext.Property_";

			// Token: 0x04000346 RID: 838
			private const string PropertyPostfix = "_";

			// Token: 0x02000131 RID: 305
			public static class Cloud
			{
				// Token: 0x04000456 RID: 1110
				public const string RoleInstance = "TelemetryContext.Cloud.RoleInstance";

				// Token: 0x04000457 RID: 1111
				public const string RoleName = "TelemetryContext.Cloud.RoleName";

				// Token: 0x04000458 RID: 1112
				private const string CloudPrefix = "TelemetryContext.Cloud.";
			}

			// Token: 0x02000132 RID: 306
			public static class Component
			{
				// Token: 0x04000459 RID: 1113
				public const string Version = "TelemetryContext.Component.Version";

				// Token: 0x0400045A RID: 1114
				private const string ComponentPrefix = "TelemetryContext.Component.";
			}

			// Token: 0x02000133 RID: 307
			public static class Device
			{
				// Token: 0x0400045B RID: 1115
				public const string Id = "TelemetryContext.Device.Id";

				// Token: 0x0400045C RID: 1116
				public const string Language = "TelemetryContext.Device.Language";

				// Token: 0x0400045D RID: 1117
				public const string Model = "TelemetryContext.Device.Model";

				// Token: 0x0400045E RID: 1118
				public const string NetworkType = "TelemetryContext.Device.NetworkType";

				// Token: 0x0400045F RID: 1119
				public const string OemName = "TelemetryContext.Device.OemName";

				// Token: 0x04000460 RID: 1120
				public const string OperatingSystem = "TelemetryContext.Device.OperatingSystem";

				// Token: 0x04000461 RID: 1121
				public const string ScreenResolution = "TelemetryContext.Device.ScreenResolution";

				// Token: 0x04000462 RID: 1122
				public const string Type = "TelemetryContext.Device.Type";

				// Token: 0x04000463 RID: 1123
				private const string DevicePrefix = "TelemetryContext.Device.";
			}

			// Token: 0x02000134 RID: 308
			public static class Location
			{
				// Token: 0x04000464 RID: 1124
				public const string Ip = "TelemetryContext.Location.Ip";

				// Token: 0x04000465 RID: 1125
				private const string LocationPrefix = "TelemetryContext.Location.";
			}

			// Token: 0x02000135 RID: 309
			public static class Operation
			{
				// Token: 0x04000466 RID: 1126
				public const string CorrelationVector = "TelemetryContext.Operation.CorrelationVector";

				// Token: 0x04000467 RID: 1127
				public const string Id = "TelemetryContext.Operation.Id";

				// Token: 0x04000468 RID: 1128
				public const string Name = "TelemetryContext.Operation.Name";

				// Token: 0x04000469 RID: 1129
				public const string ParentId = "TelemetryContext.Operation.ParentId";

				// Token: 0x0400046A RID: 1130
				public const string SyntheticSource = "TelemetryContext.Operation.SyntheticSource";

				// Token: 0x0400046B RID: 1131
				private const string OperationPrefix = "TelemetryContext.Operation.";
			}

			// Token: 0x02000136 RID: 310
			public static class Session
			{
				// Token: 0x0400046C RID: 1132
				public const string Id = "TelemetryContext.Session.Id";

				// Token: 0x0400046D RID: 1133
				public const string IsFirst = "TelemetryContext.Session.IsFirst";

				// Token: 0x0400046E RID: 1134
				private const string SessionPrefix = "TelemetryContext.Session.";
			}

			// Token: 0x02000137 RID: 311
			public static class User
			{
				// Token: 0x0400046F RID: 1135
				public const string AccountId = "TelemetryContext.User.AccountId";

				// Token: 0x04000470 RID: 1136
				public const string AuthenticatedUserId = "TelemetryContext.User.AuthenticatedUserId";

				// Token: 0x04000471 RID: 1137
				public const string Id = "TelemetryContext.User.Id";

				// Token: 0x04000472 RID: 1138
				public const string UserAgent = "TelemetryContext.User.UserAgent";

				// Token: 0x04000473 RID: 1139
				private const string UserPrefix = "TelemetryContext.User.";
			}
		}
	}
}
