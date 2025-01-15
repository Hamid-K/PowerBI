using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.RequestProtection;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004D1 RID: 1233
	internal sealed class OnIncomingRequestOperationBehavior : IOperationBehavior
	{
		// Token: 0x06002584 RID: 9604 RVA: 0x00085717 File Offset: 0x00083917
		public OnIncomingRequestOperationBehavior([NotNull] Action<EcfRequestContext> onIncomingRequest, [NotNull] IEventsKitFactory eventsKitFactory)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action<EcfRequestContext>>(onIncomingRequest, "onIncomingRequest");
			ExtendedDiagnostics.EnsureArgumentNotNull<IEventsKitFactory>(eventsKitFactory, "eventsKitFactory");
			this.m_onIncomingRequest = onIncomingRequest;
			this.m_eventsKitFactory = eventsKitFactory;
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x00085743 File Offset: 0x00083943
		public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
			dispatchOperation.Invoker = new OnIncomingRequestOperationInvoker(dispatchOperation, this.m_onIncomingRequest, this.m_eventsKitFactory);
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Validate(OperationDescription operationDescription)
		{
		}

		// Token: 0x04000D4A RID: 3402
		private Action<EcfRequestContext> m_onIncomingRequest;

		// Token: 0x04000D4B RID: 3403
		private IEventsKitFactory m_eventsKitFactory;
	}
}
