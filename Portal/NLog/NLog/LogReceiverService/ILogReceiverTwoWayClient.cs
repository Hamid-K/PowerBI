using System;
using System.ServiceModel;

namespace NLog.LogReceiverService
{
	// Token: 0x02000092 RID: 146
	[ServiceContract(Namespace = "http://nlog-project.org/ws/", ConfigurationName = "NLog.LogReceiverService.ILogReceiverClient")]
	public interface ILogReceiverTwoWayClient
	{
		// Token: 0x060009BB RID: 2491
		[OperationContract(AsyncPattern = true, Action = "http://nlog-project.org/ws/ILogReceiverServer/ProcessLogMessages", ReplyAction = "http://nlog-project.org/ws/ILogReceiverServer/ProcessLogMessagesResponse")]
		IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState);

		// Token: 0x060009BC RID: 2492
		void EndProcessLogMessages(IAsyncResult result);
	}
}
