using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200048F RID: 1167
	public class WsHttpBindingData : IBindingData
	{
		// Token: 0x060023FD RID: 9213 RVA: 0x0008131C File Offset: 0x0007F51C
		public WsHttpBindingData(EndpointInfo endpointInfo)
		{
			SecurityMode securityMode = (SecurityMode)Enum.Parse(typeof(SecurityMode), endpointInfo.SecurityMode.ToString());
			WSHttpBinding wshttpBinding = new WSHttpBinding(securityMode, false)
			{
				ReaderQuotas = 
				{
					MaxDepth = endpointInfo.MaxDepth,
					MaxStringContentLength = endpointInfo.MaxStringContentLength,
					MaxArrayLength = endpointInfo.MaxArrayLength
				}
			};
			long maxMessageSize = endpointInfo.MaxMessageSize;
			wshttpBinding.MaxReceivedMessageSize = maxMessageSize;
			if (endpointInfo.ReliableSessionEnabled)
			{
				wshttpBinding.ReliableSession.Enabled = true;
				wshttpBinding.ReliableSession.Ordered = endpointInfo.ReliableSessionOrderedMessages;
			}
			if (!string.IsNullOrEmpty(endpointInfo.ClientCredentialType))
			{
				if (securityMode == SecurityMode.TransportWithMessageCredential || securityMode == SecurityMode.Transport)
				{
					wshttpBinding.Security.Transport.ClientCredentialType = (HttpClientCredentialType)Enum.Parse(typeof(HttpClientCredentialType), endpointInfo.ClientCredentialType);
				}
				if (securityMode == SecurityMode.TransportWithMessageCredential || securityMode == SecurityMode.Message)
				{
					wshttpBinding.Security.Message.ClientCredentialType = (MessageCredentialType)Enum.Parse(typeof(MessageCredentialType), endpointInfo.ClientCredentialType);
				}
			}
			wshttpBinding.MaxBufferPoolSize = endpointInfo.MaxBufferPoolSize;
			this.Binding = wshttpBinding;
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x0008144A File Offset: 0x0007F64A
		// (set) Token: 0x060023FF RID: 9215 RVA: 0x00081452 File Offset: 0x0007F652
		public Binding Binding { get; private set; }

		// Token: 0x06002400 RID: 9216 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBehaviors(ServiceEndpoint endpoint)
		{
		}
	}
}
