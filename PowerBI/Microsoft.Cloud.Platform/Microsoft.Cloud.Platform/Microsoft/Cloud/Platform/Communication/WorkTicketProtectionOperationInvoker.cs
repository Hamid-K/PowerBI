using System;
using System.ServiceModel.Dispatcher;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.RequestProtection;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004DC RID: 1244
	internal class WorkTicketProtectionOperationInvoker : IOperationInvoker
	{
		// Token: 0x060025C2 RID: 9666 RVA: 0x00085DC0 File Offset: 0x00083FC0
		public WorkTicketProtectionOperationInvoker([NotNull] DispatchOperation dispatchOperation, [NotNull] Func<IRequestProtectionContext, WorkTicket> createWorkTicket, [NotNull] IEventsKitFactory eventsKitFactory)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<IRequestProtectionContext, WorkTicket>>(createWorkTicket, "createWorkTicket");
			ExtendedDiagnostics.EnsureArgumentNotNull<IEventsKitFactory>(eventsKitFactory, "eventsKitFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<DispatchOperation>(dispatchOperation, "dispatchOperation");
			this.m_innerOperationInvoker = dispatchOperation.Invoker;
			this.m_createWorkTicket = createWorkTicket;
			this.m_eventsKitFactory = eventsKitFactory;
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x00085E0E File Offset: 0x0008400E
		public object[] AllocateInputs()
		{
			return this.m_innerOperationInvoker.AllocateInputs();
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x00085E1C File Offset: 0x0008401C
		public object Invoke(object instance, object[] inputs, out object[] outputs)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceVerbose("Entering Invoke in {0}".FormatWithInvariantCulture(new object[] { typeof(WorkTicketProtectionOperationInvoker).Name }));
			object obj;
			using (this.m_createWorkTicket(new EcfRequestContext(this.m_eventsKitFactory)))
			{
				obj = this.m_innerOperationInvoker.Invoke(instance, inputs, out outputs);
			}
			return obj;
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x00085E98 File Offset: 0x00084098
		public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceVerbose("Entering InvokeBegin in {0}".FormatWithInvariantCulture(new object[] { typeof(WorkTicketProtectionOperationInvoker).Name }));
			WorkTicket workTicket = this.m_createWorkTicket(new EcfRequestContext(this.m_eventsKitFactory));
			IAsyncResult asyncResult;
			using (DisposeController disposeController = new DisposeController(workTicket))
			{
				ChainedAsyncResult<WorkTicket> chainedAsyncResult = new ChainedAsyncResult<WorkTicket>(callback, state, workTicket);
				chainedAsyncResult.InnerResult = this.m_innerOperationInvoker.InvokeBegin(instance, inputs, new AsyncCallback(chainedAsyncResult.BeginAsyncFunctionCallback), null);
				disposeController.PreventDispose();
				asyncResult = chainedAsyncResult;
			}
			return asyncResult;
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x00085F40 File Offset: 0x00084140
		public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceVerbose("Entering InvokeEnd in {0}".FormatWithInvariantCulture(new object[] { typeof(WorkTicketProtectionOperationInvoker).Name }));
			ChainedAsyncResult<WorkTicket> chainedAsyncResult = (ChainedAsyncResult<WorkTicket>)result;
			object obj;
			using (chainedAsyncResult.WorkTicket)
			{
				chainedAsyncResult.End();
				obj = this.m_innerOperationInvoker.InvokeEnd(instance, out outputs, chainedAsyncResult.InnerResult);
			}
			return obj;
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x00085FC0 File Offset: 0x000841C0
		public bool IsSynchronous
		{
			get
			{
				return this.m_innerOperationInvoker.IsSynchronous;
			}
		}

		// Token: 0x04000D5E RID: 3422
		private Func<IRequestProtectionContext, WorkTicket> m_createWorkTicket;

		// Token: 0x04000D5F RID: 3423
		private IOperationInvoker m_innerOperationInvoker;

		// Token: 0x04000D60 RID: 3424
		private IEventsKitFactory m_eventsKitFactory;
	}
}
