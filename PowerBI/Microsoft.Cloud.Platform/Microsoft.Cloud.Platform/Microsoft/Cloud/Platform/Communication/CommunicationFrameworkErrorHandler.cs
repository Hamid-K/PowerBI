using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004BD RID: 1213
	internal class CommunicationFrameworkErrorHandler : IErrorHandler
	{
		// Token: 0x0600251B RID: 9499 RVA: 0x000840A3 File Offset: 0x000822A3
		public bool HandleError(Exception error)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Warning, "The service threw an exception: {0}.", new object[] { error });
			ExtendedEnvironment.ApplyFailSlowOnFatalPolicy(this, error);
			return false;
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x000840C8 File Offset: 0x000822C8
		public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
			ExtendedEnvironment.ApplyFailSlowOnFatalPolicy(this, error);
			MessageFault messageFault = MessageFault.CreateFault(ErrorHandlerHelper.GetFaultCode(error), new FaultReason(error.Message), error, new NetDataContractSerializer());
			fault = Message.CreateMessage(version, messageFault, null);
		}
	}
}
