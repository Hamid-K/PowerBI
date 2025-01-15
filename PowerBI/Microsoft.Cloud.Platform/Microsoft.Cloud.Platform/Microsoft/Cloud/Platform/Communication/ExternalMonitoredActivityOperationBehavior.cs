using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.MonitoredUtils;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004CC RID: 1228
	public sealed class ExternalMonitoredActivityOperationBehavior : IOperationBehavior
	{
		// Token: 0x06002567 RID: 9575 RVA: 0x00085084 File Offset: 0x00083284
		public ExternalMonitoredActivityOperationBehavior(ClientActivityContextSource source, [NotNull] ActivityType activityType, string rootActivityHeaderName, string clientActivityHeaderName, [NotNull] IActivityFactory activityFactory, [NotNull] IMonitoredActivityCompletionModelFactory completionModelFactory)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ActivityType>(activityType, "activityType");
			ExtendedDiagnostics.EnsureArgumentNotNull<IActivityFactory>(activityFactory, "activityFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<IMonitoredActivityCompletionModelFactory>(completionModelFactory, "completionModelFactory");
			ExtendedDiagnostics.EnsureOperation(source.HasFlag(ClientActivityContextSource.CreateNew), "This behavior should not be added if CreateNew is not turned on");
			this.m_source = source;
			this.m_activityType = activityType;
			this.m_activityFactory = activityFactory;
			this.m_rootActivityHeaderName = rootActivityHeaderName;
			this.m_clientActivityHeaderName = clientActivityHeaderName;
			this.m_completionModelFactory = completionModelFactory;
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x00085102 File Offset: 0x00083302
		public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
			dispatchOperation.Invoker = new ExternalMonitoredActivityOperationInvoker(dispatchOperation.Invoker, this.m_source, this.m_activityType, this.m_rootActivityHeaderName, this.m_clientActivityHeaderName, this.m_activityFactory, this.m_completionModelFactory);
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Validate(OperationDescription operationDescription)
		{
		}

		// Token: 0x04000D31 RID: 3377
		private IMonitoredActivityCompletionModelFactory m_completionModelFactory;

		// Token: 0x04000D32 RID: 3378
		private IActivityFactory m_activityFactory;

		// Token: 0x04000D33 RID: 3379
		private ClientActivityContextSource m_source;

		// Token: 0x04000D34 RID: 3380
		private string m_clientActivityHeaderName;

		// Token: 0x04000D35 RID: 3381
		private string m_rootActivityHeaderName;

		// Token: 0x04000D36 RID: 3382
		private ActivityType m_activityType;
	}
}
