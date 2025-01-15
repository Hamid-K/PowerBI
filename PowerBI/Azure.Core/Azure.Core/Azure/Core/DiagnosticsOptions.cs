using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000043 RID: 67
	[NullableContext(2)]
	[Nullable(0)]
	public class DiagnosticsOptions
	{
		// Token: 0x060001DD RID: 477 RVA: 0x00005F2F File Offset: 0x0000412F
		protected internal DiagnosticsOptions()
			: this(ClientOptions.Default.Diagnostics)
		{
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00005F44 File Offset: 0x00004144
		internal DiagnosticsOptions(DiagnosticsOptions diagnosticsOptions)
		{
			if (diagnosticsOptions != null)
			{
				this.ApplicationId = diagnosticsOptions.ApplicationId;
				this.IsLoggingEnabled = diagnosticsOptions.IsLoggingEnabled;
				this.IsTelemetryEnabled = diagnosticsOptions.IsTelemetryEnabled;
				this.LoggedHeaderNames = new List<string>(diagnosticsOptions.LoggedHeaderNames);
				this.LoggedQueryParameters = new List<string>(diagnosticsOptions.LoggedQueryParameters);
				this.LoggedContentSizeLimit = diagnosticsOptions.LoggedContentSizeLimit;
				this.IsDistributedTracingEnabled = diagnosticsOptions.IsDistributedTracingEnabled;
				this.IsLoggingContentEnabled = diagnosticsOptions.IsLoggingContentEnabled;
				return;
			}
			this.LoggedHeaderNames = new List<string>
			{
				"x-ms-request-id", "x-ms-client-request-id", "x-ms-return-client-request-id", "traceparent", "MS-CV", "Accept", "Cache-Control", "Connection", "Content-Length", "Content-Type",
				"Date", "ETag", "Expires", "If-Match", "If-Modified-Since", "If-None-Match", "If-Unmodified-Since", "Last-Modified", "Pragma", "Request-Id",
				"Retry-After", "Server", "Transfer-Encoding", "User-Agent", "WWW-Authenticate"
			};
			this.LoggedQueryParameters = new List<string> { "api-version" };
			this.IsTelemetryEnabled = (!DiagnosticsOptions.EnvironmentVariableToBool(Environment.GetEnvironmentVariable("AZURE_TELEMETRY_DISABLED"))) ?? true;
			this.IsDistributedTracingEnabled = (!DiagnosticsOptions.EnvironmentVariableToBool(Environment.GetEnvironmentVariable("AZURE_TRACING_DISABLED"))) ?? true;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000061AC File Offset: 0x000043AC
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x000061B4 File Offset: 0x000043B4
		public bool IsLoggingEnabled { get; set; } = true;

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000061BD File Offset: 0x000043BD
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x000061C5 File Offset: 0x000043C5
		public bool IsDistributedTracingEnabled { get; set; } = true;

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000061CE File Offset: 0x000043CE
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x000061D6 File Offset: 0x000043D6
		public bool IsTelemetryEnabled { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000061DF File Offset: 0x000043DF
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x000061E7 File Offset: 0x000043E7
		public bool IsLoggingContentEnabled { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000061F0 File Offset: 0x000043F0
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x000061F8 File Offset: 0x000043F8
		public int LoggedContentSizeLimit { get; set; } = 4096;

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00006201 File Offset: 0x00004401
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00006209 File Offset: 0x00004409
		[Nullable(1)]
		public IList<string> LoggedHeaderNames
		{
			[NullableContext(1)]
			get;
			[NullableContext(1)]
			internal set;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00006212 File Offset: 0x00004412
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000621A File Offset: 0x0000441A
		[Nullable(1)]
		public IList<string> LoggedQueryParameters
		{
			[NullableContext(1)]
			get;
			[NullableContext(1)]
			internal set;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00006223 File Offset: 0x00004423
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000622B File Offset: 0x0000442B
		public string ApplicationId
		{
			get
			{
				return this._applicationId;
			}
			set
			{
				if (value != null && value.Length > 24)
				{
					throw new ArgumentOutOfRangeException("value", string.Format("{0} must be shorter than {1} characters", "ApplicationId", 25));
				}
				this._applicationId = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00006262 File Offset: 0x00004462
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00006273 File Offset: 0x00004473
		public static string DefaultApplicationId
		{
			get
			{
				return ClientOptions.Default.Diagnostics.ApplicationId;
			}
			set
			{
				ClientOptions.Default.Diagnostics.ApplicationId = value;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006288 File Offset: 0x00004488
		private static bool? EnvironmentVariableToBool(string value)
		{
			if (string.Equals(bool.TrueString, value, StringComparison.OrdinalIgnoreCase) || string.Equals("1", value, StringComparison.OrdinalIgnoreCase))
			{
				return new bool?(true);
			}
			if (string.Equals(bool.FalseString, value, StringComparison.OrdinalIgnoreCase) || string.Equals("0", value, StringComparison.OrdinalIgnoreCase))
			{
				return new bool?(false);
			}
			return null;
		}

		// Token: 0x040000DA RID: 218
		private const int MaxApplicationIdLength = 24;

		// Token: 0x040000DB RID: 219
		private string _applicationId;
	}
}
