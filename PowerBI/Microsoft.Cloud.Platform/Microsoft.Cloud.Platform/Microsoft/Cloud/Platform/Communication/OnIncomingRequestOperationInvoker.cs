using System;
using System.ServiceModel.Dispatcher;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.RequestProtection;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004D2 RID: 1234
	internal class OnIncomingRequestOperationInvoker : IOperationInvoker
	{
		// Token: 0x06002589 RID: 9609 RVA: 0x00085760 File Offset: 0x00083960
		public OnIncomingRequestOperationInvoker([NotNull] DispatchOperation dispatchOperation, [NotNull] Action<EcfRequestContext> onIncomingRequest, [NotNull] IEventsKitFactory eventsKitFactory)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action<EcfRequestContext>>(onIncomingRequest, "onIncomingRequest");
			ExtendedDiagnostics.EnsureArgumentNotNull<IEventsKitFactory>(eventsKitFactory, "eventsKitFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<DispatchOperation>(dispatchOperation, "dispatchOperation");
			this.m_innerOperationInvoker = dispatchOperation.Invoker;
			this.m_onIncomingRequest = onIncomingRequest;
			this.m_eventsKitFactory = eventsKitFactory;
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000857AE File Offset: 0x000839AE
		public object[] AllocateInputs()
		{
			return this.m_innerOperationInvoker.AllocateInputs();
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x000857BB File Offset: 0x000839BB
		public object Invoke(object instance, object[] inputs, out object[] outputs)
		{
			this.m_onIncomingRequest(new EcfRequestContext(this.m_eventsKitFactory));
			return this.m_innerOperationInvoker.Invoke(instance, inputs, out outputs);
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000857E1 File Offset: 0x000839E1
		public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
		{
			this.m_onIncomingRequest(new EcfRequestContext(this.m_eventsKitFactory));
			return this.m_innerOperationInvoker.InvokeBegin(instance, inputs, callback, state);
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x00085809 File Offset: 0x00083A09
		public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
		{
			return this.m_innerOperationInvoker.InvokeEnd(instance, out outputs, result);
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x00085819 File Offset: 0x00083A19
		public bool IsSynchronous
		{
			get
			{
				return this.m_innerOperationInvoker.IsSynchronous;
			}
		}

		// Token: 0x04000D4C RID: 3404
		private Action<EcfRequestContext> m_onIncomingRequest;

		// Token: 0x04000D4D RID: 3405
		private IOperationInvoker m_innerOperationInvoker;

		// Token: 0x04000D4E RID: 3406
		private IEventsKitFactory m_eventsKitFactory;
	}
}
