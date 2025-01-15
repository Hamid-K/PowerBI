using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004D4 RID: 1236
	public class RemoveHttpActionEndpointBehavior : IEndpointBehavior
	{
		// Token: 0x06002594 RID: 9620 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x0008587C File Offset: 0x00083A7C
		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			clientRuntime.MessageInspectors.Add(new RemoveHttpActionMessageInspector());
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Validate(ServiceEndpoint endpoint)
		{
		}
	}
}
