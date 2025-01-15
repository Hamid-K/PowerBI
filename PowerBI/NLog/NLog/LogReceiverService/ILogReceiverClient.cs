using System;
using System.ServiceModel;

namespace NLog.LogReceiverService
{
	// Token: 0x0200008E RID: 142
	[ServiceContract(Namespace = "http://nlog-project.org/ws/", ConfigurationName = "NLog.LogReceiverService.ILogReceiverClient")]
	[Obsolete("Use ILogReceiverOneWayClient or ILogReceiverTwoWayClient instead. Marked obsolete before v4.3.11 and it may be removed in a future release.")]
	public interface ILogReceiverClient
	{
		// Token: 0x060009B5 RID: 2485
		[OperationContract(AsyncPattern = true, Action = "http://nlog-project.org/ws/ILogReceiverServer/ProcessLogMessages", ReplyAction = "http://nlog-project.org/ws/ILogReceiverServer/ProcessLogMessagesResponse")]
		IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState);

		// Token: 0x060009B6 RID: 2486
		void EndProcessLogMessages(IAsyncResult result);
	}
}
