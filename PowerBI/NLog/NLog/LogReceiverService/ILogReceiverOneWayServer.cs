using System;
using System.ServiceModel;

namespace NLog.LogReceiverService
{
	// Token: 0x02000090 RID: 144
	[ServiceContract(Namespace = "http://nlog-project.org/ws/")]
	public interface ILogReceiverOneWayServer
	{
		// Token: 0x060009B9 RID: 2489
		[OperationContract(IsOneWay = true)]
		void ProcessLogMessages(NLogEvents events);
	}
}
