using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004C9 RID: 1225
	public class ExternalCallContextBehavior : IOperationBehavior
	{
		// Token: 0x06002559 RID: 9561 RVA: 0x00084C56 File Offset: 0x00082E56
		public ExternalCallContextBehavior(ActivityType activityType, string rootActivityHeaderName, string clientActivityHeaderName, IActivityFactory activityFactory)
			: this(activityType, rootActivityHeaderName, clientActivityHeaderName, ClientActivityContextSource.Header, activityFactory)
		{
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x00084C64 File Offset: 0x00082E64
		public ExternalCallContextBehavior(ActivityType activityType, string rootActivityHeaderName, string clientActivityHeaderName, ClientActivityContextSource source, IActivityFactory activityFactory)
		{
			ExtendedDiagnostics.EnsureOperation(source.HasFlag(ClientActivityContextSource.CreateNew), "This behavior should not be added if CreateNew is not turned on");
			this.m_activityType = activityType;
			this.m_rootActivityHeaderName = rootActivityHeaderName;
			this.m_clientActivityHeaderName = clientActivityHeaderName;
			this.m_correlationIdsSource = source;
			this.m_activityFactory = activityFactory;
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x00084CB8 File Offset: 0x00082EB8
		public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
			dispatchOperation.CallContextInitializers.Add(new ExternalCallContextInitializer(this.m_activityType, this.m_rootActivityHeaderName, this.m_clientActivityHeaderName, this.m_correlationIdsSource, this.m_activityFactory));
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Validate(OperationDescription operationDescription)
		{
		}

		// Token: 0x04000D24 RID: 3364
		private readonly ActivityType m_activityType;

		// Token: 0x04000D25 RID: 3365
		private readonly string m_rootActivityHeaderName;

		// Token: 0x04000D26 RID: 3366
		private readonly string m_clientActivityHeaderName;

		// Token: 0x04000D27 RID: 3367
		private readonly ClientActivityContextSource m_correlationIdsSource;

		// Token: 0x04000D28 RID: 3368
		private readonly IActivityFactory m_activityFactory;
	}
}
