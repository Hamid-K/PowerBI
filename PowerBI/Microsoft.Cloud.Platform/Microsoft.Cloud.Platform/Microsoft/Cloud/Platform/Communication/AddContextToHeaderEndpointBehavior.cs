using System;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004B7 RID: 1207
	public class AddContextToHeaderEndpointBehavior : IEndpointBehavior
	{
		// Token: 0x06002500 RID: 9472 RVA: 0x00083C13 File Offset: 0x00081E13
		public AddContextToHeaderEndpointBehavior(IActivityFactory activityFactory)
		{
			this.m_activityFactory = activityFactory;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x00083C22 File Offset: 0x00081E22
		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			clientRuntime.MessageInspectors.Add(new ClientMessageInspector());
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x00083C34 File Offset: 0x00081E34
		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			foreach (OperationDescription operationDescription in endpoint.Contract.Operations)
			{
				if (!operationDescription.Behaviors.Where((IOperationBehavior b) => b.GetType().Equals(typeof(CallContextBehavior))).Any<IOperationBehavior>())
				{
					operationDescription.Behaviors.Add(new CallContextBehavior(this.m_activityFactory));
				}
			}
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Validate(ServiceEndpoint endpoint)
		{
		}

		// Token: 0x04000D0C RID: 3340
		private IActivityFactory m_activityFactory;

		// Token: 0x04000D0D RID: 3341
		internal static string ACTIVITY_HEADER_NAME = "Activity";

		// Token: 0x04000D0E RID: 3342
		internal static string ACTIVITY_HEADER_NAMESPACE = "ActivityNS";

		// Token: 0x04000D0F RID: 3343
		internal static string FORCE_TRACES_HEADER_NAME = "ForceTraces";

		// Token: 0x04000D10 RID: 3344
		internal static string FORCE_TRACES_HEADER_NAMESPACE = "ForceTracesNS";
	}
}
