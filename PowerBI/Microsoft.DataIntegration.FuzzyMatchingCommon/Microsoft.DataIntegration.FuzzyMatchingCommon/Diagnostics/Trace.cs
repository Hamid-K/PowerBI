using System;
using System.Diagnostics;
using Microsoft.SqlServer.Server;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics
{
	// Token: 0x02000045 RID: 69
	[Serializable]
	internal class Trace
	{
		// Token: 0x06000239 RID: 569 RVA: 0x0001314B File Offset: 0x0001134B
		protected static IEventSink Initialize()
		{
			if (SqlContext.IsAvailable)
			{
				return new SqlEventSink();
			}
			return new AppEventSink();
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0001315F File Offset: 0x0001135F
		public static IEventSink EventSink
		{
			get
			{
				return Trace.m_EventSink;
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00013166 File Offset: 0x00011366
		[Conditional("TRACE")]
		public static void WriteLine(string message)
		{
			Trace.EventSink.Trace(DateTime.Now + ": " + message);
		}

		// Token: 0x0400005E RID: 94
		protected static readonly IEventSink m_EventSink = Trace.Initialize();
	}
}
