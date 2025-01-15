using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200048A RID: 1162
	public sealed class HttpsWithSoap12BindingData : IBindingData
	{
		// Token: 0x060023EB RID: 9195 RVA: 0x00080F10 File Offset: 0x0007F110
		public HttpsWithSoap12BindingData(EndpointInfo endpointInfo)
		{
			this.Binding = new CustomBinding
			{
				Elements = 
				{
					new TextMessageEncodingBindingElement
					{
						MessageVersion = MessageVersion.Soap12,
						ReaderQuotas = 
						{
							MaxDepth = endpointInfo.MaxDepth,
							MaxStringContentLength = endpointInfo.MaxStringContentLength,
							MaxArrayLength = endpointInfo.MaxArrayLength
						}
					},
					new HttpsTransportBindingElement
					{
						MaxReceivedMessageSize = endpointInfo.MaxMessageSize,
						MaxBufferSize = endpointInfo.MaxMessageSize.ToInt32(),
						MaxBufferPoolSize = endpointInfo.MaxBufferPoolSize,
						TransferMode = endpointInfo.TransferDataMode
					}
				}
			};
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x00080FC3 File Offset: 0x0007F1C3
		// (set) Token: 0x060023ED RID: 9197 RVA: 0x00080FCB File Offset: 0x0007F1CB
		public Binding Binding { get; private set; }

		// Token: 0x060023EE RID: 9198 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBehaviors(ServiceEndpoint endpoint)
		{
		}
	}
}
