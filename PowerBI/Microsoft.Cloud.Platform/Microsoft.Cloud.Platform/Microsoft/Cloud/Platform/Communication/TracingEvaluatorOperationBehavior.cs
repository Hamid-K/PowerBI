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
	// Token: 0x020004D8 RID: 1240
	public sealed class TracingEvaluatorOperationBehavior : IOperationBehavior
	{
		// Token: 0x060025AD RID: 9645 RVA: 0x00085A20 File Offset: 0x00083C20
		public TracingEvaluatorOperationBehavior([NotNull] IEventsKitFactory eventsKitFactory, [NotNull] IPlatformActivityTracingPolicyEvaluator tracingEvaluator)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IPlatformActivityTracingPolicyEvaluator>(tracingEvaluator, "tracingEvaluator");
			ExtendedDiagnostics.EnsureArgumentNotNull<IEventsKitFactory>(eventsKitFactory, "eventsKitFactory");
			this.m_tracingEvaluator = tracingEvaluator;
			this.m_eventsKitFactory = eventsKitFactory;
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x00085A4C File Offset: 0x00083C4C
		public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
			dispatchOperation.Invoker = new TracingEvaluatorOperationInvoker(this.m_eventsKitFactory, dispatchOperation, this.m_tracingEvaluator);
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Validate(OperationDescription operationDescription)
		{
		}

		// Token: 0x04000D55 RID: 3413
		private IPlatformActivityTracingPolicyEvaluator m_tracingEvaluator;

		// Token: 0x04000D56 RID: 3414
		private IEventsKitFactory m_eventsKitFactory;
	}
}
