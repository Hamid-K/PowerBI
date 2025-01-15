using System;

namespace NLog.LogReceiverService
{
	// Token: 0x02000095 RID: 149
	public class LogReceiverOneWayForwardingService : BaseLogReceiverForwardingService, ILogReceiverOneWayServer
	{
		// Token: 0x060009D3 RID: 2515 RVA: 0x0001A2B0 File Offset: 0x000184B0
		public LogReceiverOneWayForwardingService()
			: this(null)
		{
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0001A2B9 File Offset: 0x000184B9
		public LogReceiverOneWayForwardingService(LogFactory logFactory)
			: base(logFactory)
		{
		}
	}
}
