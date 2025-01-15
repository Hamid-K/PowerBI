using System;

namespace NLog.LogReceiverService
{
	// Token: 0x02000094 RID: 148
	public class LogReceiverForwardingService : BaseLogReceiverForwardingService, ILogReceiverServer
	{
		// Token: 0x060009D1 RID: 2513 RVA: 0x0001A29E File Offset: 0x0001849E
		public LogReceiverForwardingService()
			: this(null)
		{
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0001A2A7 File Offset: 0x000184A7
		public LogReceiverForwardingService(LogFactory logFactory)
			: base(logFactory)
		{
		}
	}
}
