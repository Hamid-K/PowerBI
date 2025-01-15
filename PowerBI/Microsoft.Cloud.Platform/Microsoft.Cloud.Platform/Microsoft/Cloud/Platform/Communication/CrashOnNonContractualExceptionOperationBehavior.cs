using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004C4 RID: 1220
	internal sealed class CrashOnNonContractualExceptionOperationBehavior : IOperationBehavior
	{
		// Token: 0x06002538 RID: 9528 RVA: 0x00084518 File Offset: 0x00082718
		public CrashOnNonContractualExceptionOperationBehavior(IEnumerable<Type> knownExceptions, NonContractualExceptionBehavior crashServerOnNonContractualExceptionBehavior, ICommunicationFrameworkEventsKit eventsKit)
		{
			this.m_knownExceptions = knownExceptions;
			this.m_eventsKit = eventsKit;
			this.m_crashServerOnNonContractualExceptionBehavior = crashServerOnNonContractualExceptionBehavior;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x00084535 File Offset: 0x00082735
		public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
			dispatchOperation.Invoker = new CrashOnNonContractualExceptionOperationInvoker(operationDescription, dispatchOperation, this.m_knownExceptions, this.m_crashServerOnNonContractualExceptionBehavior, this.m_eventsKit);
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Validate(OperationDescription operationDescription)
		{
		}

		// Token: 0x04000D19 RID: 3353
		private IEnumerable<Type> m_knownExceptions;

		// Token: 0x04000D1A RID: 3354
		private ICommunicationFrameworkEventsKit m_eventsKit;

		// Token: 0x04000D1B RID: 3355
		private NonContractualExceptionBehavior m_crashServerOnNonContractualExceptionBehavior;
	}
}
