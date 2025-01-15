using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x0200007A RID: 122
	internal static class MonitoredEventHandlerVisitorExtensions
	{
		// Token: 0x060003A0 RID: 928 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		public static void Visit<T>(this IEnumerable<T> events, IMonitoredEventHandler monitoredEventHandler) where T : IMonitoredEventHandlerVisitor
		{
			foreach (T t in events)
			{
				t.Visit(monitoredEventHandler);
			}
		}
	}
}
