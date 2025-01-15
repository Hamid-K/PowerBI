using System;
using System.ServiceModel;

namespace NLog.LogReceiverService
{
	// Token: 0x0200008F RID: 143
	[ServiceContract(Namespace = "http://nlog-project.org/ws/", ConfigurationName = "NLog.LogReceiverService.ILogReceiverOneWayClient")]
	public interface ILogReceiverOneWayClient
	{
		// Token: 0x060009B7 RID: 2487
		[OperationContract(IsOneWay = true, AsyncPattern = true, Action = "http://nlog-project.org/ws/ILogReceiverOneWayServer/ProcessLogMessages")]
		IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState);

		// Token: 0x060009B8 RID: 2488
		void EndProcessLogMessages(IAsyncResult result);
	}
}
