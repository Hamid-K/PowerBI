using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004BA RID: 1210
	internal class ClientExceptionHandlingBehavior : IEndpointBehavior
	{
		// Token: 0x0600250F RID: 9487 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x00083ED9 File Offset: 0x000820D9
		public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
		{
			behavior.MessageInspectors.Add(new ClientExceptionHandlingMessageInspector());
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Validate(ServiceEndpoint serviceEndpoint)
		{
		}
	}
}
