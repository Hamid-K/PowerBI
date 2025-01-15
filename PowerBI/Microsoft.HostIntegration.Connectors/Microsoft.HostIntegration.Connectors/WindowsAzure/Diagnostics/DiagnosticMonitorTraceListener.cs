using System;
using System.Diagnostics;
using Microsoft.Cis.Eventing.Listeners;

namespace Microsoft.WindowsAzure.Diagnostics
{
	// Token: 0x02000461 RID: 1121
	public class DiagnosticMonitorTraceListener : RDEventMonitoringAgentListener
	{
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x0600272E RID: 10030 RVA: 0x00077944 File Offset: 0x00075B44
		public static string DefaultGuid
		{
			get
			{
				return DiagnosticMonitorTraceListener.defaultGuid;
			}
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x0007794B File Offset: 0x00075B4B
		public DiagnosticMonitorTraceListener(string guid)
			: base(guid)
		{
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x00077954 File Offset: 0x00075B54
		public DiagnosticMonitorTraceListener(Guid guid)
			: base(guid)
		{
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x0007795D File Offset: 0x00075B5D
		public DiagnosticMonitorTraceListener()
			: base(DiagnosticMonitorTraceListener.defaultGuid)
		{
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002732 RID: 10034 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool IsThreadSafe
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x0007796A File Offset: 0x00075B6A
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x00077973 File Offset: 0x00075B73
		public override void Write(string message)
		{
			base.Write(message);
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x0007797C File Offset: 0x00075B7C
		public override void WriteLine(string message)
		{
			base.WriteLine(message);
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x00077985 File Offset: 0x00075B85
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
		{
			base.TraceEvent(eventCache, source, eventType, id);
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x00077992 File Offset: 0x00075B92
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			base.TraceEvent(eventCache, source, eventType, id, message);
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000779A1 File Offset: 0x00075BA1
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			base.TraceEvent(eventCache, source, eventType, id, format, args);
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000779B2 File Offset: 0x00075BB2
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			base.TraceData(eventCache, source, eventType, id, data);
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x000779C1 File Offset: 0x00075BC1
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			base.TraceData(eventCache, source, eventType, id, data);
		}

		// Token: 0x04001729 RID: 5929
		private static readonly string defaultGuid = "8D5CB585-360B-4442-8252-6C28A3DCA52E";
	}
}
