using System;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Reflection;

namespace Microsoft.IdentityModel.Logging
{
	// Token: 0x02000006 RID: 6
	[EventSource(Name = "Microsoft.IdentityModel.EventSource")]
	public class IdentityModelEventSource : EventSource
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002161 File Offset: 0x00000361
		private IdentityModelEventSource()
		{
			this.LogLevel = EventLevel.Warning;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002170 File Offset: 0x00000370
		public static IdentityModelEventSource Logger { get; } = new IdentityModelEventSource();

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002177 File Offset: 0x00000377
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000217E File Offset: 0x0000037E
		public static bool ShowPII { get; set; } = false;

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002186 File Offset: 0x00000386
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000218D File Offset: 0x0000038D
		public static bool LogCompleteSecurityArtifact { get; set; } = false;

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002195 File Offset: 0x00000395
		public static string HiddenPIIString { get; } = "[PII of type '{0}' is hidden. For more details, see https://aka.ms/IdentityModel/PII.]";

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000219C File Offset: 0x0000039C
		public static string HiddenSecurityArtifactString { get; } = "[Security Artifact of type '{0}' is hidden. For more details, see https://aka.ms/IdentityModel/SecurityArtifactLogging.]";

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000021A3 File Offset: 0x000003A3
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000021AA File Offset: 0x000003AA
		public static bool HeaderWritten { get; set; } = false;

		// Token: 0x0600001D RID: 29 RVA: 0x000021B2 File Offset: 0x000003B2
		[Event(6, Level = EventLevel.LogAlways)]
		public void WriteAlways(string message)
		{
			if (base.IsEnabled())
			{
				message = IdentityModelEventSource.PrepareMessage(EventLevel.LogAlways, message, Array.Empty<object>());
				base.WriteEvent(6, message);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000021D2 File Offset: 0x000003D2
		[NonEvent]
		public void WriteAlways(string message, params object[] args)
		{
			if (base.IsEnabled())
			{
				if (args != null)
				{
					this.WriteAlways(LogHelper.FormatInvariant(message, args));
					return;
				}
				this.WriteAlways(message);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000021F4 File Offset: 0x000003F4
		[Event(1, Level = EventLevel.Verbose)]
		public void WriteVerbose(string message)
		{
			if (base.IsEnabled() && this.LogLevel >= EventLevel.Verbose)
			{
				message = IdentityModelEventSource.PrepareMessage(EventLevel.Verbose, message, Array.Empty<object>());
				base.WriteEvent(1, message);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000221D File Offset: 0x0000041D
		[NonEvent]
		public void WriteVerbose(string message, params object[] args)
		{
			if (base.IsEnabled() && this.LogLevel >= EventLevel.Verbose)
			{
				if (args != null)
				{
					this.WriteVerbose(LogHelper.FormatInvariant(message, args));
					return;
				}
				this.WriteVerbose(message);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002248 File Offset: 0x00000448
		[Event(2, Level = EventLevel.Informational)]
		public void WriteInformation(string message)
		{
			if (base.IsEnabled() && this.LogLevel >= EventLevel.Informational)
			{
				message = IdentityModelEventSource.PrepareMessage(EventLevel.Informational, message, Array.Empty<object>());
				base.WriteEvent(2, message);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002271 File Offset: 0x00000471
		[NonEvent]
		public void WriteInformation(string message, params object[] args)
		{
			if (base.IsEnabled() && this.LogLevel >= EventLevel.Informational)
			{
				if (args != null)
				{
					this.WriteInformation(LogHelper.FormatInvariant(message, args));
					return;
				}
				this.WriteInformation(message);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000229C File Offset: 0x0000049C
		[Event(3, Level = EventLevel.Warning)]
		public void WriteWarning(string message)
		{
			if (base.IsEnabled() && this.LogLevel >= EventLevel.Warning)
			{
				message = IdentityModelEventSource.PrepareMessage(EventLevel.Warning, message, Array.Empty<object>());
				base.WriteEvent(3, message);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000022C5 File Offset: 0x000004C5
		[NonEvent]
		public void WriteWarning(string message, params object[] args)
		{
			if (args != null)
			{
				this.WriteWarning(LogHelper.FormatInvariant(message, args));
				return;
			}
			this.WriteWarning(message);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000022DF File Offset: 0x000004DF
		[Event(4, Level = EventLevel.Error)]
		public void WriteError(string message)
		{
			if (base.IsEnabled() && this.LogLevel >= EventLevel.Error)
			{
				message = IdentityModelEventSource.PrepareMessage(EventLevel.Error, message, Array.Empty<object>());
				base.WriteEvent(4, message);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002308 File Offset: 0x00000508
		[NonEvent]
		public void WriteError(string message, params object[] args)
		{
			if (base.IsEnabled() && this.LogLevel >= EventLevel.Error)
			{
				if (args != null)
				{
					this.WriteError(LogHelper.FormatInvariant(message, args));
					return;
				}
				this.WriteError(message);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002333 File Offset: 0x00000533
		[Event(5, Level = EventLevel.Critical)]
		public void WriteCritical(string message)
		{
			if (base.IsEnabled() && this.LogLevel >= EventLevel.Critical)
			{
				message = IdentityModelEventSource.PrepareMessage(EventLevel.Critical, message, Array.Empty<object>());
				base.WriteEvent(5, message);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000235C File Offset: 0x0000055C
		[NonEvent]
		public void WriteCritical(string message, params object[] args)
		{
			if (base.IsEnabled() && this.LogLevel >= EventLevel.Critical)
			{
				if (args != null)
				{
					this.WriteCritical(LogHelper.FormatInvariant(message, args));
					return;
				}
				this.WriteCritical(message);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002387 File Offset: 0x00000587
		[NonEvent]
		public void Write(EventLevel level, Exception innerException, string message)
		{
			this.Write(level, innerException, message, null);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002394 File Offset: 0x00000594
		[NonEvent]
		public void Write(EventLevel level, Exception innerException, string message, params object[] args)
		{
			if (innerException != null)
			{
				if (!IdentityModelEventSource.ShowPII && !LogHelper.IsCustomException(innerException))
				{
					message = string.Format(CultureInfo.InvariantCulture, "Message: {0}, InnerException: {1}", message, innerException.GetType());
				}
				else
				{
					message = string.Format(CultureInfo.InvariantCulture, "Message: {0}, InnerException: {1}", message, innerException.Message);
				}
			}
			if (!IdentityModelEventSource.HeaderWritten)
			{
				this.WriteAlways(string.Format(CultureInfo.InvariantCulture, IdentityModelEventSource._versionLogMessage, typeof(IdentityModelEventSource).GetTypeInfo().Assembly.GetName().Version.ToString()));
				this.WriteAlways(string.Format(CultureInfo.InvariantCulture, IdentityModelEventSource._dateLogMessage, DateTime.UtcNow));
				if (IdentityModelEventSource.ShowPII)
				{
					this.WriteAlways(IdentityModelEventSource._piiOnLogMessage);
				}
				else
				{
					this.WriteAlways(IdentityModelEventSource._piiOffLogMessage);
				}
				IdentityModelEventSource.HeaderWritten = true;
			}
			switch (level)
			{
			case EventLevel.LogAlways:
				this.WriteAlways(message, args);
				return;
			case EventLevel.Critical:
				this.WriteCritical(message, args);
				return;
			case EventLevel.Error:
				this.WriteError(message, args);
				return;
			case EventLevel.Warning:
				this.WriteWarning(message, args);
				return;
			case EventLevel.Informational:
				this.WriteInformation(message, args);
				return;
			case EventLevel.Verbose:
				this.WriteVerbose(message, args);
				return;
			default:
				this.WriteError(LogHelper.FormatInvariant("MIML10002: Unknown log level: {0}.", new object[] { level }));
				this.WriteError(message, args);
				return;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000024EC File Offset: 0x000006EC
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000024F4 File Offset: 0x000006F4
		public EventLevel LogLevel { get; set; }

		// Token: 0x0600002D RID: 45 RVA: 0x00002500 File Offset: 0x00000700
		private static string PrepareMessage(EventLevel level, string message, params object[] args)
		{
			if (message == null)
			{
				return string.Empty;
			}
			try
			{
				if (args != null && args.Length != 0)
				{
					return string.Format(CultureInfo.InvariantCulture, "[{0}]{1} {2}", level.ToString(), DateTime.UtcNow.ToString(CultureInfo.InvariantCulture), LogHelper.FormatInvariant(message, args));
				}
				return string.Format(CultureInfo.InvariantCulture, "[{0}]{1} {2}", level.ToString(), DateTime.UtcNow.ToString(CultureInfo.InvariantCulture), message);
			}
			catch
			{
			}
			string text;
			try
			{
				text = LogHelper.FormatInvariant("[{0}]{1} {2}", new object[]
				{
					level.ToString(),
					DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
					message
				});
			}
			catch (Exception)
			{
				text = level.ToString() + DateTime.UtcNow.ToString(CultureInfo.InvariantCulture) + message;
			}
			return text;
		}

		// Token: 0x04000021 RID: 33
		private static string _versionLogMessage = "Library version: {0}.";

		// Token: 0x04000022 RID: 34
		private static string _dateLogMessage = "Date: {0}.";

		// Token: 0x04000023 RID: 35
		private static string _piiOffLogMessage = "PII (personally identifiable information) logging is currently turned off. Set IdentityModelEventSource.ShowPII to 'true' to view the full details of exceptions.";

		// Token: 0x04000024 RID: 36
		private static string _piiOnLogMessage = "PII (personally identifiable information) logging is currently turned on. Set IdentityModelEventSource.ShowPII to 'false' to hide PII from log messages.";
	}
}
