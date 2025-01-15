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
	// Token: 0x020004DB RID: 1243
	public sealed class WorkTicketProtectionOperationBehavior : IOperationBehavior
	{
		// Token: 0x060025BD RID: 9661 RVA: 0x00085D7A File Offset: 0x00083F7A
		public WorkTicketProtectionOperationBehavior([NotNull] IEventsKitFactory eventsKitFactory, [NotNull] Func<IRequestProtectionContext, WorkTicket> createWorkTicket)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<IRequestProtectionContext, WorkTicket>>(createWorkTicket, "createWorkTicket");
			ExtendedDiagnostics.EnsureArgumentNotNull<IEventsKitFactory>(eventsKitFactory, "eventsKitFactory");
			this.m_createWorkTicket = createWorkTicket;
			this.m_eventsKitFactory = eventsKitFactory;
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x00085DA6 File Offset: 0x00083FA6
		public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
			dispatchOperation.Invoker = new WorkTicketProtectionOperationInvoker(dispatchOperation, this.m_createWorkTicket, this.m_eventsKitFactory);
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Validate(OperationDescription operationDescription)
		{
		}

		// Token: 0x04000D5C RID: 3420
		private Func<IRequestProtectionContext, WorkTicket> m_createWorkTicket;

		// Token: 0x04000D5D RID: 3421
		private IEventsKitFactory m_eventsKitFactory;
	}
}
