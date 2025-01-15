using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200048D RID: 1165
	public class NetTcpBindingData : IBindingData
	{
		// Token: 0x060023F5 RID: 9205 RVA: 0x000810C4 File Offset: 0x0007F2C4
		public NetTcpBindingData(EndpointInfo endpointInfo)
		{
			NetTcpBinding netTcpBinding = new NetTcpBinding((SecurityMode)Enum.Parse(typeof(SecurityMode), endpointInfo.SecurityMode.ToString()), false)
			{
				ReaderQuotas = 
				{
					MaxDepth = endpointInfo.MaxDepth,
					MaxStringContentLength = endpointInfo.MaxStringContentLength,
					MaxArrayLength = endpointInfo.MaxArrayLength
				}
			};
			long maxMessageSize = endpointInfo.MaxMessageSize;
			netTcpBinding.MaxReceivedMessageSize = maxMessageSize;
			netTcpBinding.MaxBufferSize = maxMessageSize.ToInt32();
			if (endpointInfo.ReliableSessionEnabled)
			{
				netTcpBinding.ReliableSession.Enabled = true;
				netTcpBinding.ReliableSession.Ordered = endpointInfo.ReliableSessionOrderedMessages;
			}
			if (!string.IsNullOrEmpty(endpointInfo.ClientCredentialType))
			{
				TcpClientCredentialType tcpClientCredentialType = (TcpClientCredentialType)Enum.Parse(typeof(TcpClientCredentialType), endpointInfo.ClientCredentialType, true);
				netTcpBinding.Security.Transport.ClientCredentialType = tcpClientCredentialType;
			}
			netTcpBinding.TransferMode = endpointInfo.TransferDataMode;
			netTcpBinding.MaxBufferPoolSize = endpointInfo.MaxBufferPoolSize;
			netTcpBinding.MaxConnections = Math.Max(netTcpBinding.MaxConnections, endpointInfo.MaxConnections);
			this.Binding = netTcpBinding;
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x000811E8 File Offset: 0x0007F3E8
		// (set) Token: 0x060023F7 RID: 9207 RVA: 0x000811F0 File Offset: 0x0007F3F0
		public Binding Binding { get; private set; }

		// Token: 0x060023F8 RID: 9208 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBehaviors(ServiceEndpoint endpoint)
		{
		}
	}
}
