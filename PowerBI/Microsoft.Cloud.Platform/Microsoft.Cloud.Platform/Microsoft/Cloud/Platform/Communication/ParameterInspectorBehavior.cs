using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004D3 RID: 1235
	internal class ParameterInspectorBehavior : IEndpointBehavior
	{
		// Token: 0x0600258F RID: 9615 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x00085828 File Offset: 0x00083A28
		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			foreach (ClientOperation clientOperation in clientRuntime.Operations)
			{
				clientOperation.ParameterInspectors.Add(new CommunicationFrameworkParameterInspector());
			}
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Validate(ServiceEndpoint endpoint)
		{
		}
	}
}
