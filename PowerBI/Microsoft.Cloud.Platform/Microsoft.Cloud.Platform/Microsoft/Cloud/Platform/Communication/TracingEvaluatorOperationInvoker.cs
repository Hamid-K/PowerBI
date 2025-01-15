using System;
using System.Collections.Generic;
using System.ServiceModel.Dispatcher;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.RequestProtection;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004D9 RID: 1241
	internal class TracingEvaluatorOperationInvoker : IOperationInvoker
	{
		// Token: 0x060025B2 RID: 9650 RVA: 0x00085A68 File Offset: 0x00083C68
		public TracingEvaluatorOperationInvoker([NotNull] IEventsKitFactory eventsKitFactory, [NotNull] DispatchOperation dispatchOperation, [NotNull] IPlatformActivityTracingPolicyEvaluator tracingEvaluator)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IPlatformActivityTracingPolicyEvaluator>(tracingEvaluator, "tracingEvaluator");
			ExtendedDiagnostics.EnsureArgumentNotNull<IEventsKitFactory>(eventsKitFactory, "eventsKitFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<DispatchOperation>(dispatchOperation, "dispatchOperation");
			this.m_tracingEvaluator = tracingEvaluator;
			this.m_eventsKitFactory = eventsKitFactory;
			this.m_innerOperationInvoker = dispatchOperation.Invoker;
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x00085AB6 File Offset: 0x00083CB6
		public object[] AllocateInputs()
		{
			return this.m_innerOperationInvoker.AllocateInputs();
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x00085AC4 File Offset: 0x00083CC4
		public object Invoke(object instance, object[] inputs, out object[] outputs)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceVerbose("Entering Invoke in {0}".FormatWithInvariantCulture(new object[] { typeof(TracingEvaluatorOperationInvoker).Name }));
			object obj;
			using (this.IsTracingEnforced() ? UtilsContext.Current.PushTracingForced(true) : null)
			{
				obj = this.m_innerOperationInvoker.Invoke(instance, inputs, out outputs);
			}
			return obj;
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x00085B40 File Offset: 0x00083D40
		public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceVerbose("Entering InvokeBegin in {0}".FormatWithInvariantCulture(new object[] { typeof(TracingEvaluatorOperationInvoker).Name }));
			IAsyncResult asyncResult;
			using (this.IsTracingEnforced() ? UtilsContext.Current.PushTracingForced(true) : null)
			{
				ChainedAsyncResult<WorkTicket> chainedAsyncResult = new ChainedAsyncResult<WorkTicket>(callback, state, null);
				chainedAsyncResult.InnerResult = this.m_innerOperationInvoker.InvokeBegin(instance, inputs, new AsyncCallback(chainedAsyncResult.BeginAsyncFunctionCallback), null);
				asyncResult = chainedAsyncResult;
			}
			return asyncResult;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x00085BDC File Offset: 0x00083DDC
		public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceVerbose("Entering InvokeEnd in {0}".FormatWithInvariantCulture(new object[] { typeof(TracingEvaluatorOperationInvoker).Name }));
			ChainedAsyncResult<WorkTicket> chainedAsyncResult = (ChainedAsyncResult<WorkTicket>)result;
			chainedAsyncResult.End();
			return this.m_innerOperationInvoker.InvokeEnd(instance, out outputs, chainedAsyncResult.InnerResult);
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x00085C35 File Offset: 0x00083E35
		public bool IsSynchronous
		{
			get
			{
				return this.m_innerOperationInvoker.IsSynchronous;
			}
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x00085C44 File Offset: 0x00083E44
		private bool IsTracingEnforced()
		{
			Guid rootActivityId = UtilsContext.Current.Activity.RootActivityId;
			string clientActivityId = UtilsContext.Current.Activity.ClientActivityId;
			EcfRequestContext requestContext = new EcfRequestContext(this.m_eventsKitFactory);
			bool tracingEnforced = false;
			if (this.m_tracingEvaluator != null)
			{
				Exception ex = TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
				{
					tracingEnforced = this.m_tracingEvaluator.EvaluateForNewIncomingRequestEvent(clientActivityId, rootActivityId, requestContext.RequestLength, requestContext.RequestUri.ToString(), string.Join<KeyValuePair<string, IEnumerable<string>>>(", ", requestContext.RequestHeaders), requestContext.ClientEndpoint.ToString());
				});
				if (ex != null)
				{
					TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceWarning("Failed to evaluate activity tracing policy. Message='{0}'", new object[] { ex.Message });
				}
			}
			return tracingEnforced;
		}

		// Token: 0x04000D57 RID: 3415
		private IPlatformActivityTracingPolicyEvaluator m_tracingEvaluator;

		// Token: 0x04000D58 RID: 3416
		private IOperationInvoker m_innerOperationInvoker;

		// Token: 0x04000D59 RID: 3417
		private IEventsKitFactory m_eventsKitFactory;
	}
}
