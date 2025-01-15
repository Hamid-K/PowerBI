using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200048E RID: 1166
	public class WebHttpBindingData : IBindingData
	{
		// Token: 0x060023F9 RID: 9209 RVA: 0x000811FC File Offset: 0x0007F3FC
		public WebHttpBindingData(EndpointInfo binding)
		{
			WebHttpBinding webHttpBinding = new WebHttpBinding((WebHttpSecurityMode)Enum.Parse(typeof(WebHttpSecurityMode), binding.SecurityMode.ToString()))
			{
				ReaderQuotas = 
				{
					MaxDepth = binding.MaxDepth,
					MaxStringContentLength = binding.MaxStringContentLength,
					MaxArrayLength = binding.MaxArrayLength
				}
			};
			long maxMessageSize = binding.MaxMessageSize;
			webHttpBinding.MaxReceivedMessageSize = maxMessageSize;
			webHttpBinding.MaxBufferSize = maxMessageSize.ToInt32();
			if (!string.IsNullOrEmpty(binding.ClientCredentialType))
			{
				HttpClientCredentialType httpClientCredentialType = (HttpClientCredentialType)Enum.Parse(typeof(HttpClientCredentialType), binding.ClientCredentialType, true);
				webHttpBinding.Security.Transport.ClientCredentialType = httpClientCredentialType;
			}
			webHttpBinding.TransferMode = binding.TransferDataMode;
			webHttpBinding.MaxBufferPoolSize = binding.MaxBufferPoolSize;
			this.Binding = webHttpBinding;
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x000812E3 File Offset: 0x0007F4E3
		// (set) Token: 0x060023FB RID: 9211 RVA: 0x000812EB File Offset: 0x0007F4EB
		public Binding Binding { get; private set; }

		// Token: 0x060023FC RID: 9212 RVA: 0x000812F4 File Offset: 0x0007F4F4
		public void AddBehaviors(ServiceEndpoint endpoint)
		{
			WebHttpBehavior webHttpBehavior = new WebHttpBehavior
			{
				AutomaticFormatSelectionEnabled = true
			};
			endpoint.Behaviors.Add(webHttpBehavior);
		}
	}
}
