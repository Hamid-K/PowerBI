using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000486 RID: 1158
	public class BasicHttpBindingData : IBindingData
	{
		// Token: 0x060023B8 RID: 9144 RVA: 0x00080AF0 File Offset: 0x0007ECF0
		public BasicHttpBindingData(EndpointInfo endpointInfo)
		{
			BasicHttpBinding basicHttpBinding = new BasicHttpBinding((BasicHttpSecurityMode)Enum.Parse(typeof(BasicHttpSecurityMode), endpointInfo.SecurityMode.ToString()))
			{
				ReaderQuotas = 
				{
					MaxDepth = endpointInfo.MaxDepth,
					MaxStringContentLength = endpointInfo.MaxStringContentLength,
					MaxArrayLength = endpointInfo.MaxArrayLength
				}
			};
			long maxMessageSize = endpointInfo.MaxMessageSize;
			basicHttpBinding.MaxReceivedMessageSize = maxMessageSize;
			basicHttpBinding.MaxBufferSize = maxMessageSize.ToInt32();
			if (!string.IsNullOrEmpty(endpointInfo.ClientCredentialType))
			{
				HttpClientCredentialType httpClientCredentialType = (HttpClientCredentialType)Enum.Parse(typeof(HttpClientCredentialType), endpointInfo.ClientCredentialType, true);
				basicHttpBinding.Security.Transport.ClientCredentialType = httpClientCredentialType;
			}
			basicHttpBinding.TransferMode = endpointInfo.TransferDataMode;
			basicHttpBinding.MaxBufferPoolSize = endpointInfo.MaxBufferPoolSize;
			this.Binding = basicHttpBinding;
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x00080BD7 File Offset: 0x0007EDD7
		// (set) Token: 0x060023BA RID: 9146 RVA: 0x00080BDF File Offset: 0x0007EDDF
		public Binding Binding { get; private set; }

		// Token: 0x060023BB RID: 9147 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBehaviors(ServiceEndpoint endpoint)
		{
		}
	}
}
