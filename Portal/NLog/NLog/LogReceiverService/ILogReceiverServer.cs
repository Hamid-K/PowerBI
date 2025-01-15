using System;
using System.ServiceModel;

namespace NLog.LogReceiverService
{
	// Token: 0x02000091 RID: 145
	[ServiceContract(Namespace = "http://nlog-project.org/ws/")]
	public interface ILogReceiverServer
	{
		// Token: 0x060009BA RID: 2490
		[OperationContract]
		void ProcessLogMessages(NLogEvents events);
	}
}
