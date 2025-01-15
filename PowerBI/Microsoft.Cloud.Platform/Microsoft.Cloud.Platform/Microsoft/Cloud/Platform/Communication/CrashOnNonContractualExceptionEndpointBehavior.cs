using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004C3 RID: 1219
	internal class CrashOnNonContractualExceptionEndpointBehavior : IEndpointBehavior
	{
		// Token: 0x06002533 RID: 9523 RVA: 0x0008445A File Offset: 0x0008265A
		public CrashOnNonContractualExceptionEndpointBehavior(IEnumerable<Type> knownExceptions, NonContractualExceptionBehavior crashServerOnNonContractualExceptionBehavior, ICommunicationFrameworkEventsKit eventsKit)
		{
			this.m_knownExceptions = knownExceptions;
			this.m_crashServerOnNonContractualExceptionBehavior = crashServerOnNonContractualExceptionBehavior;
			this.m_eventsKit = eventsKit;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x00084478 File Offset: 0x00082678
		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			foreach (OperationDescription operationDescription in endpoint.Contract.Operations)
			{
				if (!operationDescription.Behaviors.Where((IOperationBehavior b) => b.GetType().Equals(typeof(CrashOnNonContractualExceptionOperationBehavior))).Any<IOperationBehavior>())
				{
					operationDescription.Behaviors.Add(new CrashOnNonContractualExceptionOperationBehavior(this.m_knownExceptions, this.m_crashServerOnNonContractualExceptionBehavior, this.m_eventsKit));
				}
			}
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Validate(ServiceEndpoint endpoint)
		{
		}

		// Token: 0x04000D16 RID: 3350
		private IEnumerable<Type> m_knownExceptions;

		// Token: 0x04000D17 RID: 3351
		private ICommunicationFrameworkEventsKit m_eventsKit;

		// Token: 0x04000D18 RID: 3352
		private NonContractualExceptionBehavior m_crashServerOnNonContractualExceptionBehavior;
	}
}
