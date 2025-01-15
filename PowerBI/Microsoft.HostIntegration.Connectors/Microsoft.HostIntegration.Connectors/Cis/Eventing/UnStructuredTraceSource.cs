using System;
using System.Diagnostics;
using System.Text;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x0200049B RID: 1179
	public static class UnStructuredTraceSource
	{
		// Token: 0x060028F5 RID: 10485 RVA: 0x0007CD2B File Offset: 0x0007AF2B
		private static UnStructuredTraceSource.UnstructuredEvent GetUnstructuredEvent()
		{
			return UnStructuredTraceSource.ev;
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x0007CD34 File Offset: 0x0007AF34
		public static void TraceData(TraceSource source, TraceEventType eventType, int id, object data)
		{
			UnStructuredTraceSource.UnstructuredEvent unstructuredEvent = UnStructuredTraceSource.GetUnstructuredEvent();
			unstructuredEvent.TraceTo(source, eventType, id, "{0}", new object[] { data });
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x0007CD64 File Offset: 0x0007AF64
		public static void TraceData(TraceSource source, TraceEventType eventType, int id, params object[] data)
		{
			UnStructuredTraceSource.UnstructuredEvent unstructuredEvent = UnStructuredTraceSource.GetUnstructuredEvent();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				stringBuilder.Append("{" + i + "}, ");
			}
			unstructuredEvent.TraceTo(source, eventType, id, stringBuilder.ToString(), data);
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x0007CDB8 File Offset: 0x0007AFB8
		public static void TraceEvent(TraceSource source, TraceEventType eventType, int id)
		{
			UnStructuredTraceSource.UnstructuredEvent unstructuredEvent = UnStructuredTraceSource.GetUnstructuredEvent();
			unstructuredEvent.TraceTo(source, eventType, id);
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x0007CDD4 File Offset: 0x0007AFD4
		public static void TraceEvent(TraceSource source, TraceEventType eventType, int id, string message)
		{
			UnStructuredTraceSource.UnstructuredEvent unstructuredEvent = UnStructuredTraceSource.GetUnstructuredEvent();
			unstructuredEvent.TraceTo(source, eventType, id, message, new object[0]);
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x0007CDF8 File Offset: 0x0007AFF8
		public static void TraceEvent(TraceSource source, TraceEventType eventType, int id, string format, params object[] args)
		{
			UnStructuredTraceSource.UnstructuredEvent unstructuredEvent = UnStructuredTraceSource.GetUnstructuredEvent();
			unstructuredEvent.TraceTo(source, eventType, id, format, args);
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x0007CE18 File Offset: 0x0007B018
		public static void TraceInformation(TraceSource source, string message)
		{
			UnStructuredTraceSource.UnstructuredEvent unstructuredEvent = UnStructuredTraceSource.GetUnstructuredEvent();
			unstructuredEvent.TraceTo(source, TraceEventType.Information, 0, message, new object[0]);
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x0007CE3C File Offset: 0x0007B03C
		public static void TraceInformation(TraceSource source, string format, params object[] args)
		{
			UnStructuredTraceSource.UnstructuredEvent unstructuredEvent = UnStructuredTraceSource.GetUnstructuredEvent();
			unstructuredEvent.TraceTo(source, TraceEventType.Information, 0, format, args);
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x0007CE5C File Offset: 0x0007B05C
		public static void TraceTransfer(TraceSource source, int id, string message, Guid relatedActivityId)
		{
			UnStructuredTraceSource.UnstructuredEvent unstructuredEvent = UnStructuredTraceSource.GetUnstructuredEvent();
			unstructuredEvent.TraceTo(source, TraceEventType.Transfer, id, message, new object[] { relatedActivityId });
		}

		// Token: 0x04001817 RID: 6167
		private static UnStructuredTraceSource.UnstructuredEvent ev = new UnStructuredTraceSource.UnstructuredEvent();

		// Token: 0x0200049C RID: 1180
		private class UnstructuredEvent : RDEventBase
		{
		}
	}
}
