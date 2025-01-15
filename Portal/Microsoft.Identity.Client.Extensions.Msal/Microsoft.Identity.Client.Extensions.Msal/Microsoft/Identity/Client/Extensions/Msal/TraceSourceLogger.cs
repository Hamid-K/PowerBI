using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x02000022 RID: 34
	public class TraceSourceLogger
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00004258 File Offset: 0x00002458
		public TraceSourceLogger(TraceSource traceSource)
		{
			this.Source = traceSource;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004267 File Offset: 0x00002467
		public TraceSource Source { get; }

		// Token: 0x06000099 RID: 153 RVA: 0x0000426F File Offset: 0x0000246F
		public void LogInformation(string message)
		{
			this.Source.TraceEvent(TraceEventType.Information, 0, TraceSourceLogger.FormatLogMessage(message));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004284 File Offset: 0x00002484
		public void LogError(string message)
		{
			this.Source.TraceEvent(TraceEventType.Error, 0, TraceSourceLogger.FormatLogMessage(message));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004299 File Offset: 0x00002499
		public void LogWarning(string message)
		{
			this.Source.TraceEvent(TraceEventType.Warning, 0, TraceSourceLogger.FormatLogMessage(message));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000042B0 File Offset: 0x000024B0
		private static string FormatLogMessage(string message)
		{
			return "[MSAL.Extension][" + DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture) + "] " + message;
		}
	}
}
