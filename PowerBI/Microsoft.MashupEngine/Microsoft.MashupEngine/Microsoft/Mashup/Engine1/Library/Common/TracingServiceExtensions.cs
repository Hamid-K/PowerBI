using System;
using System.Diagnostics;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001158 RID: 4440
	internal static class TracingServiceExtensions
	{
		// Token: 0x0600743F RID: 29759 RVA: 0x0018F55B File Offset: 0x0018D75B
		public static bool Enabled(this ITracingService service)
		{
			return service.Levels > SourceLevels.Off;
		}

		// Token: 0x06007440 RID: 29760 RVA: 0x0018F566 File Offset: 0x0018D766
		public static bool VerboseEnabled(this ITracingService service)
		{
			return (service.Levels & SourceLevels.Verbose) == SourceLevels.Verbose;
		}

		// Token: 0x06007441 RID: 29761 RVA: 0x0018F575 File Offset: 0x0018D775
		public static bool WarningEnabled(this ITracingService service)
		{
			return (service.Levels & SourceLevels.Warning) == SourceLevels.Warning;
		}

		// Token: 0x06007442 RID: 29762 RVA: 0x0018F582 File Offset: 0x0018D782
		public static bool Enabled(this IHostTrace trace)
		{
			return trace.Levels > SourceLevels.Off;
		}

		// Token: 0x06007443 RID: 29763 RVA: 0x0018F58D File Offset: 0x0018D78D
		public static bool VerboseEnabled(this IHostTrace trace)
		{
			return (trace.Levels & SourceLevels.Verbose) == SourceLevels.Verbose;
		}

		// Token: 0x06007444 RID: 29764 RVA: 0x0018F59C File Offset: 0x0018D79C
		public static bool WarningEnabled(this IHostTrace trace)
		{
			return (trace.Levels & SourceLevels.Warning) == SourceLevels.Warning;
		}

		// Token: 0x06007445 RID: 29765 RVA: 0x0018F5A9 File Offset: 0x0018D7A9
		public static bool Enabled(this IHostTrace trace, TraceEventType type)
		{
			return (trace.Levels & (SourceLevels)type) > SourceLevels.Off;
		}
	}
}
